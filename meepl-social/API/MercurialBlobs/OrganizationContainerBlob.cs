using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// This is the blob we are storing in the database for clan and club relationships
/// </summary>
public class OrganizationContainerBlob : IMercurial
{
    public List<long> Clubs;
    public List<long> Clans;
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Clubs)
            .Append(Clans)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Clubs)
            .Append(Clans);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Clubs)
            .Read(ref Clans)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Clubs)
            .Read(ref Clans);
    }
}