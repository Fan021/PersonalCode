Imports System.IO.Ports
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Public MustInherit Class VirtualPort

    Protected _Port As Integer = 1
    Protected _Settings As String = "9600,N,8,1"

    ReadOnly Property Port As Integer
        Get
            Return _Port
        End Get
    End Property

    ReadOnly Property Settings As String
        Get
            Return _Settings
        End Get
    End Property

    Public Overridable Function Init(ByVal iPort As Integer, ByVal Settings As String) As Integer
        Return 0
    End Function
   
    Public Overridable Function Quit() As Integer
    End Function

    Public Overridable Sub WriteLine(ByVal text As String)
    End Sub

    Public Overridable Sub Write(ByVal text As String)
    End Sub

    Overridable ReadOnly Property BytesToRead() As Integer
        Get
            Return Nothing
        End Get
    End Property

    Public Overridable Function ReadByte() As Byte
        Return 0
    End Function

    Public Overridable Sub DiscardInBuffer()

    End Sub

    Public Overridable Sub DiscardOutBuffer()

    End Sub
End Class

Public Class Serial
    Inherits VirtualPort

    Private _comm As SerialPort

    Public Overrides Function Init(ByVal iPort As Integer, ByVal Settings As String) As Integer
        Dim Setting() As String = Settings.Split(",")
        Try
            _comm = New SerialPort
            With _comm
                .PortName = "COM" + iPort.ToString
                .BaudRate = CInt(Setting(0))
                Select Case Setting(1).ToUpper
                    Case "N"
                        .Parity = Parity.None
                    Case "E"
                        .Parity = Parity.Even
                    Case "O"
                        .Parity = Parity.Odd
                    Case Else
                        Return -1
                End Select
                .DataBits = CInt(Setting(2))
                .StopBits = CInt(Setting(3))
                .Open()
            End With

            _Port = iPort
            _Settings = Settings

        Catch ex As Exception
            Throw
            Return -1
        End Try

        Return 0
    End Function

    Public Overrides Function Quit() As Integer
        Try
            If _comm IsNot Nothing AndAlso _comm.IsOpen Then

                _comm.Close()
                _comm = Nothing

            End If
        Catch ex As Exception
            Throw
            Return -1
        End Try
        Return 0
    End Function

    Public Overrides Sub Write(ByVal text As String)
     
        _comm.Write(text)


    End Sub

    Public Overrides Sub WriteLine(ByVal text As String)
     
        _comm.WriteLine(text)


    End Sub

    Public Overrides Sub DiscardInBuffer()
        _comm.DiscardInBuffer()
    End Sub

    Public Overrides Sub DiscardOutBuffer()
        _comm.DiscardOutBuffer()
    End Sub

    Public Overrides ReadOnly Property BytesToRead As Integer
        Get
            Return _comm.BytesToRead
        End Get
    End Property

    Public Overrides Function ReadByte() As Byte
        Return _comm.ReadByte()
    End Function
End Class

Public Class Parallel
    Inherits VirtualPort

