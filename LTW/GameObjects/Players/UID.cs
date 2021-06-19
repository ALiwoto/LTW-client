// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using WotoProvider.Interfaces;
using LTW.SandBox;
using LTW.Security;
using LTW.LoadingService;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Troops;
using LTW.GameObjects.Heroes;
using LTW.GameObjects.ServerObjects;
using LTW.GameObjects.GameResources;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Players
{
    public sealed class UID
    {
        //-------------------------------------------------
        #region Constant's Region
        public const int MAX_SERVER_INDEX           = ServerManager.MAX_UID_SERVERS;
#pragma warning disable 1584
        /// <summary>
        /// <see cref="GetValue()"/> + <see cref="UID_Lcation_Name"/> is the
        /// target.
		/// use <see cref="ServerManager.Get_UID_Server(in UID)"/> to get the
		/// server.
        /// </summary>
        public const string UID_Lcation_Name        = "_そおうかっと";
        /// <summary>
        /// The value is Shirou
        /// </summary>
        public const string CharSeparator           = "四郎";
        /// <summary>
        /// <see cref="PlayerInfo.PlayerName"/> + <see cref="USERNAME_TO_UID"/>
        /// is the target, which is in the server 
        /// <see cref="ServerManager.Get_UID_Server(in StrongString)"/>.
        /// </summary>
        public const string USERNAME_TO_UID         = "_有ざー名前";
        public const int MIN_SERVER_INDEX           = 0;
        public const int BASE_UID_SERVER_INDEX_SHOW = 10;
        public const int MAX_TRY                    = 50;
        public const int START_INDEX                = 0;
        public const int LENGTH_INDEX               = 2;
        private const string UID_MESSAGE            = "UID_LTW";
        private const string NULL_UID               = "ｎゥぅ";
        private const string FIRST_GENERATION       = "00000001";
        private const string UID_FORMAT             = "0000000000";
        private const string INDEX_FORMAT           = "00";
        private const int UID_VALUE_INDEX           = 0;
        private const int PLAYER_SOCKET_INDEX       = 1;
        private const int USERNAME_INDEX            = 2;
        private const int NULL_INDEX                = -1;
        #endregion
        //-------------------------------------------------
        #region online Properties Region
        /// <summary>
        /// The Player Socket of this UID,
        /// <code>The Code is 0.</code>
        /// </summary>
        public IPlayerSocket TheSocket { get; private set; }
        /// <summary>
        /// The Player UserName,
        /// <code>The Code is 1.</code>
        /// </summary>
        public StrongString PlayerUserName { get; private set; }
        #endregion
        //-------------------------------------------------
        #region offline Properties Region
        public bool IsLoaded { get; private set; }
        public bool IsNullUID { get; }
        #endregion
        //-------------------------------------------------
        #region field's Region
        private static UID _null;
        /// <summary>
        /// the Strong String value of this UID,
        /// <code>the Code is 0.</code>
        /// </summary>
        private StrongString _value;
        /// <summary>
        /// the uid server index.
        /// </summary>
        private readonly int _uid_server_index_;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        /// <summary>
        /// create a new instance of the UID,
        /// which is for creating mode.
        /// in fact, you can create a new UID with this constructor.
        /// </summary>
        /// <param name="_index_"></param>
        /// <param name="_value_"></param>
        /// <param name="_socket_"></param>
        /// <param name="_username_"></param>
        private UID(int _index_, ulong _value_, IPlayerSocket _socket_, IStringProvider<StrongString> _username_)
        {
            _uid_server_index_  = _index_;
            _value              = _value_.ToString();
            TheSocket           = _socket_;
            PlayerUserName      = _username_.GetStrong();
            IsLoaded            = true;
        }
        /// <summary>
        /// create a new instance of the <see cref="UID"/>,
        /// with the server value.
        /// </summary>
        /// <param name="_server_value"></param>
        /// <param name="_socket_"></param>
        private UID(IStringProvider<StrongString> _value_, bool logInMode = false)
        {
            if (logInMode)
            {
                SetParams(_value_.GetStrong());
                _uid_server_index_ = _value.Substring(START_INDEX, LENGTH_INDEX).ToInt32() -
                    BASE_UID_SERVER_INDEX_SHOW;
                IsLoaded = true;
            }
            else
            {
                _uid_server_index_ = _value_.Substring(START_INDEX, LENGTH_INDEX).ToInt32() - 
                    BASE_UID_SERVER_INDEX_SHOW;
                _value = _value_.GetStrong();
                IsLoaded = false;
            }
        }
        /// <summary>
        /// create a new instance of UID, which is a Null UID,
        /// so it means the owner of this UID is not a player.
        /// </summary>
        private UID()
        {
            IsNullUID = true;
        }
        /// <summary>
        /// the destructor od the UID.
        /// </summary>
        ~UID()
        {
            if (_value != null)
            {
                _value.Dispose();
            }
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        /// <summary>
        /// get naked uid server index.
        /// it means it is zero-based.
        /// </summary>
        /// <returns></returns>
        public int Get_UID_ServerIndex()
        {
            if (IsNullUID)
            {
                return NULL_INDEX;
            }
            return _uid_server_index_;
        }
        /// <summary>
        /// get non-zeo-based uid.
        /// </summary>
        /// <returns></returns>
        public StrongString GetForServer()
        {
            if (IsNullUID)
            {
                return NULL_UID;
            }
            StrongString myString =           CharSeparator +
                _value                      + CharSeparator + // 0
                TheSocket.GetForServer()    + CharSeparator + // 1
                PlayerUserName              + CharSeparator;  // 2
            return myString;
        }
        /// <summary>
        /// use this method to showing this uid to the player, or
        /// use it to get the target of the player database locations.
        /// </summary>
        /// <returns></returns>
        public StrongString GetValue()
        {
            if (IsNullUID)
            {
                return NULL_UID;
            }
            return _value;
        }
        /// <summary>
        /// Load the UID, 
        /// load <see cref="TheSocket"/>
        /// and <see cref="PlayerUserName"/>
        /// from the server.
        /// this method will work if and only if 
        /// <see cref="IsLoaded"/> is false.
        /// </summary>
        /// <returns>
        /// if the <see cref="IsLoaded"/> is true,
        /// this method will return you true;
        /// otherwise if the loading operation fails,
        /// it will return you false,
        /// and if the operation was successful, it will return you true.
        /// </returns>
        public async Task<bool> Load()
        {
            if (IsNullUID)
            {
                return false;
            }
            if (IsLoaded)
            {
                return IsLoaded;
            }
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(in _uid_server_index_);
            var _target = GetValue() + UID_Lcation_Name;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false;
            }
            SetParams(existing.Decode());
            IsLoaded = true;
            return IsLoaded;
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        /// <summary>
        /// converting the server value strong string,
        /// which is generated by <see cref="GetForServer()"/>
        /// method of UID class, to it's parameters.
        /// </summary>
        /// <param name="_server_value"></param>
        private void SetParams(StrongString _server_value)
        {
            if (IsNullUID)
            {
                return;
            }
            var myString    = _server_value.Split(CharSeparator);
            _value          = myString[UID_VALUE_INDEX];
            TheSocket       = PlayerSocket.Parse(myString[PLAYER_SOCKET_INDEX]);
            PlayerUserName  = myString[USERNAME_INDEX];
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static async Task<bool> GenerateFirstGeneration()
        {
            IServerProvider<QString, DataBaseClient> _s;
            StrongString _target = ServerManager.Generation_Location_Name;
            StrongString myString;
            DataBaseCreation _req;
            int first_index;
            for (int i = MIN_SERVER_INDEX; i <= MAX_SERVER_INDEX; i++)
            {
                _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Generation_Server(i);
                if (!await ThereIsServer.Actions.Exists(_s, _target))
                {
                    first_index = i + BASE_UID_SERVER_INDEX_SHOW;
                    myString = first_index.ToString(INDEX_FORMAT) + FIRST_GENERATION;
                    _req = new DataBaseCreation(UID_MESSAGE, QString.Parse(myString));
                    await ThereIsServer.Actions.CreateData(_s, _target, _req);
                }
                else
                {
                    continue;
                }
            }
            return true;
        }
        /// <summary>
        /// use this method just for generating a new UID.
        /// </summary>
        /// <param name="_username_"></param>
        /// <param name="_socket_"></param>
        /// <returns></returns>
        public static async Task<UID> GenerateUID(StrongString _username_, IPlayerSocket _socket_)
        {
            int myIndex = Randomic.GetRandom(MIN_SERVER_INDEX, MAX_SERVER_INDEX);
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Generation_Server(myIndex);
            StrongString _target = ServerManager.Generation_Location_Name;
            UID uid;
            ulong _value;
            for (var i = 0; ; i++)
            {
                var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return null;
                }
                _value = existing.Decode().ToUInt64();
                _value++;
                var _req = new DataBaseUpdateRequest(UID_MESSAGE, _value.ToString(UID_FORMAT),
                    existing.Sha.GetStrong());
                var _result = await ThereIsServer.Actions.UpdateDataOnce(_s, _target, _req);
                if (_result.IsDeadCallBack)
                {
                    continue;
                }
                if (ThereIsServer.ServerSettings.HasConnectionClosed || i >= MAX_TRY)
                {
                    return null;
                }
                break;
            }
            //----------------------
            uid = new UID(myIndex, _value, _socket_, _username_);
            _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(uid);
            _target = uid.GetValue() + UID_Lcation_Name;
            var _creation = new DataBaseCreation(UID_MESSAGE, QString.Parse(uid.GetForServer()));
            await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //----------------------
            _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(_username_);
            _target = _username_ + USERNAME_TO_UID;
            _creation = new DataBaseCreation(UID_MESSAGE, QString.Parse(uid.GetValue()));
            await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //----------------------


            return uid;
        }
        /// <summary>
        /// generate a null UID.
        /// </summary>
        /// <returns></returns>
        public static UID GenerateNullUID()
        {
            if (_null is null)
            {
                _null = new UID();
            }
            return _null;
        }
        public static UID[] GenerateNullUIDs(int _count)
        {
            var _value = new UID[_count];
            for (int i = 0; i < _value.Length; i++)
            {
                _value[i] = GenerateNullUID();
            }
            return _value;
        }
        /// <summary>
        /// load UID by username.
        /// use this for signin.
        /// </summary>
        /// <param name="_username_"></param>
        /// <param name="_socket_"></param>
        /// <returns></returns>
        public static async Task<UID> LoadUID(StrongString _username_)
        {
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(_username_);
            var _target = _username_ + USERNAME_TO_UID;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                return GenerateNullUID();
            }
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            var _uid_string = existing.Decode();
            var _index = _uid_string.Substring(START_INDEX, LENGTH_INDEX).ToInt32() - BASE_UID_SERVER_INDEX_SHOW;
            //-------------------------
            _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(_index);
            _target = _uid_string + UID_Lcation_Name;
            existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            return Parse(existing.Decode());
        }
        /// <summary>
        /// use this for link start.
        /// </summary>
        /// <param name="_uid_value"></param>
        /// <returns></returns>
        public static UID GetUID(StrongString _uid_value)
        {
            if (_uid_value == NULL_UID)
            {
                return new UID();
            }
            return new UID(_uid_value);
        }
        public static UID Parse(StrongString _uid_value)
        {
            return new UID(_uid_value, true);
        }
        public static async Task<bool> DeletePlayer(UID _uid_)
        {
            if (_uid_ == null || _uid_.IsNullUID)
            {
                return true;
            }
            if (!await PlayerInfo.DeletePlayerInfo(_uid_))
            {
                return false;
            }
            if (!await Me.DeleteMe(_uid_))
            {
                return false;
            }
            if (!await Troop.DeleteTroops(_uid_))
            {
                return false;
            }
            if (!await MagicalTroop.DeleteMagicalTroops(_uid_))
            {
                return false;
            }
            if (!await PlayerResources.DeletePlayerResources(_uid_))
            {
                return false;
            }
            if (!await HeroManager.DeletePlayerHeroes(_uid_))
            {
                return false;
            }
            if (!await DeleteUID(_uid_))
            {
                return false;
            }
            return true;
        }
        public static async Task<bool> DeletePlayer(StrongString _user_name_)
        {
            return await DeletePlayer(await LoadUID(_user_name_));
        }
        private static async Task<bool> DeleteUID(UID _uid_)
        {
            bool _r1, _r2;
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(_uid_);
            var _target = _uid_.GetValue() + UID_Lcation_Name;
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
                var _req = new DataBaseDeleteRequest(UID_MESSAGE, existing.Sha.GetStrong());
                _r1 = await ThereIsServer.Actions.DeleteData(_s, _target, _req);
                //---------------------------------------------
            }
            //---------------------------------------------
            _s = ThereIsServer.ServersInfo.ServerManager.Get_UID_Server(_uid_.PlayerUserName);
            _target = _uid_.PlayerUserName + USERNAME_TO_UID;
            existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                _r2 = true;
            }
            else
            {
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return false;
                }
                //---------------------------------------------
                var _req = new DataBaseDeleteRequest(UID_MESSAGE, existing.Sha.GetStrong());
                _r2 = await ThereIsServer.Actions.DeleteData(_s, _target, _req);
                //---------------------------------------------
            }
            return _r1 && _r2;
        }
        #endregion
        //-------------------------------------------------
    }
}
