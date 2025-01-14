// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.

using Meepl.API.Enums;
using Newtonsoft.Json;

namespace Meepl.API;

/// <summary>
/// The platform associated profile that we use for internal linking.
/// </summary>
public class PlatformProfile
{
    /// <summary>
    /// The platform identifier, Ex. a Meta Org-scoped Identifier
    /// </summary>
    [JsonProperty("platformid")] public ulong PlatformIdentifier { get; }
    /// <summary>
    /// The Tablebound identifier
    /// </summary>
    [JsonProperty("tableboundid")] public TableboundIdentifier TableboundIdentifier { get; }
    /// <summary>
    /// The account type of the platform profile
    /// </summary>
    [JsonProperty("accounttype")] public AccountType Type { get; }

    public PlatformProfile(ulong platformIdentifier, ulong tableboundIdentifier, AccountType type)
    {
        PlatformIdentifier = platformIdentifier;
        TableboundIdentifier = TableboundIdentifier.Parse((ulong) tableboundIdentifier);
        Type = type;
    }
        
    public PlatformProfile()
    {
        PlatformIdentifier = 0;
        TableboundIdentifier = TableboundIdentifier.CreateEmpty();
        Type = AccountType.UNKNOWN;
    }

    /// <summary>
    /// Checks to see if a platform profile is a valid profile or just a "null" profile
    /// </summary>
    /// <returns>The validity of the profile</returns>
    public bool IsValid()
    {
        if (PlatformIdentifier == 0) return false;
        if (TableboundIdentifier.IsEmpty()) return false;
        if (Type == AccountType.UNKNOWN) return false;
        return true;
    }

    public override string ToString()
    {
        return "Platform Profile\n" +
               "PlatformIdentifier: " + PlatformIdentifier + "\n" +
               "TableboundIdentifier: " + TableboundIdentifier + "\n" +
               "AccountType: " + Type + "\n";
    }
}