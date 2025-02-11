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

    public async Task<TableboundProfile> GetTableboundProfile(ulong tid)
    {
        var connection = CreateConnection();
        await connection.OpenAsync();

        var friendBlob = await GetFriendList(tid);
        var blockBlob = await GetBlockList(tid);
        var visibleBadges = await GetBadges(tid);

    }
    
    /*public async Task<TableboundProfile> GetTableboundProfile(ulong tid)
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
    }*/

    public Task UpdateTableboundProfile(TableboundProfile profile)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Badges

    public Task<BadgeBlob> GetBadge(ulong badgeIdentifier)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Event

    public Task<EventBlob> GetEvent(ulong eventIdentifier)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Friend List

    public async Task<PersonListBlob> GetFriendList(ulong tableboundID)
    {
        string cmd = "SELECT FRIENDBLOB FROM FRIENDS WHERE ENTRYOWNER = $1;";
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

    #endregion


    #region Block List
    
    public Task<PersonListBlob> GetBlockList(ulong tableboundID)
    {
        throw new NotImplementedException();
    }

    #endregion
    
    
    private static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString: ConnectionString);
    }
    
}