// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using WotoProvider.Enums;
using LTW.SandBox;
using LTW.Security;
using LTW.Constants;
using LTW.Controls.Moving;
using M_Manager = LTW.Controls.Moving.MoveManager;
using XColor = Microsoft.Xna.Framework.Color;
using XPoint = Microsoft.Xna.Framework.Point;
using XRectangle = Microsoft.Xna.Framework.Rectangle;

namespace LTW.Controls.Elements
{
	partial class GraphicElements
	{
		//-------------------------------------------------
		#region Designing Region
		private void InitializeComponent()
		{
			//----------------------------------
			//News:
			//----------------------------------
			//Names:
			//TabIndexes
			//FontAndTextAligns:
			//Sizes:
			//Locations:
			//Colors:
			//ComboBoxes:
			//Enableds:
			//Texts:
			//AddRanges:
			//ToolTipSettings:
			//----------------------------------
			//Events:
			//----------------------------------
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Methods Region
		/// <summary>
		/// Apply the element, so the element draw itself.
		/// you should call this method only once and only at the start.
		/// </summary>
		public virtual void Apply()
		{
			if (!this.IsApplied)
			{
				this.IsApplied = true;
			}
		}
		/// <summary>
		/// Dispose the elements,
		/// this method will release all the resources used
		/// by the element, so use it carefully!
		/// </summary>
		public virtual void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.IsDisposed = true;
			}
		}
		/// <summary>
		/// Disable the element,
		/// this method will set the <see cref="Enabled"/> property 
		/// to <c>false</c>.
		/// if you want to enable the element, use <see cref="Enable()"/> method.
		/// </summary>
		public virtual void Disable()
		{
			if (this.Enabled)
			{
				this.Enabled = false;
			}
			this.Manager?.DisableAll();
		}
		/// <summary>
		/// Enable the element.
		/// If the element is already enabled, this method
		/// won't ignore `Manager.EnableAll()`.
		/// It will call `Manager.EnableAll()` even if this
		/// element is already enabled.
		/// But if the element is disposed, this method won't
		/// do anything. It won't call `Manager.EnableAll()` at all.
		/// </summary>
		public virtual void Enable()
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (!this.Enabled)
			{
				this.Enabled = true;
			}
			this.Manager?.EnableAll();
		}
		/// <summary>
		/// Enable the element.
		/// If the element is already enabled, this method
		/// won't ignore `Manager.EnableAll()`.
		/// It will call `Manager.EnableAll()` even if this
		/// element is already enabled.
		/// But if the element is disposed, this method won't
		/// do anything. It won't call `Manager.EnableAll()` at all.
		/// </summary>
		/// <param name="childs">
		/// set it to true if you want this method to enable all
		/// children of this elemenet as well (if any exist).
		/// </param>
		public virtual void Enable(bool childs)
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (!this.Enabled)
			{
				this.Enabled = true;
			}
			if (childs) 
			{
				this.Manager?.EnableAll();
			}
		}
		/// <summary>
		/// Enable the owner mover. <code></code>
		/// NOTICE: if you enable the <see cref="OwnerMover"/>,
		/// then it doesn't matter what is the <see cref="Movements"/> 
		/// of this element, the element will shock it's owner
		/// (if the owner is not null).
		/// Please take note that if this element is disposed,
		/// then this method will do nothing.
		/// </summary>
		public virtual void EnableOwnerMover()
		{
			if (this.IsDisposed) 
			{
				return;
			}
			if (!this.OwnerMover)
			{
				this.OwnerMover = true;
			}
		}
		/// <summary>
		/// Disable the owner mover.
		/// this method will set the property 
		/// <see cref="OwnerMover"/> to <c>false</c>.
		/// </summary>
		public virtual void DisableOwnerMover()
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (this.OwnerMover)
			{
				this.OwnerMover = false;
			}
		}
		/// <summary>
		/// Show the element.
		/// If this element is disposed, this method
		/// will do nothing.
		/// </summary>
		public virtual void Show()
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (!this.Visible)
			{
				this.Visible = true;
			}
		}
		/// <summary>
		/// Hide the element.
		/// </summary>
		public virtual void Hide()
		{
			if (this.Visible)
			{
				this.Visible = false;
			}
		}
		/// <summary>
		/// make the element barren, so it cannot have any
		/// child.
		/// it makes the element light so you can use it 
		/// better.
		/// WARNING: if this element has childs, this method will get rid of them.
		/// and barrening the element has no returning, so you won't be able to
		/// reverse it after barrening the element.
		/// </summary>
		public virtual void Barren()
		{
			// please don't check `if (this.IsDisposed)` here,
			// because there is possibility that a user
			// wants to make this element barren after
			// disposing it.
			// it won't do any hurt, but it's useless at this
			// point. so let it be. all the childern will be
			// disposen at this point (of course if they exist
			// in the first place).
			if (!this.IsBarren)
			{
				this.Manager?.DisposeAll();
				this.Manager = null;
			}
		}
		/// <summary>
		/// Shock the <see cref="MoveManager"/> of the element.
		/// If this element is disposed or is hidden, this method
		/// won't do anything.
		/// </summary>
		public virtual void Shocker()
		{
			if (this.IsDisposed || !this.Visible)
			{
				return;
			}
			if (this.WasMouseIn())
			{
				if (this.OwnerMover)
				{
					if (this.HasOwner && this.Owner != null)
					{
						if (this.Owner.Movements != ElementMovements.NoMovements)
						{
							this.Owner.Shocker(this);
						}
					}
				}
				if (this.Movements != ElementMovements.NoMovements)
				{
					if (this.MoveManager != null)
					{
						if (this.MoveManager.Enabled)
						{
							this.LastPoint = M_Manager.Point.ToVector2();
							this.MoveManager?.Shocker(this);
						}
					}
				}
			}
		}

		/// <summary>
		/// Shock the <see cref="MoveManager"/> of the element and
		/// specify which child shocked it.
		/// If this element is disposed or is hidden, this method
		/// won't do anything.
		/// </summary>
		/// <param name="child">
		/// In most situations you have to send `this` for 
		/// this argument.
		/// </param>
		public virtual void Shocker(GraphicElements child)
		{
			if (this.IsDisposed || !this.Visible)
			{
				return;
			}
			if (this.WasMouseIn())
			{
				if (this.OwnerMover)
				{
					if (this.HasOwner && this.Owner != null)
					{
						if (this.Owner.Movements != ElementMovements.NoMovements)
						{
							child.LastPoint = this.LastPoint = 
								M_Manager.Point.ToVector2();
							this.Owner.Shocker(this);
						}
					}
				}
				if (this.Movements != ElementMovements.NoMovements)
				{
					if (this.MoveManager != null)
					{
						if (this.MoveManager.Enabled)
						{
							child.LastPoint = this.LastPoint = 
								M_Manager.Point.ToVector2();
							this.MoveManager?.Shocker(child);
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Lock the mouse on this element, so even if we
		/// change the location of the mouse with high speed,
		/// it doesn't bug out.
		/// If the element is disposed or it's hidden,
		/// this method won't work.
		/// </summary>
		public virtual void LockMouse()
		{
			if (this.IsDisposed || !this.Visible)
			{
				return;
			}
			if (!this.IsMouseLocked)
			{
				this.IsMouseLocked = true;
				LockedElement = this;
			}
		}

		/// <summary>
		/// Unlock the mouse so we don't move this element
		/// according to mouse moves.
		/// </summary>
		public virtual void UnLockMouse()
		{
			// please do not check if this element is
			// already disposed or not.
			// it will cause some bugs, because it's possible
			// that we call this method after calling `Dispose` method.
			// so please take note of that.
			if (this.IsMouseLocked)
			{
				this.IsMouseLocked = false;
				// do NOT set LockedElement to null here.
				// LockedElement = null;
			}
		}

		/// <summary>
		/// Discharge the <see cref="MoveManager"/> of the element.
		/// </summary>
		public virtual void Discharge()
		{
			if (this.OwnerMover)
			{
				if (this.HasOwner && this.Owner != null)
				{
					if (this.Owner.Movements != ElementMovements.NoMovements)
					{
						this.Owner.Discharge();
					}
				}
			}
			if (this.Movements != ElementMovements.NoMovements)
			{
				if (this.MoveManager != null)
				{
					if (this.MoveManager.Enabled)
					{
						if (this.MoveManager.IsShocked)
						{
							this.MoveManager.Discharge(this);
						}
					}
				}
			}
		}
		/// <summary>
		/// Move the element with the specified parameters.
		/// </summary>
		/// <param name="divergeX">
		/// x diverge of the element.
		/// </param>
		/// <param name="divergeY">
		/// y diverge of the element.
		/// </param>
		public virtual void MoveMe(in float divergeX, in float divergeY)
		{
			if (this.Movements != ElementMovements.NoMovements)
			{
				Vector2 pos;
				if (this.HasOwner)
				{
					pos = this.RealPosition;
				}
				else
				{
					pos = this.Position;
				}
				switch (this.Movements)
				{
					case ElementMovements.NoMovements:
						return;
					case ElementMovements.VerticalMovements:
						this.ChangeLocation(pos.X, pos.Y + divergeY);
						break;
					case ElementMovements.HorizontalMovements:
						this.ChangeLocation(pos.X + divergeX, pos.Y);
						break;
					case ElementMovements.VerticalHorizontalMovements:
						this.ChangeLocation(pos.X + divergeX, pos.Y + divergeY);
						break;
					default:
						break;
				}
				this.LastPoint = M_Manager.Point.ToVector2();
			}
		}
		/// <summary>
		/// Move the element and call 
		/// the <see cref="MoveMe(float, float)"/> method
		/// with the automatic value.
		/// </summary>
		public virtual void MoveMe()
		{
			if (this.Movements != ElementMovements.NoMovements)
			{
				var current = M_Manager.Point;
				var last = this.LastPoint;
				switch (this.Movements)
				{
					case ElementMovements.NoMovements:
						return;
					case ElementMovements.VerticalMovements:
						this.MoveMe(BASE_INDEX, current.Y - last.Y);
						break;
					case ElementMovements.HorizontalMovements:
						this.MoveMe(current.X - last.X, BASE_INDEX);
						break;
					case ElementMovements.VerticalHorizontalMovements:
						this.MoveMe(current.X - last.X, current.Y - last.Y);
						break;
					default:
						break;
				}
				this.LastPoint = current.ToVector2();
			}
		}

		/// <summary>
		/// call this in the game client,
		/// when the mouse moved.
		/// </summary>
		internal virtual void MouseChange()
		{
			if (!this.Enabled || !this.Visible)
			{
				return;
			}
			// check if the mouse only is in the region of this element or not.
			if (!this.MouseHere() && this.MouseIn())
			{
				this.Manager?.MouseChange();
			}
			else
			{
				// check if the mouse currently is in the region or not.
				if (this.MouseIn() || this.IsMouseLocked)
				{
					// check if mouse was in the region in previous update
					// or not.
					if (this.IsMouseIn || this.IsMouseLocked)
					{
						// it means in the previous update, the mouse pointer
						// was in the region, and now it's also in the region.
						// <----------->
						// check we should raise the mouse move event or not.
						this._checkMouseMove();
						// check we should raise the mouse click event or not.
						this._checkClick();
					}
					else
					{
						// it means the mouse pointer was not in the region of this 
						// graphic element, but now it is in the region.
						// so you should set the property and rase the mouse enter event.
						this.IsMouseIn = true;
						this.OnMouseEnter();
					}
				}
				else
				{
					// it means the mouse pointer currenty is not in the
					// region of this graphic element.
					// check if the mouse pointer was in the region of this 
					// graphic element in the previous update or not.
					if (this.IsMouseIn)
					{
						// it means the mouse pointer was in the region of this
						// graphic element, but it's not.
						// what do you think about it??
						// what does it mean??
						// of course it means that the mouse pointer leaves the
						// region of the graphic element,
						// so you should raise the mouse leave event and
						// also set the IsMouseIn property to false.
						this.IsMouseIn = false;
						this.OnMouseLeave();
						// invalid the current status.
						this.invalidOnce();
					}
					else
					{
						// impossible to reach here!
						// if you reach here, it means some mistakes have been
						// taken.
						// anyway, just in case I will add return here.
						return;
					}
				}
			}

		}

		/// <summary>
		/// rendering the image rectangle by enum 
		/// <see cref="ImageSizeMode"/>.
		/// </summary>
		protected virtual void ImageSizeModeRender()
		{
			; // do nothing here.
		}

		/// <summary>
		/// Raises the <see cref="LeftClick"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnLeftClick()
		{
			Task.Run(() =>
			{
				// raise the event in another thread.
				this.LeftClick?.Invoke(this, null);
				this.Click?.Invoke(this, null);
			});
		}
		/// <summary>
		/// Raises the <see cref="RightClick"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnRightClick()
		{
			Task.Run((() =>
			{
				// raise the event in another thread.
				this.RightClick?.Invoke(this, null);
				this.Click?.Invoke(this, null);
			}));
		}
		/// <summary>
		/// Raises the <see cref="MouseEnter"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnMouseEnter()
		{
			Task.Run((() =>
			{
				// raise the event in another thread.
				this.MouseEnter?.Invoke(this, null);
			}));
		}
		/// <summary>
		/// Raises the <see cref="MouseLeave"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnMouseLeave()
		{
			Task.Run((() =>
			{
				// raise the event in another thread.
				this.MouseLeave?.Invoke(this, null);
			}));
		}
		/// <summary>
		/// Raises the <see cref="LeftDown"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnLeftDown()
		{
			if (BigFather.InputElement != this)
			{
				BigFather.DeactiveInputable();
			}
			Task.Run((() =>
			{
				// lock the mouse, so even if user changes
				// its mouse location with speed of light,
				// we change the location of this element.
				this.LockMouse();
				// Shock the element in another thread.
				this.Shocker();
				// raise the event in another thread.
				this.LeftDown?.Invoke(this, null);
			}));
		}
		/// <summary>
		/// Raises the <see cref="LeftUp"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnLeftUp()
		{
			Task.Run(() =>
			{
				// please unlock the mouse when user release
				// it's mouse, thanks; but if you don't 
				// unlock it, another graphic elements won't
				// trigger their own events like MouseEnter,
				// MouseDown, etc...
				this.UnLockMouse();
				// discharge the element in another thread.
				this.Discharge();
				// raise the event in another thread.
				this.LeftUp?.Invoke(this, null);
			});
		}
		/// <summary>
		/// Raises the <see cref="RightDown"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnRightDown()
		{
			if (BigFather.InputElement != this)
			{
				BigFather.DeactiveInputable();
			}
			Task.Run((() =>
			{
				// raise the event in another thread.
				this.RightDown?.Invoke(this, null);
			}));
		}
		/// <summary>
		/// Raises the <see cref="RightUp"/> event.
		/// </summary>
		/// <!--
		/// WARNING:
		///		Do NOT use Task.Run for this method!
		///		I've already used it for rasing the event handler in
		///		the method, so you should NOT use it for call this method!
		/// -->
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void OnRightUp()
		{
			Task.Run((() =>
			{
				// raise the event in another thread.
				this.RightUp?.Invoke(this, null);
			}));
		}
		/// <summary>
		/// check for mouse move.
		/// </summary>
		private void _checkMouseMove()
		{
			var p1 = BigFather.CurrentState.Position;
			var p2 = BigFather.LastMouseState.Position;
			if (p1 != p2)
			{
				Task.Run(() =>
				{
					// raise the event in another thread.
					this.MouseMove?.Invoke(this, null);
				});
			}
		}
		/// <summary>
		/// check for mouse click.
		/// </summary>
		private void _checkClick()
		{
			this._checkLeftDown();
			this._checkRightDown();
			// this._checkLeftClick(); moved to _checkLeftDown
			// this._checkRightClick();
		}
		/// <summary>
		/// check for left click.
		/// </summary>
		private void _checkLeftClick()
		{
			if (this.LeftDownOnce)
			{
				if (!this.IsLeftDown || !BigFather.IsLeftDown)
				{
					this.LeftDownOnce = false;
					var _p1 = BigFather.PreviousLeftDownPoint;
					var _p2 = BigFather.CurrentState.Position;
					if (_p1 == null)
					{
						return;
					}
					var _p3 = (_p1.Value - _p2).Abs();
					if (_p3.X < DivergeVector.X || _p3.Y < DivergeVector.Y)
					{
						// it means the left button of the mouse was clicked.
						this.OnLeftClick();
					}
				}
			}
		}
		/// <summary>
		/// check for right click.
		/// </summary>
		private void _checkRightClick()
		{
			if (this.RightDownOnce)
			{
				if (!this.IsRightDown)
				{
					this.RightDownOnce = false;
					var _p1 = BigFather.PreviousRightDownPoint;
					var _p2 = BigFather.CurrentState.Position;
					if (_p1 == null)
					{
						this.invalidOnce();
						return;
					}
					var _p3 = (_p1.Value - _p2).Abs();
					if (_p3.X < DivergeVector.X || _p3.Y < DivergeVector.Y)
					{
						// it means the left button of the mouse was clicked.
						this.OnRightClick();
					}
				}
			}
		}
		/// <summary>
		/// check for left down.
		/// </summary>
		private void _checkLeftDown()
		{
			if (BigFather.IsLeftDown && !this.IsLeftDown)
			{
				var _p = BigFather.LeftDownPoint;
				if (_p == null)
				{
					return;
				}
				if (_p != null && !this.Rectangle.Contains(_p.Value))
				{
					this.invalidOnce();
					return;
				}
				this.IsLeftDown = true;
				this.LeftDownOnce = true;
				this.OnLeftDown();
				return;
			}
			if (this.IsLeftDown)
			{
				if (!BigFather.IsLeftDown)
				{
					this.IsLeftDown = false;
					this.OnLeftUp();
					this._checkLeftClick();
				}
			}
		}
		/// <summary>
		/// check for right down.
		/// </summary>
		private void _checkRightDown()
		{
			if (BigFather.IsRightDown && !this.IsRightDown)
			{
				var _p = BigFather.RightDownPoint;
				if (_p == null)
				{
					return;
				}
				// check if the rectangle of this element contains current
				// point or not.
				if (!this.Rectangle.Contains(_p.Value))
				{
					return;
				}
				this.IsRightDown = true;
				this.RightDownOnce = true;
				this.OnRightDown();
				return;
			}
			if (this.IsRightDown)
			{
				if (!BigFather.IsRightDown)
				{
					this.IsRightDown = false;
					this.OnRightUp();
					this._checkRightClick();
				}
			}
		}
		/// <summary>
		/// invalid the current status.
		/// </summary>
		private void invalidOnce()
		{
			if (this.LeftDownOnce)
			{
				this.LeftDownOnce = false;
			}
			if (this.RightDownOnce)
			{
				this.RightDownOnce = false;
			}
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		/// <summary>
		/// check if the current mouse state of 
		/// the <see cref="BigFather"/> is in the region 
		/// of this element or not.
		/// </summary>
		/// <returns>true if it is in the region, 
		/// otherwise false.</returns>
		public virtual bool MouseIn()
		{
			bool inChild = false, me;
			if (this.Manager != null)
			{
				inChild = this.Manager.MouseContains();
			}
			me = this.Rectangle.Contains(BigFather.CurrentState.Position);
			return inChild || me;
		}
		public virtual bool WasMouseIn()
		{
			if (!this.Visible || !this.Enabled || !this.IsApplied)
			{
				return false;
			}
			return this.IsMouseIn || this.MouseIn();
		}
		public virtual bool ContainsChild(in IMoveable moveable)
		{
			if (moveable == this)
			{
				return true;
			}
			if (this.Manager != null)
			{
				if (moveable is GraphicElements _e)
				{
					return this.Manager.ContainsChild(_e);
				}
			}
			return false;
		}
		/// <summary>
		/// check if the mouse only is in the region of this element or not.
		/// </summary>
		/// <returns></returns>
		protected virtual bool MouseHere()
		{
			if (this.Manager != null)
			{
				if (this.Manager.MouseContains())
				{
					return false;
				}
			}
			return this.Rectangle.Contains(BigFather.CurrentState.Position);
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		/// <summary>
		/// set the status of the element.
		/// </summary>
		/// <param name="_status">
		/// the status of the element.
		/// this value is unsigned.
		/// </param>
		public virtual void SetStatus(in uint _status)
		{
			if (this.CurrentStatus != _status)
			{
				this.CurrentStatus = _status;
			}
		}
		/// <summary>
		/// Set the Label.Name Property with the Constant Parameter writed
		/// in ThereIsConstants.ResourcesNames.
		/// </summary>
		/// <param name="constParam">
		/// The Constant Parameter setted in <code> ThereIsConstants.ResourcesNames </code> 
		/// and in
		/// MainForm.MyRes.
		/// </param>
		public virtual void SetLabelName(in StrongString constParam)
		{
			this.RealName = constParam;
			this.Name = this.RealName +
				ThereIsConstants.ResourcesName.End_Res_Name;
		}
		/// <summary>
		/// This Method will set the Label.Text Property with the algorithm
		/// from MainForm.MyRes.
		/// </summary>
		public virtual void SetLabelText()
		{
			this.ChangeText(this.MyRes.GetString(
				this.MyRes.GetString(this.Name.GetValue()) +
				ThereIsConstants.ResourcesName.Separate_Character +
				ThereIsConstants.AppSettings.Language.ToString() +
				this.CurrentStatus.ToString()));
		}
		/// <summary>
		/// Setting the Text property to customValue.
		/// </summary>
		/// <param name="customValue">
		/// the custom Text.
		/// </param>
		public virtual void SetLabelText(in StrongString customValue)
		{
			ChangeText(customValue);
		}
		public virtual void SetOwner(in GraphicElements owner, in bool dont_add = false)
		{
			if (owner == null || owner.IsDisposed)
			{
				return;
			}
			if (this.Manager != null)
			{
				if (this.Manager.ContainsChild(owner))
				{
					return;
				}
			}
			if (this.HasOwner)
			{
				if (this.Owner == owner)
				{
					return;
				}
				else
				{
					this.Owner.Manager.Remove(this);
				}
			}
			this.Owner = owner;
			this.FatherLocation = this.Owner;
			this.HasOwner = true;
			if (owner is SandBoxElement)
			{
				this.HasSandBoxOwner = true;
			}
			if (this.Owner.Manager != null)
			{
				if (!dont_add)
				{
					this.Owner.Manager.Add(this);
				}
			}
		}
		public virtual void ChangeSize(in int w, in int h)
		{
			var _size = new Point(w, h);
			this.Rectangle = new Rectangle(this.Rectangle.Location, _size);
		}
		public virtual void ChangeSize(in float w, in float h)
		{
			ChangeSize((int)w, (int)h);
		}
		public virtual void ChangeLocation(in Vector2 location)
		{
			if (this.HasOwner)
			{
				this.RealPosition = location;
				this.Position = this.RealPosition + this.Owner.Position;
			}
			else
			{
				this.Position = location;
			}
			this.ChangeRectangle();
			this.Manager?.UpdateLocations();
		}
		public virtual void ChangeLocation(in float x, in float y)
		{
			if (this.HasOwner)
			{
				this.RealPosition = new(x, y);
				this.Position = this.Owner.Position + this.RealPosition;
			}
			else
			{
				this.Position = new(x, y);
			}
			this.ChangeRectangle();
			this.Manager?.UpdateLocations();
		}
		public virtual void ChangeLocation(in int x, in int y)
		{
			if (this.HasOwner)
			{
				if (this.Position.X != x || this.Position.Y != y)
				{
					this.RealPosition = new(x, y);
					this.Position = this.Owner.Position + this.RealPosition;
				}
			}
			else
			{
				if (this.Position.X != x || this.Position.Y != y)
				{
					this.Position = new(x, y);
				}
			}
			this.ChangeRectangle();
			this.Manager?.UpdateLocations();
		}
		public virtual void OwnerLocationUpdate()
		{
			if (this.HasOwner && this.Owner != null)
			{
				this.Position = this.Owner.Position + this.RealPosition;
				this.ChangeRectangle();
			}
		}
		/// <summary>
		/// change the priority of this very <see cref="GraphicElements"/>, 
		/// so it can be added in the right place of the manager.
		/// if this element has an owner, and if the <see cref="Manager"/> 
		/// of it's owner is not null, then it will remove itself
		/// from the manager (before changing the priority),
		/// and then it will change it's priority,
		/// after that, it will add itself to the manager 
		/// (if the add boolean is set to <c>true</c>).
		/// </summary>
		/// <param name="priority">
		/// the new priority of this element.
		/// </param>
		/// <param name="add">
		/// if you want to add this element to it's owner's manager, set this arg to 
		/// <c>true</c>, but please notice that if this element has no owner, 
		/// this element will add itself to the <see cref="BigFather"/>'s manager.
		/// but if you don't want this element add itself to any manager, then
		/// set this arg to false. the default value of this arg (as you can see) is
		/// <c>false</c>.
		/// </param>
		public virtual void ChangePriority(in ElementPriority priority, 
											in bool add = false)
		{
			if (this.Priority != priority)
			{
				if (this.HasOwner && this.Owner != null)
				{
					this.Owner.Manager?.Remove(this);
					this.Priority = priority;
					if (add)
					{
						this.Owner.Manager?.Add(this);
					}
				}
				else
				{
					// check if the static property,
					// BigFather, is null or not.
					// NOTICE: the BigFather will never be null,
					// but I add this if just-in-case.
					if (BigFather != null)
					{
						BigFather.ElementManager?.Remove(this);
						this.Priority = priority;
						if (add)
						{
							BigFather.ElementManager?.Add(this);
						}
					}
				}
			}
		}
		/// <summary>
		/// change the <see cref="ElementMovements"/> of this 
		/// <see cref="GraphicElements"/>.
		/// this method will set the <see cref="Movements"/> property of this
		/// element and will add this element to the passed-by second arg.
		/// </summary>
		/// <param name="movements">
		/// the movement of this element.
		/// </param>
		/// <param name="manager">
		/// if this value is null, 
		/// and is the <see cref="MoveManager"/> of this element is not null,
		/// we will remove the element from the manager.
		/// </param>
		public virtual void ChangeMovements(ElementMovements movements, in IMoveManager manager)
		{
			if (manager == null)
			{
				if (this.MoveManager == null)
				{
					this.MoveManager = new MoveManager(this);
					setMovements();
				}
				else
				{
					this.MoveManager?.Remove(this);
					this.Movements = ElementMovements.NoMovements;
				}
				return;
			}
			if (this.MoveManager != manager)
			{
				this.MoveManager?.Remove(this);
				manager.AddMe(this);
				this.MoveManager = manager;
			}
			setMovements();
			return;
			void setMovements()
			{
				if (this.Movements != movements)
				{
					this.Movements = movements;
				}
			}
		}
		/// <summary>
		/// change the fucking movements of this very fucking 
		/// <see cref="GraphicElements"/>, so it can be moved easily
		/// (very very fucking easily) by the player.
		/// <!--NOTICE: if -->
		/// </summary>
		/// <param name="movements"></param>
		public virtual void ChangeMovements(in ElementMovements movements)
		{
			if (movements == ElementMovements.NoMovements)
			{
				if (this.Movements != movements)
				{
					this.Movements = movements;
					return;
				}
			}
			this.ChangeMovements(movements, this.MoveManager);
		}
		public abstract void ChangeFont(in SpriteFontBase font);
		public virtual void ChangeForeColor(in XColor color)
		{
			if (this.ForeColor != color)
			{
				this.ForeColor = color;
			}
		}
		public virtual void ChangeBackColor(in XColor color)
		{
			if (this.BackGroundColor != color)
			{
				this.BackGroundColor = color;
			}
			var _t = this.GetBackGroundTexture(color);

			this.BackGroundImage = _t;
		}
		public virtual void ChangeImage()
		{
			this.ChangeImageContent(this.MyRes.GetString(
				this.MyRes.GetString(this.Name) + PIC_RES));
		}
		/// <summary>
		/// change the image of this element.
		/// </summary>
		/// <param name="texture">
		/// the image texture. this value can be passed as a null value.
		/// that way, the element won't show any image.
		/// </param>
		public virtual void ChangeImage(in Texture2D texture)
		{
			// just check if the texture is not disposed,
			// you don't have to check if the texture is null or not!
			if (!texture.IsDisposed)
			{
				// check if the current image is the same as the 
				// passed texture or not
				if (this.Image != texture)
				{
					// the last Image should NOT be disposed here!
					// if you are not a dumbass and wanna reduce the 
					// memory usage, you should dispose the image
					// before changing it!
					// this.Image?.Dispose();
					this.Image = texture;
					// render the rectangle of the image,
					// by specified enum parameter.
					this.ImageSizeModeRender();
				}
			}
		}
		/// <summary>
		/// change the image of this element with the 
		/// <see cref="BigFather"/>'s content.
		/// </summary>
		/// <param name="content_name"></param>
		public virtual void ChangeImageContent(in StrongString content_name)
		{
			if (Content != null)
			{
				this.ChangeImage(Content.Load<Texture2D>(content_name.GetValue()));
			}
		}
		public virtual void ChangeImageSizeMode(in ImageSizeMode mode)
		{
			if (this.ImageSizeMode != mode)
			{
				this.ImageSizeMode = mode;
				this.ImageSizeModeRender();
			}
		}
		public virtual void ChangeRectangle(in XRectangle rect)
		{
			this.ChangeLocation(rect.Location.ToVector2());
			this.ChangeSize(rect.Size.X, rect.Size.Y);
		}
		protected virtual void ChangeRectangle()
		{
			var location = new XPoint((int)this.Position.X, (int)this.Position.Y);
			var size = this.Rectangle.Size;
			this.Rectangle = new(location, size);
			if (this.Image != null)
			{
				this.ImageSizeModeRender();
			}
		}
		#endregion
		//-------------------------------------------------
		#region abstract Method's region
		/// <summary>
		/// Update this Graphic Element.
		/// </summary>
		/// <param name="gameTime"></param>
		public abstract void Update(GameTime gameTime);
		/// <summary>
		/// Change the text of this graphic element.
		/// </summary>
		/// <param name="text">
		/// the new text of this graphic element.
		/// if it's null, you won't get any exception.
		/// the text will be considered as an empty text and
		/// nothing will be displayed on the element.
		/// </param>
		public abstract void ChangeText(in StrongString text);
		
		/// <summary>
		/// Get a Background <see cref="Texture2D"/> by 
		/// specified <see cref="Color"/> for this 
		/// graphic element.
		/// </summary>
		/// <param name="color">
		/// the <see cref="Color"/> parameter.
		/// </param>
		/// <returns>
		/// a <see cref="Texture2D"/> with 
		/// the equal bound of this element.
		/// </returns>
		protected abstract Texture2D GetBackGroundTexture(XColor color);
		/// <summary>
		/// Updating the graphic parameters of this element.
		/// </summary>
		protected abstract void UpdateGraphics();
		#endregion
		//------------------------------------------------
		#region Graphical Method's Elements
		/// <summary>
		/// draw the surface of this graphic element.
		/// </summary>
		/// <param name="gameTime">
		/// the <see cref="GameTime"/> of the LTW.
		/// </param>
		/// <param name="spriteBatch">
		/// the <see cref="SpriteBatch"/> tool 
		/// which is necessary for drawing the graphic surface.
		/// </param>
		public abstract void Draw(in GameTime gameTime, in SpriteBatch spriteBatch);
		#endregion
		//-------------------------------------------------
	}
}
