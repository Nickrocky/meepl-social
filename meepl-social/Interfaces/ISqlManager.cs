using Meepl.API.MercurialBlobs;
using Meepl.API.MercurialBlobs.Badges;
using Meepl.API.MercurialBlobs.Events;

namespace Meepl.API;

/// <summary>
/// The main SQL interface that represents the functions needed for interoping with postgres. Its an interface so that
/// we can mock against it for unit testing.
/// </summary>
public interface ISQLManager
{
    #region Tablebound Profiles
    
    /// <summary>
    /// Gets the tablebound profile associated to this tablebound id
    /// </summary>
    /// <param name="tid">The tablebound id you want to query with</param>
    /// <returns>The tablebound profile associated with that id</returns>
    Task<MeeplProfile> GetTableboundProfile(MeeplIdentifier tid);
    
    /// <summary>
    /// Updates the Tablebound profile of the user specified
    /// </summary>
    /// <param name="profile">The profile of the person you want to update</param>
    /// <note>THIS ONLY UPDATES THE MAIN TABLE 'TABLEBOUND_PROFILE' FOR A PROFILE</note>
    Task UpdateTableboundProfile(MeeplProfile profile);

    /// <summary>
    /// Grants a Badge to a Player
    /// </summary>
    /// <param name="containerBlob">The new badge container for the player</param>
    /// <param name="identifier">The identifier for that player</param>
    /// <returns></returns>
    Task GrantBadgeToPlayer(BadgeContainerBlob containerBlob, MeeplIdentifier identifier);

    /// <summary>
    /// Gets a players badge container containing both their visible and unlocked badge lists
    /// </summary>
    /// <param name="identifier">The players identifier</param>
    /// <returns>The badge container for that player</returns>
    Task<BadgeContainerBlob> GetBadgeContainer(MeeplIdentifier identifier);

    /// <summary>
    /// Gets a players event container containing all of the events they are registered to attend
    /// </summary>
    /// <param name="meeplIdentifier">The players identifier</param>
    /// <returns>The event container for that player</returns>
    Task<EventContainerBlob> GetEventContainer(MeeplIdentifier meeplIdentifier);

    /// <summary>
    /// Gets all of the organizations this player is associated to
    /// </summary>
    /// <param name="meeplIdentifier">The players identifier</param>
    /// <returns>The clubs and clans this player is a part of</returns>
    Task<OrganizationContainerBlob> GetOrganizationContainer(MeeplIdentifier meeplIdentifier);
    
    #endregion

    #region Badges
    
    /// <summary>
    /// Gets the badge from the database
    /// </summary>
    /// <param name="badgeIdentifier">The identifier for the badge in the database</param>
    /// <returns>The badge associated to that identifier in the database.</returns>
    public Task<BadgeBlob> GetBadge(ulong badgeIdentifier);
    
    /// <summary>
    /// Inserts a badge blob entry into the database for a given badge
    /// </summary>
    /// <param name="badge">The badge you want to insert into the database</param>
    public Task InsertBadge(BadgeBlob badge);
    
    /// <summary>
    /// Updates a badge blob in database to reflect the supplied badge blob
    /// </summary>
    /// <param name="badge">The badge you want use to update the database entry</param>
    public Task UpdateBadge(BadgeBlob badge);
    
    /// <summary>
    /// Deletes a badge blob from the database
    /// </summary>
    /// <note>
    /// This is incredibly unlikely to occur but its here nonetheless
    /// </note>
    /// <param name="badge">The badge you want to find and delete from the database/param>
    public Task DeleteBadge(BadgeBlob badge);

    #endregion

    #region Events

    /// <summary>
    /// Gets the event from the database
    /// </summary>
    /// <param name="eventIdentifier">The identifier for the event in the database</param>
    /// <returns>The event associated to that identifier in the database</returns>
    public Task<EventBlob> GetEvent(ulong eventIdentifier);

    /// <summary>
    /// Adds a new entry to the database for a given event
    /// </summary>
    /// <param name="eventBlob">The event you want to add to the database</param>
    public Task InsertEvent(EventBlob eventBlob);
    
    /// <summary>
    /// Deletes an entry from the database
    /// </summary>
    /// <param name="eventBlob">The event you are looking to delete</param>
    public Task DeleteEvent(EventBlob eventBlob);
    
    /// <summary>
    /// Updates the information for a given entry in the database
    /// </summary>
    /// <param name="eventBlob">The event info you want to be in the database</param>
    public Task UpdateEvent(EventBlob eventBlob);
    
    #endregion

    #region Friends

    /// <summary>
    /// Gets the list of friends a person has from the database
    /// </summary>
    /// <returns>The person list blob</returns>
    public Task<PersonListBlob> GetFriendList(MeeplIdentifier tableboundID);

    /// <summary>
    /// Updates the person list blob for a profiles friend list
    /// </summary>
    /// <param name="personListBlob">The person list blob you want to use for the update</param>
    /// <note> This function has an upsert behavior </note>
    public Task UpdateFriendList(PersonListBlob personListBlob, MeeplIdentifier tableboundID);
    
    /// <summary>
    /// Gets the list of friend requests for a user from database
    /// </summary>
    /// <returns>The person list blob</returns>
    public Task<List<FriendRequestBlob>> GetFriendRequestList(MeeplIdentifier tableboundID);
    
    #endregion

    #region Blocked

    /// <summary>
    /// Gets the list of blocked people a person has from the database
    /// </summary>
    /// <returns>The person list blob</returns>
    public Task<PersonListBlob> GetBlockList(MeeplIdentifier tableboundID);
    
    /// <summary>
    /// Updates the person list blob for a profiles blocked list
    /// </summary>
    /// <param name="personListBlob">The person list blob you want to use for the update</param>
    /// <param name="tableboundID">The person you are updating the block list of</param>
    /// <note> This function has an upsert behavior </note>
    public Task UpdateBlockList(PersonListBlob personListBlob, MeeplIdentifier tableboundID);

    #endregion

    #region Username Forced Changes

    /// <summary>
    /// Gets the full list of all usernames that are being forced to change their username
    /// </summary>
    public Task<List<ulong>> GetUsernameForceChangeList();

    #endregion

    #region Server Lists

    /// <summary>
    /// Gets the user's server list blob from our storage
    /// </summary>
    /// <param name="meeplIdentifier">The user that is trying to get their blob's identifier</param>
    /// <returns>A new instance of the ServerListBlob stored for this user or an empty object</returns>
    public Task<ServerListBlob> GetServerListBlob(MeeplIdentifier meeplIdentifier);

    #endregion
}