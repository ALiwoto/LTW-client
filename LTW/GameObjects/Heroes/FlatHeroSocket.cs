// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.GameObjects.Heroes
{
    class FlatHeroSocket : IHeroSocket
    {
        //-------------------------------------------------
        #region Constant's Region

        #endregion
        //-------------------------------------------------
        #region Properties Region
        public int ServerIndex { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public FlatHeroSocket(int _server_index)
        {
            ServerIndex = _server_index;
        }
        #endregion
        //-------------------------------------------------
    }
}
