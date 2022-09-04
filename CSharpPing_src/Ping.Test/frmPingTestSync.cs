using System;
using System.Windows.Forms;
using System.Threading;
using BS.Utilities;

namespace PingTest
{
	public class frmPingTestSync : System.Windows.Forms.Form
	{
		#region Auto-Generated Code

		private System.Windows.Forms.Button cmdPing;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.TextBox txtHostname;
		private System.Windows.Forms.ListBox lstResponses;
		private BS.Utilities.Ping netMon;
		private System.ComponentModel.IContainer components;

		public frmPingTestSync()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.cmdPing = new System.Windows.Forms.Button();
			this.txtHostname = new System.Windows.Forms.TextBox();
			this.lstResponses = new System.Windows.Forms.ListBox();
			this.cmdClose = new System.Windows.Forms.Button();
			this.netMon = new BS.Utilities.Ping(this.components);
			this.SuspendLayout();
			// 
			// cmdPing
			// 
			this.cmdPing.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdPing.Location = new System.Drawing.Point(224, 8);
			this.cmdPing.Name = "cmdPing";
			this.cmdPing.TabIndex = 0;
			this.cmdPing.Text = "Ping Host";
			this.cmdPing.Click += new System.EventHandler(this.cmdPing_Click);
			// 
			// txtHostname
			// 
			this.txtHostname.Location = new System.Drawing.Point(8, 8);
			this.txtHostname.Name = "txtHostname";
			this.txtHostname.Size = new System.Drawing.Size(208, 20);
			this.txtHostname.TabIndex = 1;
			this.txtHostname.Text = "";
			// 
			// lstResponses
			// 
			this.lstResponses.Location = new System.Drawing.Point(8, 40);
			this.lstResponses.Name = "lstResponses";
			this.lstResponses.Size = new System.Drawing.Size(400, 199);
			this.lstResponses.TabIndex = 2;
			// 
			// cmdClose
			// 
			this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdClose.Location = new System.Drawing.Point(336, 248);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.TabIndex = 3;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// frmPingTestSync
			// 
			this.AcceptButton = this.cmdPing;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(416, 278);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lstResponses);
			this.Controls.Add(this.txtHostname);
			this.Controls.Add(this.cmdPing);
			this.Name = "frmPingTestSync";
			this.Text = "Ping Test";
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		private void cmdPing_Click(object sender, System.EventArgs e)
		{
			if (txtHostname.Text == null || txtHostname.Text.Length == 0)
				return;

			lstResponses.Items.Clear();

			PingResponse response = netMon.PingHost(txtHostname.Text, 4);	

			if (response == null)
			{
				lstResponses.Items.Add("Error");
			}
			else
			{
				ProcessResponse(response);
			}
		}
				
		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ProcessResponse(PingResponse response)
		{
			switch (response.PingResult)
			{
				case PingResponseType.Ok:
				case PingResponseType.RequestTimedOut:
					foreach (int pingTime in response.ResponseTimes)
					{
						if (pingTime == Constants.InvalidInt)
						{
							lstResponses.Items.Add("Ping Timeout");
						}
						else
						{
							lstResponses.Items.Add("Reply from " + response.ServerEndPoint.Address.ToString() + ": time=" + pingTime);
						}
					}

					lstResponses.Items.Add("Ping stats for " + response.ServerEndPoint.Address.ToString());
					lstResponses.Items.Add("Packets: Sent = " + response.PacketsSent.ToString() + ", Received = " + response.PacketsReceived + ", Lost = " + response.Lost.ToString());
					if (response.PacketsReceived > 0)
					{
						lstResponses.Items.Add("Approximate times:");
						lstResponses.Items.Add("Minimum = " + response.MinimumTime + "ms, Maximum = " + response.MaximumTime + "ms, Average = " + response.AverageTime + "ms");
					}

					break;
				case PingResponseType.InternalError:
					lstResponses.Items.Add("Internal Error occurred: " + response.ErrorMessage);
					break;
				case PingResponseType.ConnectionError:
					lstResponses.Items.Add("Connection Error: " + response.ErrorMessage);
					break;
				case PingResponseType.CouldNotResolveHost:
					lstResponses.Items.Add("Could not resolve host");
					break;
			}
		}
	}
}
