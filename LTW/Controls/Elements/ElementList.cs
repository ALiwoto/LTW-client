// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LTW.GameObjects.WMath;

namespace LTW.Controls.Elements
{
    public class ElementList<T> : ListW<T> 
        where T : GraphicElements
    {
        //-------------------------------------------------
        #region Properties Region
        public ElementManager Manager { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        internal ElementList(in ElementManager _manager_)
        {
            Manager = _manager_;
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public bool MouseIn()
        {
            for (int i = Length - 1; i >= 0; i--)
            {
                if (this[i].MouseIn())
                {
                    return true;
                }
            }
            return false;
        }
        public bool WasMouseIn()
        {
            for (int i = Length - 1; i >= 0; i--)
            {
                if (this[i].WasMouseIn())
                {
                    return true;
                }
            }
            return false;
        }
        public void MouseChange()
        {
            for (int i = Length - 1; i >= 0; i--)
            {
                if (this[i] != null)
                {
                    if (this[i].WasMouseIn())
                    {
                        this[i]?.MouseChange();
                        return;
                    }
                }
            }
        }
        public virtual bool ContainsChild(T _item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] != null)
                {
                    if (this[i].ContainsChild(_item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Method's Region
        public void MouseMove()
        {
            for (int i = Length - 1; i >= 0; i--)
            {
                // check if the MouseIn property of this element
                // is true or not.
                // it helps us find out if the mouse was in the
                // region of the element or not.
                if (this[i].IsMouseIn)
                {

                }
                else
                {
                    if (this[i].MouseIn())
                    {

                    }
                }
            }
        }
        /// <summary>
        /// dispose all the elements in the list.
        /// </summary>
        public void DisposeAll()
        {
            for (int i = 0; i < Length; i++)
            {
                this[i]?.Dispose();
            }
        }
        /// <summary>
        /// apply all the elements in the list.
        /// </summary>
        public void ApplyAll()
        {
            for (int i = 0; i < Length; i++)
            {
                this[i]?.Apply();
            }
        }
        /// <summary>
        /// disable all the elements in the list.
        /// </summary>
        public void DisableAll()
        {
            for (int i = 0; i < Length; i++)
            {
                this[i]?.Disable();
            }
        }
        /// <summary>
        /// enable all the elements in the list.
        /// </summary>
        public void EnableAll()
        {
            for (int i = 0; i < Length; i++)
            {
                this[i]?.Enable();
            }
        }
        #endregion
        //-------------------------------------------------
        #region Graphics Method's Region 
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Length; i++)
            {
                this[i]?.Draw(gameTime, spriteBatch);
            }
        }
        #endregion
        //-------------------------------------------------
    }
}
