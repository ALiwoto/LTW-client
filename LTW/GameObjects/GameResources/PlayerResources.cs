// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Text;
using System.Threading.Tasks;
using WotoProvider.Enums;
using LTW.SandBox;
using LTW.Security;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.GameResources
{
    public sealed class PlayerResources
    {
        //-------------------------------------------------
        #region Constants Region
        public const string EndFileName = "_リショーセズ";
        public const string InCharSeparator = "歌";
        public const string OutCharSeparator = "花";
        public const string RES_MESSAGE = "R_P -- LTW";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public Resources Resources { get; set; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private PlayerResources(Resources resourcesValue)
        {
            Resources = resourcesValue;
        }
        public Unit this[PlayerResourceType type]
        {
            get
            {
                return Resources[type];
            }
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public StrongString GetForServer()
        {
            StrongString myString =
                Resources.GetForServer() + OutCharSeparator;
            return myString;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        /// <summary>
        /// Parse the string value to Troops in the following order:
        /// <see cref="Saber"/>, <see cref="Archer"/>, <see cref="Lancer"/>, <see cref="Assassin"/>
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static PlayerResources ParseToPlayerResources(StrongString theString)
        {
            StrongString[] myStrings = theString.Split(OutCharSeparator);
            PlayerResources playerResources = 
                new PlayerResources(Resources.ParseToResources(myStrings[0]));
            return playerResources;
        }
        public static PlayerResources GetBasicPlayerResources()
        {
            return new PlayerResources(Resources.GetBasicResources());
        }
        /// <summary>
        /// Create player's troops for the first time.
        /// </summary>
        /// <param name="troops"></param>
        public static async Task<DataBaseDataChangedInfo> CreatePlayerResources(PlayerResources playerResources)
        {
            StrongString myString = playerResources.GetForServer();
            //---------------------------------------------
            var _me = ThereIsServer.GameObjects.MyProfile;
            var _target = _me.UID.GetValue() + EndFileName;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Resources_Server(_me.Socket);
            var _creation = new DataBaseCreation(RES_MESSAGE, QString.Parse(myString));
            return await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //---------------------------------------------
        }
        /// <summary>
        /// Save Player's Resources(Update them to the server.)
        /// </summary>
        /// <param name="troops"></param>
        public static async Task<DataBaseDataChangedInfo> SavePlayerResources(PlayerResources _r, Player _p)
        {
            StrongString myString = _r.GetForServer();
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Resources_Server(_p.Socket);
            var _target = _p.UID.GetValue() + EndFileName;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing == null || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(RES_MESSAGE, QString.Parse(myString), _existing.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        public static async Task<PlayerResources> LoadPlayerResources()
        {
            var _p = ThereIsServer.GameObjects.MyProfile;
            return await LoadPlayerResources(_p);
        }
        public static async Task<PlayerResources> LoadPlayerResources(Player _p)
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Resources_Server(_p.Socket);
            var _target = _p.UID.GetValue() + EndFileName;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing == null || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            return ParseToPlayerResources(_existing.Decode());
        }
        public static async Task<bool> DeletePlayerResources(UID _uid_)
        {
            //---------------------------------------------
            var _target = _uid_.GetValue() + EndFileName;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Resources_Server(_uid_.TheSocket);
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
            var _req = new DataBaseDeleteRequest(RES_MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
    }
}
