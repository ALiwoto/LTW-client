// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Threading.Tasks;
using WotoProvider.Interfaces;
using LTW.SandBox;
using LTW.Security;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Guilds;
using LTW.GameObjects.Kingdoms;
using LTW.GameObjects.ServerObjects;
using LTW.GameObjects.Players.Avataring;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Players
{
    public class PlayerInfo
    {
        //-------------------------------------------------
        #region constants Region
        /// <summary>
        /// player info server location.
        /// </summary>
        public const string PI_Server_LOC     = "_これはインフォです";
        public const string CharSeparater   = "い";
        public const string NotSetString    = "ナットセット";
        public const string MESSAGE         = "LTW -- PlayerInfo";
        #endregion
        //-------------------------------------------------
        #region Server Properties Region
        //These properties has the Check number, that means they should 
        //be saved in the server.

        /// <summary>
        /// The Name of Player, it will be the same in the servers files.
        /// It is also the username that user entered at the first time.
        /// The check number is : 1.
        /// </summary>
        public virtual StrongString PlayerName { get; protected set; }
        /// <summary>
        /// the level of the player.
        /// The check number is : 2.
        /// </summary>
        public virtual ushort PlayerLevel { get; protected set; }
        /// <summary>
        /// this parameter should be between 1 and 50.
        /// if that is 0, that means player won't show in the lvl rankings.
        /// The check number is : 3.
        /// </summary>
        public virtual ushort PlayerLVLRanking { get; protected set; }
        /// <summary>
        /// this parameter should be between 1 and 50.
        /// if that is 0, that means player won't show in the lvl rankings.
        /// The check number is : 4.
        /// </summary>
        public virtual ushort PlayerPowerRanking { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 5.
        /// </summary>
        public virtual StrongString PlayerGuildName { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 6.
        /// </summary>
        public virtual GuildPosition GuildPosition { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 7.
        /// </summary>
        public virtual IDateProvider<DateTime, Trigger, StrongString> LastSeen { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 8.
        /// </summary>
        public virtual Unit PlayerPower { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 9.
        /// </summary>
        public virtual StrongString PlayerIntro { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 10.
        /// </summary>
        public virtual Avatar PlayerAvatar { get; protected set; }
        /// <summary>
        /// The Player's Avatar Frame.
        /// The check number is : 11.
        /// </summary>
        public virtual AvatarFrame PlayerAvatarFrame { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 12.
        /// </summary>
        public virtual ushort PlayerVIPlvl { get; protected set; }
        /// <summary>
        /// This is the current exp of this level.
        /// The check number is : 13.
        /// </summary>
        public virtual Unit PlayerCurrentExp { get; protected set; }
        /// <summary>
        /// The total Exp of the Player.
        /// The check number is : 14.
        /// </summary>
        public virtual Unit PlayerTotalExp { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 15.
        /// </summary>
        public virtual Unit PlayerCurrentVIPExp { get; protected set; }
        /// <summary>
        /// 
        /// The check number is : 16.
        /// </summary>
        public virtual PlayerElement ThePlayerElement { get; protected set; }
        /// <summary>
        /// Please Convert it to the int when you are tring 
        /// to updating or creating the playerKingdom
        /// in <see cref="UpdatePlayerInfo()"/> or
        /// <see cref="Me.CreatePlayerInfoWorker(object, EventArgs)"/>.
        /// The check number is : 17.
        /// </summary>
        public virtual LTW_Kingdoms PlayerKingdom { get; protected set; }
        /// <summary>
        /// The check number is : 18.
        /// </summary>
        public virtual SocialPosition SocialPosition { get; protected set; }
        /// <summary>
        /// The User ID.
        /// The check number is 19.
        /// </summary>
        public virtual UID UID { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region Socket Region
        public IPlayerSocket Socket { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region Offline Properties Region
        /// <summary>
        /// Existence of the player in the Server.
        /// it is false by default, so you should run the function <see cref="CheckForExistence()"/>,
        /// after that, this parameter will be true.
        /// </summary>
        public virtual bool PlayerExists { get; protected set; }
        public virtual bool IsEmpty { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region Ordinary Method's Region
        protected StrongString PlayerInfoGetForServer()
        {
            return
                    PlayerName                                      + CharSeparater + // 1
                    PlayerLevel.ToString()                          + CharSeparater + // 2
                    PlayerLVLRanking.ToString()                     + CharSeparater + // 3
                    PlayerPowerRanking.ToString()                   + CharSeparater + // 4
                    PlayerGuildName                                 + CharSeparater + // 5
                    ((uint)GuildPosition).ToString()                + CharSeparater + // 6
                    LastSeen.GetForServer()                         + CharSeparater + // 7
                    PlayerPower.GetForServer()                      + CharSeparater + // 8
                    PlayerIntro                                     + CharSeparater + // 9
                    PlayerAvatar.GetForServer()                     + CharSeparater + // 10
                    PlayerAvatarFrame.GetForServer()                + CharSeparater + // 11
                    PlayerVIPlvl.ToString()                         + CharSeparater + // 12
                    PlayerCurrentExp.GetForServer()                 + CharSeparater + // 13
                    PlayerTotalExp.GetForServer()                   + CharSeparater + // 14
                    PlayerCurrentVIPExp.GetForServer()              + CharSeparater + // 15
                    ((int)ThePlayerElement).ToString()              + CharSeparater + // 16
                    ((int)PlayerKingdom).ToString()                 + CharSeparater + // 17
                    SocialPosition.GetForServer()                   + CharSeparater;  // 18
        }
        private void SetParams(StrongString serverValue)
        {
            StrongString[] myStrings = serverValue.Split(CharSeparater);
            PlayerName          = myStrings[0];                                     // 1
            PlayerLevel         = myStrings[1].ToUInt16();                          // 2
            PlayerLVLRanking    = myStrings[2].ToUInt16();                          // 3
            PlayerPowerRanking  = myStrings[3].ToUInt16();                          // 4
            PlayerGuildName     = myStrings[4];                                     // 5
            GuildPosition       = (GuildPosition)myStrings[5].ToUInt16();           // 6
            LastSeen            = DateProvider.Parse(myStrings[6]);                 // 7
            PlayerPower         = Unit.ConvertToUnit(myStrings[7]);                 // 8
            PlayerIntro         = myStrings[8];                                     // 9
            PlayerAvatar        = Avatar.ConvertToAvatar(myStrings[9]);             // 10
            PlayerAvatarFrame   = AvatarFrame.ParseToAvatarFrame(myStrings[10]);    // 11
            PlayerVIPlvl        = myStrings[11].ToUInt16();                         // 12
            PlayerCurrentExp    = Unit.ConvertToUnit(myStrings[12]);                // 13
            PlayerTotalExp      = Unit.ConvertToUnit(myStrings[13]);                // 14          
            PlayerCurrentVIPExp = Unit.ConvertToUnit(myStrings[14]);                // 15
            ThePlayerElement    = (PlayerElement)myStrings[15].ToUInt16();          // 16
            PlayerKingdom       = (LTW_Kingdoms)myStrings[16].ToInt32();            // 17
            SocialPosition      = SocialPosition.GetSocialPosition(myStrings[17]);  // 18
        }
        protected void SetPlayerInfoParams(StrongString serverValue)
        {
            SetParams(serverValue);
        }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        /// <summary>
        /// Do not use this constructor please.
        /// this is only for <see cref="Me"/> and also
        /// <seealso cref="Player"/> classes.
        /// </summary>
        protected PlayerInfo()
        {
            PlayerExists = false;
        }
        /// <summary>
        /// You can't use this directly,
        /// please use <see cref="GetPlayerInfo(string, bool)"/>
        /// instead.
        /// </summary>
        /// <param name="playerName"></param>
        private PlayerInfo(StrongString playerName)
        {
            PlayerName = playerName;
            if (PlayerName == ThereIsConstants.Path.NotSet)
            {
                IsEmpty             = true;
                PlayerAvatar        = Avatar.GetDefaultAvatar();
                PlayerAvatarFrame   = AvatarFrame.GetDefaultAvatarFrame();
            }
            else
            {
                IsEmpty = false;
            }
        }
        #endregion
        //-------------------------------------------------
        #region Servering Methods Region
        public async Task<bool> ReloadPlayerInfo()
        {
            if (IsEmpty)
            {
                return true;
            }
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerInfo_Server(Socket);
            var _target = UID.GetValue() + PI_Server_LOC;
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
        public async Task<bool> CheckForExistence(StrongString _username)
        {
            //---------------------------------------------
            var _target = _username + UID.USERNAME_TO_UID;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(in _username);
            return await ThereIsServer.Actions.Exists(_s, _target);
            //---------------------------------------------
        }
        public async Task<bool> CheckForExistence(UID _uid)
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(_uid);
            var _target = _uid.GetValue() + UID.UID_Lcation_Name;
            return await ThereIsServer.Actions.Exists(_s, _target); ;
            //---------------------------------------------
        }
        /// <summary>
        /// Updating the Player info to the Server.
        /// All of them will be updated.
        /// </summary>
        public async Task<DataBaseDataChangedInfo> UpdatePlayerInfo()
        {
            try
            {
                var _target = UID.GetValue() + PI_Server_LOC;
                var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerInfo_Server(Socket);
                var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
                var _req = new DataBaseUpdateRequest(MESSAGE, QString.Parse(PlayerInfoGetForServer()), existing.Sha);
                return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<DataBaseDataChangedInfo> UpdatePlayerInfo(DataBaseContent existing)
        {
            try
            {
                //-----------------------------------------
                var _target = UID.GetValue() + PI_Server_LOC;
                var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerInfo_Server(Socket);
                //-----------------------------------------
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
                var _req = new DataBaseUpdateRequest(MESSAGE, QString.Parse(PlayerInfoGetForServer()), existing.Sha);
                return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            }
            catch
            {
                return null;
            }
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static async Task<PlayerInfo> GetPlayerInfo(StrongString name, bool loadFromServer)
        {
            PlayerInfo player = new PlayerInfo(name);
            if (loadFromServer && player.PlayerName != ThereIsConstants.Path.NotSet)
            {
                player.PlayerExists = await player.CheckForExistence(player.PlayerName);
                if (player.PlayerExists)
                {
                    await player.ReloadPlayerInfo();
                }
                else
                {
                    return null;
                }
            }
            return player;
        }
        public static PlayerInfo GetPlayerInfo(StrongString name)
        {
            return new PlayerInfo(name);
        }
        public static async Task<bool> DeletePlayerInfo(UID _uid_)
        {
            bool _r1, _r2;
            //---------------------------------------------
            var _target = _uid_.GetValue() + PI_Server_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerInfo_Server(_uid_.TheSocket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                _r1 = true;
            }
            else
            {
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return false;
                }
                //---------------------------------------------
                var _req_ = new DataBaseDeleteRequest(MESSAGE, existing.Sha.GetStrong());
                _r1 = await ThereIsServer.Actions.DeleteData(_s, _target, _req_);
                //---------------------------------------------
            }


            //---------------------------------------------
            _target = _uid_.GetValue() + ThereIsServer.ServersInfo.EndCheckingFileName;
            _s = ThereIsServer.ServersInfo.ServerManager.Get_Login_Server(_uid_.TheSocket);
            existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                _r2 = true;
                return _r1 && _r2;
            }
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false;
            }
            //---------------------------------------------
            var _req = new DataBaseDeleteRequest(Player.LI_MESSAGE, existing.Sha.GetStrong());
            _r2 = await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------

            return _r1 && _r2;
        }
        #endregion
        //-------------------------------------------------
    }
}
