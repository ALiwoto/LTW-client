// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using System.Collections.Generic;
using WotoProvider.EventHandlers;
using LTW.SandBox;
using LTW.Security;
using LTW.Controls;
using LTW.GameObjects.WMath;
using LTW.Controls.Workers;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Heroes
{
    public sealed class HeroManager
    {
        //-------------------------------------------------
        #region constatnts Region
        public const string FirstNameString = "Hero Manager - ver 1.1.5.0000";
        public const string SERVER_LOC = "_拾うマナージャー";
        public const string InCharSeparator = "ｙOｗ";
        public const string OutCharSeparator = "R＋T";
        private const string MESSAGE = "HERO_MANAGER -- LTW";
        private const int ID_INDEX = 0;
        private const int SI_INDEX = 1;
        private const int WAIT_TIME = 500; // 500 ms
        private const int MAX_TIME_OUT = 20; // 20 * 500 ms
        #endregion
        //-------------------------------------------------
        #region Server Properties Region
        public List<Hero> Heroes { get; private set; }
        public int Length { get => Heroes.Count; }
        #endregion
        //-------------------------------------------------
        #region Offline Properties Region
        public Player ThePlayer { get; }
        public StrongString[] IDs { get; private set; }
        public IHeroSocket[] ServerIndexes { get; private set; }
        public bool IsLoadedCompletely
        {
            get
            {
                if (_load_completes == null)
                {
                    return false;
                }
                for (int i = 0; i < Length; i++)
                {
                    if (!_load_completes[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private bool[] _load_completes;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private HeroManager(Hero[] heroes, Player _player)
        {
            Heroes = new List<Hero>(heroes);
            ThePlayer = _player;
        }
        private HeroManager(StrongString _server_value_)
        {
            StrongString[] myStrings = _server_value_.Split(OutCharSeparator);
            Heroes = new List<Hero>(new Hero[myStrings.Length]);
            StrongString[] currentHeroString;
            Worker[] _w = new Worker[Length];
            _load_completes = new bool[Length];
            for (int i = 0; i < myStrings.Length; i++)
            {
                currentHeroString = myStrings[i].Split(InCharSeparator);
                IDs[i] = currentHeroString[ID_INDEX];
                ServerIndexes[i] = new FlatHeroSocket(currentHeroString[SI_INDEX].ToInt32());
                _w[i] = new Worker(LoadHeroWorker, (uint)i);
                _w[i].Start();
            }
        }
        #endregion
        //-------------------------------------------------
        #region Get Methods Region
        public StrongString GetForServer()
        {
            StrongString myString = OutCharSeparator;
            for (int i = 0; i < Length; i++)
            {
                myString += 
                    Heroes[i].HeroID                    + InCharSeparator + // index : 0
                    Heroes[i].ServerIndex.ToString()    + InCharSeparator;  // index : 1
                myString += OutCharSeparator;
            }
            return myString;
        }
        public Unit GetTotalHeroesPower()
        {
            Unit myUnit = Unit.GetBasicUnit();
            for(int i = 0; i < Length; i++)
            {
                myUnit += Heroes[i].Power;
            }
            return myUnit;
        }
        public bool Exists(StrongString _hero_ID)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Heroes[i].HeroID == _hero_ID)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Exists(Hero _hero)
        {
            return Exists(_hero.HeroID);
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        /// <summary>
        /// Add a new hero to the hero list of this 
        /// hero manager and immediately update the
        /// Heroes in the Server DataBase.
        /// </summary>
        /// <param name="myHero"></param>
        public async Task<DataBaseDataChangedInfo> AddHero(Hero myHero)
        {
            for (int i = 0; i < Length; i++)
            {
                if (Heroes[i].HeroID == myHero.HeroID)
                {
                    // This Player already has the hero
                    // so you should not add this hero to the
                    // array.
                    return null;
                }
            }
            Heroes.Add(myHero);
            await CreateHero(myHero);

            return await UpdateManager();
        }
        public async Task<bool> ReloadAllHeroes()
        {
            if (Heroes != null)
            {
                Heroes.Clear();
                Heroes = null;
            }
            //---------------------------------------------
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerHeroSocket_Server(_p.Socket);
            var _target = _p.UID.GetValue() + SERVER_LOC;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false;
            }
            StrongString[] myStrings = _existing.Decode().Split(OutCharSeparator);
            Heroes = new List<Hero>(new Hero[myStrings.Length]);
            StrongString[] currentHeroString;
            Worker[] _w = new Worker[Length];
            _load_completes = new bool[Length];
            for (int i = 0; i < myStrings.Length; i++)
            {
                currentHeroString = myStrings[i].Split(InCharSeparator);
                IDs[i] = currentHeroString[ID_INDEX];
                ServerIndexes[i] = new FlatHeroSocket(currentHeroString[SI_INDEX].ToInt32());
                _w[i] = new Worker(LoadHeroWorker, (uint)i);
                _w[i].Start();
            }
            // the waiting room,
            // or rather, loop.
            for (int i = 0; i < MAX_TIME_OUT; i++)
            {
                // check if the damn heroes
                // are loaded completely or not.
                if (IsLoadedCompletely)
                {
                    // manager is loaded completely
                    // with kuso mitaina heroes within it,
                    // so return the manager.
                    return true;
                }
                else
                {
                    // wait untile all of the workers
                    // are done with their damn
                    // hero loading....
                    await Task.Delay(WAIT_TIME);
                }
            }
            return true;
        }
        public async Task<DataBaseDataChangedInfo> UpdateHeroInfo(Hero myHero)
        {
            if (Exists(myHero))
            {
                return await myHero.UpdateHero();
            }
            return null;
        }
        private async Task<DataBaseDataChangedInfo> UpdateManager()
        {
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerHeroSocket_Server(_p.Socket);
            var _target = _p.UID.GetValue() + SERVER_LOC;
            var _m = GetBlankManager();
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            var _req = new DataBaseUpdateRequest(MESSAGE, QString.Parse(_m.GetForServer()), _existing.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
        }
        private async Task<DataBaseDataChangedInfo> CreateHero(Hero _h)
        {
            return await Hero.CreatePlayerHero(_h);
        } 
        #endregion
        //-------------------------------------------------
        #region Overrided Region
        public override string ToString()
        {
            return FirstNameString;
        }
        #endregion
        //-------------------------------------------------
        #region LoadHero Region
        private async void LoadHeroWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
        {
            var _index = (int)sender.Index;
            var _p = ThereIsServer.GameObjects.MyProfile;
            Heroes[_index] = await Hero.LoadHero(IDs[_index], _p, ServerIndexes[_index]);
            _load_completes[_index] = true;
        }
        #endregion
        //-------------------------------------------------
        #region Static Methods Region
        public static async Task<HeroManager> GetHeroManager(UID _uid_)
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerHeroSocket_Server(_uid_.TheSocket);
            var _target = _uid_.GetValue() + SERVER_LOC;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.DoesNotExist)
            {
                return null;
            }
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            // parse the manager and start the workers for doing their work.
            var _manager = Parse(_existing.Decode());
            // the waiting room,
            // or rather, loop.
            for (int i = 0 ; i < MAX_TIME_OUT ; i++)
            {
                // check if the manager and it's damn heroes
                // are loaded completely or not.
                if (_manager.IsLoadedCompletely)
                {
                    // manager is loaded completely
                    // with kuso mitaina heroes within it,
                    // so return the manager.
                    return _manager;
                }
                else
                {
                    // wait untile all of the workers
                    // are done with their damn
                    // hero loading....
                    await Task.Delay(WAIT_TIME);
                }
            }
            // don't know what should I do,
            // but return the manager anyway.
            // keep it in mind if you reached this 
            // point of the code,
            // it means there is a problem,
            // but you should not let the players notice it.
            // the manager at this rate is not completely loaded,
            // but it may be loaded since player is playing around ...
            // yes there is possiblity ....
            // there is hope ...
            // but if (only if and by any chance),
            // the heroes didn't load at that time,
            // you should just reload the heroes, while
            // playing around with player.
            // for example you can show yui sandbox 
            // and reload the unloaded heroes.
            return _manager;
        }
        public static async Task<HeroManager> GetHeroManager()
        {
            var _p = ThereIsServer.GameObjects.MyProfile;
            return await GetHeroManager(_p.UID);
        }
        private static HeroManager GetBlankManager()
        {
            var _h = new Hero[0];
            var _p = ThereIsServer.GameObjects.MyProfile;
            return new HeroManager(_h, _p);
        }
        /// <summary>
        /// create a new and blank <see cref="HeroManager"/> and
        /// create the location of the server for it.
        /// the player is <see cref="ThereIsServer.ServerSettings.MyProfile"/>.
        /// </summary>
        /// <returns></returns>
        public static async Task<HeroManager> CreateHeroManager()
        {
            //---------------------------------------------
            var _m = GetBlankManager();
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _target = _p.UID.GetValue() + SERVER_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerHeroSocket_Server(_p.Socket);
            var _req = new DataBaseCreation(MESSAGE, QString.Parse(_m.GetForServer()));
            await ThereIsServer.Actions.CreateData(_s, _target, _req);
            //---------------------------------------------
            return _m;
        }
        public static async Task<bool> DeletePlayerHeroes(UID _uid_)
        {
            HeroManager _h = await GetHeroManager(_uid_);
            if (_h is null)
            {
                return true;
            }
            for (int i = 0; i < _h.Length; i++)
            {
                await _h.Heroes[i].DeleteHero(_uid_);
            }
            //---------------------------------------------
            var _target = _uid_.GetValue() + SERVER_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_PlayerHeroSocket_Server(_uid_.TheSocket);
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
            var _req = new DataBaseDeleteRequest(MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        private static HeroManager Parse(StrongString _server_value_)
        {
            return new HeroManager(_server_value_);
        }
        #endregion
        //-------------------------------------------------
    }
}
