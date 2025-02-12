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

public class EventBlob : IMercurial
{
    public ulong EventIdentifier;
    
    /// <summary>
    /// The name of the Event when you hover it
    /// </summary>
    
    public string Name { get; set; }
    
    /// <summary>
    /// The description of the Event when you hover over it
    /// </summary>
    
    public string Description { get; set; }
    
    /// <summary>
    ///  Which type of event is being made
    /// </summary>
    
    public EventSource EventHostType { get; set; }
    
    
    /// <summary>
    /// This is the ID of the Event Host
    /// </summary>
    
    public ulong EventHostId { get; set; }
    
    
    /// <summary>
    /// What is the start and end time of the event
    /// </summary>
    
    public DateTime EventTimeStart { get; set; }
    public DateTime EventTimeEnd { get; set; }

    #region Mercurial Serialization

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        pack.Append(EventIdentifier);
        pack.Append(Name);
        pack.Append(Description);
        pack.Append((byte)EventHostType);
        pack.Append(EventHostId);
        pack.Append(EventTimeStart.ToBinary());
        pack.Append(EventTimeEnd.ToBinary());
        return pack.Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(EventIdentifier)
            .Append(Name)
            .Append(Description)
            .Append((byte)EventHostType)
            .Append(EventHostId)
            .Append(EventTimeStart.ToBinary())
            .Append(EventTimeEnd.ToBinary());
        
    }

    public void FromBytes(byte[] payload)
    {
        string name = "", description = "";
        ulong eventHostId = 0;
        byte eventSource = 0;
        long eventTimeStart = 0;
        long eventTimeEnd = 0;
       
        
        
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref EventIdentifier)
            .Read(ref name)
            .Read(ref description)
            .Read(ref eventSource)
            .Read(ref eventHostId)
            .Read(ref eventTimeStart)
            .Read(ref eventTimeEnd)
            .Finish();
        
        EventHostType = (EventSource)eventSource;
        EventTimeStart= DateTime.FromBinary(eventTimeStart);
        EventTimeEnd  = DateTime.FromBinary(eventTimeEnd);
        
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        string name = "", description = "";
        ulong eventHostId = 0;
        byte eventSource = 0;
        long eventTimeStart = 0;
        long eventTimeEnd = 0;

        unpack
            .Read(ref EventIdentifier)
            .Read(ref name)
            .Read(ref description)
            .Read(ref eventSource)
            .Read(ref eventHostId)
            .Read(ref eventTimeStart)
            .Read(ref eventTimeEnd);
        
        
        EventHostType = (EventSource)eventSource;
        EventTimeStart= DateTime.FromBinary(eventTimeStart);
        EventTimeEnd  = DateTime.FromBinary(eventTimeEnd);

    }

    #endregion
    
}