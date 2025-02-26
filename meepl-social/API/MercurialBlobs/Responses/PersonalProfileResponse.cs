using Mercurial.Interfaces;
using Mercurial.Util;
using Newtonsoft.Json;

namespace Meepl.API.MercurialBlobs.Responses;

[Serializable]
public class PersonalProfileResponse : IMercurial
{
    [JsonProperty("Profile")] public MeeplProfile Profile { get; set; }
    [JsonProperty("Msg")] public string Message { get; set; }
    
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
        MeeplProfile profile = new MeeplProfile();
        string msg = "";
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref profile)
            .Read(ref msg)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        MeeplProfile profile = new MeeplProfile();
        string msg = "";
        unpack
            .Read(ref profile)
            .Read(ref msg);
    }
}