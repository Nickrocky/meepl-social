using Meepl.API;
using Meepl.Models;

namespace Meepl.Social.Interfaces;

public interface IFriendService
{
    
    bool IsValidUserId(ulong userId)
    {
        return TableboundIdentifier.Parse(userId).IsEmpty();
    }
    List<ulong> GetFriends(ulong userId);
    
    List<ulong> GetBlockedUsers(ulong userId);
    List<ulong> GetFriendRequests(ulong userId);
    
    bool AddFriend();
}
