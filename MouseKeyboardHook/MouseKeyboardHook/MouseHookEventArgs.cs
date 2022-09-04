/*
*	Keyboard & Mouse Hook
*	File: MouseHookEventArgs.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Windows.Forms;
using System.Drawing;

namespace MouseKeyboardHook
{
    public delegate void MouseHookEventHandler(object sender, MouseHookEventArgs e);

    public class MouseHookEventArgs : EventArgs
    {
        MouseButtons _Button = MouseButtons.None;
        Point _Point = Point.Empty;
        bool _Handled = false;

        public MouseHookEventArgs(MouseButtons buttons, Point pt)
        {
            _Button = buttons;
            _Point = pt;
        }

        public MouseButtons Button
        {
            get { return _Button; }
        }

        public Point Point
        {
            get { return _Point; }
        }

        /// <summary>
        ///  Gets or sets that is mouse operation handled or not ?
        /// </summary>
        public bool Handled
        {
            get { return _Handled; }
            set { _Handled = value; }
        }
    }
}
