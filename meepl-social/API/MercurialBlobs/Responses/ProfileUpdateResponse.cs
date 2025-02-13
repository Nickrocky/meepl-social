using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Responses;

public class ProfileUpdateResponse : IMercurial
{
    public string Msg;
    
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Msg)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Msg);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Msg)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Msg);
    }
    
    
}