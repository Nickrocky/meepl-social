using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Responses;

public class ServerListActionResponse : IMercurial
{
    public string Msg;
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack.Append(Msg)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        throw new NotImplementedException();
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack.Read(ref Msg);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        throw new NotImplementedException();
    }
}