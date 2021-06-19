// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Threading.Tasks;
using LTW.SandBox;
using LTW.Security;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Chatting;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Kingdoms
{
    public class KingdomInfo
    {
        //-------------------------------------------------
        #region Constant's Region
        public const string KingdomInfo_LOC = "キングダムインフォです";
        /// <summary>
        /// The Value is Gunsou, use it for separating the strings from server.
        /// </summary>
        public const string CharSeparatpr = "軍曹";
        private const string MESSAGE = "Kingdoms -- LTW";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// This is just a custom name which selected by the king of this kingdom.
        /// </summary>
        public StrongString KingdomName { get; set; }
        /// <summary>
        /// The power of the King of this Kingdom.
        /// </summary>
        public Unit KingsPower { get; set; }
        public ushort KingdomLevel { get; set; }
        //-------------------------------------------------
        // These guys should be saved in another files in this kingdom(don't load them in the same file 
        //   as KingdomInfo File, got it?) :
        public KingdomThrone Throne { get; set; }
        public KingdomRankings Rankings { get; set; }
        /// <summary>
        /// This will show you which Server you should use for this kingdom.
        /// DO NOT Use it in <see cref="GetForServer"/>
        /// North   : 1, 
        /// South   : 2,
        /// West    : 3,
        /// East    : 4,
        /// Center  : ? (5).
        /// check here: <see cref="LTW_Kingdoms"/>
        /// </summary>
        public uint Index { get; set; }
        public LTW_Kingdoms Provider { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private KingdomInfo(LTW_Kingdoms provider)
        {
            Provider = provider;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Method's Region
        private void SetParams(StrongString serverValue)
        {
            StrongString[] myStrings = serverValue.Split(CharSeparatpr);
            KingdomName     = myStrings[0];                     // 1
            KingsPower      = Unit.ConvertToUnit(myStrings[1]); // 2
            KingdomLevel    = myStrings[2].ToUInt16();          // 3
        }
        public ChatChannels GetKingdomChannel()
        {
            switch (Provider)
            {
                case LTW_Kingdoms.North:
                    return ChatChannels.K_1_Chat;
                case LTW_Kingdoms.South:
                    return ChatChannels.K_2_Chat;
                case LTW_Kingdoms.West:
                    return ChatChannels.K_3_Chat;
                case LTW_Kingdoms.East:
                    return ChatChannels.K_4_Chat;
                // case LTW_Kingdoms.Center: not completed yet.
                default:
                    throw new Exception();
            }
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static async Task<DataBaseDataChangedInfo> CreateKingdomInfo(LTW_Kingdoms index)
        {
            KingdomInfo kingdomInfo = new KingdomInfo(index)
            {
                Index = (uint)index,
                KingdomName = index.ToString(),
                KingsPower = Unit.GetBasicUnit(),
                KingdomLevel = 0,
            };
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(index);
            var _target = KingdomInfo_LOC;
            var _req = new DataBaseCreation(MESSAGE, QString.Parse(kingdomInfo.GetForServer()));
            return await ThereIsServer.Actions.CreateData(_s, _target, _req);
            //---------------------------------------------
        }
        public StrongString GetForServer()
        {
            string myString = 
                KingdomName.GetValue()                  + CharSeparatpr + // 1
                KingsPower.GetForServer().GetValue()    + CharSeparatpr + // 2
                KingdomLevel.ToString()                 + CharSeparatpr;  // 3
            return myString;
        }
        public static async Task<KingdomInfo> GetKingdomInfo(uint index)
        {
            return await GetKingdomInfo((LTW_Kingdoms)index);
        }
        public static async Task<KingdomInfo> GetKingdomInfo(LTW_Kingdoms _kingdom_)
        {
            KingdomInfo kingdomInfo = new KingdomInfo(_kingdom_)
            {
                Index = (uint)_kingdom_,
            };
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(kingdomInfo.Provider);
            var _target = KingdomInfo_LOC;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            kingdomInfo.SetParams(_existing.Decode());
            kingdomInfo.Rankings = await KingdomRankings.GetKingdomRankings(kingdomInfo);
            kingdomInfo.Throne = await KingdomThrone.GetKingdomThrone(kingdomInfo);
            return kingdomInfo;

        }
        public static async Task<bool> Delete(LTW_Kingdoms _kingdom_)
        {
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Kingdom_Server(_kingdom_);
            var _target = KingdomInfo_LOC;
            bool _del1 = false, _del2, _del3;
            if (await ThereIsServer.Actions.Exists(_s, _target))
            {
                var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                var _req = new DataBaseDeleteRequest(MESSAGE, _existing.Sha.GetStrong());
                _del1 = await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            }
            _del2 = await KingdomRankings.DeleteRankings(_kingdom_);
            _del3 = await KingdomThrone.DeleteThrone(_kingdom_);
            return _del1 && _del2 && _del3;
        }
        #endregion
        //-------------------------------------------------
    }
}
