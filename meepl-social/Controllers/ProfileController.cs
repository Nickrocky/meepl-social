using Meepl.API;
using Meepl.Managers;
using Meepl.Models;
using Meepl.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meepl.Controllers;

[AllowAnonymous]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("profile")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Looks to see if a name is taken
    /// </summary>
    /// <param name="username">The username you are attempting to select</param>
    /// <returns>If the name is taken or not</returns>
    [HttpGet]
    [Route("personal")]
    public async Task<ActionResult<TableboundProfile>> GetPersonalProfile()
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        Console.WriteLine("Attempting to check the following tablebound account: " + identity);
       // var profile = await ProfileManager.GetPersonalProfile(identity);
        //Console.WriteLine("Found account: " + profile.Username + " with the identifier: " + profile.TableboundIdentifier.Value);
        //return Ok (profile);
        return Ok("Test message");
    }

}