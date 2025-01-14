// Copyright (c) Tablebound LLC. 2025 and affiliates.
// All rights reserved.
using Meepl.API.Enums;
using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

/// <summary>
/// The role metadata for a particular role, 
/// </summary>
public class Role : IMercurial
{
    public string Name;
    public int Color;
    public bool TRANSFER_CLUB_OWNERSHIP;
    public bool DELETE_CLUB;
    public bool RENAME_CLUB;
    public bool ADMINISTRATOR;
    public bool MODIFY_CLUBHOUSE;
    public bool PROMOTE_PLAYER;
    public bool DEMOTE_PLAYER;
    public bool DELETE_ROLE;
    public bool CREATE_ROLE;
    public bool RENAME_ROLE;
    public bool INVITE_TO_CLUB;
    public bool KICK_PLAYER_FROM_CLUB;
    public bool BAN_PLAYER;
    public bool BAN_PLAYER_FROM_CLUBHOUSE;
    public bool UNBAN_PLAYER_FROM_CLUBHOUSE;
    public bool CHANGE_CLUBHOUSE_PRIVACY_LEVEL;
    public bool SEND_CLUB_BROADCAST;
    public bool CREATE_EVENT;
    public bool DELETE_EVENT;
    public bool MODIFY_EVENT;
    public bool UNBAN_PLAYER;
    public bool TEMP_BAN_PLAYER;
    public bool CHANGE_DESCRIPTION;
    public bool CHANGE_RECRUITING_STATUS;
    public bool CHANGE_ICON;
    public bool CHANGE_TAGS;

    /// <summary>
    /// This constructor is only for deserializing from a Mercurial Blob
    /// </summary>
    public Role()
    { }
    
    /// <summary>
    /// This is the primary constructor for Roles
    /// </summary>
    /// <param name="name">The name of the Role</param>
    /// <param name="color">The color the role displays as</param>
    /// <param name="permissions">The permissions for the role</param>
    public Role(string name, int color, params Permissions[] permissions)
    {
        Name = name;
        foreach (Permissions permission in permissions)
        {
            GrantPermission(permission);
        }
    }
    

    public byte[] GetBytes()
    {
        Pack pack = new Pack();
        BitBundleFactory bundleFactory = new BitBundleFactory();
        bundleFactory
            .Append(TRANSFER_CLUB_OWNERSHIP)
            .Append(DELETE_CLUB)
            .Append(RENAME_CLUB)
            .Append(ADMINISTRATOR)
            .Append(MODIFY_CLUBHOUSE)
            .Append(PROMOTE_PLAYER)
            .Append(DEMOTE_PLAYER)
            .Append(DELETE_ROLE)
            .Append(CREATE_ROLE)
            .Append(RENAME_ROLE)
            .Append(INVITE_TO_CLUB)
            .Append(KICK_PLAYER_FROM_CLUB)
            .Append(BAN_PLAYER)
            .Append(BAN_PLAYER_FROM_CLUBHOUSE)
            .Append(UNBAN_PLAYER_FROM_CLUBHOUSE)
            .Append(CHANGE_CLUBHOUSE_PRIVACY_LEVEL)
            .Append(SEND_CLUB_BROADCAST)
            .Append(CREATE_EVENT)
            .Append(DELETE_EVENT)
            .Append(MODIFY_EVENT)
            .Append(UNBAN_PLAYER)
            .Append(TEMP_BAN_PLAYER)
            .Append(CHANGE_DESCRIPTION)
            .Append(CHANGE_RECRUITING_STATUS)
            .Append(CHANGE_ICON)
            .Append(CHANGE_TAGS);
        return pack
            .Append(Name)
            .Append(Color)
            .Append(bundleFactory)
            .Build();
    }

    public void AppendComponentBytes(Pack packer)
    {
        BitBundleFactory bundleFactory = new BitBundleFactory();
        bundleFactory
            .Append(TRANSFER_CLUB_OWNERSHIP)
            .Append(DELETE_CLUB)
            .Append(RENAME_CLUB)
            .Append(ADMINISTRATOR)
            .Append(MODIFY_CLUBHOUSE)
            .Append(PROMOTE_PLAYER)
            .Append(DEMOTE_PLAYER)
            .Append(DELETE_ROLE)
            .Append(CREATE_ROLE)
            .Append(RENAME_ROLE)
            .Append(INVITE_TO_CLUB)
            .Append(KICK_PLAYER_FROM_CLUB)
            .Append(BAN_PLAYER)
            .Append(BAN_PLAYER_FROM_CLUBHOUSE)
            .Append(UNBAN_PLAYER_FROM_CLUBHOUSE)
            .Append(CHANGE_CLUBHOUSE_PRIVACY_LEVEL)
            .Append(SEND_CLUB_BROADCAST)
            .Append(CREATE_EVENT)
            .Append(DELETE_EVENT)
            .Append(MODIFY_EVENT)
            .Append(UNBAN_PLAYER)
            .Append(TEMP_BAN_PLAYER)
            .Append(CHANGE_DESCRIPTION)
            .Append(CHANGE_RECRUITING_STATUS)
            .Append(CHANGE_ICON)
            .Append(CHANGE_TAGS);
        packer
            .Append(Name)
            .Append(Color)
            .Append(bundleFactory);
    }

