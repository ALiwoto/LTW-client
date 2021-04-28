// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
#if (OLD_LTW)
using System.Windows.Forms;
#endif
using System.Threading.Tasks;
using WotoProvider.EventHandlers;
using LTW.SandBox;
using LTW.Security;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.WMath;
using LTW.Controls.Workers;
using LTW.GameObjects.Troops;
using LTW.GameObjects.Heroes;
using LTW.GameObjects.Guilds;
using LTW.GameObjects.Kingdoms;
using LTW.GameObjects.Chatting;
using LTW.GameObjects.Characters;
using LTW.GameObjects.ServerObjects;
using LTW.GameObjects.GameResources;
using LTW.GameObjects.Players.Avataring;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Players
{
    /// <summary>
    /// Notice:
    /// In the creating part, you should create all the info, such as
    /// <see cref="Troop"/> for <see cref="Player.PlayerTroops"/>,
    /// <see cref="PlayerResources"/> for <see cref="Player.PlayerResources"/>,
    /// <see cref="Village"/> for <see cref="Player.PlayerVillages"/>, etc...<code></code>
    /// but <see cref="Hero"/> for <see cref="Player.PlayerHeroes"/>,
    /// should be created with <see cref="HeroManager.CreateHeroManager()"/>,
    /// right after the Element selecting of Tohsaka.
    /// </summary>
    public partial class Me : Player
    {
        //-----------------------------------------------------------
        #region Constants Region
        /// <summary>
        /// Use this one for object <see cref="Me"/>,
        /// There is another constant like this, 
        /// check: <see cref="PlayerInfo.PI_Server_LOC"/>,
        /// that is for just the PlayerInfo Information.
        /// </summary>
        public const string FileEndName2    = "_これはミです";
        public const string USERNAME_TO_UID = UID.USERNAME_TO_UID;
        public const string ME_MESSAGE      = "ME -- LTW";
        public const int PlayerLvlParamCal1 = 10;
        public const int PlayerLvlParamCal2 = 2;
        public const int PlayerLvlParamCal3 = 4;
        private const int SECURE_DELAY      = 500; // 500 ms
        #endregion
        //-----------------------------------------------------------
        #region Offline Properties Region
        /// <summary>
        /// Notice: Do NOT save it to the server,
        /// and also do NOT load it from the server ME,
        /// you should load it with <see cref="PlayerInfo.PlayerKingdom"/>,
        /// look: <see cref="KingdomInfo.GetKingdomInfo(uint)"/>.
        /// </summary>
        public KingdomInfo KingdomInfo { get; set; }

        public bool HasLogin { get; set; }
        public bool HasLogOut { get; set; }
        public bool HasAccExist { get; set; }
        public bool IsWaitingForSecuredWorking { get; set; }
        public bool IsSecuredMeWorkingOver { get; set; }
        public bool CreatingAcc { get; set; }
        public bool SignInAcc { get; set; }
        public bool LinkStart { get; set; }
        public bool IsOnSecondStepOfLinkStart { get; set; }
        //---------------------------------------
        public StrongString Username { get; set; }
        public StrongString Password { get; set; }
        public StrongString Token { get; set; }
        #endregion
        //-----------------------------------------------------------
        #region Servering Properties Region
        /// <summary>
        /// This one should be loaded from the server. <code></code>
        /// The Code is: 0.
        /// </summary>
        public StorySteps StoryStep { get; private set; } = StorySteps.TheFirstShowingWithBookStory;
        /// <summary>
        /// The Player's Avatar Frame List. <code></code>
        /// The Code is: 1.
        /// </summary>
        public AvatarFrameList AvatarFrameList { get; private set; }
        /// <summary>
        /// The Special Avatars which user allowed to use. <code></code>
        /// The Code is: 2.
        /// </summary>
        public AvatarList AvatarList { get; private set; }
        /// <summary>
        /// The list of the blocked player which are blocked by me. XD <code></code>
        /// The Code is: 3.
        /// </summary>
        public ChatBlockList MyBlockList { get; private set; }
        #endregion
        //-----------------------------------------------------------
        #region Constructor Region
        /// <summary>
        /// preparing the loading data or the creating the player datas:
        /// <see cref="PlayerInfo"/> and <see cref="Player"/> and <see cref="Me"/>.
        /// </summary>
        /// <param name="createAcc"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public Me(in bool createAcc, in StrongString username, in StrongString password)
        {
            // check if the create acc parameters is true or not
            if (createAcc)
            {
                //-------------------------------------
                Username    = username;
                Password    = password;
                CreatingAcc = true;
                SignInAcc   = false;
                //-------------------------------------
                var _checker = new Worker(CheckingForExistingUserWorker);
                _checker.Start();
                ThereIsServer.ServerSettings.TimeWorker ??= new Trigger();
                ThereIsServer.ServerSettings.TimeWorker.Interval    = 1000;
                ThereIsServer.ServerSettings.TimeWorker.Tick        += 
                    ThereIsServer.Actions.TimeWorkerWorksForCreating;
                ThereIsServer.ServerSettings.TimeWorker.Enabled     = true;
                //-------------------------------------
            }
            else
            {
                //-------------------------------------
                Username    = username;
                Password    = password;
                CreatingAcc = false;
                SignInAcc   = true;
                //-------------------------------------
                var _checker = new Worker(CheckingForExistingUserWorker);
                _checker.Start();
                ThereIsServer.ServerSettings.TimeWorker ??= new Trigger();
                ThereIsServer.ServerSettings.TimeWorker.Interval    = 1000;
                ThereIsServer.ServerSettings.TimeWorker.Tick        +=
                    ThereIsServer.Actions.TimeWorkerWorksForSigningIn;
                ThereIsServer.ServerSettings.TimeWorker.Enabled     = true;
                //-------------------------------------
            }
        }
        /// <summary>
        /// Checking the existance of this profile.
        /// For Link Start.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="theToken"></param>
        public Me(in StrongString username, in StrongString theToken, UID _uid)
        {
            //-------------------------------------
            Username                    = username;
            Token                       = theToken;
            UID                         = _uid;
            Socket                      = _uid.TheSocket;
            CreatingAcc                 = false;
            SignInAcc                   = false;
            LinkStart                   = true;
            IsOnSecondStepOfLinkStart   = false;
            //-------------------------------------
            Worker _t = new Worker(Pri_linkStartExistingWorker);
            _t.Start();
            ThereIsServer.ServerSettings.TimeWorker ??= new Trigger();
            ThereIsServer.ServerSettings.TimeWorker.Interval = 1000;
            ThereIsServer.ServerSettings.TimeWorker.Tick +=
                ThereIsServer.Actions.TimeWorkerWorksForPriLinkStart;
            ThereIsServer.ServerSettings.TimeWorker.Enabled = true;
            //-------------------------------------
        }
        #endregion
        //-----------------------------------------------------------
        #region Creating Region
        public async Task<bool> GeneratePlayerUID()
        {
            Socket = PlayerSocket.Generate();
            UID = await UID.GenerateUID(Username, Socket);
            return true;
        }
        public async void CreatePlayerProfile()
        {
            await GeneratePlayerUID();
            //---------------------------------------------
            var createProfileTimer    = new Worker(CreatePlayerInfoWorker);
            createProfileTimer.Start();
            //---------------------------------------------
            var createMeTimer         = new Worker(CreateMeWorker);
            createMeTimer.Start();
            //---------------------------------------------
            var create_Player_Worker = new Worker(Create_Player_Worker);
            create_Player_Worker.Start();
            var createPlayerTroops_Worker = new Worker(CreatePlayerTroopsWorker);
            createPlayerTroops_Worker.Start();
            var createPlayerMagicalTroops_Worker = new Worker(CreatePlayerMagicalTroopsWorker);
            createPlayerMagicalTroops_Worker.Start();
            var createPlayerResources_Worker = new Worker(CreatePlayerResourcesWorker);
            createPlayerResources_Worker.Start();
            var createPlayerHeroes_Worker = new Worker(CreatePlayerHeroesWorker);
            createPlayerHeroes_Worker.Start();
            //---------------------------------------------
        }
        /// <summary>
        /// Checking for Existance, setting the <see cref="CreateProfileSandBox.DoesPlayerExists"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CheckingForExistingUserWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            HasAccExist = await CheckForExistence(Username);
            if (ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return;
            }
            if (!CreatingAcc && SignInAcc)
            {
                await LoadUID();
            }
            AfterExistenceChecked();
        }
        private async void Pri_linkStartExistingWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            if (UID == null || Socket == null)
            {
                HasAccExist = false;
            }
            else
            {
                HasAccExist = await CheckForExistence(UID);
            }
            if (ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return;
            }
            AfterExistenceChecked();
        }
        private void AfterExistenceChecked()
        {
            if (HasAccExist)
            {
#if ME


                if (ThereIsConstants.Forming.TheMainForm.IsShowingSandBox)
                {
                    var _b = ThereIsConstants.Forming.TheMainForm.ShowingSandBox;
                    if (_b is CreateProfileSandBox mySand)
                    {
                        mySand.IsCheckingForExistingEnded = true;
                        mySand.DoesPlayerExists = true;
                    }
                }
#endif
                return;
            }
            else
            {
#if ME

                if (ThereIsConstants.Forming.TheMainForm.IsShowingSandBox)
                {
                    var _b = ThereIsConstants.Forming.TheMainForm.ShowingSandBox;
                    if (_b is CreateProfileSandBox mySand)
                    {
                        mySand.IsCheckingForExistingEnded = true;
                        mySand.DoesPlayerExists = false;
                    }
                }
#endif
                return;
            }
        }
        /// <summary>
        /// load the <see cref="PlayerInfo.UID"/> from the
        /// server, as well as loading the <see cref="PlayerInfo.Socket"/>.
        /// </summary>
        private async Task<bool> LoadUID()
        {
            UID = await UID.LoadUID(Username);
            Socket = UID.TheSocket;
            if (UID == null)
            {
                return false;
            }
            if (Socket == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// create a new location for <see cref="PlayerInfo"/>,
        /// using it's <see cref="PlayerInfo.PlayerInfoGetForServer()"/>,
        /// as well as creating the login location for player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CreatePlayerInfoWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            PlayerName          = Username;                                     // 1
            PlayerLevel         = 0;                                            // 2
            PlayerLVLRanking    = 0;                                            // 3
            PlayerPowerRanking  = 0;                                            // 4
            PlayerGuildName     = NotSetString;                                 // 5
            GuildPosition       = GuildPosition.NotJoined;                      // 6
            LastSeen            = ThereIsConstants.AppSettings.GlobalTiming;    // 7
            PlayerPower         = new Unit(0);                                  // 8
            PlayerIntro         = NotSetString;                                 // 9
            PlayerAvatar        = Avatar.GetDefaultAvatar();                    // 10
            PlayerAvatarFrame   = AvatarFrame.GetDefaultAvatarFrame();          // 11
            PlayerVIPlvl        = 0;                                            // 12
            PlayerCurrentExp    = Unit.GetBasicUnit();                          // 13
            PlayerTotalExp      = Unit.GetBasicUnit();                          // 14
            PlayerCurrentVIPExp = Unit.GetBasicUnit();                          // 15
            ThePlayerElement    = PlayerElement.NotSet;                         // 16
            PlayerKingdom       = LTW_Kingdoms.NotSet;                          // 17
            SocialPosition      = SocialPosition.GetSocialPosition();           // 18
            //-----------------------------------------------
            var _target     = UID.GetValue() + PI_Server_LOC;
            var _s          = ThereIsServer.ServersInfo.ServerManager.Get_PlayerInfo_Server(Socket);
            var _creation   = new DataBaseCreation(MESSAGE, QString.Parse(PlayerInfoGetForServer()));
            await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //------------------------------------------------------
            StrongString myString = StrongString.Empty;
            SecuredMe _me_ = new SecuredMe(ref myString);
            // there is no need for loop checking here.
            // so, just check if the protocol of the securedMe is safe
            // or not
            if (!_me_.IsSecure || !_me_.IsSecuredProtocol)
            {
                // it means something went wrong during the 
                // securedMe process, so you should close the connection
                ThereIsServer.ServerSettings.CloseConnection();
            }
            _target = UID.GetValue() + ThereIsServer.ServersInfo.EndCheckingFileName;
            _s = ThereIsServer.ServersInfo.ServerManager.Get_Login_Server(Socket);
            _creation = new DataBaseCreation(LI_MESSAGE, QString.Parse(myString));
            await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //------------------------------------------------------

            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded1 = true;
            IsWaitingForSecuredWorking = true;

            _me_ = new SecuredMe(true, Username, Password, this);
            
            for(; ; )
            {
                if (!_me_.IsSecure || !_me_.IsSecuredProtocol)
                {
                    ThereIsServer.ServerSettings.CloseConnection();
                }
                else if (_me_.IsOver)
                {
                    _me_ = null;
                    break;
                }
                await Task.Delay(SECURE_DELAY);
            }
            if (_me_ is null)
            {
                return;
            }
        }
        /// <summary>
        /// create a new location for <see cref="Me"/>,
        /// using it's <see cref="MeGetForServer()"/> as the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CreateMeWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            AvatarFrameList = AvatarFrameList.GenerateEmptyList();
            AvatarList      = AvatarList.GenerateEmptyList();
            MyBlockList     = ChatBlockList.GenerateBlankChatBlockList();
            //-----------------------------------------------
            var _target = UID.GetValue() + FileEndName2;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Me_Server(Socket);
            var _creation = new DataBaseCreation(ME_MESSAGE, QString.Parse(MeGetForServer()));
            await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //-----------------------------------------------

            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded2 = true;
            GC.Collect();
        }
        /// <summary>
        /// create a new location for <see cref="Player"/>,
        /// using it's <see cref="Player.PlayerGetForServer()"/> as the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="handler"></param>
        private async void Create_Player_Worker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Player_Server(Socket);
            var _target = UID.GetValue() + EndFileName_Player;
            var _creation = new DataBaseCreation(PLAYER_MESSAGE, QString.Parse(PlayerGetForServer()));
            await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //-----------------------------------------------

            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded3 = true;
            GC.Collect();
        }
        /// <summary>
        /// create a new location for <see cref="Troop"/>,
        /// using it's <see cref="Troop.CreatePlayerTroops(Troop[])"/> as the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="handler"></param>
        private async void CreatePlayerTroopsWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            PlayerTroops = TroopManager.GetTroopManager(Troop.GetBasicTroops());

            //-----------------------------------------------
            await Troop.CreatePlayerTroops(PlayerTroops.Troops);
            //-----------------------------------------------



            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded4 = true;
            GC.Collect();
        }
        /// <summary>
        /// create a new location for <see cref="MagicalTroop"/>,
        /// using it's <see cref="MagicalTroop.CreatePlayerMagicalTroops(MagicalTroop)"/>
        /// as the data creator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="handler"></param>
        private async void CreatePlayerMagicalTroopsWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            PlayerMagicalTroops = MagicalTroop.GetBasicMagicalTroop();
            //-----------------------------------------------
            await MagicalTroop.CreatePlayerMagicalTroops(PlayerMagicalTroops);
            //-----------------------------------------------

            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded5 = true;
            GC.Collect();
        }
        /// <summary>
        /// create a new location for <see cref="PlayerResources"/>,
        /// using it's <see cref="PlayerResources.CreatePlayerResources(PlayerResources)"/> 
        /// as the data creator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="handler"></param>
        private async void CreatePlayerResourcesWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            PlayerResources = PlayerResources.GetBasicPlayerResources();
            //-----------------------------------------------
            await PlayerResources.CreatePlayerResources(PlayerResources);
            //-----------------------------------------------

            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded6 = true;
            GC.Collect();
        }
        /// <summary>
        /// create a new location for <see cref="HeroManager"/>,
        /// using it's <see cref="HeroManager.CreateHeroManager()"/> 
        /// as the data creator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CreatePlayerHeroesWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            PlayerHeroes = await HeroManager.CreateHeroManager();
            //-----------------------------------------------



            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsCreatingEnded7 = true;
            GC.Collect();
        }
        #endregion
        //-----------------------------------------------------------
        #region Login and loading Region
        public async void Login(bool AfterConfirmingSecuredMe = false)
        {
            if (!AfterConfirmingSecuredMe)
            {
                IsWaitingForSecuredWorking = true;
                SecuredMe _me_ = new SecuredMe(Username, Password, true, this);
                for(; ; )
                {
                    if (!_me_.IsSecure || !_me_.IsSecuredProtocol)
                    {
                        ThereIsServer.ServerSettings.CloseConnection();
                    }
                    else if (_me_.IsOver)
                    {
                        _me_ = null;
                        break;
                    }
                    await Task.Delay(SECURE_DELAY);
                }
            }
            else
            {
                //---------------------------------------------
                var loadProfileInfoTimer = new Worker(LoadPlayerInfoWorker);
                loadProfileInfoTimer.Start();
                //---------------------------------------------
                var loadMeTimer = new Worker(LoadMeWorker);
                loadMeTimer.Start();
                //---------------------------------------------
                Worker load_Player_Timer = new Worker(LoadPlayerWorker);
                load_Player_Timer.Start();
                //---------------------------------------------
                Worker tokenObj_Timer = new Worker(TokenObjWorker);
                tokenObj_Timer.Start();
                //---------------------------------------------
            }
        }
        private async void TokenObjWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            StrongString myString = StrongString.Empty;
            SecuredMe _me_ = new SecuredMe(ref myString);
            if (!_me_.IsSecure || !_me_.IsSecuredProtocol)
            {
                ThereIsServer.ServerSettings.CloseConnection();
            }
            //---------------------------------------------
            var _target = UID.GetValue() + ThereIsServer.ServersInfo.EndCheckingFileName;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Login_Server(Socket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(LI_MESSAGE, QString.Parse(myString), existing.Sha);
            await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
            ThereIsServer.ServerSettings.TokenObj = myString;
            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsSignInEnded4 = true;
            GC.Collect();
        }
        /// <summary>
        /// Use this function to load datas which are necessary for <see cref="PlayerInfo"/>,
        /// which are saved at: <see cref="PlayerInfo.PI_Server_LOC"/> at the Server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadPlayerInfoWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //---------------------------------------------
            var targetFile = UID.GetValue() + PI_Server_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerInfo_Server(Socket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, targetFile);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            SetPlayerInfoParams(existing.Decode());

            // Update the LastSeen of the player.
            LastSeen = ThereIsConstants.AppSettings.GlobalTiming;
            await UpdatePlayerInfo(existing);

            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsSignInEnded1 = true;
            GC.Collect();
        }
        /// <summary>
        /// Use it to load the datas for <see cref="Me"/>'s information,
        /// in the <see cref="FileEndName2"/> at the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadMeWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //---------------------------------------------
            var _target = UID.GetValue() + FileEndName2;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Me_Server(Socket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            SetParams(existing.Decode());
            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsSignInEnded2 = true;
            GC.Collect();
        }
        /// <summary>
        /// with this fuctions, you will load the Information for <see cref="Player"/> in
        /// the <see cref="Player.EndFileName_Player"/> at server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadPlayerWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            //-----------------------------------------------
            var _target = UID.GetValue() + EndFileName_Player;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Player_Server(Socket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //-----------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            SetPlayerParams(existing.Decode());
            //((CreateProfileSandBox)ThereIsConstants.Forming.TheMainForm.ShowingSandBox).IsSignInEnded3 = true;
            GC.Collect();
        }
        #endregion
        //-----------------------------------------------------------
        #region Link_Start and LogOut Region
        public async void Link_Start(bool IsAfterSecuredMe = false)
        {
            if (!IsAfterSecuredMe)
            {
                IsWaitingForSecuredWorking = true;
                SecuredMe _me_ = new SecuredMe(Username, Token, this);
                for(; ; )
                {
                    if (!_me_.IsSecure && !_me_.IsSecuredProtocol)
                    {
                        ThereIsServer.ServerSettings.CloseConnection();
                    }
                    else if (_me_.IsOver)
                    {
                        _me_ = null;
                        break;
                    }
                    await Task.Delay(SECURE_DELAY);
                }
            }
            else
            {
                //-------------------------------------
                //Username = username;
                //Token = theToken;
                //CreatingAcc = false;
                //SignInAcc = false;
                //LinkStart = true;
                IsOnSecondStepOfLinkStart = true;
                string test = Username.GetValue();
                //-------------------------------------
                var _checker = new Worker(CheckingForExistingUserWorker);
                _checker.Start();
                ThereIsServer.ServerSettings.TimeWorker ??= new Trigger();
                ThereIsServer.ServerSettings.TimeWorker.Interval = 1000;
                ThereIsServer.ServerSettings.TimeWorker.Tick +=
                    ThereIsServer.Actions.TimeWorkerWorksForPriLinkStart;
                ThereIsServer.ServerSettings.TimeWorker.Enabled = true;
                //-------------------------------------
            }
        }
        //--------------------------
        public async void LogOut(bool AfterChecking = false)
        {
            if (AfterChecking)
            {
                IsWaitingForSecuredWorking = true;
                SecuredMe _me_ = new SecuredMe(Username, this, Token);
                for (; ;)
                {
                    if (!_me_.IsSecure || !_me_.IsSecuredProtocol)
                    {
                        ThereIsServer.ServerSettings.CloseConnection();
                    }
                    else if (_me_.IsOver)
                    {
                        _me_ = null;
                        break;
                    }
                    await Task.Delay(SECURE_DELAY);
                }
            }
            else
            {
                //--------------------------------------------
                var _checker = new Worker(CheckingForExistingUserWorker);
                _checker.Start();
                ThereIsServer.ServerSettings.TimeWorker ??= new Trigger();
                ThereIsServer.ServerSettings.TimeWorker.Interval = 1000;
                ThereIsServer.ServerSettings.TimeWorker.Tick +=
                    ThereIsServer.Actions.TimeWorkerWorksForLogingOut;
                ThereIsServer.ServerSettings.TimeWorker.Enabled = true;
            }
        }
        #endregion
        //-----------------------------------------------------------
        #region Setting Methods Region
        private void SetParams(StrongString serverValue)
        {
            StrongString[] myStrings = serverValue.Split(CharSeparater);
            StoryStep       = (StorySteps)myStrings[0].ToUInt16();        // 0
            AvatarFrameList = AvatarFrameList.Parse(myStrings[1]);  // 1
            AvatarList      = AvatarList.Parse(myStrings[2]);            // 2
            MyBlockList     = ChatBlockList.Parse(myStrings[3]);        // 3
        }
        /// <summary>
        /// Use it Just once, and in the Element Selection with the Tohsaka's 
        /// Dialog.
        /// </summary>
        /// <param name="playerElement"></param>
        public void SetPlayerElement(PlayerElement playerElement)
        {
            ThePlayerElement = playerElement;
        }
        public void SetPlayerKingdom(LTW_Kingdoms playerKingdom)
        {
            PlayerKingdom = playerKingdom;
        }
        public bool SetSocialPosition(PlayerSocialPosition position = PlayerSocialPosition.OrdinaryPlayer)
        {
            SocialPosition = SocialPosition.GetSocialPosition(position);
            return true;
        }
        public bool SetSocialPosition(StrongString value)
        {
            SocialPosition = SocialPosition.GetSocialPosition(value);
            return true;
        }
        public void SetPlayerStoryStep()
        {
            StoryStep = StoryStep++;
        }
        public void SetPlayerStoryStep(StorySteps step)
        {
            StoryStep = step;
        }
        /// <summary>
        /// set the new avatar of the player, 
        /// if the avatar is special and is not in the
        /// avatar list of the player, this method will return
        /// false. if the new avatar value is already 
        /// selevted, will return false.
        /// </summary>
        /// <param name="avatar">
        /// The new Avatar value.
        /// </param>
        /// <returns>
        /// true, if the operation was successful,
        /// otherwise false.
        /// </returns>
        public bool SetPlayerAvatar(Avatar avatar)
        {
            if (avatar == null || PlayerAvatar == avatar)
            {
                return false;
            }
            if (avatar.IsSpecial)
            {
                if (!AvatarList[avatar])
                {
                    return false;
                }
            }
            PlayerAvatar = avatar;
            return true;
        }
        public bool SetPlayerAvatar(StrongString avatarString)
        {
            return SetPlayerAvatar(Avatar.ConvertToAvatar(avatarString));
        }
        public bool AddPlayerAvatar(StrongString avatarString)
        {
            return AddPlayerAvatar(Avatar.ConvertToAvatar(avatarString));
        }
        public bool AddPlayerAvatar(Avatar avatar)
        {
            if (!avatar.IsSpecial)
            {
                return false;
            }
            AvatarList.AddAvatar(avatar);
            return true;
        }
        public bool SetPlayerAvatarFrame(AvatarFrame frame)
        {
            if (!AvatarFrameList[frame])
            {
                return false;
            }
            PlayerAvatarFrame = frame;
            return true;
        }
        public bool SetPlayerAvatarFrame(StrongString frameString)
        {
            return SetPlayerAvatarFrame(AvatarFrame.ParseToAvatarFrame(frameString));
        }
        public bool AddAvatarFrame(AvatarFrame frame)
        {
            AvatarFrameList.AddAvatarFrame(frame);
            return true;
        }
        public bool AddAvatarFrame(StrongString frameString)
        {
            return AddAvatarFrame(AvatarFrame.ParseToAvatarFrame(frameString));
        }
        /// <summary>
        /// Resume total Player Power.
        /// </summary>
        public void ResumePlayerPower()
        {
            Unit thePower   = Unit.GetBasicUnit();
            thePower        += PlayerHeroes.GetTotalHeroesPower();
            thePower        += PlayerTroops.GetTotalTroopsPower();
            PlayerPower     = thePower;
        }
        #endregion
        //-----------------------------------------------------------
        #region Getting Methods Region
        public Unit GetNeededExpForNextLvl()
        {
            int n = PlayerLevel / PlayerLvlParamCal1, m = PlayerLevel / PlayerLvlParamCal2,
                l = PlayerLvlParamCal3 * (PlayerLevel + 1);
            ulong myLong = (ulong)(((int)System.Math.Pow(n, PlayerLvlParamCal3)) + 
                (PlayerLvlParamCal3 * m) + 
                (l * (PlayerLevel + 1)));
            return Unit.ConvertToUnit(myLong);
        }
        /// <summary>
        /// getting a float between 0 and 1 which
        /// indicate the advancing of the player in leveling.
        /// </summary>
        /// <returns></returns>
        public double GetAdvancingExp()
        {
            return ((double)PlayerCurrentExp.ConvertToInt()) / GetNeededExpForNextLvl().ConvertToInt();
        }
        private StrongString MeGetForServer()
        {
            return
                ((ushort)StoryStep).ToString()              + CharSeparater + // 0
                AvatarFrameList.GetForServer().GetValue()   + CharSeparater + // 1
                AvatarList.GetForServer().GetValue()        + CharSeparater + // 2
                MyBlockList.GetForServer().GetValue()       + CharSeparater;  // 3
        }
        #endregion
        //-----------------------------------------------------------
        #region Online Servering Methods Region
        //These Methods are online workings, 
        //so you should 
        public async Task<bool> ReloadMe()
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Me_Server(Socket);
            var _target = UID.GetValue() + FileEndName2;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            SetParams(_existing.Decode());
            return true;
        }
        public async Task<DataBaseDataChangedInfo> UpdateMe()
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Me_Server(Socket);
            var _target = UID.GetValue() + FileEndName2;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(ME_MESSAGE, QString.Parse(MeGetForServer()), _existing.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-----------------------------------------------------------
        #region static Method's Region
        public static async Task<bool> DeleteMe(UID _uid_)
        {
            //---------------------------------------------
            var _target = _uid_.GetValue() + FileEndName2;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Me_Server(_uid_.TheSocket);
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
            var _req = new DataBaseDeleteRequest(ME_MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-----------------------------------------------------------
    }
}
