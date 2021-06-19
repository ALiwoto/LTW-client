// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Octokit;
using LTW.Security;

namespace LTW.GameObjects.ServerObjects
{
    public class DataBaseDeleteRequest : DeleteFileRequest
    {
        //-------------------------------------------------
        #region Constructors Region
        public DataBaseDeleteRequest(StrongString theMessage, StrongString theSha) :
            base(theMessage.GetValue(), theSha.GetValue())
        {
            // do nothing here ... (for now)
        }
        #endregion
        //-------------------------------------------------
    }
}
