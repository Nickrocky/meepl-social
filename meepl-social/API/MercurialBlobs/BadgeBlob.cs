// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
using Mercurial.Interfaces;
using Mercurial.Util;
using Newtonsoft.Json;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// This is the object representing badges as a Mercurial blob, there is also JSON serialization enabled so we can upload Badges as JSONs
/// </summary>
public class BadgeBlob : IMercurial
{
    /// <summary>
    /// The name of the Badge when you hover over it
    /// </summary>
    [JsonProperty("Name")] public string Name { get; set; }
    
    /// <summary>
    /// The description of the Badge when you hover over it
    /// </summary>
    [JsonProperty("Description")] public string Description { get; set; }
    
    /// <summary>
    /// The location of the icon on the CDN
    /// </summary>
    [JsonProperty("CDN")] public string CDNRoute { get; set; }

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(Name)
            .Append(Description)
            .Append(CDNRoute)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(Name)
            .Append(Description)
            .Append(CDNRoute);
    }

    public void FromBytes(byte[] payload)
    {
        string name = "", description = "", cdnroute = "";
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref name)
            .Read(ref description)
            .Read(ref cdnroute)
            .Finish();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        string name = "", description = "", cdnroute = "";
        unpack
            .Read(ref name)
            .Read(ref description)
            .Read(ref cdnroute);
    }
}