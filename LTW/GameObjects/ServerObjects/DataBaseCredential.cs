// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Octokit;
using LTW.Security;

namespace LTW.GameObjects.ServerObjects
{
    class DataBaseCredential : Credentials
    {
        //-------------------------------------------------
        #region Constructor's Region
        public DataBaseCredential(QString value) :
            base(value.GetValue())
        {
            // do nothing here, (for now) ...
        }
        #endregion
        //-------------------------------------------------
    }
}
