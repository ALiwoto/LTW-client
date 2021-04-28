// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using LTW.SandBox;
using LTW.Security;
using LTW.GameObjects.Troops;
using LTW.GameObjects.Heroes;
using LTW.GameObjects.ServerObjects;
using LTW.GameObjects.GameResources;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Players
{
    public class Player : PlayerInfo
    {
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// Save it with this order: <see cref="CastleLvl"/> + ..
        /// </summary>
        public const string EndFileName_Player = "_ポレやー";
        public const string EndFileName_Heroes = "_緋色渦";
        public const string EndFileName_Villages = "_ウィれー儒";
        public const string PLAYER_MESSAGE = "PLayer -- LTW";
        public const string LI_MESSAGE = "LI_Player -- LTW";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public HeroManager PlayerHeroes { get; set; }
        public Village[] PlayerVillages { get; set; }
        /// <summary>
        /// The PlayerTroops in the castle.
        /// </summary>
        public TroopManager PlayerTroops { get; set; } 
        public MagicalTroop PlayerMagicalTroops { get; set; }
        //-------------------------------------------------
        /// <summary>
        /// This parameter Should be set in: <see cref="EndFileName_Player"/>
        /// </summary>
        public ushort CastleLvl { get; set; }
        public PlayerResources PlayerResources { get; set; }
        #endregion
        //-------------------------------------------------
        #region Constructor Region
        protected Player()
        {
            ;/*
                * nothing is here,
                * please come back again.
                * thanks, wotoTeam Corp. ALi.w
             */
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        protected StrongString PlayerGetForServer()
        {
            StrongString myString = CharSeparater +
                CastleLvl.ToString() + CharSeparater;
            return myString;
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        protected void SetPlayerParams(StrongString serverValue)
        {
            SetParams(serverValue);
        }
        private void SetParams(StrongString serverValue)
        {
            StrongString[] myStrings = serverValue.Split(CharSeparater);

            CastleLvl = myStrings[0].ToUInt16();
        }
        #endregion
        //-------------------------------------------------
        #region Online Methods Region
        public async Task<bool> ReloadPlayer()
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Player_Server(Socket);
            var _target = UID.GetValue() + EndFileName_Player;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false;
            }
            SetParams(existing.Decode());
            return true;
        }
        public async Task<DataBaseDataChangedInfo> UpdatePlayer()
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Player_Server(Socket);
            var _target = UID.GetValue() + EndFileName_Player;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(PLAYER_MESSAGE, PlayerGetForServer(), existing.Sha.GetStrong());
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static async Task<bool> DeletePlayer(UID _uid_)
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Player_Server(_uid_.TheSocket);
            var _target = _uid_.GetValue() + EndFileName_Player;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                return true;
            }
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false;
            }
            //---------------------------------------------
            var _req = new DataBaseDeleteRequest(PLAYER_MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
    }
}
