using Meepl.API.MercurialBlobs;
using Meepl.API.MercurialBlobs.Responses;
using Meepl.Managers;
using Meepl.Models;
using Meepl.Util;
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
    private readonly FriendManager _friendManager;

    public FriendController(ILogger<FriendController> logger, FriendManager friendManager)
    {
        _logger = logger;
        _friendManager = friendManager;
    }

    /// <summary>
    /// Retrieves the list of friends for a user.
    /// </summary>
    [HttpGet("friends")]
    public async Task<ActionResult<byte[]>> GetFriendsAsync(ulong userId)
    {
        _logger.LogInformation("Fetching friends list for user: {UserId}", userId);

        if (!_friendManager.IsValidUserID(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return File(new PersonListResponse()
            {
                Msg = ErrorCodes.FRIENDLIST_RETRIEVAL_INVALID_USER,
                PersonListBlob = new PersonListBlob()
            }.GetBytes(), "application/octet-stream");
        }

        var result = await _friendManager.GetFriendsAsync(userId);
        PersonListResponse response = new PersonListResponse()
        {
            PersonListBlob = result,
            Msg = ErrorCodes.FRIENDLIST_RETRIEVAL_SUCCESS
        };

        return File(response.GetBytes(), "application/octet-stream");
    }

    /// <summary>
    /// Retrieves the list of blocked users for a user.
    /// </summary>
    [HttpGet("blocked")]
    public async Task<ActionResult<BlockedUsers_Lookup>> GetBlockedUsersAsync(ulong userId)
    {
        _logger.LogInformation("Fetching blocked users for user: {UserId}", userId);

        if (!_friendManager.IsValidUserID(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var result = await _friendManager.GetBlockedUsersAsync(userId);
        if (result == null)
        {
            _logger.LogWarning("No blocked users found for user: {UserId}", userId);
            return NotFound(new { Msg = "No blocked users found." });
        }

        return Ok(result);
    }

    /// <summary>
    /// Retrieves the incoming and outgoing friend requests for a user.
    /// </summary>
    [HttpGet("requests/{userId}")]
    public async Task<ActionResult<FriendRequests_Lookup>> GetFriendRequestsAsync(ulong userId)
    {
        _logger.LogInformation("Fetching friend requests for user: {UserId}", userId);

        if (!_friendManager.IsValidUserID(userId))
        {
            _logger.LogWarning("Invalid user ID: {UserId}", userId);
            return BadRequest(new { Msg = "Invalid user ID." });
        }

        var result = await _friendManager.GetFriendRequestsAsync(userId);
        if (result == null)
        {
            _logger.LogWarning("No friend requests found for user: {UserId}", userId);
            return NotFound(new { Msg = "No friend requests found." });
        }

        return Ok(result);
    }

    /// <summary>
    /// Handles a request to add a new friend.
    /// </summary>
    [HttpPost("add")]
    public async Task<ActionResult> AddFriendAsync([FromBody] FriendRequest request)
    {
        _logger.LogInformation("Processing friend addition request from {RequesterId} to {FriendId}", request.RequesterId, request.FriendId);

        if (!_friendManager.IsValidUserID(request.RequesterId) || 
            !_friendManager.IsValidUserID(request.FriendId))
        {
            _logger.LogWarning("Invalid requester or friend ID: {RequesterId}, {FriendId}", request.RequesterId, request.FriendId);
            return BadRequest(new { Msg = "Invalid requester or friend ID." });
        }

        var success = await _friendManager.AddFriendAsync(request.RequesterId, request.FriendId);
        if (success == null)
        {
            _logger.LogWarning("Failed to add friend for {RequesterId}", request.RequesterId);
            return StatusCode(500, new { Msg = "Failed to add friend." });
        }

        _logger.LogInformation("Successfully added friend for {RequesterId}", request.RequesterId);
        return Ok(new { Msg = "Friend added successfully." });
    }
    
    /// <summary>
    /// Handles a blocking a user
    /// </summary>
    [HttpPost("block")]
    public async Task<ActionResult<byte[]>> BlockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        //_logger.LogInformation("Fetching friends list for user: {requesterId}", requesterId);

        if (!_friendManager.IsValidUserID(requesterId))
        {
            _logger.LogWarning("Invalid user ID: {requesterId}", requesterId);
            return File(new PersonListResponse()
            {
                Msg = ErrorCodes.BLOCK_USER_INVALID_USER,
                PersonListBlob = new PersonListBlob()
            }.GetBytes(), "application/octet-stream");
        }

        var result = await _friendManager.BlockUserAsync(requesterId, blockedUserId);
        PersonListResponse response = new PersonListResponse()
        {
            PersonListBlob = result,
            Msg = ErrorCodes.BLOCK_USER_SUCCESS
        };

        return File(response.GetBytes(), "application/octet-stream");
    }
}
