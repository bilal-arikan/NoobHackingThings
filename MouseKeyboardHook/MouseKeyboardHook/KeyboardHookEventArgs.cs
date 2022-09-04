/*
*	Keyboard & Mouse Hook
*	File: KeyboardHookEventArgs.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Windows.Forms;

namespace MouseKeyboardHook
{
    public delegate void KeyboardHookEventHandler (object sender, KeyboardHookEventArgs e);

    /// <summary>
    ///  Event argument class for the keyboard events
    /// </summary>
    public class KeyboardHookEventArgs : EventArgs
    {
        Keys _DialogKeys = Keys.None;
        Keys _Key = Keys.None;
        bool _Handled = false;

        public KeyboardHookEventArgs(Keys dialogKey, Keys key)
        {
            _DialogKeys = dialogKey;
            _Key = key;
        }

        public Keys DialogKeys
        {
            get { return _DialogKeys; }
        }

        public Keys Key
        {
            get { return _Key; }
        }

        /// <summary>
        ///  Gets or sets that whether the event is handled or not ?
        /// </summary>
        public bool Handled
        {
            get { return _Handled; }
            set { _Handled = value; }
        }
    }
}
