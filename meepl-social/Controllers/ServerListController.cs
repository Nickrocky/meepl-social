using Meepl.API.MercurialBlobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meepl.Controllers;

[AllowAnonymous]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("social/serverlist")]
public class ServerListController : ControllerBase
{
    private readonly ILogger<ServerListController> _logger;

    public ServerListController(ILogger<ServerListController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("web")]
    public Task<ActionResult<ServerListBlob>> GetServerList()
    {
        
    }

    [HttpGet]
    [Route("game")]
    public Task<ActionResult<byte[]>> GetServerListGame()
    {
        
    }

}