using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Badges;

public class BadgeMetadata : IMercurial
{
    public ulong BadgeIdentifier;
    public DateTime UnlockedTime;
    
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(BadgeIdentifier)
            .Append(UnlockedTime.ToBinary())
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(BadgeIdentifier)
            .Append(UnlockedTime.ToBinary());
    }

    public void FromBytes(byte[] payload)
    {        
        long timestampLong = 0;
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref BadgeIdentifier)
            .Read(ref timestampLong)
            .Finish();
        UnlockedTime = DateTime.FromBinary(timestampLong);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        long timestampLong = 0;
        unpack
            .Read(ref BadgeIdentifier)
            .Read(ref timestampLong);
        UnlockedTime = DateTime.FromBinary(timestampLong);
    }
}