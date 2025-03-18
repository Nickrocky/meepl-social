using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Responses;

public class ServerListResponse : IMercurial
{
    public ServerListBlob ServerListBlob;
    public string Message;
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack.Append(ServerListBlob)
            .Append(Message)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        throw new NotImplementedException();
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack.Read(ref ServerListBlob);
        unpack.Read(ref Message);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        throw new NotImplementedException();
    }
}