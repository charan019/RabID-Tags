Public Class MbForm
    Dim tagnum As Int16 = 0
    Friend value As String
    Public Shared logger As log4net.ILog
    Friend flag As Boolean = False
    Dim saveFlag As Boolean = False


    Public Sub MsgBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'popup to enter the number of tags to be engraved
        If My.Forms.Form1.mbFlag Then
            EnterTags(My.Forms.Form1.mbValue)
        Else
            'display the popup "saved successfully"
            save()
        End If
        Try
            'Get the logger as named in the configuration file.
            logger = log4net.LogManager.GetLogger("WindowsApplication4")
            logger.Info("Form2_Load() - Start")
            logger.Debug("Form2_Load() - Code Implementation goes here......")
        Catch ex As Exception
            logger.Error("Form1_Load() - " & ex.Message)
        Finally
            logger.Info("Form1_Load() - Finish")
        End Try
    End Sub

    'method to display the message to enter the number of tags
    Public Sub EnterTags(ByVal num As Integer)
        Panel2.Visible = False
        Panel1.Visible = True
        tagnum = num
        If num = 4 Then
            Label1.Text = "Enter the number of tags to be" & Environment.NewLine & "engraved (1-4)"
        Else
            Label1.Text = "Enter the number of tags to be" & Environment.NewLine & "engraved (1-3)"
        End If
        TextBox1.Select()
    End Sub

    'method to display the text
    Public Sub save()
        Panel2.Visible = True
        Panel1.Visible = False
        Label2.Text = "Saved Successfully."
        saveFlag = True
    End Sub

    'OK button to close the popup after viewing the message
    Public Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If saveFlag Then
            Me.Close()
        Else
            Panel1.Visible = True
            Panel2.Visible = False
        End If
    End Sub

    'OK button to close the popup after entering the number of tags to be engraved
    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        value = TextBox1.Text
        logger.Info("v in mbform:" + value)
        If value.Length = 1 Then
            If tagnum = 4 Then
                'for holders with four tags to engrave
                If value = "1" Or value = "2" Or value = "2" Or value = "3" Or value = "4" Then
                    flag = True
                    Me.Close()
                Else
                    Panel1.Visible = False
                    Panel2.Visible = True
                    Label2.Text = "You must enter a number between (1-4)."
                End If
            Else
                'for holders with three tags to engrave
                If value = "1" Or value = "2" Or value = "2" Or value = "3" Then
                    flag = True
                    Me.Close()
                Else
                    Panel1.Visible = False
                    Panel2.Visible = True
                    Label2.Text = "You must enter a number between (1-3)."
                End If
            End If
        Else
            'displaying a message if number other than 1-4 is entered
            If tagnum = 4 Then
                Panel1.Visible = False
                Panel2.Visible = True
                Label2.Text = "You must enter a number between (1-4)."
            Else
                'displaying a message if number other than 1-3 is entered
                Panel1.Visible = False
                Panel2.Visible = True
                Label2.Text = "You must enter a number between (1-3)."
            End If
        End If
        saveFlag = False
    End Sub

    'cancel button to close the popup
    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        flag = False
        Me.Close()
    End Sub

    'disable enter key while entering the number for tags to engrave
    Public Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
        End If
        value = e.KeyCode.ToString
    End Sub
End Class