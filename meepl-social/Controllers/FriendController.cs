using Meepl.API;
using Meepl.Managers;
//using Meepl.Models;
using Meepl.Util;
using Microsoft.AspNetCore.Mvc;

namespace Meepl.Controllers;

[ApiController]
[Route("social/friends")]

public class FriendController : ControllerBase
{
    private readonly ILogger<FriendController> _logger;

    public FriendController(ILogger<FriendController> logger)
    {
        _logger = logger;
    }
    
    
}