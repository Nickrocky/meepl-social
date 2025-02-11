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
    private static Dictionary<ulong, List<FriendRequestBlob>> FriendRequestCache = new Dictionary<ulong, List<FriendRequestBlob>>();

    public static FriendManager Get()
    {
        return instance;
    }

    public async Task Init(ISQLManager sqlManager)
    {
        instance = this;
        SQLManagerProvider = sqlManager;
    }

    public bool IsValidUserID(ulong tableboundID)
    {
        return (!MeeplIdentifier.Parse(tableboundID).IsEmpty());
    }

    /// <summary>
    /// Retrieves User's Friend List from Cache
    /// </summary>
    /// <param name="tableboundID"></param>
    /// <returns></returns>
    public async Task<PersonListBlob> GetFriendsAsync(ulong tableboundID)
    {
        if (FriendsListCache.ContainsKey(tableboundID)) return FriendsListCache[tableboundID];
        var friendList = await SQLManagerProvider.GetFriendList(MeeplIdentifier.Parse(tableboundID));
        FriendsListCache.Add(tableboundID, friendList);
        return friendList;
    }

    /// <summary>
    /// Retrieves User's Blocked List from Cache
    /// </summary>
    /// <param name="requesterId"></param>
    /// <returns></returns>
    public async Task<PersonListBlob> GetBlockedUsersAsync(ulong tableboundID)
    {

        if (BlockedListCache.ContainsKey(tableboundID)) return BlockedListCache[tableboundID];
        var blockList = await SQLManagerProvider.GetBlockList(MeeplIdentifier.Parse(tableboundID));
        BlockedListCache.Add(tableboundID, blockList);
        return blockList;
    }

    public async Task<List<FriendRequestBlob>> GetFriendRequestsAsync(ulong tableboundID)
    {
        if (FriendRequestCache.ContainsKey(tableboundID)) return FriendRequestCache[tableboundID];
        var friendRequestList = await SQLManagerProvider.GetFriendRequestList(MeeplIdentifier.Parse(tableboundID));
        FriendRequestCache.Add(tableboundID, friendRequestList);
        return friendRequestList;
    }

        public async Task AddFriendAsync(ulong requesterId, ulong friendId)
        { 
            throw new NotImplementedException();
         }
        
        public async Task RemoveFriendAsync(ulong requesterId, ulong friendId)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requesterId"></param>
        /// <param name="blockedUserId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task BlockUserAsync(ulong requesterId, ulong blockedUserId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requesterId"></param>
        /// <param name="blockedUserId"></param>
        /// <returns></returns>
        public async Task UnblockUserAsync(ulong requesterId, ulong blockedUserId)
        {
           throw new NotImplementedException();
        }
    }
