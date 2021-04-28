// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.GameObjects.Resources;

namespace LTW.Controls
{
    /// <summary>
    /// Woto Resources Provider.
    /// </summary>
    public interface IRes
    {
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// Woto ResourceManager.
        /// </summary>
        WotoRes MyRes { get; set; }
        #endregion
        //-------------------------------------------------
    }
}
