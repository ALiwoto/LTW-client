// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Octokit;
using LTW.Security;
using LTW.Constants;

namespace LTW.GameObjects.ServerObjects
{
    public class DataBaseCreation : CreateFileRequest
    {
        //-------------------------------------------------
        #region Constants Region

        #endregion
        //-------------------------------------------------
        #region Contructors Region
        public DataBaseCreation(StrongString theMessage, 
            QString theContext) :
            base(theMessage.GetValue(), theContext.GetString())
        {
            // do nothing here...
        }
        public DataBaseCreation(StrongString theMessage,
            string theContext) : this(theMessage, QString.Parse(theContext.ToStrong()))
        {

        }
        #endregion
        //-------------------------------------------------
    }
}
