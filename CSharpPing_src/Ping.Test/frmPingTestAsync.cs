using System;
using System.Windows.Forms;
using System.Threading;
using BS.Utilities;

namespace PingTest
{
	public class frmPingTestAsync : System.Windows.Forms.Form
	{
		#region Auto-Generated Code

		private System.Windows.Forms.Button cmdPing;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.TextBox txtHostname;
		private System.Windows.Forms.ListBox lstResponses;
		private System.Windows.Forms.Button cmdCancel;
		private BS.Utilities.Ping netMon;
		private System.ComponentModel.IContainer components;

		public frmPingTestAsync()
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
			this.cmdCancel = new System.Windows.Forms.Button();
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
			// cmdCancel
			// 
			this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCancel.Location = new System.Drawing.Point(224, 8);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Visible = false;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// netMon
			// 
			this.netMon.PingError += new BS.Utilities.PingErrorEventHandler(this.netMon_PingError);
			this.netMon.PingStarted += new BS.Utilities.PingStartedEventHandler(this.netMon_PingStarted);
			this.netMon.PingResponse += new BS.Utilities.PingResponseEventHandler(this.netMon_PingResponse);
			this.netMon.PingCompleted += new BS.Utilities.PingCompletedEventHandler(this.netMon_PingCompleted);
			// 
			// frmPingTest
			// 
			this.AcceptButton = this.cmdPing;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdClose;
			this.ClientSize = new System.Drawing.Size(416, 278);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lstResponses);
			this.Controls.Add(this.txtHostname);
			this.Controls.Add(this.cmdPing);
			this.Controls.Add(this.cmdCancel);
			this.Name = "frmPingTest";
			this.Text = "Ping Test";
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new frmPingTestAsync());
		}

		private IAsyncResult result;
		private bool cancel = false;

		private void cmdPing_Click(object sender, System.EventArgs e)
		{
			if (txtHostname.Text == null || txtHostname.Text.Length == 0)
				return;

			cancel = false;
			lstResponses.Items.Clear();

			result = netMon.BeginPingHost(new AsyncCallback(EndPing), txtHostname.Text, 4);

			if (result != null)
			{
				cmdCancel.Visible = true;
				cmdPing.Visible = false;
			}
		}
		
		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			cancel = true;
		}

		private void EndPing(IAsyncResult result)
		{
			netMon.EndPingHost(result);

			cmdCancel.Visible = false;
			cmdPing.Visible = true;			
		}

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void netMon_PingStarted(object sender, PingStartedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new PingStartedEventHandler(netMon_PingStarted), new object[] { sender, e });
			}
			else
			{
				lstResponses.Items.Add("Ping Started for: " + e.ServerEndPoint.Address.ToString() + " at " + e.StartDateTime.ToString());
			}
		}

		private void netMon_PingResponse(object sender, PingResponseEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new PingResponseEventHandler(netMon_PingResponse), new object[] { sender, e });
			}
			else
			{
				if (e.Result == PingResponseType.Ok)
					lstResponses.Items.Add("Reply from " + e.ServerAddress.ToString() + ": bytes=" + e.ByteCount.ToString() + " time=" + e.ResponseTime);
				else
					lstResponses.Items.Add("Ping Error: " + e.Result.ToString());

				e.Cancel = cancel;
			}
		}

		private void netMon_PingCompleted(object sender, PingCompletedEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new PingCompletedEventHandler(netMon_PingCompleted), new object[] { sender, e });
			}
			else
			{
				switch (e.PingResponse.PingResult)
				{
					case PingResponseType.Ok:
					case PingResponseType.RequestTimedOut:
						lstResponses.Items.Add("Ping stats for " + e.PingResponse.ServerEndPoint.Address.ToString());
						lstResponses.Items.Add("Packets: Sent = " + e.PingResponse.PacketsSent.ToString() + ", Received = " + e.PingResponse.PacketsReceived + ", Lost = " + e.PingResponse.Lost);
						lstResponses.Items.Add("Approximate times:");
						lstResponses.Items.Add("Minimum = " + e.PingResponse.MinimumTime + "ms, Maximum = " + e.PingResponse.MaximumTime + "ms, Average = " + e.PingResponse.AverageTime + "ms");
						break;
					case PingResponseType.Canceled:
						lstResponses.Items.Add("Ping Canceled.");
						break;
					case PingResponseType.InternalError:
						lstResponses.Items.Add("Internal Errors occurred.");
						break;
					case PingResponseType.ConnectionError:
						lstResponses.Items.Add("Connection Error: " + e.PingResponse.ErrorMessage);
						break;
				}
			}
		}

		private void netMon_PingError(object sender, PingErrorEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new PingErrorEventHandler(netMon_PingError), new object[] { sender, e });
			}
			else
			{
				lstResponses.Items.Add("Ping Error: " + e.Message + ", at " + e.ErrorDateTime.ToString());
			}
		}
	}
}
