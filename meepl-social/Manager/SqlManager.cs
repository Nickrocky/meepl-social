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
    
    
    
    private static NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString: ConnectionString);
    }

    #region Tablebound Profile

    public Task<TableboundProfile> GetTableboundProfile(ulong tid)
    {
        throw new NotImplementedException();
    }

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

    #region Events

    public Task<EventBlob> GetEvent(ulong eventIdentifier)
    {
        throw new NotImplementedException();
    }

    #endregion

    
}