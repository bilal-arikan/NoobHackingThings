/*
*	Keyboard & Mouse Hook
*	File: MouseHook.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace MouseKeyboardHook
{
    [DefaultProperty("Enabled"),
    DefaultEvent("MouseDown"),
    ToolboxBitmap(typeof(MouseHook), "Resources.MouseHook.bmp")]
    public class MouseHook : Component
    {
        [Description("Occurs when mouse hook is started")]
        public event EventHandler Started = null;
        [Description("Occurs when mouse hook is stopped")]
        public event EventHandler Stoped = null;
        [Description("Occurs when a mouse button is down")]
        public event MouseHookEventHandler MouseDown = null;
        [Description("Occurs when a mouse button is up")]
        public event MouseHookEventHandler MouseUp = null;
        [Description("Occurs when mouse move")]
        public event MouseHookEventHandler MouseMove = null;
        [Description("Occurs whe mouse wheel button is used")]
        public event MouseWheelEventHandler MouseWheel = null;

        int procedure = 0;
        int instanceId = 0;
        NativeMethods.LowLevelMouseProcedure dlgProcedure = null;

        public MouseHook()
        {
            dlgProcedure = new NativeMethods.LowLevelMouseProcedure(OnMouseProcedure);

            Module module = Assembly.GetExecutingAssembly().GetModules()[0];
            instanceId = Marshal.GetHINSTANCE(module).ToInt32();
        }

        int OnMouseProcedure(int nCode, IntPtr wParam, ref NativeMethods.MOUSE lParam)
        {
            int msg = wParam.ToInt32();
            if (msg == NativeMethods.WM_MOUSEWHEEL)
            {
                if (MouseWheel != null)
                {
                    int delta = (short)NativeMethods.GetHighOrder(lParam.mouseData);
                  
                    MouseWheelEventArgs e = new MouseWheelEventArgs(MouseButtons.None,
                        new Point(lParam.position.x, lParam.position.y),
                        delta);
                    OnMouseWheel(e);
                    if (e.Handled)
                        return 0;
                }
            }
            else
            {
                MouseHookEventArgs e = null;
                int x ;
                switch (wParam.ToInt32())
                {
                    case NativeMethods.WM_RBUTTONDOWN:
                        if (MouseDown != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.Right,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseDown(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_LBUTTONDOWN:
                        if (MouseDown != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.Left,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseDown(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_LBUTTONUP:
                        if (MouseUp != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.Left,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseUp(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_RBUTTONUP:
                        if (MouseUp != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.Right,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseUp(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_MOUSEMOVE:
                        if (MouseMove != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.None,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseMove(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_MBUTTONDOWN:
                        if (MouseDown != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.Middle,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseDown(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_MBUTTONUP:
                        if (MouseUp != null)
                        {
                            e = new MouseHookEventArgs(MouseButtons.Middle,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseUp(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_XBUTTONDOWN:
                        if (MouseDown != null)
                        {
                            x = NativeMethods.GetHighOrder(lParam.mouseData);
                            e = new MouseHookEventArgs(x == 1 ? 
                                MouseButtons.XButton1 : MouseButtons.XButton2,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseDown(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                    case NativeMethods.WM_XBUTTONUP:
                        if (MouseUp != null)
                        {
                            x = NativeMethods.GetHighOrder(lParam.mouseData);
                            e = new MouseHookEventArgs(x == 1 ?
                                MouseButtons.XButton1 : MouseButtons.XButton2,
                                new Point(lParam.position.x, lParam.position.y));
                            OnMouseUp(e);
                            if (e.Handled)
                                return 0;
                        }
                        break;
                }
            }

            return NativeMethods.NextMouseHook(NativeMethods.WH_MOUSE_LL,
                nCode, wParam, ref lParam);
        }

        protected virtual void OnMouseDown(MouseHookEventArgs e)
        {
            MouseDown(this, e);
        }

        protected virtual void OnMouseUp(MouseHookEventArgs e)
        {
            MouseUp(this, e);
        }

        protected virtual void OnMouseMove(MouseHookEventArgs e)
        {
            MouseMove(this, e);
        }

        protected virtual void OnMouseWheel(MouseWheelEventArgs e)
        {
            MouseWheel(this, e);
        }

        void Start()
        {
            Stop();

            procedure = NativeMethods.SetMouseHook(NativeMethods.WH_MOUSE_LL,
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

        [Description("Gets or sets the mouse hook state"),
        DefaultValue(false)]
        public bool Enabled
        {
            get { return (procedure != 0); }
            set
            {
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
