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

    public Task<Result<List<ulong>>> GetFriendsAsync(ulong userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<ulong>>> GetBlockedUsersAsync(ulong userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<ulong>>> GetFriendRequestsAsync(ulong userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddFriendAsync(ulong requesterId, ulong friendId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveFriendAsync(ulong requesterId, ulong friendId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> BlockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UnblockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        throw new NotImplementedException();
    }
}