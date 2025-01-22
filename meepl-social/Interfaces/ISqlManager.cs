using Meepl.API;
using Meepl.API.Enums;

namespace Meepl.API;

public interface ISQLManager
{
    #region Platform Profiles

    /// <summary>
    /// Gets a Platform Profile from the database given the account type and the platform identifier
    /// </summary>
    /// <param name="gameIdentifier">The game you are querying</param>
    /// <param name="platformIdentifier">The platform identifier</param>
    /// <param name="type">The account type</param>
    /// <returns>The platform profile if it exists</returns>
    Task<PlatformProfile> GetPlatformProfile(ulong gameIdentifier, ulong platformIdentifier, AccountType type);
    
    /// <summary>
    /// Adds a Platform profile to the database
    /// </summary>
    /// <param name="profile">The profile you want to add to the database</param>
    /// <param name="gameIdentifier">The game identifier the platform id is bound to</param>
    /// <returns>Nothing</returns>
    Task PutPlatformProfile(PlatformProfile profile, ulong gameIdentifier);

    /// <summary>
    /// Deletes a platform profile from Meepl (this is here for administrative purposes and cannot be triggered via normal token endpoints)
    /// </summary>
    /// <param name="profile">The profile you want to delete</param>
    /// <returns>Nothing</returns>
    Task DeletePlatformProfile(PlatformProfile profile);

    #endregion

    #region Tablebound Profiles
    
    /// <summary>
    /// Gets the public Tablebound Profile version of a profile
    /// </summary>
    /// <param name="tid">Tablebound ID</param>
    /// <returns></returns>
    Task<TableboundProfile> GetPublicTableboundProfile(ulong tid);
    
    /// <summary>
    /// Gets the tablebound profile associated to this tablebound id
    /// </summary>
    /// <param name="tid">The tablebound id you want to query with</param>
    /// <returns>The tablebound profile associated with that id</returns>
    Task<TableboundProfile> GetTableboundProfile(ulong tid);
    
    /// <summary>
    /// Puts a tablebound profile into storage
    /// </summary>
    /// <param name="profile">The profile you want to save</param>
    Task PutTableboundProfile(TableboundProfile profile);

    /// <summary>
    /// Gets the highest available Tablebound ID for this particular shard as determined by the provided min and maxes
    /// </summary>
    /// <param name="min">The shards minimum number (0)</param>
    /// <param name="max">The shards maximum number + 1</param>
    /// <returns>The current highest available Tablebound id for this shard</returns>
    Task<ulong> GetLatestTableboundIdentifier(ulong min, ulong max);

    #endregion

    #region Username Utils
    
    //Username Lookups
    /// <summary>
    /// Gets all of the usernames currently in use on the server
    /// </summary>
    /// <note>
    /// THIS SHOULD ONLY BE CALLED ON SERVER STARTUP the bloom filter would get populated with any new entries made while server is up
    /// </note>
    /// <returns>A new list of all of the currently in use usernames</returns>
    Task<List<string>> GetAllUsernames();
    
    /// <summary>
    /// Gets the total list of usernames on our internal blacklist
    /// </summary>
    /// <returns>A new instance of the username blacklist</returns>
    Task<List<string>> GetUsernameBlacklist();
    
    /// <summary>
    /// Changes a given users username for their tablebound profile
    /// </summary>
    /// <param name="tableboundIdentifier">Their tablebound id</param>
    /// <param name="username">The new username they want</param>
    Task UpdateUsername(ulong tableboundIdentifier, string username);
    
    #endregion
}