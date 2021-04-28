// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using WotoProvider.Enums;
using LTW.Security;
using LTW.Controls.Moving;
using XColor = Microsoft.Xna.Framework.Color;

namespace LTW.Controls.Elements
{
    partial class ButtonElement
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
            //---------------------------------------------
            //Border:
            this.ChangeBorderF(ButtonColors.WhiteSmoke);
            //priorities:
            this.ChangePriority(ElementPriority.Normal);
            //---------------------------------------------
            //Applies:
            this._flat.Apply();
            //shows:
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
            // draw the surface of the button.
            this._flat?.Draw(gameTime, spriteBatch);
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
        protected override Texture2D GetBackGroundTexture(XColor color)
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
        }
        public override void SetLabelText(in StrongString customValue)
        {
            this._flat?.SetLabelText(in customValue);
        }
        public override void ChangeSize(in float w, in float h)
        {
            this._flat?.ChangeSize(in w, in h);
        }
        public override void ChangeSize(in int w, in int h)
        {
            this._flat?.ChangeSize(in w, in h);
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
        }
        public override void OwnerLocationUpdate()
        {
            this.ChangeLocation(this.RealPosition);
        }
        public override void ChangeFont(in SpriteFontBase font)
        {
            this._flat?.ChangeFont(in font);
        }
        public override void ChangeForeColor(in XColor color)
        {
            this._flat.ChangeForeColor(in color);
        }
        public override void ChangeText(in StrongString text)
        {
            this._flat?.ChangeText(in text);
        }
        /// <summary>
        /// WARNING: since the button elements cannot be moveable, 
        /// using this method is useless and the button's movements 
        /// property will always be <see cref="ElementMovements.NoMovements"/>.
        /// </summary>
        /// <param name="movements">
        /// no matter what is this value, the movements of a button elements will
        /// never change!
        /// </param>
        public override void ChangeMovements(in ElementMovements movements)
        {
            // do nothing here!
            // you shall not move the button elements!
            // I won't let this happens!
            // You shall NOT pass!
        }
        /// <summary>
        /// WARNING: since the button elements cannot be moveable, 
        /// using this method is useless and the button's movements 
        /// property will always be <see cref="ElementMovements.NoMovements"/>.
        /// PLEASE do NOT use this method.
        /// </summary>
        /// <param name="movements"></param>
        /// <param name="manager"></param>
        public override void ChangeMovements(ElementMovements movements, in IMoveManager manager)
        {
            // do nothing here!
            // you shall not move the button elements!
            // I won't let this happens!
            // You shall NOT pass!
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        public void ChangeAlignmation(StringAlignmation alignmation)
        {
            _flat?.ChangeAlignmation(alignmation);
        }
        public void ChangeForeColor(XColor color, float w)
        {
            this._flat?.ChangeForeColor(color, w);
        }
        public void ChangeBorder(ButtonColors color)
        {
            if (this.BorderColor != color)
            {
                this.BorderColor = color;
                this.ChangeBorder();
            }
        }
        /// <summary>
        /// change the size of this button to it's default value.
        /// </summary>
        public void ChangeSize()
        {
            this.ChangeSize(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        }
        private void ChangeBorderF(ButtonColors color)
        {
            this.BorderColor = color;
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
                case ButtonColors.WhiteSmoke:
                    this.ChangeForeColor(XColor.WhiteSmoke);
                    break;
                case ButtonColors.Red:
                    this.ChangeForeColor(XColor.Red);
                    break;
                case ButtonColors.GreenYellow:
                    this.ChangeForeColor(XColor.GreenYellow);
                    break;
                case ButtonColors.DarkGreen:
                    this.ChangeForeColor(XColor.DarkGreen);
                    break;
                default:
                    break;
            }
        }
        private StrongString GetContentBorderName()
        {
            switch (this.BorderColor)
            {
                case ButtonColors.WhiteSmoke:
                    return WhiteSmoke_Border_FileName;
                case ButtonColors.Red:
                    return Red_Border_FileName;
                case ButtonColors.GreenYellow:
                    return GreenYellow_Border_FileName;
                case ButtonColors.DarkGreen:
                    return DarkGreen_Border_FileName;
                default:
                    return null;
            }
        }
        #endregion
        //-------------------------------------------------
        #region ordinary Method's Region
        internal void EnableMouseEnterEffect()
        {
            this.UseMouseEnterEffect = true;
        }
        #endregion
        //-------------------------------------------------
        #region event Method's Region
        private void _flat_MouseLeave(object sender, EventArgs e)
        {
            if (this.InMouseEnterEffect)
            {
                this.InMouseEnterEffect = false;
                this.ChangeRectangle(_real_rect);
            }
        }
        private void _flat_MouseEnter(object sender, EventArgs e)
        {
            if (this.UseMouseEnterEffect && !this.InMouseEnterEffect)
            {
                var pos = this.RealPosition.ToPoint();
                var size = this.Rectangle.Size;
                this._real_rect = new(pos, size);
                if (this._eff_rect.HasValue && !this.HasOwner)
                {
                    this.ChangeRectangle(this._eff_rect.Value);
                    this.InMouseEnterEffect = true;
                }
                else
                {
                    var offSet_x = ME_EFFECT_OFFSET * this.Width;
                    var offSet_y = ME_EFFECT_OFFSET * this.Height;
                    this.ChangeSize(offSet_x, offSet_y);
                    var offShort_x = ME_EFFECT_OFFSHORT * this.Width;
                    var offShort_y = ME_EFFECT_OFFSHORT * this.Height;
                    float pos_x, pos_y;
                    if (this.HasOwner && this.Owner != null)
                    {
                        pos_x = this.RealPosition.X - offShort_x;
                        pos_y = this.RealPosition.Y - offShort_y;
                    }
                    else
                    {
                        pos_x = this.Position.X - offShort_x;
                        pos_y = this.Position.Y - offShort_y;
                    }
                    this.ChangeLocation(pos_x, pos_y);
                    this._eff_rect = this._flat.Rectangle;
                    this.InMouseEnterEffect = true;
                }
            }
        }
        #endregion
        //-------------------------------------------------
    }
}
