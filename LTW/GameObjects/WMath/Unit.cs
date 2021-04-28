// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using LTW.Client;
using LTW.Security;

namespace LTW.GameObjects.WMath
{
#pragma warning disable IDE0047
#pragma warning disable IDE0017
	public sealed class Unit
	{
		//-------------------------------------------------
		#region Constants Region
		public const int BASE_INT					= Universe.DEFAULT_Z_BASE;
		public const ushort TheMaxValueOfEachUnit	= 1000;
		public const string CharSeparater			= "う";
		public const string FullTosStringSeparater	= ", ";
		public const string FullUnitFormatConvertor	= "000";
		public const string NegetiveStringValue		= "- ";
		public const string NegetiveSerialized		= "-";
		public const string PossitiveSerialized		= "+";
		public const string BilStringValue			= " B";
		public const string MilStringValue			= " M";
		public const string KilStringValue			= " K";
		#endregion
		//-------------------------------------------------
		#region Private fields Region
		private ushort bil;
		private ushort mil;
		private ushort kil;
		private ushort nil;
		#endregion
		//-------------------------------------------------
		#region Properties Region
		/// <summary>
		/// The Billion unit.
		/// </summary>
		public ushort Bil
		{
			get
			{
				return bil;
			}
			private set
			{
				if(value != 0)
				{
					bil = value;
				}
				else
				{
					bil = 0;
				}
			}
		}
		/// <summary>
		/// The Million unit.
		/// </summary>
		public ushort Mil
		{
			get
			{
				return mil;
			}
			private set
			{
				if(value >= TheMaxValueOfEachUnit)
				{
					Bil += Convert.ToUInt16(value / TheMaxValueOfEachUnit);
					mil = Convert.ToUInt16(value % TheMaxValueOfEachUnit);
				}
				else
				{
					mil = value;
				}
			}
		}
		/// <summary>
		/// The Kilo unit.
		/// </summary>
		public ushort Kil
		{
			get
			{
				return kil;
			}
			private set
			{
				if(value >= TheMaxValueOfEachUnit)
				{
					Mil += Convert.ToUInt16(value / TheMaxValueOfEachUnit);
					kil = Convert.ToUInt16(value % TheMaxValueOfEachUnit);
				}
				else
				{
					kil = value;
				}
			}
		}
		/// <summary>
		/// The Ordinary unit. (Primary)
		/// </summary>
		public ushort Nil
		{
			get
			{
				return nil;
			}
			private set
			{
				if(value >= TheMaxValueOfEachUnit)
				{
					Kil += Convert.ToUInt16(value / TheMaxValueOfEachUnit);
					nil = Convert.ToUInt16(value % TheMaxValueOfEachUnit);
				}
				else
				{
					nil = value;
				}
			}
		}
		/// <summary>
		/// if this unit is negative, this property will be true,
		/// otherwise, it will be false.
		/// </summary>
		public bool IsNegative { get; private set; }
		#endregion
		//-------------------------------------------------
		#region Constructors Region
		public Unit()
		{
			; // nothing
		}
		public Unit(ushort nilValue)
		{
			Nil = nilValue;
		}
		public Unit(ushort kilValue, ushort nilValue)
		{
			Kil = kilValue;
			Nil = nilValue;
		}
		public Unit(ushort milValue, ushort kilValue, ushort nilValue)
		{
			Mil = milValue;
			Kil = kilValue;
			Nil = nilValue;
		}
		public Unit(ushort bilValue, ushort milValue, ushort kilValue, ushort nilValue)
		{
			Bil = bilValue;
			Mil = milValue;
			Kil = kilValue;
			Nil = nilValue;
		}
		public Unit(Unit baseUnit)
		{
			Bil = baseUnit.Bil;
			Mil = baseUnit.Mil;
			Kil = baseUnit.Kil;
			Nil = baseUnit.Nil;
			IsNegative = baseUnit.IsNegative;
		}
		#endregion
		//-------------------------------------------------
		#region Get Method's Region
		public ulong ConvertToInt()
		{
			ulong myLong = 0;
			myLong += (ulong)(Bil * (System.Math.Pow(10, 9)));
			myLong += (ulong)(Mil * (System.Math.Pow(10, 6)));
			myLong += (ulong)(Kil * (System.Math.Pow(10, 3)));
			myLong += (ulong)(Nil * (System.Math.Pow(10, 0)));
			return myLong;
		}
		public StrongString GetForServer()
		{
			var _s = CharSeparater;
			_s +=
				(IsNegative ? NegetiveSerialized :
				PossitiveSerialized) + 
				Bil.ToString() + CharSeparater +
				Mil.ToString() + CharSeparater +
				Kil.ToString() + CharSeparater +
				Nil.ToString() + CharSeparater;;
			return _s;
		}
		#endregion
		//-------------------------------------------------
		#region Set Method's Region
		/// <summary>
		/// Setting the Unit by a string value.
		/// </summary>
		/// <param name="TheString"></param>
		public void SetTheUnit(in StrongString TheString)
		{
			var myString	= TheString.Split(CharSeparater);
			var _base		= myString[BASE_INT];
			int _b			= BASE_INT;
			bool _n			= false;
			if (_base == NegetiveSerialized || _base == PossitiveSerialized)
			{
				_b = 1;
				if (_base == NegetiveSerialized)
				{
					_n = true;
				}
			}
			Bil = myString[_b].ToUInt16();
			_b++;
			Mil = myString[_b].ToUInt16();
			_b++;
			Kil = myString[_b].ToUInt16();
			_b++;
			Nil = myString[_b].ToUInt16();
			IsNegative = _n;
		}
		#endregion
		//-------------------------------------------------
		#region Overrided Methods Region
		/// <summary>
		/// Warning: Don't use this function for saving this object to the server,
		/// this is just for showing this object in the Design of the Game.
		/// If you want to save this to the server, use this: <see cref="GetForServer()"/>!
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (IsNegative)
			{
				return NegetiveStringValue + (-this);
			}

