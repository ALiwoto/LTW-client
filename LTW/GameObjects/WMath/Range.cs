// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.GameObjects.WMath
{
    public struct Range
    {
        //-------------------------------------------------
        #region Properties Region
        public int Minimum { get; }
        public int Maximum { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public Range(int minimum, int maximum)
        {
            if (minimum > maximum)
            {
                Maximum = minimum;
                Minimum = maximum;
            }
            else
            {

                Minimum = minimum;
                Maximum = maximum;
            }
        }
        public Range(float minimum, float maximum) : this((int)minimum, (int)maximum)
        {
            // ;
        }
        #endregion
        //-------------------------------------------------
    }
}
