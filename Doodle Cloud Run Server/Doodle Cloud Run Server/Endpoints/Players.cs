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

    /*send json:
    {
    "name": "(name)"
    }
    returns:
    {

    */
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
        gameState.drawerID = gameState.turn - 1;
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
        if(gameState.drawerID != clientId)
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
        response.drawerId = gameState.drawerID;

        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("/players/send-drawing-data/")]
    public async Task<ActionResult> SendDrawingData( [FromBody] Actions drawingData)
    {
        return StatusCode(200);
    }

    public string HideWord(string startWord)
    {
        return startWord;
    }

    [HttpGet]
    [Route("/players/getgamestate/{userID}/{actionCount}/{chatCount}")]
    public async Task<ActionResult> GetGameState(int userID, int actionCount, int chatCount)
    {
        GameStateResponse response = new GameStateResponse();

        response.RoundNumber = gameState.round;
        response.Word = gameState.currentWord;
        response.drawerID = gameState.drawerID;
        if (actionCount < gameState.actions.Count)
        {
            response.NewActions = gameState.actions.GetRange(actionCount, gameState.actions.Count - actionCount);
        }
        if (chatCount < gameState.chat.Count)
        {
            response.NewMessages = gameState.chat.GetRange(chatCount, gameState.actions.Count - chatCount);
        }

        return StatusCode(200, response);

        /*if ((DateTime.Now - gameState.StartTimestamp).TotalSeconds >= 60)
        {
            gameState.StartTimestamp = DateTime.Now;
            gameState.round++;
            gameState.actions.Clear();
            gameState.chat.Clear();
            // pick new word, add to gamestate
            // give users what they're missing
            // pick new guesser
            // send guesser info
            // send non-guesser info
        }
        else
        {
            if (userID == gameState.drawerID)
            {
                // see if user is guesser
                //if guesser

                // else
            }
        }*/

        //return response;
    }

    [HttpPost]
    [Route("/players/guess/")]
    public async Task<ActionResult> getGuess([FromBody] GuessCreateRequest body, int userId)
    {
        Message message = new Message(); 
        message.guess = body.guess;
        message.userId = userId;
     
        if (message.guess == gameState.currentWord)
        {
            message.correct = true;
            message.guess = " guessed the word.";

        }
        else
        {
            message.correct = false;
            
        }
        gameState.chat.Add(message);
        //server updates chat based on correctness.



        return StatusCode(200, message);
    }

   
}

[HttpPost]
[Route("/state/word")]
public async Task<ActionResult> CreateNewPlayer([FromBody] WordChoiceRequest body){

    if (body.clientId == gameState.drawerID) {
        gameState.currentWord = body.word;
        //gameState.StartTimestamp = DateTime.Now();
    }
    
    return StatusCode(200);
}