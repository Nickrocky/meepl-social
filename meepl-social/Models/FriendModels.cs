using Newtonsoft.Json;

namespace Meepl.Models;


/// <summary>
/// Represents a lookup for blocked users.
/// </summary>
public struct BlockedUsers_Lookup
{
    /// <summary>
    /// The unique TableboundIdentifier for the user.
    /// </summary>
    [JsonProperty("user_id")]
    public ulong UserId { get; set; }

    /// <summary>
    /// A list of blocked users for the user, identified by TableboundIdentifiers.
    /// </summary>
    [JsonProperty("blocked_users")]
    public List<ulong> BlockedUsers { get; set; }
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
public class FriendRequest
{
    /// <summary>
    /// The unique TableboundIdentifier of the user sending the friend request.
    /// </summary>
    [JsonProperty("requester_id")]
    public ulong RequesterId { get; set; }

    /// <summary>
    /// The unique TableboundIdentifier of the user to add as a friend.
    /// </summary>
    [JsonProperty("friend_id")]
    public ulong FriendId { get; set; }
    

    /// <summary>
    /// Optional message included in the friend request.
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}

/// <summary>
/// Represents blocking a user.
/// </summary>
public class BlockUser
{
    /// <summary>
    /// The unique TableboundIdentifier of the user sending the friend request.
    /// </summary>
    [JsonProperty("requester_id")]
    public ulong RequesterId { get; set; }

    /// <summary>
    /// The unique TableboundIdentifier of the user to be blocked.
    /// </summary>
    [JsonProperty("blockedUserId")]
    public ulong BlockedUserId { get; set; }
    
}
