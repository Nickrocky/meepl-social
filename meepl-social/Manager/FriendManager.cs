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
    private static Dictionary<ulong, List<ulong>> FriendRequestCache = new Dictionary<ulong, List<ulong>>();

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
        return (!MeeplIdentifier.Parse(userID).IsEmpty());
    }
    /// <summary>
    /// Retrieves User's Friend List from Cache
    /// </summary>
    /// <param name="userId"></param>
    ///
    /// <returns></returns>
    public async Task<PersonListBlob> GetFriendsAsync(ulong userId)
    {
        if (FriendsListCache.ContainsKey(userId)) return FriendsListCache[userId];
        var friendList = await SQLManagerProvider.GetFriendList(userId);
        FriendsListCache.Add(userId, friendList);
        return friendList;
    }
    /// <summary>
    /// Retrieves User's Blocked List from Cache
    /// </summary>
    /// <param name="requesterId"></param>
    /// <returns></returns>
    public async Task<PersonListBlob> GetBlockedUsersAsync(ulong userId)
    {
        
        if (BlockedListCache.ContainsKey(userId)) return BlockedListCache[userId];
        var blockList = await SQLManagerProvider.GetBlockList(userId);
        BlockedListCache.Add(userId, blockList);
        return blockList;
    }

    public async Task<List<ulong>> GetFriendRequestsAsync(ulong userId)
    {
        if(FriendRequestCache.ContainsKey(userId)) return FriendRequestCache[userId];
        List<ulong> friendRequestList = await SQLManagerProvider.GetFriendList(userId);
        FriendRequestCache.Add(userId,friendRequestList);

    public async Task<bool> AddFriendAsync(ulong requesterId, ulong friendId)
    {
        return await Task.FromResult(!MeeplIdentifier.Parse(friendId).IsEmpty());
    }

    public async Task<bool> RemoveFriendAsync(ulong requesterId, ulong friendId)
    {
        await RemoveFriendAsync(requesterId, friendId);
        return await Task.FromResult(true);
    }

    /// <summary>
    /// Uses the BlockedListCache to add user to Block List
    /// </summary>
    /// <param name="requesterId"></param>
    /// <param name="blockedUserId"></param>
    /// <notes>Nick double check that this method is adding the blockedUserId to the blocked list and not just giving the blocked user the requesters blocked list</notes>
    /// <returns></returns>
    public async Task<PersonListBlob> BlockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        if (BlockedListCache.ContainsKey(requesterId)) return BlockedListCache[requesterId];
        var blockList = await SQLManagerProvider.GetBlockList(requesterId);
        BlockedListCache.Add(requesterId, blockList);
        return blockList;
    }

    /// <summary>
    /// Uses the BlockedListCache to remove user to Block List
    /// </summary>
    /// <param name="requesterId"></param>
    /// <param name="blockedUserId"></param>
    /// <notes>Nick double check that this method is adding the blockedUserId to the blocked list and not just giving the blocked user the requesters blocked list</notes>
    /// <returns></returns>
    public async Task<PersonListBlob> UnblockUserAsync(ulong requesterId, ulong blockedUserId)
    {
        if (BlockedListCache.ContainsKey(requesterId)) return BlockedListCache[requesterId];
        var blockList = await SQLManagerProvider.GetBlockList(requesterId);
        BlockedListCache.Add(blockedUserId, blockList);
        return blockList;
    }
}