    public void FromBytes(byte[] payload)
    {
        Unpack unpack = new Unpack(payload);
        BitBundleUnpacker bundleFactory = new BitBundleUnpacker();
        unpack
            .Read(ref Name)
            .Read(ref Color)
            .Read(ref bundleFactory)
            .Finish();
        bundleFactory
            .Read(ref TRANSFER_CLUB_OWNERSHIP)
            .Read(ref DELETE_CLUB)
            .Read(ref RENAME_CLUB)
            .Read(ref ADMINISTRATOR)
            .Read(ref MODIFY_CLUBHOUSE)
            .Read(ref PROMOTE_PLAYER)
            .Read(ref DEMOTE_PLAYER)
            .Read(ref DELETE_ROLE)
            .Read(ref CREATE_ROLE)
            .Read(ref RENAME_ROLE)
            .Read(ref INVITE_TO_CLUB)
            .Read(ref KICK_PLAYER_FROM_CLUB)
            .Read(ref BAN_PLAYER)
            .Read(ref BAN_PLAYER_FROM_CLUBHOUSE)
            .Read(ref UNBAN_PLAYER_FROM_CLUBHOUSE)
            .Read(ref CHANGE_CLUBHOUSE_PRIVACY_LEVEL)
            .Read(ref SEND_CLUB_BROADCAST)
            .Read(ref CREATE_EVENT)
            .Read(ref DELETE_EVENT)
            .Read(ref MODIFY_EVENT)
            .Read(ref UNBAN_PLAYER)
            .Read(ref TEMP_BAN_PLAYER)
            .Read(ref CHANGE_DESCRIPTION)
            .Read(ref CHANGE_RECRUITING_STATUS)
            .Read(ref CHANGE_ICON)
            .Read(ref CHANGE_TAGS);
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        BitBundleUnpacker bundleFactory = new BitBundleUnpacker();
        unpack
            .Read(ref Name)
            .Read(ref Color)
            .Read(ref bundleFactory)
            .Finish();
        bundleFactory
            .Read(ref TRANSFER_CLUB_OWNERSHIP)
            .Read(ref DELETE_CLUB)
            .Read(ref RENAME_CLUB)
            .Read(ref ADMINISTRATOR)
            .Read(ref MODIFY_CLUBHOUSE)
            .Read(ref PROMOTE_PLAYER)
            .Read(ref DEMOTE_PLAYER)
            .Read(ref DELETE_ROLE)
            .Read(ref CREATE_ROLE)
            .Read(ref RENAME_ROLE)
            .Read(ref INVITE_TO_CLUB)
            .Read(ref KICK_PLAYER_FROM_CLUB)
            .Read(ref BAN_PLAYER)
            .Read(ref BAN_PLAYER_FROM_CLUBHOUSE)
            .Read(ref UNBAN_PLAYER_FROM_CLUBHOUSE)
            .Read(ref CHANGE_CLUBHOUSE_PRIVACY_LEVEL)
            .Read(ref SEND_CLUB_BROADCAST)
            .Read(ref CREATE_EVENT)
            .Read(ref DELETE_EVENT)
            .Read(ref MODIFY_EVENT)
            .Read(ref UNBAN_PLAYER)
            .Read(ref TEMP_BAN_PLAYER)
            .Read(ref CHANGE_DESCRIPTION)
            .Read(ref CHANGE_RECRUITING_STATUS)
            .Read(ref CHANGE_ICON)
            .Read(ref CHANGE_TAGS);
    }

