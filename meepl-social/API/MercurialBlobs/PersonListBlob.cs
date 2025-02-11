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
    public List<MeeplIdentifier> PersonList = new List<MeeplIdentifier>();

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        return pack
            .Append(PersonList)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        packer
            .Append(PersonList);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        unpack
            .Read(ref PersonList);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        unpack
            .Read(ref PersonList);
    }
}