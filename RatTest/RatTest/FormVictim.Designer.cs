namespace RatTest
{
    partial class FormVictim
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPagePCInfo = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLocY = new System.Windows.Forms.Label();
            this.labelLocX = new System.Windows.Forms.Label();
            this.buttonLocation = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPageSendCommand = new System.Windows.Forms.TabPage();
            this.textBoxCMD = new System.Windows.Forms.TextBox();
            this.buttonSendCmd = new System.Windows.Forms.Button();
            this.buttonServerClose = new System.Windows.Forms.Button();
            this.buttonServerRestart = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPagePCInfo.SuspendLayout();
            this.tabPageSendCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPagePCInfo);
            this.tabControl1.Controls.Add(this.tabPageSendCommand);
            this.tabControl1.Location = new System.Drawing.Point(89, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(827, 418);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPagePCInfo
            // 
            this.tabPagePCInfo.Controls.Add(this.label6);
            this.tabPagePCInfo.Controls.Add(this.label5);
            this.tabPagePCInfo.Controls.Add(this.textBox3);
            this.tabPagePCInfo.Controls.Add(this.listView1);
            this.tabPagePCInfo.Controls.Add(this.label4);
            this.tabPagePCInfo.Controls.Add(this.label3);
            this.tabPagePCInfo.Controls.Add(this.labelLocY);
            this.tabPagePCInfo.Controls.Add(this.labelLocX);
            this.tabPagePCInfo.Controls.Add(this.buttonLocation);
            this.tabPagePCInfo.Controls.Add(this.textBox2);
            this.tabPagePCInfo.Controls.Add(this.textBox1);
            this.tabPagePCInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPagePCInfo.Name = "tabPagePCInfo";
            this.tabPagePCInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePCInfo.Size = new System.Drawing.Size(819, 392);
            this.tabPagePCInfo.TabIndex = 0;
            this.tabPagePCInfo.Text = "PC Information";
            this.tabPagePCInfo.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Internet IP Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Local IP Adresses";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(7, 366);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(646, 20);
            this.textBox3.TabIndex = 8;
            // 
            // listView1
            // 
            this.listView1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(7, 236);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(646, 97);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Windows";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kullanıcı";
            // 
            // labelLocY
            // 
            this.labelLocY.AutoSize = true;
            this.labelLocY.Location = new System.Drawing.Point(7, 165);
            this.labelLocY.Name = "labelLocY";
            this.labelLocY.Size = new System.Drawing.Size(35, 13);
            this.labelLocY.TabIndex = 4;
            this.labelLocY.Text = "label2";
            // 
            // labelLocX
            // 
            this.labelLocX.AutoSize = true;
            this.labelLocX.Location = new System.Drawing.Point(7, 152);
            this.labelLocX.Name = "labelLocX";
            this.labelLocX.Size = new System.Drawing.Size(332, 13);
            this.labelLocX.TabIndex = 3;
            this.labelLocX.Text = "https://www.google.com.tr/maps/@40.9764732,27.1741741,13.25z";
            // 
            // buttonLocation
            // 
            this.buttonLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLocation.Location = new System.Drawing.Point(7, 184);
            this.buttonLocation.Name = "buttonLocation";
            this.buttonLocation.Size = new System.Drawing.Size(646, 23);
            this.buttonLocation.TabIndex = 2;
            this.buttonLocation.Text = "Location";
            this.buttonLocation.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(6, 74);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(647, 75);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(7, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(646, 20);
            this.textBox1.TabIndex = 0;
            // 
            // tabPageSendCommand
            // 
            this.tabPageSendCommand.Controls.Add(this.textBoxCMD);
            this.tabPageSendCommand.Controls.Add(this.buttonSendCmd);
            this.tabPageSendCommand.Location = new System.Drawing.Point(4, 22);
            this.tabPageSendCommand.Name = "tabPageSendCommand";
            this.tabPageSendCommand.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSendCommand.Size = new System.Drawing.Size(819, 392);
            this.tabPageSendCommand.TabIndex = 1;
            this.tabPageSendCommand.Text = "Send Message";
            this.tabPageSendCommand.UseVisualStyleBackColor = true;
            // 
            // textBoxCMD
            // 
            this.textBoxCMD.Location = new System.Drawing.Point(7, 7);
            this.textBoxCMD.Multiline = true;
            this.textBoxCMD.Name = "textBoxCMD";
            this.textBoxCMD.Size = new System.Drawing.Size(236, 154);
            this.textBoxCMD.TabIndex = 2;
            // 
            // buttonSendCmd
            // 
            this.buttonSendCmd.Location = new System.Drawing.Point(7, 167);
            this.buttonSendCmd.Name = "buttonSendCmd";
            this.buttonSendCmd.Size = new System.Drawing.Size(236, 23);
            this.buttonSendCmd.TabIndex = 1;
            this.buttonSendCmd.Text = "Send";
            this.buttonSendCmd.UseVisualStyleBackColor = true;
            this.buttonSendCmd.Click += new System.EventHandler(this.buttonSendCmd_Click);
            // 
            // buttonServerClose
            // 
            this.buttonServerClose.Location = new System.Drawing.Point(13, 13);
            this.buttonServerClose.Name = "buttonServerClose";
            this.buttonServerClose.Size = new System.Drawing.Size(70, 47);
            this.buttonServerClose.TabIndex = 1;
            this.buttonServerClose.Text = "button1";
            this.buttonServerClose.UseVisualStyleBackColor = true;
            // 
            // buttonServerRestart
            // 
            this.buttonServerRestart.Location = new System.Drawing.Point(13, 66);
            this.buttonServerRestart.Name = "buttonServerRestart";
            this.buttonServerRestart.Size = new System.Drawing.Size(70, 47);
            this.buttonServerRestart.TabIndex = 2;
            this.buttonServerRestart.Text = "button1";
            this.buttonServerRestart.UseVisualStyleBackColor = true;
            // 
            // FormVictim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 442);
            this.Controls.Add(this.buttonServerRestart);
            this.Controls.Add(this.buttonServerClose);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormVictim";
            this.Text = "FormVictim";
            this.tabControl1.ResumeLayout(false);
            this.tabPagePCInfo.ResumeLayout(false);
            this.tabPagePCInfo.PerformLayout();
            this.tabPageSendCommand.ResumeLayout(false);
            this.tabPageSendCommand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPagePCInfo;
        private System.Windows.Forms.TabPage tabPageSendCommand;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLocY;
        private System.Windows.Forms.Label labelLocX;
        private System.Windows.Forms.Button buttonLocation;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSendCmd;
        private System.Windows.Forms.TextBox textBoxCMD;
        private System.Windows.Forms.Button buttonServerClose;
        private System.Windows.Forms.Button buttonServerRestart;
    }
}