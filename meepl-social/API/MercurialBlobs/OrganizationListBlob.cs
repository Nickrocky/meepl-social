using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class OrganizationListBlob : IMercurial
{
    public List<long> Organizations = new List<long>();

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Organizations)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Organizations);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Organizations)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Organizations);
    }
}