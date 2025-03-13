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

    public Dictionary<MeeplIdentifier, ServerListBlob> ServerListBlobs = new Dictionary<MeeplIdentifier, ServerListBlob>();

    
    
}