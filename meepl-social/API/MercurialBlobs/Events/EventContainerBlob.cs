using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Events;

public class EventContainerBlob : IMercurial
{
    public List<long> Events;
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Events)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Events);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Events)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Events);
    }
}