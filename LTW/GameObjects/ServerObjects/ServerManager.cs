// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;
using LTW.LoadingService;
using LTW.GameObjects.Item;
using LTW.GameObjects.Heroes;
using LTW.GameObjects.Players;
using LTW.GameObjects.Chatting;
using LTW.GameObjects.Kingdoms;
using WotoProvider.Interfaces;

namespace LTW.GameObjects.ServerObjects
{
    internal sealed class ServerManager : IServerManager
    {
        //-------------------------------------------------
        #region Constant's Region
        public const string Generation_Location_Name        = "じゃねローしぇン";
        public const string MAIN_SERVER_LOCATION            = "ServerConfiguration";
        public const string NO_SHA                          = "NOSHA";
        public const string DEFAULT_MESSAGE                 = "LTW";
        public const int MAX_G_SERVERS                      = 10;
        public const int MAX_UID_SERVERS                    = 10;
        public const int MAX_LOGIN_SERVERS                  = 2;
        public const int MAX_PLAYERINFO_SERVER              = 2;
        public const int MAX_ME_SERVERS                     = 2;
        public const int MAX_PLAYER_SERVERS                 = 2;
        public const int MAX_TROOPS_SERVERS                 = 2;
        public const int MAX_MTROOPS_SERVERS                = 2;
        public const int MAX_RESOURCES_SERVERS              = 2;
        public const int MAX_PLAYER_HERO_SOCKET_SERVERS     = 2;
        public const int MAX_SECURED_ME_SERVERS             = 2;
        public const int MAX_PLAYER_ITEMS_SOCKET_SERVERS    = 2;
        public const int MAX_GENERAL_ITEMS_SERVERS          = 2;
        public const int MAX_WEAPONS_ITEMS_SERVERS          = 2;
        public const int MAX_WAR_EXTRA_ITEMS_SERVERS        = 2;
        public const int MAX_CURRENCY_ITEMS_SERVERS         = 2;
        public const int MAX_EVENTS_ITEMS_SERVERS           = 2;
        public const int MAX_HEROES_SHARDS_ITEMS_SERVERS    = 2;
        public const int MAX_GIFT_ITEMS_SERVERS             = 2;
        public const int MAX_HEROES_INFO_SERVERS            = 2;
        public const int MIN_T                              = 0;
        #endregion
        //-------------------------------------------------
        #region Properties Region

