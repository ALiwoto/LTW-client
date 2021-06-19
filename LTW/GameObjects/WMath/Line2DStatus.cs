// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.GameObjects.WMath
{
	/// <summary>
	/// The status of a <see cref="Line2D"/>
	/// </summary>
	public enum Line2DStatus
	{
		/// <summary>
		/// The line is limited in both x1 and x2.
		/// </summary>
		BothLimited     = (0 << 0b00),
		/// <summary>
		/// The line is not limited at all.
		/// </summary>
		BothContinous   = (1 << 0b00),
		/// <summary>
		/// The line is unlimited in x1 and y1.
		/// </summary>
		ContinousOne    = (1 << 0b01),
		/// <summary>
		/// The line is unlimited in x2 and y2.
		/// </summary>
		ContinousTwo    = (1 << 0b10),
    }
}