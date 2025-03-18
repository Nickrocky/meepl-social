using Meepl.API;
using Meepl.API.MercurialBlobs;

namespace Meepl.Managers;

public class ServerListManager
{
    private static ServerListManager instance;
    private ISQLManager _sqlManager;

    public static ServerListManager Get()
    {
        return instance;
    }

    private Dictionary<ulong, ServerListBlob> ServerListBlobs = new Dictionary<ulong, ServerListBlob>();

    public void Init(ISQLManager sqlManager)
    {
        instance = this;
        ServerListBlobs = new Dictionary<ulong, ServerListBlob>();
        _sqlManager = sqlManager;
    }


    public async Task RemoveServerFromList(MeeplIdentifier identifier, ServerEntry entry)
    {
        if (await HasServerListBlob(identifier))
        {
            var blob = ServerListBlobs[identifier.Container];
            blob.RemoveServerEntry(entry);
            ServerListBlobs[identifier.Container] = blob;
        }
    }

    public async Task<ServerListBlob> GetServerList(MeeplIdentifier meeplIdentifier)
    {
        if (await HasServerListBlob(meeplIdentifier))
        {
            return ServerListBlobs[meeplIdentifier.Container];
        }

        return new ServerListBlob();
    }

    public async Task AddServerToList(MeeplIdentifier identifier, ServerEntry entry)
    {
        ServerListBlob blob = new ServerListBlob();

        if (await HasServerListBlob(identifier))
        {
            blob = ServerListBlobs[identifier.Container];
            blob.AddServerEntry(entry);
            ServerListBlobs[identifier.Container] = blob;
        }
        else
        {
            blob.AddServerEntry(entry);
            ServerListBlobs.Add(identifier.Container, blob);
        }
        await _sqlManager.UpdateServerListBlob(identifier, blob);
    }
    
    public async Task<bool> HasServerListBlob(MeeplIdentifier identifier)
    {
        if (!ServerListBlobs.ContainsKey(identifier.Container))
        {
            var blob = await _sqlManager.GetServerListBlob(identifier);
            ServerListBlobs.Add(identifier.Container, blob);
            return true;
        }

        return ServerListBlobs.ContainsKey(identifier.Container);
    }

}