// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Collections;
using LTW.GameObjects.WMath;
using LTW.Controls.Elements;

namespace LTW.Controls.Moving
{
    partial class MoveManager
    {
        //-------------------------------------------------
        #region Initialize Method's Region
        private void InitializeComponent()
        {
            //---------------------------------------------
            this.Elements = new(this);
            //---------------------------------------------
            //Enabled:
            this.Enable();
            //---------------------------------------------
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
        #region ordinary Method's Region
        public virtual void MoveUs()
        {
            foreach (var _e in this.Elements)
            {
                if (_e != null)
                {
                    if (!_e.IsDisposed && _e.Visible)
                    {
                        _e.MoveMe();
                    }
                }
            }
        }
        public virtual void Discharge(IMoveable sender)
        {
            if (this.IsShocked)
            {
                if (this.Activated != null)
                {
                    this.IsShocked = false;
                    this.Activated.MouseMove -= Activated_MouseMove;
                    this.Activated = null;
                }
            }
        }
        public virtual void Shocker(IMoveable sender)
        {
            if (sender != null && !sender.IsDisposed)
            {
                this.IsShocked = true;
                this.Activated = sender;
                this.Activated.MouseMove -= Activated_MouseMove;
                this.Activated.MouseMove += Activated_MouseMove;
            }
        }
        public virtual void AddMe(IMoveable me)
        {
            var i = this.Contains(me);
            if (i != NOT_FOUND)
            {
                this.Elements.Remove(this.Elements[i]);
            }
            this.Elements.Add(me);
        }
        public virtual void Add(IMoveable moveable)
        {
            this.AddMe(moveable);
        }
        public virtual void Remove(IMoveable moveable)
        {
            if (this.Elements.Exists(moveable))
            {
                this.Elements.Remove(moveable);
            }
        }
        public virtual void Enable()
        {
            if (!this.Enabled)
            {
                this.Enabled = true;
            }
        }
        public virtual void Disable()
        {
            if (this.Enabled)
            {
                this.Enabled = false;
            }
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public virtual int Contains(IMoveable moveable)
        {
            for (int i = 0; i < this.Elements.Length; i++)
            {
                if (this.Elements[i] != null)
                {
                    if (this.Elements[i].ContainsChild(moveable))
                    {
                        return i;
                    }
                }
            }
            return NOT_FOUND;
        }
        public IEnumerator GetEnumerator()
        {
            return this.Elements.GetEnumerator();
        }
        #endregion
        //-------------------------------------------------
        #region Event Method's Region
        private void Activated_MouseMove(object sender, EventArgs e)
        {
			if (this.Activated == null || sender != this.Activated)
			{
				return;
			}
			if (this.Activated.IsDisposed || !this.Activated.Visible)
			{
				return;
			}
            if (this.Enabled && this.IsShocked)
            {
                if (this.MustDown)
                {
                    if (!GraphicElement.BigFather.IsLeftDown)
                    {
                        return;
                    }
                }
                this.MoveUs();
            }
        }
        #endregion
        //-------------------------------------------------
    }
}
