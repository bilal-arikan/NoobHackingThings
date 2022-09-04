/*
*	Keyboard & Mouse Hook
*	File: Form1.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void AddString(string str, TextBox txt)
        {
            string current = txt.Text;
            if (current.Length > 2000)
                current = current.Substring(0, 2000);

            txt.Text = str + "\r\n" + current;
        }

        private void keyboardHook1_Started(object sender, EventArgs e)
        {
            btnStartKeyboard.Text = "Stop Keyboard Hook";
        }

        private void keyboardHook1_Stoped(object sender, EventArgs e)
        {
            btnStartKeyboard.Text = "Start Keyboard Hook";
        }

        private void mouseHook1_Started(object sender, EventArgs e)
        {
            btnStartMouse.Text = "Stop Mouse Hook";
        }

        private void mouseHook1_Stoped(object sender, EventArgs e)
        {
            btnStartMouse.Text = "Start Mouse Hook";
        }

        private void btnStartKeyboard_Click(object sender, EventArgs e)
        {
            keyboardHook1.Enabled = !keyboardHook1.Enabled;
            txtKeyboard.Focus();
        }

        private void btnStartMouse_Click(object sender, EventArgs e)
        {
            mouseHook1.Enabled = !mouseHook1.Enabled;
        }

        private void keyboardHook1_KeyDown(object sender, MouseKeyboardHook.KeyboardHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Key Down: Dialog Keys->");
            text.Append(e.DialogKeys.ToString());
            text.Append(", Key: ");
            text.Append(e.Key.ToString());

            e.Handled = chkHandle.Checked;

            AddString(text.ToString(), txtKeyboard);
        }

        private void keyboardHook1_KeyUp(object sender, MouseKeyboardHook.KeyboardHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Key Up: Dialog Keys->");
            text.Append(e.DialogKeys.ToString());
            text.Append(", Key: ");
            text.Append(e.Key.ToString());

            e.Handled = chkHandle.Checked;

            AddString(text.ToString(), txtKeyboard);
        }

        private void keyboardHook1_SysKeyDown(object sender, MouseKeyboardHook.KeyboardHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Sys Key Down: Dialog Keys->");
            text.Append(e.DialogKeys.ToString());
            text.Append(", Key: ");
            text.Append(e.Key.ToString());

            e.Handled = chkHandle.Checked;

            AddString(text.ToString(), txtKeyboard);
        }

        private void keyboardHook1_SysKeyUp(object sender, MouseKeyboardHook.KeyboardHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Sys Key Up: Dialog Keys->");
            text.Append(e.DialogKeys.ToString());
            text.Append(", Key: ");
            text.Append(e.Key.ToString());

            e.Handled = chkHandle.Checked;

            AddString(text.ToString(), txtKeyboard);
        }

        private void mouseHook1_MouseDown(object sender, MouseKeyboardHook.MouseHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Mouse Down: Button->");
            text.Append(e.Button.ToString());
            text.Append(", Point: ");
            text.Append(e.Point.ToString());

            AddString(text.ToString(), txtMouse);
        }

        private void mouseHook1_MouseMove(object sender, MouseKeyboardHook.MouseHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Mouse Move: Button->");
            text.Append(e.Button.ToString());
            text.Append(", Point: ");
            text.Append(e.Point.ToString());

            AddString(text.ToString(), txtMouse);
        }

        private void mouseHook1_MouseUp(object sender, MouseKeyboardHook.MouseHookEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Mouse Up: Button->");
            text.Append(e.Button.ToString());
            text.Append(", Point: ");
            text.Append(e.Point.ToString());

            AddString(text.ToString(), txtMouse);
        }

        private void mouseHook1_MouseWheel(object sender, MouseKeyboardHook.MouseWheelEventArgs e)
        {
            StringBuilder text = new StringBuilder();
            text.Append("Mouse Wheel: Button->");
            text.Append(e.Button.ToString());
            text.Append(", Point: ");
            text.Append(e.Point.ToString());
            text.Append(", Delta: ");
            text.Append(e.Delta.ToString());

            AddString(text.ToString(), txtMouse);
        }

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(@"http://www.ozcandegirmenci.com");
		}
    }
}