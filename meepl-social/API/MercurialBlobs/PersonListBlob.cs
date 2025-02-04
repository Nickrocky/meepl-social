// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// The blob representing a list of players, this object is p much a formality
/// </summary>
public class PersonListBlob : IMercurial
{
    /// <summary>
    /// List of all of the people in that person list
    /// </summary>
    public List<TableboundIdentifier> PersonList = new List<TableboundIdentifier>();

    public byte[] GetBytes()
    {
        List<long> identifiers = new List<long>();
        foreach (TableboundIdentifier identifier in PersonList)
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
        List<long> identifiers = new List<long>();
        foreach (TableboundIdentifier identifier in PersonList)
        {
            identifiers.Add((long) identifier.Value);
        }

        packer
            .Append(identifiers);
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

        PersonList = tableboundIdentifiers;
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        List<TableboundIdentifier> tableboundIdentifiers = new List<TableboundIdentifier>();
        List<long> longs = new List<long>();
        unpack
            .Read(ref longs);
        
        foreach (long val in longs)
        {
            tableboundIdentifiers.Add(TableboundIdentifier.Parse((ulong)val));
        }

        PersonList = tableboundIdentifiers;
    }
}