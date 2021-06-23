// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using WotoProvider.Enums;
using WotoProvider.Interfaces;
using LTW.Client;
using LTW.Security;
using LTW.Constants;
using LTW.Controls.Moving;
using LTW.GameObjects.UGW;
using LTW.GameObjects.Resources;
using XRectangle = Microsoft.Xna.Framework.Rectangle;
using XPoint	 = Microsoft.Xna.Framework.Point;

namespace LTW.Controls.Elements
{
	/// <summary>
	/// Graphics Element.
	/// this class is abstract.
	/// just inherit your element from this class.
	/// </summary>
	public abstract partial class GraphicElement : IRes, IDisposable, IMoveable, ILocation
	{
		//-------------------------------------------------
		#region Constant's Region
		/// <summary>
		/// the pictures' suffix in the woto resources manager.
		/// </summary>
		public const string PIC_RES = "_Pic";
		/// <summary>
		/// represent the unsigned base index, which is zero.
		/// </summary>
		public const int BASE_INDEX = 0;
		/// <summary>
		/// represent the pixel base for drawing a pixel on 
		/// an image.
		/// </summary>
		public const int PIXEL_BASE = 1;
		/// <summary>
		/// the default pen width for drawing a text on an image.
		/// </summary>
		public const float DEFAULT_PEN_W = 10.0f;
		public const int DEVERGE_VALUE = 2;
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		/// <summary>
		/// The Big Father of the <see cref="GraphicElement"/>!
		/// </summary>
		public static GameClient BigFather
		{
			get => ThereIsConstants.Forming.GameClient;
		}
		/// <summary>
		/// The Content Manager of the LTW Game!
		/// </summary>
		public static ContentManager Content
		{
			get
			{
				if (BigFather != null)
				{
					return BigFather.Content;
				}
				return null;
			}
		}
		public static FontManager FontManager
		{
			get
			{
				if (BigFather != null)
				{
					return BigFather.FontManager;
				}
				return null;
			}
		}
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public virtual WotoRes MyRes { get; set; }
		/// <summary>
		/// NOTICE:
		/// this is not my father!!
		/// this is my own manager, contains my children!
		/// please do NOT use it as it is my father!!!
		/// </summary>
		public virtual ElementManager Manager { get; protected set; }
		public virtual GraphicElement Owner { get; protected set; }
		public virtual StrongString RealName { get; protected set; }
		public virtual StrongString Name { get; protected set; }
		public virtual StrongString Text { get; protected set; }
		/// <summary>
		/// The Font.
		/// </summary>
		public virtual SpriteFontBase Font { get; protected set; }
		/// <summary>
		/// the texture which should be draw on the background of the element.
		/// </summary>
		public virtual Texture2D Image { get; protected set; }
		/// <summary>
		/// if the back color is transparent, this texture should be 
		/// null.
		/// </summary>
		protected virtual Texture2D BackGroundImage { get; set; }
		public virtual Color ForeColor { get; set; }
		public virtual Color BackGroundColor { get; set; } = Color.Transparent;
		public virtual Color Tint { get; set; } = Color.White;
		public virtual Vector2 LastPoint { get; set; }
		/// <summary>
		/// the real position of this element on its owner.
		/// </summary>
		public virtual Vector2 RealPosition { get; protected set; }
		/// <summary>
		/// the position of this element on the big father.
		/// </summary>
		public virtual Vector2 Position { get; set; }
		public virtual XPoint ImageRealLocation { get; protected set; }
		public virtual XRectangle Rectangle { get; set; }
		public virtual XRectangle ImageRectangle { get; protected set; }
		public virtual IMoveManager MoveManager { get; protected set; }
		public virtual ILocation FatherLocation { get; protected set; }
		public virtual ElementMovements Movements { get; protected set; }
		public virtual ElementPriority Priority { get; protected set; }
		public virtual ImageSizeMode ImageSizeMode { get; protected set; }
		public float X
		{
			get
			{
				return Position.X;
			}
		}
		public float Y
		{
			get
			{
				return Position.Y;
			}
		}
		public float Width
		{
			get
			{
				return Rectangle.Width;
			}
		}
		public float Height
		{
			get
			{
				return Rectangle.Height;
			}
		}
		public virtual uint CurrentStatus { get; set; }
		public virtual bool IsApplied { get; protected set; }
		public virtual bool IsDisposed { get; protected set; }
		public virtual bool Enabled { get; protected set; }
		public virtual bool HasOwner { get; protected set; }
		public virtual bool HasSandBoxOwner { get; protected set; }
		public virtual bool Visible { get; protected set; }
		public virtual bool IsMouseIn { get; protected set; }
		public virtual bool IsLeftDown { get; protected set; }
		public virtual bool LeftDownOnce { get; protected set; }
		public virtual bool IsRightDown { get; protected set; }
		public virtual bool RightDownOnce { get; protected set; }
		/// <summary>
		/// Gets a value indicating whether the element has been barrened of.
		/// </summary>
		/// <value>
		/// true if the control has been barrened of; otherwise, false.
		/// </value>
		public virtual bool IsBarren { get; protected set; }
		public virtual bool OwnerMover { get; protected set; }
		public virtual bool IsMouseLocked { get; protected set; }
		#endregion
		//-------------------------------------------------
		#region static field's Region
		public static readonly Vector2 DivergeVector = 
			new(DEVERGE_VALUE, DEVERGE_VALUE);
		internal static GraphicElement LockedElement { get; set; }
		#endregion
		//-------------------------------------------------
		#region event field's Region
		/// <summary>
		/// Occurs when the mouse pointer enters the control.
		/// </summary>
		internal virtual event EventHandler MouseEnter;
		/// <summary>
		/// Occurs when the mouse pointer leaves the control.
		/// </summary>
		internal virtual event EventHandler MouseLeave;
		/// <summary>
		/// Occurs when the mouse pointer is moved over the control.
		/// </summary>
		public event EventHandler MouseMove;
		/// <summary>
		/// Occurs when the mouse pointer is over the control and
		/// left mouse button is pressed.
		/// </summary>
		internal virtual event EventHandler LeftDown;
		/// <summary>
		/// Occurs when the mouse pointer is over the control and
		/// left mouse button is released.
		/// </summary>
		internal virtual event EventHandler LeftUp;
		/// <summary>
		/// Occurs when the mouse pointer is over the control and
		/// right mouse button is pressed.
		/// </summary>
		internal virtual event EventHandler RightDown;
		/// <summary>
		/// Occurs when the mouse pointer is over the control and
		/// right mouse button is released.
		/// </summary>
		internal virtual event EventHandler RightUp;
		/// <summary>
		/// Occurs when the control is clicked with the left button of mouse.
		/// </summary>
		internal virtual event EventHandler LeftClick;
		/// <summary>
		/// Occurs when the control is clicked with the right button of mouse.
		/// </summary>
		internal virtual event EventHandler RightClick;
		/// <summary>
		/// Occurs when the control is clicked with nor left button nor right.
		/// for example by a <see cref="GamePad"/>.
		/// </summary>
		internal virtual event EventHandler Click;
		#endregion
		//-------------------------------------------------
		#region Constructor Region
		protected GraphicElement(IRes myRes, bool isBarren = false)
		{
			MyRes = myRes.MyRes;
			CurrentStatus = 1;
			IsBarren = isBarren;
			//Father = _t;
			InitializeComponent();
		}
		/// <summary>
		/// for <see cref="SandBox.SandBoxElement"/>
		/// </summary>
		protected GraphicElement()
		{
			CurrentStatus = 1;
			InitializeComponent();
			; // nothing here, what are you looking for?
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		~GraphicElement()
		{
			if (Click != null)
			{
				Click = null;
			}
		}
		#endregion
		//-------------------------------------------------
	}
}
