// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;

namespace LTW.GameObjects.ServerObjects
{
    /// <summary>
    /// an interface for Sockets.
    /// </summary>
    public interface ISocket
    {
        //-------------------------------------------------
        #region Properties Region

        #endregion
        //-------------------------------------------------
        #region Method's Region
        StrongString GetForServer();
        #endregion
        //-------------------------------------------------
    }
}
