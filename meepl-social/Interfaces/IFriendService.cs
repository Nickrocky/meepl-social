using Meepl.API;
using Meepl.Models;
using Meepl.Util;

namespace Meepl.Social.Interfaces;

/// <summary>
/// Interface for managing friend-related operations.
/// </summary>
public interface IFriendService
{
    /// <summary>
    /// Determines if a user ID is valid.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that resolves to a boolean indicating validity.</returns>
    Task<bool> IsValidUserIdAsync(ulong userId);
    


    /// <summary>
    /// Retrieves the friends list of a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that resolves to a list of friends.</returns>
    Task<Result<List<ulong>>> GetFriendsAsync(ulong userId);

    /// <summary>
    /// Retrieves the list of blocked users for a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that resolves to a list of blocked users.</returns>
    Task<Result<List<ulong>>> GetBlockedUsersAsync(ulong userId);

    /// <summary>
    /// Retrieves the friend requests for a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that resolves to a list of friend requests.</returns>
    Task<Result<List<ulong>>> GetFriendRequestsAsync(ulong userId);

    /// <summary>
    /// Adds a friend for the user.
    /// </summary>
    /// <param name="requesterId">The ID of the user sending the request.</param>
    /// <param name="friendId">The ID of the user to add as a friend.</param>
    /// <returns>A task that resolves to a boolean indicating success.</returns>
    Task<bool> AddFriendAsync(ulong requesterId, ulong friendId);

    /// <summary>
    /// Removes a friend for the user.
    /// </summary>
    /// <param name="requesterId">The ID of the user removing the friend.</param>
    /// <param name="friendId">The ID of the friend to remove.</param>
    /// <returns>A task that resolves to a boolean indicating success.</returns>
    Task<bool> RemoveFriendAsync(ulong requesterId, ulong friendId);

    /// <summary>
    /// Blocks a user.
    /// </summary>
    /// <param name="requesterId">The ID of the user performing the block.</param>
    /// <param name="blockedUserId">The ID of the user to block.</param>
    /// <returns>A task that resolves to a boolean indicating success.</returns>
    Task<bool> BlockUserAsync(ulong requesterId, ulong blockedUserId);

    /// <summary>
    /// Unblocks a user.
    /// </summary>
    /// <param name="requesterId">The ID of the user performing the unblock.</param>
    /// <param name="blockedUserId">The ID of the user to unblock.</param>
    /// <returns>A task that resolves to a boolean indicating success.</returns>
    Task<bool> UnblockUserAsync(ulong requesterId, ulong blockedUserId);
}
