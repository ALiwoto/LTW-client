// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LTW.SandBox;
using LTW.GameObjects.WMath;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.Controls.Elements
{
    partial class ElementManager
    {
        //-------------------------------------------------
        #region Initialize Method's Region
        private void InitializeComponent()
        {
            //---------------------------------------------
            //news:
            this.VeryLowElements    = new ElementList<GraphicElement>(this); // 1
            this.LowElements        = new ElementList<GraphicElement>(this); // 2
            this.NormalElements     = new ElementList<GraphicElement>(this); // 3
            this.HighElements       = new ElementList<GraphicElement>(this); // 4
            this.VeryHighElements   = new ElementList<GraphicElement>(this); // 5
            this.SuperHighElements  = new ElementList<GraphicElement>(this); // 6
            this.BeyondHighElements = new ElementList<GraphicElement>(this); // 7
            this.TopMostElements    = new ElementList<GraphicElement>(this); // 8
            this.LowSandBoxes       = new ElementList<SandBoxElement>(this);  // 9
            this.TopMostSandBoxes   = new ElementList<SandBoxElement>(this);  // 10
            this.Elements           = new ListW<ElementList<GraphicElement>>()
            {
                this.VeryLowElements,
                this.LowElements,
                this.NormalElements,
                this.HighElements,
                this.VeryHighElements,
                this.SuperHighElements,
                this.BeyondHighElements,
                this.TopMostElements,
            };
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
        #region private Method's Region
        /// <summary>
        /// disable all the ordinary elements when a new sandbox is added.
        /// NOTICE: this method will only set the enable property of the elements
        /// to false, it won't change the sandboxes' property,
        /// so when a ErrorSandBox is called, you should use 
        /// <see cref="DisableAllSandBoxes()"/> besides this method.
        /// <!-- 2021 / 02 / 15  11:25 PM -->
        /// </summary>
        public void DisableAll()
        {
            this.VeryLowElements?.DisableAll();     // 1
            this.LowElements?.DisableAll();         // 2
            this.NormalElements?.DisableAll();      // 3
            this.HighElements?.DisableAll();        // 4
            this.VeryHighElements?.DisableAll();    // 5
            this.SuperHighElements?.DisableAll();   // 6
            this.BeyondHighElements?.DisableAll();  // 7
            this.TopMostElements?.DisableAll();     // 8
        }
        /// <summary>
        /// use this when an ErrorSandBox is added to the manager.
        /// this method will disable the all of SandBoxes.
        /// NOTICE: this method will NOT disable the ErrorSandBoxes,
        /// but if there is Low and TopMostErrorSandBox, 
        /// this method will disable the LowErrorSandBox.
        /// </summary>
        public void DisableAllSandBoxes(bool _is_top_added = false)
        {
            this.LowSandBoxes?.DisableAll();        // 9
            this.TopMostSandBoxes?.DisableAll();    // 10
            if (_is_top_added)
            {
                this.LowErrorSandBox?.Disable();
            }
        }
        /// <summary>
        /// enable all the ordinary elements.
        /// NOTICE: this method will only set the enable property of the elements
        /// to true, it won't change the sandboxes' property,
        /// so when a ErrorSandBox is called, you should use 
        /// <see cref="DisableAllSandBoxes(bool)"/> besides this method.
        /// <!-- 2021 / 02 / 15  11:25 PM -->
        /// </summary>
        public void EnableAll()
        {
            this.VeryLowElements?.EnableAll();     // 1
            this.LowElements?.EnableAll();         // 2
            this.NormalElements?.EnableAll();      // 3
            this.HighElements?.EnableAll();        // 4
            this.VeryHighElements?.EnableAll();    // 5
            this.SuperHighElements?.EnableAll();   // 6
            this.BeyondHighElements?.EnableAll();  // 7
            this.TopMostElements?.EnableAll();     // 8
        }
        /// <summary>
        /// enable the last sand box of this manager.
        /// use this method when a sandbox has been closed.
        /// for example if a LowErrorSandBox is closed,
        /// but please consider this, if a TopMostSandBox is closed,
        /// it means the game should be closed too, so there is no need to call this method.
        /// </summary>
        public void EnableLastSandBox()
        {
            if (LowErrorSandBox != null)
            {
                if (!LowErrorSandBox.Enabled)
                {
                    LowErrorSandBox.Enable();
                    return;
                }
            }
            if (TopMostSandBoxes.Length > 1)
            {
                for (int i = TopMostSandBoxes.Length - 1; i >= 0; i--)
                {
                    if (TopMostSandBoxes[i] != null)
                    {
                        if (!TopMostSandBoxes[i].Enabled)
                        {
                            TopMostSandBoxes[i].Enable();
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (LowSandBoxes.Length > 0)
            {
                for (int i = LowSandBoxes.Length - 1; i >= 0; i--)
                {
                    if (LowSandBoxes[i] != null)
                    {
                        if (!LowSandBoxes[i].Enabled)
                        {
                            LowSandBoxes[i].Enable();
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public bool Exists(GraphicElement _e)
        {
            // check if the passed-by graphic element is an sandbox or not.
            if (_e is SandBoxBase _s)
            {
                // check if the passed-by sandbox is 
                // an error sandbox or not.
                if (_s.IsErrorSandBox())
                {
                    // check if this element manager is owned by
                    // some graphic element or not.
                    if (HasOwner)
                    {
                        // NOTICE: 
                        // only the Element Manager of the GameClient
                        // should have the error sandbox,
                        // so if you are looking for an error sandbox
                        // in here, then you are in a wrong place!
                        return false;
                    }
                    if (this.LowErrorSandBox != null)
                    {
                        if (LowErrorSandBox.Equals(_s))
                        {
                            return true;
                        }
                    }
                    if (TopMostErrorSandBox != null)
                    {
                        if (TopMostErrorSandBox.Equals(_s))
                        {
                            return true;
                        }
                    }
                    //return LowErrorSandBox.Equals(_s) || TopMostErrorSandBox.Equals(_s);
                    return false;
                }
                else
                {
                    // if the passed-by value is not an error sandbox,
                    // but just an ordinary sandbox, then it doesn't matter
                    // if this manager is owned by only an ordinary sandbox or element,
                    // you still can get the list of the sandboxes in this manager
                    // and search for the passed by element.
                    // for example, this manager is owned by a HallSandBox 
                    // (or not even a sandbox), and it has a sandbox on it,
                    // so long as the passed-by value is not an error sandbox,
                    // it's okay to try for searching for it.
                    return GetList(_s.SandBoxPriority).Exists(_s);
                }
            }
            // it means the passed-by graphic element is just an ordinary
            // element, so you can get the list using it's priority.
            return GetList(_e.Priority).Exists(_e);
        }
        public bool ContainsChild(GraphicElement _e)
        {
            // check if the passed-by graphic element is an sandbox or not.
            if (_e is SandBoxBase _s)
            {
                // check if the passed-by sandbox is 
                // an error sandbox or not.
                if (_s.IsErrorSandBox())
                {
                    // check if this element manager is owned by
                    // some graphic element or not.
                    if (HasOwner)
                    {
                        // NOTICE: 
                        // only the Element Manager of the GameClient
                        // should have the error sandbox,
                        // so if you are looking for an error sandbox
                        // in here, then you are in a wrong place!
                        return false;
                    }
                    // check if the sand box you are looking for is 
                    // one of the low error sand boxes or their child or not.
                    return LowErrorSandBox.ContainsChild(_s) || TopMostErrorSandBox.ContainsChild(_s);
                }
                else
                {
                    // if the passed-by value is not an error sandbox,
                    // but just an ordinary sandbox, then it doesn't matter
                    // if this manager is owned by only an ordinary sandbox or element,
                    // you still can get the list of the sandboxes in this manager
                    // and search for the passed by element.
                    // for example, this manager is owned by a HallSandBox 
                    // (or not even a sandbox), and it has a sandbox on it,
                    // so long as the passed-by value is not an error sandbox,
                    // it's okay to try for searching for it.
                    // check if the low sandboxes list is null or not.
                    if (this.LowSandBoxes != null)
                    {
                        // check if the sandbox you are looking for is 
                        // one the low sandboxes or their child or not.
                        if (this.LowSandBoxes.ContainsChild(_s))
                        {
                            // it means the sandbox is one the low sandboxes
                            // or their child.
                            return true;
                        }
                    }
                    // check if the top most sandboxes list is null or not.
                    if (this.TopMostSandBoxes != null)
                    {
                        // check if the sandbox you are looking for is 
                        // one the top most sandboxes or their child or not.
                        if (this.TopMostSandBoxes.ContainsChild(_s))
                        {
                            // it means the sandbox is one the top most sandboxes
                            // or their child.
                            return true;
                        }
                    }
                    // huh? we are sure that the passed-by value was a sandbox,
                    // but it is not in our lists, so it means you should
                    // return false.
                    return false;
                }
            }
            // using a loop for searching in the elements.
            for (int i = 0; i < Elements.Length; i++)
            {
                // check if the current elment list is null or not.
                if (this.Elements[i] != null)
                {
                    // check if the current element list contains this element
                    // or not.
                    if (this.Elements[i].ContainsChild(_e))
                    {
                        // it means the current element list contans the element,
                        // so you should return true.
                        return true;
                    }
                }
            }
            // it means the passed-by graphic element is just an ordinary
            // element, so you can get the list using it's priority.
            return false;
        }
        public bool MouseContains()
        {
            if (this.TopMostErrorSandBox != null)
            {
                if (this.TopMostErrorSandBox.WasMouseIn())
                {
                    return true;
                }
            }
            if (this.LowErrorSandBox != null)
            {
                if (this.LowErrorSandBox.WasMouseIn())
                {
                    return true;
                }
            }
            if (this.TopMostSandBoxes != null)
            {
                if (this.TopMostSandBoxes.WasMouseIn())
                {
                    return true;
                }
            }
            if (this.LowSandBoxes != null)
            {
                if (this.LowSandBoxes.WasMouseIn())
                {
                    return true;
                }
            }
            for (int i = this.Elements.Length - 1; i >= 0 ; i--)
            {
                if (this.Elements[i] != null)
                {
                    if (this.Elements[i].WasMouseIn())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public GraphicElement[] GetArray(ElementPriority _p)
        {
            // get the array of the list.
            return GetList(_p).GetArray();
        }
        public ElementList<GraphicElement> GetList(ElementPriority _p)
        {
            // switch on the priority to get the right element list.
            return _p switch
            {
                ElementPriority.VeryLow => VeryLowElements,
                ElementPriority.Low => LowElements,
                ElementPriority.Normal => NormalElements,
                ElementPriority.High => HighElements,
                ElementPriority.VeryHigh => VeryHighElements,
                ElementPriority.SuperHigh => SuperHighElements,
                ElementPriority.BeyondHigh => BeyondHighElements,
                ElementPriority.TopMost => TopMostElements,
                _ => null,
            };
        }
        public ElementList<SandBoxElement> GetList(SandBoxPriority _p)
        {
            // switch on the sandbox priority to get the right element list.
            return _p switch
            {
                SandBoxPriority.LowSandBox => LowSandBoxes,
                SandBoxPriority.TopMostSandBox => TopMostSandBoxes,
                _ => null,
            };
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Method's Region
        public void MouseChange()
        {
            if (this.TopMostErrorSandBox != null)
            {
                if (this.TopMostErrorSandBox.WasMouseIn())
                {
                    this.TopMostErrorSandBox?.MouseChange();
                    return;
                }
            }
            if (this.LowErrorSandBox != null)
            {
                if (this.LowErrorSandBox.WasMouseIn())
                {
                    this.LowErrorSandBox?.MouseChange();
                    return;
                }
            }
            if (this.TopMostSandBoxes != null)
            {
                if (this.TopMostSandBoxes.WasMouseIn())
                {
                    this.TopMostSandBoxes?.MouseChange();
                    return;
                }
            }
            if (this.LowSandBoxes != null)
            {
                if (this.LowSandBoxes.WasMouseIn())
                {
                    this.TopMostSandBoxes?.MouseChange();
                    return;
                }
            }
            if (this.Elements != null)
            {
                for (int i = this.Elements.Length - 1; i >= 0; i--)
                {
                    if (this.Elements[i] != null)
                    {
                        if (this.Elements[i].WasMouseIn())
                        {
                            this.Elements[i]?.MouseChange();
                            return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Adds an object to the end of the <see cref="ElementManager"/>.
        /// </summary>
        /// <param name="_e">
        /// The object to be added to the end of the <see cref="ElementManager"/>. The
        /// value can be null for reference types.
        /// </param>
        public void Add(GraphicElement _e)
        {
            // check if the passed-by graphic element already exists or not.
            if (this.Exists(_e))
            {
                // it means the passed-by graphic element, already exists in this
                // element manager, so you cannot add it again.
                return;
            }
            // check if the passed-by graphic element is a sandbox or not.
            if (_e is SandBoxBase _s)
            {
                // check if the passed-by sandbox is an error sandbox or not.
                if (_s.IsErrorSandBox() && _s is ErrorSandBox _err)
                {
                    // check if this element manager is owned by some
                    // other graphic element or not.
                    if (this.HasOwner)
                    {
                        // if you wanna add an error sandbox to an element manager,
                        // the target element manager should be owned by big father
                        // (I mean the GameClient).
                        // right, you can't add an error sandbox to another
                        // element managers, which have owner.
                        return;
                    }
                    // check if the sandbox priority of the sandbox is set to the
                    // LowErrorSandBox or not.
                    if (_s.SandBoxPriority == SandBoxPriority.LowErrorSandBox)
                    {
                        // check if the LowErrorSandBox is null or not.
                        if (this.LowErrorSandBox != null)
                        {
                            // there should be only one LowErrorSandBox
                            // in the same time, so if you wanna add a new one,
                            // please ensure that the previous one (if you added before)
                            // is disposed and alredy removed from the manager, 
                            // otherwise, the manager won't add the new LowErrorSandBox.
                            return;
                        }
                        else
                        {
                            // once a sandbox is added to the manager,
                            // please don't forget to set the enabled property of
                            // the other elements to false.
                            this.DisableAll();
                            this.DisableAllSandBoxes();
                            this.LowErrorSandBox = _err;
                            return;
                        }
                    }
                    // check if the sandbox priority of the sandbox is set to the
                    // TopMostErrorSandBox or not.
                    else if (_s.SandBoxPriority == SandBoxPriority.TopMostErrorSandBox)
                    {
                        // check if already a TopMostErrorSandBox is added or not. 
                        if (this.TopMostErrorSandBox != null)
                        {
                            // it means the TopMostErrorSandBox is not null,
                            // so a TopMostErrorSandBox is already exists in the game
                            // and you cannot add another one.
                            return;
                        }
                        // NOTICE: when a TopMostErrorSandBox such as
                        // NoInternetConnectionSandBox(Connection Closed) is added to the manager of the
                        // GameClient (other managers cannot add error sandboxes),
                        // the total activity of the game should be stopped working,
                        // because it is a fatal error and game will not be able to continue
                        // it's activity, well it's more our privacy policy to do so when
                        // the internet connection is lost.
                        // once a TopMostErrorSandBox is added, you cannot remove it from the
                        // manager, and manager will set the enabled property of other
                        // Elements to false.
                        // for security reason also, player should reset the game,
                        // it means they have to close the game and then start it again.
                        // NOTICE: in the MainForm (Starting Mode) of the GameClient,
                        // if the user does not access to the internet connection,
                        // NoInternetConnection (not ConnectionClosed Mode), should be added
                        // as LowErrorSandBox, but at that rate, it's impossible to add another
                        // sandboxes on it, becase the game activity will not work 
                        // (there is no activity in that time at the first place),
                        // so physically, it is a TopMostErrorSandBox, 
                        // but recognized as a LowErrorSandBox.
                        // Disable all of the graphic elements.
                        this.DisableAll();
                        // disable all of the sandboxes, dispite of being error sandbox or not
                        // (set the default arg to true).
                        this.DisableAllSandBoxes(true);
                        // set the TopMostErrorSandBox.
                        this.TopMostErrorSandBox = _err;
                        return;
                    }
                    // it's impossible to reach this point, but just in case return the method.
                    return;
                }
                else
                {
                    // get the list of the sandboxes using passed-by 
                    // sandbox's SandBoxPriority property and add it to that list.
                    var _ls = GetList(_s.SandBoxPriority);
                    _ls?.Add(_s);
                    return;
                }
            }
            // get the list of the elements using element's pririty property,
            // and add it to that list.
            var _l = GetList(_e.Priority);
            _l?.Add(_e);
        }
        /// <summary>
        /// clear the whole element manager elements.
        /// this mehod will DISPOSE them all.
        /// </summary>
        public void Clear()
        {
            // dispose all the elements.
            DisposeAll();
            VeryLowElements?.Clear();
            LowElements?.Clear();
            NormalElements?.Clear();
            HighElements?.Clear();
            VeryHighElements?.Clear();
            SuperHighElements?.Clear();
            BeyondHighElements?.Clear();
            TopMostElements?.Clear();
        }
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ElementManager"/>.
        /// </summary>
        /// <param name="_e">
        /// the params elements.
        /// </param>
        public void AddRange(params GraphicElement[] _e)
        {
            for (int i = 0; i < _e.Length; i++)
            {
                Add(_e[i]);
            }
        }
        public void Remove(GraphicElement _e)
        {
            if (Exists(_e))
            {
                _e.Dispose();
            }
        }
        /// <summary>
        /// Dispose All Elements.
        /// </summary>
        public void DisposeAll()
        {
            this.VeryLowElements?.DisposeAll();
            this.LowElements?.DisposeAll();
            this.NormalElements?.DisposeAll();
            this.HighElements?.DisposeAll();
            this.VeryHighElements?.DisposeAll();
            this.SuperHighElements?.DisposeAll();
            this.BeyondHighElements?.DisposeAll();
            this.TopMostElements?.DisposeAll();
        }
        /// <summary>
        /// update the locations of the children elements.
        /// if this manager has no owner (is game client's manager),
        /// this method will do nothing.
        /// </summary>
        public void UpdateLocations()
        {
            if (!this.HasOwner)
            {
                return;
            }
            if (this.TopMostSandBoxes != null)
            {
                foreach (var _s in this.TopMostSandBoxes)
                {
                    _s?.OwnerLocationUpdate();
                }
            }
            if (this.LowSandBoxes != null)
            {
                foreach (var _s in this.LowSandBoxes)
                {
                    _s.OwnerLocationUpdate();
                }
            }
            foreach (var _eL in this.Elements)
            {
                foreach (var _e in _eL)
                {
                    _e?.OwnerLocationUpdate();
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region Graphical and controller Region
        /// <summary>
        /// Drawing the Elements in order of their priorities.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="graphics"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
#if DRAW_OLD
            this.VeryLowElements?.Draw(gameTime, spriteBatch);
            this.LowElements?.Draw(gameTime, spriteBatch);
            this.NormalElements?.Draw(gameTime, spriteBatch);
            this.HighElements?.Draw(gameTime, spriteBatch);
            this.VeryHighElements?.Draw(gameTime, spriteBatch);
            this.SuperHighElements?.Draw(gameTime, spriteBatch);
            this.BeyondHighElements?.Draw(gameTime, spriteBatch);
            this.TopMostElements?.Draw(gameTime, spriteBatch);
#endif
            if (this.Elements != null)
            {
                foreach (var _el in this.Elements)
                {
                    _el?.Draw(gameTime, spriteBatch);
                }
            }
            this.LowSandBoxes?.Draw(gameTime, spriteBatch);
            this.TopMostSandBoxes?.Draw(gameTime, spriteBatch);
            this.LowErrorSandBox?.Draw(gameTime, spriteBatch);
            this.TopMostErrorSandBox?.Draw(gameTime, spriteBatch);
        }
        #endregion
        //-------------------------------------------------
    }
}