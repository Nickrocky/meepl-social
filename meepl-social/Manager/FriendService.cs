using Meepl.API;
using Meepl.Models;
using Meepl.Social.Interfaces;
using Meepl.Util;

namespace Meepl.Managers;

public class FriendService : IFriendService
{
    public async Task<bool> IsValidUserIdAsync(ulong userId)
    {
        return await Task.FromResult(!TableboundIdentifier.Parse(userId).IsEmpty());

    }

    public async Task<Result<List<ulong>>> GetFriendsAsync(ulong userId)
    {
        var friends = await GetFriendsAsync(userId);
        return new Result<List<ulong>>(friends);
    }

    public async Task<Result<List<ulong>>> GetBlockedUsersAsync(ulong userId)
    {
        var blocked = await GetBlockedUsersAsync(userId);
        return new Result<List<ulong>>(blocked);
    }

    public async Task<Result<List<ulong>>> GetFriendRequestsAsync(ulong userId)
    {
        var requests = await GetFriendRequestsAsync(userId);
        return new Result<List<ulong>>(requests);
    }

    public async Task<bool> AddFriendAsync(ulong requesterId, ulong friendId)
    {
        return await Task.FromResult(!TableboundIdentifier.Parse(friendId).IsEmpty());
    }

    public async Task<bool> RemoveFriendAsync(ulong requesterId, ulong friendId)
    {
        await RemoveFriendAsync(requesterId, friendId);
        return await Task.FromResult(true);
    }

    public async Task<bool> BlockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        await BlockUserAsync(requesterId, blockedUserId);
        return await Task.FromResult(true);
    }

    public async Task<bool> UnblockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        await UnblockUserAsync(requesterId, blockedUserId);
        return await Task.FromResult(true);
    }
}