// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using TextCopy;
using WotoProvider.Enums;
using WotoProvider.EventHandlers;
using LTW.Security;
using LTW.Controls.Moving;
using TheMouseInput = Microsoft.Xna.Framework.Input;

namespace LTW.Controls.Elements
{
	partial class InputElement : GraphicElements
	{
		//-------------------------------------------------
		#region Initialize Region
		/// <summary>
		/// Initializer of this <see cref="ButtonElement"/>.
		/// </summary>
		private void InitializeComponent()
		{
			//---------------------------------------------
			//news:
			this._flat = new FlatElement(this, true);
			if (this.Manager != null)
			{
				this.Manager?.DisposeAll();
				this.Manager = null;
			}
			checkLinerTexture(); // supposed to be a `new` kind, so it's here
			//---------------------------------------------
			//names:
			//status:
			//fontAndTextAligns:
			//priorities:
			this.ChangePriority(ElementPriority.Normal);
			//sizes:
			this.ChangeLinerSize();
			//ownering:
			//locations:
			this.ChangeLinerPos();
			//movements:
			//colors:
			this.ChangeBorderF(InputBorders.NoBorder);
			//enableds:
			//texts:
			//images:
			//applyAndShow:
			this._flat.Apply();
			this._flat.Show();
			//---------------------------------------------
			//events:
			this._flat.MouseEnter -= _flat_MouseEnter;
			this._flat.MouseLeave -= _flat_MouseLeave;
			this._flat.MouseEnter += _flat_MouseEnter;
			this._flat.MouseLeave += _flat_MouseLeave;
			//---------------------------------------------
		}
		#endregion
		//-------------------------------------------------
		#region Graphical Method's Region
		public override void Draw(in GameTime gameTime, in SpriteBatch spriteBatch)
		{
			// check if the batch is null or disposed or not
			if (spriteBatch == null || spriteBatch.IsDisposed)
			{
				// do NOT draw yourself if the batch is null or disposed!
				return;
			}
			// check if this element is disposed or applied or visible
			if (this.IsDisposed || !this.IsApplied || !this.Visible)
			{
				// it means this element should not draw itself, so return
				return;
			}
			// draw the surface of the input element.
			this._flat?.Draw(gameTime, spriteBatch);
			if (this.Focused && _linerTexture != null)
			{
				if (_showLiner)
				{
					spriteBatch.Begin();
					spriteBatch.Draw(_linerTexture, _linerRect, Color.White);
					spriteBatch.End();
				}
			}
		}
		#endregion
		//-------------------------------------------------
		#region overrided Method's Region
		protected override Texture2D GetBackGroundTexture(Color color)
		{
#if BUTTON_BACKGROUND
			// w: 75, h: 23.
			// the respect is 0.3066666666666667 .
			DColor back = DColor.FromArgb(170, DColor.Black);
			const float w = 300, h = 92;
			PointF[] unlimitedPointWorks = new[]
			{
				new PointF((w / 10), 0),
				new PointF(w - 1, 0),
				new PointF(w - 1, 2 * (h / 3)),
				new PointF(9 * (w / 10), h - 1),
				new PointF(0, h - 1),
				new PointF(0, 1 * (h / 3)),
			};
			using (var i = new Bitmap(Rectangle.Width, Rectangle.Height))
			{
				Graphics g = Graphics.FromImage(i);
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				g.FillPolygon(new SolidBrush(back), unlimitedPointWorks);
				g.DrawPolygon(new Pen(DColor.WhiteSmoke, 1.25f), unlimitedPointWorks);
				i.Save(@"C:\Users\mrwoto\Programming\Project\LTW\LTW_IMAGES\f_010320212340.bin", 
					System.Drawing.Imaging.ImageFormat.Png);
			}
#endif
			return null;
		}
		protected override void UpdateGraphics()
		{
			;
		}
		protected internal override void OnLeftClick()
		{
			BigFather?.ActivateInputable(this);
			Task.Run((() =>
			{
				this._flat?.OnLeftClick();
			}));
		}
		protected internal override void OnRightClick()
		{
			Task.Run(() =>
			{
				this._flat?.OnRightClick();
			});
		}
		protected internal override void OnMouseEnter()
		{
			Task.Run(() =>
			{
				this._flat?.OnMouseEnter();
			});
		}
		protected internal override void OnMouseLeave()
		{
			Task.Run(() =>
			{
				this._flat?.OnMouseLeave();
			});
		}
		protected internal override void OnLeftDown()
		{
			Task.Run((() =>
			{
				this._flat?.OnLeftDown();
			}));
		}
		protected internal override void OnLeftUp()
		{
			Task.Run((() =>
			{
				this._flat?.OnLeftUp();
			}));
		}
		protected internal override void OnRightDown()
		{
			Task.Run((() =>
			{
				this._flat?.OnRightDown();
			}));
		}
		protected internal override void OnRightUp()
		{
			Task.Run(() =>
			{
				this._flat?.OnRightUp();
			});

		}
		public override void Update(GameTime gameTime)
		{
			// do nothing here (just for now!)
			// by ALi.w
			// in : 08 / 03 / 2021
		}
		public override void SetLabelName(in StrongString constParam)
		{
			this._flat?.SetLabelName(in constParam);
		}
		public override void SetLabelText()
		{
			this._flat?.SetLabelText();
			this.ChangeLinerPos();
		}
		public override void SetLabelText(in StrongString customValue)
		{
			this._flat?.SetLabelText(in customValue);
			this.ChangeLinerPos();
		}
		public override void ChangeSize(in float w, in float h)
		{
			this._flat?.ChangeSize(in w, in h);
			this.ChangeLinerSize();
		}
		public override void ChangeSize(in int w, in int h)
		{
			this._flat?.ChangeSize(in w, in h);
			this.ChangeLinerSize();
		}

