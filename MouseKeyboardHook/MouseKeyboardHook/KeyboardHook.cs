/*
*	Keyboard & Mouse Hook
*	File: KeyboardHook.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace MouseKeyboardHook
{
    [DefaultEvent("KeyDown"),
    DefaultProperty("Enabled"),
    ToolboxBitmap(typeof(KeyboardHook), "Resources.KeyboardHook.bmp")]
    public class KeyboardHook : Component
    {
        [Description("Occurs when keyboard hook is started")]
        public event EventHandler Started = null;
        [Description("Occurs when keyboard hook is stopped")]
        public event EventHandler Stoped = null;
        [Description("Occurs when a key is down")]
        public event KeyboardHookEventHandler KeyDown = null;
        [Description("Occurs when a key is up")]
        public event KeyboardHookEventHandler KeyUp = null;
        [Description("Occurs when a system key is down")]
        public event KeyboardHookEventHandler SysKeyDown = null;
        [Description("Occurs when a system key is up")]
        public event KeyboardHookEventHandler SysKeyUp = null;

		bool _Enable = false;

        int procedure = 0;
        int instanceId = 0;
        NativeMethods.LowLevelKeyboardProcedure dlgProcedure = null;

        public KeyboardHook()
        {
            dlgProcedure = new NativeMethods.LowLevelKeyboardProcedure(OnKeyboardProcedure);

            Module module = Assembly.GetExecutingAssembly().GetModules()[0];
            instanceId = Marshal.GetHINSTANCE(module).ToInt32();
        }

        void Start()
        {
            Stop();

            procedure = NativeMethods.SetKeyboardHook(NativeMethods.WH_KEYBOARD_LL,
                dlgProcedure, instanceId, 0);

            GC.Collect();
            OnStarted();
        }

        void Stop()
        {
            if (procedure != 0)
                NativeMethods.UnhookWindowsHookEx(procedure);

            procedure = 0;
            GC.KeepAlive(dlgProcedure);
            OnStopped();
        }

        int OnKeyboardProcedure(int nCode, IntPtr wParam, ref NativeMethods.KEYBOARD lParam)
        {
            switch (wParam.ToInt32())
            { 
                case NativeMethods.WM_KEYDOWN:
                    if (KeyDown != null)
                    {
                        KeyboardHookEventArgs e = GetEventArgs(wParam, ref lParam);
                        OnKeyDown(e);
                        if (e.Handled)
                            return 0;
                    }
                    break;
                case NativeMethods.WM_KEYUP:
                    if (KeyUp != null)
                    {
                        KeyboardHookEventArgs e = GetEventArgs(wParam, ref lParam);
                        OnKeyUp(e);
                        if (e.Handled)
                            return 0;
                    }
                    break;
                case NativeMethods.WM_SYSKEYDOWN:
                    if (SysKeyDown != null)
                    {
                        KeyboardHookEventArgs e = GetEventArgs(wParam, ref lParam);
                        OnSysKeyDown(e);
                        if (e.Handled)
                            return 0;
                    }
                    break;
                case NativeMethods.WM_SYSKEYUP:
                    if (SysKeyUp != null)
                    {
                        KeyboardHookEventArgs e = GetEventArgs(wParam, ref lParam);
                        OnSysKeyUp(e);
                        if (e.Handled)
                            return 0;
                    }
                    break;
            }

            return NativeMethods.NextKeyboardHook(NativeMethods.WH_KEYBOARD_LL,
                nCode, wParam, ref lParam);
        }

        protected virtual void OnKeyDown(KeyboardHookEventArgs e)
        {
            KeyDown(this, e);
        }

        protected virtual void OnKeyUp(KeyboardHookEventArgs e)
        {
            KeyUp(this, e);
        }

        protected virtual void OnSysKeyDown(KeyboardHookEventArgs e)
        {
            SysKeyDown(this, e);
        }

        protected virtual void OnSysKeyUp(KeyboardHookEventArgs e)
        {
            SysKeyUp(this, e);
        }

        KeyboardHookEventArgs GetEventArgs(IntPtr wParam, ref NativeMethods.KEYBOARD lParam)
        {
            Keys dialog = Keys.None;

            switch (lParam.flags)
            { 
                case 0:
                    dialog = Keys.Control;
                    break;
                case 1:
                    dialog = Keys.LWin;
                    break;
                case 32:
                    dialog = Keys.Alt;
                    break;
            }

            return new KeyboardHookEventArgs(dialog, (Keys)lParam.vkCode);
        }

        // Close while disposing
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Enabled = false;
                dlgProcedure = null;

                GC.SuppressFinalize(this);
            }
            base.Dispose(disposing);
        }

        protected virtual void OnStarted()
        {
            if (Started != null)
                Started(this, EventArgs.Empty);
        }

        protected virtual void OnStopped()
        {
            if (Stoped != null)
                Stoped(this, EventArgs.Empty);
        }

        [Description("Gets or sets the keyboard hook status"),
        DefaultValue(false),
		Category("Behaviour")]
        public bool Enabled
        {
            get { return (procedure != 0); }
            set {
                if (Enabled == value)
                    return;

                if (value)
                    Start();
                else
                    Stop();
            }
        }
    }
}
