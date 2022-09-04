/*
*	Keyboard & Mouse Hook
*	File: MouseWheelEventArgs.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseKeyboardHook
{
    public delegate void MouseWheelEventHandler(object sender, MouseWheelEventArgs e);

    public class MouseWheelEventArgs : MouseHookEventArgs
    {
        int _Delta = 0;

        public MouseWheelEventArgs(MouseButtons buttons,
            Point pt, int delta)
            : base(buttons, pt)
        {
            _Delta = delta;
        }

        public int Delta
        {
            get { return _Delta; }
        }
    }
}
