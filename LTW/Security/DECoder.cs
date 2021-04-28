// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;

namespace LTW.Security
{
    public sealed class DECoder : DECodingBase
    {
        //-------------------------------------------------
        #region Constant's Region

        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public DECoder()
        {

        }
        ~DECoder()
        {

        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Method's Region
        public override StrongString GetDecodedValue(QString value)
        {
            return TheEncoderValue.GetString(Convert.FromBase64String(value.GetValue()));
        }
        #endregion
        //-------------------------------------------------
    }
}