			if(Bil == 0)
			{
				if(Mil == 0)
				{
					if(Kil == 0)
					{
						return Nil.ToString();
					}
					else
					{
						return Kil.ToString() + KilStringValue;
					}
				}
				else
				{
					return Mil.ToString() + MilStringValue;
				}
			}
			else
			{
				return Bil.ToString() + BilStringValue;
			}
		}
		/// <summary>
		/// Get the string of this Unit, by indecating whether you want it full or not.
		/// <!--This is not an overrided method,
		/// but this is a overloaded method for ToString() method,
		/// so I thought it it'd be better to right it here.-->
		/// </summary>
		/// <param name="fullUnit">
		/// if true, it returns a full mode unit string.
		/// </param>
		/// <returns></returns>
		public string ToString(bool fullUnit)
		{
			if (fullUnit)
			{
				string myString = string.Empty;
				if(Bil != 0)
				{
					myString += Bil.ToString() + FullTosStringSeparater;
					myString += Mil.ToString(FullUnitFormatConvertor) + FullTosStringSeparater;
					myString += Kil.ToString(FullUnitFormatConvertor) + FullTosStringSeparater;
					myString += Nil.ToString(FullUnitFormatConvertor);
				}
				else
				{
					if (Mil != 0)
					{
						myString += Mil.ToString() + FullTosStringSeparater;
						myString += Kil.ToString(FullUnitFormatConvertor) + FullTosStringSeparater;
						myString += Nil.ToString(FullUnitFormatConvertor);
					}
					else
					{
						if (Kil != 0)
						{
							myString += Kil.ToString() + FullTosStringSeparater;
							myString += Nil.ToString(FullUnitFormatConvertor);
						}
						else
						{
							myString += Nil.ToString();
						}
					}
				}
				if (IsNegative)
				{
					myString = NegetiveStringValue + myString;
				}
				return myString;
			}
			else
			{
				return ToString();
			}
		}
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
		//-------------------------------------------------
		#region Static Methods Region
		public static Unit ConvertToUnit(StrongString theUnitString)
		{
			StrongString[] myString = theUnitString.Split(CharSeparater);
			var _base	= myString[BASE_INT];
			int _b		= 0;
			bool _n		= false;
			Unit _u		= new();
			
			if (_base == NegetiveSerialized || _base == PossitiveSerialized)
			{
				_b = 1;
				if (_base == NegetiveSerialized)
				{
					_n = true;
				}
			}
			_u.Bil = myString[_b].ToUInt16();
			_b++;
			_u.Mil = myString[_b].ToUInt16();
			_b++;
			_u.Kil = myString[_b].ToUInt16();
			_b++;
			_u.Nil = myString[_b].ToUInt16();
			_u.IsNegative = _n;
			return _u;
		}
		public static Unit ConvertToUnit(ulong theULongValue, bool isNegative = false)
		{
			Unit final = new Unit()
			{
				IsNegative = isNegative,
			};
			final.Nil = (ushort)(theULongValue % TheMaxValueOfEachUnit);
			theULongValue /= TheMaxValueOfEachUnit;
			final.Kil = (ushort)(theULongValue % TheMaxValueOfEachUnit);
			theULongValue /= TheMaxValueOfEachUnit;
			final.Mil = (ushort)(theULongValue % TheMaxValueOfEachUnit);
			theULongValue /= TheMaxValueOfEachUnit;
			final.Bil = (ushort)(theULongValue % TheMaxValueOfEachUnit);
			return final;
		}
		public static Unit GetBasicUnit()
		{
			return new Unit()
			{
				Bil = 0,
				Mil = 0,
				Kil = 0,
				Nil = 0
			};
		}
		public static Unit Max(Unit left, Unit right)
		{
			if (left is null)
			{
				if (right is null)
				{
					return null;
				}
				else
				{
					return right;
				}
			}
			else
			{
				if (right is null)
				{
					return left;
				}
				else
				{
					if (left > right)
					{
						return left;
					}
					else
					{
						return right;
					}
				}
			}
		}
		public static int Max(int left, int right)
		{
			return System.Math.Max(left, right);
		}
		public static int Min(int left, int right)
		{
			return System.Math.Min(left, right);
		}
		public static Unit Min(Unit left, Unit right)
		{
			if (left is null)
			{
				if (right is null)
				{
					return null;
				}
				else
				{
					return right;
				}
			}
			else
			{
				if (right is null)
				{
					return left;
				}
				else
				{
					if (left < right)
					{
						return left;
					}
					else
					{
						return right;
					}
				}
			}
		}
		public static Unit Abs(Unit value)
		{
			return new Unit(value)
			{
				IsNegative = false,
			};
		}
		public static int Abs(int value)
		{
			return System.Math.Abs(value);
		}
		public static float Abs(float value)
		{
			return System.Math.Abs(value);
		}
		#endregion
		//-------------------------------------------------
		#region Operators Region
		/// <summary>
		/// Determine whether these two Units have 
		/// the same value or not.
		/// true, if they are equal; otherwise false.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator ==(Unit a, Unit b)
		{
			if(a is null)
			{
				if(b is null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if(b is null)
			{
				return false;
			}
			if(a.IsNegative != b.IsNegative)
			{
				if(a.Bil == b.Bil && a.Bil == 0)
				{
					if(a.Mil == b.Mil && a.Mil == 0)
					{
						if(a.Kil == b.Kil && a.Kil == 0)
						{
							if(a.Nil == b.Nil && a.Nil == 0)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			if(a.Bil == b.Bil)
			{
				if(a.Mil == b.Mil)
				{
					if(a.Kil == b.Kil)
					{
						if(a.Nil == b.Nil)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Determine whether these two Units don't have
		/// the same value or not.
		/// true, if they are not equal; otherwise false.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator !=(Unit a, Unit b)
		{
			if (a is null)
			{
				if (b is null)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else if (b is null)
			{
				return true;
			}
			if (a.Bil == b.Bil)
			{
				if (a.Mil == b.Mil)
				{
					if (a.Kil == b.Kil)
					{
						if (a.Nil == b.Nil)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					else
					{
						return true;
					}
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}
		public static bool operator ==(Unit a, ushort b)
		{
			return a == new Unit(b);
		}
		public static bool operator !=(Unit a, ushort b)
		{
			return a != new Unit(b);
		}
		public static bool operator >(Unit a, Unit b)
		{
			if(a.IsNegative == b.IsNegative)
			{
				if (a.IsNegative)
				{
					if (a.Bil > b.Bil)
					{
						return false;
					}
					else if (a.Bil < b.Bil)
					{
						return true;
					}
					else
					{
						if (a.Mil > b.Mil)
						{
							return false;
						}
						else if (a.Mil < b.Mil)
						{
							return true;
						}
						else
						{
							if (a.Kil > b.Kil)
							{
								return false;
							}
							else if (a.Kil < b.Kil)
							{
								return true;
							}
							else
							{
								if (a.Nil > b.Nil)
								{
									return false;
								}
								else
								{
									return false;
								}
							}
						}
					}
				}
				else
				{
					if (a.Bil > b.Bil)
					{
						return true;
					}
					else if (a.Bil < b.Bil)
					{
						return false;
					}
					else
					{
						if (a.Mil > b.Mil)
						{
							return true;
						}
						else if (a.Mil < b.Mil)
						{
							return false;
						}
						else
						{
							if (a.Kil > b.Kil)
							{
								return true;
							}
							else if (a.Kil < b.Kil)
							{
								return false;
							}
							else
							{
								if (a.Nil > b.Nil)
								{
									return true;
								}
								else
								{
									return false;
								}
							}
						}
					}
				}
				
			}
			else if(a == GetBasicUnit() && b == GetBasicUnit())
			{
				return false;
			}
			else
			{
				// a is negative bot b not
				if (a.IsNegative)
				{
					if(a == b)
					{
						return false;
					}

					return false;
				}
				// b is negative bot a not
				else
				{
					return true;
				}
			}
			
		}
		public static bool operator >(Unit a, ushort b)
		{
			return a > new Unit(b);
		}
		public static bool operator <(Unit a, Unit b)
		{
			if(a == b)
			{
				return false;
			}
			return !(a > b);
		}
		public static bool operator <(Unit a, ushort b)
		{
			return a < new Unit(b);
		}
		public static bool operator >=(Unit a, Unit b)
		{
			return a > b || a == b;
		}
		public static bool operator >=(Unit a, ushort b)
		{
			return a >= new Unit(b);
		}
		public static bool operator <=(Unit a, Unit b)
		{
			return a < b || a == b;
		}
		public static bool operator <=(Unit a, ushort b)
		{
			return a <= new Unit(b);
		}
		public static Unit operator +(Unit a, Unit b)
		{
			if(a.IsNegative == b.IsNegative)
			{
				// a and b both are negative
				if (a.IsNegative)
				{
					return -((-a) + (-b));
				}
				// neither a nor b is not negative
				else
				{
					Unit final = new Unit();
					if (a.Bil + b.Bil <= ushort.MaxValue)
					{
						final.Bil = (ushort)(a.Bil + b.Bil);
					}
					else
					{
						return null;
					}
					final.Mil = (ushort)(a.Mil + b.Mil);
					final.Kil = (ushort)(a.Kil + b.Kil);
					final.Nil = (ushort)(a.Nil + b.Nil);
					return final;
				}
			}
			else
			{
				// a is negativa but b not
				if (a.IsNegative)
				{
					return b - (-a);
				}
				// b is negative but a not
				else
				{
					return a - (-b);
				}
			}
			
		}
		public static Unit operator -(Unit a)
		{
			Unit instead = new Unit(a)
			{
				IsNegative = !a.IsNegative
			};
			return instead;
		}
		public static Unit operator -(Unit a, Unit b)
		{
			// change their position if a is not greater than b
			if(a < b)
			{
				return -(b - a);
			}
			

			if(a.IsNegative == b.IsNegative)
			{
				// a and b both are negative
				if (a.IsNegative)
				{
					return -((-a) + (-b));
				}
				// niether a nor b is negative
				else
				{
					// Create a new copy from objects just in case:
					a = new Unit(a);
					b = new Unit(b);
					Unit final = new Unit();

					// a.Bil is definitely greater or equal to 
					// b.Bil, so don't worry.
					final.Bil = (ushort)(a.bil - b.bil);
					a.Bil = final.Bil;

					// may a.Mil < b.Mil
					if(a.Mil < b.Mil)
					{
						a.bil--;
						final.bil--;
						a.mil += TheMaxValueOfEachUnit;
					}
					final.Mil = (ushort)(a.Mil - b.Mil);
					a.Mil = final.Mil;

					if (a.kil < b.kil)
					{
						if (a.mil == 0)
						{
							a.bil--;
							final.bil--;
							a.mil += TheMaxValueOfEachUnit;
							final.mil = a.mil;
						}
						a.mil--;
						final.mil--;
						a.kil += TheMaxValueOfEachUnit;
						final.Kil = (ushort)(a.Kil - b.Kil);
						a.kil = final.kil;
						if (a.nil < b.nil)
						{
							a.kil--;
							final.kil--;
							a.nil += TheMaxValueOfEachUnit;
							final.Nil = (ushort)(a.Nil - b.Nil);
						}
						else
						{
							final.Nil = (ushort)(a.Nil - b.Nil);
						}
					}
					else
					{
						final.Kil = (ushort)(a.Kil - b.Kil);
						a.kil = final.kil;
						if (a.nil < b.nil)
						{
							if(a.kil == 0)
							{
								if(a.mil == 0)
								{
									a.bil--;
									a.mil += TheMaxValueOfEachUnit;
								}
								a.mil--;
								a.kil += TheMaxValueOfEachUnit;
							}
							a.kil--;
							final.kil--;
							a.nil += TheMaxValueOfEachUnit;
							final.Nil = (ushort)(a.Nil - b.Nil);
						}
						else
						{
							final.Nil = (ushort)(a.Nil - b.Nil);
						}
					}

					return final;
				}
			}
			else
			{
				// a is negative but b not
				if (a.IsNegative)
				{
					// Unreachable code
					// Because in the first line of
					// this method we check that 
					// the a should be greater than b
					return null;
				}
				// b is negative but a not
				else
				{
					return a + (-b);
				}
			}
			
		}
		/// <summary>
		/// Warning:
		/// the first variable should have very small value :|
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Unit operator *(short a, Unit b)
		{
			// a is negative
			if(System.Math.Abs(a) != a)
			{
				return -(((short)-a) * b);
			}
			// b is negative, but a not
			if (b.IsNegative)
			{
				return -(a * (-b));
			}
			Unit final = new Unit
			{
				// niether a nor b is not negative
				Bil = (ushort)(a * b.Bil),
				Mil = (ushort)(a * b.Mil),
				Kil = (ushort)(a * b.Kil),
				Nil = (ushort)(a * b.Nil)
			};
			return final;
		}
		public static Unit operator *(Unit a, short b)
		{
			return b * a;
		}
		public static Unit operator *(ushort a, Unit b)
		{
			return (short)a * b;
		}
		public static Unit operator *(Unit a, ushort b)
		{
			return (short)b * a;
		}
		public static Unit operator *(uint a, Unit b)
		{
			return (short)a * b;
		}
		public static Unit operator *(Unit a, uint b)
		{
			return (short)b * a;
		}
		/// <summary>
		/// Warning : these variables should be very small,
		/// otherwise we will get error.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Unit operator *(Unit a, Unit b)
		{

			if (a.Bil > 0 || a.Mil > 0 || a.Kil > 0 || a.Nil > 0) 
			{
				Unit final = new Unit()
				{
					IsNegative = (a.IsNegative || b.IsNegative) && 
						(!a.IsNegative || !b.IsNegative),
				};
				if (a.Bil > 0 || a.Mil > 0 || a.Kil > 0)
				{
					// not Supported.
					if (b.Bil > 0 || b.Mil > 0 || b.Kil > 0)
					{
						return GetBasicUnit();
					}
					else
					{
						final.Bil = (ushort)(a.Bil * b.Nil);
						final.Mil = (ushort)(a.Mil * b.Nil);
						final.Kil = (ushort)(a.Kil * b.Nil);
						final.Nil = (ushort)(a.Nil * b.Nil);
						return final;
					}
				}
				else if (b.Bil > 0 || b.Mil > 0 || b.Kil > 0)
				{
					// not Supported.
					if (a.Bil > 0 || a.Mil > 0 || a.Kil > 0)
					{
						return GetBasicUnit();
					}
					else
					{
						final.Bil = (ushort)(b.Bil * a.Nil);
						final.Mil = (ushort)(b.Mil * a.Nil);
						final.Kil = (ushort)(b.Kil * a.Nil);
						final.Nil = (ushort)(b.Nil * a.Nil);
						return final;
					}
				}
				else
				{
					final.Bil = (ushort)(a.Bil * b.Nil);
					final.Mil = (ushort)(a.Mil * b.Nil);
					final.Kil = (ushort)(a.Kil * b.Nil);
					final.Nil = (ushort)(a.Nil * b.Nil);
					return final;
				}
			}
			else
			{
				return GetBasicUnit();
			}
		}
		/// <summary>
		/// Warning:
		/// the first variable should have very small value :|
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Unit operator /(Unit a, short b)
		{
			if(b == 0)
			{
				// cannot devide by zero.
				// return the base unit (0).
				return GetBasicUnit();
			}
			bool isBN = !(System.Math.Abs(b) == b);
			Unit final = ConvertToUnit(a.ConvertToInt() / (ulong)b);
			final.IsNegative = (a.IsNegative || isBN) && (!a.IsNegative || !isBN);
			return final;
		}
		#endregion
		//-------------------------------------------------
	}
#pragma warning restore IDE0017
#pragma warning restore IDE0047
}
