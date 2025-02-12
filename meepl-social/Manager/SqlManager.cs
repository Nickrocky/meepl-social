using System.Data;
using Meepl.API;
using Meepl.API.Enums;
using Meepl.API.MercurialBlobs;
using Meepl.API.MercurialBlobs.Badges;
using Meepl.API.MercurialBlobs.Events;
using Meepl.Social.Interfaces;
using Npgsql;

namespace Meepl.Managers;

public class SqlManager : ISQLManager
{
    public static string ConnectionString = "";
    public static string MeeplUniverseConnectionString = "";
    private static SqlManager _instance = new SqlManager();

    public static SqlManager Get()
    {
        return _instance;
    }
    
    public void Init()
    {
        _instance = this;
    }

    #region Tablebound Profile

    public async Task<MeeplProfile> GetTableboundProfile(MeeplIdentifier tid)
    {
        if (tid.IsEmpty()) return new MeeplProfile();
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var friendBlob = await GetFriendList(tid);
        var blockBlob = await GetBlockList(tid);
        var badgeBlob = await GetBadgeContainer(tid);
        var eventBlob = await GetEventContainer(tid);
        var clubBlob = await GetOrganizationContainer(tid);
        var friendRequestBlob = await GetFriendRequestList(tid);

        var cmd = "SELECT * FROM TABLEBOUND_PROFILE WHERE TABLEBOUND_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.Add(new NpgsqlParameter() {Value = (long) tid.Container});

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) return new MeeplProfile();

        await reader.ReadAsync();

        var username = reader.GetString(1);
        var bio = reader.GetString(2);
        var action = reader.GetString(3);
        var cdnlink = reader.GetString(4);
        var status = (StatusIndicator)reader.GetInt16(5);

        await reader.DisposeAsync();
        await connection.CloseAsync();
        
