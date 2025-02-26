using Mercurial.Interfaces;
using Mercurial.Util;
using Newtonsoft.Json;

namespace Meepl.API.MercurialBlobs.Badges;

public class BadgeMetadata : IMercurial
{
    [JsonProperty("BadgeIdentifier")] public ulong BadgeIdentifier { get; set; }
    [JsonProperty("UnlockedTime")] public DateTime UnlockedTime { get; set; }
    
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
        ulong badgeIdentifier = 0;
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref badgeIdentifier)
            .Read(ref timestampLong)
            .Finish();
        BadgeIdentifier = badgeIdentifier;
        UnlockedTime = DateTime.FromBinary(timestampLong);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        long timestampLong = 0;
        ulong badgeIdentifier = 0;
        unpack
            .Read(ref badgeIdentifier)
            .Read(ref timestampLong);
        BadgeIdentifier = badgeIdentifier;
        UnlockedTime = DateTime.FromBinary(timestampLong);
    }
}