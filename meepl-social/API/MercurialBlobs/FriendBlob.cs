// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// The blob representing a players friends, this object is p much a formality
/// </summary>
public class FriendsBlob : IMercurial
{
    /// <summary>
    /// List of all of the people that a person is friends with
    /// </summary>
    public List<TableboundIdentifier> Friends { get; set; }

    public byte[] GetBytes()
    {
        List<long> identifiers = new List<long>();
        foreach (TableboundIdentifier identifier in Friends)
        {
            identifiers.Add((long) identifier.Value);
        }
        Pack pack = new Pack();
        return pack
            .Append(identifiers)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        throw new NotSupportedException();
    }

    public void FromBytes(byte[] payload)
    {
        List<TableboundIdentifier> tableboundIdentifiers = new List<TableboundIdentifier>();
        List<long> longs = new List<long>();
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref longs);
        
        foreach (long val in longs)
        {
            tableboundIdentifiers.Add(TableboundIdentifier.Parse((ulong)val));
        }

        Friends = tableboundIdentifiers;
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        throw new NotSupportedException();
    }
}