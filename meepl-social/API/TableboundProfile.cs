using Newtonsoft.Json;

namespace Meepl.API
{
    /// <summary>
    /// A users Tablebound profile 
    /// </summary>
    public class TableboundProfile
    {
        //Used to see if we need to update the database with the data in here on fixed intervals
        [JsonIgnore] public bool IsDirty { get; set; }
        
        //Base Profile Info
        [JsonIgnore] public TableboundIdentifier TableboundIdentifier { get; set; } //We dont want to send the tablebound id object

        //This is a small private property that we can serialize in lieu of the Tablebound ID object
        [JsonProperty("tableboundIdentifier")]
        private ulong flattenedTableboundIdentifier
        {
            get
            {
                return TableboundIdentifier.Value;
            }
        }

        [JsonProperty("username")] public string Username { get; set;}
        [JsonProperty("bio")] public string Bio { get; set;}
        [JsonProperty("status")] public StatusIndicator StatusIndicator { get; set;}

        //Decorations
        [JsonProperty("profilepicture")] public ulong ProfilePicture { get; set;}
        [JsonProperty("background")] public ulong Background { get; set;}
        [JsonProperty("title")] public ulong Title { get; set; }
        [JsonProperty("visiblebadges")] public List<ulong> VisibleBadges { get; set; }
        [JsonProperty("unlockedbadges")] public List<ulong> UnlockedBadges { get; set;}
        
        //Social Module
        [JsonProperty("friendids")] public List<ulong> FriendIdentifiers { get; set;}
        [JsonProperty("blockedids")] public List<ulong> BlockedIdentifiers { get; set;}
        [JsonProperty("clubids")] public List<ulong> ClubIdentifiers { get; set;}

        public TableboundProfile(ulong tableboundIdentifier, StatusIndicator indicator, string username, string bio,
            ulong profilePicture, ulong background, ulong title, List<ulong> visibleBadges, List<ulong> unlockedBadges, List<ulong> friendIdentifiers,
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
            UnlockedBadges = new List<ulong>();
            Background = 0;
            Title = 0;
        }
        
        public TableboundProfile(TableboundProfile publicProfile, List<ulong> friendIdentifiers, List<ulong> blockedIdentifiers, List<ulong> clubIdentifiers, List<ulong> unlockedBadges)
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
            UnlockedBadges = new List<ulong>();
            VisibleBadges = new List<ulong>();
            Background = 0;
            Title = 0;
        }

        /// <summary>
        /// Gets a clone of this profile but with all of the personal private information (Ex. Clubs, friends, block list) removed
        /// </summary>
        /// <returns>The non-personal profile of that person</returns>
        public TableboundProfile GetNonPersonalProfileClone()
        {
            return new TableboundProfile(TableboundIdentifier.Value, StatusIndicator, Username, Bio, ProfilePicture, Background, Title, VisibleBadges,new List<ulong>(), new List<ulong>(), new List<ulong>(), new List<ulong>());
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