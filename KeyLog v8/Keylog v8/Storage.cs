using System;

using System.IO;
using System.Net;

namespace KeyLog_v8
{
    class Storage
    {
        public Storage(string kayitKonum,string dosyaAdi)
        {
            Name = dosyaAdi;
            Directory = kayitKonum;
        }

        string Name;
        string Directory;
        StreamWriter SWrite;
        StreamReader SRead;


        public bool SetText(string data)
        {
            try
            {
                SWrite = new StreamWriter(Directory + Name, false);
                SWrite.Write(data);
                SWrite.Close();
                return true; 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public int AddText(string data)
        {
            try
            {
                SWrite = new StreamWriter(Directory + Name, true);
                SWrite.Write(data);
                SWrite.Close();

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public string ReadText()
        {
            try
            {
                SRead = new StreamReader(Directory + Name);
                string temp = SRead.ReadToEnd();
                SRead.Close();

                return temp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
