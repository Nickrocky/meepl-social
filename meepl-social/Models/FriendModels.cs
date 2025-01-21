using Newtonsoft.Json;

namespace Meepl.Models;

/// <summary>
/// Represents a lookup for a user's friends list.
/// </summary>
public struct FriendList_Lookup
{
    /// <summary>
    /// The unique TableboundIdentifier for the user.
    /// </summary>
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// A list of friends for the user, identified by TableboundIdentifiers.
    /// </summary>
    [JsonProperty("friends")]
    public List<string> Friends { get; set; }
}

/// <summary>
/// Represents a lookup for blocked users.
/// </summary>
public struct BlockedUsers_Lookup
{
    /// <summary>
    /// The unique TableboundIdentifier for the user.
    /// </summary>
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// A list of blocked users for the user, identified by TableboundIdentifiers.
    /// </summary>
    [JsonProperty("blocked_users")]
    public List<string> BlockedUsers { get; set; }
}

/// <summary>
/// Represents a lookup for friend requests.
/// </summary>
public struct FriendRequests_Lookup
{
    /// <summary>
    /// The unique TableboundIdentifier for the user.
    /// </summary>
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// A list of incoming friend requests for the user, identified by TableboundIdentifiers.
    /// </summary>
    [JsonProperty("incoming_requests")]
    public List<string> IncomingRequests { get; set; }

    /// <summary>
    /// A list of outgoing friend requests from the user, identified by TableboundIdentifiers.
    /// </summary>
    [JsonProperty("outgoing_requests")]
    public List<string> OutgoingRequests { get; set; }
}

/// <summary>
/// Represents a request to add a friend.
/// </summary>
public class AddFriendRequest
{
    /// <summary>
    /// The unique TableboundIdentifier of the user sending the friend request.
    /// </summary>
    [JsonProperty("requester_id")]
    public string RequesterId { get; set; }

    /// <summary>
    /// The unique TableboundIdentifier of the user to add as a friend.
    /// </summary>
    [JsonProperty("friend_id")]
    public string FriendId { get; set; }

    /// <summary>
    /// Optional message included in the friend request.
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}
