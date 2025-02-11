using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Responses;

public class BlockPersonResponse : IMercurial
{
    public string Message;


    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Message)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Message);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Message);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Message);
    }
}