        #endregion
        //-------------------------------------------------
        #region field's Method Region
		private readonly ServerInfo _getUIDByName_server_ = "";
		private readonly ServerInfo _main_server_ = "";
		private readonly ServerInfo _npc_info_server_ = "";
        private readonly ServerInfo[] _channel_servers_ =
        {
			
        };
        private readonly ServerInfo[] _kingdoms_servers =
        {
			
        };
        /// <summary>
        /// use me, to see what is the latest UID last time generated.
        /// then add 1 to it, and update it.
        /// the user's UID will be the that value, don't forget.
        /// after that, you should use that UID in
        /// <see cref="_uid_servers_"/>,
        /// yeah, create a new database location and place the player username and player socket
        /// in that location.
        /// also don't forget to create a new location for player in
        /// <seealso cref="_getUIDByName_server_"/>, which is for
        /// getting player's UID with their username.
        /// </summary>
        private readonly ServerInfo[] _uid_generation_servers_ =
        {
			
        };
        /// <summary>
        /// give me uid, and I will give the player socket.
        /// </summary>
        private readonly ServerInfo[] _uid_servers_ =
        {
			
        };
        private readonly ServerInfo[] _login_servers_ =
        {
			
        };
        private readonly ServerInfo[] _playerInfo_servers_ =
        {
			
        };
        private readonly ServerInfo[] _Me_servers_ =
        {
			
        };
        private readonly ServerInfo[] _player_servers_ =
        {
			
        };
        private readonly ServerInfo[] _troops_servers_ =
        {
			
        };
        private readonly ServerInfo[] _magicalTroops_servers_ =
        {
			
        };
        private readonly ServerInfo[] _resources_servers_ =
        {
			
        };
        private readonly ServerInfo[] _playerHeroSocket_servers_ =
        {
			
        };
        private readonly ServerInfo[] _securedMe_servers_ =
        {
			
        };
        private readonly ServerInfo[] _playerItemsSocket_servers_ =
        {
			
        };
        private readonly ServerInfo[] _generalItems_servers_ =
        {
			
        };
        private readonly ServerInfo[] _weaponsItems_servers_ =
        {
			
        };
        private readonly ServerInfo[] _warExtraItems_servers_ =
        {
			
        };
        private readonly ServerInfo[] _currencyItems_servers_ =
        {
			
        };
        private readonly ServerInfo[] _eventItems_servers_ =
        {
			
        };
        private readonly ServerInfo[] _heroesShardsItem_servers_ =
        {
			
        };
        private readonly ServerInfo[] _giftItems_servers_ =
        {
			
        };
        private readonly ServerInfo[] _heroesInfo_servers_ =
        {
			
        };
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private ServerManager()
        {
            ; // nothing is here ...
        }
        ~ServerManager()
        {

        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        /// <summary>
        /// Main Server for Configuration
        /// </summary>
        /// <returns></returns>
        public IServerProvider<QString, DataBaseClient> Get_Main_Server()
        {
            return _main_server_;
        }
        public IServerProvider<QString, DataBaseClient> Get_Channel_Server(ChatChannels _channel_)
        {
            try
            {
                var _i = (int)_channel_;
                return _channel_servers_[_i];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
            
        }
        public IServerProvider<QString, DataBaseClient> Get_Kingdom_Server(LTW_Kingdoms _kingdom_)
        {
            if (_kingdom_ == LTW_Kingdoms.NotSet)
            {
                return ServerInfo.GetInvalid();
            }
            var _index = (int)_kingdom_ - 1;
            return _kingdoms_servers[_index];
        }
        /// <summary>
        /// use this, to get the server which you should
        /// createdata with user name of the player.
        /// player will load the data with this server by it's username
        /// and gets his uid from this server.
        /// </summary>
        /// <param name="_username_"></param>
        /// <returns></returns>
        public IServerProvider<QString, DataBaseClient> Get_UID_Server(in StrongString _username_)
        {
            if (_username_ != null)
            {
                return _getUIDByName_server_;
            }
            return ServerInfo.GetInvalid();
        }
        /// <summary>
        /// getting an UID Generation server with 
        /// specified index.
        /// the index should be between 0 and 10.
        /// NOTICE: DO NOT add 10 to the index,
        /// I will do it myself in here.
        /// </summary>
        /// <param name="_index_">
        /// index of the generation server.
        /// this value should be greater than or equal to 0,
        /// and smaller than or equal to 10.
        /// </param>
        /// <returns></returns>
        public IServerProvider<QString, DataBaseClient> Get_UID_Generation_Server(in int _index_)
        {
            try
            {
                return _uid_generation_servers_[_index_];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_UID_Server(in int _index_)
        {
            try
            {
                return _uid_servers_[_index_];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_UID_Server(in UID _id_)
        {
            try
            {
                return Get_UID_Server(_id_.Get_UID_ServerIndex());
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_Login_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _login_servers_[_socket_.LoginS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_PlayerInfo_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _playerInfo_servers_[_socket_.PlayerInfoS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_Me_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _Me_servers_[_socket_.MeS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_Player_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _player_servers_[_socket_.PlayerS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_Troops_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _troops_servers_[_socket_.TroopS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_MagicalTroops_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _magicalTroops_servers_[_socket_.MagicalTroopS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_Resources_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _resources_servers_[_socket_.ResourcesS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_PlayerHeroSocket_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _playerHeroSocket_servers_[_socket_.PlayerHeroesS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_SecuredMe_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _securedMe_servers_[_socket_.SecuredMeS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_PlayerItemsSocket_Server(in IPlayerSocket _socket_)
        {
            try
            {
                return _playerItemsSocket_servers_[_socket_.PlayerItemsS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_GeneralItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _generalItems_servers_[_socket_.GeneralItemsS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_WeaponsItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _weaponsItems_servers_[_socket_.WeaponsS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_WarExtraItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _warExtraItems_servers_[_socket_.WarExtraItemS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_CurrencyItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _currencyItems_servers_[_socket_.CurrencyS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_EventItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _eventItems_servers_[_socket_.EventItemsS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_HeroesShardsItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _heroesShardsItem_servers_[_socket_.HeroesShardsS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_GiftItems_Server(in IItemSocket _socket_)
        {
            try
            {
                return _giftItems_servers_[_socket_.GiftItemsS_Index];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_HeroInfo_Server(in IHeroSocket _socket_)
        {
            try
            {
                return _heroesInfo_servers_[_socket_.ServerIndex];
            }
            catch
            {
                return ServerInfo.GetInvalid();
            }
        }
        public IServerProvider<QString, DataBaseClient> Get_NPCInfo_Server()
        {
            return _npc_info_server_;
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region

        #endregion
        //-------------------------------------------------
        #region static Method's Region
        internal static void Generate()
        {
            if (ThereIsServer.ServersInfo.ServerManager == null)
            {
                ThereIsServer.ServersInfo.ServerManager = new ServerManager();
            }
        }
        #endregion
        //-------------------------------------------------
    }
}
