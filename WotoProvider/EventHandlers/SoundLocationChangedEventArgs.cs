// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace WotoProvider.EventHandlers
{
    public class SoundLocationChangedEventArgs : WotoEventArgs
    {
        public string NewLocation { get; }
        public SoundLocationChangedEventArgs(string theNewLocation, WotoCreation wotoCreation) :
            base(wotoCreation)
        {
            NewLocation = theNewLocation;
        }
    }
}
