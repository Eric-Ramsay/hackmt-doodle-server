using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Server;

[Route("api/")]
public class Players : ControllerBase
{
    public Players()
    {

    }

    //cli.Post("/players/1") <---C++

    [HttpPost]
    [Route("/players/{clientId}")]
    public async Task<ActionResult> CreateNewPlayer([FromBody] PlayerCreateRequest body, string clientId){
        Models player = new Models();

        Console.Write(string.Format("client id: {0} data: {1}\n", clientId, body.name));

        player.clientId = body.clientId;
        player.name = body.name;

        return StatusCode(200);

    }
    [HttpPost]
    [Route("/players/sendword/{word}")]
    public async Task<ActionResult> SendWord(string word)
    {
        return StatusCode(200);
    }

    [HttpPost]
    [Route("/players/send-drawing-data/{drawingData}")]
    public async Task<ActionResult> SendDrawingData(Draw drawingData)
    {
        return StatusCode(200);
    }


    [HttpGet]
    [Route("/players/getgamestate/")]
    public async Task<ActionResult> GetGameState()
    {
        GameState gameState = new GameState();
        return StatusCode(200, gameState); 
    }

}

