// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Text;
using WotoProvider.Interfaces;
using WotoProvider.Interfaces.Security;

namespace LTW.Security
{
    public abstract class DECodingBase : IDECoderProvider<StrongString, QString>, ICryptoTransform
    {
        //-------------------------------------------------
        #region Properties Region
        public Encoding TheEncoderValue { get; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        protected DECodingBase()
        {
            TheEncoderValue = Encoding.Unicode;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Method's Region
        public abstract StrongString GetDecodedValue(QString value);
        #endregion
        //-------------------------------------------------
    }
}
