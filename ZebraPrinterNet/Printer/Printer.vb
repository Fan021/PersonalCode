Imports System.Xml
Imports System.IO
Imports System.IO.Ports


Public Interface ISerialNo
    Function GetSerialNo() As String
    Property SNName() As String
End Interface

''' <summary>
''' The Library for handle label printer functions. 
''' Now only serial port communication is supported.
''' The print files should include label format file and print content file.
''' </summary>
Public Class Printer
    Friend mCom As VirtualPort
    Friend PrintTxtList As New Dictionary(Of String, String)
    Dim m_ISNGenerator As ISerialNo
    Dim m_strLastLabel As String


    ReadOnly Property Comm() As VirtualPort
        Get
            Return mCom
        End Get
    End Property

    ReadOnly Property PrintContent(ByVal key As String) As String
        Get
            Return PrintTxtList(key.ToUpper)
        End Get
    End Property

    ''' <summary>
    ''' Initialize printer communication.
    ''' </summary>
    ''' <param name="iPort">serial port number</param>
    ''' <param name="Settings">serial port setting string</param>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function Init(ByVal iPort As Integer, ByVal Settings As String) As Integer
        me.mCom = new Serial()
        Return Me.mCom.Init(iPort, Settings)
    End Function


    Public Function InitParallel(ByVal iPort As Integer, ByVal Settings As String) As Integer
        me.mCom = new Parallel()
        Return Me.mCom.Init(iPort, Settings)
    End Function

    Public Function InitNetPrinter(ByVal iport As Integer, ByVal ipAddr As String) As Integer
        Me.mCom = New NetPrinter
        Return Me.mCom.Init(iport, ipAddr)
    End Function
    Public Property SNGenerator() As Object
        Get
            Return m_ISNGenerator
        End Get
        Set(ByVal value As Object)
            If value.GetType.GetInterface("ISerialNo") IsNot Nothing Then
                m_ISNGenerator = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' this function get the field data from variable area in the label print file.
    ''' the prefix is the flag before varialbe
    ''' the suffix is the flag behide variable
    ''' </summary>
    ''' <param name="shortPrintFile">Print file name, short name</param>
    ''' <param name="prefix">prefix string before variable, cannot be null</param>
    ''' <param name="suffix">suffix string behide variable, can be null</param>
    ''' <returns>the field data string</returns>
    Public Function GetField(ByVal shortPrintFile As String, ByVal prefix As String, ByVal suffix As String) As String
        Dim PrintTxt As String
        Try

            PrintTxt = PrintTxtList(shortPrintFile.ToUpper)
            Dim p1 As Integer = PrintTxt.IndexOf(prefix)
            Dim p2 As Integer = PrintTxt.IndexOf(suffix + vbCrLf, p1)
            Dim s As String = PrintTxt.Substring(p1 + prefix.Length, p2 - p1 - prefix.Length)
            Return s
        Catch ex As Exception
            Throw
            Return "ERROR"
        End Try
    End Function


    ''' <summary>
    ''' Load format file to the printer internal memory.
    ''' </summary>
    ''' <param name="FormatFile">format file path and name</param>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function LoadFormatFile(ByVal FormatFile As String) As Integer
        Dim SR As StreamReader
        Try
            'Load Format file
            SR = File.OpenText(FormatFile)
            Do
                'Threading.Thread.Sleep(5)
                mCom.WriteLine(SR.ReadLine)
            Loop Until SR.EndOfStream
            SR.Close()
        Catch ex As Exception
            Throw ex
            Return -1
        End Try
        Return 0
    End Function

    ''' <summary>
    ''' Load Print file content to this class internal variable.
    ''' </summary>
    ''' <param name="PrintFile">Print file path and name</param>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function LoadPrintFile(ByVal PrintFile As String) As Integer

        Dim SR As StreamReader
        Dim fShortName As String
        Dim PrintTxt As String

        Try
            'Load Print file
            fShortName = System.IO.Path.GetFileName(PrintFile).ToUpper
            SR = File.OpenText(PrintFile)
            PrintTxt = SR.ReadToEnd
            SR.Close()
            If PrintTxtList.ContainsKey(fShortName) Then
                PrintTxtList(fShortName) = PrintTxt
            Else
                PrintTxtList.Add(fShortName, PrintTxt)
            End If
        Catch ex As Exception
            Throw
            Return -1
        End Try
        Return 0
    End Function

    ''' <summary>
    ''' this function set the field data to variable area in the label print file.
    ''' the prefix is the flag before variable
    ''' the suffix is the flag after variable
    ''' </summary>
    ''' <param name="shortPrintFile">Print file name, short name</param>
    ''' <param name="preField">prefix string before variable, cannot be null</param>
    ''' <param name="postField">suffix string after variable, can be null</param>
    ''' <param name="sContent">the field data string to be sent</param>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function SetField(ByVal shortPrintFile As String, ByVal preField As String, ByVal postField As String, ByVal sContent As String) As Integer
        Try
            Dim PrintTxt As String = PrintTxtList(shortPrintFile.ToUpper)


            With PrintTxt
                Dim p1 As Integer = .IndexOf(preField)
                Dim p2 As Integer = .IndexOf(postField + vbCrLf, p1)
                Dim s As String = preField + .Substring(p1 + preField.Length, p2 - p1 - preField.Length)
                PrintTxt = .Replace(s, preField + sContent)
            End With
            PrintTxtList(shortPrintFile.ToUpper) = PrintTxt

        Catch e As Exception
            Throw
            Return -1
        End Try
        Return 0
    End Function

    ''' <summary>
    ''' call this function will print label out.
    ''' this function will automatic replace some specific field with defined data.
    ''' for example:
    ''' &lt;Date:yyyyMMdd&gt; stands for 20100426, (if taday is april 26th in 2010year)
    ''' &lt;Time:HHmm&gt; stands for 10:08.
    ''' </summary>
    ''' <param name="shortPrintFile">Print file name, short name</param>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function PrintLabel(ByVal shortPrintFile As String) As Integer
        Try

            Dim p1, p2 As Integer
            Dim s As String = ""
            Dim PrintTxt As String

            PrintTxt = PrintTxtList(shortPrintFile.ToUpper)

            'With PrintTxt
            'Handle Date format
            p1 = PrintTxt.IndexOf("<Date:")
            If p1 > 0 Then
                p2 = PrintTxt.IndexOf(">", p1)
                s = PrintTxt.Substring(p1 + 6, p2 - p1 - 6)
                PrintTxt = PrintTxt.Replace(PrintTxt.Substring(p1, p2 - p1 + 1), Format(Now, s))
            End If

            'Handle Time format
            p1 = PrintTxt.IndexOf("<Time:")
            If p1 > 0 Then
                p2 = PrintTxt.IndexOf(">", p1)
                s = PrintTxt.Substring(p1 + 6, p2 - p1 - 6)
                PrintTxt = PrintTxt.Replace(PrintTxt.Substring(p1, p2 - p1 + 1), Format(Now, s))
            End If

            'Handle SerialNo
            If Me.m_ISNGenerator IsNot Nothing Then
                p1 = PrintTxt.IndexOf("<SN:")
                If p1 > 0 Then
                    p2 = PrintTxt.IndexOf(">", p1)
                    PrintTxt = PrintTxt.Replace(PrintTxt.Substring(p1, p2 - p1 + 1), Me.m_ISNGenerator.GetSerialNo())
                End If
            End If

            mCom.Write(PrintTxt)
            m_strLastLabel = PrintTxt
        Catch e As Exception
            Throw
            Return -1
        End Try
        Return 0

    End Function
    ''' <summary>
    ''' call this function will print the last label out.
    ''' </summary>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function PrintLastLabel() As Integer
        Try
            mCom.Write(m_strLastLabel)
        Catch e As Exception
            Throw
            Return -1
        End Try
        Return 0

    End Function
    ''' <summary>
    ''' Close printer serial communication
    ''' </summary>
    ''' <returns>0 for success, non-zero for fail</returns>
    Public Function Quit() As Integer
        Return me.mCom.Quit()
    End Function

    Public Sub ShowDebugWindow()
        Dim mForm As New frmDebugPrinter
        mForm.mPrinter = Me
        mForm.Show()
    End Sub

End Class
