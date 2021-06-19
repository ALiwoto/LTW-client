// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;
using LTW.GameObjects.ServerObjects;

namespace LTW.GameObjects.Item
{
    public class PlayerItemSocket : ISocket ,IItemSocket
    {
        //-------------------------------------------------
        #region Constant's Region

        #endregion
        //-------------------------------------------------
        #region Peroperties Region
        /// <summary>
        /// General Items Server Index.
        /// The Code is 0.
        /// </summary>
        public int GeneralItemsS_Index { get; }
        /// <summary>
        /// Weapon Server Index.
        /// The Code is 1.
        /// </summary>
        public int WeaponsS_Index { get; }
        /// <summary>
        /// War Extra Item Server Index.
        /// The Code is 2.
        /// </summary>
        public int WarExtraItemS_Index { get; }
        /// <summary>
        /// Currency Server Index.
        /// The Code is 3.
        /// </summary>
        public int CurrencyS_Index { get; }
        /// <summary>
        /// Event Items Server Index.
        /// The Code is 4.
        /// </summary>
        public int EventItemsS_Index { get; }
        /// <summary>
        /// Heroes Shards Server Index.
        /// The Code is 5.
        /// </summary>
        public int HeroesShardsS_Index { get; }
        /// <summary>
        /// Gift Items Server Index.
        /// The Code is 6.
        /// </summary>
        public int GiftItemsS_Index { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region

        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public StrongString GetForServer()
        {
            return null;
        }
        #endregion
        //-------------------------------------------------
    }
}
