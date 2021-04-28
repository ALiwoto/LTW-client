// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Octokit;
using LTW.Security;

namespace LTW.GameObjects.ServerObjects
{
    /// <summary>
    /// This class represents the parameters to update datas in DataBase.
    /// </summary>
    public class DataBaseUpdateRequest : UpdateFileRequest
    {
        //-------------------------------------------------
        #region Contructors Region
        public DataBaseUpdateRequest(StrongString theMessage, StrongString theContext, StrongString theSha) :
            base(theMessage.GetValue(), 
                QString.Parse(theContext).GetString(), 
                theSha.GetValue())
        {
            // do nothing here...
        }
        public DataBaseUpdateRequest(StrongString theMessage, 
            QString theContext, StrongString theSha) :
            base(theMessage.GetValue(),
                theContext.GetString(),
                theSha.GetValue())
        {
            // do nothing here...
        }
        public DataBaseUpdateRequest(StrongString theMessage,
            QString theContext, QString theSha) :
            base(theMessage.GetValue(),
                theContext.GetString(),
                theSha.GetValue())
        {
            // do nothing here...
        }
        #endregion
        //-------------------------------------------------
    }
}
