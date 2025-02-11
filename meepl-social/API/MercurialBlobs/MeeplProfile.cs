using Meepl.API.MercurialBlobs.Badges;
using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class MeeplProfile : IMercurial
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

    #region Meepl Social

    /// <summary>
    /// All of the friends a given player has
    /// </summary>
    public PersonListBlob FriendsList;
    
    /// <summary>
    /// All of the blocked players a given player has
    /// </summary>
    public PersonListBlob BlockedList;
    
    /// <summary>
    /// All of the friend requests a player currently has open
    /// </summary>
    public List<FriendRequestBlob> FriendRequestBlobs;

    #endregion

    #region Badges

    /// <summary>
    /// All of a user's unlocked badges
    /// </summary>
    public List<BadgeMetadata> Unlocked_Badges = new List<BadgeMetadata>();
    
    /// <summary>
    /// All of a user's visible badges
    /// </summary>
    public List<BadgeMetadata> Visible_Badges = new List<BadgeMetadata>();

    #endregion

    #region Events

    /// <summary>
    /// All of the Events this user has signed up to be a part of
    /// </summary>
    public List<long> Events = new List<long>();

    #endregion

    #region Organizations

    /// <summary>
    /// All of the clubs this user is a part of
    /// </summary>
    public List<long> Clubs = new List<long>();
    
    /// <summary>
    /// All of the clans this user is a part of
    /// </summary>
    public List<long> Clans = new List<long>();

    #endregion

    #region Constructors

    public MeeplProfile()
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
            .Append(FriendsList)
            .Append(BlockedList)
            .Append(FriendRequestBlobs)
            .Append(Unlocked_Badges)
            .Append(Visible_Badges)
            .Append(Events)
            .Append(Clubs)
            .Append(Clans)
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
            .Append(FriendsList)
            .Append(BlockedList)
            .Append(FriendRequestBlobs)
            .Append(Unlocked_Badges)
            .Append(Visible_Badges)
            .Append(Events)
            .Append(Clubs)
            .Append(Clans);
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
            .Read(ref FriendsList)
            .Read(ref BlockedList)
            .Read(ref FriendRequestBlobs)
            .Read(ref Unlocked_Badges)
            .Read(ref Visible_Badges)
            .Read(ref Events)
            .Read(ref Clubs)
            .Read(ref Clans)
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
            .Read(ref FriendsList)
            .Read(ref BlockedList)
            .Read(ref FriendRequestBlobs)
            .Read(ref Unlocked_Badges)
            .Read(ref Visible_Badges)
            .Read(ref Events)
            .Read(ref Clubs)
            .Read(ref Clans)
            .Finish();
        Indicator = (StatusIndicator) indicator;
    }

    #endregion
}