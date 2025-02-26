using Meepl.API.MercurialBlobs.Badges;
using Mercurial.Interfaces;
using Mercurial.Util;
using Newtonsoft.Json;

namespace Meepl.API.MercurialBlobs;

public class MeeplProfile : IMercurial
{
    #region Core Elements

    /// <summary>
    /// This is the real backend identifier we use to identify a user
    /// </summary>
    [JsonProperty("MeeplIdentifier")] public MeeplIdentifier MeeplIdentifier { get; set; }

    /// <summary>
    /// The username of the user you can find this player by in Meepl
    /// </summary>
    [JsonProperty("Username")] public string Username  { get; set; }

    /// <summary>
    /// The short bio of the user found on their profile in Meepl
    /// </summary>
    [JsonProperty("Biography")] public string Biography  { get; set; }

    /// <summary>
    /// The 'action' text for a user "PlayerA is now playing Overrealms"
    /// </summary>
    [JsonProperty("Action")] public string Action  { get; set; }

    /// <summary>
    /// The cdn link to the profile image for this player
    /// </summary>
    [JsonProperty("CDN")] public string ProfileCDNLink  { get; set; }

    /// <summary>
    /// The status indicator of a player ex. online, offline, away, etc.
    /// </summary>
    [JsonIgnore] public StatusIndicator Indicator;

    [JsonProperty("StatusIndicator")]
    private byte indicator
    {
        get => (byte)Indicator;
        set => Indicator = (StatusIndicator)value;
    }

    #endregion

    #region Meepl Universe Equipped Items

    /// <summary>
    /// A toggle-able title for a profile that is unlocked via Meepl Universe
    /// </summary>
    [JsonProperty("UniverseTitle")] public ulong UniverseTitle { get; set; }

    #endregion

    #region Meepl Social

    /// <summary>
    /// All of the friends a given player has
    /// </summary>
    [JsonIgnore] public PersonListBlob FriendsList;
    [JsonProperty("FriendsList")]
    private List<ulong> FriendsListIds
    {
        get => FriendsList.GetPersonList();
        set => FriendsList.FromList(value);
    }

    /// <summary>
    /// All of the blocked players a given player has
    /// </summary>
    [JsonIgnore] public PersonListBlob BlockedList;
    [JsonProperty("BlockedList")] private List<ulong> BlockedListIds
    {
        get => BlockedList.GetPersonList();
        set => BlockedList.FromList(value);
    }
    
    
    /// <summary>
    /// All of the friend requests a player currently has open
    /// </summary>
    [JsonProperty("FriendRequestBlobs")] public List<FriendRequestBlob> FriendRequestBlobs;

    #endregion

    #region Badges

    /// <summary>
    /// All of a user's unlocked badges
    /// </summary>
    [JsonProperty("UnlockedBadges")] public List<BadgeMetadata> Unlocked_Badges {get; set;}
    
    /// <summary>
    /// All of a user's visible badges
    /// </summary>
    [JsonProperty("VisibleBadges")] public List<BadgeMetadata> Visible_Badges { get; set; }

    #endregion

    #region Events

    /// <summary>
    /// All of the Events this user has signed up to be a part of
    /// </summary>
    [JsonProperty("Events")] public List<long> Events { get; set; }

    #endregion

    #region Organizations

    /// <summary>
    /// All of the clubs this user is a part of
    /// </summary>
    [JsonProperty("Clubs")] public List<long> Clubs { get; set; }
    
    /// <summary>
    /// All of the clans this user is a part of
    /// </summary>
    [JsonProperty("Clans")] public List<long> Clans {get; set;}

    #endregion

    #region Constructors

    public MeeplProfile()
    {
        Unlocked_Badges = new List<BadgeMetadata>();
        Visible_Badges = new List<BadgeMetadata>();
        Events = new List<long>();
        Clubs = new List<long>();
        Clans = new List<long>();
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

    /// <summary>
    /// Converts a MeeplProfile into a PublicMeeplProfile by scraping out the more sensitive data from a MeeplProfile
    /// </summary>
    /// <returns>A public Meepl profile for this user</returns>
    public PublicMeeplProfile ToPublicProfile()
    {
        return new PublicMeeplProfile()
        {
            MeeplIdentifier = this.MeeplIdentifier,
            Action = this.Action,
            Biography = this.Biography,
            Indicator = this.Indicator,
            Username = this.Username,
            UniverseTitle = this.UniverseTitle,
            Visible_Badges = this.Visible_Badges,
            ProfileCDNLink = this.ProfileCDNLink
        };
    }
}