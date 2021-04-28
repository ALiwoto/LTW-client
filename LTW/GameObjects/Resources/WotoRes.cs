// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Globalization;
using System.ComponentModel;
using LTW.Security;

namespace LTW.GameObjects.Resources
{
    public sealed class WotoRes : ComponentResourceManager
    {
        //-------------------------------------------------
        #region Constants Region
        public const string WotoResStringName = "WotoRes from :";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public string Name { get; private set; }
        #endregion
        //-------------------------------------------------
        #region Costructor Region
        public WotoRes(Type t) : base(t)
        {
            ;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region

        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public bool StringExists(string name)
        {
            return !(GetString(name) is null);
        }
        public bool ObjectExists(string name)
        {
            return !(GetObject(name) is null);
        }
        public StrongString GetString(StrongString name)
        {
            return base.GetString(name.GetValue());
        }
		public byte[] GetBytes(StrongString name)
		{
			var _r = base.GetObject(name.GetValue());
			if (_r is byte[] _b)
			{
				return _b;
			}
			return null;
		}
        public object GetObject(StrongString name)
        {
            return base.GetObject(name.GetValue());
        }
        #endregion
        //-------------------------------------------------
        #region Overrided Methods Region
        public override string GetString(string strName)
        {
            return base.GetString(strName);
        }
        public override object GetObject(string name)
        {
            return base.GetObject(name);
        }
        public override void ApplyResources(object value, string objectName, CultureInfo culture)
        {
            base.ApplyResources(value, objectName, culture);
        }
        public override string ToString()
        {
            return WotoResStringName + BaseName;
        }
        #endregion
        //-------------------------------------------------
    }
}
