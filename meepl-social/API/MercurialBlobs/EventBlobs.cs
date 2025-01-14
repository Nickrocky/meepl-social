// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.

using Meepl.API.Enums;
using Mercurial.Interfaces;
using Mercurial.Util;
using Newtonsoft.Json;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// This is the object representing badges as a Mercurial blob, there is also JSON serialization enabled so we can upload Badges as JSONs
/// </summary>

public class EventBlobs : IMercurial
{ 
    /// <summary>
    /// The name of the Event when you hover it
    /// </summary>
    
    [JsonProperty("Name")] public string Name { get; set; }
    
    /// <summary>
    /// The description of the Event when you hover over it
    /// </summary>
    
    [JsonProperty("Description")] public string Description { get; set; }
    
    /// <summary>
    ///  Which type of event is being made
    /// </summary>
    
    [JsonProperty("EventHostType")] public EventSource EventHostType { get; set; }
    
    
    /// <summary>
    /// This is the ID of the Event Host
    /// </summary>
    
    [JsonProperty("EventHostID")] public ulong EventHostID { get; set; }
    
    
    /// <summary>
    /// What is the start and end time of the event
    /// </summary>
    
    [JsonProperty("EventTimeStart")] public DateTime EventTimeStart { get; set; }
    [JsonProperty("EventTimeEnd")] public DateTime EventTimeEnd { get; set; }
    
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