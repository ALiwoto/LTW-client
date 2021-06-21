// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using WotoProvider.Enums;

namespace LTW.Controls
{
	public interface IInputable
	{
		//-------------------------------------------------
        #region Properties Region
        bool IsDisposed { get; }
		bool Focused { get; }
		InputBorders BorderColor { get; }
        #endregion
        //-------------------------------------------------
		#region Methods Region
		void InputEvent(object sender, TextInputEventArgs e);
		void Focus();
		void Focus(bool force);
		void UnFocus();
		#endregion
        //-------------------------------------------------
	}
}