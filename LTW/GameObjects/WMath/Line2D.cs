// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using Microsoft.Xna.Framework;

namespace LTW.GameObjects.WMath
{
	public class Line2D
	{
		//-------------------------------------------------
		#region Constants Region
		
		#endregion
		//-------------------------------------------------
		#region Properties Region
        public Line2DStatus Status { get; protected set; }
		public int X1 { get; protected set; }
		public int Y1 { get; protected set; }
		public int X2 { get; protected set; }
		public int Y2 { get; protected set; }
		public double A { get; protected set; }
		public double B { get; protected set; }
		public bool IsPoint
		{
			get
			{
				return X1 == X2 && Y1 == Y2;
			}
		}
		public bool IsFunc
		{
			get
			{
				// if x1 is equal with x2, it means that this line
				// is not act as a function.
				// it also can be a point (if and only if y1 == y2),
				// but it doesn't matter for us, because we won't consider 
				// a point as a function.
				// if you want to determine this line is a point or not,
				// use property `IsPoint { get; }` .
				return X1 != X2;
			}
		}
		public bool IsLimited
		{
			get 
			{
				return Status == Line2DStatus.BothLimited;
			}
		}
		public bool IsUnlimited
		{
			get 
			{
				return !IsLimited;
			}
		}
		#endregion
		//-------------------------------------------------
		#region field's Region
		
		#endregion
		//-------------------------------------------------
		#region Constructor's Region
		private Line2D(in int x1, in int y1, in int x2, in int y2)
		{

		}
		private Line2D(in Point p1, in Point p2)
		{
			
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		
		#endregion
		//-------------------------------------------------
		#region ordinary Method's Region
		



		private void _invalidFormula()
		{
			double _n = Y2 - Y1;
			double _m = X2 - X1;
			A = _n / _m;
			_m = X1 * A;
			B = Y1 - _m;
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public Rectangle GetRectangle()
		{
			
			return default;
		}
		public bool[][] GetRaw()
		{
			// TODO
			return null;
		}
		public bool[][] GetRaw(in int x0, in int y0, in int w, in int h)
		{
			return null;
		}
		public bool ContainDomain(in int x)
		{
			// TODO: impliment it.
			return false;
		}
		public int GetY(in int x)
		{
			if (!ContainDomain(in x))
			{
				return default;
			}
			return default;
		}
		public Point GetPoint(in int x)
		{
			if (!ContainDomain(in x))
			{
				return Point.Zero;
			}
			return new(x, GetY(in x));
		}
		public Vector2 GetVector2(in int x)
		{
			if (!ContainDomain(in x))
			{
				return Vector2.Zero;
			}
			return new(x, GetY(in x));
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		public void ChangeStatus(in Line2DStatus _status)
		{
			if (Status != _status)
			{
				Status = _status;
			}
		}
		#endregion
		//-------------------------------------------------
		#region static Method's Region
		public static Line2D NewLine2D(in int x1, in int y1, 
											in int x2, in int y2)
		{
			return null;
		}
		public static Line2D NewLine2D(in Point p1, in Point p2)
		{
			return null;
		}
		#endregion
		//-------------------------------------------------
	}
}
