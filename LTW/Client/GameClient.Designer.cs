// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WotoProvider.EventHandlers;
using WotoProvider.Enums;
using LTW.SandBox;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.UGW;
using LTW.Controls.Elements;
using LTW.GameObjects.Resources;
using LTW.SandBox.ErrorSandBoxes;
using Microsoft.Xna.Framework.Input;
//using FontStashSharp;
using LTW.Security;
using SColor = Microsoft.Xna.Framework.Color;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Point = Microsoft.Xna.Framework.Point;

namespace LTW.Client
{
	partial class GameClient
	{
		//-------------------------------------------------
		#region MainForm Region

		//-------------------------------------------------
		#region Initialize Region
		/// <summary>
		/// MainForm Initialize Component.
		/// </summary>
		private void MF_InitializeComponents()
		{
			try
			{
				// HttpListener _listener = new HttpListener();
				TcpClient _tcp = new TcpClient();
				NetworkStream _stream;
				_tcp.Connect("localhost", 37372);
				_stream = _tcp.GetStream();
				byte[] _b = new byte[512];
				var _result = _stream.Read(_b);
				var _str = System.Text.Encoding.UTF8.GetString(_b).TrimEnd();
				Console.WriteLine("got something! :" + _str + " - " + _str.Length + " - "+ _b.Length);
			}
			catch
			{
				
			}
			
#if BUTTON_TEST_1
			//---------------------------------------------
			//news:
			this.MyRes = new WotoRes(typeof(GameClient));
			this.FirstFlatElement = new FlatElement(this, ElementMovements.HorizontalMovements);
			ButtonElement test = new ButtonElement(this);
			FlatElement _f1 = new FlatElement(this, ElementMovements.VerticalHorizontalMovements);
			FlatElement _f2 = new FlatElement(this, ElementMovements.HorizontalMovements);
			this.LoadMFBackGround();
			//---------------------------------------------
			//names:
			this.FirstFlatElement.SetLabelName(FirstLabelNameInRes);

			//fontAndTextAligns:
			this.FirstFlatElement.ChangeFont(this.FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 19));
			test.ChangeFont(this.FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 19));
			_f1.ChangeFont(this.FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 19));
			_f2.ChangeFont(this.FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 19));
			
			this.FirstFlatElement.ChangeAlignmation(StringAlignmation.MiddleCenter);
			test.ChangeAlignmation(StringAlignmation.MiddleCenter);
			_f1.ChangeAlignmation(StringAlignmation.MiddleCenter);
			_f2.ChangeAlignmation(StringAlignmation.MiddleCenter);
			//priorities:
			this.FirstFlatElement.ChangePriority(ElementPriority.Normal);
			_f1.ChangePriority(ElementPriority.Normal);
			_f2.ChangePriority(ElementPriority.VeryHigh);
			test.ChangePriority(ElementPriority.High);
			//sizes:
			this.FirstFlatElement.ChangeSize(this.Width / 6, this.Height / 6);
			test.ChangeSize(150, 46);
			_f1.ChangeSize(200, 300);
			_f2.ChangeSize(100, 100);
			//ownering:
			_f2.SetOwner(_f1);
			//locations:
			this.FirstFlatElement.ChangeLocation((Width - FirstFlatElement.Width) -
				(2 * SandBoxBase.from_the_edge),
				(Height - FirstFlatElement.Height) - SandBoxBase.from_the_edge);

			test.ChangeLocation(100f, 100f);
			_f1.ChangeLocation(300f, 200f);
			_f2.ChangeLocation(10f, 10f);
			//movements:
			this.FirstFlatElement.ChangeMovements(ElementMovements.VerticalMovements);
			_f1.ChangeMovements(ElementMovements.VerticalHorizontalMovements);
			//colors:
			// this.FirstFlatElement.ChangeBackColor(SColor.Red);
			this.FirstFlatElement.ChangeForeColor(SColor.DarkSeaGreen);
			test.ChangeBorder(WotoProvider.Enums.ButtonColors.WhiteSmoke);
			_f1.ChangeBackColor(new SColor(SColor.Orange, 0.5f));
			_f2.ChangeBackColor(SColor.Blue);
			_f2.ChangeForeColor(new SColor(SColor.Red, 0.7f));
			//test.ChangeForeColor(SColor.Red);
			//enableds:
			test.EnableMouseEnterEffect();
			//texts:
			this.FirstFlatElement.SetLabelText();
			test.SetLabelText("Test");
			//_f1.SetLabelText("F1");
			_f2.SetLabelText("Flat2");
			//images:
			this.FirstFlatElement.ChangeImage();
			
			
			//applyAndShow:
			this.FirstFlatElement.Apply();
			this.FirstFlatElement.Show();
			_f2.Apply();
			_f2.Show();
			_f1.Apply();
			_f1.Show();

			test.Apply();
			test.Show();
			//events:
			this.GameUniverse.WotoPlanet.MouseDown += WotoPlanet_MouseDown;
			this.GameUniverse.WotoPlanet.MouseUp += WotoPlanet_MouseUp;
			this.Window.TextInput += Window_TextInput;
			//---------------------------------------------
			//addRanges:
			this.ElementManager.Add(this.FirstFlatElement);
			this.ElementManager.Add(test);
			this.ElementManager.Add(_f1);
			//---------------------------------------------
