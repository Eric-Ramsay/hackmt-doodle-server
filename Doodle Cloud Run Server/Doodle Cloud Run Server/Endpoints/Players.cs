using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        player.clientId = clientId;
        player.name = body.data;

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

