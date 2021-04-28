// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace WotoProvider.EventHandlers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LoopModeChangedEventArgs : WotoEventArgs
    {
        //-------------------------------------------------
        #region Properties Region
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public bool LoopModeTurnedOn { get; set; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public LoopModeChangedEventArgs(bool loopModeTurnedOn, WotoCreation wotoCreation) : base(wotoCreation)
        {
            LoopModeTurnedOn = loopModeTurnedOn;
        }
        #endregion
        //-------------------------------------------------
    }
}
