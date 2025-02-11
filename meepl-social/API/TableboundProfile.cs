using Meepl.API.MercurialBlobs.Badges;
using Newtonsoft.Json;

namespace Meepl.API
{
    /// <summary>
    /// A users Tablebound profile 
    /// </summary>
    public class TableboundProfile
    {
        #region Core Information

        /// <summary>
        /// Used for determining if a particular Tablebound Profile has not been synchronized to the database
        /// </summary>
        [JsonIgnore] public bool IsDirty { get; set; }
        
        /// <summary>
        /// This is the real representation of Tablebound Identifiers that is used by the server
        /// </summary>
        [JsonIgnore] public TableboundIdentifier TableboundIdentifier { get; set; } //We dont want to send the tablebound id object

        /// <summary>
        /// This the the 'serialized' form of the Tablebound Identifier object
        /// </summary>
        [JsonProperty("tableboundIdentifier")]
        private ulong flattenedTableboundIdentifier
        {
            get
            {
                return TableboundIdentifier.Value;
            }
        }

        /// <summary>
        /// This is a user's username
        /// </summary>
        [JsonProperty("username")] public string Username { get; set;}
        
        /// <summary>
        /// This is the brief description you can find on a users profile.
        /// </summary>
        [JsonProperty("bio")] public string Bio { get; set;}
        
        /// <summary>
        /// This is the status of a user, Ex. Online, Offline, Do Not Disturb etc.
        /// </summary>
        [JsonProperty("status")] public StatusIndicator StatusIndicator { get; set;}

        #endregion
        
        #region Decorations

        /// <summary>
        /// The Meepl Identifier for the Profile Picture (This likely will need to change)
        /// </summary>
        [JsonProperty("profilepicture")] public ulong ProfilePicture { get; set;}
        /// <summary>
        /// This is the legacy control for the Background of a Meepl Card
        /// </summary>
        [JsonProperty("background")] public ulong Background { get; set;}
        
        /// <summary>
        /// This is a title that is unlocked in an experience world
        /// </summary>
        [JsonProperty("title")] public ulong Title { get; set; }
        
        /// <summary>
        /// This is the set of badges you want to display on your profile
        /// </summary>
        [JsonProperty("visiblebadges")] public BadgeContainerBlob VisibleBadges { get; set; }
        
        /// <summary>
        /// This is the set of all badges that are unlocked for a user.
        /// </summary>
        [JsonProperty("unlockedbadges")] public BadgeContainerBlob UnlockedBadges { get; set;}

        #endregion
        
        #region Social Information

        /// <summary>
        /// The list of all tablebound IDs that this user is friends with
        /// </summary>
        [JsonProperty("friendids")] public List<ulong> FriendIdentifiers { get; set;}
        
        /// <summary>
        /// The list of all tablebound IDs that this user has blocked
        /// </summary>
        [JsonProperty("blockedids")] public List<ulong> BlockedIdentifiers { get; set;}
        
        /// <summary>
        /// The list of all club identifiers 
        /// </summary>
        [JsonProperty("clubids")] public List<ulong> ClubIdentifiers { get; set;}

        #endregion

        #region Constructors

        public TableboundProfile(ulong tableboundIdentifier, StatusIndicator indicator, string username, string bio,
            ulong profilePicture, ulong background, ulong title, BadgeContainerBlob visibleBadges, BadgeContainerBlob unlockedBadges, List<ulong> friendIdentifiers,
            List<ulong> blockedIdentifiers, List<ulong> clubIdentifiers)
        {
            TableboundIdentifier = TableboundIdentifier.Parse((ulong)tableboundIdentifier);
            Username = username;
            StatusIndicator = indicator;
            Bio = bio;
            ProfilePicture = profilePicture;
            Background = background;
            Title = title;
            UnlockedBadges = unlockedBadges;
            FriendIdentifiers = friendIdentifiers;
            BlockedIdentifiers = blockedIdentifiers;
            ClubIdentifiers = clubIdentifiers;
            VisibleBadges = visibleBadges;
        }
        
        public TableboundProfile(ulong tableboundIdentifier, ulong profilePicture)
        {
            TableboundIdentifier = TableboundIdentifier.Parse((ulong) tableboundIdentifier);
            Username = "";
            Bio = "";
            ClubIdentifiers = new List<ulong>();
            FriendIdentifiers = new List<ulong>();
            BlockedIdentifiers = new List<ulong>();
            ProfilePicture = profilePicture;
            UnlockedBadges = new BadgeContainerBlob();
            Background = 0;
            Title = 0;
        }
        
        public TableboundProfile(TableboundProfile publicProfile, List<ulong> friendIdentifiers, List<ulong> blockedIdentifiers, List<ulong> clubIdentifiers, BadgeContainerBlob unlockedBadges)
        {
            TableboundIdentifier = publicProfile.TableboundIdentifier;
            Username = publicProfile.Username;
            Bio = publicProfile.Bio;
            ClubIdentifiers = clubIdentifiers;
            FriendIdentifiers = friendIdentifiers;
            BlockedIdentifiers = blockedIdentifiers;
            ProfilePicture = publicProfile.ProfilePicture;
            UnlockedBadges = unlockedBadges;
            VisibleBadges = publicProfile.VisibleBadges;
            Background = publicProfile.Background;
            Title = publicProfile.Title;
        }
        
        public TableboundProfile()
        {
            TableboundIdentifier = TableboundIdentifier.CreateEmpty();
            Username = "";
            Bio = "";
            ClubIdentifiers = new List<ulong>();
            FriendIdentifiers = new List<ulong>();
            BlockedIdentifiers = new List<ulong>();
            ProfilePicture = 0;
            UnlockedBadges = new BadgeContainerBlob();
            VisibleBadges = new BadgeContainerBlob();
            Background = 0;
            Title = 0;
        }


        #endregion
        
        /// <summary>
        /// Gets a clone of this profile but with all of the personal private information (Ex. Clubs, friends, block list) removed
        /// </summary>
        /// <returns>The non-personal profile of that person</returns>
        public TableboundProfile GetNonPersonalProfileClone()
        {
            return new TableboundProfile(TableboundIdentifier.Value, StatusIndicator, Username, Bio, ProfilePicture, Background, Title, VisibleBadges,new BadgeContainerBlob(), new List<ulong>(), new List<ulong>(), new List<ulong>());
        }

        public override string ToString()
        {
            return "Tablebound Profile\n" +
                   "TableboundIdentifier: " + TableboundIdentifier + "\n" +
                   "Username: " + Username + "\n" +
                   "Bio: " + Bio + "\n" +
                   "ProfilePicture: " + ProfilePicture + "\n" +
                   "Background: " + Background + "\n" +
                   "Title: " + Title + "\n" +
                   "UnlockedBadges: " + UnlockedBadges + "\n" +
                   "FriendIdentifiers: " + FriendIdentifiers + "\n" +
                   "BlockedIdentfiers: " + BlockedIdentifiers + "\n" +
                   "ClubIdentifiers: " + ClubIdentifiers + "\n" +
                   "VisibleBadges: " + VisibleBadges + "\n";
        }
    }
}