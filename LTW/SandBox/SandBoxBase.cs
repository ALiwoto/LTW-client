// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using LTW.Client;
using LTW.Controls.Elements;

namespace LTW.SandBox
{
    public abstract partial class SandBoxBase : SandBoxElement
    {
        //-------------------------------------------------
        #region Constant's Region
        public const int from_the_edge = 10;
        public const int SeparatorLine_Height = 12;
        #endregion
        //-------------------------------------------------
        #region static Property Region
        /// <summary>
        /// in the previos version of the LTW,
        /// we had something with this syntax: 
        /// <code>
        /// public GameControl.PageControl UnderForm { get; private set; }
        /// </code>
        /// so, I just wanted to create something similar in this version,
        /// but the difference is that this UnderForm will always return you
        /// the <see cref="GameClient"/> of the game.
        /// </summary>
        /// <value>
        /// the <see cref="GameClient"/>of the game,
        /// which is also available in <c>ThereIsConstants</c> class.
        /// </value>
        public static GameClient UnderForm
        {
            get => BigFather;
        }
        #endregion
        //-------------------------------------------------
        #region Properties Region
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
                    if (_flat.Position != value)
                    {
                        _flat.Position = value;
                    }
                }
            }
        }
        public override Vector2 RealPosition
        {
            get
            {
                if (_flat != null)
                {
                    return _flat.RealPosition;
                }
                return Vector2.Zero;
            }
            protected set
            {
                if (_flat != null)
                {
                    if (_flat.RealPosition != value)
                    {
                        _flat.ChangeLocation(value);
                    }
                }
            }
        }
        /// <summary>
        /// the priority of this very sandbox.
        /// </summary>
        public virtual SandBoxPriority SandBoxPriority { get; protected set; }
        /// <summary>
        /// a value for checking whether this sandbox was closed
        /// by me or by player.
        /// </summary>
        /// <value><c>true</c> if this sandbox was closed by me;
        /// otherwise <c>false</c>.</value>
        public virtual bool ClosedByMe { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region field's Region
        protected FlatElement _flat;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        /// <summary>
        /// Constructor of The Great Holy <see cref="SandBoxBase"/>.
        /// </summary>
        protected SandBoxBase() : base()
        {
            InitializeComponent();
        }
        #endregion
        //-------------------------------------------------
    }
}
