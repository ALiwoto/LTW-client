// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.Controls.Elements
{
    public enum ElementPriority
    {
        /// <summary>
        /// for <see cref="MapElement"/>
        /// </summary>
        VeryLow = 0,
        /// <summary>
        /// for the elements which are in front of the map.
        /// </summary>
        Low = 1,
        /// <summary>
        /// for DialongCharacterElements
        /// </summary>
        Normal = 2,
        High = 3,
        VeryHigh = 4,
        SuperHigh = 5,
        BeyondHigh = 6,
        TopMost = 7,
        SandBox = 8,
    }
}
