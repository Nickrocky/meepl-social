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

    /// <summary>
    /// Gets a list of all of the people on the list as a ulong list
    /// </summary>
    /// <returns>A new ulong list</returns>
    public List<ulong> GetPersonList()
    {
        List<ulong> personList = new();
        foreach (var person in PersonList)
        {
            personList.Add(person.Container);
        }
        return personList;
    }

    public void FromList(List<ulong> personList)
    {
        PersonList = new List<MeeplIdentifier>();
        foreach (var person in personList)
        {
            PersonList.Add(MeeplIdentifier.Parse(person));
        }
    }
    
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