// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace LTW.SandBox
{
	/// <summary>
	/// The Priority of Sandboxes.
	/// which is used in <see cref="SandBoxBase.Priority"/>
	/// </summary>
    public enum SandBoxPriority
    {
		/// <summary>
		/// represents a low priority sandbox.
		/// </summary>
        LowSandBox          = 0,
		/// <summary>
		/// a TopMost Sandbox.
		/// this sandbox is not an error type, so
		/// the Error sandboxes still can be shown on it.
		/// </summary>
        TopMostSandBox      = 1,
		/// <summary>
		/// an Error sandbox with a low priority.
		/// </summary>
        LowErrorSandBox     = 2,
		/// <summary>
		/// an error sandbox with a TopMost priority,
		/// at this rate, another activity of the game will be stopped,
		/// and game will be closed after closing this sandbox.
		/// use it for ConnectionClosedSandBox.
		/// </summary>
        TopMostErrorSandBox = 3,
    }
}
