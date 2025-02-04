using Meepl.API;
using Meepl.API.MercurialBlobs;
using Meepl.Models;
using Meepl.Social.Interfaces;
using Meepl.Util;


namespace Meepl.Managers;

public class FriendManager : IFriendManager
{
    private static FriendManager instance;
    private static ISQLManager SQLManagerProvider;
    private static Dictionary<ulong, PersonListBlob> FriendsListCache = new Dictionary<ulong, PersonListBlob>();
    private static Dictionary<ulong, PersonListBlob> BlockedListCache = new Dictionary<ulong, PersonListBlob>();

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

    public async Task<PersonListBlob> GetFriendsAsync(ulong userId)
    {
        if (FriendsListCache.ContainsKey(userId)) return FriendsListCache[userId];
        var friendList = await SQLManagerProvider.GetFriendList(userId);
        FriendsListCache.Add(userId, friendList);
        return friendList;
    }

    public async Task<PersonListBlob> GetBlockedUsersAsync(ulong userId)
    {
        var blocked = await GetBlockedUsersAsync(userId);
        //return new List<ulong>(blocked);
        throw new NotImplementedException();
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