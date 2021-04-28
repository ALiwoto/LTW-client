// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LTW.Security;

namespace LTW.Controls.Elements
{
    /// <summary>
    /// a flat and light element which is good for showing a text.
    /// the size of this element on the memory will be too low,
    /// which is why you can use it anywhere.
    /// also you can use this for graphical effects such as 
    /// outline of the texts.
    /// </summary>
    public partial class FlatElement : GraphicElements
    {
        //-------------------------------------------------
        #region Constant's Region
        public const float TEXT_OFFSET = 0.0f;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public StrongString FixedText { get; protected set; }
        public Vector2 TextLocation { get; protected set; }
        public StringAlignmation Alignmation { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region field's Region
        //private int _text_sfont;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myRes"></param>
        /// <param name="isBarren"></param>
        public FlatElement(IRes myRes, bool isBarren = false) : base(myRes, isBarren)
        {
            InitializeComponent();
        }
        public FlatElement(IRes myRes, ElementMovements movements, bool isBarren = false) :
            base(myRes, isBarren)
        {
            Movements = movements;
            InitializeComponent();
        } 
        #endregion
        //-------------------------------------------------
    }
}
