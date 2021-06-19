// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Octokit;
using LTW.Security;
using LTW.Constants;

namespace LTW.GameObjects.ServerObjects
{
    public class DataBaseClient : GitHubClient
    {
        //-------------------------------------------------
        #region Constructor's Region
        public DataBaseClient(DataBaseHeader header, QString cridental) :
            base(header)
        {
            Credentials = new DataBaseCredential(cridental);
            SetRequestTimeout(ThereIsConstants.AppSettings.DefaultDataBaseTimeOut);
        }
        #endregion
        //-------------------------------------------------
    }
}
