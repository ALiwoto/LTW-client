// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;

namespace LTW.GameObjects.Item
{
    public interface IItemSocket
    {
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// General Items Server Index.
        /// The Code is 0.
        /// </summary>
        int GeneralItemsS_Index { get; }
        /// <summary>
        /// Weapon Server Index.
        /// The Code is 1.
        /// </summary>
        int WeaponsS_Index { get; }
        /// <summary>
        /// War Extra Item Server Index.
        /// The Code is 2.
        /// </summary>
        int WarExtraItemS_Index { get; }
        /// <summary>
        /// Currency Server Index.
        /// The Code is 3.
        /// </summary>
        int CurrencyS_Index { get; }
        /// <summary>
        /// Event Items Server Index.
        /// The Code is 4.
        /// </summary>
        int EventItemsS_Index { get; }
        /// <summary>
        /// Heroes Shards Server Index.
        /// The Code is 5.
        /// </summary>
        int HeroesShardsS_Index { get; }
        /// <summary>
        /// Gift Items Server Index.
        /// The Code is 6.
        /// </summary>
        int GiftItemsS_Index { get; }
        #endregion
        //-------------------------------------------------
        #region Method's Region
        StrongString GetForServer();
        #endregion
        //-------------------------------------------------
    }
}
