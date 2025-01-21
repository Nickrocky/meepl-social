using Meepl.Models;

namespace Meepl.Social.Interfaces;

public interface IFriendService
{
    bool IsValidUserId(string userId);
    List<string> GetFriends(string userId);
    List<string> GetBlockedUsers(string userId);
    FriendRequests_Lookup? GetFriendRequests(string userId);
    bool AddFriend(AddFriendRequest request);
}
