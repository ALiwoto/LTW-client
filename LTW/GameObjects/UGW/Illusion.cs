// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LTW.Security;
using LTW.Constants;
using static LTW.Client.Universe;

namespace LTW.GameObjects.UGW
{
	/// <summary>
	/// Provides methods for graphic 2D drawing.
	/// </summary>
	public class Illusion
	{
		//-------------------------------------------------
		#region Constants Region
		
		#endregion
		//-------------------------------------------------
		#region field's Region
        /// <summary>
	    /// the texture of this illusion.
	    /// </summary>
		private Texture2D _texture;
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public byte[][] Data { get; private set; }
		public byte[] RawData { get; private set; }
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		private Illusion(in Texture2D _t)
		{
			_texture = _t;
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region

		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region

		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public Texture2D GetTexture2D()
		{
			return _texture;
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region

		#endregion
		//-------------------------------------------------
		#region static Method's Region
		/// <summary>
		/// Get an illusion by passed-by texture parameter
		/// as its component.
		/// </summary>
		/// <param name="_texture"> 
		/// the texture component.
		/// </param>
		public static Illusion GetIllusion(in Texture2D _texture)
		{
			if (_texture != null && !_texture.IsDisposed)
			{
				return new(in _texture);
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by texture parameter
		/// as its component with specified with and height.
		/// </summary>
		/// <param name="_texture"> 
		/// the texture component.
		/// </param>
		/// <param name="_w"> 
		/// the with.
		/// </param>
		/// <param name="_h"> 
		/// the height.
		/// </param>
		public static Illusion GetIllusion(in Texture2D _texture, 
											in int _w, in int _h)
		{
			if (_texture != null && _texture.IsDisposed)
			{
				var _rect = 
					new Rectangle(DEFAULT_Z_BASE, DEFAULT_Z_BASE, _w, _h);
				if (_texture.Bounds.Contains(_rect))
				{
					return GetIllusion(in _texture, in _rect);
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by texture parameter
		/// as its component with specified region.
		/// </summary>
		/// <param name="_texture"> 
		/// the texture component.
		/// </param>
		/// <param name="_rect"> 
		/// the region.
		/// </param>
		public static Illusion GetIllusion(in Texture2D _texture, 
											in Rectangle _rect)
		{
			if (_texture != null && !_texture.IsDisposed)
			{
				var client = ThereIsConstants.Forming.GameClient;
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					if (!_texture.Bounds.Contains(_rect))
					{
						return null;
					}
					var _t = new Texture2D(g, _rect.Width, _rect.Height);
					var _data = new Color[_rect.Width * _rect.Height];
					_texture.GetData(_data, DEFAULT_Z_BASE, _data.Length);
					_t.SetData(_data, DEFAULT_Z_BASE, _data.Length);
					return GetIllusion(in _t);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by path to texture parameter
		/// as its component.
		/// </summary>
		/// <param name="_full_path"> 
		/// the texture full path in the local storage.
		/// </param>
		public static Illusion GetIllusion(in StrongString _full_path)
		{
			if (_full_path == null)
			{
				return null;
			}
			var _b1 = !_full_path.IsHealthy();
			var _b2 = !File.Exists(_full_path.GetValue());
			if (_b1 || _b2)
			{
				return null;
			}
			var client = ThereIsConstants.Forming.GameClient;
			if (client != null && client.Verified)
			{
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					var _t = Texture2D.FromFile(g, _full_path.GetValue());
					return GetIllusion(in _t);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by path to texture parameter
		/// as its component with specified with and height.
		/// </summary>
		/// <param name="_full_path"> 
		/// the texture full path in the local storage.
		/// </param>
		/// <param name="_w"> 
		/// the with.
		/// </param>
		/// <param name="_h"> 
		/// the height.
		/// </param>
		public static Illusion GetIllusion(in StrongString _full_path, 
											in int _w, in int _h)
		{
			if (_full_path == null)
			{
				return null;
			}
			var _b1 = !_full_path.IsHealthy();
			var _b2 = !File.Exists(_full_path.GetValue());
			if (_b1 || _b2)
			{
				return null;
			}
			var client = ThereIsConstants.Forming.GameClient;
			if (client != null && client.Verified)
			{
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					var _t1 = Texture2D.FromFile(g, _full_path.GetValue());
					return GetIllusion(in _t1, in _w, in _h);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by path to texture parameter
		/// as its component with specified region.
		/// </summary>
		/// <param name="_full_path"> 
		/// the texture full path in the local storage.
		/// </param>
		/// <param name="_rect"> 
		/// the region.
		/// </param>
		public static Illusion GetIllusion(in StrongString _full_path, 
											in Rectangle _rect)
		{
			if (_full_path == null)
			{
				return null;
			}
			var _b1 = !_full_path.IsHealthy();
			var _b2 = !File.Exists(_full_path.GetValue());
			if (_b1 || _b2)
			{
				return null;
			}
			var client = ThereIsConstants.Forming.GameClient;
			if (client != null && client.Verified)
			{
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					var _t = Texture2D.FromFile(g, _full_path.GetValue());
					return GetIllusion(in _t, in _rect);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by stream.
		/// notice that the stream should be readable.
		/// </summary>
		/// <param name="_stream"> 
		/// the stream which contains the data for the texture component.
		/// </param>
		public static Illusion GetIllusion(in Stream _stream)
		{
			if (_stream == null || !_stream.CanRead)
			{
				return null;
			}
			var client = ThereIsConstants.Forming.GameClient;
			if (client != null && client.Verified)
			{
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					var _t = Texture2D.FromStream(g, _stream);
					return GetIllusion(in _t);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by stream with the specified width
		/// and height.
		/// notice that the stream should be readable.
		/// </summary>
		/// <param name="_stream"> 
		/// the stream which contains the data for the texture component.
		/// </param>
		/// <param name="_w"> 
		/// the width.
		/// </param>
		/// <param name="_h"> 
		/// the height.
		/// </param>
		public static Illusion GetIllusion(in Stream _stream, 
											in int _w, in int _h)
		{
			var client = ThereIsConstants.Forming.GameClient;
			if (client != null && client.Verified)
			{
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					var _t = Texture2D.FromStream(g, _stream);
					return GetIllusion(in _t, in _w, in _h);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by stream with the specified region.
		/// notice that the stream should be readable.
		/// </summary>
		/// <param name="_stream"> 
		/// the stream which contains the data for the texture component.
		/// </param>
		/// <param name="_rect"> 
		/// the region.
		/// </param>
		public static Illusion GetIllusion(in Stream _stream, 
											in Rectangle _rect)
		{
			var client = ThereIsConstants.Forming.GameClient;
			if (client != null && client.Verified)
			{
				var g = client.GraphicsDevice;
				if (g != null && !g.IsDisposed)
				{
					var _t = Texture2D.FromStream(g, _stream);
					return GetIllusion(in _t, in _rect);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Get an illusion by passed-by byte data.
		/// notice that the byte data is not the texture's data,
		/// it's the file data.
		/// </summary>
		/// <param name="_file_data"> 
		/// the byte data.
		/// </param>
		public static Illusion GetIllusion(in byte[] _file_data)
		{
			Stream m = new MemoryStream(_file_data);
			if (_file_data != null && m.CanRead)
			{
				return GetIllusion(in m);
			}
			return null;
		}

		/// <summary>
		/// Get an illusion by passed-by byte data with specified width
		/// and height.
		/// notice that the byte data is not the texture's data,
		/// it's the file data.
		/// </summary>
		/// <param name="_file_data"> 
		/// the byte data.
		/// </param>
		/// <param name="_w"> 
		/// the width.
		/// </param>
		/// <param name="_h"> 
		/// the height.
		/// </param>
		public static Illusion GetIllusion(in byte[] _file_data, 
											in int _w, in int _h)
		{
			var m = new MemoryStream(_file_data);
			if (m != null && m.CanRead)
			{
				return GetIllusion(m, in _w, in _h);
			}
			return null;
		}

		/// <summary>
		/// Get an illusion by passed-by byte data with specified region.
		/// notice that the byte data is not the texture's data,
		/// it's the file data.
		/// </summary>
		/// <param name="_file_data"> 
		/// the byte data.
		/// </param>
		/// <param name="_rect"> 
		/// the region.
		/// </param>
		public static Illusion GetIllusion(in byte[] _file_data, 
											in Rectangle _rect)
		{
			var m = new MemoryStream(_file_data);
			if (m != null && m.CanRead)
			{
				return GetIllusion(m, in _rect);
			}
			return null;
		}
		#endregion
		//-------------------------------------------------
	}
}