#endif
			//---------------------------------------------
			//news:
			this.MyRes = new WotoRes(typeof(GameClient));
			this.FirstFlatElement = new FlatElement(this, 
				ElementMovements.VerticalHorizontalMovements);
			ProfileWrongSandBox test = new();
			this.LoadMFBackGround();
			//---------------------------------------------
			//names:
			this.FirstFlatElement.SetLabelName(FirstLabelNameInRes);
			//fontAndTextAligns:
			this.FirstFlatElement.ChangeFont(this.FontManager.GetSprite(LTW_Fonts.LTW_tt_regular, 26));
			this.FirstFlatElement.ChangeAlignmation(StringAlignmation.MiddleCenter);
			//priorities:
			this.FirstFlatElement.ChangePriority(ElementPriority.Normal);
			//sizes:
			this.FirstFlatElement.ChangeSize(this.Width / 6, this.Height / 6);
			//ownering:
			//locations:
			this.FirstFlatElement.ChangeLocation((Width - FirstFlatElement.Width) -
				(2 * SandBoxBase.from_the_edge),
				(Height - FirstFlatElement.Height) - SandBoxBase.from_the_edge);
			//movements:
			//colors:
			this.FirstFlatElement.ChangeForeColor(SColor.DarkSeaGreen);
			//enableds:
			//texts:
			this.FirstFlatElement.SetLabelText();
			//images:
			this.FirstFlatElement.ChangeImage();
			//applyAndShow:
			this.FirstFlatElement.Apply();
			this.FirstFlatElement.Show();
			test.Apply();
			test.Show();
			//events:
			this.InitializeMainEvents();
			//---------------------------------------------
			//addRanges:
			this.ElementManager.AddRange(
				this.FirstFlatElement,
				test);
			//---------------------------------------------
			//finalBlow:
			//---------------------------------------------
		}
		/// <summary>
		/// add the main events.
		/// </summary>
		private void InitializeMainEvents()
		{
			//---------------------------------------------
			this.GameUniverse.MouseDown		-= WotoPlanet_MouseDown;
			this.GameUniverse.MouseUp		-= WotoPlanet_MouseUp;
			this.Window.TextInput			-= Window_TextInput;
			this.GameUniverse.MouseDown		+= WotoPlanet_MouseDown;
			this.GameUniverse.MouseUp		+= WotoPlanet_MouseUp;
			this.Window.TextInput			+= Window_TextInput;
			//---------------------------------------------
			#if !SETVER_TEST
			System.Net.Http.HttpClient test = new System.Net.Http.HttpClient();
			test.BaseAddress = new Uri("https://ltw-game.herokuapp.com");
			System.Net.Http.HttpRequestMessage ro = new System.Net.Http.HttpRequestMessage();
			ro.Headers.Add("test", "    HELLO!!!!!!!");
			var _res = test.Send(ro);

			Stream receiveStream = _res.Content.ReadAsStream();
			StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
			var text1 = readStream.ReadToEnd();

			ro = new System.Net.Http.HttpRequestMessage();
			_res = test.Send(ro);

			receiveStream = _res.Content.ReadAsStream();
			readStream = new StreamReader(receiveStream, Encoding.UTF8);
			var text2 = readStream.ReadToEnd();
			#endif
		}
		/// <summary>
		/// Load the Main Form Background of the game.
		/// </summary>
		/// <param name="_loading"></param>
		private void LoadMFBackGround(bool _loading = true)
		{
			if (_loading)
			{
				this.BackGroundTexture?.Dispose();
				var _num = DateTime.Now.Second % EntryCount;
				var _name = EntryPicNameInRes + _num.ToString();
				var _b = (byte[]) this.MyRes.GetObject(_name);
				using (var m = new MemoryStream(_b))
				{
					this.BackGroundTexture = Texture2D.FromStream(GraphicsDevice, m);
				}
			}
			else
			{
				// Not Completed
				this.BackGroundTexture?.Dispose();
				using (var m = this.MyRes.GetStream(AincradNameInRes))
				{
					this.BackGroundTexture = Texture2D.FromStream(GraphicsDevice, m);
				}
			}
			
		}
		#endregion
		//-------------------------------------------------

		#endregion
		//-------------------------------------------------
		#region GameClient Region

		#endregion
		//-------------------------------------------------
		#region overrided Method's Region
		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			ThereIsConstants.Forming.GameClient = this;
			this.FontManager = FontManager.GenerateManager(this);
			this.ElementManager = new ElementManager();
			this.MF_InitializeComponents();
			base.Initialize();
			// check if the game window position is zero or not.
			if (Window.Position != Point.Zero)
			{
				// set the game window position to zero.
				Window.Position = Point.Zero;
			}
