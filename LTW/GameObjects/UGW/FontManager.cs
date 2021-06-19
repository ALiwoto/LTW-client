// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FontStashSharp;
using LTW.Client;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.Resources;

namespace LTW.GameObjects.UGW
{
	public sealed partial class FontManager : IRes, IDisposable
	{
		//-------------------------------------------------
		#region Constant's Region
		/// <summary>
		/// Old Story Bold File In Res.
		/// </summary>
		public const string OSBFileInRes		= "Old Story Bold";
		/// <summary>
		/// Old Story Bold Italic File In Res.
		/// </summary>
		public const string OSBIFileInRes		= "Old Story Bold Italic";
		/// <summary>
		/// LTWTT Bold File In Res.
		/// </summary>
		public const string LTWTTBoldFileInRes	= "LTWWelcomeTT-Bold";
		/// <summary>
		/// LTWTT Regular File In Res.
		/// </summary>
		public const string LTWTTRFileInRes		= "LTWWelcomeTT-Regular";
		/// <summary>
		/// Noto Sans Regular File In Res.
		/// </summary>
		public const string NSRFileInRes		= "NotoSansJP-Regular";
		public const int FontBitmapWidth				= 1024;
		public const int FontBitmapHeight			   	= 1024;
		public const int OLDSTORY_INDEX				 	= 0;
		public const int LTW_INDEX					  	= 1;
		public const int STROKE_AMOUNT					= 1;
		#endregion
		//-------------------------------------------------
		#region static Properties Region

		#endregion
		//-------------------------------------------------
		#region Properties Region
		public WotoRes MyRes { get; set; }
		public GameClient Client { get; }
		public int GraphicsLevel { get; }
		#endregion
		//-------------------------------------------------
		#region field's Region
		private CharacterRange[] _ranges;
		private FontSystem _old_story_bold;
		private FontSystem _old_story_bold_italic;
		private FontSystem _LTW_bold;
		private FontSystem _LTW_regular;
		private FontSystem _noto_sans_regular;
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		private FontManager(GameClient _client, int _level = 2)
		{
			Client			= _client;
			GraphicsLevel	= _level;
			InitializeComponents();
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		~FontManager()
		{
			Dispose();
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public SpriteFontBase GetSprite(LTW_Fonts _s_font, int size)
		{
			switch (_s_font)
			{
				case LTW_Fonts.LTW_tt_bold:
				{
					if (this._LTW_bold != null)
					{
						return this._LTW_bold.GetFont(size);
					}
					break;
				}
				case LTW_Fonts.LTW_tt_regular:
				{
					if (this._LTW_regular != null)
					{
						return this._LTW_regular.GetFont(size);
					}
					break;
				}
				case LTW_Fonts.old_story_bold:
				{
					if (this._old_story_bold != null)
					{
						return this._old_story_bold.GetFont(size);
					}
					break;
				}
				case LTW_Fonts.old_story_bold_italic:
				{
					if (this._old_story_bold_italic != null)
					{
						return this._old_story_bold_italic.GetFont(size);
					}
					break;
				}
				case LTW_Fonts.noto_sans_JP:
				{
					if (this._noto_sans_regular != null)
					{
						return this._noto_sans_regular.GetFont(size);
					}
					break;
				}
				default:
				{
					// we have to ensure everything goes well, 
					// so if there is a problem here, we should return a
					// default font (instead of null) 
					// which will act in an emergency situation.
					return _getDefault();
				}
			}
			return null;

			// local functions:
			SpriteFontBase _getDefault()
			{
				if (this._LTW_bold != null)
				{
					return this._LTW_bold.GetFont(size);
				}
				return null;
			}
		}
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		/// <summary>
		///	Generate a new FontManager.
		/// <summary>
		public static FontManager GenerateManager(GameClient _client)
		{
			// check if client is null or not.
			// if there is no client, we SHOULD NOT create a new font manager.
			if (_client == null)
			{
				// it means the client is not generated yet, so we should 
				// return null.
				return null;
			}
			// check if the client alread has another font manager or not.
			if (_client.FontManager != null)
			{
				// it means a font manager has already been created.
				// so return it instead of creating a new one.
				return _client.FontManager;
			}
			// check if the graphic device of the client
			// has been created or not, and is disposed or not.
			if (_client.GraphicsDevice == null || 
					_client.GraphicsDevice.IsDisposed)
			{
				// for creating fonts, we have to use GraphicsDevice
				// property of the GameClient, which is why it's important
				// for it to be present and not disposed.
				// at this rate we can't generate any font manager and will
				// return null.
				return null;
			}
			// create a new font manager object.
			// please do NOT set the properties and fields in here.
			return new FontManager(_client);
		}
		#endregion
		//-------------------------------------------------
	}
}
