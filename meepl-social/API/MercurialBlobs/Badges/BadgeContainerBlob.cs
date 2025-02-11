using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Badges;

public class BadgeContainerBlob : IMercurial
{
    public List<BadgeMetadata> Unlocked_Badges = new List<BadgeMetadata>();
    public List<BadgeMetadata> Visible_Badges = new List<BadgeMetadata>();

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Unlocked_Badges)
            .Append(Visible_Badges)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Unlocked_Badges)
            .Append(Visible_Badges);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Unlocked_Badges)
            .Read(ref Visible_Badges)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Unlocked_Badges)
            .Read(ref Visible_Badges);
    }
}