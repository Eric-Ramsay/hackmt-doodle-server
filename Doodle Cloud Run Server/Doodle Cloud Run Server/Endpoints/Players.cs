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

    [HttpPost]
    [Route("/players/{clientId}")]
    public async Task<ActionResult> CreateNewPlayer([FromBody] PlayerCreateRequest body, string clientId){
        Player player = new Player();

        player.clientId = clientId;
        player.name = body.data;

        return StatusCode(200);

    }
}
