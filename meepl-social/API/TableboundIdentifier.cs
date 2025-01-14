// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.

using Meepl.API.Enums;
using Newtonsoft.Json;

namespace Meepl.API;

/// <summary>
/// These are the Tablebound specific identifiers, if you want to understand how they work read up on it in the link provided
/// </summary>
/// <see href="https://docs.tablebound.com/books/technical-tools/page/crystal-specification"/>
public class TableboundIdentifier
{
    /// <summary>
    /// This is the thing that is actually storing all of the weird data we are putting in
    /// </summary>
    private ulong container;
    
    private static readonly ulong AREA_IDENTIFIER_MASK = 0xFC00000000000000;
    private static readonly ulong SHARD_MASK = 0x03FF000000000000;
    private static readonly ulong IDENTIFIER_MASK = 0x0000FFFFFFFFFFFF;

    private AreaIdentifier AreaIdentifier;
    private ushort ShardIdentifier;
    private ulong UserIdentifier;

    [JsonIgnore] public ulong Value
    { 
        get
        {
            return container;
        }
    }

    public bool IsEmpty()
    {
        return container == 0;
    }
    
    /// <summary>
    /// Creates a "null" Tablebound identifier
    /// </summary>
    /// <returns>An invalid Tablebound identifier</returns>
    public static TableboundIdentifier CreateEmpty()
    {
        return new TableboundIdentifier(0);
    }

    /// <summary>
    /// Creates a Tablebound Identifier from its constituent parts
    /// </summary>
    /// <param name="areaIdentifier">The area the initial registration server was</param>
    /// <param name="shardIdentifier">The shard number of that server</param>
    /// <param name="userIncrement">That shard's user increment</param>
    /// <returns>A properly packed Tablebound Identifier</returns>
    public static TableboundIdentifier Create(AreaIdentifier areaIdentifier, ushort shardIdentifier, ulong userIncrement)
    {
        byte areaIdentifierVal = (byte) areaIdentifier;
        ulong container = 0;
        container += ((ulong) areaIdentifierVal << 58);
        container += ((ulong)shardIdentifier << 48);
        container += userIncrement;
        return new TableboundIdentifier(container);
    }

    /// <summary>
    /// Parses a ulong container into a crystal identifier
    /// </summary>
    /// <param name="container">The container you want to parse</param>
    /// <returns>That crystal identifier value</returns>
    public static TableboundIdentifier Parse(ulong container)
    {
        return new TableboundIdentifier(container);
    }

    //I did this so people dont try and just new CrystalIdentifier(some number) this bc this is the container and not just a number use parse if you want to parse a container into a crystal identifier
    //I understand that effectively it has the same functional purpose the difference is that someone has to actually think "huh maybe this isn't right" for a moment - nick
    private TableboundIdentifier(ulong container)
    {
        this.container = container;
        AreaIdentifier = (AreaIdentifier) ((byte) (container >> 58));
        ShardIdentifier = (ushort) ((SHARD_MASK & container) >> 48);
        UserIdentifier = (IDENTIFIER_MASK & container);
    }

    public override string ToString()
    {
        return "" + container;
    }
}

