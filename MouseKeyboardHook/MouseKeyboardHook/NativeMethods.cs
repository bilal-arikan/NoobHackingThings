/*
*	Keyboard & Mouse Hook
*	File: NativeMethods.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Runtime.InteropServices;

namespace MouseKeyboardHook
{
    public class NativeMethods
    {
        public static int GetLowOrder(int pt)
        {
            return (pt & 0xffff);
        }

        public static int GetHighOrder(int pt)
        {
            return ((pt >> 0x10) & 0xffff);
        }

        public delegate int LowLevelKeyboardProcedure(int nCode,
            IntPtr wParam, ref KEYBOARD lParam);
        public delegate int LowLevelMouseProcedure(int nCode,
            IntPtr wParam, ref MOUSE lParam);

        [DllImport("user32.dll", EntryPoint="SetWindowsHookEx")]
        public static extern int SetKeyboardHook(int hookType,
            LowLevelKeyboardProcedure procedure, int hMode, int dwThread);

        [DllImport("user32.dll", EntryPoint="SetWindowsHookEx")]
        public static extern int SetMouseHook(int hookType,
            LowLevelMouseProcedure procedure, int hMode, int dwThread);

        [DllImport("user32.dll", EntryPoint="CallNextHookEx")]
        public static extern int NextKeyboardHook(int hookType, int nCode,
            IntPtr wParam, ref KEYBOARD lParam);

        [DllImport("user32.dll", EntryPoint="CallNextHookEx")]
        public static extern int NextMouseHook(int hookType, int nCode,
            IntPtr wParam, ref MOUSE lParam);

        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(int hookType,
            IntPtr procedure, int hMode, int dwThread);

        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(int hookType, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(int dwThread);

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBOARD
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSE
        {
            public POINT position;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;

        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;

        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_MOUSEWHEEL = 0x20A;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_XBUTTONDOWN = 0x20B;
        public const int WM_XBUTTONUP = 0x20C;

        public const int XBUTTON1 = 0x0001;
        public const int XBUTTON2 = 0x0002;
    }
}
