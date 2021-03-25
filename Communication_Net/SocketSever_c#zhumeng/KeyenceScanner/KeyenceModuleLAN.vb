Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics

Public Class StateObject
    ' Client socket.
    Public WorkSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BufferSize As Integer = 256
    ' Receive buffer.
    Public Buffer(BufferSize) As Byte
    ' Received data string.
    Public ReceivedData As String
End Class

Public Class KeyenceModuleLAN

    Private _mIP As String = ""
    Private _nPort As Integer = 0
    Private _client As Socket
    Private _remoteEP As IPEndPoint
    ' ManualResetEvent instances signal completion.
    Private _connectDone As New ManualResetEvent(False)
    Private _sendDone As New ManualResetEvent(False)
    Private _receiveDone As New ManualResetEvent(False)
    Private _response As String = String.Empty

    Public Sub New()

    End Sub

    Public Function Init(ByVal mIP As String, ByVal nPort As Integer) As Boolean
        _mIP = mIP
        _nPort = nPort

        Dim isTestRusult As Boolean = False
        Try
            Dim ipAddress As IPAddress = IPAddress.Parse(_mIP)
            _remoteEP = New IPEndPoint(ipAddress, _nPort)
            _client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            _connectDone.Reset()
            _client.BeginConnect(_remoteEP, New AsyncCallback(AddressOf ConnectCallback), _client)
            Thread.Sleep(50)
            isTestRusult = _connectDone.WaitOne(500, True)
            'If Not isTestRusult Then Throw New HardwareException("Scanner init fail, IP:" + _mIP.ToString)
            Return isTestRusult
        Catch ex As Exception
            Return False
        End Try
        Return isTestRusult
    End Function

    Public Function Scan(ByVal iTimeOut As Integer) As String
        Try
            Dim sw As Stopwatch = New Stopwatch()
            _response = ""
            sw.Start()
            If Not TrigON() Then Return ""
            Receive(_client)

            Do While True
                If _response <> "" Then
                    If Not TrigOFF() Then Return ""
                    Return _response
                End If

                If sw.ElapsedMilliseconds > iTimeOut Then
                    If Not TrigOFF() Then Return ""
                    Return ""
                End If
            Loop
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function

    Public Function Quit() As Boolean
        Try
            If _client IsNot Nothing Then
                If _client.Connected Then _client.Disconnect(False)
                _client.Close()
                _client.Dispose()
            End If
        Catch ex As Exception
            Throw New Exception("Function:Quit " + ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

    Private Sub ConnectCallback(ByVal ar As IAsyncResult)
        Try
            Dim client As Socket = CType(ar.AsyncState, Socket)
            If Not client.Connected Then Return
            client.EndConnect(ar)
            _connectDone.Set()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Receive(ByVal client As Socket)
        Try
            Dim state As New StateObject
            state.WorkSocket = client
            client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceiveCallback(ByVal ar As IAsyncResult)
        Try
            Dim state As StateObject = CType(ar.AsyncState, StateObject)
            Dim client As Socket = state.WorkSocket
            Dim bytesRead As Integer = client.EndReceive(ar)

            If bytesRead > 0 Then
                state.ReceivedData = Encoding.ASCII.GetString(state.Buffer, 0, bytesRead)
                'client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
                _response += state.ReceivedData
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Send(ByVal client As Socket, ByVal byteData() As Byte)
        Try
            client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendCallback(ByVal ar As IAsyncResult)
        Try
            Dim client As Socket = CType(ar.AsyncState, Socket)
            Dim bytesSent As Integer = client.EndSend(ar)
            _sendDone.Set()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function TrigON() As Boolean
        Try
            Dim byteData(4) As Byte
            byteData(0) = 2
            byteData(4) = 3
            Array.Copy(Encoding.ASCII.GetBytes("LON"), 0, byteData, 1, 3)
            _sendDone.Reset()
            Send(_client, byteData)
            _sendDone.WaitOne()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Function TrigOFF() As Boolean
        Try
            Dim byteData(5) As Byte
            byteData(0) = 2
            byteData(5) = 3
            Array.Copy(Encoding.ASCII.GetBytes("LOFF"), 0, byteData, 1, 4)
            _sendDone.Reset()
            Send(_client, byteData)
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

End Class
