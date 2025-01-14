// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
namespace Meepl.API.Enums;

/// <summary>
/// The area identifier associated with the region the registration first occurred with. Ex. VintHill = NA_EAST, Atlanta = NA_EAST, Portland = NA_WEST
/// </summary>
public enum AreaIdentifier : byte
{
    NA_EAST = 0,
    NA_CENTRAL,
    NA_WEST,
    NA_SOUTH,
    NA_ISLES,
    EU_EAST,
    EU_CENTRAL,
    EU_NORTH,
    EU_WEST,
    SA_NORTH,
    SA_CENTRAL,
    SA_EAST,
    AFR_EAST,
    AFR_NW,
    AFR_SOUTH,
    JPN_NORTH,
    JPN_SOUTH,
    KR,
    CHN_EAST,
    CHN_SOUTH,
    CHN_WEST,
    IND,
    AS_CENTRAL,
    AS_WEST,
    AS_SOUTH,
    OCN_NORTH,
    OCN_CENTRAL,
    OCN_EAST,
    OCN_WEST
}