using Mercurial.Interfaces;
using Mercurial.Util;

namespace Meepl.API.MercurialBlobs;

public class MeeplProfile : IMercurial
{
    #region Core Elements

    /// <summary>
    /// This is the real backend identifier we use to identify a user
    /// </summary>
    public MeeplIdentifier MeeplIdentifier;

    /// <summary>
    /// The username of the user you can find this player by in Meepl
    /// </summary>
    public string Username;

    /// <summary>
    /// The short bio of the user found on their profile in Meepl
    /// </summary>
    public string Biography;

    /// <summary>
    /// The 'action' text for a user "PlayerA is now playing Overrealms"
    /// </summary>
    public string Action;

    /// <summary>
    /// The cdn link to the profile image for this player
    /// </summary>
    public string ProfileCDNLink;

    /// <summary>
    /// The status indicator of a player ex. online, offline, away, etc.
    /// </summary>
    public StatusIndicator Indicator;

    #endregion

    #region Meepl Universe Equipped Items

    /// <summary>
    /// A toggle-able title for a profile that is unlocked via Meepl Universe
    /// </summary>
    public ulong UniverseTitle;

    #endregion

    #region Meepl Social

    public PersonListBlob FriendsList;
    public PersonListBlob BlockedList;

    #endregion
    
    #region Mercurial Serialization

    public byte[] GetBytes()
    {
        throw new NotImplementedException();
    }

    public void AppendComponentBytes(Pack packer)
    {
        throw new NotImplementedException();
    }

    public void FromBytes(byte[] payload)
    {
        throw new NotImplementedException();
    }

    public void ComponentFromBytes(Unpack unpack)
    {
        throw new NotImplementedException();
    }

    #endregion
}