// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace WotoProvider.Interfaces
{
    public interface IStringProvider<T> where T: class
    {
        //-------------------------------------------------
        #region this Region
        char this[int index] { get; }
        #endregion
        //-------------------------------------------------
        #region Properties Region
        int Length { get; }
        bool IsDisposed { get; }
        #endregion
        //-------------------------------------------------
        #region Methods Region
		/// <summary>
		/// 
		/// 
		/// </summary>
        void ChangeValue(string anotherValue);
		/// <summary>
		/// 
		/// 
		/// </summary>
        string GetValue();
		/// <summary>
		/// 
		/// 
		/// </summary>
        void Dispose();
		/// <summary>
		/// 
		/// 
		/// </summary>
        int IndexOf(string value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        int IndexOf(char value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        int IndexOf(T value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        int ToInt32();
		/// <summary>
		/// 
		/// 
		/// </summary>
        ushort ToUInt16();
		/// <summary>
		/// 
		/// 
		/// </summary>
        ulong ToUInt64();
		/// <summary>
		/// 
		/// 
		/// </summary>
        float ToSingle();
		/// <summary>
		/// 
		/// 
		/// </summary>
        T GetStrong();
		/// <summary>
		/// 
		/// 
		/// </summary>
        T[] Split(params string[] separator);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T[] Split(params T[] separator);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Substring(in int startIndex, in int length);
		
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Substring(in int startIndex);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Remove(in char value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Remove(in int startIndex, in int count);
		T RemoveSpecial();
		/// <summary>
		/// simply appends a character to the end of the 
		/// string provider.
		/// </summary>
		/// <param name="value"> 
		/// the character which to append.
		/// </param>
        T Append(in char value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in char value, in bool _check);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in string value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in string value, in bool _check);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in string value, in int count);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in string value, in int count, in bool _check);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(params string[] values);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in bool _check, params string[] values);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in T value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in T value, in bool _check);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in T value, in int count);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in T value, in int count, in bool _check);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(params T[] value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in bool _check, params T[] value);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in char value, in int count);
		/// <summary>
		/// 
		/// 
		/// </summary>
        T Append(in char value, in int count, in bool _check);
		/// <summary>
		/// 
		/// 
		/// </summary>
		bool HasSpecial();
		bool IsSignedChar(in int _index);
        #endregion
        //-------------------------------------------------
    }
}
