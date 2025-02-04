using Meepl.API;
using Meepl.Models;
using Meepl.Social.Interfaces;
using Meepl.Util;


namespace Meepl.Managers;

public class FriendManager : IFriendManager
{
    private static FriendManager instance;
    private static ISQLManager SQLManagerProvider;

    public static FriendManager Get()
    {
        return instance;
    }
    public async Task Init(ISQLManager sqlManager)
    {
        instance = this;
        SQLManagerProvider = sqlManager;
    }
    public bool IsValidUserID(ulong userID)
    {
        return (!TableboundIdentifier.Parse(userID).IsEmpty());
    }

    public async Task<List<ulong>> GetFriendsAsync(ulong userId)
    {
        return await SQLManagerProvider.
        //return new List<ulong>();
    }

    public async Task<List<ulong>> GetBlockedUsersAsync(ulong userId)
    {
        var blocked = await GetBlockedUsersAsync(userId);
        return new List<ulong>(blocked);
    }

    public async Task<List<ulong>> GetFriendRequestsAsync(ulong userId)
    {
        var requests = await GetFriendRequestsAsync(userId);
        return new List<ulong>(requests);
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