using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class ServerListBlob : IMercurial
{
    private List<ServerEntry> ServerList = new List<ServerEntry>();

    public void AddServerEntry(ServerEntry entry)
    {
        ServerList.Add(entry);
    }

    public void RemoveServerEntry(ServerEntry entry)
    {
        List<ServerEntry> newList = new List<ServerEntry>();
        foreach (var serverEntry in ServerList)
        {
            if(serverEntry.IPAddress.Equals(entry.IPAddress) && serverEntry.Port == entry.Port) continue;
            newList.Add(serverEntry);
        }
    }
    
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(ServerList)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(ServerList);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack.Read(ref ServerList);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref ServerList);
    }
}