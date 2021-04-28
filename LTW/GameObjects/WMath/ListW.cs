// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Collections.Generic;

namespace LTW.GameObjects.WMath
{
    public class ListW<T> : List<T>
    {
        //-------------------------------------------------
        #region Properties Region
        public virtual int Length { get => Count; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public ListW()
        {

        }
        public ListW(IEnumerable<T> _e) : base(_e)
        {

        }
        public ListW(int _cap) : base(_cap)
        {

        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public virtual bool Exists(T _item)
        {
            return Contains(_item);
        }
        public virtual T[] GetArray()
        {
            T[] _t = new T[Length];
            for (int i = 0; i < Length; i++)
            {
                _t[i] = this[i];
            }
            return _t;
        }
        #endregion
        //-------------------------------------------------
    }
}
