// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;

namespace WotoProvider.EventHandlers
{
    public class WotoEventArgs : EventArgs
    {
        public WotoCreation WotoCreation { get; }

        public WotoEventArgs(WotoCreation wotoCreation)
        {
            WotoCreation = wotoCreation;
        }
    }
}
