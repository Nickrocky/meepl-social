using Meepl.API.Enums;
using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class FriendRequestBlob : IMercurial
{
    public MeeplIdentifier Issuer;
    public MeeplIdentifier Recipient;
    public string Message;

    public byte[] GetBytes()
    {
        throw new NotImplementedException();
    }

    public void AppendComponentBytes(Pack packer)
    {
        throw new NotImplementedException();
    }

    public void FromBytes(byte[] payload)
    {
        throw new NotImplementedException();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        throw new NotImplementedException();
    }
}