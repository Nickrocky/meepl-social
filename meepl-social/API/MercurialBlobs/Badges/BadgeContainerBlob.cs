using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Badges;

public class BadgeContainerBlob : IMercurial
{
    public List<BadgeMetadata> Badges = new List<BadgeMetadata>();

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Badges)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Badges);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Badges)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Badges);
    }
}