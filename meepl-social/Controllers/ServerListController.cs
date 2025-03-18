using Meepl.API;
using Meepl.API.MercurialBlobs;
using Meepl.API.MercurialBlobs.Responses;
using Meepl.Managers;
using Meepl.Util;
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
    [Route("get")]
    public async Task<ActionResult<byte[]>> GetServerList()
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        if(identity == 0) return File(
            new ServerListResponse()
        {
            ServerListBlob = new ServerListBlob(),
            Message = ErrorCodes.PROFILE_INVALID_PROFILE
        }.GetBytes(), "application/octet-stream");
        
        Console.WriteLine("Attempting to check the following tablebound account: " + identity);
        var profile = await ProfileManager.GetProfile(identity);
        Console.WriteLine("Found account: " + profile.Username + " with the identifier: " + profile.MeeplIdentifier.Container);
        var serverListBlob = await ServerListManager.Get().GetServerList(MeeplIdentifier.Parse(identity));
        
        return File(new ServerListResponse()
        {
            ServerListBlob = serverListBlob,
            Message = ErrorCodes.SERVERLIST_RETRIEVAL_SUCCESS
        }.GetBytes(), "application/octet-stream");
    }

    
    [HttpPost]
    [Route("add")]
    public async Task<ActionResult<byte[]>> AddToServerList(string ipAddress, ushort port)
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        if(identity == 0) return File(
            new ServerListActionResponse()
            {
                Msg = ErrorCodes.PROFILE_INVALID_PROFILE
            }.GetBytes(), "application/octet-stream");
        
        Console.WriteLine("Attempting to check the following tablebound account: " + identity);
        var profile = await ProfileManager.GetProfile(identity);
        Console.WriteLine("Found account: " + profile.Username + " with the identifier: " + profile.MeeplIdentifier.Container);

        var serverEntry = new ServerEntry()
        {
            IPAddress = ipAddress,
            Port = port
        };
        var meeplID = MeeplIdentifier.Parse(identity);
        
        await ServerListManager.Get().AddServerToList(meeplID, serverEntry);
        
        return File(new ServerListActionResponse()
        {
            Msg = ErrorCodes.SERVERLIST_ACTION_SUCCESS
        }.GetBytes(), "application/octet-stream");
    }

    [HttpPost]
    [Route("remove")]
    public Task<ActionResult<byte[]>> RemoveFromServerList()
    {
        throw new NotImplementedException();
    }

}