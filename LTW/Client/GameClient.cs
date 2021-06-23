// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using LTW.Controls;
using LTW.Security;
using LTW.Constants;
using LTW.GameObjects.UGW;
using LTW.Controls.Elements;
using LTW.GameObjects.Resources;

namespace LTW.Client
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public sealed partial class GameClient : Game, IRes
	{
		//-------------------------------------------------
		#region Constant's Region
		public const string ToStringValue = "-- LTW GAME CLIENT --";
		public const string AincradNameInRes = "Aincrad";
		public const string EntryPicNameInRes = "SwordEntry";
		public const string FirstLabelNameInRes = "Label1";
		/// <summary>
		/// This is 8.
		/// </summary>
		public const uint EntryCount = 0b1000;
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public WotoRes MyRes { get; set; }
		/// <summary>
		/// Graphics Device Manager of the LTW GameClient.
		/// </summary>
		public GraphicsDeviceManager GraphicsDM { get; private set; }
		/// <summary>
		/// Sprite Batch of the LTW GameClient.
		/// </summary>
		public SpriteBatch SpriteBatch { get; private set; }
		/// <summary>
		/// The Universe of the LTW Game.
		/// </summary>
		public Universe GameUniverse { get; }
		/// <summary>
		/// The Font Manager of the game.
		/// </summary>
		public FontManager FontManager { get; private set; }
		public ElementManager ElementManager { get; private set; }
		/// <summary>
		/// the First FlatElement, which shows the player:
		/// "Connecting... " and "Checking for Updates..."
		/// </summary>
		public FlatElement FirstFlatElement { get; private set; }
		public Texture2D BackGroundTexture { get; private set; }
		internal RequestType Request { get; set; }
		internal MouseState LastMouseState { get; private set; }
		internal IInputable InputElement { get; private set; }
		internal MouseState CurrentState { get; private set; }
	#nullable enable
		internal Point? LeftDownPoint { get; private set; }
		internal Point? PreviousLeftDownPoint { get; private set; }
		internal Point? RightDownPoint { get; private set; }
		internal Point? PreviousRightDownPoint { get; private set; }
	#nullable disable
		//public SoundPlayer SoundPlayer { get; set; }
		public int Width
		{
			get
			{
				if (GameUniverse != null)
				{
					return GameUniverse.Width;
				}
				return Universe.DEFAULT_Z_BASE;
			}
		}
		public int Height
		{
			get
			{
				if (GameUniverse != null)
				{
					return GameUniverse.Height;
				}
				return Universe.DEFAULT_Z_BASE;
			}
		}
		internal bool IsLeftDown { get; private set; }
		internal bool IsRightDown { get; private set; }
		public bool Verified { get; set; }
		public bool IsConnecting { get; set; }
		public bool IsShowingSandBox { get; set; }
		public bool IsCheckingForUpdate { get; set; }
		public bool IsCheckingForUpdateEnded { get; set; }
		//public bool IsTheLastVer { get; set; }
		public bool IsServerOnBreak { get; set; }
		public bool IsServerUpdating { get; set; }
		public bool IsServerOnline { get; set; }
		public bool IsTheFirstTime { get; set; }
		public bool IsCtrlDown { get; private set; }
		/// <summary>
		/// NOTE: this value is just a bool for the DateTimeWorker
		/// </summary>
		public bool MainMenuLoaded { get; set; }
		public bool Universe_Request { get; set; }
		public StrongString ReleasingDate { get; set; } = null;
		#endregion
		//-------------------------------------------------
		#region field's Region

		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		public GameClient(bool verify)
		{
			if (!verify)
			{
				return;
			}
			try
			{
				GraphicsDM = new GraphicsDeviceManager(this)
				{
					PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
					PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
				};
				IsMouseVisible = true;
				GameUniverse = new Universe(Window.Handle, this);
				Content.RootDirectory = ThereIsConstants.Path.Content;
			}
			catch (NoSuitableGraphicsDeviceException ex)
			{
				Verified = false;
				Console.WriteLine(ex.Message);
				return;
			}
			catch (Exception ex)
			{
				Verified = false;
				Console.WriteLine("Another exception:\n " + ex.Message);
				return;
			}
			//---------------------------------------------
			IsConnecting				= true;
			IsShowingSandBox			= false;
			IsCheckingForUpdate		 = false;
			IsCheckingForUpdateEnded	= false;
			//IsTheLastVer				= false;
			IsServerOnBreak			 = false;
			IsServerUpdating			= false;
			IsServerOnline			  = false;
			IsTheFirstTime			  = false;
			MainMenuLoaded			  = false;
			ReleasingDate			   = null;
			//---------------------------------------------
			Verified = verify;

		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		
		#endregion
		//-------------------------------------------------
	}
}
