// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.Client
{
    /// <summary>
    /// Provide the types of the internal requests
    /// between Universe and GameClient.
    /// </summary>
    internal enum RequestType
    {
        /// <summary>
        /// it means there is no request here
        /// and all requests are done.
        /// </summary>
        None = 0,
        /// <summary>
        /// the request for acitvating the holy planet of woto.
        /// </summary>
        Activate = 1,
    }
}
