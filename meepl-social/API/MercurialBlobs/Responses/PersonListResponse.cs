using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Responses;

public class PersonListResponse : IMercurial
{

    public PersonListBlob PersonListBlob;
    public string Msg;
    
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(PersonListBlob)
            .Append(Msg)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(PersonListBlob)
            .Append(Msg)
            .Build();
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref PersonListBlob)
            .Read(ref Msg)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref PersonListBlob)
            .Read(ref Msg);
    }
}