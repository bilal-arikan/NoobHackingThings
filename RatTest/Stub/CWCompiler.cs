using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Stub
{
    public class CWCompiler
    {
        public System.Reflection.Assembly Assembly;
        public IList<string> UsingListesi { get; private set; }
        public IList<string> ReferenceListesi { get; private set; }
        public string _namespace = "DynamicCore";
        public string _class = "Core";
        /*Kod derleme işleminden sonra programdaki fonksiyonları çalıştırabilmek için kullanacağımız değişken*/
        private Type MethodType = null;
        public bool SonCalismaHatali { get; private set; }
        public string FilePath = null;
        public string Kod { get; set; }
        //C# Dilinde tanımlama için yeni bir Derleyici tanımlıyoruz.
        CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
        CompilerParameters parameters = new CompilerParameters();
        


        public CWCompiler()
        {
            UsingListesi = new List<string>();
            ReferenceListesi = new List<string>();
        }
        //--------------------------------------------------------------------------------------
        public bool Compile(string codes,bool saveToDisk,string filenameFull)
        {
            //Belirtmiş olduğumuz referansları compilere ekliyoruz.
            for (int i = 0; i < ReferenceListesi.Count; i++)
            {
                parameters.ReferencedAssemblies.Add(ReferenceListesi[i]);
            }
            StringBuilder SB = new StringBuilder();
            //Belirtmiş olduğumuz usingleri StringBuilder' in sonuna ekliyoruz.
            for (int i = 0; i < UsingListesi.Count; i++)
            {
                SB.Append(UsingListesi[i]);
            }
            /*namespaceyi geçerli uygulamamızdaki kodlara doğrudan erişebilmek için,
            geçerli projemizle aynı isimde yaptık.*/
            SB.Append("namespace "+_namespace+"{ ");
            SB.Append("public class "+_class+"{ ");
            SB.Append(codes);
            SB.Append("} ");
            SB.Append("}");
            //Kod işlemi bittikten sonra derleme işlemini yapıyoruz.
            Kod = SB.ToString();

            //Projemizi bir dll kütüphanesi olarak ayarlıyoruz.
            parameters.CompilerOptions = "/t:library";
            CompilerResults cr;
            if (saveToDisk)
            {
                parameters.GenerateInMemory = false;
                parameters.GenerateExecutable = false;
                parameters.OutputAssembly = filenameFull;
            }
            else
            {
                //Projemiz doğrudan hafıza derlenmesini sağlıyoruz.
                parameters.GenerateInMemory = true;
            }
            
            cr = codeProvider.CompileAssemblyFromSource(parameters,Kod);
            //Eğer derleme işlemi hatalı olduysa, ekranda mesaj olarak gösterecek.
            if (cr.Errors.Count > 0)
            {
                string ErrMessage = null;
                for (int i = 0; i < cr.Errors.Count; i++)
                {
                    ErrMessage += "Hata(" + i + "): " + cr.Errors[i].ErrorText +
                        "(Satır: " + cr.Errors[i].Line + ", Sütun: " +
                        cr.Errors[i].Column + ")" + "\n";

                }
                Reporter.Show("ERR","Kod Derlenirken Hata Oluştu ", ErrMessage);
                this.Assembly = null;
                return false;
            }
            this.Assembly = cr.CompiledAssembly;
            this.FilePath = filenameFull;

            if (saveToDisk)
            {
                File.SetAttributes(FilePath, FileAttributes.Hidden | FileAttributes.System);
            }
            return true;
        }
        //--------------------------------------------------------------------------------------
        public object Run(string Method, Object[] Args = null)
        {
            //Değeri varsayılan olarak false yapıyoruz.
            SonCalismaHatali = true;
            if (this.Assembly == null)
                return "ERR|Assembly null";

            //CWAddons sınıfının içerisindeki kodları çalıştırmak için.
            object Obj = this.Assembly.CreateInstance(_namespace + "." + _class);
            //Obj değişkenine atanan sınıfın fonksiyonlarını çalıştırabilmemiz için
            MethodType = Obj.GetType();

            //Method null veya boş veya GecerliType null ise devam etmeyecek.
            if (string.IsNullOrEmpty(Method))
                return "ERR|Argument null" ;

            if (MethodType == null)
                return "ERR|MethodType null";

            try
            {
                    //Hedef fonksiyonu alıyoruz.
                    MethodInfo AddonsRun = MethodType.GetMethod(Method);
                    /*Hedef fonksiyonu belirtmiş olduğumuz parametrelerle
                     çalıştırıyoruz ve hedef fonksiyondan gelen cevabı dönüyor. */
                object temp = AddonsRun.Invoke(Method, Args);

                SonCalismaHatali = false;
                return temp;
            }
            catch (Exception e)
            {
                //SonCalismaHatali = true;
                return "ERR|"+e.Message;
            }
        }
        //--------------------------------------------------------------------------------------
        public bool LoadDllFromFile(string filenameFull)
        {
            try
            {
                this.Assembly = Assembly.LoadFile(filenameFull);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
        //-------------------------------------------------------------------------------------
        public object StartMethod(string _namespace, string _class, string _method, object[] args)
        {
            var theType = this.Assembly.GetType(_namespace + "." + _class);
            object c = Activator.CreateInstance(theType);
            MethodInfo method = theType.GetMethod(_method);
            return method.Invoke(c, args);
        }
        //--------------------------------------------------------------------------------------
        public bool CheckWhitoutError()
        {
            CompilerParameters p = new CompilerParameters();

            //Belirtmiş olduğumuz referansları compilere ekliyoruz.
            for (int i = 0; i < ReferenceListesi.Count; i++)
            {
                p.ReferencedAssemblies.Add(ReferenceListesi[i]);
            }
            StringBuilder SB = new StringBuilder();
            //Belirtmiş olduğumuz usingleri StringBuilder' in sonuna ekliyoruz.
            for (int i = 0; i < UsingListesi.Count; i++)
            {
                SB.Append(UsingListesi[i]);
            }
            /*namespaceyi geçerli uygulamamızdaki kodlara doğrudan erişebilmek için,
            geçerli projemizle aynı isimde yaptık.*/
            SB.Append("namespace " + _namespace + "{ ");
            SB.Append("public class " + _class + "{ ");
            SB.Append(Kod);
            SB.Append("} ");
            SB.Append("}");
            //Kod işlemi bittikten sonra derleme işlemini yapıyoruz.
            string KodTemp = SB.ToString();
            p.CompilerOptions = "/t:library";
            p.GenerateInMemory = true;
            CompilerResults crTemp = codeProvider.CompileAssemblyFromSource(p, KodTemp);
            //crTemp.CompiledAssembly = null;

            //Eğer derleme işlemi hatalı olduysa, ekranda mesaj olarak gösterecek.
            if ( crTemp.Errors.Count > 0 )
                return false;
            else
                return true;
        }


    }
}
