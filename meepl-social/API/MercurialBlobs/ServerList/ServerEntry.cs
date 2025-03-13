using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class ServerEntry : IMercurial
{
    public string IPAddress;
    public ushort Port;

    public byte[] GetBytes()
    {
        throw new NotSupportedException();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(IPAddress)
            .Append(Port);
    }

    public void FromBytes(byte[] payload)
    {
        throw new NotSupportedException();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref IPAddress)
            .Read(ref Port);
    }
}