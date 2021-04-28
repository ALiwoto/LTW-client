// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using LTW.SandBox;
using LTW.Constants;
using LTW.Security;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Kingdoms
{
    public class PowerRankings : Rankings
    {
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// File name in the server.
        /// </summary>
        public const string PowerRankings_LOC = "ぱウェーランキング.LTW";
        /// <summary>
        /// The Value is Kizu, use in server to separate the players from each other.
        /// </summary>
        public const string OutCharSeparator = "傷";
        /// <summary>
        /// The Value is Ame, use in server to separate the players' Info from each other.
        /// </summary>
        public const string InCharSeparator = "雨";
        /// <summary>
        /// This is 50.
        /// </summary>
        public const uint MAXIMUM_RANKS = 0b110010;
        private const string MESSAGE = "P_R -- LTW";
        private const int NAME_INDEX = 0;
        private const int POWER_INDEX = 1;
        private const int UID_INDEX = 2;
        #endregion
        //-------------------------------------------------
        #region online Properties Region
        /// <summary>
        /// the names of the players in the rankings.
        /// The check Code is 0.
        /// </summary>
        public StrongString[] PlayerNames { get; private set; }
        /// <summary>
        /// the powers of the players in the rankings.
        /// the check Code is 1.
        /// </summary>
        public Unit[] PlayerPowers { get; private set; }
        /// <summary>
        /// the UID of the players in the rankings.
        /// the check code is 2.
        /// </summary>
        public UID[] PlayerUIDs { get; private set; }
        #endregion
        //-------------------------------------------------
        #region offline Properties Region
        public KingdomInfo Kingdom { get; private set; }
        public LTW_Kingdoms Provider
        {
            get
            {
                if (Kingdom != null)
                {
                    return Kingdom.Provider;
                }
                return LTW_Kingdoms.NotSet;
            }
        }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private PowerRankings()
        {
            RankingsMode = RankingsMode.PowerRankings;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public StrongString GetForServer()
        {
            StrongString myString = OutCharSeparator;
            for (int i = 0; i < MAXIMUM_RANKS; i++)
            {
                myString += 
                    PlayerNames[i]                  + InCharSeparator + // 0
                    PlayerPowers[i].GetForServer()  + InCharSeparator + // 1
                    PlayerUIDs[i].GetValue()        + InCharSeparator + // 2
                    OutCharSeparator;
            }
            return myString;
        }
        public async Task<DataBaseDataChangedInfo> UpdatePowerRankings()
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(Provider);
            var _target = PowerRankings_LOC;
            var existiong = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------

            if (existiong.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }

            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(MESSAGE, QString.Parse(GetForServer()), existiong.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static async Task<DataBaseDataChangedInfo> CreatePowerRankings(KingdomInfo kingdom)
        {
            PowerRankings powerRankings = new PowerRankings()
            {
                Kingdom = kingdom,
                PlayerNames = new StrongString[MAXIMUM_RANKS],
                PlayerPowers = new Unit[MAXIMUM_RANKS],
                PlayerUIDs = new UID[MAXIMUM_RANKS],
            };
            for (int i = 0; i < MAXIMUM_RANKS; i++)
            {
                powerRankings.PlayerNames[i] = ThereIsConstants.Path.NotSet;
                powerRankings.PlayerPowers[i] = Unit.GetBasicUnit();
                powerRankings.PlayerUIDs[i] = UID.GenerateNullUID();
            }
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(kingdom.Provider);
            var _target = PowerRankings_LOC;
            var _req = new DataBaseCreation(MESSAGE, QString.Parse(powerRankings.GetForServer()));
            return await ThereIsServer.Actions.CreateData(_s, _target, _req);
            //---------------------------------------------
        }
        public static async Task<PowerRankings> GetPowerRankings(KingdomInfo kingdom)
        {
            PowerRankings powerRankings = new PowerRankings()
            {
                Kingdom = kingdom,
                PlayerNames = new StrongString[MAXIMUM_RANKS],
                PlayerPowers = new Unit[MAXIMUM_RANKS],
                PlayerUIDs = new UID[MAXIMUM_RANKS],
            };
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(kingdom.Provider);
            var _target = PowerRankings_LOC;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                await CreatePowerRankings(kingdom);
                //---------------------------------------------
                existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                //---------------------------------------------
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
                }
            }
            else
            {
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
                }
            }
            StrongString[] myStrings = existing.Decode().Split(OutCharSeparator);
            StrongString[] InStrings;
            for (int i = 0; i < MAXIMUM_RANKS; i++)
            {
                InStrings = myStrings[i].Split(InCharSeparator);
                powerRankings.PlayerNames[i] = InStrings[NAME_INDEX];
                powerRankings.PlayerPowers[i] = Unit.ConvertToUnit(InStrings[POWER_INDEX]);
                powerRankings.PlayerUIDs[i] = UID.GetUID(InStrings[UID_INDEX]);
            }
            return powerRankings;
        }
        public static async Task<bool> DeletePowerRankings(LTW_Kingdoms _kingdom_)
        {
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(_kingdom_);
            var _target = PowerRankings_LOC;
            
            if (await ThereIsServer.Actions.Exists(_s, _target))
            {
                var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                var _req = new DataBaseDeleteRequest(MESSAGE, existing.Sha.GetStrong());
                var _del = await ThereIsServer.Actions.DeleteData(_s, _target, _req);
                return _del;
            }
            return true;
        }
        #endregion
        //-------------------------------------------------
    }
}
