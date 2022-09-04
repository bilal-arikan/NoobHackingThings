/*
*	Keyboard & Mouse Hook
*	File: Form1.Designer.cs
*	Copyright (C) 2007 Ozcan DEGIRMENCI - All rights reserved
*	http://www.ozcandegirmenci.com
*/

namespace TestApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                // onemli mutlaka kapatmammiz lazimm program kapanmadan once
                keyboardHook1.Enabled = false;
                mouseHook1.Enabled = false;
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.keyboardHook1 = new MouseKeyboardHook.KeyboardHook();
			this.mouseHook1 = new MouseKeyboardHook.MouseHook();
			this.txtKeyboard = new System.Windows.Forms.TextBox();
			this.txtMouse = new System.Windows.Forms.TextBox();
			this.btnStartKeyboard = new System.Windows.Forms.Button();
			this.btnStartMouse = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.chkHandle = new System.Windows.Forms.CheckBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// keyboardHook1
			// 
			this.keyboardHook1.Started += new System.EventHandler(this.keyboardHook1_Started);
			this.keyboardHook1.KeyDown += new MouseKeyboardHook.KeyboardHookEventHandler(this.keyboardHook1_KeyDown);
			this.keyboardHook1.SysKeyDown += new MouseKeyboardHook.KeyboardHookEventHandler(this.keyboardHook1_SysKeyDown);
			this.keyboardHook1.KeyUp += new MouseKeyboardHook.KeyboardHookEventHandler(this.keyboardHook1_KeyUp);
			this.keyboardHook1.Stoped += new System.EventHandler(this.keyboardHook1_Stoped);
			this.keyboardHook1.SysKeyUp += new MouseKeyboardHook.KeyboardHookEventHandler(this.keyboardHook1_SysKeyUp);
			// 
			// mouseHook1
			// 
			this.mouseHook1.Started += new System.EventHandler(this.mouseHook1_Started);
			this.mouseHook1.MouseUp += new MouseKeyboardHook.MouseHookEventHandler(this.mouseHook1_MouseUp);
			this.mouseHook1.MouseDown += new MouseKeyboardHook.MouseHookEventHandler(this.mouseHook1_MouseDown);
			this.mouseHook1.MouseMove += new MouseKeyboardHook.MouseHookEventHandler(this.mouseHook1_MouseMove);
			this.mouseHook1.Stoped += new System.EventHandler(this.mouseHook1_Stoped);
			this.mouseHook1.MouseWheel += new MouseKeyboardHook.MouseWheelEventHandler(this.mouseHook1_MouseWheel);
			// 
			// txtKeyboard
			// 
			this.txtKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtKeyboard.Location = new System.Drawing.Point(3, 12);
			this.txtKeyboard.Multiline = true;
			this.txtKeyboard.Name = "txtKeyboard";
			this.txtKeyboard.ReadOnly = true;
			this.txtKeyboard.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtKeyboard.Size = new System.Drawing.Size(404, 211);
			this.txtKeyboard.TabIndex = 0;
			// 
			// txtMouse
			// 
			this.txtMouse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMouse.Location = new System.Drawing.Point(3, 229);
			this.txtMouse.Multiline = true;
			this.txtMouse.Name = "txtMouse";
			this.txtMouse.ReadOnly = true;
			this.txtMouse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMouse.Size = new System.Drawing.Size(404, 205);
			this.txtMouse.TabIndex = 1;
			// 
			// btnStartKeyboard
			// 
			this.btnStartKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStartKeyboard.Location = new System.Drawing.Point(413, 9);
			this.btnStartKeyboard.Name = "btnStartKeyboard";
			this.btnStartKeyboard.Size = new System.Drawing.Size(107, 51);
			this.btnStartKeyboard.TabIndex = 2;
			this.btnStartKeyboard.Text = "Start Keyboard Hook";
			this.btnStartKeyboard.UseVisualStyleBackColor = true;
			this.btnStartKeyboard.Click += new System.EventHandler(this.btnStartKeyboard_Click);
			// 
			// btnStartMouse
			// 
			this.btnStartMouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStartMouse.Location = new System.Drawing.Point(413, 229);
			this.btnStartMouse.Name = "btnStartMouse";
			this.btnStartMouse.Size = new System.Drawing.Size(107, 51);
			this.btnStartMouse.TabIndex = 3;
			this.btnStartMouse.Text = "Start Mouse Hook";
			this.btnStartMouse.UseVisualStyleBackColor = true;
			this.btnStartMouse.Click += new System.EventHandler(this.btnStartMouse_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(413, 90);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 133);
			this.textBox1.TabIndex = 4;
			// 
			// chkHandle
			// 
			this.chkHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkHandle.AutoSize = true;
			this.chkHandle.Location = new System.Drawing.Point(412, 66);
			this.chkHandle.Name = "chkHandle";
			this.chkHandle.Size = new System.Drawing.Size(60, 17);
			this.chkHandle.TabIndex = 5;
			this.chkHandle.Text = "Handle";
			this.chkHandle.UseVisualStyleBackColor = true;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(410, 421);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(109, 13);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Ozcan DEGIRMENCI";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 439);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.chkHandle);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnStartMouse);
			this.Controls.Add(this.btnStartKeyboard);
			this.Controls.Add(this.txtMouse);
			this.Controls.Add(this.txtKeyboard);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Test Mouse & Keyboard Hook Components";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private MouseKeyboardHook.KeyboardHook keyboardHook1;
        private MouseKeyboardHook.MouseHook mouseHook1;
        private System.Windows.Forms.TextBox txtKeyboard;
        private System.Windows.Forms.TextBox txtMouse;
        private System.Windows.Forms.Button btnStartKeyboard;
        private System.Windows.Forms.Button btnStartMouse;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkHandle;
		private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

