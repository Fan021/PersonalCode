Public Class Zebra
    Inherits Printer

    Protected STX As Byte = 2
    Protected ETX As Byte = 3

    Enum PrintMode As Byte
        Rewind = 0
        PeelOff
        TearOff
        Cutter
        Applicator
    End Enum

    Structure Status
        Dim strCommSettings As String
        Dim bIsPaperOut As Boolean
        Dim bIsPuused As Boolean
        Dim nLabelLength As Integer
        Dim nNumOfFormats As Integer
        Dim bIsBufferFulled As Boolean
        Dim bIsDiagnosticMode As Boolean
        Dim bIsPartialFormat As Boolean
        Dim bIsRamCorrupt As Boolean
        Dim bIsUnderTemperature As Boolean
        Dim bIsOverTemperature As Boolean

        Dim strFunctionSettings As String
        Dim bIsHeadUp As Boolean
        Dim bIsRibbonOut As Boolean
        Dim bIsThermelTransferMode As Boolean
        Dim PrintMode As PrintMode
        Dim PrintWindthMode As Byte
        Dim bIsLabelWaitting As Boolean
        Dim nLabelsRemaining As Integer
        Dim NumberOfGraphicImages As Integer

        Dim Password As String
        Dim bIsRAMInstalled As Boolean
    End Structure

    Public Function GetPrinterStatus() As Status
        Dim retStatus As New Status
        Dim strText As String = ""

        retStatus.bIsLabelWaitting = False

        'Send ~HS
        Comm.DiscardInBuffer()
        Comm.DiscardOutBuffer()
        Comm.Write("~HS")

        'Waitting for responese
        System.Threading.Thread.Sleep(10)

        '1st response
        strText = WaitingForResponse(500)
        If strText = "" Then
            Return Nothing
        End If
        GetInfoFromFirstMessage(retStatus, strText)

        '2nd response
        strText = WaitingForResponse(500)
        If strText = "" Then
            Return Nothing
        End If
        GetInfoFromSecondMessage(retStatus, strText)

        '2nd response
        strText = WaitingForResponse(500)
        If strText = "" Then
            Return Nothing
        End If
        GetInfoFromThirdMessage(retStatus, strText)


        Return retStatus
    End Function

    Private Sub GetInfoFromFirstMessage(ByRef status As Status, ByVal msg As String)
        Dim items() As String = msg.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)

        Debug.Assert(items.Length = 12)
        status.strCommSettings = items(0)
        status.bIsPaperOut = (items(1) = "1")
        status.bIsPuused = (items(2) = "1")
        status.nLabelLength = Integer.Parse(items(3))
        status.nNumOfFormats = Integer.Parse(items(4))
        status.bIsBufferFulled = (items(5) = "1")
        status.bIsDiagnosticMode = (items(6) = "1")
        status.bIsPartialFormat = (items(7) = "1")
        status.bIsRamCorrupt = (items(9) = "1")
        status.bIsUnderTemperature = (items(10) = "1")
        status.bIsOverTemperature = (items(11) = "1")

    End Sub


    Private Sub GetInfoFromSecondMessage(ByRef status As Status, ByVal msg As String)
        Dim items() As String = msg.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)

        Debug.Assert(items.Length = 11)
        status.strFunctionSettings = items(0)
        status.bIsHeadUp = (items(2) = "1")
        status.bIsRibbonOut = (items(3) = "1")
        status.bIsThermelTransferMode = (items(4) = "1")
        status.PrintMode = [Enum].Parse(GetType(PrintMode), items(5))
        status.PrintWindthMode = (items(6) = "1")
        status.bIsLabelWaitting = (items(7) = "1")
        status.nLabelsRemaining = Integer.Parse(items(8))
        'status. = (items(9) = "1")
        'status.bIsUnderTemperature = (items(10) = "1")
        'status.bIsOverTemperature = (items(11) = "1")

    End Sub

    Private Sub GetInfoFromThirdMessage(ByRef status As Status, ByVal msg As String)
        Dim items() As String = msg.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)

        Debug.Assert(items.Length = 2)
        status.Password = items(0)
        status.bIsRAMInstalled = (items(1) = "1")

    End Sub

    Private Function WaitingForResponse(ByVal nTimeout As Integer) As String
        Dim strRet As String = ""
        Dim CurrData As Byte
        Dim Watch As New Stopwatch
        Dim bSuccess As Boolean = False

        Watch.Start()

        'Wait For STX
        Do
            If Comm.BytesToRead > 0 Then
                If Comm.ReadByte() = STX Then
                    bSuccess = True
                    Exit Do
                End If
            End If

            If Watch.ElapsedMilliseconds > nTimeout Then
                strRet = ""
                bSuccess = False
                Exit Do
            End If

            Threading.Thread.Sleep(0)
        Loop

        'Read data
        If bSuccess Then
            Do
                If Comm.BytesToRead > 0 Then
                    CurrData = Comm.ReadByte()

                    If CurrData = ETX Then
                        bSuccess = True
                        Exit Do
                    Else
                        strRet += Chr(CurrData)
                    End If
                End If

                If Watch.ElapsedMilliseconds > nTimeout Then
                    strRet = ""
                    bSuccess = False
                    Exit Do
                End If

                Threading.Thread.Sleep(0)
            Loop

        End If

        Watch.Stop()

        Return IIf(bSuccess, strRet, "")

    End Function
End Class
