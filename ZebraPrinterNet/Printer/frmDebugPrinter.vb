Imports System.IO
Public Class frmDebugPrinter

    Friend mPrinter As Printer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If mPrinter Is Nothing Then
                mPrinter = New Printer
            End If

            'get check status
            If RdSerial.Checked Then
                If mPrinter.Init(CInt(TextBox1.Text), TextBox2.Text) = 0 Then
                    StatusLabel.Text = "Printer Initialize OK"
                    Button1.Enabled = False
                    Button2.Enabled = True
                    GroupBox2.Enabled = True
                Else
                    StatusLabel.Text = "Printer Initialize Fail"
                End If
            Else
                If mPrinter.InitParallel(CInt(TextBox1.Text), TextBox2.Text) = 0 Then
                    StatusLabel.Text = "Printer Initialize OK"
                    Button1.Enabled = False
                    Button2.Enabled = True
                    GroupBox2.Enabled = True
                Else
                    StatusLabel.Text = "Printer Initialize Fail"
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If mPrinter Is Nothing Then Return
        mPrinter.Quit()
        GroupBox2.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim dlg As New System.Windows.Forms.OpenFileDialog
            'If dlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '    mPrinter.LoadFormatFile(dlg.FileName)
            '    StatusLabel.Text = dlg.FileName + " file loaded."
            'End If

        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim dlg As New System.Windows.Forms.OpenFileDialog
            'If dlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            '    mPrinter.LoadPrintFile(dlg.FileName)
            '    Dim s As String = Path.GetFileName(dlg.FileName)
            '    If ListBox1.Items.Contains(s) = False Then
            '        ListBox1.Items.Add(s)
            '    End If
            '    StatusLabel.Text = dlg.FileName + " file loaded."
            'End If
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            If ListBox1.SelectedIndex > -1 Then
                mPrinter.PrintLabel(ListBox1.SelectedItem)
            End If
        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub


    Private Sub frmDebugPrinter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If mPrinter Is Nothing Then Return

        If mPrinter.mCom Is Nothing Then
            Button1.Enabled = True
            RdSerial.Enabled = True
            RdParallel.Enabled = True
            Button2.Enabled = False
            GroupBox2.Enabled = False

            RdSerial.Checked = True
        Else
            If TypeOf mPrinter.mCom Is Serial Then
                RdSerial.Checked = True
            Else
                RdParallel.Checked = True
            End If

            With mPrinter.mCom
                TextBox1.Text = .Port
                TextBox2.Text = .Settings
            End With
            Button1.Enabled = False
            Button2.Enabled = True
            GroupBox2.Enabled = True
            For Each s As String In mPrinter.PrintTxtList.Keys
                ListBox1.Items.Add(s)
            Next

        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If ListBox1.SelectedIndex > -1 Then
                mPrinter.SetField(ListBox1.SelectedItem, TextBox3.Text, TextBox4.Text, TextBox5.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            If ListBox1.SelectedIndex > -1 Then
                TextBox5.Text = mPrinter.GetField(ListBox1.SelectedItem, TextBox3.Text, TextBox4.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class