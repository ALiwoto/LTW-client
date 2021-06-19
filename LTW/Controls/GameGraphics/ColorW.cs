// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Drawing;

namespace LTW.Controls.GameGraphics
{
    public class ColorW
    {
        //-------------------------------------------------
        #region Constant's Region

        #endregion
        //-------------------------------------------------
        #region field's Region
        private Color _color;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public ColorW(Color color)
        {
            _color = color;
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        public void ChangeColor(Color color)
        {
            _color = color;
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public Color GetColor()
        {
            return _color;
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static ColorW ConvertToColorW(Color color)
        {
            return new ColorW(color);
        }
        #endregion
        //-------------------------------------------------
        #region static operator's Region
        public static implicit operator Color(ColorW v)
        {
            return v.GetColor();
        }
        #endregion
        //-------------------------------------------------
    }
}
