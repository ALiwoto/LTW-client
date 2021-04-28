// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace WotoProvider.EventHandlers
{
    public class TickHandlerEventArgs<T> : WotoEventArgs
        where T : class
    {
        /// <summary>
        /// my Father.
        /// </summary>
        public T Father { get; }
        //-------------------------------------------------
        public TickHandlerEventArgs(WotoCreation creation, T fatherSender) :
            base(creation)
        {
            Father = fatherSender;
        }
        //-------------------------------------------------
    }
}
