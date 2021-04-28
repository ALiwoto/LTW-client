// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using WotoProvider.Enums;
using LTW.Security;
using LTW.Controls.Moving;

namespace LTW.Controls.Elements
{
	partial class FlatElement
	{
		//-------------------------------------------------
		#region Initialize Method's Region
		private void InitializeComponent()
		{
			//---------------------------------------------
			//news:
			if (!this.IsBarren)
			{
				this.Manager = new ElementManager(this);
			}
			//---------------------------------------------
			if (Movements != ElementMovements.NoMovements)
			{
				this.MoveManager = new MoveManager(this);
			}
			//---------------------------------------------
			//---------------------------------------------
			//---------------------------------------------
			//---------------------------------------------
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
			// check if the texture and text are null or not
			if (this.Image == null && this.FixedText == null && this.BackGroundImage == null)
			{
				// it means this elements does not have anything to draw,
				// so it should not being the BigFather's sprite batch.
				Manager?.Draw(gameTime, spriteBatch);
				return;
			}
			// begin the spriteBatch tool to drawing the graphic surface.
			spriteBatch.Begin();
			// check if the background image is null or disposed or not.
			if (this.BackGroundImage != null && !this.BackGroundImage.IsDisposed)
			{
				// check if the current background color is transparent of not.
				// if the color is transparent you don't have to draw anything.
				if (this.BackGroundColor != Color.Transparent)
				{
					// it means the current background color of this flat element
					// is not transparent, so you should draw the background color 
					// of this flat element.
					spriteBatch.Draw(this.BackGroundImage, this.Rectangle, this.Tint);
				}
			}
			// check if the Image property of this flat element is null or disposed
			// or not.
			if (this.Image != null && !this.Image.IsDisposed)
			{
				// draw the Image
				spriteBatch.Draw(this.Image, this.ImageRectangle, this.Image.Bounds, this.Tint);
			}
			// check if the fixed text is null or healthy.
			if (!StrongString.IsNullOrEmpty(this.FixedText))
			{
				// check if the Font is null or not.
				if (this.Font != null)
				{
					// draw the fixed-text using spriteBatch tool
					// and specified location.
					spriteBatch.DrawString(this.Font, 
							this.FixedText.GetValue(), 
							this.TextLocation, 
							this.ForeColor);
				}
			}
			// Call the End method of spriteBatch tool.
			spriteBatch.End();
			// call the Draw Method of the manager (it it's not null), 
			// so the children can draw their surface (if any exists).
			this.Manager?.Draw(gameTime, spriteBatch);
		}
		#endregion
		//-------------------------------------------------
		#region overrided Method's Region
		public override void Update(GameTime gameTime)
		{

		}
		public override void SetLabelText()
		{
			base.SetLabelText();
			this.ChangeTextLocation();
		}
		public override void SetLabelText(in StrongString customValue)
		{
			base.SetLabelText(in customValue);
			this.ChangeTextLocation();
		}
		public override void ChangeSize(in float w, in float h)
		{
			base.ChangeSize(in w, in h);
			this.ChangeTextLocation();
			if (this.Image != null)
			{
				this.ImageSizeModeRender();
			}
		}
		public override void ChangeSize(in int w, in int h)
		{
			base.ChangeSize(in w, in h);
			this.ChangeTextLocation();
			if (this.Image != null)
			{
				this.ImageSizeModeRender();
			}
		}
		public override void ChangeLocation(in float x, in float y)
		{
			base.ChangeLocation(in x, in y);
			this.ChangeTextLocation();
		}
		public override void ChangeLocation(in int x, in int y)
		{
			base.ChangeLocation(in x, in y);
			this.ChangeTextLocation();
		}
		public override void ChangeLocation(in Vector2 location)
		{
			base.ChangeLocation(in location);
			this.ChangeTextLocation();
		}
		public override void OwnerLocationUpdate()
		{
			base.OwnerLocationUpdate();
			this.ChangeTextLocation();
		}
		public override void ChangeFont(in SpriteFontBase font)
		{
			if (font == null || this.Font == font)
			{
				return;
			}
			this.Font = font;
		}
		public override void ChangeForeColor(in Color color)
		{
			this.ChangeForeColor(in color, DEFAULT_PEN_W);
		}
		public override void ChangeText(in StrongString text)
		{
			if (this.Text == text)
			{
				return;
			}
			this.Text = text;
			this.FixedText = this.Text.FixMe(this.Font, this.Rectangle.Width);
			this.ChangeTextLocation();
		}
		protected override Texture2D GetBackGroundTexture(Color color)
		{
			if (color != Color.Transparent)
			{
				Texture2D t = new Texture2D(BigFather.GraphicsDevice, PIXEL_BASE, PIXEL_BASE);
				t.SetData<Color>(new Color[] { color });
#if BACKGROUND_TEXTURE

				var g = BigFather.GetGraphics();
				using (var image = new Bitmap(PIXEL_BASE, PIXEL_BASE))
				{
					g = Graphics.FromImage(image);
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.FillRectangle(new SolidBrush(color), BASE_INDEX, BASE_INDEX, PIXEL_BASE, PIXEL_BASE);
					var m = new MemoryStream();
					image.Save(m, ImageFormat.Png);
					t = Texture2D.FromStream(BigFather.GraphicsDevice, m);
					g.Dispose();
					m.Dispose();
				}
#endif
				this.BackGroundImage = t;
				return t;
			}
			return null;
		}


		protected override void UpdateGraphics()
		{
			;
		}
		protected override void ImageSizeModeRender()
		{
			switch (this.ImageSizeMode)
			{
				case ImageSizeMode.Strengthen:
					if (this.ImageRectangle != this.Rectangle)
					{
						this.ImageRectangle = this.Rectangle;
					}
					break;
				case ImageSizeMode.Center:
					if (this.Image != null)
					{
						var x = (int)(this.Width / 2) - (this.Image.Width / 2);
						var y = (int)(this.Height / 2) - (this.Image.Height / 2);
						var w = this.Image.Width;
						var h = this.Image.Height;
						this.ImageRealLocation = new(x, y);
						var loc = this.Rectangle.Location + this.ImageRealLocation;
						var size = new Point(w, h);
						this.ImageRectangle = new(loc, size);
					}
					break;
				default:
					break;
			}
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		public void ChangeAlignmation(StringAlignmation alignmation)
		{
			if (this.Alignmation != alignmation)
			{
				this.Alignmation = alignmation;
			}
			this.ChangeTextLocation();
		}
		public void ChangeForeColor(in Color color, in float w)
		{
			base.ChangeForeColor(color);
		}

		private void ChangeTextLocation()
		{
			if (this.Font == null || this.FixedText == null)
			{
				return;
			}
			var s = this.Font.MeasureString(this.FixedText.GetValue());
			switch (this.Alignmation)
			{
				case StringAlignmation.TopLeft:
					//break;
				case StringAlignmation.TopCenter:
					//break;
				case StringAlignmation.TopRight:
					//break;
				case StringAlignmation.MiddleLeft:
					//break;
				case StringAlignmation.MiddleCenter:
					//break;
				case StringAlignmation.MiddleRight:
					//break;
				case StringAlignmation.BottomLeft:
					//break;
				case StringAlignmation.BottomCenter:
					//break;
				case StringAlignmation.BottomRight:
					var w = this.Position.X + (this.Width / 2) - (s.X / 2);
					var h = this.Position.Y + (this.Height / 2) - (s.Y / 2);
					this.TextLocation = new Vector2(w, h);
					break;
				default:
					break;
			}
		}
		
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		
		#endregion
		//-------------------------------------------------
	}
}
