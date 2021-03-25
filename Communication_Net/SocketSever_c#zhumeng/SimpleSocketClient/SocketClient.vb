Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Public Class SocketClient
    '客户端Socket
    Private _cliSocket As Socket

    '接收信息线程
    Private myThread As Thread
    Private _RecData As String
    Private _Article As String
    Private _LKSN As String
    Private _ErrorMsg As String

    Private Property ErrorMsg() As String
        Get
            ErrorMsg = _ErrorMsg
        End Get
        Set(ByVal value As String)
            _ErrorMsg = value
        End Set
    End Property
    Private Property LKSN() As String
        Get
            LKSN = _LKSN
        End Get
        Set(ByVal value As String)
            _LKSN = value
        End Set

    End Property

    Private Property Article() As String
        Get
            Article = _Article
        End Get
        Set(ByVal value As String)
            _Article = value
        End Set
    End Property

    Public Sub InitConnect(ByVal strIPAddress As String)

        Dim remoteEP As New IPEndPoint(Net.IPAddress.Parse(strIPAddress), 5566)
        _cliSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        Try
            _cliSocket.Connect(remoteEP)
            If Not _cliSocket.Connected Then
                Throw New Exception("连接失败！")
            End If
            myThread = New Thread(AddressOf ReciveMsg)
            myThread.Start()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ReciveMsg()
        Dim data As String = String.Empty
        Dim tokens() As String
        While True
            Dim bytes() As Byte = New Byte(1024) {}
            Dim bytesRec As Integer = _cliSocket.Receive(bytes)
            data = Encoding.UTF8.GetString(bytes)
            tokens = data.Trim.Split("|")
            Select Case tokens(0).ToUpper '分析接收到的数据，可自己定义更多一些
                Case "Exit"
                    'cliSocket.Shutdown(SocketShutdown.Both)
                    _cliSocket.Close()
                    Exit Sub
                Case "ARTICLE"
                    Article = tokens(1)
                Case "SN"
                    LKSN = tokens(1)
            End Select
        End While
    End Sub

    '发送信息
    Public Sub SendRequest(ByVal strMsg As String)

        Dim msg As Byte() = Encoding.UTF8.GetBytes(strMsg.ToUpper() + "|")
        Dim bytesSent As Integer = _cliSocket.Send(msg)

    End Sub

    '关闭窗口时发关退出信息并清理资源
    Public Sub Quit()
        If _cliSocket.Connected Then
            Dim msg As Byte() = Encoding.UTF8.GetBytes("Exit|客户端退出:")
            Dim bytesSent As Integer = _cliSocket.Send(msg)
            myThread.Abort()
        End If
        _cliSocket.Close()
        _cliSocket.Dispose()

    End Sub

    Public Function GetArticle() As String
        Return Article
    End Function

    Public Function GetSNAndTestReslut() As String

        Return LKSN + "|" + ErrorMsg

    End Function

End Class
