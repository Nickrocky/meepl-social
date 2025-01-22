using Meepl.Models;
using Meepl.Social.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meepl.Controllers;

/// <summary>
/// Controller for managing social connections (friends, blocks, friend requests).
/// </summary>
[ApiController]
[Route("social")]
public class FriendController : ControllerBase
{
    private readonly ILogger<FriendController> _logger;
    private readonly IFriendService _friendService;

    public FriendController(ILogger<FriendController> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    /// <summary>
    /// Retrieves the list of friends for a user.
    /// </summary>
    /// <param name="userId">The unique TableboundIdentifier for the user.</param>
    /// <returns>A structured response with the user's friends list.</returns>
    [HttpGet]
    [Route("friends/{userId}")]
    public ActionResult<FriendList_Lookup> GetFriends(ulong userId)
    {
        _logger.LogInformation("Fetching friends list for user: {UserId}", userId);

        if (!_friendService.IsValidUserId(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var friends = _friendService.GetFriends(userId);
        if (friends == null)
        {
            _logger.LogWarning("No friends found for user: {UserId}", userId);
            return NotFound(new { Msg = "No friends found." });
        }

        return Ok(new FriendList_Lookup
        {
            UserId = userId,
            Friends = friends
        });
    }

    /// <summary>
    /// Retrieves the list of blocked users for a user.
    /// </summary>
    /// <param name="userId">The unique TableboundIdentifier for the user.</param>
    /// <returns>A structured response with the user's blocked users list.</returns>
    [HttpGet]
    [Route("blocked/{userId}")]
    public ActionResult<BlockedUsers_Lookup> GetBlockedUsers(ulong userId)
    {
        _logger.LogInformation("Fetching blocked users for user: {UserId}", userId);

        if (!_friendService.IsValidUserId(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var blockedUsers = _friendService.GetBlockedUsers(userId);
        if (blockedUsers == null)
        {
            _logger.LogWarning("No blocked users found for user: {UserId}", userId);
            return NotFound(new { Msg = "No blocked users found." });
        }

        return Ok(new BlockedUsers_Lookup
        {
            UserId = userId,
            BlockedUsers = blockedUsers
        });
    }

    /// <summary>
    /// Retrieves the incoming and outgoing friend requests for a user.
    /// </summary>
    /// <param name="userId">The unique TableboundIdentifier for the user.</param>
    /// <returns>A structured response with the user's friend requests.</returns>
    [HttpGet]
    [Route("requests/{userId}")]
    public ActionResult<FriendRequests_Lookup> GetFriendRequests(ulong userId)
    {
        _logger.LogInformation("Fetching friend requests for user: {UserId}", userId);

        if (!_friendService.IsValidUserId(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var requests = _friendService.GetFriendRequests(userId);
        if (requests == null)
        {
            _logger.LogWarning("No friend requests found for user: {UserId}", userId);
            return NotFound(new { Msg = "No friend requests found." });
        }

        return Ok(requests);
    }

    /// <summary>
    /// Handles a request to add a new friend.
    /// </summary>
    /// <param name="request">The details of the friend addition request.</param>
    /// <returns>A status message indicating success or failure.</returns>
    [HttpPost]
    [Route("add")]
    public ActionResult AddFriend([FromBody] FriendRequest request)
    {
        _logger.LogInformation("Processing friend addition request from {RequesterId} to {FriendId}", request.RequesterId, request.FriendId);

        if (!_friendService.IsValidUserId(request.RequesterId) || !_friendService.IsValidUserId(request.FriendId))
        {
            _logger.LogWarning("Invalid requester or friend ID: {RequesterId}, {FriendId}", request.RequesterId, request.FriendId);
            return BadRequest(new { Msg = "Invalid requester or friend ID." });
        }

        var success = _friendService.AddFriend();
        if (!success)
        {
            _logger.LogWarning("Failed to add friend for {RequesterId}", request.RequesterId);
            return StatusCode(500, new { Msg = "Failed to add friend." });
        }

        _logger.LogInformation("Successfully added friend for {RequesterId}", request.RequesterId);
        return Ok(new { Msg = "Friend added successfully." });
    }
}
