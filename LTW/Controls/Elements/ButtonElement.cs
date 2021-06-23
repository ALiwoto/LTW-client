// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using LTW.Security;
using System;
using WotoProvider.Enums;

namespace LTW.Controls.Elements
{
    /// <summary>
    /// The Button provider of the LTW game.
    /// this will provide you a high quality of the button.
    /// </summary>
    public partial class ButtonElement : GraphicElement
    {
        //-------------------------------------------------
        #region Constant's Region
        public const string WhiteSmoke_Border_FileName  = "f_\\m_\\f_010320212340";
        public const string Red_Border_FileName         = "f_\\m_\\f_010320212341";
        public const string GreenYellow_Border_FileName = "f_\\m_\\f_010320212342";
        public const string DarkGreen_Border_FileName   = "f_\\m_\\f_010320212343";
        public const float ME_EFFECT_OFFSET             = 1.06f;
        public const float ME_EFFECT_OFFSHORT           = 0.04f;
        public const int DEFAULT_WIDTH                  = 150;
        public const int DEFAULT_HEIGHT                 = 46;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// WARNING: The Button Elements does NOT have ant childrens!
        /// you should not add any graphic elements to a button element!
        /// if you try to get the value of this property, you will get null,
        /// so be carefull about it!
        /// </summary>
        public override ElementManager Manager
        {
            get
            {
                // the button elements should not have any children, so 
                // the manager should be null.
                return null;
            }
            protected set
            {
                // check if the base manager is null or not.
                if (base.Manager != null)
                {
                    // it means the base manager is not null,
                    // but rule is rule, the manager of the button elements 
                    // SHOULD be null!
                    // don't ever forget it!
                    // and also don't use the value passed-by here.
                    base.Manager?.DisposeAll();
                    base.Manager = null;
                }
            }
        }
        public override StrongString RealName
        {
            get
            {
                if (_flat != null)
                {
                    return _flat.RealName;
                }
                return StrongString.Empty;
            }
            protected set
            {
                ; // do nothing here.
            }
        }
        public override StrongString Name
        {
            get
            {
                if (_flat != null)
                {
                    return _flat.Name;
                }
                return StrongString.Empty;
            }
            protected set
            {
                ; // do nothing here.
            }
        }
        public override Rectangle Rectangle
        {
            get
            {
                if (_flat != null)
                {
                    return _flat.Rectangle;
                }
                return Rectangle.Empty;
            }
            set
            {
                if (_flat != null)
                {
                    _flat.Rectangle = value;
                }
            }
        }
        public override Vector2 Position
        {
            get
            {
                if (_flat != null)
                {
                    return _flat.Position;
                }
                return Vector2.Zero;
            }
            set
            {
                if (_flat != null)
                {
                    _flat.Position = value;
                }
            }
        }
        public override Vector2 RealPosition { get; protected set; }
        public override Color BackGroundColor
        {
            get
            {
                if (_flat != null)
                {
                    return _flat.BackGroundColor;
                }
                return Color.Transparent;
            }
        }
        public virtual ButtonColors BorderColor { get; protected set; }
        public virtual bool UseMouseEnterEffect { get; protected set; }
        public virtual bool InMouseEnterEffect { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region field's Region
        /// <summary>
        /// the flat element (surface) of the button.
        /// use this to draw the surface graphics.
        /// </summary>
        private FlatElement _flat;
        private Rectangle _real_rect;
#nullable enable
        private Rectangle? _eff_rect;
#nullable disable
        #endregion
        //-------------------------------------------------
        #region event field's Region
        internal override event EventHandler LeftClick
        {
            add
            {
                if (_flat != null)
                {
                    _flat.LeftClick += value;
                }
            }
            remove
            {
                if (_flat != null)
                {
                    _flat.LeftClick -= value;
                }
            }
        }
        internal override event EventHandler LeftUp
        {
            add
            {
                if (_flat != null)
                {
                    _flat.LeftUp += value;
                }
            }
            remove
            {
                if (_flat != null)
                {
                    _flat.LeftUp -= value;
                }
            }
        }
        internal override event EventHandler RightDown
        {
            add
            {
                if (_flat != null)
                {
                    _flat.RightDown += value;
                }
            }
            remove
            {
                if (_flat != null)
                {
                    _flat.RightUp -= value;
                }
            }
        }
        internal override event EventHandler RightUp
        {
            add
            {
                if (_flat != null)
                {
                    _flat.RightUp += value;
                }
            }
            remove
            {
                if (_flat != null)
                {
                    _flat.RightUp -= value;
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public ButtonElement(IRes myRes) : base(myRes, true)
        {
            InitializeComponent();
        }
        #endregion
        //-------------------------------------------------
        #region Destructor's Region
        ~ButtonElement()
        {
            if (_flat != null)
            {
                _flat?.Dispose();
                _flat = null;
            }
            if (Manager != null)
            {
                Manager?.DisposeAll();
                Manager = null;
            }
        }
        #endregion
        //-------------------------------------------------
    }
}