#if __LINUX__
			// check if game universe have to check the 
			// __mmf__ assist file or not.
			if (!this.GameUniverse._checkFile)
			{
				this.GameUniverse._checkFile = true;
			}
#endif
			// game should always be in the fullscreen mode.
			this.GraphicsDM.ToggleFullScreen();
		}


		//private FontSystem Fonts;
		//private SpriteFont test;
		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			//---------------------------------------------
			//news:
			this.SpriteBatch = new SpriteBatch(GraphicsDevice);
			//---------------------------------------------
		}
		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			//---------------------------------------------
			//---------------------------------------------
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				this.Exit();
			}
			// update the game universe, so it can handle its own events.
			this.GameUniverse?.UpdateUniverse();
			// check the requests came from outside of the envinment of the Game. 
			this.CheckRequests();
			// check mouse actions of the elements.
			this.MouseActions();
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			this.GraphicsDevice.Clear(SColor.Black);
			this.DrawBackGround();
			this.ElementManager?.Draw(gameTime, this.SpriteBatch);
			base.Draw(gameTime);
		}
		#endregion
		//-------------------------------------------------
		#region Background Method's Region
		/// <summary>
		/// allow the <see cref="GameClient"/> to draw its background.
		/// </summary>
		private void DrawBackGround()
		{
			if (this.BackGroundTexture == null || this.BackGroundTexture.IsDisposed)
			{
				return;
			}
			this.SpriteBatch.Begin();
			this.SpriteBatch.Draw(this.BackGroundTexture, this.GameUniverse.XRectangle, 
				this.BackGroundTexture.Bounds, 
				SColor.White);
			this.SpriteBatch.End();
		}
		#endregion
		//-------------------------------------------------
		#region Odrinary Method's Region
		private void CheckRequests()
		{
			// chec if there is a request from the universe or not.
			if (this.Universe_Request)
			{
				switch (Request)
				{
					case RequestType.None:
						break;
					case RequestType.Activate:
					{
						try
						{
							this.Universe_Request = false;
							this.Request = RequestType.None;
							// this.GameUniverse.WotoPlanet?.Show();
							// this.GameUniverse.WotoPlanet?.BringToFront();
							// this.GameUniverse.WotoPlanet?.Activate();
							// this.GameUniverse.WotoPlanet?.Focus();
							this.GraphicsDM.ToggleFullScreen();
						}
						catch
						{
							// the activating was not successful,
							// so what should we do??
							// here was the last step that we could avtive the
							// holy planet of woto, but it failed, 
							// it means there is no further steps.
							// so the story will end right here right now.
						}
						break;
					}
					default:
						break;
				}

			}
		}
		private void MouseActions()
		{
			this.LastMouseState = this.CurrentState;
			this.CurrentState = Mouse.GetState();
			if (this.ElementManager != null)
			{
				if (this.ElementManager.MouseContains())
				{
					this.ElementManager.MouseChange();
				}
			}
		}
		#endregion
		//-------------------------------------------------
		#region event Method's Region
		private void WotoPlanet_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.IsLeftDown)
				{
					this.LeftDownPoint = null;
					this.IsLeftDown = false;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (this.IsRightDown)
				{
					this.RightDownPoint = null;
					this.IsRightDown = false;
				}
			}
		}

		private void WotoPlanet_MouseDown(object sender, MouseEventArgs e)
		{
			
			if (e.Button == MouseButtons.Left)
			{
				if (!this.IsLeftDown)
				{
					this.LeftDownPoint = this.CurrentState.Position;
					this.IsLeftDown = true;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (!this.IsRightDown)
				{
					this.RightDownPoint = this.CurrentState.Position;
					this.IsRightDown = true;
				}
			}
		}

		private void Window_TextInput(object sender, TextInputEventArgs e)
		{
			this.FirstFlatElement.ChangeText(
				this.FirstFlatElement.Text.Append(e.Character, true));
		}
		#endregion
		//-------------------------------------------------
	}
}
