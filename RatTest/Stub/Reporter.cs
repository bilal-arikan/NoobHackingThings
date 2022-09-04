using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stub
{
    static class Reporter
    {
        public static void Show(string prefix, string message, string data = "")
        {
            
            if (prefix == "ERR") {
                    Console.WriteLine("HATA>  "+message + " "+data);
            }else if(prefix == "SUCCESS"){
                    Console.WriteLine("---->  " + message + " " + data);
            }else
            {

            }
            
        }
    }
}