#Region "API"

    Const GENERIC_READ = &H80000000
    Const GENERIC_WRITE = &H40000000
    Const FILE_SHARE_READ = 1
    Const FILE_SHARE_WRITE = 2
    Const OPEN_EXISTING = 3
    Const INVALID_HANDLE_VALUE = -1

    Private _hFile As Integer = 0


    Public Structure SECURITY_ATTRIBUTES
        Dim nLength As Integer
        Dim lpSecurityDescriptor As Integer
        Dim bInheritHandle As Integer
    End Structure

    Public Structure OVERLAPPED
        Dim Internal As Integer
        Dim InternalHigh As Integer
        Dim offset As Integer
        Dim OffsetHigh As Integer
        Dim hEvent As Integer
    End Structure

    'Public Structure COMMTIMEOUTS
    '    Dim ReadIntervalTimeout As UInteger
    '    Dim ReadTotalTimeoutMultiplier As UInteger
    '    Dim ReadTotalTimeoutConstant As UInteger
    '    Dim WriteTotalTimeoutMultiplier As UInteger
    '    Dim WriteTotalTimeoutConstant As UInteger
    'End Structure

    <DllImport("kernel32.dll")> _
    Public Shared Function CreateFile(ByVal lpszFileName As String, _
                                      ByVal dwDesiredAccess As Integer, _
                                      ByVal dwShareMode As Integer, _
                                      ByVal lpSecurityAttributes As Integer, _
                                      ByVal dwCreationDisposition As Integer, _
                                      ByVal dwFlagsAndAttributes As Integer, _
                                      ByVal hTemplateFile As Integer) As Integer
    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function WriteFile(ByVal hFile As Integer, _
                                       ByVal lpBuffer As IntPtr, _
                                       ByVal nNumberOfBytesToWrite As Integer, _
                                       ByRef lpNumberOfBytesWritten As Integer, _
                                       ByVal lpOverlapped As Integer _
                                       ) As Integer
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function ReadFile(ByVal hFile As Integer, _
                                       ByVal lpBuffer As IntPtr, _
                                       ByVal nNumberOfBytesToRead As Integer, _
                                       ByRef lpNumberOfBytesRead As Integer, _
                                       ByVal lpOverlapped As Integer _
                                       ) As Integer
    End Function

    '<DllImport("kernel32.dll")>
    'Public Shared Function SetCommTimeouts(ByVal hFile As Integer,
    '                                   ByRef val As COMMTIMEOUTS
    '                                   ) As Integer
    'End Function



    <DllImport("kernel32.dll")> _
    Public Shared Function CloseHandle(ByVal hFile As Integer) As Integer
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function GetLastError() As Integer
    End Function


#End Region

    Public Overrides Function Init(ByVal iPort As Integer, ByVal Settings As String) As Integer

        Dim nRes As Integer = CreateFile("\\.\LPT" + iPort.ToString, _
                   GENERIC_READ Or GENERIC_WRITE, _
                   FILE_SHARE_WRITE, _
                   0, _
                   OPEN_EXISTING, _
                   0, _
                   0)

        If nRes = INVALID_HANDLE_VALUE Then Return -1

        'set time out
        'Dim timeout As COMMTIMEOUTS
        'timeout.ReadIntervalTimeout = 1000
        'timeout.ReadTotalTimeoutConstant = 1000
        'timeout.ReadTotalTimeoutMultiplier = 1000
        'timeout.WriteTotalTimeoutConstant = 1000
        'timeout.WriteTotalTimeoutMultiplier = 1000

        'Dim result As Integer = SetCommTimeouts(nRes, timeout)
        'If result = 0 Then
        '    result = GetLastError()
        '    CloseHandle(nRes)

        '    Return -1
        'End If

        _hFile = nRes
        _Port = iPort
        _Settings = Settings

        Return 0

    End Function

    Public Overrides Function Quit() As Integer
        If _hFile <> INVALID_HANDLE_VALUE Then
            CloseHandle(_hFile)
            _hFile = INVALID_HANDLE_VALUE
        End If
    End Function

    Public Overrides Sub WriteLine(ByVal text As String)
        If _hFile = INVALID_HANDLE_VALUE Then
            Throw New Exception("device is not open!,please call function Init first")
        End If

        text += vbCrLf

        Dim nWritten As Integer = 0
        Dim nErrorCode As Integer = 0
        Dim data() As Byte
        'alloc memory
        Dim pData As IntPtr = Marshal.AllocHGlobal(text.Length + 1)

        data = System.Text.ASCIIEncoding.ASCII.GetBytes(text)
        Marshal.Copy(data, 0, pData, text.Length)

        'Write
        Dim nRes As Integer = WriteFile(_hFile, pData, text.Length, nWritten, 0)
        Marshal.FreeHGlobal(pData)

        If nRes = 0 Or nWritten <> text.Length Then
            nErrorCode = GetLastError()
            Throw New Exception("failed to write data to parallel port!, error code is " + nErrorCode.ToString)
        End If

    End Sub

    Public Overrides Sub Write(ByVal text As String)
        If _hFile = INVALID_HANDLE_VALUE Then
            Throw New Exception("device is not open!,please call function Init first")
        End If

        Dim nWritten As Integer = 0
        Dim nErrorCode As Integer = 0
        Dim data() As Byte

        'alloc memory
        Dim pData As IntPtr = Marshal.AllocHGlobal(text.Length + 1)
        data = System.Text.ASCIIEncoding.ASCII.GetBytes(text)
        Marshal.Copy(data, 0, pData, text.Length)

        'Write
        Dim nRes As Integer = WriteFile(_hFile, pData, text.Length, nWritten, 0)
        Marshal.FreeHGlobal(pData)

        If nRes = 0 Or nWritten <> text.Length Then
            nErrorCode = GetLastError()
            Throw New Exception("failed to write data to parallel port!, error code is " + nErrorCode.ToString)
        End If
    End Sub
