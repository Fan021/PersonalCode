Imports System.ComponentModel
Imports System.Threading
Public Class Client

    Private _client As New SocketClient
    Private _thread As Thread

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        _client.InitConnect(Me.TextBox1.Text.Trim())
        '_thread = New Thread(New ThreadStart(AddressOf ShowData))
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        _client.SendRequest(Me.TextBox2.Text.Trim())
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        _client.Quit()

    End Sub

    Private Sub ShowData()

        Me.RichTextBox1.AppendText("Article|" + _client.GetArticle)
        Me.RichTextBox1.AppendText(vbCrLf)
        Me.RichTextBox1.AppendText("SN|" + _client.GetSNAndTestReslut)
        Me.RichTextBox1.AppendText(vbCrLf)

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        ShowData()

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.TextBox1.Text = "10.180.17.178"
    End Sub

    Private Sub Client_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        _client.Quit()
    End Sub

End Class