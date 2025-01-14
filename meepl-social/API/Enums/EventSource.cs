// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
namespace Meepl.API.Enums;

/// <summary>
/// This is a enum representing the various sources that can create events.
/// </summary>
public enum EventSource : byte
{
    TABLEBOUND = 0,
    GAME,
    CLUB,
    CLAN,
    PLAYER,
}