        return new MeeplProfile()
        {
            Username = username,
            Action = action,
            Biography = bio,
            ProfileCDNLink = cdnlink,
            Indicator = status,
            Clans = clubBlob.Clans.Organizations,
            Clubs = clubBlob.Clubs.Organizations,
            Events = eventBlob.Events,
            BlockedList = blockBlob,
            FriendsList = friendBlob,
            MeeplIdentifier = tid,
            UniverseTitle = 0,
            Unlocked_Badges = badgeBlob.Unlocked_Badges,
            Visible_Badges = badgeBlob.Visible_Badges,
            FriendRequestBlobs = friendRequestBlob
        };
    }

    public async Task UpdateTableboundProfile(MeeplProfile profile)
    {
        if (profile.MeeplIdentifier.IsEmpty()) return;
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "UPDATE TABLEBOUND_PROFILE SET USERNAME = $1, BIO = $2, ACTION = $3, ICONCDNLINK = $4, STATUS = $5 WHERE TABLEBOUND_ID = $6;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter(){ Value = profile.Username },
            new NpgsqlParameter(){ Value = profile.Biography },
            new NpgsqlParameter(){ Value = profile.Action },
            new NpgsqlParameter(){ Value = profile.ProfileCDNLink },
            new NpgsqlParameter(){ Value = (short) profile.Indicator },
            new NpgsqlParameter(){ Value = (long) profile.MeeplIdentifier.Container }
        });

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task GrantBadgeToPlayer(BadgeContainerBlob containerBlob, MeeplIdentifier identifier)
    {
        if (identifier.IsEmpty()) return;
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "INSERT INTO PROFILE_BADGES (TABLEBOUND_ID, BADGE_CONTAINER_BLOB, BLOBHASH) VALUES ($1, $2, $3) ON CONFLICT (TABLEBOUND_ID) DO UPDATE SET BADGE_CONTAINER_BLOB = $2, BLOBHASH = $3;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter() { Value = (long) identifier.Container },
            new NpgsqlParameter() { Value = containerBlob.GetBytes() },
            new NpgsqlParameter() { Value = containerBlob.GetHash() },
        });

        await command.ExecuteNonQueryAsync();
        await connection.DisposeAsync();
    }
    
    public async Task<BadgeContainerBlob> GetBadgeContainer(MeeplIdentifier meeplIdentifier)
    {
        if (meeplIdentifier.IsEmpty()) return new BadgeContainerBlob();
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "SELECT BADGE_CONTAINER_BLOB FROM PROFILE_BADGES WHERE TABLEBOUND_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.Add(new NpgsqlParameter() { Value = (long)meeplIdentifier.Container });

        var reader = await command.ExecuteReaderAsync();

        BadgeContainerBlob blob = new BadgeContainerBlob();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            byte[] buffer = new byte[8192];
            var numberRead = reader.GetBytes(0, 0, buffer, 0, 8192);
            blob.FromBytes(buffer);
        }

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return blob;

    }

    public async Task<EventContainerBlob> GetEventContainer(MeeplIdentifier meeplIdentifier)
    {
        if (meeplIdentifier.IsEmpty()) return new EventContainerBlob();
        
        var connection = CreateConnection();
        await connection.OpenAsync();
        
        var cmd = "SELECT EVENT_CONTAINER_BLOB FROM PROFILE_EVENTS WHERE TABLEBOUND_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        
        var reader = await command.ExecuteReaderAsync();

        EventContainerBlob blob = new EventContainerBlob();
        if (reader.HasRows)
        {
            await reader.ReadAsync();
            byte[] buffer = new byte[8192];
            var numberRead = reader.GetBytes(0, 0, buffer, 0, 8192);
            blob.FromBytes(buffer);
        }

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return blob;
    }

    public async Task<OrganizationContainerBlob> GetOrganizationContainer(MeeplIdentifier meeplIdentifier)
    {
        if (meeplIdentifier.IsEmpty()) return new OrganizationContainerBlob();
        
        var connection = CreateConnection();
        await connection.OpenAsync();
        
        var cmd = "SELECT CLAN_CONTAINER_BLOB, CLUB_CONTAINER_BLOB FROM PROFILE_MEMBERSHIPS WHERE TABLEBOUND_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        
        var reader = await command.ExecuteReaderAsync();

        OrganizationContainerBlob blob = new OrganizationContainerBlob();
        if (reader.HasRows)
        {
            OrganizationListBlob clubList = new OrganizationListBlob();
            OrganizationListBlob clanList = new OrganizationListBlob();
            await reader.ReadAsync();
            byte[] clans = new byte[8192];
            byte[] clubs = new byte[8192];
            reader.GetBytes(0, 0, clans, 0, 8192);
            reader.GetBytes(1, 0, clubs, 0, 8192);
            
            clubList.FromBytes(clubs);
            clanList.FromBytes(clans);
            blob.Clans = clanList;
            blob.Clubs = clubList;
        }

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return blob;
    }

    #endregion

    #region Badges

    public async Task<BadgeBlob> GetBadge(ulong badgeIdentifier)
    {
        if(badgeIdentifier == 0) return new BadgeBlob();
       
        var connection = CreateConnection();
        await connection.OpenAsync();
        
        var cmd = "SELECT BADGE_BLOB FROM BADGES WHERE BADGE_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        
        command.Parameters.Add(new NpgsqlParameter() { Value = (long) badgeIdentifier });
        
        var reader = await command.ExecuteReaderAsync();
        
        if (!reader.HasRows) return new BadgeBlob();
        await reader.ReadAsync();

        BadgeBlob blob = new BadgeBlob();
        byte[] buffer = new byte[512];
        reader.GetBytes(0, 0, buffer, 0, 512);
        
        blob.FromBytes(buffer);

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return blob;
    }

    public async Task InsertBadge(BadgeBlob badge)
    {
        if(badge.ID == 0) return;
        
        var connection = CreateConnection();
        await connection.OpenAsync();
        
        var cmd = "INSERT INTO BADGES (BADGE_ID, BADGE_BLOB, BLOBHASH) VALUES ($1, $2, $3);";
        var command = new NpgsqlCommand(cmd, connection);
        
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter(){ Value = badge.ID },
            new NpgsqlParameter(){ Value = badge.GetBytes() },
            new NpgsqlParameter(){ Value = badge.GetHash() },
        });

        await command.ExecuteNonQueryAsync();
        await command.DisposeAsync();
        await connection.CloseAsync();
        
    }
    
    /// <summary>
    /// Updates information in the badge's blob
    /// </summary>
    /// <notes> Badge ID 0 is set to NULL always</notes>
    /// <param name="badge"></param>
    public async Task UpdateBadge(BadgeBlob badge)
    {
        if (badge.ID == 0) return;
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "UPDATE BADGES SET BADGE_BLOB = $2, BLOBHASH = $3 WHERE BADGE_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter(){ Value = badge.ID },
            new NpgsqlParameter(){ Value = badge.GetBytes() },
            new NpgsqlParameter(){ Value = badge.GetHash() },
        });

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public async Task DeleteBadge(BadgeBlob badge)
    {
        if (badge.ID == 0) return;
        
        var connection = CreateConnection();
        await connection.OpenAsync();
        
        var cmd = "DELETE FROM BADGES WHERE BADGE_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter(){ Value = badge.ID },
        });
        
        await command.ExecuteNonQueryAsync();
        await command.DisposeAsync();
        await connection.CloseAsync();
    }

    #endregion

    #region Event

    public Task<EventBlob> GetEvent(ulong eventIdentifier)
    {
        throw new NotImplementedException();
    }

    public async Task InsertEvent(EventBlob eventBlob)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEvent(EventBlob eventBlob)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateEvent(EventBlob eventBlob)
    {
        throw new NotImplementedException();   
    }

    #endregion

    #region Friend List

    public async Task<PersonListBlob> GetFriendList(MeeplIdentifier tableboundID)
    {
        if (tableboundID.Container == 0) return new PersonListBlob();
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "SELECT FRIEND_BLOB FROM FRIENDS WHERE TABLEBOUND_ID = $1";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.Add(new NpgsqlParameter() { Value = tableboundID.Container });

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) return new PersonListBlob();

        await reader.ReadAsync();
        byte[] friends = new byte[8192];
        reader.GetBytes(0, 0, friends, 0, 8192);

        PersonListBlob personListBlob = new PersonListBlob();
        personListBlob.FromBytes(friends);

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return personListBlob;
    }

    public async Task UpdateFriendList(PersonListBlob personListBlob, MeeplIdentifier tableboundID)
    {
        if (tableboundID.Container == 0) return;
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "UPDATE FRIENDS SET FRIEND_BLOB = $2 WHERE TABLEBOUND_ID = $1";
        var command = new NpgsqlCommand(cmd, connection);
        
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter() {Value = (long) tableboundID.Container},
            new NpgsqlParameter() {Value = personListBlob.GetBytes()}
        });

        await command.ExecuteNonQueryAsync();
        
        await command.DisposeAsync();
        await connection.DisposeAsync();
    }

    public async Task<List<FriendRequestBlob>> GetFriendRequestList(MeeplIdentifier tableboundID)
    {
        if (tableboundID.Container == 0) return new List<FriendRequestBlob>();
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "SELECT * FROM FRIEND_REQUESTS WHERE RECIPIENT = $1";
        var command = new NpgsqlCommand(cmd, connection);

        var reader = await command.ExecuteReaderAsync();

        List<FriendRequestBlob> friendRequestBlobs = new List<FriendRequestBlob>();
        string msg = "";
        MeeplIdentifier issuer, recipient;
        FriendRequestBlob requestBlob;
        while (await reader.ReadAsync())
        {
            requestBlob = new FriendRequestBlob();
            issuer = MeeplIdentifier.Parse((ulong) reader.GetInt64(0));
            recipient = MeeplIdentifier.Parse((ulong) reader.GetInt64(1));
            msg = reader.GetString(2);
            requestBlob.Issuer = issuer;
            requestBlob.Recipient = recipient;
            requestBlob.Message = msg;
            friendRequestBlobs.Add(requestBlob);
        }

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return friendRequestBlobs;
    }

    #endregion
    
    #region Block List
    
    public async Task<PersonListBlob> GetBlockList(MeeplIdentifier tableboundID)
    {
        if (tableboundID.Container == 0) return new PersonListBlob();
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "SELECT BLOCKED_BLOB FROM BLOCKED WHERE TABLEBOUND_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.Add(new NpgsqlParameter() { Value = tableboundID.Container });

        var reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        PersonListBlob blob = new PersonListBlob();
        byte[] buffer = new byte[8192];
        var numberRead = reader.GetBytes(0, 0, buffer, 0, 8192);
        blob.FromBytes(buffer);

        await command.DisposeAsync();
        await reader.DisposeAsync();
        await connection.DisposeAsync();
        
        return blob;
    }

    public async Task UpdateBlockList(PersonListBlob personListBlob, MeeplIdentifier meeplIdentifier)
    {
        if (meeplIdentifier.Container == 0) return;
        
        var connection = CreateConnection();
        await connection.OpenAsync();

        var cmd = "UPDATE BLOCKED SET BLOCKED_BLOB = $2 WHERE TABLEBOUND_ID = $1;";
        var command = new NpgsqlCommand(cmd, connection);
        command.Parameters.AddRange(new NpgsqlParameter[]
        {
            new NpgsqlParameter() { Value = meeplIdentifier.Container },
            new NpgsqlParameter() { Value = personListBlob.GetBytes()}
        });

        await command.ExecuteNonQueryAsync();

        await command.DisposeAsync();
        await connection.DisposeAsync();
    }

    #endregion
    
    
    private static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString: ConnectionString);
    }
    
}