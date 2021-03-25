Imports System.IO.Ports

Public Class KeyenceModuleRS232

    Private _serial As SerialPort
    Private _nPort As Integer = 1
    Private _baudRate As Integer = 9600
    Private _parity As Parity = Parity.None

    Public Const STX As Byte = &H1B
    Public Const ETX As Byte = &HD

    Sub New(ByVal nPort As Integer, ByVal BaudRate As Integer, ByVal Prity As Parity)
        _nPort = nPort
        _baudRate = BaudRate
        _parity = Prity
    End Sub

    Public Function Init() As Boolean
        Dim bRes As Boolean = False
        If _serial Is Nothing Then
            _serial = New SerialPort("COM" + _nPort.ToString, _baudRate)
            _serial.Parity = _parity

            Try
                _serial.Open()
            Catch ex As Exception
                bRes = False
                Throw ex
            End Try

            bRes = True
        End If

        Return bRes
    End Function

    Public Function Scan(ByVal nTimeOut As Long) As String
        'Trig On
        Dim wh As New Stopwatch()
        Dim strBarcode As String = ""

        Try
            'trig on
            Me._serial.DiscardInBuffer()
            Me._serial.DiscardOutBuffer()
            Me.TrigOn()

            'start timer
            wh.Start()

            'wait for CR
            Dim byCurr As Byte

            Do
                If _serial.BytesToRead > 0 Then
                    byCurr = _serial.ReadByte
                    If byCurr = ETX Then
                        Exit Do
                    End If
                    strBarcode += Chr(byCurr)
                End If
                If wh.ElapsedMilliseconds > nTimeOut Then
                    strBarcode = ""

                    Exit Do
                End If
            Loop
        Catch ex As Exception
            Return ""
        Finally
            Me.TrigOff()
            wh = Nothing
        End Try

        Return strBarcode
    End Function

    Public Function Scan(band As Integer, ByVal nTimeOut As Long) As String
        'Trig On
        Dim wh As New Stopwatch()
        Dim strBarcode As String = ""

        Try
            'trig on
            Me._serial.DiscardInBuffer()
            Me._serial.DiscardOutBuffer()
            Me.TrigOn(band)

            'start timer
            wh.Start()

            'wait for CR
            Dim byCurr As Byte

            Do
                If _serial.BytesToRead > 0 Then
                    byCurr = _serial.ReadByte
                    If byCurr = ETX Then
                        Exit Do
                    End If
                    strBarcode += Chr(byCurr)
                End If
                If wh.ElapsedMilliseconds > nTimeOut Then
                    strBarcode = ""

                    Exit Do
                End If
            Loop
        Catch ex As Exception
            Return ""
        Finally
            Me.TrigOff()
            wh = Nothing
        End Try

        Return strBarcode
    End Function

    Public Function SendRead(ByVal cmd() As Byte, ByVal nTimeout As Integer) As Byte()
        Dim watch As New Stopwatch()
        Dim bRet As Boolean = False
        Dim byRecv As New List(Of Byte)
        Dim currentByte As Byte = 0

        _serial.DiscardInBuffer()
        _serial.DiscardOutBuffer()

        _serial.Write(cmd, 0, cmd.Length)
        watch.Start()

        Do
            If _serial.BytesToRead > 0 Then
                currentByte = _serial.ReadByte

                If currentByte <> &HD Then 'CR
                    byRecv.Add(currentByte)
                Else
                    bRet = True
                    Exit Do
                End If
            End If

            If watch.ElapsedMilliseconds > nTimeout Then
                bRet = False
                Exit Do
            End If

            Threading.Thread.Sleep(1)
        Loop

        Return byRecv.ToArray
    End Function

    Public Function TrigOn() As Boolean
        If _serial Is Nothing Then Return False
        Dim send(4) As Byte
        send(0) = STX
        send(4) = ETX

        Array.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes("LON"), 0, send, 1, 3)

        _serial.Write(send, 0, send.Length)
        'Dim Ret = SendRead(send, 500)

        'If Ret.Length < 2 Then Return False

        'If Ret(0) <> &H4F Or Ret(1) <> &H4B Then 'OK
        '    Return False
        'End If

        Return True
    End Function

    Public Function TrigOn(band As Integer) As Boolean
        If _serial Is Nothing Then Return False
        Dim send(6) As Byte
        send(0) = STX
        send(6) = ETX

        Array.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes("LON" + band.ToString("D02")),
                   0, send, 1, 5)

        _serial.Write(send, 0, send.Length)
        'Dim Ret = SendRead(send, 500)

        'If Ret.Length < 2 Then Return False

        'If Ret(0) <> &H4F Or Ret(1) <> &H4B Then 'OK
        '    Return False
        'End If

        Return True
    End Function

    Public Function TrigOff() As Boolean
        If _serial Is Nothing Then Return False
        Dim send(5) As Byte
        send(0) = STX
        send(5) = ETX
        Array.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes("LOFF"), 0, send, 1, 4)

        _serial.Write(send, 0, send.Length)

        Return True
    End Function

    Public Function Aim(ByVal bOnOff As Boolean) As Boolean
        If _serial Is Nothing Then Return False
        Dim send(7) As Byte
        send(0) = STX
        If bOnOff Then
            Array.Resize(send, 7)
            send(6) = ETX

            Array.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes("_LDON"), 0, send, 1, 5)
        Else
            Array.Resize(send, 8)
            send(7) = ETX

            Array.Copy(System.Text.ASCIIEncoding.ASCII.GetBytes("_LDOFF"), 0, send, 1, 6)

        End If

        Dim Ret = SendRead(send, 500)

        If Ret.Length < 2 Then Return False

        If Ret(0) <> &H4F Or Ret(1) <> &H4B Then 'OK
            Return False
        End If

        Return True

    End Function

    Public Function Quit() As Boolean
        If _serial IsNot Nothing Then
            If _serial.IsOpen Then
                _serial.Close()
            End If

            _serial = Nothing
        End If

        Return True
    End Function

End Class


