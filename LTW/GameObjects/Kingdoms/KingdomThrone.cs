// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using LTW.SandBox;
using LTW.Security;
using LTW.Constants;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Kingdoms
{
    /// <summary>
    /// King                1
    /// Queen               2
    /// MinisterOfWar       3
    /// MinisterOfWealth    4
    /// Hierarch            5
    /// Guardians' Chief    6
    /// Clown               7
    /// </summary>
    public class KingdomThrone
    {
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// KingdomThrone File Name for getting and saving it from and to in server.
        /// </summary>
        public const string KingdomThrone_LOC       = "ソロノ";
        /// <summary>
        /// The Value is: Gunsotsu
        /// </summary>
        public const string InCharSeparator         = "傷花";
        public const string OutCharSeparator        = "囮";
        /// <summary>
        /// The MAXIMUM of the Throne Position count.
        /// This is 7.
        /// </summary>
        public const int MAXIMUM_POSITION           = 0b111;
        private const string MESSAGE                = "T_R -- LTW";
        private const int NAME_INNER_INDEX                = 0;
        private const int UID_INNER_INDEX                 = 1;
        #endregion
        //-------------------------------------------------
        #region Server Properties Region
        /// <summary>
        /// The Player Names.
        /// The Code is 0.
        /// </summary>
        public StrongString[] PlayerNames { get; private set; }
        /// <summary>
        /// The Players' UIDs.
        /// The Code is 1.
        /// </summary>
        public UID[] PlayerUIDs { get; private set; }
        #endregion
        //-------------------------------------------------
        #region Offline Properties Region
        public KingdomInfo Kingdom { get; internal set; }
        public int Length
        {
            get
            {
                if (PlayerNames != null)
                {
                    return PlayerNames.Length;
                }
                else
                {
                    return 0;
                }
            }
        }
        public LTW_Kingdoms Provider
        {
            get
            {
                if (Kingdom != null)
                {
                    return Kingdom.Provider;
                }
                else
                {
                    return LTW_Kingdoms.NotSet;
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private KingdomThrone()
        {
            ;
        }
        #endregion
        //-------------------------------------------------
        #region Offline Methods Region
        public StrongString GetForServer()
        {
            StrongString myString = OutCharSeparator;
            for (int i = 0; i < Length; i++)
            {
                myString +=
                    PlayerNames[i]              + InCharSeparator + // index : 0
                    PlayerUIDs[i].GetValue()    + InCharSeparator + // index : 1
                    OutCharSeparator;  
            }
            return myString;
        }
        /// <summary>
        /// NOTICE: The object of <see cref="PlayerInfo"/>
        /// that I will return, does not exists in the server,
        /// please do it yourself.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public PlayerInfo GetPlayerInfo(ThronePositions position)
        {
            PlayerInfo player = null;
            if (position == ThronePositions.OrdinaryCitizen)
            {
                return player;
            }
            int _index = (int)position - 1;
            player = PlayerInfo.GetPlayerInfo(PlayerNames[_index]);
            return player;
        }
        #endregion
        //-------------------------------------------------
        #region Online and non-static Methods region
        public async Task<DataBaseDataChangedInfo> UpdateKingdomThrone()
        {
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(Provider);
            var _target = KingdomThrone_LOC;
            var _req = new DataBaseCreation(MESSAGE, QString.Parse(GetForServer()));
            return await ThereIsServer.Actions.CreateData(_s, _target, _req);
        }
        #endregion
        //-------------------------------------------------
        #region static mathods region
        //These methods are server and online working
        //so you should use await keyword for them.
        public static async Task<DataBaseDataChangedInfo> CreateKingdomThrone(KingdomInfo kingdomInfo)
        {
            var _notSet = new StrongString[]
            {
                ThereIsConstants.Path.NotSet, // 0
                ThereIsConstants.Path.NotSet, // 1
                ThereIsConstants.Path.NotSet, // 2
                ThereIsConstants.Path.NotSet, // 3
                ThereIsConstants.Path.NotSet, // 4
                ThereIsConstants.Path.NotSet, // 5
                ThereIsConstants.Path.NotSet, // 6
            };
            KingdomThrone kingdomThrone = new KingdomThrone()
            {
                PlayerNames = _notSet,
                PlayerUIDs = UID.GenerateNullUIDs(MAXIMUM_POSITION),
            };
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(kingdomInfo.Provider);
            var _target = KingdomThrone_LOC;
            var _req = new DataBaseCreation(MESSAGE, QString.Parse(kingdomThrone.GetForServer()));
            return await ThereIsServer.Actions.CreateData(_s, _target, _req);
            //---------------------------------------------
        }
        public static async Task<KingdomThrone> GetKingdomThrone(KingdomInfo kingdom)
        {
            KingdomThrone kingdomThrone = new KingdomThrone();
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(kingdom.Provider);
            var _target = KingdomThrone_LOC;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.DoesNotExist)
            {
                await CreateKingdomThrone(kingdom);
                //---------------------------------------------
                _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                //---------------------------------------------
                if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
            }
            else
            {
                if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
            }
            StrongString[] myStrings = _existing.Decode().Split(OutCharSeparator);
            kingdomThrone.PlayerNames = new StrongString[myStrings.Length];
            kingdomThrone.PlayerUIDs = new UID[myStrings.Length];
            StrongString[] anothers;
            for (int i = 0; i < myStrings.Length; i++)
            {
                anothers = myStrings[i].Split(InCharSeparator);
                kingdomThrone.PlayerNames[i] = anothers[NAME_INNER_INDEX];
                kingdomThrone.PlayerUIDs[i]  = UID.GetUID(anothers[UID_INNER_INDEX]);
            }
            return kingdomThrone;
        }
        public static async Task<bool> DeleteThrone(LTW_Kingdoms _kingdom_)
        {
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(_kingdom_);
            var _target = KingdomThrone_LOC;
            if (await ThereIsServer.Actions.Exists(_s, _target))
            {
                var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                var _req = new DataBaseDeleteRequest(MESSAGE, existing.Sha.GetStrong());
                return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            }
            return true;
        }
        #endregion
        //-------------------------------------------------
    }
}
