// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;

namespace LTW.GameObjects.Players
{
    public interface IPlayerSocket
    {
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// player login file's Server's Index.
        /// this login file is neccessary when you want to
        /// check if another device has loged in into the account 
        /// or not.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 0.</code>
        /// </summary>
        int LoginS_Index { get; }
        /// <summary>
        /// PlayerInfo file's server's Index.
        /// this file contains the PlayerInfo class.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 1.</code>
        /// </summary>
        int PlayerInfoS_Index { get; }
        /// <summary>
        /// Me's Server's Index.
        /// this file contains the class Me.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 2.</code>
        /// </summary>
        int MeS_Index { get; }
        /// <summary>
        /// Player's Server's Index.
        /// this server, should contains the file which
        /// contains the class player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 3.</code>
        /// </summary>
        int PlayerS_Index { get; }
        /// <summary>
        /// Troop's Sever Index.
        /// the ordinary Troops of the player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 4.</code>
        /// </summary>
        int TroopS_Index { get; }
        /// <summary>
        /// Magical Troops' Server Index.
        /// the Magical Troops of the player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 5.</code>
        /// </summary>
        int MagicalTroopS_Index { get; }
        /// <summary>
        /// Resources Server Index.
        /// The Resources of the player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 6.</code>
        /// </summary>
        int ResourcesS_Index { get; }
        /// <summary>
        /// Player Heroes Socket file's Server Index.
        /// this file should contain the socket file for player heroes.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 7.</code>
        /// </summary>
        int PlayerHeroesS_Index { get; }
        /// <summary>
        /// Secured Me Server Index.
        /// this file is for Secured Me.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 8.</code>
        /// </summary>
        int SecuredMeS_Index { get; }
        /// <summary>
        /// Player's Items Server Index.
        /// this file is contains the Item Socket for players.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 9.</code>
        /// </summary>
        int PlayerItemsS_Index { get; }
        #endregion
        //-------------------------------------------------
        #region Method's Region
        StrongString GetForServer();
        #endregion
        //-------------------------------------------------
    }
}
