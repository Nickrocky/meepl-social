// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
namespace Meepl.API.Enums;

/// <summary>
/// This is a enum representing the various sources that can create events.
/// </summary>
public enum EventSource : byte
{
    /// <summary>
    /// This is like if we sent out a thing globally as a company
    /// </summary>
    TABLEBOUND = 0, 
    
    /// <summary>
    /// A game sending out a request across all of the players that are a part of that community
    /// </summary>
    GAME,
    
    /// <summary>
    /// A club sending out a request across all of the players in the club
    /// </summary>
    CLUB,
    
    /// <summary>
    /// A clan sending out a request across all of the players in the clan
    /// </summary>
    CLAN,
    
    /// <summary>
    /// A player made request being sent to all players that they specify
    /// </summary>
    PLAYER,
}