		public override void ChangeLocation(in float x, in float y)
		{
			this.RealPosition = new(x, y);
			if (this.HasOwner && this.Owner != null)
			{
				var r_x = this.Owner.Rectangle.Location.X + x;
				var r_y = this.Owner.Rectangle.Location.Y + y;
				this._flat?.ChangeLocation(in r_x, in r_y);
			}
			else
			{
				this._flat?.ChangeLocation(in x, in y);
			}
			this.ChangeLinerPos();
		}
		public override void ChangeLocation(in int x, in int y)
		{
			this.RealPosition = new(x, y);
			if (this.HasOwner && this.Owner != null)
			{
				var r_x = this.Owner.Rectangle.Location.X + x;
				var r_y = this.Owner.Rectangle.Location.Y + y;
				this._flat?.ChangeLocation(in r_x, in r_y);
			}
			else
			{
				this._flat?.ChangeLocation(in x, in y);
			}
			this.ChangeLinerPos();
		}
		public override void ChangeLocation(in Vector2 location)
		{
			this.RealPosition = location;
			if (this.HasOwner && this.Owner != null)
			{
				var loc = this.Owner.Rectangle.Location.ToVector2() + location;
				this._flat?.ChangeLocation(in loc);
			}
			else
			{
				this._flat?.ChangeLocation(in location);
			}
			this.ChangeLinerPos();
		}
		public override void OwnerLocationUpdate()
		{
			this.ChangeLocation(this.RealPosition);
		}
		public override void ChangeFont(in SpriteFontBase font)
		{
			this._flat?.ChangeFont(in font);
		}
		public override void ChangeForeColor(in Color color)
		{
			this._flat.ChangeForeColor(in color);
		}
		public override void ChangeText(in StrongString text)
		{
			this._flat?.ChangeText(in text);
			this.ChangeLinerPos();
		}
		/// <summary>
		/// WARNING: since the input elements cannot be moveable, 
		/// using this method is useless and the input's movements 
		/// property will always be <see cref="ElementMovements.NoMovements"/>.
		/// </summary>
		/// <param name="movements">
		/// no matter what is this value, the movements of a
		/// input element will never change!
		/// </param>
		public override void ChangeMovements(in ElementMovements movements)
		{
			// do nothing here!
			// you shall not move the input elements!
			// I won't let this happens!
			// You shall NOT pass!
		}
		/// <summary>
		/// WARNING: since the input elements cannot be moveable, 
		/// using this method is useless and the input's movements 
		/// property will always be <see cref="ElementMovements.NoMovements"/>.
		/// PLEASE do NOT use this method.
		/// </summary>
		/// <param name="movements"></param>
		/// <param name="manager"></param>
		public override void ChangeMovements(ElementMovements movements, in IMoveManager manager)
		{
			// do nothing here!
			// you shall not move the input elements!
			// I won't let this happens!
			// You shall NOT pass!
		}
		
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public virtual bool IsShortcutKey(InputKeyEventArgs e)
		{
			switch (e.Key)
			{
				case TheMouseInput.Keys.Left:
				case TheMouseInput.Keys.Right:
				case TheMouseInput.Keys.PageDown:
				case TheMouseInput.Keys.PageUp:
				case TheMouseInput.Keys.End:
				case TheMouseInput.Keys.Home:
				case TheMouseInput.Keys.F6:
				case TheMouseInput.Keys.F7:
					return true;
			}

			return false;
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		public void ChangeAlignmation(StringAlignmation alignmation)
		{
			_flat?.ChangeAlignmation(alignmation);
		}
		public void ChangeForeColor(Color color, float w)
		{
			this._flat?.ChangeForeColor(color, w);
		}
		public void ChangeBorder(InputBorders border)
		{
			if (this.BorderColor != border)
			{
				this.BorderColor = border;
				this.ChangeBorder();
			}
		}
		/// <summary>
		/// change the size of this input element to the default 
		/// values of height and width (they are non-zero constants).
		/// </summary>
		public void ChangeSize()
		{
			this.ChangeSize(DEFAULT_WIDTH, DEFAULT_HEIGHT);
		}
		/// <summary>
		/// multiple the current width and height of this input element.
		/// </summary>
		/// <param name="multiple">
		/// the float multiple number.
		/// please do note that if this number is 0 or 1, there will be no
		/// change applied to this element.
		/// </param>
		public void ChangeSize(float multiple)
		{
			if (multiple == BASE_INDEX || multiple == PIXEL_BASE)
			{
				return;
			}
			float w = this.Width != BASE_INDEX ? this.Width :
			DEFAULT_WIDTH;
			float h = this.Height != BASE_INDEX ? this.Height :
			DEFAULT_HEIGHT;
			this.ChangeSize(multiple * w, multiple * h);
			// do NOT update the liner size here again,
			// we have already done that in another overloaded method,
			// so there is no need to do it again.
			// this.ChangeLinerSize();
		}
		private void ChangeBorderF(InputBorders border)
		{
			this.BorderColor = border;
			this.ChangeBorder();
		}
		private void ChangeBorder()
		{
			if (this._flat == null || this._flat.IsDisposed)
			{
				return;
			}
			this._flat.ChangeImageContent(GetContentBorderName());
			switch (this.BorderColor)
			{
				case InputBorders.NoBorder:
					this.ChangeForeColor(Color.WhiteSmoke);
					break;
				case InputBorders.DarkGreen:
					this.ChangeForeColor(Color.DarkGreen);
					break;	
				case InputBorders.Gold:
					this.ChangeForeColor(Color.Gold);
					break;	
				case InputBorders.Goldenrod:
					this.ChangeForeColor(Color.Goldenrod);
					break;	
				case InputBorders.Gray:
					this.ChangeForeColor(Color.Gray);
					break;	
				case InputBorders.Green:
					this.ChangeForeColor(Color.Green);
					break;	
				case InputBorders.Red:
					this.ChangeForeColor(Color.Red);
					break;	
				case InputBorders.SkyBlue:
					this.ChangeForeColor(Color.SkyBlue);
					break;	
				default:
					break;
			}
		}
		private StrongString GetContentBorderName()
		{
			switch (this.BorderColor)
			{
				case InputBorders.NoBorder:
					return Nothing_Border_FileName;
				case InputBorders.DarkGreen:
					return DarkGreen_Border_FileName;
				case InputBorders.Gold:
					return Gold_Border_FileName;
				case InputBorders.Goldenrod:
					return Goldenrod_Border_FileName;
				case InputBorders.Gray:
					return Gray_Border_FileName;
				case InputBorders.Green:
					return Green_Border_FileName;
				case InputBorders.Red:
					return Red_Border_FileName;
				case InputBorders.SkyBlue:
					return SkyBlue_Border_FileName;
				default:
					return null;
			}
		}
		public StringAlignmation GetAlignmation()
		{
			if (this._flat == null)
			{
				return default;
			}
			return this._flat.Alignmation;
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		/// <summary>
		/// focus on this input element so it get input
		/// from the user.
		/// </summary>
		public virtual void Focus()
		{
			var b = this.IsDisposed || !this.IsApplied || 
				!this.Visible || !this.Enabled;
			if (b)
			{
				return;
			}
			if (!this.Focused)
			{
				this.Focused = true;
			}
		}
		/// <summary>
		/// focus on this input element so it get input
		/// from the user.
		/// this method is useful specially when you want to
		/// force this input element to be activated before 
		/// user click on it.
		/// </summary>
		/// <param name="force">
		/// set this to true if you want to force this input
		/// element to focus itself even if it's not
		/// applied, or it's hidden or disabled.
		/// please notice that if this element is disposed,
		/// this method won't do anything.
		/// also if you force it, it will also make itself
		/// activated on main client.
		/// </param>
		public virtual void Focus(bool force)
		{
			var b = (!this.IsDisposed) && ((this.IsApplied && 
				this.Visible && this.Enabled) || force);
			if (!b)
			{
				return;
			}
			if (!this.Focused)
			{
				this.Focused = true;
			}
			if (force)
			{
				BigFather?.ActivateInputable(this, false);
			}
		}
		/// <summary>
		/// focus on this input element so it won't get any inputs
		/// from the user.
		/// </summary>
		public virtual void UnFocus()
		{
			if (this.Focused)
			{
				this.Focused = false;
			}
		}
		internal void EnableMouseEnterEffect()
		{
			this.UseMouseEnterEffect = true;
		}
		private void checkLinerTexture()
		{
			if (Content == null)
			{
				return;
			}
			if (_linerTexture == null || _linerTexture.IsDisposed)
			{
				_linerTexture = Content.Load<Texture2D>(Line_Violet_FileName);
			}
			if (_lineTrigger == null || _lineTrigger.IsDisposed) 
			{
				_lineTrigger = new();
			}
			_lineTrigger?.SetInterval(LINER_INTERVAL);
			_lineTrigger?.AddTick(Liner_Tick);
			_lineTrigger?.SetTag(this);
			_lineTrigger?.Start();
			_linerPosition = default;
			ChangeLinerRect();
		}
		private void ChangeLinerSize()
		{
			_linerSize = new(_linerTexture.Width, 3 * (this.Height / 5));
			ChangeLinerRect();
		}
		private void ChangeLinerPos()
		{
			if (_linerTexture == null || this._flat == null)
			{
				return;
			}
			var w = _linerSize.X;
			var h = _linerSize.Y;
			if (w == default || h == default)
			{
				return;
			}
			var l = this._flat.GetFinalTextLocation();
			var y = this.RealPosition.Y + 
				((this.Height / 2) - (h / 2));
			float x;
			if (this.GetAlignmation() == StringAlignmation.MiddleCenter
				&& l.X == default)
			{
				x = this.RealPosition.X + (this.Width / 2);
			}
			else
			{
				x = l.X == default ? 
				this.RealPosition.X + LINER_EDGE : l.X;
			}
			_linerPosition = new(x, y);
			ChangeLinerRect();
		}
		private void ChangeLinerRect()
		{
			_linerRect = new(_linerPosition.ToPoint(), _linerSize.ToPoint());
		}
		private void Liner_Tick(Trigger sender, TickHandlerEventArgs<Trigger> handler)
		{
			if (sender.Tag is InputElement me)
			{
				if (this == me)
				{
					if (_showLiner)
					{
						_showLiner = false;
					}
					else
					{
						_showLiner = true;
					}
				}
			}
		}
		#endregion
		//-------------------------------------------------
		#region event Method's Region
		public void InputEvent(object sender, TextInputEventArgs e)
		{
			if (this.Font == null)
			{
				return;
			}
			var c = e.Character;
			// newline character is not supported (yet).
			if (c == StrongString.SIGNED_CHAR1)
			{
				return;
			}
			if (this.Text == null)
			{
				this.ChangeText(StrongString.Empty.Append(e.Character, true));
				return;
			}
			else
			{
				var m = this.Text.Append(c).MeasureString(this.Font);
				// check if the characters' width is more than
				// our input element width or not.
				if (m.X >= this.Width - LINER_EDGE)
				{
					if (e.Character != StrongString.UNSIGNED_CHAR1)
					{
						return;
					}
				}
			}
			this.ChangeText(this.Text.Append(e.Character, true));
		}
		
		/// <summary>
		/// event that should be called from BigFather (MainClient)
		/// which tells us a shortcut key on user's keyboard
		/// has been clicked. shortcut keys should be triggered
		/// with clicking `ctrl` key on user's keyboard. like `ctrl + v`
		/// which represents a paste shortcut key.
		/// </summary>
		/// <param name="sender">
		/// the sender of our event (which is `BigFather.Window` or
		/// `GameUniverse.WotoClient` and in fact 
		/// it's `Microsoft.Xna.Framework.SdlGameWindow`). 
		/// </param>
		/// <param name="e">
		/// our event args which contains important information about
		/// shortcut key event.
		/// </param>
		/// <param name="ctrl">
		/// it's `true` if user holds control key on it's keyboard.
		/// </param>
		public void ShortcutEvent(object sender, InputKeyEventArgs e, bool ctrl)
		{
			if (ctrl)
			{
				switch (e.Key)
				{
					case TheMouseInput.Keys.V:
					{
						PasteEvent(e);
						break;
					}
				}
			}
		}
		private void PasteEvent(InputKeyEventArgs e)
		{
			if (this.Font == null)
			{
				return;
			}
			try
			{
				StrongString text = null;
				if (!this.IsMultiLine)
				{
					text = ClipboardService.GetText();
					if (text == null)
					{
						return;
					}
					text = text.FixMe(this.Font, this.Width);
					if (text == null)
					{
						return;
					}
					var texts = text.Split(StrongString.SIGNED_CHAR1.ToString());
					if (texts == null || texts.Length == default)
					{
						return;
					}
					text = texts[default];
				}
				this.ChangeText(this.Text + text);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private void _flat_MouseLeave(object sender, EventArgs e)
		{
			if (this.InMouseEnterEffect)
			{
				try 
				{
					TheMouseInput.Mouse.SetCursor(TheMouseInput.MouseCursor.Arrow);
					this.InMouseEnterEffect = false;
				} 
				catch 
				{
					// can't do anything here,
					// it's really weird to reach here after all.
					// since it's not a necessary feature to change the
					// cursor, we don't need to throw the exception anyway.
				}
			}
		}
		private void _flat_MouseEnter(object sender, EventArgs e)
		{
			if (this.UseMouseEnterEffect && !this.InMouseEnterEffect)
			{
				try 
				{
					TheMouseInput.Mouse.SetCursor(TheMouseInput.MouseCursor.IBeam);
					this.InMouseEnterEffect = true;
				} 
				catch 
				{
					// can't do anything here,
					// it's really weird to reach here after all.
					// since it's not a necessary feature to change the
					// cursor, we don't need to throw the exception anyway.
				}
			}
		}
		#endregion
		//-------------------------------------------------
	}
}
