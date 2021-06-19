// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using LTW.Security;
using LTW.Controls.Moving;
using LTW.Controls.Elements;

namespace LTW.SandBox
{
    partial class SandBoxBase : SandBoxElement
    {
        //-------------------------------------------------
        #region Initialize Method's Region
        private void InitializeComponent()
        {
            //---------------------------------------------
            //news:
            this.MyRes = new(this.GetType());
            this._flat = new FlatElement(this, true);
            this.Manager = new ElementManager(this);
            //---------------------------------------------
            //fontAndTextAligns:
            //priorities:
            this.ChangePriority(ElementPriority.SandBox); // move it to sandbox base
            //sizes:
            //ownering:
            //locations:
            //movements:
            this.ChangeMovements(ElementMovements.VerticalHorizontalMovements);
            //colors:
            this._flat.ChangeBackColor(new Color(Color.Black, 0.7f));
            //enableds:
            //texts:
            //images:
            //applyAndShow:
            this._flat.Apply();
            this._flat.Show();
            //events:
            //---------------------------------------------
            //addRanges:
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
            // draw the surface of the sandbox.
            this._flat?.Draw(gameTime, spriteBatch);
            this.Manager?.Draw(gameTime, spriteBatch);
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
        protected override void UpdateGraphics()
        {
            ;
        }
        public override void Update(GameTime gameTime)
        {
            // do nothing here (just for now!)
            // by ALi.w
            // in : 08 / 03 / 2021
        }
        public override void SetLabelName(in StrongString constParam)
        {
            this._flat?.SetLabelName(constParam);
        }
        public override void SetLabelText()
        {
            this._flat?.SetLabelText();
        }
        public override void SetLabelText(in StrongString customValue)
        {
            this._flat?.SetLabelText(customValue);
        }
        /// <summary>
        /// change the size of this sandbox.
        /// </summary>
        /// <param name="w">
        /// the width.
        /// </param>
        /// <param name="h">
        /// the height.
        /// </param>
        public override void ChangeSize(in float w, in float h)
        {
            this._flat?.ChangeSize(w, h);
        }
        /// <summary>
        /// change the size of this sandbox.
        /// </summary>
        /// <param name="w">
        /// the width.
        /// </param>
        /// <param name="h">
        /// the height.
        /// </param>
        public override void ChangeSize(in int w, in int h)
        {
            this._flat?.ChangeSize(w, h);
        }
        /// <summary>
        /// change the location of this sandbox.
        /// </summary>
        /// <param name="x">
        /// the x-coordinate of the sandbox's new location.
        /// </param>
        /// <param name="y">
        /// the y-coordinate of the sandbox's new location.
        /// </param>
        public override void ChangeLocation(in float x, in float y)
        {
            this._flat?.ChangeLocation(x, y);
        }
        /// <summary>
        /// change the location of this sandbox.
        /// </summary>
        /// <param name="x">
        /// the x-coordinate of the sandbox's new location.
        /// </param>
        /// <param name="y">
        /// the y-coordinate of the sandbox's new location.
        /// </param>
        public override void ChangeLocation(in int x, in int y)
        {
            this._flat?.ChangeLocation(x, y);
        }
        /// <summary>
        /// change the location of this sandbox.
        /// </summary>
        /// <param name="location">
        /// the vector2 which represent the new location of this sandbox.
        /// </param>
        public override void ChangeLocation(in Vector2 location)
        {
            this._flat?.ChangeLocation(location);
        }
        /// <summary>
        /// change the default font of this sandbox.
        /// </summary>
        /// <param name="font">
        /// the <see cref="SpriteFont"/> value which will be
        /// the default font of this sandbox.
        /// </param>
        public override void ChangeFont(in SpriteFontBase font)
        {
            this._flat?.ChangeFont(font);
        }
        /// <summary>
        /// change the dafualt fore color of this sandbox.
        /// </summary>
        /// <param name="color">
        /// the <see cref="XColor"/> value which will be the
        /// default fore color of this sandbox.
        /// </param>
        public override void ChangeForeColor(in Color color)
        {
            this._flat?.ChangeForeColor(color);
        }
        /// <summary>
        /// change the default text of this sandbox :|
        /// dunna what will this fucking method do.
        /// probably nothing, so don't use it anyway.
        /// I just overrided it just-in-case.
        /// </summary>
        /// <param name="text">
        /// the <see cref="StrongString"/> value which will be the
        /// default text of this fucking bullshit sandbox.
        /// </param>
        public override void ChangeText(in StrongString text)
        {
            this._flat?.ChangeText(text);
        }
        public override void ChangeMovements(in ElementMovements movements)
        {
            if (this.MoveManager != null)
            {
                base.ChangeMovements(movements);
            }
            else
            {
                base.ChangeMovements(movements, new MoveManager(this));
            }
        }
        public override void ChangeMovements(ElementMovements movements, in IMoveManager manager)
        {
            base.ChangeMovements(movements, manager);
        }
        #endregion
        //-------------------------------------------------
        #region ordinary Method's Region
        /// <summary>
        /// set the location of the sandbox to the
        /// center of the location.
        /// <code>NOTICE:</code>
        /// if you change the size of the sandbox,
        /// you should call this method again in order to 
        /// regain the centering of the sandbox.
        /// </summary>
        public void CenterToScreen()
        {
            var x = (UnderForm.Width / 2) - (this.Width / 2);
            var y = (UnderForm.Height / 2) - (this.Height / 2);
            this.ChangeLocation(x, y);
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public bool IsErrorSandBox()
        {
            return
                SandBoxPriority == SandBoxPriority.LowErrorSandBox ||
                SandBoxPriority == SandBoxPriority.TopMostErrorSandBox;
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region

        #endregion
        //-------------------------------------------------
        #region sealed Method's Region
        protected override sealed Texture2D GetBackGroundTexture(Color color)
        {
            return null;
        }
        #endregion
        //-------------------------------------------------
    }
}
