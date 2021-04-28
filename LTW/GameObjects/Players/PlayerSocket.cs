// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;
using LTW.GameObjects.WMath;
using LTW.GameObjects.ServerObjects;

namespace LTW.GameObjects.Players
{
    public class PlayerSocket : ISocket, IPlayerSocket
    {
        //-------------------------------------------------
        #region Constant's Region
        private const string _int_format = "00";
        private const string CharSeparator = "諸軍";
        private const int INDEX_0 = 0;
        private const int INDEX_1 = 1;
        private const int INDEX_2 = 2;
        private const int INDEX_3 = 3;
        private const int INDEX_4 = 4;
        private const int INDEX_5 = 5;
        private const int INDEX_6 = 6;
        private const int INDEX_7 = 7;
        private const int INDEX_8 = 8;
        private const int INDEX_9 = 9;
        private const int MAX_0 = ServerManager.MAX_LOGIN_SERVERS;
        private const int MAX_1 = ServerManager.MAX_PLAYERINFO_SERVER;
        private const int MAX_2 = ServerManager.MAX_ME_SERVERS;
        private const int MAX_3 = ServerManager.MAX_PLAYER_SERVERS;
        private const int MAX_4 = ServerManager.MAX_TROOPS_SERVERS;
        private const int MAX_5 = ServerManager.MAX_MTROOPS_SERVERS;
        private const int MAX_6 = ServerManager.MAX_RESOURCES_SERVERS;
        private const int MAX_7 = ServerManager.MAX_PLAYER_HERO_SOCKET_SERVERS;
        private const int MAX_8 = ServerManager.MAX_SECURED_ME_SERVERS;
        private const int MAX_9 = ServerManager.MAX_PLAYER_ITEMS_SOCKET_SERVERS;
        private const int MIN_T = 0;
        #endregion
        //-------------------------------------------------
        #region Peroperties Region
        /// <summary>
        /// player login file's Server's Index.
        /// this login file is neccessary when you want to
        /// check if another device has loged in into the account 
        /// or not.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 0.</code>
        /// </summary>
        public int LoginS_Index { get; private set; }
        /// <summary>
        /// PlayerInfo file's server's Index.
        /// this file contains the PlayerInfo class.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 1.</code>
        /// </summary>
        public int PlayerInfoS_Index { get; private set; }
        /// <summary>
        /// Me's Server's Index.
        /// this file contains the class Me.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 2.</code>
        /// </summary>
        public int MeS_Index { get; private set; }
        /// <summary>
        /// Player's Server's Index.
        /// this server, should contains the file which
        /// contains the class player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 3.</code>
        /// </summary>
        public int PlayerS_Index { get; private set; }
        /// <summary>
        /// Troop's Sever Index.
        /// the ordinary Troops of the player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 4.</code>
        /// </summary>
        public int TroopS_Index { get; private set; }
        /// <summary>
        /// Magical Troops' Server Index.
        /// the Magical Troops of the player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 5.</code>
        /// </summary>
        public int MagicalTroopS_Index { get; private set; }
        /// <summary>
        /// Resources Server Index.
        /// The Resources of the player.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 6.</code>
        /// </summary>
        public int ResourcesS_Index { get; private set; }
        /// <summary>
        /// Player Heroes Socket file's Server Index.
        /// this file should contain the socket file for player heroes.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 7.</code>
        /// </summary>
        public int PlayerHeroesS_Index { get; private set; }
        /// <summary>
        /// Secured Me Server Index.
        /// this file is for Secured Me.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 8.</code>
        /// </summary>
        public int SecuredMeS_Index { get; private set; }
        /// <summary>
        /// Player's Items Server Index.
        /// this file is contains the Item Socket for players.
        /// the format in the GetForServer should be the "00".
        /// <code>The Code is 9.</code>
        /// </summary>
        public int PlayerItemsS_Index { get; private set; }
        #endregion
        //-------------------------------------------------
        #region field's Region
        private readonly StrongString _value;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private PlayerSocket()
        {
            _value = GenerateNew();
        }
        private PlayerSocket(StrongString _value_)
        {
            SetParams(_value_);
            _value = _value_;
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public StrongString GetForServer()
        {
            return _value;
        }
        private StrongString GenerateNew()
        {
            LoginS_Index        = Randomic.GetRandom(MIN_T, MAX_0);
            PlayerInfoS_Index   = Randomic.GetRandom(MIN_T, MAX_1);
            MeS_Index           = Randomic.GetRandom(MIN_T, MAX_2);
            PlayerS_Index       = Randomic.GetRandom(MIN_T, MAX_3);
            TroopS_Index        = Randomic.GetRandom(MIN_T, MAX_4);
            MagicalTroopS_Index = Randomic.GetRandom(MIN_T, MAX_5);
            ResourcesS_Index    = Randomic.GetRandom(MIN_T, MAX_6);
            PlayerHeroesS_Index = Randomic.GetRandom(MIN_T, MAX_7);
            SecuredMeS_Index    = Randomic.GetRandom(MIN_T, MAX_8);
            PlayerItemsS_Index  = Randomic.GetRandom(MIN_T, MAX_9);
            return SumSalvation();
        }
        private StrongString SumSalvation()
        {
            StrongString myString =               CharSeparator + 
                LoginS_Index.ToString(_int_format)         + CharSeparator + // 0
                PlayerInfoS_Index.ToString(_int_format)    + CharSeparator + // 1
                MeS_Index.ToString(_int_format)            + CharSeparator + // 2
                PlayerS_Index.ToString(_int_format)        + CharSeparator + // 3
                TroopS_Index.ToString(_int_format)         + CharSeparator + // 4
                MagicalTroopS_Index.ToString(_int_format)  + CharSeparator + // 5
                ResourcesS_Index.ToString(_int_format)     + CharSeparator + // 6
                PlayerHeroesS_Index.ToString(_int_format)  + CharSeparator + // 7
                SecuredMeS_Index.ToString(_int_format)     + CharSeparator + // 8
                PlayerItemsS_Index.ToString(_int_format)   + CharSeparator;  // 9
            return myString;
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        private void SetParams(StrongString _value)
        {
            StrongString[] myStrings = _value.Split(CharSeparator);
            if (myStrings.Length > INDEX_0)
            {
                LoginS_Index = myStrings[INDEX_0].ToInt32();
            }
            else
            {
                LoginS_Index = Randomic.GetRandom(MIN_T, MAX_0);
            }
            if (myStrings.Length > INDEX_1)
            {
                PlayerInfoS_Index = myStrings[INDEX_1].ToInt32();
            }
            else
            {
                PlayerInfoS_Index = Randomic.GetRandom(MIN_T, MAX_1);
            }
            if (myStrings.Length > INDEX_2)
            {
                MeS_Index = myStrings[INDEX_2].ToInt32();
            }
            else
            {
                MeS_Index = Randomic.GetRandom(MIN_T, MAX_2);
            }
            if (myStrings.Length > INDEX_3)
            {
                PlayerS_Index = myStrings[INDEX_3].ToInt32();
            }
            else
            {
                PlayerS_Index = Randomic.GetRandom(MIN_T, MAX_3);
            }
            if (myStrings.Length > INDEX_4)
            {
                TroopS_Index = myStrings[INDEX_4].ToInt32();
            }
            else
            {
                TroopS_Index = Randomic.GetRandom(MIN_T, MAX_4);
            }
            if (myStrings.Length > INDEX_5)
            {
                MagicalTroopS_Index = myStrings[INDEX_5].ToInt32();
            }
            else
            {
                MagicalTroopS_Index = Randomic.GetRandom(MIN_T, MAX_5);
            }
            if (myStrings.Length > INDEX_6)
            {
                ResourcesS_Index = myStrings[INDEX_6].ToInt32();
            }
            else
            {
                ResourcesS_Index = Randomic.GetRandom(MIN_T, MAX_6);
            }
            if (myStrings.Length > INDEX_7)
            {
                PlayerHeroesS_Index = myStrings[INDEX_7].ToInt32();
            }
            else
            {
                PlayerHeroesS_Index = Randomic.GetRandom(MIN_T, MAX_7);
            }
            if (myStrings.Length > INDEX_8)
            {
                SecuredMeS_Index = myStrings[INDEX_8].ToInt32();
            }
            else
            {
                SecuredMeS_Index = Randomic.GetRandom(MIN_T, MAX_8);
            }
            if (myStrings.Length > INDEX_9)
            {
                PlayerItemsS_Index = myStrings[INDEX_9].ToInt32();
            }
            else
            {
                PlayerItemsS_Index = Randomic.GetRandom(MIN_T, MAX_9);
            }
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static PlayerSocket Parse(StrongString serverString)
        {
            return new PlayerSocket(serverString);
        }
        public static PlayerSocket Generate()
        {
            return new PlayerSocket();
        }
        #endregion
        //-------------------------------------------------
    }
}
