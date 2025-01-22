using Meepl.API;
using Meepl.Models;
using Meepl.Social.Interfaces;

namespace Meepl.Managers;

public class FriendService : IFriendService
{
    public bool IsValidUserId(ulong userId)
    {
        return TableboundIdentifier.Parse(userId).IsEmpty();
    }

    public List<ulong> GetFriends(ulong userId)
    {
        throw new NotImplementedException();
    }

    public List<ulong> GetBlockedUsers(ulong userId)
    {
        throw new NotImplementedException();
    }

    List<ulong> IFriendService.GetFriendRequests(ulong userId)
    {
        throw new NotImplementedException();
    }

    public List<ulong> AddFriend(ulong userId)
    {
        throw new NotImplementedException();
    }

    public bool AddFriend()
    {
        throw new NotImplementedException();
    }

    public FriendRequests_Lookup GetFriendRequests(ulong userId)
    {
        throw new NotImplementedException();
    }

    public bool AddFriend(FriendRequest request)
    {
        throw new NotImplementedException();
    }
}