using Meepl.API.MercurialBlobs.Badges;
using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// When you are retrieving someone elses profile this is the set of data you receive for them
/// </summary>
public class PublicMeeplProfile : IMercurial
{
    #region Core Elements

    /// <summary>
    /// This is the real backend identifier we use to identify a user
    /// </summary>
    public MeeplIdentifier MeeplIdentifier;

    /// <summary>
    /// The username of the user you can find this player by in Meepl
    /// </summary>
    public string Username;

    /// <summary>
    /// The short bio of the user found on their profile in Meepl
    /// </summary>
    public string Biography;

    /// <summary>
    /// The 'action' text for a user "PlayerA is now playing Overrealms"
    /// </summary>
    public string Action;

    /// <summary>
    /// The cdn link to the profile image for this player
    /// </summary>
    public string ProfileCDNLink;

    /// <summary>
    /// The status indicator of a player ex. online, offline, away, etc.
    /// </summary>
    public StatusIndicator Indicator;

    #endregion

    #region Meepl Universe Equipped Items

    /// <summary>
    /// A toggle-able title for a profile that is unlocked via Meepl Universe
    /// </summary>
    public ulong UniverseTitle;

    #endregion
    
    #region Badges

    /// <summary>
    /// All of a user's visible badges
    /// </summary>
    public List<BadgeMetadata> Visible_Badges = new List<BadgeMetadata>();

    #endregion

    #region Constructors

    public PublicMeeplProfile()
    {
        
    }

    #endregion
    
    #region Mercurial Serialization

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(MeeplIdentifier)
            .Append(Username)
            .Append(Biography)
            .Append(Action)
            .Append(ProfileCDNLink)
            .Append((byte)Indicator)
            .Append(UniverseTitle)
            .Append(Visible_Badges)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(MeeplIdentifier)
            .Append(Username)
            .Append(Biography)
            .Append(Action)
            .Append(ProfileCDNLink)
            .Append((byte)Indicator)
            .Append(UniverseTitle)
            .Append(Visible_Badges);
    }

    public void FromBytes(byte[] payload)
    {
        byte indicator = 0;
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref MeeplIdentifier)
            .Read(ref Username)
            .Read(ref Biography)
            .Read(ref Action)
            .Read(ref ProfileCDNLink)
            .Read(ref indicator)
            .Read(ref UniverseTitle)
            .Read(ref Visible_Badges)
            .Finish();
        Indicator = (StatusIndicator) indicator;
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        byte indicator = 0;
        unpack
            .Read(ref MeeplIdentifier)
            .Read(ref Username)
            .Read(ref Biography)
            .Read(ref Action)
            .Read(ref ProfileCDNLink)
            .Read(ref indicator)
            .Read(ref UniverseTitle)
            .Read(ref Visible_Badges)
            .Finish();
        Indicator = (StatusIndicator) indicator;
    }

    #endregion
}