    /// <summary>
    /// Grants a permission to a given role
    /// </summary>
    /// <note>
    /// If the role already did have the permission this is simply just going to set it to true again.
    /// </note>
    /// <param name="permission">The permission you want to grant to the role</param>
    public void GrantPermission(Permissions permission)
    {
        switch (permission)
        {
            case Permissions.TRANSFER_CLUB_OWNERSHIP:
                TRANSFER_CLUB_OWNERSHIP = true;
                break;
            case Permissions.DELETE_CLUB:
                DELETE_CLUB = true;
                break;
            case Permissions.RENAME_CLUB:
                RENAME_CLUB = true;
                break;
            case Permissions.ADMINISTRATOR:
                ADMINISTRATOR = true;
                break;
            case Permissions.MODIFY_CLUBHOUSE:
                MODIFY_CLUBHOUSE = true;
                break;
            case Permissions.PROMOTE_PLAYER:
                PROMOTE_PLAYER = true;
                break;
            case Permissions.DEMOTE_PLAYER:
                DEMOTE_PLAYER = true;
                break;
            case Permissions.DELETE_ROLE:
                DELETE_ROLE = true;
                break;
            case Permissions.CREATE_ROLE:
                CREATE_ROLE = true;
                break;
            case Permissions.RENAME_ROLE:
                RENAME_ROLE = true;
                break;
            case Permissions.INVITE_TO_CLUB:
                INVITE_TO_CLUB = true;
                break;
            case Permissions.KICK_PLAYER_FROM_CLUB:
                KICK_PLAYER_FROM_CLUB = true;
                break;
            case Permissions.BAN_PLAYER:
                BAN_PLAYER = true;
                break;
            case Permissions.BAN_PLAYER_FROM_CLUBHOUSE:
                BAN_PLAYER_FROM_CLUBHOUSE = true;
                break;
            case Permissions.UNBAN_PLAYER_FROM_CLUBHOUSE:
                UNBAN_PLAYER_FROM_CLUBHOUSE = true;
                break;
            case Permissions.CHANGE_CLUBHOUSE_PRIVACY_LEVEL:
                CHANGE_CLUBHOUSE_PRIVACY_LEVEL = true;
                break;
            case Permissions.SEND_CLUB_BROADCAST:
                SEND_CLUB_BROADCAST = true;
                break;
            case Permissions.CREATE_EVENT:
                CREATE_EVENT = true;
                break;
            case Permissions.DELETE_EVENT:
                DELETE_EVENT = true;
                break;
            case Permissions.MODIFY_EVENT:
                MODIFY_EVENT = true;
                break;
            case Permissions.UNBAN_PLAYER:
                UNBAN_PLAYER = true;
                break;
            case Permissions.TEMP_BAN_PLAYER:
                TEMP_BAN_PLAYER = true;
                break;
            case Permissions.CHANGE_DESCRIPTION:
                CHANGE_DESCRIPTION = true;
                break;
            case Permissions.CHANGE_RECRUITING_STATUS:
                CHANGE_RECRUITING_STATUS = true;
                break;
            case Permissions.CHANGE_ICON:
                CHANGE_ICON = true;
                break;
            case Permissions.CHANGE_TAGS:
                CHANGE_TAGS = true;
                break;
        }
    }
    
    /// <summary>
    /// Removes a given permission from a role if it has it.
    /// </summary>
    /// <note>
    /// If the role already didnt have the permission this is simply just going to set it to false again.
    /// </note>
    /// <param name="permission">The permission you would like to revoke</param>
    public void RevokePermission(Permissions permission)
    {
        switch (permission)
        {
            case Permissions.TRANSFER_CLUB_OWNERSHIP:
                TRANSFER_CLUB_OWNERSHIP = false;
                break;
            case Permissions.DELETE_CLUB:
                DELETE_CLUB = false;
                break;
            case Permissions.RENAME_CLUB:
                RENAME_CLUB = false;
                break;
            case Permissions.ADMINISTRATOR:
                ADMINISTRATOR = false;
                break;
            case Permissions.MODIFY_CLUBHOUSE:
                MODIFY_CLUBHOUSE = false;
                break;
            case Permissions.PROMOTE_PLAYER:
                PROMOTE_PLAYER = false;
                break;
            case Permissions.DEMOTE_PLAYER:
                DEMOTE_PLAYER = false;
                break;
            case Permissions.DELETE_ROLE:
                DELETE_ROLE = false;
                break;
            case Permissions.CREATE_ROLE:
                CREATE_ROLE = false;
                break;
            case Permissions.RENAME_ROLE:
                RENAME_ROLE = false;
                break;
            case Permissions.INVITE_TO_CLUB:
                INVITE_TO_CLUB = false;
                break;
            case Permissions.KICK_PLAYER_FROM_CLUB:
                KICK_PLAYER_FROM_CLUB = false;
                break;
            case Permissions.BAN_PLAYER:
                BAN_PLAYER = false;
                break;
            case Permissions.BAN_PLAYER_FROM_CLUBHOUSE:
                BAN_PLAYER_FROM_CLUBHOUSE = false;
                break;
            case Permissions.UNBAN_PLAYER_FROM_CLUBHOUSE:
                UNBAN_PLAYER_FROM_CLUBHOUSE = false;
                break;
            case Permissions.CHANGE_CLUBHOUSE_PRIVACY_LEVEL:
                CHANGE_CLUBHOUSE_PRIVACY_LEVEL = false;
                break;
            case Permissions.SEND_CLUB_BROADCAST:
                SEND_CLUB_BROADCAST = false;
                break;
            case Permissions.CREATE_EVENT:
                CREATE_EVENT = false;
                break;
            case Permissions.DELETE_EVENT:
                DELETE_EVENT = false;
                break;
            case Permissions.MODIFY_EVENT:
                MODIFY_EVENT = false;
                break;
            case Permissions.UNBAN_PLAYER:
                UNBAN_PLAYER = false;
                break;
            case Permissions.TEMP_BAN_PLAYER:
                TEMP_BAN_PLAYER = false;
                break;
            case Permissions.CHANGE_DESCRIPTION:
                CHANGE_DESCRIPTION = false;
                break;
            case Permissions.CHANGE_RECRUITING_STATUS:
                CHANGE_RECRUITING_STATUS = false;
                break;
            case Permissions.CHANGE_ICON:
                CHANGE_ICON = false;
                break;
            case Permissions.CHANGE_TAGS:
                CHANGE_TAGS = false;
                break;
        }
    }
    
}