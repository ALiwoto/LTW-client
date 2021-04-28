// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WotoProvider.Enums;
using WotoProvider.EventHandlers;
using LTW.Controls;
using LTW.Security;
using LTW.Constants;
#if __WINDOWS__
using LTW.Controls.Workers;
#endif
using XPoint = Microsoft.Xna.Framework.Point;
using XRectangle = Microsoft.Xna.Framework.Rectangle;

namespace LTW.Client
{
	/// <summary>
	/// Window Manager of the LTW.
	/// </summary>
	public sealed class Universe
	{
		//-------------------------------------------------
		#region Constant's Region
		/// <summary>
		/// windows manager of the LTW world.
		/// </summary>
		public const string ToStringValue = "UNIVERSE_MANAGER -- LTW";
		public const string UnknownCommandError = "ERR: Unknown Command Entered: ";
		public const string Config_File_Name = 
			"UNIVERSE_CONFIG" + ThereIsConstants.Path.FilesEnd_Name;
#if __WINDOWS__
		public const string Config_Filter = 
			"*" + ThereIsConstants.Path.FilesEnd_Name;
#endif
		public const string DEFAULT_CONFIG_TEXT = "-- LTW UNIVERSE -- V1.0.0";
		public const string CONFIG_UP_COMMAND = "universe_request(up)";
		public const string CONFIG_CLOSE_COMMAND1 = "close";
		public const string CONFIG_CLOSE_COMMAND2 = "close()";
		public const string CONFIG_CLOSE_COMMAND3 = "close(force)";
		public const string CONFIG_CLOSE_COMMAND4 = "close() --f";
		public const string CONFIG_CLOSE_COMMAND5 = "/close";
		public const string CONFIG_CLOSE_COMMAND6 = "/close --f";
		public const string CONFIG_CLOSE_COMMAND7 = "/close -force";
		public const int DEFAULT_Z_BASE		   = 0b0;
		public const int DEFAULT_A_BASE		   = 0b1;
		public const int DEFAULT_B_BASE		   = 0b10;
#if __LINUX__
		public const int WORKER_INTERVAL = 0b111110100;
#endif
		#endregion
		//-------------------------------------------------
		#region Properties Region
		/// <summary>
		/// Handle of the Windows.
		/// </summary>
		public IntPtr Handle { get; }
		/// <summary>
		/// the main window of the GameClient,
		/// casted to the c# window currency (WindowsForm).
		/// </summary>
		public GameWindow WotoPlanet { get; }
		public GameClient Client { get; }
#if __LINUX__
		public Trigger WatcherWorker { get; private set; }
#elif __WINDOWS__
		public Worker WatcherWorker { get; private set; }
#endif
		public FileSystemWatcher Watcher { get; private set; }
		public StrongString ConfigPath { get; private set; }
		public StrongString ConfigDir { get; private set; }
		public StrongString LastCommand { get; private set; }
		public XRectangle XRectangle { get; }
		public int Width
		{
			get
			{
				if (Client != null)
				{
					return Client.GraphicsDM.PreferredBackBufferWidth;
				}
				return DEFAULT_Z_BASE;
			}
		}
		public int Height
		{
			get
			{
				if (Client != null)
				{
					return Client.GraphicsDM.PreferredBackBufferHeight;
				}
				return DEFAULT_Z_BASE;
			}
		}
		public int X
		{
			get
			{
				if (WotoPlanet != null)
				{
					return WotoPlanet.Position.X;
				}
				return DEFAULT_Z_BASE;
			}
		}
		public int Y
		{
			get
			{
				if (WotoPlanet != null)
				{
					return WotoPlanet.Position.Y;
				}
				return DEFAULT_Z_BASE;
			}
		}
		/// <summary>
		/// use this to check if the operation of the 
		/// getting and setting of the planet handle was succesful 
		/// or not,
		/// and if it's completed or not.
		/// </summary>
		public bool Completed { get; }
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		public static bool IsWindows { get; set; }
		public static bool IsUnix { get; set; }
		internal static FileStream _mapped { get; set; }
		internal bool _checkFile { get; set; }
		#endregion
		//-------------------------------------------------
		#region field's Region
		private bool _left_pressed;
		private bool _right_pressed;
#if __LINUX__
		private readonly string __mmf__;
#endif
		#endregion
		//-------------------------------------------------
		#region Events Region
		internal event MouseEventHandler MouseDown;
		internal event MouseEventHandler MouseUp;
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		/// <summary>
		/// create a new instance of <see cref="Universe"/> 
		/// for LTW Game.
		/// <!--BY: ALi.w-->
		/// </summary>
		/// <param name="_handle_">
		/// the handler.
		/// </param>
		/// <param name="_client_">
		/// the client.
		/// </param>
		public Universe(IntPtr _handle_, GameClient _client_)
		{
			Handle = _handle_;
			Client = _client_;
			WotoPlanet = _client_.Window;
			SetBorder();
			SetLocation();
			SetWatcher();
			var w = Client.GraphicsDM.PreferredBackBufferWidth;
			var h = Client.GraphicsDM.PreferredBackBufferHeight;
			XRectangle = new XRectangle(X, Y, w, h);
			WotoPlanet.Position = Point.Zero;
#if __LINUX__
			__mmf__ = ThereIsConstants.Path.Here + 
						  Program.MMF_NAME;  
#endif
			Completed = true;
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		private void SetBorder()
		{
			WotoPlanet.IsBorderless = true;
		}
		private void SetLocation()
		{
			WotoPlanet.Position = new XPoint(0, 0);
		}
		private void SetWatcher()
		{
			if (ConfigPath == null)
			{
				ConfigDir = ThereIsConstants.Path.Here;
				ConfigPath = ConfigDir + Config_File_Name;
			}
			if (!File.Exists(ConfigPath.GetValue()))
			{
				var _f = File.Create(ConfigPath.GetValue());
				_f.Dispose();
			}
			File.WriteAllText(ConfigPath.GetValue(), DEFAULT_CONFIG_TEXT);
#if __LINUX__
			WatcherWorker ??= new Trigger();
			WatcherWorker.Tick += Watcher_Worker;
			WatcherWorker.SetInterval(WORKER_INTERVAL);
			WatcherWorker.Start();
#elif __WINDOWS__
			// create a new worker and start it up.
			// ReSharper disable once RedundantArgumentDefaultValue
			WatcherWorker = new Worker(Watcher_Worker, DEFAULT_Z_BASE);
			WatcherWorker.Start();
#endif
		}
		#endregion
		//-------------------------------------------------
		#region Worker Method's Region
#if __LINUX__

		private void Watcher_Worker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
		{
			if (_checkFile)
			{
				__checkFiles__();   
			}
			else
			{
				if (sender != this.WatcherWorker)
				{
					sender?.Stop();
					sender?.Dispose();
				}
				this.WatcherWorker?.Stop();
				this.WatcherWorker?.Dispose();
			}
		}

#elif __WINDOWS__
		private void Watcher_Worker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
		{
			Watcher ??= new FileSystemWatcher(ConfigDir.GetValue(), Config_Filter)
			{
				EnableRaisingEvents = true
			};
			Watcher.Changed -= Watcher_Changed;
			Watcher.Changed += Watcher_Changed;
		}
#endif

		private void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			if (e.ChangeType == WatcherChangeTypes.Changed)
			{
				try
				{
					LastCommand = File.ReadAllText(e.FullPath);
					switch (LastCommand.ToLower().GetValue())
					{
						case CONFIG_CLOSE_COMMAND1:
						case CONFIG_CLOSE_COMMAND2:
						case CONFIG_CLOSE_COMMAND3:
						case CONFIG_CLOSE_COMMAND4:
						case CONFIG_CLOSE_COMMAND5:
						case CONFIG_CLOSE_COMMAND6:
						case CONFIG_CLOSE_COMMAND7:
							Close();
							break;
						case CONFIG_UP_COMMAND:
							ComeOnUp();
							break;
						default:
							// it means the command is unknown to us.
							Debug.Print( UnknownCommandError +
										 LastCommand.GetValue());
							break;
					}
				}
				catch (Exception _e)
				{
					// it means there was an error here ..
					// so, never mind it and just continue the game :|
					Debug.Print(_e.Message);
					return;
				}
			}
		}
		#endregion
		//-------------------------------------------------
		#region Ordinary Method's Region
		/// <summary>
		/// come on up!
		/// for activating the holy planet of woto,
		/// after a non-single-one proccess of game has runned.
		/// </summary>
		public void ComeOnUp()
		{
			if (WotoPlanet == null)
			{
				return;
			}
			// try if we can access to the cross-threading holy planet of woto
			// or not.
			try
			{
				
			}
			catch
			{
				// it means the access here is denied!
				// so send a request to the client,
				// which is definitely on the main thread,
				// so it has access and the request will be done.
				Client.Request = RequestType.Activate;
				Client.Universe_Request = true;
			}
		}
		public void Close()
		{
			if (!this.Completed)
			{
				return;
			}
			Watcher?.Dispose();
			Client?.Exit();
		}
		/// <summary>
		/// Call this method in <see cref="GameClient.Update(GameTime)"/>.
		/// </summary>
		internal void UpdateUniverse()
		{
			var _state = Mouse.GetState();
			checkMouseButtons(MouseButtons.Left, _state);
			checkMouseButtons(MouseButtons.Right, _state);
		}
		/// <summary>
		/// check the MouseButton states.
		/// </summary>
		/// <param name="_b"></param>
		/// <param name="_state"></param>
		private void checkMouseButtons(MouseButtons _b, MouseState _state)
		{
			switch (_b)
			{
				case MouseButtons.Left:
					if (_state.LeftButton == ButtonState.Pressed)
					{
						if (!_left_pressed)
						{
							_left_pressed	  = true;
							var _cr = ThereIsConstants.AppSettings.WotoCreation;
							var _arg		   = new MouseEventArgs(_cr, _b);
							MouseDown?.Invoke(this, _arg);
						}
					}
					else
					{
						if (_left_pressed)
						{
							_left_pressed	  = false;
							var _cr = ThereIsConstants.AppSettings.WotoCreation;
							var _arg		   = new MouseEventArgs(_cr, _b);
							MouseUp?.Invoke(this, _arg);
						}
					}
					break;
				case MouseButtons.Right:
					if (_state.RightButton == ButtonState.Pressed)
					{
						if (!_right_pressed)
						{
							_right_pressed	 = true;
							var _cr = ThereIsConstants.AppSettings.WotoCreation;
							var _arg		   = new MouseEventArgs(_cr, _b);
							MouseDown?.Invoke(this, _arg);
						}
					}
					else
					{
						if (_right_pressed)
						{
							_right_pressed	  = false;
							var _cr  = ThereIsConstants.AppSettings.WotoCreation;
							var _arg			= new MouseEventArgs(_cr, _b);
							MouseUp?.Invoke(this, _arg);
						}
					}
					break;
				// ReSharper disable once RedundantEmptySwitchSection
				default:
					/*
					 * TODO: in the future you have to add more options.
					 */
					return;
			}
			
		}
#if __LINUX__
		/// <summary>
		/// check the <see cref="__mmf__"/> file.
		/// do NOT make this method public.
		/// </summary>
		private void __checkFiles__()
		{
			// check if the __mmf__ exists or not.
			if (!File.Exists(__mmf__))
			{
				// it means that the __mmf__ file has been deleted.
				// so you have to close the game!
				Client.Exit();
			}
		}
#endif		
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		/// <summary>
		/// send a request to another existing universe for 
		/// activating their woto planet.
		/// </summary>
		public static void Universe_Request()
		{
			var path = Directory.GetCurrentDirectory() + Config_File_Name;
			File.WriteAllText(path, CONFIG_UP_COMMAND);
		}
		/// <summary>
		/// pre-setup the universe of the game. 
		/// </summary>
		internal static void SetUpUniverse()
		{
			var _p	= Environment.OSVersion.Platform;
			IsWindows =	_p	== PlatformID.Win32NT;
			IsUnix =	_p	== PlatformID.Unix;
		}
		#endregion
		//-------------------------------------------------
	}
}
