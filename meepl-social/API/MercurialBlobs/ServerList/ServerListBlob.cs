using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class ServerListBlob : IMercurial
{
    private List<ServerEntry> ServerList;


    public byte[] GetBytes()
    {
        throw new NotSupportedException();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(ServerList);
    }

    public void FromBytes(byte[] payload)
    {
        throw new NotSupportedException();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref ServerList);
    }
}