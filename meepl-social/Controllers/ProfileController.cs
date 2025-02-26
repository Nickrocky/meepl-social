using Meepl.API;
using Meepl.API.MercurialBlobs;
using Meepl.API.MercurialBlobs.Responses;
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
[Route("social/profile")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    [Route("game")]
    public async Task<ActionResult<byte[]>> GetPersonalProfile()
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        if(identity == 0) return File(new PersonalProfileResponse()
        {
            Profile = new MeeplProfile(),
            Message = ErrorCodes.PROFILE_INVALID_PROFILE
        }.GetBytes(), "application/octet-stream");
        Console.WriteLine("Attempting to check the following tablebound account: " + identity);
        var profile = await ProfileManager.GetProfile(identity);
        Console.WriteLine("Found account: " + profile.Username + " with the identifier: " + profile.MeeplIdentifier.Container);
        return File(new PersonalProfileResponse()
        {
            Profile = profile,
            Message = ErrorCodes.PROFILE_PERSONAL_RETRIEVAL_SUCCESS
        }.GetBytes(), "application/octet-stream");
    }
    
    public async Task<ActionResult<byte[]>> UpdateBio()
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<byte[]>> UpdateAction()
    {
        throw new NotImplementedException();

    }

    /// <summary>
    /// Looks to see if a name is taken
    /// </summary>
    /// <param name="username">The username you are attempting to select</param>
    /// <returns>If the name is taken or not</returns>
    /*public async Task<ActionResult<byte[]>> UpdateUsername()
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        if(identity == 0) return File(new ProfileUpdateResponse()
        {
            Msg = ErrorCodes.PROFILE_INVALID_PROFILE
        }.GetBytes(), "application/octet-stream");
        if(!await ProfileManager.CheckForUsernameOnForceChangeList(identity)) return File(new ProfileUpdateResponse()
        {
            Msg = ErrorCodes.PROFILE_NOT_ON_USERNAME_CHANGE_LIST
        }.GetBytes(), "application/octet-stream");
        
    }*/

    public async Task<ActionResult<byte[]>> UpdateProfilePicture()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates a players status
    /// </summary>
    /// <param name="s">The status indicator you want to update your profile to</param>
    /// <returns>Update response</returns>
    public async Task<ActionResult<byte[]>> UpdateStatus(byte s)
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        if(identity == 0) return File(new ProfileUpdateResponse()
        {
            Msg = ErrorCodes.PROFILE_INVALID_PROFILE
        }.GetBytes(), "application/octet-stream");

        var profile = await ProfileManager.GetProfile(identity);
        ProfileManager.SetStatusIndicator(ref profile, (StatusIndicator) s);
        return File(new ProfileUpdateResponse()
        {
            Msg = ErrorCodes.PROFILE_UPDATED_STATUS_INDICATOR_SUCCESSFULLY
        }.GetBytes(), "application/octet-stream");
    }
    
    [HttpGet]
    [Route("web")]
    public async Task<ActionResult<MeeplProfile>> GetPersonalProfileAsync()
    {
        var identity = HttpUtils.ParseIdentityClaim(HttpContext);
        if (identity == 0)
            return Ok(new PersonalProfileResponse()
            {
                Profile = new MeeplProfile(),
                Message = ErrorCodes.PROFILE_INVALID_PROFILE
            });
        Console.WriteLine("Attempting to check the following tablebound account: " + identity);
        var profile = await ProfileManager.GetProfile(identity);
        return Ok(profile);
    }
    
}