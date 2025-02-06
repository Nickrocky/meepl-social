using Meepl.API.MercurialBlobs;

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
    Task<TableboundProfile> GetTableboundProfile(ulong tid);
    
    
    /// <summary>
    /// Updates the Tablebound profile of the user specified
    /// </summary>
    /// <param name="profile">The profile of the person you want to update</param>
    /// <returns></returns>
    Task UpdateTableboundProfile(TableboundProfile profile);

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
    public Task<PersonListBlob> GetFriendList(ulong tableboundID);

    /// <summary>
    /// Gets the list of blocked people a person has from the database
    /// </summary>
    /// <returns>The person list blob</returns>
    public Task<PersonListBlob> GetBlockList(ulong tableboundID);

    #endregion

}