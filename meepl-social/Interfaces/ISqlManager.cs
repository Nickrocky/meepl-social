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

    #endregion

    #region Events

    /// <summary>
    /// Gets the event from the database
    /// </summary>
    /// <param name="eventIdentifier">The identifier for the event in the database</param>
    /// <returns>The event associated to that identifier in the database</returns>
    public Task<EventBlob> GetEvent(ulong eventIdentifier);

    #endregion
    
}