// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.Controls.Elements
{
    public enum ElementMovements
    {
        /// <summary>
        /// That means this Elements should not move at the time 
        /// mouse downed.
        /// </summary>
        NoMovements = 0,
        /// <summary>
        /// This element will move vertical
        /// at the time mouse downed.
        /// </summary>
        VerticalMovements = 1,
        /// <summary>
        /// This element will move Horizontal
        /// at the time mouse downed.
        /// </summary>
        HorizontalMovements = 2,
        /// <summary>
        /// This element will move both vertical and Horizontal
        /// at the time mouse downed.
        /// </summary>
        VerticalHorizontalMovements = 3,
    }
}
