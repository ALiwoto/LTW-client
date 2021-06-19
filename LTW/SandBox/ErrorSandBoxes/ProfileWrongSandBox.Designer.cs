// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotoProvider.Enums;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.UGW;
using LTW.Controls.Elements;

namespace LTW.SandBox.ErrorSandBoxes
{
    partial class ProfileWrongSandBox
    {
        //-------------------------------------------------
        #region Initialize Method's Region
        private void InitializeComponent()
        {
            //---------------------------------------------
            //news:
            this.TitleElement = new FlatElement(this, true);
            this.BodyElement = new FlatElement(this, true);
            this.BackButton = new ButtonElement(this);
            //---------------------------------------------
            //loading:
            this.LeftTexture = Content.Load<Texture2D>(LEFT_BABYLONIA_ENTRANCE);
            this.RightTexture = Content.Load<Texture2D>(RIGHT_BABYLONIA_ENTRANCE);
            this.LineTexture = Content.Load<Texture2D>(LINE_BABYLONIA_ENTRANCE);
            //names:
            this.TitleElement.SetLabelName(SandBoxLabel1NameInRes);
            this.BodyElement.SetLabelName(SandBoxLabel2NameInRes);
            this.BackButton.SetLabelName(SandBoxButton1NameInRes);
            //status:
            this.TitleElement.SetStatus(1);
            this.BodyElement.SetStatus(1);
            this.BackButton.SetStatus(1);
            //fontAndTextAligns:
            this.TitleElement.ChangeFont(FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 28));
            this.BodyElement.ChangeFont(FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 26));
            this.BackButton.ChangeFont(FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 25));
            this.TitleElement.ChangeAlignmation(StringAlignmation.MiddleCenter);
            this.BodyElement.ChangeAlignmation(StringAlignmation.MiddleCenter);
            this.BackButton.ChangeAlignmation(StringAlignmation.MiddleCenter);
            //priorities:
            this.SandBoxPriority = SandBoxPriority.LowErrorSandBox;
            this.TitleElement.ChangePriority(ElementPriority.Normal);
            this.BodyElement.ChangePriority(ElementPriority.Normal);
            this.BackButton.ChangePriority(ElementPriority.High);
            //sizes:
            this.ChangeSize(2f * (UnderForm.Width / 5), UnderForm.Height / 3);
            this.TitleElement.ChangeSize(Width - from_the_edge,
                        (Height / 3) - (SeparatorLine_Height / 2));
            this.BodyElement.ChangeSize(Width - from_the_edge,
                (1 * (Height / 3)) - (SeparatorLine_Height / 2));
            this.BackButton.ChangeSize();
            //ownering:
            this.TitleElement.SetOwner(this);
            this.BodyElement.SetOwner(this);
            this.BackButton.SetOwner(this);
            //locations:
            this.CenterToScreen();
            this.TitleElement.ChangeLocation(from_the_edge / 2, 0);
            this.BodyElement.ChangeLocation(TitleElement.RealPosition.X, TitleElement.RealPosition.Y +
                TitleElement.Height + SeparatorLine_Height);
            this.BackButton.ChangeLocation((this.Width / 2) -
                (this.BackButton.Width / 2),
                this.BodyElement.RealPosition.Y + this.BodyElement.Height +
                (2 * from_the_edge));
            //rectangles:
            this.CalculateTexturesRect();
            //movements:
            this.TitleElement.ChangeMovements(ElementMovements.NoMovements);
            this.BodyElement.ChangeMovements(ElementMovements.NoMovements);
            //colors:
            this.TitleElement.ChangeForeColor(Color.White);
            this.BodyElement.ChangeForeColor(Color.White);
            this.BackButton.ChangeBorder(ButtonColors.WhiteSmoke);
            //enableds:
            this.TitleElement.EnableOwnerMover();
            this.BodyElement.EnableOwnerMover();
            this.BackButton.EnableMouseEnterEffect();
            //texts:
            this.TitleElement.SetLabelText();
            this.BodyElement.SetLabelText();
            this.BackButton.SetLabelText();
            //images:
            this._flat.ChangeImageSizeMode(ImageSizeMode.Center);
            this._flat.ChangeImageContent(this.MyRes.GetString(SandBoxBackGNameInRes));
            //applyAndShow:
            this.TitleElement.Apply();
            this.TitleElement.Show();
            this.BodyElement.Apply();
            this.BodyElement.Show();
            this.BackButton.Apply();
            this.BackButton.Show();
            //events:
            //---------------------------------------------
            //addRanges:

            //---------------------------------------------
            //finalBlow:
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
            // draw the surface of the sandbox.
            this._flat?.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            if (this.LeftTexture != null && !this.LeftTexture.IsDisposed)
            {
                spriteBatch.Draw(this.LeftTexture, this.LeftTextureRectangle, this.Tint);
            }
            if (this.LineTexture != null && !this.LineTexture.IsDisposed)
            {
                spriteBatch.Draw(this.LineTexture, this.LineTextureRectangle, this.Tint);
            }
            if (this.RightTexture != null && !this.RightTexture.IsDisposed)
            {
                spriteBatch.Draw(this.RightTexture, this.RightTextureRectangle, this.Tint);
            }
            spriteBatch.End();
            // draw the manager
            this.Manager?.Draw(gameTime, spriteBatch);
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
        public override void ChangeLocation(in float x, in float y)
        {
            base.ChangeLocation(in x, in y);
            this.CalculateTexturesRect(false);
            this.Manager?.UpdateLocations();
        }
        public override void ChangeLocation(in int x, in int y)
        {
            base.ChangeLocation(in x, in y);
            this.CalculateTexturesRect(false);
            this.Manager?.UpdateLocations();
        }
        public override void ChangeLocation(in Vector2 location)
        {
            base.ChangeLocation(in location);
            this.CalculateTexturesRect(false);
            this.Manager?.UpdateLocations();
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region

        #endregion
        //-------------------------------------------------
        #region Set Method's Region

        #endregion
        //-------------------------------------------------
        #region ordinary Method's Region
        /// <summary>
        /// calculate the rectangles of the 
        /// <see cref="LeftTexture"/>, 
        /// <see cref="RightTexture"/>,
        /// <see cref="LineTexture"/> for their drawing.
        /// </summary>
        /// <param name="first">
        /// this value should be <c>true</c> if you are calling this mathod
        /// from <see cref="InitializeComponent()"/>;
        /// otherwise it should be false.
        /// </param>
        private void CalculateTexturesRect(bool first = true)
        {
            if (first)
            {
                const int start_point = from_the_edge * 2;
                const int off_set = (2 * from_the_edge / 5) + (4 * from_the_edge);
                int x, y, w, h;
                int c_y = (int)(Height / 4) - (SeparatorLine_Height / 2);
                int max1 = MathHelper.Max(this.LeftTexture.Height, this.RightTexture.Height);
                max1 = MathHelper.Max(max1, this.LineTexture.Height);
                int find_c = c_y + (max1 / 2);
                //---------------------------------------------
                w = this.LeftTexture.Width / 2;
                h = this.LeftTexture.Height / 2;
                x = start_point;
                y = find_c - (h / 2);
                this.LeftTextureRealLocation = new(x, y);
                this.LeftTextureRectangle = new(x, y, w, h);
                //---------------------------------------------
                w = (int)(this.Width - off_set) - (2 * w);
                h = this.LineTexture.Height / 2;
                x = x + this.LeftTextureRectangle.Width + (from_the_edge / 5);
                y = find_c - (h / 2);
                this.LineTextureRealLocation = new(x, y);
                this.LineTextureRectangle = new(x, y, w, h);
                //---------------------------------------------
                w = this.RightTexture.Width / 2;
                h = this.RightTexture.Height / 2;
                x = x + this.LineTextureRectangle.Width + (from_the_edge / 5);
                y = find_c - (h / 2);
                this.RightTextureRealLocation = new(x, y);
                this.RightTextureRectangle = new(x, y, w, h);
                //---------------------------------------------
                finalBlow();
            }
            else
            {
                anotherBlow();
            }

            void finalBlow()
            {
                Point loc;
                //-----------------------------------------
                loc = this.Rectangle.Location + this.LeftTextureRectangle.Location;
                this.LeftTextureRectangle = new(loc, this.LeftTextureRectangle.Size);
                //-----------------------------------------
                loc = this.Rectangle.Location + this.LineTextureRectangle.Location;
                this.LineTextureRectangle = new(loc, this.LineTextureRectangle.Size);
                //-----------------------------------------
                loc = this.Rectangle.Location + this.RightTextureRectangle.Location;
                this.RightTextureRectangle = new(loc, this.RightTextureRectangle.Size);
                //-----------------------------------------
            }
            void anotherBlow()
            {
                Point loc;
                //-----------------------------------------
                loc = this.Rectangle.Location + this.LeftTextureRealLocation;
                this.LeftTextureRectangle = new(loc, this.LeftTextureRectangle.Size);
                //-----------------------------------------
                loc = this.Rectangle.Location + this.LineTextureRealLocation;
                this.LineTextureRectangle = new(loc, this.LineTextureRectangle.Size);
                //-----------------------------------------
                loc = this.Rectangle.Location + this.RightTextureRealLocation;
                this.RightTextureRectangle = new(loc, this.RightTextureRectangle.Size);
                //-----------------------------------------
            }
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
        public override void Update(GameTime gameTime)
        {

        }
        protected override void UpdateGraphics()
        {

        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region

        #endregion
        //-------------------------------------------------
    }
}
