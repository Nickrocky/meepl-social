// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
namespace Meepl.API.Enums;

/// <summary>
/// The enum for all permissions, these are never serialized as ints so have no fear there, they are just serialized as bitbundles.
/// </summary>
public enum Permissions
{
    TRANSFER_CLUB_OWNERSHIP = 0,
    DELETE_CLUB,
    RENAME_CLUB,
    ADMINISTRATOR,
    MODIFY_CLUBHOUSE,
    PROMOTE_PLAYER,
    DEMOTE_PLAYER,
    DELETE_ROLE,
    CREATE_ROLE,
    RENAME_ROLE,
    INVITE_TO_CLUB,
    KICK_PLAYER_FROM_CLUB,
    BAN_PLAYER,
    BAN_PLAYER_FROM_CLUBHOUSE,
    UNBAN_PLAYER_FROM_CLUBHOUSE,
    CHANGE_CLUBHOUSE_PRIVACY_LEVEL,
    SEND_CLUB_BROADCAST,
    CREATE_EVENT,
    DELETE_EVENT,
    MODIFY_EVENT,
    UNBAN_PLAYER,
    TEMP_BAN_PLAYER,
    CHANGE_DESCRIPTION,
    CHANGE_RECRUITING_STATUS,
    CHANGE_ICON,
    CHANGE_TAGS
}