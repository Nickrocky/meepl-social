using Meepl.API;
using Meepl.API.Enums;

namespace Meepl.Managers;

public class ProfileManager
{
    private static Dictionary<ulong, TableboundProfile> Profiles = new Dictionary<ulong, TableboundProfile>();
    
    private const ulong DEFAULT_PFP = 4294967296; //The Overrealms Icon Universe ID
    
    private static ISQLManager SQLManagerProvider;
    private static ulong StartingNumber = 1;
    private ushort ShardIdentifier;
    private static ulong latestIdentfier; //Since the meta data is in the MSB so if you +1'd this id you would be aight

    private AreaIdentifier areaIdentifier;

    private static ProfileManager instance;

    public static ProfileManager Get()
    {
        return instance;
    }
    
    /// <summary>
    /// Sets up the initial state for the profile manager
    /// </summary>
    public async Task Init(ISQLManager sqlManager)
    {
        instance = this;
        SQLManagerProvider = sqlManager;
    }
    
    /// <summary>
    /// Get's a players profile, which contains only public facing information regarding a players account
    /// </summary>
    /// <param name="playerIdentifier">the players identifier</param>
    /// <returns>the player profile that represents that player</returns>
    public static async Task<TableboundProfile> GetProfile(ulong playerIdentifier)
    {
        if (Profiles.ContainsKey(playerIdentifier)) return Profiles[playerIdentifier];
        TableboundProfile profile = await SQLManagerProvider.GetTableboundProfile(playerIdentifier);
        Profiles.Add(playerIdentifier, profile);
        return profile.GetNonPersonalProfileClone();
    }

    /// <summary>
    /// If a given Tablebound Profile exists
    /// </summary>
    /// <param name="tid">The tablebound id</param>
    /// <returns>If it exists or not</returns>
    public static async Task<bool> Exists(ulong tid)
    {
        var profile = await GetProfile(tid);
        return profile.MeeplIdentifier.IsEmpty();
    }
    
    // ============================
    //         TESTING HOOKS 
    // ============================
    
    /// <summary>
    /// DO NOT USE THIS FUNCTION FOR ANYTHING BUT UNIT TESTING
    /// </summary>
    public static void AddDebugProfile(TableboundProfile profile)
    {
        Profiles.Add(profile.MeeplIdentifier.Container, profile);
    }

    /// <summary>
    /// DO NOT USE THIS FUNCTION FOR ANYTHING BUT UNIT TESTING
    /// </summary>
    public static void ClearAllProfiles()
    {
        Profiles.Clear();
    }
    
}