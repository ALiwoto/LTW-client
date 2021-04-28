// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

#if __WINDOWS__
using System.Runtime.InteropServices;

namespace WotoProvider.WotoTools
{
	public class Taskbar
	{
		/// <summary>
		/// Checking status of Showing of TaskBar in windows.
		/// true: it is showing , false: it is hide.
		/// </summary>
		public static bool IsShowing { get; set; } = true;
		[DllImport("user32.dll")]
		private static extern int FindWindow(string className, string windowText);

		[DllImport("user32.dll")]
		private static extern int ShowWindow(int hwnd, int command);

		[DllImport("user32.dll")]
		public static extern int FindWindowEx(int parentHandle, int childAfter, string className, int windowTitle);

		[DllImport("user32.dll")]
		private static extern int GetDesktopWindow();

		private const int SW_HIDE = 0;
		private const int SW_SHOW = 1;

		protected static int Handle
		{
			get
			{
				return FindWindow("Shell_TrayWnd", "");
			}
		}

		protected static int HandleOfStartButton
		{
			get
			{
				int handleOfDesktop = GetDesktopWindow();
				int handleOfStartButton = FindWindowEx(handleOfDesktop, 0, "button", 0);
				return handleOfStartButton;
			}
		}

		private Taskbar()
		{
			// hide ctor
		}

		public static void Show()
		{
			IsShowing = true;
			ShowWindow(Handle, SW_SHOW);
			ShowWindow(HandleOfStartButton, SW_SHOW);
		}

		public static void Hide()
		{
			IsShowing = false;
			ShowWindow(Handle, SW_HIDE);
			ShowWindow(HandleOfStartButton, SW_HIDE);
		}
	}
}
#endif