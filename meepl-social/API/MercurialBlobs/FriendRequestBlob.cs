using Meepl.API.Enums;
using Mercurial.Interfaces;
using Mercurial.Util;
using Newtonsoft.Json;

namespace Meepl.API.MercurialBlobs;

public class FriendRequestBlob : IMercurial
{
    [JsonProperty("Issuer")] public MeeplIdentifier Issuer { get; set; }
    [JsonProperty("Recipient")] public MeeplIdentifier Recipient { get; set; }
    [JsonProperty("Message")] public string Message { get; set; }

    public byte[] GetBytes()
    {
        Pack pack = new();
        return pack.Append(Issuer)
            .Append(Recipient)
            .Append(Message)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Issuer)
            .Append(Recipient)
            .Append(Message);
    }

    public void FromBytes(byte[] payload)
    {
        MeeplIdentifier issuer = new();
        MeeplIdentifier recipient = new();
        string message = "";
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref issuer)
            .Read(ref recipient)
            .Read(ref message)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        MeeplIdentifier issuer = new();
        MeeplIdentifier recipient = new();
        string message = "";
        unpack
            .Read(ref issuer)
            .Read(ref recipient)
            .Read(ref message);
    }
}