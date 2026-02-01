using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.IO;

namespace Server;

[Route("api/")]
public class Players : ControllerBase
{
    private static GameState gameState;
    static Players()
    {
        gameState = new GameState();
    }

    //cli.Post("/players/1") <---C++

    [HttpPost]
    [Route("/players")]
    public async Task<ActionResult> CreateNewPlayer([FromBody] PlayerCreateRequest body){
        Client player = new Client();
        player.clientId = gameState.players.Count;
        player.connected = true;
        gameState.players.Add(player);
        
        PlayerCreateResponse response = new PlayerCreateResponse();
        response.clientId = player.clientId;

        return StatusCode(200, response);

    }
    [HttpGet]
    [Route("/state/transition")]
    public async Task<ActionResult> GetRoundTransition()
    {
        // Pick a drawer
        Client drawer = new Client();
        do
        {
            if(gameState.turn < gameState.players.Count)
            {
                drawer = gameState.players[gameState.turn];
                gameState.turn += 1;
            } else
            {
                gameState.turn = 0;
            }
        } while (drawer.connected != true);
        // Drawer picks word
        TransitionResponse response = new TransitionResponse();
        gameState.drawerId = gameState.turn - 1;
        response.drawerId = gameState.turn - 1;
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Shared.Next(gameState.randomWords.Count);
            response.chosenWords.Add(gameState.randomWords[randomIndex]);
        }

        foreach (var player in gameState.players)
        {
            response.scores[player.clientId] = player.playerScore;
        }

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("/state/round-start/{clientId}")]
    public async Task<ActionResult> RoundStart([FromBody] RoundStartRequest body, int clientId)
    {
        if(gameState.drawerId != clientId)
        {
            //Throw error here
        }
        RoundStartResponse response = new RoundStartResponse();
        response.uncensoredWord = body.word;
        gameState.currentWord = body.word;
        response.censoredWord = "";
        // Censor the word for guessers
        for(int i = 0; i < response.uncensoredWord.Length; ++i)
        {
            if(response.uncensoredWord[i] != ' ')
            {
                response.censoredWord += '_';
            }
            else
            {
                response.censoredWord += response.uncensoredWord[i];
            }
        }
        response.drawerId = gameState.drawerId;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("/players/send-drawing-data/{drawingData}")]
    public async Task<ActionResult> SendDrawingData(Draw drawingData)
    {
        return StatusCode(200);
    }

    [HttpGet]
    [Route("/players/getgamestate/{index}")]
    public async Task<ActionResult> GetGameState(string word)
    {
        GameStateResponse gameStateResponse = new GameStateResponse();
        gameStateResponse.drawing = gameState.drawing;
        gameStateResponse.currentWord = gameState.currentWord;
        gameStateResponse.round = gameState.round;

        return StatusCode(200, gameStateResponse); 
    }

    [HttpPost]
    [Route("/players/guess")]
    public async Task<ActionResult> getGuess([FromBody] GuessCreateRequest body)
    {
        string guess = body.guess;
        GuessResponse response = new GuessResponse();
        if (guess == gameState.currentWord)
        {
            response.correct = true;
        }
        else
        {
            response.correct = false;
        }
        //server updates chat based on correctness.



        return StatusCode(200, response);
    }

   
}

