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
    [HttpGet("friends/{userId}")]
    public async Task<ActionResult<FriendList_Lookup>> GetFriendsAsync(ulong userId)
    {
        _logger.LogInformation("Fetching friends list for user: {UserId}", userId);

        if (!_friendService.IsValidUserID(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var result = await _friendService.GetFriendsAsync(userId);
        if (!result.IsSuccess || result.Value == null)
        {
            _logger.LogWarning("No friends found for user: {UserId}", userId);
            return NotFound(new { Msg = "No friends found." });
        }

        return Ok(new FriendList_Lookup
        {
            UserId = userId,
            Friends = result.Value
        });
    }

    /// <summary>
    /// Retrieves the list of blocked users for a user.
    /// </summary>
    [HttpGet("blocked/{userId}")]
    public async Task<ActionResult<BlockedUsers_Lookup>> GetBlockedUsersAsync(ulong userId)
    {
        _logger.LogInformation("Fetching blocked users for user: {UserId}", userId);

        if (!_friendService.IsValidUserID(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var result = await _friendService.GetBlockedUsersAsync(userId);
        if (!result.IsSuccess || result.Value == null)
        {
            _logger.LogWarning("No blocked users found for user: {UserId}", userId);
            return NotFound(new { Msg = "No blocked users found." });
        }

        return Ok(new BlockedUsers_Lookup
        {
            UserId = userId,
            BlockedUsers = result.Value
        });
    }

    /// <summary>
    /// Retrieves the incoming and outgoing friend requests for a user.
    /// </summary>
    [HttpGet("requests/{userId}")]
    public async Task<ActionResult<FriendRequests_Lookup>> GetFriendRequestsAsync(ulong userId)
    {
        _logger.LogInformation("Fetching friend requests for user: {UserId}", userId);

        if (!_friendService.IsValidUserID(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var result = await _friendService.GetFriendRequestsAsync(userId);
        if (!result.IsSuccess || result.Value == null)
        {
            _logger.LogWarning("No friend requests found for user: {UserId}", userId);
            return NotFound(new { Msg = "No friend requests found." });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Handles a request to add a new friend.
    /// </summary>
    [HttpPost("add")]
    public async Task<ActionResult> AddFriendAsync([FromBody] FriendRequest request)
    {
        _logger.LogInformation("Processing friend addition request from {RequesterId} to {FriendId}", request.RequesterId, request.FriendId);

        if (!_friendService.IsValidUserID(request.RequesterId) || 
            !_friendService.IsValidUserID(request.FriendId))
        {
            _logger.LogWarning("Invalid requester or friend ID: {RequesterId}, {FriendId}", request.RequesterId, request.FriendId);
            return BadRequest(new { Msg = "Invalid requester or friend ID." });
        }

        var success = await _friendService.AddFriendAsync(request.RequesterId, request.FriendId);
        if (!success)
        {
            _logger.LogWarning("Failed to add friend for {RequesterId}", request.RequesterId);
            return StatusCode(500, new { Msg = "Failed to add friend." });
        }

        _logger.LogInformation("Successfully added friend for {RequesterId}", request.RequesterId);
        return Ok(new { Msg = "Friend added successfully." });
    }
}
