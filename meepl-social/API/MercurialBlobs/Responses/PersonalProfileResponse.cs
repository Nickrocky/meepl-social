using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs.Responses;

public class PersonalProfileResponse : IMercurial
{
    public MeeplProfile Profile;
    public string Message;
    
    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Profile)
            .Append(Message)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Profile)
            .Append(Message);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref Profile)
            .Read(ref Message)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref Profile)
            .Read(ref Message);
    }
}