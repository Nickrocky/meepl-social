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
            Clans = clubBlob.Clans,
            Clubs = clubBlob.Clubs,
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
        throw new NotImplementedException();
    }

    public async Task GrantBadgeToPlayer(BadgeContainerBlob containerBlob, TableboundProfile profile)
    {
        throw new NotImplementedException();
    }

    public async Task<BadgeContainerBlob> GetBadgeContainer(MeeplIdentifier meeplIdentifier)
    {
        throw new NotImplementedException();
    }

    public async Task<EventContainerBlob> GetEventContainer(MeeplIdentifier meeplIdentifier)
    {
        throw new NotImplementedException();
    }

    public async Task<OrganizationContainerBlob> GetOrganizationContainer(MeeplIdentifier meeplIdentifier)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Badges

    public Task<BadgeBlob> GetBadge(ulong badgeIdentifier)
    {
        throw new NotImplementedException();
    }

    public async Task InsertBadge(BadgeBlob badge)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateBadge(BadgeBlob badge)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBadge(BadgeBlob badge)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public async Task UpdateFriendList(PersonListBlob personListBlob)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FriendRequestBlob>> GetFriendRequestList(MeeplIdentifier tableboundID)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Block List
    
    public Task<PersonListBlob> GetBlockList(MeeplIdentifier tableboundID)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateBlockList(PersonListBlob personListBlob)
    {
        throw new NotImplementedException();
    }

    #endregion
    
    
    private static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString: ConnectionString);
    }
    
}