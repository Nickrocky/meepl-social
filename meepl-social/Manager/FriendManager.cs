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
    private static Dictionary<ulong, PersonListBlob> FriendRequestCache = new Dictionary<ulong, PersonListBlob>();

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

    public async Task<PersonListBlob> GetFriendRequestsAsync(ulong userId)
    {
        if (FriendRequestCache.ContainsKey(userId)) return FriendRequestCache[userId];
        var friendRequestList = await SQLManagerProvider.GetFriendRequestList(userId);
        FriendRequestCache.Add(userId, friendRequestList);
    }

    public async Task<PersonListBlob> AddFriendAsync(ulong requesterId, ulong friendId)
        {
            //await SQLManagerProvider.A
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFriendAsync(ulong requesterId, ulong friendId)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonListBlob> BlockUserAsync(ulong requesterId, ulong blockedUserId)
        {
            await SQLManagerProvider.UpdateBlockList(requesterId, blockedUserId);
            ;
        }

        /// <summary>
        /// Uses the BlockedListCache to remove user to Block List
        /// </summary>
        /// <param name="requesterId"></param>
        /// <param name="blockedUserId"></param>
        /// <returns></returns>
        public async Task<PersonListBlob> UnblockUserAsync(ulong requesterId, ulong blockedUserId)
        {
            if (BlockedListCache.ContainsKey(requesterId)) return BlockedListCache[requesterId];
            var blockList = await SQLManagerProvider.GetBlockList(requesterId);
            BlockedListCache.Add(blockedUserId, blockList);
            return blockList;
        }
    }
