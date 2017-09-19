Imports System.IO

Public Class Form2
    Dim graphno As Integer = 0
    'Dim dir As String = Directory.GetCurrentDirectory
    Dim dir As String = "C:\Users\kmgrepos\Desktop\charan\project"
    Dim gpath As String = dir + "\tags\4"
    Dim graphfiles() As String = IO.Directory.GetFiles(gpath)
    Dim tFlag As Boolean = False
    Dim tFolder As Integer = 0
    Public Shared logger As log4net.ILog
    Private IsDragging As Boolean = False
    Private StartPoint As Drawing.Point
    Dim t2Focus, t3Focus, t4Focus As Boolean

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'popup to enter the password for the settings menu
        Dim dlg As New LoginForm1
        If dlg.ShowDialog = System.Windows.Forms.DialogResult.Cancel Then
            Me.Close()
        End If
        Panel1.Visible = False
        Panel2.Visible = False
        Label1.Visible = False
        ComboBox1.Visible = False
        Me.Size = New Size(770, 533)
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

    'load the graphic picture into the picturebox
    Private Sub loadGraphics()
        PictureBox1.Load(graphfiles(graphno))
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    'Button click event to add a new graphic
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Size = New Size(770, 533)
        'opens a browse dialog to select the graphic to be added
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFilname As String
        If fd.ShowDialog() = DialogResult.OK Then
            strFilname = fd.FileName
            logger.Info("filename:" + strFilname)
            Dim pos As Integer = strFilname.LastIndexOf("\") + 1
            Dim path As String = strFilname.Substring(pos)
            Try
                'copy the file in the specified path to the appilcation folder
                My.Computer.FileSystem.CopyFile(strFilname, gpath + "\" + path)
                MessageBox.Show("File uploaded successfully.")
            Catch ex As Exception
                MessageBox.Show("A file with same name already exists. Please change the filename and upload.")
            End Try
        End If
    End Sub

    'Button click event to go to the menu to remove a graphic
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        logger.Info("in button5")
        Me.Size = New Size(770, 533)
        loadGraphics()
        Panel1.Visible = True
    End Sub

    'Button click event to add a new tag
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFilname As String = ""
        'upload the picture of the new tag
        If fd.ShowDialog() = DialogResult.OK Then
            strFilname = fd.FileName
            Me.Size = New Size(920, 609)
            Panel2.Visible = True
            PictureBox2.Load(strFilname)
            PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        End If
    End Sub

    Private Sub TagToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        tFlag = True
        Panel1.Visible = True
        Label1.Visible = True
        ComboBox1.Visible = True
        ComboBox1.SelectedItem = "Page 1"
        gpath = dir + "\tags\1"
        graphfiles = IO.Directory.GetFiles(gpath)
        loadGraphics()
    End Sub

    'Button click event to iterate through the graphics to remove a required one
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        logger.Info("combo selected:" + ComboBox1.SelectedItem)
        If graphno = graphfiles.Length - 1 Then
            graphfiles = IO.Directory.GetFiles(gpath)
            graphno = 0
        Else
            graphno += 1
        End If
        PictureBox1.Load(graphfiles(graphno))
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    'Button click event to iterate through the graphics to remove a required one
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        logger.Info("combo selected:" + ComboBox1.SelectedItem)
        logger.Info(graphno.ToString)
        If graphno = 0 Then
            graphno = graphfiles.Length - 1
            logger.Info("In if loop:" + graphno.ToString)
        Else
            graphno -= 1
        End If
        PictureBox1.Load(graphfiles(graphno))
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    'Button click event to remove the graphic selected
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If graphno = graphfiles.Length - 1 Then
            graphno = 0
        Else
            graphno += 1
        End If
        PictureBox1.Load(graphfiles(graphno))
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        If tFlag Then
            MessageBox.Show("Tag deleted successfully")
            Dim f1 As New Form1
            If graphno = 0 Then
                Form1.refreshGraphic(graphfiles(graphfiles.Length - 1))
            Else
                Form1.refreshGraphic(graphfiles(graphno - 1))
            End If
            'My.Computer.FileSystem.DeleteFile(graphfiles(graphno - 1))
            'f1.Show()
        Else
            If graphno = 0 Then
                My.Computer.FileSystem.DeleteFile(graphfiles(graphfiles.Length - 1))
            Else
                My.Computer.FileSystem.DeleteFile(graphfiles(graphno - 1))
            End If
            MessageBox.Show("Graphic deleted successfully")
        End If
        graphno = graphno - 1
    End Sub

    'a combo box to place the new tag in selected page
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        If tFlag Then
            If ComboBox1.SelectedItem = "Page3" Then
                gpath = dir + "\tags\3"
            ElseIf ComboBox1.SelectedItem = "Page2" Then
                gpath = dir + "\tags\2"
            ElseIf ComboBox1.SelectedItem = "Page1" Then
                gpath = dir + "\tags\1"
            End If
            graphfiles = IO.Directory.GetFiles(gpath)
            PictureBox1.Load(graphfiles(graphno))
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        End If
    End Sub

    'Button click event to go back
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Panel1.Visible = False
    End Sub

    'Button click event to go home to the settings menu
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Me.Size = New Size(770, 533)
        Panel2.Visible = False
    End Sub

    'Method to determine if the focus is in richtextbox2
    Private Sub RichTextbox2_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox2.GotFocus
        t2Focus = True
        t3Focus = False
        t4Focus = False
    End Sub

    'Method to determine if the focus is in richtextbox3
    Private Sub RichTextbox3_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox3.GotFocus
        t2Focus = False
        t3Focus = True
        t4Focus = False
    End Sub

    'Method to determine if the focus is in richtextbox4
    Private Sub RichTextbox4_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox4.GotFocus
        t2Focus = False
        t3Focus = False
        t4Focus = True
    End Sub

    'Method to drag the textbox to fit into the tag
    Private Sub RichTextBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox2.MouseDown
        StartPoint = RichTextBox2.PointToScreen(New Drawing.Point(e.X, e.Y))
        IsDragging = True
    End Sub

    'Method to move the textbox using mouse clicl to fit into the new tag
    Private Sub RichTextBox2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox2.MouseMove
        If IsDragging Then
            Dim EndPoint As Drawing.Point = RichTextBox2.PointToScreen(New Drawing.Point(e.X, e.Y))
            RichTextBox2.Left += (EndPoint.X - StartPoint.X)
            RichTextBox2.Top += (EndPoint.Y - StartPoint.Y)
            StartPoint = EndPoint
        End If
    End Sub

    'Mouseup event to disable the dragging once mouse is released
    Private Sub RichTextBox2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox2.MouseUp
        IsDragging = False
    End Sub

    'MouseDown event to enable the dragging of the textbox
    Private Sub RichTextBox3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox3.MouseDown
        StartPoint = RichTextBox3.PointToScreen(New Drawing.Point(e.X, e.Y))
        IsDragging = True
    End Sub

    'MouseMove event to move the textbox
    Private Sub RichTextBox3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox3.MouseMove
        If IsDragging Then
            Dim EndPoint As Drawing.Point = RichTextBox3.PointToScreen(New Drawing.Point(e.X, e.Y))
            RichTextBox3.Left += (EndPoint.X - StartPoint.X)
            RichTextBox3.Top += (EndPoint.Y - StartPoint.Y)
            StartPoint = EndPoint
        End If
    End Sub

    Private Sub RichTextBox3_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox3.MouseUp
        IsDragging = False
    End Sub

    Private Sub RichTextBox4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox4.MouseDown
        StartPoint = RichTextBox4.PointToScreen(New Drawing.Point(e.X, e.Y))
        IsDragging = True
    End Sub

    'Submit event to save the positions of the textbox inside the tag
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim Loc As String = PictureBox2.ImageLocation
        Dim pos As Integer = Loc.LastIndexOf(".") + 1
        logger.Info("pos:" + pos.ToString)
        Dim textfile, c As String
        c = ""
        Dim count As Integer = 0
        textfile = dir + "\tags\meta\counter.txt"
        'increase the count of the number of tags
        If IO.File.Exists(textfile) Then
            Dim readlines() As String = IO.File.ReadAllLines(textfile)
            logger.Info(readlines.Length.ToString)
            While readlines IsNot Nothing And count < readlines.Length
                c = readlines(count)
                count += 1
            End While
            logger.Info("counter:" + c)
        End If
        RichTextBox1.Text = c + "." + Loc.Substring(pos)
        logger.Info(RichTextBox1.Text)
        'save the locations of the textbox
        If RichTextBox2.Visible Then
            RichTextBox1.AppendText(Environment.NewLine + "RichTextBox2:" + RichTextBox2.Location.X.ToString + "," + RichTextBox2.Location.Y.ToString + "," + RichTextBox2.Width.ToString + "," + RichTextBox2.Height.ToString)
        Else
            RichTextBox1.AppendText(Environment.NewLine + "RichTextBox2:NULL")
        End If
        If RichTextBox3.Visible Then
            RichTextBox1.AppendText(Environment.NewLine + "RichTextBox3:" + RichTextBox3.Location.X.ToString + "," + RichTextBox3.Location.Y.ToString + "," + RichTextBox3.Width.ToString + "," + RichTextBox3.Height.ToString)
        Else
            RichTextBox1.AppendText(Environment.NewLine + "RichTextBox3:NULL")
        End If
        If RichTextBox4.Visible Then
            RichTextBox1.AppendText(Environment.NewLine + "RichTextBox4:" + RichTextBox4.Location.X.ToString + "," + RichTextBox4.Location.Y.ToString + "," + RichTextBox4.Width.ToString + "," + RichTextBox4.Height.ToString)
        Else
            RichTextBox1.AppendText(Environment.NewLine + "RichTextBox4:NULL")
        End If
        My.Computer.FileSystem.CopyFile(Loc, dir + "\tags\new\" + c + "." + Loc.Substring(pos))
        MessageBox.Show("Tag inserted successfully.")
        'save the updated count of the tags
        If IO.File.Exists(textfile) Then
            Dim objWriter As New System.IO.StreamWriter(textfile)
            Dim counter As Integer = Convert.ToInt16(c) + 1
            objWriter.Write(counter.ToString)
            objWriter.Close()
        End If
        Dim mFile As String = dir + "\textmeasures.txt"
        'save the textbox positions to a file
        If IO.File.Exists(mFile) Then
            Dim objWriter As New System.IO.StreamWriter(mFile, True)
            Dim lines As String() = RichTextBox1.Lines
            objWriter.WriteLine()
            objWriter.WriteLine(lines(0))
            objWriter.WriteLine(lines(1))
            objWriter.WriteLine(lines(2))
            objWriter.WriteLine(lines(3))
            objWriter.Close()
        End If
    End Sub

    'increase the width of the richtextbox
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If t2Focus Then
            RichTextBox2.Size = New Size(RichTextBox2.Width + 5, RichTextBox2.Height)
        ElseIf t3Focus Then
            RichTextBox3.Size = New Size(RichTextBox3.Width + 5, RichTextBox3.Height)
        ElseIf t4Focus Then
            RichTextBox4.Size = New Size(RichTextBox4.Width + 5, RichTextBox4.Height)
        End If
    End Sub

    'decrease the width of the richtextbox
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If t2Focus Then
            RichTextBox2.Size = New Size(RichTextBox2.Width - 5, RichTextBox2.Height)
        ElseIf t3Focus Then
            RichTextBox3.Size = New Size(RichTextBox3.Width - 5, RichTextBox3.Height)
        ElseIf t4Focus Then
            RichTextBox4.Size = New Size(RichTextBox4.Width - 5, RichTextBox4.Height)
        End If
    End Sub

    'increase the height of the richtextbox
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If t2Focus Then
            RichTextBox2.Size = New Size(RichTextBox2.Width, RichTextBox2.Height + 5)
        ElseIf t3Focus Then
            RichTextBox3.Size = New Size(RichTextBox3.Width, RichTextBox3.Height + 5)
        ElseIf t4Focus Then
            RichTextBox4.Size = New Size(RichTextBox4.Width, RichTextBox4.Height + 5)
        End If
    End Sub

    'decreasse the height of the richtextbox
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If t2Focus Then
            RichTextBox2.Size = New Size(RichTextBox2.Width, RichTextBox2.Height - 5)
        ElseIf t3Focus Then
            RichTextBox3.Size = New Size(RichTextBox3.Width, RichTextBox3.Height - 5)
        ElseIf t4Focus Then
            RichTextBox4.Size = New Size(RichTextBox4.Width, RichTextBox4.Height - 5)
        End If
    End Sub

    'to make the selected richtextbox visible
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If ComboBox2.SelectedIndex > -1 Then
            Dim text As String = ComboBox2.SelectedItem.ToString
            If text.Equals("TextBox1") Then
                RichTextBox3.Visible = True
            ElseIf text.Equals("TextBox2") Then
                RichTextBox2.Visible = True
            ElseIf text.Equals("TextBox3") Then
                RichTextBox4.Visible = True
            End If
        End If
    End Sub

    'to hide the selected richtextbox
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If ComboBox2.SelectedIndex > -1 Then
            Dim text As String = ComboBox2.SelectedItem.ToString
            If text.Equals("TextBox1") Then
                RichTextBox3.Visible = False
            ElseIf text.Equals("TextBox2") Then
                RichTextBox2.Visible = False
            ElseIf text.Equals("TextBox3") Then
                RichTextBox4.Visible = False
            End If
        End If
    End Sub

    'add a plt file for the new graphic
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Me.Size = New Size(770, 533)
        Dim fd As OpenFileDialog = New OpenFileDialog()
        Dim strFilname As String
        Dim count As Integer = 0
        Dim path As String = dir + "\tags\4\plt"
        If (Not System.IO.Directory.Exists(path)) Then
            System.IO.Directory.CreateDirectory(path)
        End If
        'upload the new graphic plt file
        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            strFilname = OpenFileDialog2.FileName
            logger.Info("filename:" + strFilname)
            Dim pos As Integer = strFilname.LastIndexOf("\") + 1
            Dim gpath As String = strFilname.Substring(pos)
            Try
                My.Computer.FileSystem.CopyFile(strFilname, path + "\" + gpath)
                MessageBox.Show("File uploaded successfully.")
            Catch ex As Exception
                logger.Info(ex.ToString)
                MessageBox.Show("A file with same name already exists. Please change the filename and upload.")
            End Try
        End If
    End Sub

    'reset the count of the number of tags engraved
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim objWriter As New System.IO.StreamWriter(dir + "\tags\meta\TotalTags.txt")
        objWriter.WriteLine("0")
        objWriter.Close()
        MessageBox.Show("Reset Done. Please restart the application for the changes to reflect.")
    End Sub

    'Mousemove event to move the richtextbox
    Private Sub RichTextBox4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox4.MouseMove
        If IsDragging Then
            Dim EndPoint As Drawing.Point = RichTextBox4.PointToScreen(New Drawing.Point(e.X, e.Y))
            RichTextBox4.Left += (EndPoint.X - StartPoint.X)
            RichTextBox4.Top += (EndPoint.Y - StartPoint.Y)
            StartPoint = EndPoint
        End If
    End Sub

    'disable the dragging on mouseup
    Private Sub RichTextBox4_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox4.MouseUp
        IsDragging = False
    End Sub

End Class