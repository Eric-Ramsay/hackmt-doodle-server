using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;

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
    [Route("/players/scores")]
    public async Task<ActionResult> ListScores()
    {
        ListScoresResponse response = new ListScoresResponse();
        foreach (var player in gameState.players)
        {
            response.scores[player.clientId] = player.playerScore;
        }
        return StatusCode(200, response.scores);
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

