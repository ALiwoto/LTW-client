// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using LTW.Constants;
using LTW.SandBox;
using LTW.Security;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Kingdoms
{
    public sealed class LevelRankings : Rankings
    {
        //-------------------------------------------------
        #region Constant's Region
        /// <summary>
        /// File name in the server.
        /// </summary>
        public const string LevelRankings_LOC = "レウェローランキング.LTW";
        /// <summary>
        /// Value is ｖ, use in server.
        /// </summary>
        public const string OutCharSeparator = "ｖ";
        /// <summary>
        /// The Value is ｐｒ, use in server to separate the players' Info from each other.
        /// </summary>
        public const string InCharSeparator = "ｐｒ";
        /// <summary>
        /// This is 50.
        /// 0b110010
        /// </summary>
        public const uint MAXIMUM_RANKS = 0b110010;
        private const string MESSAGE = "LVL_R -- LTW";
        #endregion
        //-------------------------------------------------
        #region online Properties Region
        /// <summary>
        /// 
        /// The Check Code is 0.
        /// </summary>
        public StrongString[] PlayerNames { get; private set; }
        /// <summary>
        /// 
        /// The Check Code is 1.
        /// </summary>
        public uint[] PlayerLevels { get; private set; }
        /// <summary>
        /// 
        /// The Check Code is 2.
        /// </summary>
        public Unit[] PlayerTotalExp { get; private set; }
        /// <summary>
        /// The Player UIDs,
        /// The Check Code is 3.
        /// </summary>
        public UID[] PlayerUIDs { get; private set; }
        #endregion
        //-------------------------------------------------
        #region offline Properties Region
        /// <summary>
        /// Set in the game, not in the server.
        /// </summary>
        public KingdomInfo Kingdom { get; set; }
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
        #region Constructor's Region
        /// <summary>
        /// create a new instance of the <see cref="LevelRankings"/>.
        /// </summary>
        private LevelRankings()
        {
            RankingsMode = RankingsMode.LevelRankings;
        }
        #endregion
        //-------------------------------------------------
        #region online Method's Region
        public async Task<DataBaseDataChangedInfo> UpdateLevelRankings()
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(Provider);
            var _target = LevelRankings_LOC;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(MESSAGE, QString.Parse(GetForServer()), existing.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public StrongString GetForServer()
        {
            StrongString myString = OutCharSeparator;
            for (int i = 0; i < MAXIMUM_RANKS; i++)
            {
                myString +=
                    PlayerNames[i] + InCharSeparator + // 0
                    PlayerLevels[i].ToString() + InCharSeparator + // 1
                    PlayerTotalExp[i].GetForServer() + InCharSeparator + // 2
                    PlayerUIDs[i].GetValue() + InCharSeparator + // 3
                    OutCharSeparator;
            }
            return myString;
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static async Task<DataBaseDataChangedInfo> CreateLevelRankings(KingdomInfo kingdom)
        {
            LevelRankings levelRankings = new LevelRankings()
            {
                Kingdom = kingdom,
                PlayerNames = new StrongString[MAXIMUM_RANKS],
                PlayerLevels = new uint[MAXIMUM_RANKS],
                PlayerTotalExp = new Unit[MAXIMUM_RANKS],
                PlayerUIDs = new UID[MAXIMUM_RANKS],
            };
            for(int i = 0; i < MAXIMUM_RANKS; i++)
            {
                levelRankings.PlayerNames[i] = ThereIsConstants.Path.NotSet;
                levelRankings.PlayerLevels[i] = 0;
                levelRankings.PlayerTotalExp[i] = Unit.GetBasicUnit();
                levelRankings.PlayerUIDs[i] = UID.GenerateNullUID();
            }
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(levelRankings.Provider);
            var _target = LevelRankings_LOC;
            var _req = new DataBaseCreation(MESSAGE, QString.Parse(levelRankings.GetForServer()));
            return await ThereIsServer.Actions.CreateData(_s, _target, _req);
            //---------------------------------------------
        }
        public static async Task<LevelRankings> GetLevelRankings(KingdomInfo kingdom)
        {
            LevelRankings levelRankings = new LevelRankings()
            {
                Kingdom         = kingdom,
                PlayerNames     = new StrongString[MAXIMUM_RANKS],
                PlayerLevels    = new uint[MAXIMUM_RANKS],
                PlayerTotalExp  = new Unit[MAXIMUM_RANKS],
                PlayerUIDs      = new UID[MAXIMUM_RANKS],
            };
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(levelRankings.Provider);
            var _target = LevelRankings_LOC;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                await CreateLevelRankings(kingdom);
                //---------------------------------------------
                existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                //---------------------------------------------
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
            }
            else
            {
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
            }
            StrongString[] myStrings = existing.Decode().Split(OutCharSeparator);
            StrongString[] InStrings;
            for(int i = 0; i < MAXIMUM_RANKS; i++)
            {
                InStrings = myStrings[i].Split(InCharSeparator);
                levelRankings.PlayerNames[i] = InStrings[0];
                levelRankings.PlayerLevels[i] = InStrings[1].ToUInt16();
                levelRankings.PlayerTotalExp[i] = Unit.ConvertToUnit(InStrings[2]);
                levelRankings.PlayerUIDs[i] = UID.GetUID(InStrings[3]);
            }
            return levelRankings;
        }
        public static async Task<bool> DeleteLevelRankings(LTW_Kingdoms _kingdom_)
        {
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(_kingdom_);
            var _target = LevelRankings_LOC;

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
        //-----------------------------------------
    }
}
