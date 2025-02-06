using Meepl.API;
using Meepl.API.Enums;
using Meepl.API.MercurialBlobs;
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

    public async Task<TableboundProfile> GetPublicTableboundProfile(ulong tid)
    {
        var connection = CreateConnection();
        await connection.OpenAsync();
        var cmd = "SELECT * FROM TABLEBOUND_PROFILE WHERE TABLEBOUNDID = $1;";
        NpgsqlCommand command = new NpgsqlCommand(cmd, connection);
        command.Parameters.Add(new NpgsqlParameter() { Value = (long) tid });
        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (!reader.HasRows) return new TableboundProfile();
        await reader.ReadAsync();
        //Console.WriteLine("Has Rows: " + reader.HasRows + " Rows: " + reader.Rows);
        //Start to build the base profile
        var tableboundIdentifier = reader.GetInt64(0);
        var username = reader.GetString(1);
        var bio = reader.GetString(2);
        var profilepicture = reader.GetInt64(3);
        var cardbackground = reader.GetInt64(4);
        var title = reader.GetInt64(5);
        var status = reader.GetInt16(6);
        await reader.DisposeAsync();
        Console.WriteLine("TID: " + tableboundIdentifier + " Username: " +  username + " Bio: " + bio + " Profile Picture: " + profilepicture + " Card Background: " + cardbackground + " Title: " + title + " Status: " + status);
        var cmd2 = "SELECT * FROM BADGES b INNER JOIN BADGE_MEMBERS bm ON bm.badgeid = b.badgeid WHERE bm.tableboundid = $1;";
        command = new NpgsqlCommand(cmd2, connection);
        command.Parameters.Add(new NpgsqlParameter() { Value = (long) tid });
        List<ulong> visibleBadgeList = new List<ulong>();
        reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            //visibleBadgeList.Add(new BadgeBlob(reader.GetString(1), reader.GetString(3), reader.GetString(2), reader.GetInt64(0), reader.GetInt64(4)));
            visibleBadgeList.Add((ulong)reader.GetInt64(3));
        }

        await reader.DisposeAsync();
        await connection.CloseAsync();
        return new TableboundProfile((ulong) tableboundIdentifier, (StatusIndicator) status, username, bio, (ulong) profilepicture, (ulong) cardbackground, (ulong) title,
            visibleBadgeList,new List<ulong>(), new List<ulong>(), new List<ulong>(), new List<ulong>());
    }

    public async Task<TableboundProfile> GetTableboundProfile(ulong tid)
    {
        var connection = CreateConnection();
        await connection.OpenAsync();
        var cmd2 = "SELECT BLOCKEDID FROM BLOCKED WHERE ENTRYOWNER = $1;";
        var cmd3 = "SELECT FRIENDID FROM FRIENDS WHERE ENTRYOWNER = $1;";
        var cmd4 = "SELECT CLUBID FROM CLUB_MEMBERS WHERE MEMBERID = $1;";
        var cmd5 = "SELECT * FROM BADGES b INNER JOIN BADGE_MEMBERS bm ON bm.badgeid = b.badgeid WHERE bm.tableboundid = $1;";
        //var cmd6 = "SELECT * FROM PASSKEYS WHERE TABLEBOUNDID = $1;"; //todo For when we add passkey login
        
        TableboundProfile publicProfile = await GetPublicTableboundProfile(tid);
        if (publicProfile.TableboundIdentifier.IsEmpty()) return new TableboundProfile();
        //Actually build that blocked list
        var command = new NpgsqlCommand(cmd2, connection);
        command.Parameters.Add(new NpgsqlParameter() {Value = (long) tid});
        List<ulong> blockedUsers = new List<ulong>();
        var reader = await command.ExecuteReaderAsync();
        //Console.WriteLine("Has Rows: " + reader.HasRows + " Rows: " + reader.Rows);
        while (await reader.ReadAsync())
        {
            blockedUsers.Add((ulong) reader.GetInt64(0));
        }
        await reader.CloseAsync();
        
        //Actually build that friends list
        command = new NpgsqlCommand(cmd3, connection);
        command.Parameters.Add(new NpgsqlParameter() {Value = (long) tid});
        List<ulong> friends = new List<ulong>();
        reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            friends.Add((ulong) reader.GetInt64(0));
        }
        await reader.CloseAsync();
        
        //Actually build that club list
        command = new NpgsqlCommand(cmd4, connection);
        command.Parameters.Add(new NpgsqlParameter() {Value = (long) tid});
        List<ulong> clubs = new List<ulong>();
        reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            clubs.Add((ulong) reader.GetInt64(0));
        }
        await reader.CloseAsync();

        //Finally build the badge list
        command = new NpgsqlCommand(cmd5, connection);
        command.Parameters.Add(new NpgsqlParameter() {Value = (long) tid});
        List<ulong> unlockedBadgeList = new List<ulong>();
        reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            unlockedBadgeList.Add((ulong)reader.GetInt64(2));
        }
        await reader.DisposeAsync();
        await connection.CloseAsync();
        return new TableboundProfile(publicProfile, friends, blockedUsers, clubs, unlockedBadgeList);
    }

    public Task UpdateTableboundProfile(TableboundProfile profile)
    {
        throw new NotImplementedException();
    }
    
    public async Task PutTableboundProfile(TableboundProfile profile)
    {
        string cmd = "INSERT INTO TABLEBOUND_PROFILE (TABLEBOUNDID, USERNAME, BIO, PROFILEPICTURE, CARDBACKGROUND, TITLE, STATUS) VALUES ($1, $2, $3, $4, $5, $6, $7);";
        var connection = CreateConnection();
        await connection.OpenAsync();
        var command = new NpgsqlCommand(cmd, connection);
        
        NpgsqlParameter[] parameters = 
        {
            new NpgsqlParameter() { Value = (long) profile.TableboundIdentifier.Value },
            new NpgsqlParameter() { Value = profile.Username },
            new NpgsqlParameter() { Value = profile.Bio },
            new NpgsqlParameter() { Value = (long) profile.ProfilePicture},
            new NpgsqlParameter() { Value = (long) profile.Background},
            new NpgsqlParameter() { Value = (long) profile.Title},
            new NpgsqlParameter() { Value = (short) profile.StatusIndicator},
        };
        
        command.Parameters.AddRange(parameters);
        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    #endregion

    #region Badges
    public Task<BadgeBlob> GetBadge(ulong badgeIdentifier)
    {
        throw new NotImplementedException();
    }

    public Task InsertBadge(BadgeBlob badge)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBadge(BadgeBlob badge)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBadge(BadgeBlob badge)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Event

    public Task<EventBlob> GetEvent(ulong eventIdentifier)
    {
        throw new NotImplementedException();
    }

    public Task InsertEvent(EventBlob eventBlob)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEvent(EventBlob eventBlob)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEvent(EventBlob eventBlob)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Friends

    public async Task<PersonListBlob> GetFriendList(ulong tableboundID)
    {
        string cmd = "SELECT FRIENDBLOB FROM FRIENDS_TABLE WHERE TABLEBOUNDID = $1;";
        var connection = CreateConnection();
        await connection.OpenAsync();
        var command = new NpgsqlCommand(cmd, connection);
        
        NpgsqlParameter[] parameters = 
        {
            new NpgsqlParameter() { Value = (long) tableboundID }
        };
        
        var reader = await command.ExecuteReaderAsync();

        PersonListBlob personListBlob = new PersonListBlob();        
        if (reader.HasRows)
        {
            byte[] buffer = new byte[4096];
            await reader.ReadAsync();

            reader.GetBytes(0, 0, buffer, 0, 4096);
            personListBlob.FromBytes(buffer);
        }

        await reader.DisposeAsync();
        await connection.CloseAsync();

        return personListBlob;
    }

    public async Task UpdateFriendList(PersonListBlob personListBlob)
    {
        
    }
    
    #endregion

    #region Block
    public async Task<PersonListBlob> GetBlockList(ulong tableboundID)
    {
        string cmd = "SELECT BLOCKEDBLOB FROM BLOCKED_TABLE WHERE TABLEBOUNDID = $1;";
        var connection = CreateConnection();
        await connection.OpenAsync();
        var command = new NpgsqlCommand(cmd, connection);
        
        NpgsqlParameter[] parameters = 
        {
            new NpgsqlParameter() { Value = (long) tableboundID }
        };
        
        var reader = await command.ExecuteReaderAsync();

        PersonListBlob personListBlob = new PersonListBlob();        
        if (reader.HasRows)
        {
            byte[] buffer = new byte[4096];
            await reader.ReadAsync();

            reader.GetBytes(0, 0, buffer, 0, 4096);
            personListBlob.FromBytes(buffer);
        }

        await reader.DisposeAsync();
        await connection.CloseAsync();

        return personListBlob;
    }

    public Task UpdateBlockList(PersonListBlob personListBlob)
    {
        throw new NotImplementedException();
    }

    #endregion
    
    
    private static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString: ConnectionString);
    }
    
}