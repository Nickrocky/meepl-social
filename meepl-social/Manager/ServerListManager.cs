using Meepl.API;
using Meepl.API.MercurialBlobs;

namespace Meepl.Managers;

public class ServerListManager
{
    private static ServerListManager instance;

    public static ServerListManager Get()
    {
        return instance;
    }

    private Dictionary<ulong, ServerListBlob> ServerListBlobs = new Dictionary<ulong, ServerListBlob>();

    public void Init(ISQLManager sqlManager)
    {
        ServerListBlobs = new Dictionary<ulong, ServerListBlob>();
    }

    public bool HasServerListBlob(MeeplIdentifier identifier)
    {
        if (!ServerListBlobs.ContainsKey(identifier.Container))
        {
            
        }    
    }

}