End Class

Public Class NetPrinter
    Inherits VirtualPort
    Dim printSocket As Socket
    Dim printEP As IPEndPoint
    Public Overrides Function Init(ByVal iPort As Integer, ByVal Settings As String) As Integer
        Try
            Dim printIP As IPAddress = IPAddress.Parse(Settings)
            printEP = New IPEndPoint(printIP, iPort)
            printSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            printSocket.Connect(printEP)
            Return 0
        Catch ex As Exception
            Throw ex
            Return -1
        End Try
    End Function

    Public Function QueryPrinterStatus() As Boolean
        Try
            Dim data() As Byte
            Dim responsData(1024) As Byte
            Dim responsStr As String = String.Empty
            Dim dataLenth As Integer
            data = System.Text.ASCIIEncoding.ASCII.GetBytes("~HS")
            Dim mytimer As New Stopwatch
            mytimer.Start()
            Do
                printSocket.Send(data, data.Length, SocketFlags.None)
                System.Threading.Thread.Sleep(5)
                dataLenth = printSocket.Receive(responsData)
                responsStr += System.Text.Encoding.UTF8.GetString(responsData, 0, dataLenth)
                If responsStr.Contains(vbCrLf) Then
                    Return True
                End If
            Loop Until mytimer.ElapsedMilliseconds > 10000
            Return False
        Catch ex As Exception
            Return FailHandle()
        End Try
        Return True
    End Function
    Public Overrides Function Quit() As Integer
        Try
            If printSocket IsNot Nothing AndAlso printSocket.Connected Then

                printSocket.Close()
                printSocket = Nothing

            End If
        Catch ex As Exception
            Throw
            Return -1
        End Try
        Return 0
    End Function

    Public Overrides Sub Write(ByVal text As String)

        Dim data() As Byte
        data = System.Text.ASCIIEncoding.ASCII.GetBytes(text)

        If QueryPrinterStatus() Then
            printSocket.Send(data, data.Length, SocketFlags.None)
        Else
            System.Windows.Forms.MessageBox.Show("printer has no response!")
        End If

    End Sub

    Public Overrides Sub WriteLine(ByVal text As String)

        Dim data() As Byte
        data = System.Text.ASCIIEncoding.ASCII.GetBytes(text)
        Try
            printSocket.Send(data, data.Length, SocketFlags.None)
        Catch ex As Exception
            FailHandle()
            printSocket.Send(data, data.Length, SocketFlags.None)
        End Try


    End Sub

    Private Function FailHandle() As Boolean
        Try

            printSocket.Close()
            System.Threading.Thread.Sleep(10)
            printSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

            printSocket.Connect(printEP)
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Overrides ReadOnly Property BytesToRead As Integer
        Get
            If printSocket Is Nothing Then Return 0
            Return printSocket.Available
        End Get
    End Property

    Public Overrides Function ReadByte() As Byte
        Dim data(2) As Byte
        printSocket.Receive(data, 1, SocketFlags.None)
        Return data(0)
    End Function

End Class