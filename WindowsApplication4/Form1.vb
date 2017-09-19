Imports System.Text
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.ComponentModel

Public Class Form1
    Dim graphno As Integer = 0
    'Dim dir As String = Directory.GetCurrentDirectory
    Dim dir As String = "C:\Users\kmgrepos\Desktop\charan\project"
    Dim graphfiles() As String = IO.Directory.GetFiles(dir + "\tags\4")
    Dim h1, w1, h2, w2, h3, w3, p1, p2, p3, p4, p5, f1, l3, b3, RtbShift As String
    Dim t2Focus, t3Focus, t1Focus, tFlag, rFlag, nFlag, engraveFlag As Boolean
    Dim fontList As New ArrayList()
    Dim fontNo As Integer = 0
    Dim lines As Integer = 3
    Dim newLine As Boolean = False
    Dim cursorPos1, cursorPos2, cursorPos3, tagNo As Integer
    Dim rh1, rh2, rh3, rh4, rh5, rh6, rh7, total As Integer
    Dim rts1, rts2, rts3, rts4, rts5, rts6, rts7 As Integer
    Dim rtf1, rtf2, rtf3, rtf4, rtf5, rtf6, rtf7 As String
    Dim fs1, fs2, fs3, fs4, fs5, fs6, fs7 As Integer
    Dim ft1, ft2, ft3, ft4, ft5, ft6, ft7 As String
    Dim fst1, fst2, fst3, fst4, fst5, fst6, fst7 As FontStyle
    Dim destPath As String = "final.plt"
    Dim odiff As Integer = 0
    Dim ldiff As Integer = 0
    Dim path As String = dir + "\tags\meta\characters\"
    Public Shared logger As log4net.ILog
    Dim xmin, ymin, xmax, ymax As New ArrayList()
    Dim fontsize As Integer() = {12, 14, 16, 18, 24, 36, 48}
    Dim fsi As Integer = fontsize(6)
    Dim findex As Integer = 6
    Dim lnum As Integer = 1
    Dim plates As Integer = 1
    Dim finalfontsize As Integer = 1
    Dim finalfontname As String = ""
    Dim totalTags As Integer = 0
    Dim rtb2flag As Boolean = False
    Dim commaFlag As Boolean = False
    Dim gFlag As Boolean = False
    Dim rtb1flag As Boolean = False
    Dim rtb3flag As Boolean = False
    Dim multiflag As Boolean = False
    Dim graphic As String = graphfiles(0)
    Dim oldfontsize As Integer = 0
    Dim xminlist As New ArrayList 'For spacing between characters
    Dim textFlag As Boolean = False 'For erasing text
    Dim tab3Flag As Boolean = False 'For navigating using arrows
    Dim enlargeFlag As Boolean = False
    Public mbFlag As Boolean = False
    Public mbValue As Integer = 0
    Dim maxw As Integer = 0
    Dim maxw2 As Integer = 0
    Dim minc As Integer = 0
    Dim currentw As Integer = 0
    Dim setupFlag As Boolean = False
    Dim caretpos As Integer = 0
    Dim ffsList As New ArrayList
    Dim enterflag As Boolean = True
    Dim emptyFlag As Boolean = False
    Dim tagText As String = ""
    Dim win As Int16 = 0
    Dim ldiffList As New ArrayList
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.FormBorderStyle = FormBorderStyle.None
        'Me.WindowState = FormWindowState.Maximized
        Try
            'Get the logger as named in the configuration file.
            logger = log4net.LogManager.GetLogger("WindowsApplication4")
            logger.Info("Form1_Load() - Start")
            logger.Debug("Form1_Load() - Code Implementation goes here......")
        Catch ex As Exception
            logger.Error("Form1_Load() - " & ex.ToString)
        Finally
            logger.Info("Form1_Load() - Finish")
        End Try
        logger.Info("Current Directory:" + dir)
        'Initializing the variables and elements
        f1 = ""
        Label2.Text = "[in TEXT mode]"
        Label3.Text = "(or press G" & Environment.NewLine & "to engrave" & Environment.NewLine & "with graphics)"
        PictureBox1.Image = System.Drawing.Image.FromFile(dir + "\tags\meta\G.GIF")
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        Button1.Enabled = False
        Button3.Enabled = False
        'load the tags for initial home screen
        GetTags(1)
        'store the fonts to the list
        getFonts()
        'get the count of total number of tags engraved
        getTotalTags()
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Center
        RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size + 60, RichTextBox2.Font.Style)
        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
        RichTextBox3.SelectionAlignment = HorizontalAlignment.Center
        RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size, RichTextBox2.Font.Style)
        RichTextBox1.Font = New Font("Arial Narrow", RichTextBox1.Font.Size, RichTextBox1.Font.Style)
        RichTextBox3.Font = New Font("Arial Narrow", RichTextBox3.Font.Size, RichTextBox3.Font.Style)
        l3 = RichTextBox3.Location.Y
        b3 = RichTextBox3.Size.Height
        RtbShift = ""
        tFlag = False
        rFlag = False
        nFlag = False
        rts1 = RichTextBox1.Font.Size
        rts2 = RichTextBox1.Font.Size
        rts3 = RichTextBox2.Font.Size
        rts4 = RichTextBox2.Font.Size
        rts5 = RichTextBox2.Font.Size
        rts6 = RichTextBox3.Font.Size
        rts7 = RichTextBox3.Font.Size
        rtf1 = RichTextBox1.Font.Name
        rtf2 = RichTextBox1.Font.Name
        rtf3 = RichTextBox2.Font.Name
        rtf4 = RichTextBox2.Font.Name
        rtf5 = RichTextBox2.Font.Name
        rtf6 = RichTextBox3.Font.Name
        rtf7 = RichTextBox3.Font.Name
        fs1 = 0
        fs2 = 0
        fs3 = 0
        fs4 = 0
        fs5 = 0
        fs6 = 0
        fs7 = 0
        ft1 = ""
        ft2 = ""
        ft3 = ""
        ft4 = ""
        ft5 = ""
        ft6 = ""
        ft7 = ""
        engraveFlag = False
        Label16.Visible = False
        Label17.Visible = False
        Label18.Visible = False
        Label19.Visible = False
        Label20.Visible = False
        Label22.Visible = False
        Button23.Visible = True
        Button24.Visible = True
        Button25.Visible = True
        Label21.Text = "Compressed"
        'Loading the id to be engraved for rabid tags
        Dim sr = New StreamReader(dir + "\tags\meta\rabidcount.txt")
        Do While sr.Peek() >= 0
            Dim line As String = sr.ReadLine
            Label5.Text = line
        Loop
        sr.Close()
    End Sub

    'KeyDown Event for the entire form to detect keyboard controls and settings menu.
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        'F8 Control to open Settings Menu
        If e.KeyCode = Keys.F8 Then
            Dim f2 = New Form2()
            f2.Show()
        End If
        'Ctrl-N to clear text in the RichTextBox
        If (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control And textFlag Then
            If RichTextBox2.Text.Length > 0 Then
                RichTextBox1.Text = ""
                RichTextBox2.Text = ""
                RichTextBox3.Text = ""
                enterflag = True
                Label21.Text = "Compressed"
                RichTextBox2.Font = New Font(RichTextBox2.Font.Name, Convert.ToSingle(108), RichTextBox2.Font.Style)
                'For Military tags, cursor starts at the left hand side
                If tagNo = 20 Or tagNo = 22 Then
                    RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                Else
                    RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                End If
                'If Rabies Tag, clear all the text except the Rabies ID
                If Label1.Text.Contains("Rabies/ID") Then
                    rFlag = True
                    AdjustRichText(1)
                    AdjustHeight()
                End If
            End If
        End If
        'Ctrl-F to change the Font on the selected line
        If (e.KeyCode And Not Keys.Modifiers) = Keys.F AndAlso e.Modifiers = Keys.Control And textFlag Then
            Button15_Click(sender, e)
        End If
        'Shift-Up to increase the font on the selected line
        If (e.KeyCode And Not Keys.Modifiers) = Keys.Up AndAlso e.Modifiers = Keys.Shift And textFlag Then
            Button16_Click(sender, e)
        End If
        'Shift-Down to decrease the font on the selected line
        If (e.KeyCode And Not Keys.Modifiers) = Keys.Down AndAlso e.Modifiers = Keys.Shift And textFlag Then
            Button17_Click(sender, e)
        End If
        'Change to Text Mode, if in Graphics mode
        'enlargeFlag - True means it is already in text mode
        'enlargeFlag - False means it is in Graphics mode
        If e.KeyCode = Keys.T And Not enlargeFlag Then
            Button1_Click(sender, e)
        ElseIf e.KeyCode = Keys.G And Not enlargeFlag Then
            'Change to Graphics Mode, if in Text mode
            Button2_Click(sender, e)
        End If
        'Esc to go to the Home page
        If e.KeyCode = Keys.Escape Then
            Button13_Click(sender, e)
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.G AndAlso e.Modifiers = Keys.Control And textFlag Then
            'Ctrl-G to Engrave
            Button12_Click(sender, e)
        End If
        'Up arrow to change the graphic displayed
        If e.KeyCode = Keys.Up And Label10.Text.Contains("GRAPHIC") Then
            Button15_Click(sender, e)
        End If
        'Down arrow to change the graphic displayed
        If e.KeyCode = Keys.Down And Label10.Text.Contains("GRAPHIC") Then
            Button14_Click(sender, e)
        End If
    End Sub

    'Method for loading the type of fonts to the fontList ArrayList.
    Private Sub getFonts()
        fontList.Add("Arial Narrow")
        fontList.Add("Arial")
        fontList.Add("Arial Bold")
        fontList.Add("Arial Narrow Bold")
        fontList.Add("Monotype Corsiva")
    End Sub

    'Method for getting the count for total number of tags engraved
    Public Sub getTotalTags()
        Try
            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(dir + "\tags\meta\TotalTags.txt")
            Label9.Text = fileReader
            totalTags = Convert.ToInt16(fileReader)
        Catch ex As Exception
            logger.Error("exception in getTotalTags():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method for saving/updating the total number of tags engraved count
    Public Sub setTotalTags(ByVal count As Integer)
        Try
            Dim sw As StreamWriter = New StreamWriter(dir + "\tags\meta\TotalTags.txt")
            sw.Write(count)
            sw.Close()
        Catch ex As Exception
            logger.Error("exception in setTotalTags():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button click event to change into GRAPHICS Mode
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label2.Text = "[in GRAPHICS mode]"
        Label3.Text = "(or press T" & Environment.NewLine & "to engrave" & Environment.NewLine & "with text)"
        PictureBox1.Image = System.Drawing.Image.FromFile(dir + "\tags\meta\T.GIF")
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        Button2.Enabled = False
        Button1.Enabled = True
    End Sub

    'Button click event to change into TEXT Mode
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label2.Text = "[in TEXT mode]"
        Label3.Text = "(or press G" & Environment.NewLine & "to engrave" & Environment.NewLine & "with graphics)"
        PictureBox1.Image = System.Drawing.Image.FromFile(dir + "\tags\meta\G.GIF")
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        Button1.Enabled = False
        Button2.Enabled = True
    End Sub

    'Method to load the pictures of tags into the PictureBox and AutoSizing the picture
    Public Sub GetTags(i As Integer)
        'First/Home Screen
        If i = 1 Then
            Dim files() As String = IO.Directory.GetFiles(dir + "\tags\1")
            win = 0
            PictureBox2.Load(files(0))
            PictureBox3.Load(files(1))
            PictureBox4.Load(files(2))
            PictureBox5.Load(files(3))
            PictureBox6.Load(files(4))
            PictureBox7.Load(files(5))
            PictureBox8.Load(files(6))
            PictureBox9.Load(files(7))
            PictureBox10.Load(files(8))
            PictureBox11.Load(files(9))
            PictureBox12.Load(files(10))
            PictureBox13.Load(files(11))
            PictureBox14.Load(files(12))
            PictureBox15.Load(files(13))
            PictureBox16.Load(files(14))
            PictureBox2.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox3.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox4.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox5.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox6.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox7.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox8.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox9.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox10.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox11.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox12.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox13.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox14.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox15.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox16.SizeMode = PictureBoxSizeMode.AutoSize
        End If
        'Second Screen
        If i = 2 Then
            win = 1
            Dim files() As String = IO.Directory.GetFiles(dir + "\tags\2")
            'Console.WriteLine(files(7))
            logger.Info("fils length:" + files.Length.ToString)
            PictureBox17.Load(files(0))
            PictureBox18.Load(files(1))
            PictureBox19.Load(files(2))
            PictureBox20.Load(files(3))
            PictureBox21.Load(files(4))
            PictureBox17.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox18.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox19.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox20.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox21.SizeMode = PictureBoxSizeMode.StretchImage
        End If
        'Rabies/Info Screen
        If i = 3 Then
            win = 2
            Dim files() As String = IO.Directory.GetFiles(dir + "\tags\3")
            PictureBox26.Load(files(0))
            PictureBox27.Load(files(1))
            PictureBox28.Load(files(2))
            PictureBox29.Load(files(3))
            PictureBox30.Load(files(4))
            PictureBox31.Load(files(5))
            PictureBox32.Load(files(6))
            PictureBox33.Load(files(7))
            PictureBox34.Load(files(8))
            PictureBox26.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox27.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox28.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox29.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox30.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox31.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox32.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox33.SizeMode = PictureBoxSizeMode.AutoSize
            PictureBox34.SizeMode = PictureBoxSizeMode.AutoSize
        End If
        'New screen if any new tags are added
        If i = 4 Then
            win = 3
            Dim files() As String = IO.Directory.GetFiles(dir + "\tags\new")
            Dim len = files.Length
            'Console.WriteLine(files(7))
            PictureBox43.Load(files(0))
            PictureBox43.SizeMode = PictureBoxSizeMode.AutoSize
            If len = 2 Then
                PictureBox35.Load(files(1))
                PictureBox35.SizeMode = PictureBoxSizeMode.AutoSize
            ElseIf len = 3 Then
                PictureBox36.Load(files(2))
                PictureBox36.SizeMode = PictureBoxSizeMode.AutoSize
            ElseIf len = 4 Then
                PictureBox37.Load(files(3))
                PictureBox37.SizeMode = PictureBoxSizeMode.AutoSize
                PictureBox38.Load(files(4))
                PictureBox38.SizeMode = PictureBoxSizeMode.AutoSize
                PictureBox39.Load(files(5))
                PictureBox39.SizeMode = PictureBoxSizeMode.AutoSize
                PictureBox40.Load(files(6))
                PictureBox40.SizeMode = PictureBoxSizeMode.AutoSize
                PictureBox41.Load(files(7))
                PictureBox41.SizeMode = PictureBoxSizeMode.AutoSize
                PictureBox42.Load(files(8))
                PictureBox42.SizeMode = PictureBoxSizeMode.AutoSize
            End If
        End If
    End Sub

    'Button Click event for "Next" Button to navigate to the second page
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectTab(1)
        GetTags(2)
        Button23.Visible = False
        Button24.Visible = False
        Button25.Visible = False
    End Sub

    'Button Click event for "Previous" Button to navigate to the home page
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TabControl1.SelectTab(0)
        Button23.Visible = True
        Button24.Visible = True
        Button25.Visible = True
    End Sub

    'Button Click event for "Next" Button to navigate to the Rabies tag engraving page
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TabControl1.SelectTab(3)
        Label1.Text = "Rabies/ID tag" & Environment.NewLine & "Select a tag to engrave:"
        Panel1.Visible = True
        GetTags(3)
    End Sub

    'Button Click event for "Previous" Button common for engraving Rabies and Info tags
    'and to set up Rabies Tag
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        engraveFlag = False
        If Panel1.Visible Then
            TabControl1.SelectTab(3)
            If Label1.Text.Contains("set up") And Label1.Text.Contains("Rabies/ID") Then
                Label1.Text = "Info/ID tag" & Environment.NewLine & "Select a tag to engrave:"
                engraveFlag = True
            ElseIf Not Label1.Text.Contains("set up") And Label1.Text.Contains("Info/ID") Then
                Label1.Text = "Rabies/ID tag" & Environment.NewLine & "Select a tag to engrave:"
            ElseIf Label1.Text.Contains("set up") And Label1.Text.Contains("Info/ID") Then
                Label1.Text = "Select to set up" & Environment.NewLine & "Rabies/ID tag:"
            ElseIf Not Label1.Text.Contains("set up") And Label1.Text.Contains("Rabies/ID") Then
                TabControl1.SelectTab(1)
                Label1.Text = "Select to engrave a tag:"
                Panel1.Visible = False
            End If
        Else
            If nFlag Then
                TabControl1.SelectTab(4)
            Else
                TabControl1.SelectTab(1)
            End If
            Label1.Text = "Select to engrave a tag:"
            Panel1.Visible = False
        End If
    End Sub

    'Button Click event for "Next" Button common for engraving Info tags
    'and to set up Rabies Tags and Info Tags
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Panel1.Visible And Label1.Text.Contains("set up") And Label1.Text.Contains("Info/ID") Then
            Panel2.Visible = True
            Panel1.Visible = False
            TextBox1.Focus()
        Else
            TabControl1.SelectTab(3)
            Panel1.Visible = True
            If Label1.Text.Contains("Rabies/ID") And Not Label1.Text.Contains("set up") Then
                Label1.Text = "Info/ID tag" & Environment.NewLine & "Select a tag to engrave:"
                engraveFlag = True
            ElseIf Not Label1.Text.Contains("set up") And Label1.Text.Contains("Info/ID") Then
                Label1.Text = "Select to set up" & Environment.NewLine & "Rabies/ID tag:"
            ElseIf Label1.Text.Contains("set up") And Label1.Text.Contains("Rabies/ID") Then
                Label1.Text = "Select to set up" & Environment.NewLine & "Info/ID tag:"
                engraveFlag = True
            End If
        End If
    End Sub

    'Button Click event for "Previous" Button to navigate from Rabies Tag engraving page to second page
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TabControl1.SelectTab(1)
        Label1.Text = "Select to engrave a tag:"
        Panel1.Visible = False
    End Sub

    'Button Click event for "Next" Button to navigate from second page to Rabies Tag engraving page
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TabControl1.SelectTab(3)
        Label1.Text = "Rabies/ID tag" & Environment.NewLine & "Select a tag to engrave:"
        GetTags(3)
    End Sub

    'KeyPress event for the textbox to enter the id for Rabies ID engraving to check for numbers and length of the id
    Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBox1.KeyPress
        'Screen to change the Rabies ID
        If Panel2.Visible Then
            '0-9 digits allowed and length should be less than 8
            If e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57) And TextBox1.Text.Length < 8 Then
                e.Handled = False
            ElseIf e.KeyChar = ChrW(45) Then
                'hyphen(-) is allowed
                e.Handled = False
            Else
                e.Handled = True
            End If
            'When enter button is pressed, the entered id is displayed on the screen
            If e.KeyChar = ChrW(13) And TextBox1.Text.Length > 0 Then
                Label5.Text = TextBox1.Text
                Dim sw = New StreamWriter(dir + "\tags\meta\rabidcount.txt")
                sw.WriteLine(TextBox1.Text)
                sw.Close()
                TextBox1.Text = ""
            End If
        End If
    End Sub

    'Button Click event for "Previous" Button to navigate from final(id change) page to Info setup page
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Panel2.Visible = False
        TabControl1.SelectTab(3)
        Panel1.Visible = True
        Label1.Text = "Select to set up" & Environment.NewLine & "Info/ID tag:"
        engraveFlag = True
        logger.Info("label text:" + Label1.Text + ",index:" + TabControl1.SelectedIndex.ToString)
    End Sub

    'TabPage enter event to Zoom the picture of the tag upon selection of a tag
    Private Sub TabPage3_Enter(sender As Object, e As EventArgs) Handles TabPage3.Enter
        If tab3Flag Then
            logger.Info("tabpage3 enter")
            Panel3.Visible = True
            PictureBox44.Load(graphfiles(graphno))
            PictureBox44.SizeMode = PictureBoxSizeMode.Zoom
            If Not Button1.Enabled Then
                Label11.Text = "Change Font"
            ElseIf Not Button2.Enabled Then
                Label11.Text = Convert.ToString(graphno + 1) + "/" + Convert.ToString(graphfiles.Length)
            End If
        End If
    End Sub

    'Method to Enlarge the tag picture and 
    'insert textboxes and load the text saved for Rabies and Info ID tags within the boundary of the tag in text mode
    'and display the graphic in the center of the tag in graphics mode
    Private Sub EnlargeImage(Loc As String)
        enlargeFlag = True
        logger.Info("in enlarge image")
        'Label for Escape
        Label16.Visible = True
        'Label for Engrave
        Label17.Visible = True
        'Button for Open Door
        Button23.Visible = False
        'Button for Close Door
        Button24.Visible = False
        'Button to exit
        Button25.Visible = False
        Try
            Dim pos As Integer = Loc.LastIndexOf("\") + 1
            tagNo = Loc.Substring(pos).Split(".")(0)
            Dim path As String = Loc.Replace(Loc.Substring(pos), "large\" + Loc.Substring(pos))
            If nFlag And TabControl1.SelectedIndex = 4 Then
                path = Loc
            End If
            'Load the picture to be enlarged into the picturebox
            PictureBox25.Load(path)
            PictureBox25.SizeMode = PictureBoxSizeMode.Zoom
            tab3Flag = True
            TabControl1.SelectTab(2)
            tab3Flag = False
            logger.Info("Enlarge image path:" + path)
            'if in Text Mode
            If Not Button1.Enabled Then
                enterflag = True
                'For Military tags, cursor starts at the left hand side
                If tagNo = 20 Or tagNo = 22 Then
                    RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                Else
                    RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                End If
                'Labels for Keyboard control instructions
                Label21.Visible = True
                Label18.Visible = True
                Label19.Visible = True
                Label20.Visible = True
                Label22.Visible = True
                If My.Computer.Keyboard.CapsLock Then
                    Label22.Text = "Caps Lock:" & Environment.NewLine & "ON"
                Else
                    Label22.Text = "Caps Lock:" & Environment.NewLine & "OFF"
                End If
                textFlag = True
                logger.Info("in typing")
                Label7.Text = "BEGIN TYPING"
                Label10.Text = "" & Environment.NewLine & "FONT SIZE:"
                PictureBox44.Visible = False
                'Making the richtextbox visible
                RichTextBox2.Visible = True
                RichTextBox3.Visible = True
                RichTextBox1.Visible = True
                'Enabling the font increase and decrease buttons
                Button16.Visible = True
                Button17.Visible = True
                'To adjust the richtextbox inside the boundary of the tag picture
                AdjustTextBox(path.Substring(path.LastIndexOf("\") + 1))
                logger.Info("out of adj")
                'if Rabies or Info Tag get the stored text from the respective textfile
                If Label1.Text.Contains("Rabies/ID") Then
                    multiflag = True
                    rFlag = True
                    logger.Info("in rabies loop:" + Label5.Text)
                    'retrieve the text
                    AdjustRichText(0)
                    'autosize the text to fit into the tag
                    AdjustHeight()
                    logger.Info("after assign")
                    'if the page is to setup rabies tag, change the button text to save
                    If Label1.Text.Contains("set up") Then
                        Button12.Text = "Save"
                        setupFlag = True
                    End If
                ElseIf Label1.Text.Contains("Info/ID") Then
                    'retrieve the text from the info file
                    AdjustRichText(0)
                    'autosize the text to fit into the tag
                    AdjustHeight()
                    'if the page is to setup info tag, change the button text to save
                    If Label1.Text.Contains("set up") Then
                        Button12.Text = "Save"
                    End If
                ElseIf tFlag Then
                    'get the text already written in another individual tag(this is temporary i.e., clears when software is closed)
                    AdjustRichText(0)
                    AdjustHeight()
                End If
                RichTextBox2.Select()
                'Change the font type label to compressed since it is default one
                If RichTextBox2.Text.Length = 0 Then
                    Label21.Text = "Compressed"
                End If
            ElseIf Not Button2.Enabled Then
                'if the application is in Graphics mode
                Label21.Visible = False
                logger.Info("in graphics")
                Label7.Text = "PICK A" & Environment.NewLine & "GRAPHIC"
                Label10.Text = "[Up]" & Environment.NewLine & "[Down]" & Environment.NewLine & "CHOOSE" & Environment.NewLine & "DIFFERENT" & Environment.NewLine & "GRAPHIC"
                'Make the picturebox visible to display the graphics
                PictureBox44.Visible = True
                RichTextBox2.Visible = False
                RichTextBox3.Visible = False
                RichTextBox1.Visible = False
                'Remove the Font increase and decrease buttons since it is not required in graphics mode
                Button16.Visible = False
                Button17.Visible = False
                'For military or wider and smaller in length tags, reduce the size of the picturebox
                If tagNo = 18 Or tagNo = 23 Or tagNo = 21 Then
                    PictureBox44.Width = 237
                    PictureBox44.Height = 100
                    PictureBox44.Location = New Point(PictureBox44.Location.X, 220)
                End If
            End If
            'Disbale the text and graphics mode panel, if it is visible
            If Panel1.Visible Then
                Panel1.Visible = False
            End If
            If engraveFlag Then
                multiflag = True
            End If
        Catch ex As Exception
            logger.Error("exception in EnlargeImage():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to delete the grpahic and refresh the list
    Public Shared Sub refreshGraphic(ByVal file As String)
        Form1.Close()
        My.Computer.FileSystem.DeleteFile(file)
        Form1.Show()
    End Sub

    'Method to Adjust the width of the text if it is edited or its font is increased
    Public Sub Adjustwidth(ByVal i As Integer, ByVal fontname As String, ByVal fstyle As FontStyle)
        Try
            If i = 2 Then
                'Get the Height and Width of the text in the RichTextBox
                Dim g2 As Graphics = RichTextBox2.CreateGraphics
                Dim orgFont2 As New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                Dim sz2 As SizeF = g2.MeasureString(RichTextBox2.SelectedText, orgFont2)
                h2 = sz2.Height
                w2 = sz2.Width
                logger.Info("w2:" + w2.ToString + "RTB width:" + RichTextBox2.Size.Width.ToString)
                'decrease the font until width of the text is less than width of the Richtextbox
                Do Until w2 < RichTextBox2.Size.Width - 40
                    'logger.Info("in backspace:" + RichTextBox1.Size.Width.ToString + "," + RichTextBox1.Size.Height.ToString)
                    logger.Info("RTB selection size:" + RichTextBox2.SelectionFont.Size.ToString)
                    RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size - 2, fstyle)
                    orgFont2 = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                    sz2 = g2.MeasureString(RichTextBox2.SelectedText, orgFont2)
                    h2 = sz2.Height
                    w2 = sz2.Width
                    logger.Info("w2:" + w2.ToString + "RTB width:" + RichTextBox2.Size.Width.ToString)
                Loop
            End If
        Catch ex As Exception
            logger.Error("exception in AdjustWidth():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button Click Event to change the font if in Text Mode
    'to change the graphic if in Grpahic Mode
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        CalculateDim()
        Try
            logger.Info("cursorPos:" + cursorPos2.ToString)
            Dim labeltext As String = ""
            'if application is in text mode, change the font type else change the graphic in display
            If Not Button1.Enabled And RichTextBox2.Text.Length > 0 Then
                'iterating through the fonts list
                If fontNo = fontList.Count - 1 Then
                    fontNo = 0
                Else
                    fontNo += 1
                End If
                Dim text As String = ""
                Dim bflag As Boolean = False
                'Font style is regular by default
                Dim fstyle As System.Drawing.FontStyle = FontStyle.Regular
                Dim fontname As String = fontList(fontNo).ToString
                logger.Info("fontname:" + fontname)
                If fontname.Contains("Narrow") And fontname.Contains("Bold") Then
                    fontname = "Arial Narrow"
                    labeltext = "Compressed" + Environment.NewLine + "Double line"
                    bflag = True
                ElseIf fontname.Contains("Bold") Then
                    fontname = "Arial"
                    labeltext = "Spread" + Environment.NewLine + "Double line"
                    bflag = True
                End If
                If t2Focus Then
                    'if font is Monotype, changing the style to Italic and changing the label text
                    If fontname.Contains("Monotype") Then
                        fstyle = FontStyle.Italic
                        labeltext = "Cursive"
                    Else
                        fstyle = RichTextBox2.Font.Style
                    End If
                    logger.Info("fstyle:" + fstyle.ToString)
                    logger.Info("fontname:" + fontname)
                    'changing the font type label according to the font selected
                    If fontname.Contains("Narrow") And Not bflag Then
                        labeltext = "Compressed"
                    ElseIf fontname = "Arial" And Not bflag Then
                        labeltext = "Spread"
                    End If
                    Label21.Text = labeltext
                    Dim start, fin As Integer
                    start = 0
                    fin = 0
                    text = ""
                    'if cursor is on first line
                    If cursorPos2 = 0 Then
                        text = RichTextBox2.Lines(0)
                        fin = text.Length
                        'selecting the text on first line
                        RichTextBox2.Select(0, fin)
                        'if font style is bold
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width Is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf1 = fontList(fontNo).ToString
                        logger.Info("rtf1:" + rtf1)
                        logger.Info("in font2:" + RichTextBox1.SelectionFont.Size.ToString)
                        'RichTextBox2.Select(0, fin)
                    ElseIf cursorPos2 = 1 Then
                        'if cursor is on second line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(1)
                        start = RichTextBox2.Lines(0).Length + 1
                        fin = text.Length
                        'selecting the text on second line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf2 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 2 Then
                        'if cursor is on third line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(2)
                        logger.Info("text length:" + text.Length.ToString)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2
                        logger.Info("start length:" + start.ToString)
                        fin = text.Length
                        'selecting the text on third line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf3 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 3 Then
                        'if cursor is on fourth line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(3)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3
                        fin = text.Length
                        'selecting the text on fourth line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf4 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 4 Then
                        'if cursor is on fifth line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(4)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4
                        fin = text.Length
                        'selecting the text on fifth line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf5 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 5 Then
                        'if cursor is on sixth line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(5)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5
                        fin = text.Length
                        'selecting the text on sixth line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf6 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 6 Then
                        'if cursor is on seventh line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(6)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6
                        fin = text.Length
                        'selecting the text on seventh line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf7 = fontList(fontNo).ToString
                    End If
                    'Placing the cursor at the end of the line selected, after the change of the font
                    RichTextBox2.Select()
                    RichTextBox2.SelectionStart = start
                    logger.Info("selction start:" + RichTextBox2.SelectionStart.ToString)
                End If
            ElseIf Not Button2.Enabled Then
                'iterate through the graphics list
                If graphno = graphfiles.Length - 1 Then
                    graphno = 0
                Else
                    graphno += 1
                End If
                'display the selected graphic in the picturebox
                PictureBox44.Load(graphfiles(graphno))
                PictureBox44.SizeMode = PictureBoxSizeMode.Zoom
                'display the number of the graphic selected
                Label11.Text = Convert.ToString(graphno + 1) + "/" + Convert.ToString(graphfiles.Length)
                graphic = graphfiles(graphno)
                'graphic = graphic.Substring(graphic.LastIndexOf("."))
            End If
        Catch ex As Exception
            logger.Error("exception in Button15_Click():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button Click Event to change the font if in Text Mode
    'to change the graphic if in Grpahic Mode
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        CalculateDim()
        Try
            'if application is in text mode, change the font type else change the graphic in display
            If Not Button1.Enabled And RichTextBox2.Text.Length > 0 Then
                'iterating through the fonts list
                If fontNo = 0 Then
                    fontNo = fontList.Count - 1
                Else
                    fontNo -= 1
                End If
                Dim text As String = ""
                Dim bflag As Boolean = False
                Dim labeltext As String = ""
                Dim fontname As String = fontList(fontNo).ToString
                'Font style is regular by default
                Dim fstyle As System.Drawing.FontStyle = FontStyle.Regular
                If fontname.Contains("Narrow") And fontname.Contains("Bold") Then
                    fontname = "Arial Narrow"
                    labeltext = "Compressed" + Environment.NewLine + "Double line"
                    bflag = True
                ElseIf fontname.Contains("Bold") Then
                    fontname = "Arial"
                    labeltext = "Spread" + Environment.NewLine + "Double line"
                    bflag = True
                End If
                If t2Focus Then
                    'if font is Monotype, changing the style to Italic and changing the label text
                    If fontname.Contains("Monotype") Then
                        fstyle = FontStyle.Italic
                        labeltext = "Cursive"
                    Else
                        fstyle = RichTextBox2.Font.Style
                    End If
                    logger.Info("fstyle:" + fstyle.ToString)
                    logger.Info("fontname:" + fontname)
                    'changing the font type label according to the font selected
                    If fontname.Contains("Narrow") And Not bflag Then
                        labeltext = "Compressed"
                    ElseIf fontname = "Arial" And Not bflag Then
                        labeltext = "Spread"
                    End If
                    Label21.Text = labeltext
                    Dim start, fin As Integer
                    start = 0
                    fin = 0
                    text = ""
                    'if cursor is on first line
                    If cursorPos2 = 0 Then
                        text = RichTextBox2.Lines(0)
                        fin = text.Length
                        'selecting the text on first line
                        RichTextBox2.Select(0, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf1 = fontList(fontNo).ToString
                        logger.Info("rtf1:" + rtf1)
                        logger.Info("in font2:" + RichTextBox1.SelectionFont.Size.ToString)
                    ElseIf cursorPos2 = 1 Then
                        'if cursor is on second line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(1)
                        start = RichTextBox2.Lines(0).Length + 1
                        fin = text.Length
                        'selecting the text on second line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf2 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 2 Then
                        'if cursor is on third line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(2)
                        logger.Info("text length:" + text.Length.ToString)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2
                        logger.Info("start length:" + start.ToString)
                        fin = text.Length
                        'selecting the text on third line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf3 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 3 Then
                        'if cursor is on fourth line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(3)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3
                        fin = text.Length
                        'selecting the text on fourth line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            'changing the font of the text to the selected font
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf4 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 4 Then
                        'if cursor is on fifth line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(4)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4
                        fin = text.Length
                        'selecting the text on fifth line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf5 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 5 Then
                        'if cursor is on sixth line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(5)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5
                        fin = text.Length
                        'selecting the text on sixth line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf6 = fontList(fontNo).ToString
                    ElseIf cursorPos2 = 6 Then
                        'if cursor is on seventh line, the font on that text is changed to selected font
                        text = RichTextBox2.Lines(6)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6
                        fin = text.Length
                        'selecting the text on seventh line
                        RichTextBox2.Select(start, fin)
                        If bflag Then
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, FontStyle.Bold)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, FontStyle.Bold)
                        Else
                            'adjust the width of the text if width is greater than textbox width
                            Adjustwidth(2, fontname, fstyle)
                            RichTextBox2.SelectionFont = New Font(fontname, RichTextBox2.SelectionFont.Size, fstyle)
                        End If
                        rtf7 = fontList(fontNo).ToString
                    End If
                    'Placing the cursor at the end of the line selected, after the change of the font
                    RichTextBox2.Select()
                    RichTextBox2.SelectionStart = start
                    logger.Info("selction start:" + RichTextBox2.SelectionStart.ToString)
                End If
            ElseIf Not Button2.Enabled Then
                'iterate through the graphics list
                If graphno = 0 Then
                    graphno = graphfiles.Length - 1
                Else
                    graphno -= 1
                End If
                'display the selected graphic in the picturebox
                PictureBox44.Load(graphfiles(graphno))
                PictureBox44.SizeMode = PictureBoxSizeMode.Zoom
                'display the number of the graphic selected
                Label11.Text = Convert.ToString(graphno + 1) + "/" + Convert.ToString(graphfiles.Length)
                graphic = graphfiles(graphno)
                'graphic = graphic.Substring(graphic.LastIndexOf("."))
                logger.Info("graphic:" + graphic)
            End If
        Catch ex As Exception
            logger.Error("exception in Button14_Click():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button Click event for Cancel Button. Saves the text and the font and redirects to the Home page.
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        'Clear the TextBox if current page is Rabies or Info Tags, else save the text and its font to a file
        If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
            RichTextBox1.Text = ""
            RichTextBox2.Text = ""
            RichTextBox3.Text = ""
            RichTextBox1.SelectionAlignment = HorizontalAlignment.Center
            RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size + 70, RichTextBox2.Font.Style)
            RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
            RichTextBox3.SelectionAlignment = HorizontalAlignment.Center
        Else
            WriteText()
            saveFont()
        End If
        'Reset the size of the Picturebox for graphic display
        PictureBox44.Width = 277
        PictureBox44.Height = 173
        PictureBox44.Location = New Point(PictureBox44.Location.X, 194)
        'Go to Home page
        TabControl1.SelectTab(0)
        'Reset all the Labels and flags to the initial stage
        Label1.Text = "Select to engrave a tag:"
        Panel3.Visible = False
        Button12.Text = "Engrave"
        multiflag = False
        textFlag = False
        engraveFlag = False
        Label16.Visible = False
        Label17.Visible = False
        Label18.Visible = False
        Label19.Visible = False
        Label20.Visible = False
        Label22.Visible = False
        enlargeFlag = False
        Button23.Visible = True
        Button24.Visible = True
        Button25.Visible = True
        fontNo = 0
        setupFlag = False
    End Sub

    'Method to save the font size, type and style of each line in the RichTextBox
    Public Sub saveFont()
        'saving the fonts for all the lines in the textbox by selecting each line and saving to variables
        If RichTextBox2.Lines.Count >= 1 Then
            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            fs1 = RichTextBox2.SelectionFont.Size
            ft1 = RichTextBox2.SelectionFont.Name
            fst1 = RichTextBox2.SelectionFont.Style
            logger.Info("ft1:" + ft1)
        End If
        If RichTextBox2.Lines.Count >= 2 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            fs2 = RichTextBox2.SelectionFont.Size
            ft2 = RichTextBox2.SelectionFont.Name
            fst2 = RichTextBox2.SelectionFont.Style
            logger.Info("ft2:" + ft2)
        End If
        If RichTextBox2.Lines.Count >= 3 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            fs3 = RichTextBox2.SelectionFont.Size
            ft3 = RichTextBox2.SelectionFont.Name
            fst3 = RichTextBox2.SelectionFont.Style
        End If
        If RichTextBox2.Lines.Count >= 4 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            fs4 = RichTextBox2.SelectionFont.Size
            ft4 = RichTextBox2.SelectionFont.Name
            fst4 = RichTextBox2.SelectionFont.Style
        End If
        If RichTextBox2.Lines.Count >= 5 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            fs5 = RichTextBox2.SelectionFont.Size
            ft5 = RichTextBox2.SelectionFont.Name
            fst5 = RichTextBox2.SelectionFont.Style
        End If
        If RichTextBox2.Lines.Count >= 6 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            fs6 = RichTextBox2.SelectionFont.Size
            ft6 = RichTextBox2.SelectionFont.Name
            fst6 = RichTextBox2.SelectionFont.Style
        End If
        If RichTextBox2.Lines.Count >= 7 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            fs7 = RichTextBox2.SelectionFont.Size
            ft7 = RichTextBox2.SelectionFont.Name
            fst7 = RichTextBox2.SelectionFont.Style
        End If
    End Sub

    Public Sub restoreFont()
        'restoring the fonts for all the lines in the textbox by selecting each line
        If RichTextBox2.Lines.Count >= 1 Then
            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            RichTextBox2.SelectionFont = New Font(ft1, fs1, fst1)
        End If
        If RichTextBox2.Lines.Count >= 2 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            RichTextBox2.SelectionFont = New Font(ft2, fs2, fst2)
        End If
        If RichTextBox2.Lines.Count >= 3 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            RichTextBox2.SelectionFont = New Font(ft3, fs3, fst3)
        End If
        If RichTextBox2.Lines.Count >= 4 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            RichTextBox2.SelectionFont = New Font(ft4, fs4, fst4)
        End If
        If RichTextBox2.Lines.Count >= 5 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            RichTextBox2.SelectionFont = New Font(ft5, fs5, fst5)
        End If
        If RichTextBox2.Lines.Count >= 6 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            RichTextBox2.SelectionFont = New Font(ft6, fs6, fst6)
        End If
        If RichTextBox2.Lines.Count >= 7 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            RichTextBox2.SelectionFont = New Font(ft7, fs7, fst7)
        End If
    End Sub

    'Method to open Command Console in Hidden mode and run commands provided
    Public Sub RunCommandCom(command As String, arguments As String, permanent As Boolean)
        Try
            Dim p As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo()
            pi.Arguments = " " + If(permanent = True, "/k", "/C") + " " + command + " " + arguments
            pi.FileName = "cmd.exe"
            pi.WindowStyle = ProcessWindowStyle.Hidden
            pi.CreateNoWindow = True
            p.StartInfo = pi
            p.Start()
        Catch ex As Exception
            logger.Error("exception in runcommand():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button Click Event for Engrave/Save Button
    'if Engrave button, engraves the tag by generating a plt file
    'if Save Button, saves the text for Rabies or Info ID
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            saveFont()
            'Removing empty lines from the text
            RichTextBox2.Lines = RichTextBox2.Text.Split(New Char() {ControlChars.Lf}, StringSplitOptions.RemoveEmptyEntries)
            'restoring the fonts saved previously because removing empty lines causes to change the font
            restoreFont()
            lnum = RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length
            logger.Info("Panel1:" + Panel1.Visible.ToString)
            If RichTextBox2.Text.Length = 0 And Not Button1.Enabled Then
                Exit Sub
            End If
            'if the screen is setup page, save the text to its respective file
            If Label1.Text.Contains("set up") Then
                Dim objWriter As New System.IO.StreamWriter(dir + "\tags\meta\rabid_" + tagNo.ToString + ".txt")
                If Label1.Text.Contains("Info/ID") Then
                    objWriter = New System.IO.StreamWriter(dir + "\tags\meta\infoid_" + tagNo.ToString + ".txt")
                    Dim texttosave As String() = GetText()
                    For j = 0 To texttosave.Length - 1
                        objWriter.WriteLine(texttosave(j))
                    Next
                Else
                    'if rabies setup page, save all the lines except the last(id) one
                    Dim texttosave As String() = GetText()
                    For j = 0 To texttosave.Length - 2
                        objWriter.WriteLine(texttosave(j))
                    Next
                End If
                'objWriter.WriteLine(TextBox5.Text)
                objWriter.Close()
                saveFont()
                Dim filePath As String = ""
                'saving the font type, style and size to the respective tag file
                If Label1.Text.Contains("Info/ID") Then
                    filePath = dir + "\tags\meta\infoid_" + tagNo.ToString + "_meta.txt"
                ElseIf Label1.Text.Contains("Rabies/ID") Then
                    filePath = dir + "\tags\meta\rabid_" + tagNo.ToString + "_meta.txt"
                End If
                objWriter = New System.IO.StreamWriter(filePath)
                Dim text As String = ""
                text = fs1.ToString + "," + fs2.ToString + "," + fs3.ToString + "," + fs4.ToString + "," + fs5.ToString + "," + fs6.ToString + "," + fs7.ToString + Environment.NewLine
                text += ft1 + "," + ft2 + "," + ft3 + "," + ft4 + "," + ft5 + "," + ft6 + "," + ft7 + Environment.NewLine
                text += fst1.ToString + "," + fst2.ToString + "," + fst3.ToString + "," + fst4.ToString + "," + fst5.ToString + "," + fst6.ToString + "," + fst7.ToString
                objWriter.WriteLine(text)
                objWriter.Close()
                mbFlag = False
                'displaying "saved successfully" message
                Dim f2 As New MbForm
                f2.ShowDialog()
                exitScreen()
            ElseIf (Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID")) And Button2.Enabled Then
                'Engrave the text if Rabies or Info Id tags
                logger.Info("in multi tag engraving")
                'get the dimensions and parmaters of the tag
                Dim tagParams As String() = ReadDimensions("multi").Split(",")
                Dim myValue As Integer = 0
                Dim ibflag As Boolean = True
                Dim v As String
                'determining if it's a 4 or 3 multi tag holder
                If tagParams.Length = 10 Then
                    myValue = 4
                Else
                    myValue = 3
                End If
                mbFlag = True
                mbValue = myValue
                'displaying a popup to enter the number of tags to be engraved
                Dim f2 As New MbForm
                f2.ShowDialog()
                v = f2.value
                logger.Info("V:" + v)
                logger.Info("flag:" + f2.flag.ToString)
                'Exit on Cancel
                If Not f2.flag Then
                    Exit Sub
                End If
                plates = v
                Dim plttext As String = ""
                Dim counter As Integer = 0
                Dim hyphflag As Boolean = False
                Dim temp As String = ""
                If Label1.Text.Contains("Rabies/ID") Then
                    Dim rabtext As String() = GetText()
                    'get the id for rabies and increment it by the number of multi tags to be engraved
                    If rabtext(rabtext.Length - 1).Contains("-") Then
                        hyphflag = True
                        Dim num As String() = rabtext(rabtext.Length - 1).Split("-")
                        counter = Convert.ToInt16(num(num.Length - 1))
                        For i = 0 To num.Length - 2
                            temp += num(i) + "-"
                        Next
                    Else
                        counter = rabtext(rabtext.Length - 1)
                    End If
                    For i = 0 To v - 1
                        Dim res As String = (counter + i).ToString
                        Dim zeros As String = ""
                        'add zeros to the begining of the number if length is less than 4
                        If res.Length < 4 Then
                            For l = 1 To 4 - res.Length
                                zeros += "0"
                            Next
                            res = zeros + res
                        End If
                        If hyphflag Then
                            rabtext(rabtext.Length - 1) = temp + res
                        Else
                            rabtext(rabtext.Length - 1) = res
                        End If
                        'generate a plt file for each tag to be engraved
                        GeneratePlt(rabtext, i + 1)
                    Next
                    counter = counter + v
                ElseIf Label1.Text.Contains("Info/ID") Then
                    For i = 0 To v - 1
                        'generate a plt file for each tag to be engraved
                        GeneratePlt(GetText(), i + 1)
                    Next
                Else
                    'generate a plt file with the text in case of individual engraving
                    plttext = GeneratePlt(GetText(), 0)
                End If
                logger.Info("get text:" + GetText(0))
                'set the position of the text in plt file as per the tag on the holder
                MultiPlt(v, "multi")
                'set the parameters required to send the plt file to the engraving machine
                Hpgltobin()
                'call the HpglToBin software to send the file to the engraving machine
                RunCommandCom("HpglToBin.exe", destPath, True)
                logger.Info("in panel4")
                Label16.Visible = False
                Label17.Visible = False
                Label18.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label22.Visible = False
                Panel4.Visible = True
                'updating the value of total tags engraved
                totalTags = totalTags + v
                'saving it to the file
                setTotalTags(totalTags)
                getTotalTags()
                'updating the rabies id and saving it to rabidcoun.txt file
                If Label1.Text.Contains("Rabies/ID") Then
                    Dim sw = New StreamWriter(dir + "\tags\meta\rabidcount.txt")
                    Dim count As String = (counter).ToString
                    Dim zeros As String = ""
                    If count.Length < 4 Then
                        For l = 1 To 4 - count.Length
                            zeros += "0"
                        Next
                        count = zeros + count
                    End If
                    If hyphflag Then
                        logger.Info("temp:" + temp)
                        logger.Info("count:" + count)
                        count = temp + count
                    End If
                    sw.WriteLine(count)
                    sw.Close()
                    Label5.Text = count.ToString
                End If
            Else
                logger.Info("not in multi tag engraving:" + TabControl1.SelectedIndex.ToString)
                'if in text mode, engrave the text else engrave the graphic
                If Not Button1.Enabled Then
                    'generate a plt file for individual engraving
                    GeneratePlt(GetText(), 0)
                    MultiPlt(1, "single")
                Else
                    'generating a plt file for graphic
                    Dim pos As Integer = graphic.LastIndexOf("\") + 1
                    logger.Info(graphic)
                    Dim name As String = graphic.Substring(pos)
                    name = name.Substring(0, name.Length - 4)
                    'retrieving the path of plt file for selected graphic
                    Dim path As String = graphic.Replace(graphic.Substring(pos), "plt\" + name + ".plt")
                    logger.Info("path:" + path)
                    Dim xparams As Integer() = {0, 0, 0, 0, 0}
                    Dim yparams As Integer() = {0, 0, 0, 0, 0}
                    Dim xcount, ycount As Integer
                    xcount = 0
                    ycount = 0
                    Dim tagparams As String()
                    Dim ibflag As Boolean = True
                    Dim v As String = 1
                    Dim myValue As Integer
                    If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
                        'for multi engraving of the graphic
                        tagparams = ReadDimensions("multi").Split(",")
                        If tagparams.Length = 10 Then
                            myValue = 4
                        Else
                            myValue = 3
                        End If
                        mbFlag = True
                        mbValue = myValue
                        'popup to enter the number of tags to be engraved
                        Dim f2 As New MbForm
                        f2.ShowDialog()
                        v = f2.value
                        logger.Info("V:" + v)
                        logger.Info("flag:" + f2.flag.ToString)
                        plates = v
                        If Not f2.flag Then
                            Exit Sub
                        End If
                        For i = 0 To tagparams.Length - 1
                            If (i Mod 2) = 0 Then
                                xparams(xcount) = Convert.ToInt16(tagparams(i))
                                xcount += 1
                            Else
                                yparams(ycount) = Convert.ToInt16(tagparams(i))
                                ycount += 1
                            End If
                        Next
                    Else
                        'retrieving the dimesnions and parmaters required for engraving on the tag
                        tagparams = ReadDimensions("").Split(",")
                        xparams(0) = tagparams(0)
                        yparams(0) = tagparams(1)
                        xparams(1) = tagparams(2)
                        yparams(1) = tagparams(3)
                    End If
                    logger.Info("params length:" + tagparams.Length.ToString)
                    Dim mflag As Boolean = True
                    Dim xlist, ylist As New ArrayList
                    'normalizing the plt file containing the graphic
                    Normalize(path, 2)
                    path = dir + "\char.plt"
                    Dim sr = New StreamReader(path)
                    'retrieving the x and y co-ordinates in the plt file of the graphic
                    Do While sr.Peek() >= 0
                        Dim line As String = sr.ReadLine
                        If (line.StartsWith("PU") Or line.StartsWith("PD")) And line.Length > 4 Then
                            Dim coord As String() = line.Substring(2, line.Length - 2).Split(" ")
                            Console.WriteLine(coord(0).ToString)
                            Dim x As Integer = Convert.ToInt16(coord(0))
                            xlist.Add(x)
                            Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                            ylist.Add(y)
                        End If
                    Loop
                    sr.Close()
                    Dim xmin = MinCheck(xlist)
                    Dim ymin = MinCheck(ylist)
                    Dim xmax = MaxCheck(xlist)
                    Dim ymax = MaxCheck(ylist)
                    Dim peri As Double = 0
                    Dim width As Double = 0
                    Dim height As Double = 0
                    logger.Info("tagparams length:" + tagparams.Length.ToString)
                    'saving the height and widht of the tag to be engraved on
                    If tagparams.Length = 4 Then
                        width = (xparams(1) / 1000)
                        height = (yparams(1) / 1000)
                    ElseIf tagparams.Length = 10 Then
                        width = (xparams(4) / 1000)
                        height = (yparams(4) / 1000)
                    Else
                        width = (xparams(3) / 1000)
                        height = (yparams(3) / 1000)
                    End If
                    Dim xratio As Double = 1
                    Dim yratio As Double = 1
                    'calculating the width of the graphic in the original plt file
                    Dim xperi As Double = (xmax - xmin) * 0.0025
                    'calculating the height of the graphic in the original plt file
                    Dim yperi As Double = (ymax - ymin) * 0.0025
                    Dim temp1 As Double = xperi
                    Dim temp2 As Double = yperi
                    'calculating the aspect ratio of the graphic in the original plt file
                    peri = Math.Round(xperi / yperi, 2)
                    logger.Info("peri:" + peri.ToString)
                    logger.Info("xperi:" + xperi.ToString + "yperi:" + yperi.ToString)
                    logger.Info("width:" + width.ToString + "height:" + height.ToString)
                    'reduce graphic to size of the tag
                    If xperi > width Then
                        xratio = xperi / width
                    End If
                    If yperi > height Then
                        yratio = yperi / height
                    End If
                    Dim ratio As Double = xperi / yperi
                    sr = New StreamReader(path)
                    xlist.Clear()
                    ylist.Clear()
                    'sizing the graphic according to the size of the tag
                    Do While sr.Peek() >= 0
                        Dim line As String = sr.ReadLine
                        If (line.StartsWith("PU") Or line.StartsWith("PD")) And line.Length > 4 Then
                            Dim coord As String() = line.Substring(2, line.Length - 2).Split(" ")
                            Console.WriteLine(coord(0).ToString)
                            Dim x As Integer = Convert.ToInt16(coord(0))
                            x = x / xratio
                            xlist.Add(x)
                            Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                            y = y / yratio
                            ylist.Add(y)
                        End If
                    Loop
                    sr.Close()
                    xmin = MinCheck(xlist)
                    ymin = MinCheck(ylist)
                    xmax = MaxCheck(xlist)
                    ymax = MaxCheck(ylist)
                    logger.Info("xmax:" + xmax.ToString + "xmin:" + xmin.ToString)
                    'calculating the width of the resulting adjusted graphic
                    xperi = (xmax - xmin) * 0.0025
                    'calculating the height of the resulting adjusted graphic
                    yperi = (ymax - ymin) * 0.0025
                    logger.Info("xperi:" + xperi.ToString + "yperi:" + yperi.ToString + "ratio:" + ratio.ToString)
                    'calculating the resulting aspect ratio
                    ratio = Math.Round(xperi / yperi, 2)
                    Dim con As Int16 = 0
                    logger.Info("peri:" + peri.ToString + "ratio:" + ratio.ToString)
                    logger.Info("diff:" + Math.Round(Math.Abs(ratio - peri), 1).ToString)
                    'maintaining the aspect ratio of the graphic with the original and adjusted to the size of the tag
                    'if difference between original and adjusted aspect ratio is greater than 0.2, reduce it to less than 0.2
                    If peri > 1 And Math.Round(Math.Abs(ratio - peri), 1) >= 0.2 Then
                        If ratio <= 1 Then
                            con = 1
                            yratio = yratio * (xperi * peri) / 2
                        ElseIf ratio > 1 Then
                            If ratio > peri Then
                                xratio = xratio * (peri * yperi) / 2
                                con = 2
                            Else
                                yratio = yratio * xperi / peri
                                con = 1
                            End If
                        End If
                    ElseIf peri < 1 And Math.Round(Math.Abs(ratio - peri), 1) >= 0.2 Then
                        If ratio > 1 Then
                            con = 2
                            xratio = xratio * xperi / peri
                        ElseIf ratio < 1 Then
                            If ratio > peri Then
                                xratio = xratio * (peri * yperi) / 2
                                con = 2
                            Else
                                yratio = yratio * xperi / peri
                                con = 1
                            End If
                        End If
                    End If
                    logger.Info("con:" + con.ToString)
                    Dim temp As Double = xratio / yratio
                    logger.Info("peri:" + peri.ToString + "ratio:" + temp.ToString)
                    logger.Info("xperi:" + temp1.ToString + "yperi:" + temp2.ToString + "xratio:" + xratio.ToString + "yratio:" + yratio.ToString)
                    If con = 1 Then
                        'adjust the yratio(height) until difference between ratios is less than 0.2
                        logger.Info("in con1")
                        While ratio <= peri - 0.2
                            sr = New StreamReader(path)
                            xlist.Clear()
                            ylist.Clear()
                            Do While sr.Peek() >= 0
                                Dim line As String = sr.ReadLine
                                If (line.StartsWith("PU") Or line.StartsWith("PD")) And line.Length > 4 Then
                                    Dim coord As String() = line.Substring(2, line.Length - 2).Split(" ")
                                    Dim x As Integer = Convert.ToInt16(coord(0))
                                    x = x / xratio
                                    xlist.Add(x)
                                    Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                                    y = y / yratio
                                    ylist.Add(y)
                                End If
                            Loop
                            sr.Close()
                            xmin = MinCheck(xlist)
                            ymin = MinCheck(ylist)
                            xmax = MaxCheck(xlist)
                            ymax = MaxCheck(ylist)
                            logger.Info("xmax:" + xmax.ToString + "xmin:" + xmin.ToString)
                            xperi = (xmax - xmin) * 0.0025
                            yperi = (ymax - ymin) * 0.0025
                            ratio = xperi / yperi
                            logger.Info("xperi:" + xperi.ToString + "yperi:" + yperi.ToString + "ratio:" + ratio.ToString)
                            yratio = yratio + 0.2
                        End While
                    ElseIf con = 2 Then
                        'adjust the xratio(width) until difference between ratios is less than 0.2
                        logger.Info("in con2")
                        While ratio >= peri + 0.2
                            sr = New StreamReader(path)
                            xlist.Clear()
                            ylist.Clear()
                            Do While sr.Peek() >= 0
                                Dim line As String = sr.ReadLine
                                If (line.StartsWith("PU") Or line.StartsWith("PD")) And line.Length > 4 Then
                                    Dim coord As String() = line.Substring(2, line.Length - 2).Split(" ")
                                    Console.WriteLine(coord(0).ToString)
                                    Dim x As Integer = Convert.ToInt16(coord(0))
                                    x = x / xratio
                                    xlist.Add(x)
                                    Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                                    y = y / yratio
                                    ylist.Add(y)
                                End If
                            Loop
                            sr.Close()
                            xmin = MinCheck(xlist)
                            ymin = MinCheck(ylist)
                            xmax = MaxCheck(xlist)
                            ymax = MaxCheck(ylist)
                            logger.Info("xmax:" + xmax.ToString + "xmin:" + xmin.ToString)
                            xperi = (xmax - xmin) * 0.0025
                            yperi = (ymax - ymin) * 0.0025
                            ratio = xperi / yperi
                            logger.Info("xperi:" + xperi.ToString + "yperi:" + yperi.ToString + "ratio:" + ratio.ToString)
                            xratio = xratio + 0.2
                        End While
                    End If
                    logger.Info("xratio:" + xratio.ToString + ",yratio:" + yratio.ToString)
                    'xratio = xratio + 0.5
                    sr = New StreamReader(path)
                    xlist.Clear()
                    ylist.Clear()
                    'Use the finally obtained x and y ratios on the original file to get the final adjusted graphic for the required tag
                    Do While sr.Peek() >= 0
                        Dim line As String = sr.ReadLine
                        If (line.StartsWith("PU") Or line.StartsWith("PD")) And line.Length > 4 Then
                            Dim coord As String() = line.Substring(2, line.Length - 2).Split(" ")
                            Console.WriteLine(coord(0).ToString)
                            Dim x As Integer = Convert.ToInt16(coord(0))
                            x = x / xratio
                            xlist.Add(x)
                            Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                            y = y / yratio
                            ylist.Add(y)
                        End If
                    Loop
                    sr.Close()
                    xmin = MinCheck(xlist)
                    ymin = MinCheck(ylist)
                    xmax = MaxCheck(xlist)
                    ymax = MaxCheck(ylist)
                    Dim text As String = ""
                    'adjusting the resulting plt file to position of the tag on the holder
                    For i As Integer = 0 To (v - 1)
                        'x co-ordinate of the center of the tag on the holder
                        Dim xpoint As Integer = xparams(i) - ((xmax - xmin) / 2)
                        'y co-ordinate of the center of the tag on the holder
                        Dim ypoint As Integer = yparams(i) - ((ymax - ymin) / 2)
                        logger.Info("xpoint:" + xpoint.ToString)
                        sr = New StreamReader(path)
                        Do While sr.Peek() >= 0
                            Dim line As String = sr.ReadLine
                            If (line.StartsWith("PU") Or line.StartsWith("PD")) And line.Length > 4 Then
                                Dim coord As String() = line.Substring(2, line.Length - 2).Split(" ")
                                Console.WriteLine("x:" + coord(0))
                                Dim x As Integer = Convert.ToInt16(coord(0))
                                Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                                'Console.WriteLine(x.ToString)
                                x = (x / xratio) + xpoint
                                y = (y / yratio) + ypoint
                                text += line.Substring(0, 2) + x.ToString + " " + y.ToString + ";" + Environment.NewLine
                            ElseIf line = "SP1;" Or mflag Then
                                text += line + Environment.NewLine
                            End If
                        Loop
                        text += "SP1;" + Environment.NewLine
                        mflag = False
                    Next
                    sr.Close()
                    text += "SP0;"
                    'write the resulting plt file
                    Dim sw = New StreamWriter(dir + "\new2.plt")
                    sw.WriteLine(text)
                    sw.Close()
                    'f3.Close()
                End If
                Label16.Visible = False
                Label17.Visible = False
                Label18.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label22.Visible = False
                'set the parameters required for the machine to process the plt file 
                Hpgltobin()
                'send te file to the engraving machine
                RunCommandCom("HpglToBin.exe", destPath, True)
                logger.Info("in panel4")
                Panel4.Visible = True
                WriteText()
                'update the value of the toal tags
                totalTags = totalTags + 1
                'save the value of the total tags
                setTotalTags(totalTags)
                getTotalTags()
                saveFont()
            End If
            ldiffList.Clear()
        Catch ex As Exception
            logger.Error("exception in Button12_Click():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button Click Event for "Open Door" Button
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        RunCommandCom("SendToUsb.exe", "Open.bin", True)
    End Sub

    'Button Click Event for "Close Door" Button
    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        RunCommandCom("SendToUsb.exe", "Close.bin", True)
    End Sub

    'Button Click Event for "Open Door" Button
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        RunCommandCom("SendToUsb.exe", "Open.bin", True)
    End Sub

    'Button Click Event for "Close Door" Button
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        RunCommandCom("SendToUsb.exe", "Close.bin", True)
    End Sub

    'Button Click Event for "Stop Engraving" Button
    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        RunCommandCom("SendToUsb.exe", "Pause.bin", True)
        'Button20.Enabled = False
        'Button21.Enabled = True
    End Sub

    'Button Click Event for "Start Engraving" Button
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        RunCommandCom("SendToUsb.exe", "Start.bin", True)
        'Button21.Enabled = False
        'Button20.Enabled = True
    End Sub

    'Method to reset fonts and labels after clicking Exit Button
    Public Sub exitScreen()
        If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
            RichTextBox1.Text = ""
            RichTextBox2.Text = ""
            RichTextBox3.Text = ""
            RichTextBox1.SelectionAlignment = HorizontalAlignment.Center
            RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size + 70, RichTextBox2.Font.Style)
            RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
            RichTextBox3.SelectionAlignment = HorizontalAlignment.Center
        Else
            WriteText()
        End If
        Button23.Visible = True
        Button24.Visible = True
        Button25.Visible = True
        rtf1 = "Arial Narrow"
        rtf2 = "Arial Narrow"
        rtf3 = "Arial Narrow"
        rtf4 = "Arial Narrow"
        rtf5 = "Arial Narrow"
        rtf6 = "Arial Narrow"
        rtf7 = "Arial Narrow"
        TabControl1.SelectTab(0)
        Label1.Text = "Select to engrave a tag:"
        Panel3.Visible = False
        Button12.Text = "Engrave"
        Button21.Enabled = True
        Button20.Enabled = True
        Panel4.Visible = False
        multiflag = False
        Label16.Visible = False
        Label17.Visible = False
        Label18.Visible = False
        Label19.Visible = False
        Label20.Visible = False
        Label22.Visible = False
        enlargeFlag = False
        setupFlag = False
        fontNo = 0
    End Sub

    'Button Click Event for "Exit" Button, redirects to the Home page
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        PictureBox44.Width = 277
        PictureBox44.Height = 173
        PictureBox44.Location = New Point(PictureBox44.Location.X, 194)
        textFlag = False
        exitScreen()
    End Sub

    'Method to position the normalized text from the plt file according to the position on the holder
    'and generate multiple plt files in case of multi-engraving
    Public Sub MultiPlt(ByVal v As String, ByVal type As String)
        Try
            Dim mflag As Boolean = True
            Dim xlist, ylist As New ArrayList
            Dim xparams As Integer() = {0, 0, 0, 0}
            Dim yparams As Integer() = {0, 0, 0, 0}
            Dim xcount, ycount, rabidcount, f As Integer
            Dim filename As String = ""
            Dim lines As Integer = RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length
            xcount = 0
            ycount = 0
            rabidcount = 1
            f = 0
            Dim tagparams As String()
            'retrieving the dimensions and paramters of the tag
            If type = "multi" Then
                tagparams = ReadDimensions("multi").Split(",")
            Else
                tagparams = ReadDimensions("").Split(",")
            End If
            logger.Info("params length:" + tagparams.Length.ToString)
            'storing the dimensions and center co-ordinates of the tag
            For i = 0 To tagparams.Length - 3
                If (i Mod 2) = 0 Then
                    xparams(xcount) = Convert.ToInt16(tagparams(i))
                    xcount += 1
                Else
                    yparams(ycount) = Convert.ToInt16(tagparams(i))
                    If lines > 5 Then
                        yparams(ycount) = yparams(ycount)
                    End If
                    ycount += 1
                End If
            Next
            Dim sr As StreamReader
            'if rabies or info tag engraving, create a new file for each tag to be engraved
            If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
                rabidcount = v
                v = 1
                filename = f
                sr = New StreamReader(dir + "\" + (f + 1).ToString + ".plt")
                logger.Info("file:" + dir + "\" + (f + 1).ToString + ".plt")
            Else
                sr = New StreamReader(dir + "\new.plt")
                logger.Info("file:" + dir + "\new.plt")
            End If
            Dim text As String = ""
            For j = 0 To rabidcount - 1
                If j > 0 Then
                    sr = New StreamReader(dir + "\" + (j + 1).ToString + ".plt")
                    logger.Info("file:" + dir + "\" + (j + 1).ToString + ".plt")
                End If
                'retrieving the x and y co-ordinates of plt file containing the text entered
                Do While sr.Peek() >= 0
                    Dim line As String = sr.ReadLine
                    If line.StartsWith("PU") Or line.StartsWith("PD") And line.Length > 4 Then
                        Dim coord As String() = line.Split(" ")
                        Dim x As Integer = Convert.ToInt16(coord(0).Substring(2, coord(0).Length - 2))
                        'Console.WriteLine(coord(1))
                        'x = x
                        xlist.Add(x)
                        Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                        'y = y
                        ylist.Add(y)
                    End If
                Loop
                sr.Close()
                'Console.WriteLine("xlist:" + xlist.Count.ToString + ",ylist:" + ylist.Count.ToString)
                Dim xmin = Math.Abs(MinCheck(xlist))
                Dim ymin = MinCheck(ylist)
                Dim xmax = Math.Abs(MaxCheck(xlist))
                Dim ymax = MaxCheck(ylist)
                logger.Info("xmin:" + xmin.ToString + ",xmax:" + xmax.ToString + ",ymin:" + ymin.ToString + ",ymax:" + ymax.ToString)
                'adjusting the resulting plt file to position of the tag on the holder
                For i As Integer = 0 To (v - 1)
                    logger.Info("xparams:" + xparams(i).ToString + "yparams:" + yparams(i).ToString)
                    'x co-ordinate of the center of the tag on the holder
                    Dim xpoint As Integer = xparams(i) - ((xmax - xmin) / 2)
                    'y co-ordinate of the center of the tag on the holder
                    Dim ypoint As Integer = yparams(i) + ((ymax - ymin) / 2)
                    'For Military tags, cursor starts at the left hand side
                    If tagNo = 22 Or tagNo = 20 Then
                        xpoint = xparams(i) - (xmin)
                    End If
                    If j > 0 Then
                        xpoint = xparams(j) - ((xmax - xmin) / 2)
                        ypoint = yparams(j) + ((ymax - ymin) / 2)
                    End If
                    If lines = 1 Then
                        'ypoint = yparams(i)
                    End If
                    logger.Info("xpoint:" + xpoint.ToString)
                    logger.Info("ypoint:" + ypoint.ToString)
                    If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
                        sr = New StreamReader(dir + "\" + (j + 1).ToString + ".plt")
                    Else
                        sr = New StreamReader(dir + "\new.plt")
                    End If
                    'generating the final plt file
                    Do While sr.Peek() >= 0
                        Dim line As String = sr.ReadLine
                        If line.StartsWith("PU") Or line.StartsWith("PD") And line.Length > 4 Then
                            Dim coord As String() = line.Split(" ")
                            Dim x As Integer = Convert.ToInt16(coord(0).Substring(2, coord(0).Length - 2))
                            Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                            'Console.WriteLine(x.ToString)
                            x = (x) + xpoint
                            y = (y) + ypoint - ymax
                            text += coord(0).Substring(0, 2) + x.ToString + " " + y.ToString + ";" + Environment.NewLine
                        ElseIf line = "SP1;" Or mflag Then
                            text += line + Environment.NewLine
                        End If
                    Loop
                    text += "SP1;" + Environment.NewLine
                    mflag = False
                Next
                sr.Close()
                text += "SP0;" + Environment.NewLine
            Next
            Dim sw As StreamWriter = New StreamWriter(dir + "\new2.plt")
            sw.WriteLine(text)
            sw.Close()
        Catch ex As Exception
            logger.Error("exception in Multiplt():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to normalize or position the text in the plt file to the origin
    Private Sub Normalize(ByVal filename As String, ByVal flag As Integer)
        xminlist.Clear()
        Try
            Dim xlist, ylist As New ArrayList
            Dim ratio As Double = 1
            Dim height As Integer = 0
            'ratio to reduce the size of the text to fit the tag
            If flag = 0 Then
                ratio = 48 / finalfontsize
            ElseIf flag = 1 Then
                height = ldiff
                filename = dir + "\char.plt"
            End If
            Dim sr As StreamReader = New StreamReader(filename)
            logger.Info("ratio in normalize:" + ratio.ToString)
            'changing the size of the character/graphic to required font size or to fit the tag
            Do While sr.Peek() >= 0
                Dim line As String = sr.ReadLine
                If line.StartsWith("PU") Or line.StartsWith("PD") Then
                    Dim coord As String() = line.Split(" ")
                    Dim x As Integer = Convert.ToInt16(coord(0).Substring(2, coord(0).Length - 2))
                    'Console.WriteLine(coord(1))
                    xlist.Add(x / ratio)
                    Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                    ylist.Add(y / ratio)
                End If
            Loop
            sr.Close()
            Dim xmin = MinCheck(xlist)
            Dim ymin = MinCheck(ylist)
            Dim xmax = MaxCheck(xlist)
            Dim ymax = MaxCheck(ylist)
            Dim text As String = ""
            sr = New StreamReader(filename)
            'Normalizing the character/graphic with respect to the origin of the plt file
            Do While sr.Peek() >= 0
                Dim line As String = sr.ReadLine
                If line.StartsWith("PU") Or line.StartsWith("PD") Then
                    Dim coord As String() = line.Split(" ")
                    Dim x As Integer = Convert.ToInt16(coord(0).Substring(2, coord(0).Length - 2))
                    Dim y As Integer = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                    'Console.WriteLine(x.ToString)
                    x = (x / ratio) - xmin
                    y = (y / ratio) - ymin - height
                    xminlist.Add(x)
                    text += coord(0).Substring(0, 2) + x.ToString + " " + y.ToString + ";" + Environment.NewLine
                Else
                    text += line + Environment.NewLine
                End If
            Loop
            sr.Close()
            'write the normalized text to a plt file
            Dim sw As StreamWriter = New StreamWriter(dir + "\char.plt")
            sw.WriteLine(text)
            sw.Close()
        Catch ex As Exception
            logger.Error("exception in Normalize():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to generate a plt file from the text entered by the user
    Private Function GeneratePlt(ByVal input As String(), ByVal filecount As Integer) As String
        Try
            Dim count, diff As Int16
            Dim flag As Boolean = True
            Dim sflag As Boolean = False
            Dim cflag As Boolean = True
            Dim spaceflag As Boolean = False
            Dim xflag As Boolean = False
            Dim text As String
            Dim x, y, wx, hy, lx As New ArrayList()
            Dim dest As String = dir + "\new.plt"
            Dim file1 As String
            Dim lines As String() = input
            'Dim lines As String() = input.Split(",")
            Dim space As Integer = 0
            Dim xlist, ylist As New ArrayList()
            diff = 0
            ldiff = 0
            ffsList.Clear()
            logger.Info("lines length:" + lines.Length.ToString)
            'iterating through lines of text
            For j = 0 To lines.Length - 1
                text = ""
                'retrieving characters in a line
                Dim chars As Char() = lines(j).ToCharArray
                x.Clear()
                If j > 0 Then
                    oldfontsize = finalfontsize
                    'check for overlapping of height between multiple lines
                    CheckMultipleLines(chars, y, j, lines.Length)
                    y.Clear()
                End If
                'iterating through characters in a line
                For i = 0 To chars.Length - 1
                    logger.Info("i length:" + chars.Length.ToString)
                    logger.Info("chars:" + chars(i).ToString)
                    'if the character is space, increase the offset for next character
                    If chars(i) = " " Then
                        logger.Info("in space")
                        If sflag Then
                            space = space + finalfontsize * 4
                        Else
                            space = finalfontsize * 3
                            If i = 0 And j = 0 Then
                                spaceflag = True
                            End If
                            logger.Info("in else of char space:" + space.ToString)
                        End If
                        sflag = True
                        xflag = True
                        Continue For
                    End If
                    'get the appropriate plt file for the character
                    file1 = GetFile(chars(i), j, lines.Length)
                    Dim hyph As Integer = 0
                    'increase the y-axis offset for below characters, since it should be in the middle from the base
                    If chars(i) = "-" Or chars(i) = "=" Or chars(i) = "~" Then
                        hyph = DecreaseY(1)
                    End If
                    'normalize the obtained plt file
                    Normalize(file1, 0)
                    file1 = dir + "\char.plt"
                    count = 0
                    Dim negy As Integer = 0
                    'decrase the y-axis offset for below characters
                    If chars(i) = "g" Or chars(i) = "p" Or chars(i) = "q" Or chars(i) = "j" Or chars(i) = "y" Then
                        negy = DecreaseY(0)
                        gFlag = True
                    ElseIf chars(i) = "," Then
                        commaFlag = True
                        negy = DecreaseY(2)
                    ElseIf chars(i) = ";" Then
                        negy = DecreaseY(2) / 2
                    ElseIf chars(i) = "^" Or chars(i) = "`" Or chars(i) = "'" Or chars(i) = """" Then
                        'increase the y-axis offset, since these characters should be at the top in a word
                        negy = DecreaseY(3)
                        logger.Info("negy:" + negy.ToString)
                        If hy.Count > 0 Then
                            'Getting the absolute value of the offset if hy is empty, else adding maximum to the offset
                            If MaxCheck(hy) = 0 Then
                                negy = 0 - negy
                            ElseIf MaxCheck(hy) < 0 Then
                                negy = MaxCheck(hy) + negy
                            Else
                                negy = 0 - (MaxCheck(hy) - negy)
                            End If
                        Else
                            logger.Info("finalfontsize:" + finalfontsize.ToString)
                            negy = 0 - (finalfontsize * 35.2 - negy) / 6
                        End If
                        logger.Info("negy:" + negy.ToString)
                    End If
                    Dim mono As Integer = 0
                    If finalfontname.Contains("Monotype") Then
                        mono = finalfontsize * 1.5
                        'decrease the y-axis offset for below characters
                        If chars(i) = "k" Or chars(i) = "f" Or chars(i) = "Q" Or chars(i) = "R" Or chars(i) = "G" Then
                            negy = DecreaseY(0)
                        ElseIf chars(i) = "t" Or chars(i) = "l" Then
                            mono = mono + finalfontsize / 2
                        ElseIf chars(i) = "i" Then
                            mono = mono + finalfontsize * 2
                        ElseIf chars(i) = "y" Then
                            logger.Info("in y of monotype")
                            negy = negy + finalfontsize
                            mono = mono + finalfontsize * 2
                        End If
                        If i > 0 Then
                            'to reduce the sapce for the character after l
                            If chars(i - 1) = "l" Or chars(i - 1) = "i" Then
                                mono = mono + finalfontsize * 3
                            End If
                        End If
                    End If
                    Dim init As Integer = 0
                    'reading the appropriate plt file for the character
                    If IO.File.Exists(file1) Then
                        Dim readlines() As String = IO.File.ReadAllLines(file1)
                        While readlines IsNot Nothing And count < readlines.Length - 1
                            Dim line As String = readlines(count)
                            Dim ins As String = ""
                            If line.Length > 2 Then
                                ins = line.Substring(0, 2)
                            End If
                            If i = 0 Or cflag Then
                                If j = 0 Then
                                    'retrieving x and y co-ordinates of the plt file
                                    If ins = "PU" Or ins = "PD" Then
                                        line = line.Substring(2, line.Length - 3)
                                        Dim data As String() = line.Split(" ")
                                        If spaceflag Then
                                            'offset for space
                                            init = finalfontsize * (lines(j).Length) * 3
                                        End If
                                        'adding the required offsets to y and x co-ordinates
                                        x.Add((Convert.ToInt16(data(0)) + space + init - mono).ToString)
                                        y.Add(Convert.ToInt16(data(1)) - negy + hyph)
                                        If ins = "PU" Then
                                            text += "PU"
                                        Else
                                            text += "PD"
                                        End If
                                        text += (Convert.ToInt16(data(0)) + space + init - mono).ToString + " " + (Convert.ToInt16(data(1)) - negy + hyph).ToString + ";,"
                                        lx.Add((Convert.ToInt16(data(0)) + space + init - mono))
                                        If i = 0 Or xflag Then
                                            wx.Add((Convert.ToInt16(data(0)) + space + init - mono).ToString)
                                        End If
                                        hy.Add(Convert.ToInt16(data(1)) - negy + hyph)
                                    End If
                                Else
                                    If ins = "PU" Or ins = "PD" Then
                                        line = line.Substring(2, line.Length - 3)
                                        Dim data As String() = line.Split(" ")
                                        'adding the required offsets to y And x co-ordinates
                                        x.Add((Convert.ToInt16(data(0)) + space - mono).ToString)
                                        y.Add((Convert.ToInt16(data(1)) - ldiff - negy + hyph).ToString)
                                        If ins = "PU" Then
                                            text += "PU"
                                        Else
                                            text += "PD"
                                        End If
                                        text += (Convert.ToInt16(data(0)) + space - mono).ToString + " " + (Convert.ToInt16(data(1)) - ldiff - negy + hyph).ToString + ";,"
                                        lx.Add((Convert.ToInt16(data(0)) + space - mono))
                                        If i = 0 Or xflag Then
                                            wx.Add((Convert.ToInt16(data(0)) + space - mono).ToString)
                                        End If
                                        hy.Add((Convert.ToInt16(data(1)) - ldiff - negy + hyph).ToString)
                                    End If
                                End If
                            Else
                                If count = 0 And i > 0 Then
                                    GetMin(file1)
                                    'check for overlap between characters and increase the x axis co-ordinates accordingly
                                    CheckOverLap(file1, MaxCheck(x), chars(i), chars(i - 1))
                                End If
                                If ins = "PU" Or ins = "PD" Then
                                    line = line.Substring(2, line.Length - 3)
                                    Dim data As String() = line.Split(" ")
                                    If flag Then
                                        'offset for characters overlapping on the same line
                                        diff = MaxCheck(x) - minc + finalfontsize * 3 + odiff
                                        logger.Info("minc:" + minc.ToString)
                                        logger.Info("IN FLAG GPLT:" + data(0))
                                        'Console.WriteLine("Max:" + MaxCheck(x).ToString)
                                        If chars(i) = "m" Then Console.WriteLine("diff:" + diff.ToString + "," + MaxCheck(x).ToString + "," + data(0))
                                        If sflag Then
                                            diff = diff + space
                                            sflag = False
                                        End If
                                        odiff = 0
                                        x.Clear()
                                    End If
                                    'adding the required offsets to y And x co-ordinates
                                    x.Add((Convert.ToInt16(data(0)) + diff - mono).ToString)
                                    y.Add((Convert.ToInt16(data(1)) - ldiff - negy + hyph).ToString)
                                    If ins = "PU" Then
                                        text += "PU"
                                    Else
                                        text += "PD"
                                    End If
                                    If j = 0 Then
                                        text += (Convert.ToInt16(data(0)) + diff - mono).ToString + " " + (Convert.ToInt16(data(1)) - negy + hyph).ToString + ";,"
                                        lx.Add((Convert.ToInt16(data(0)) + diff - mono))
                                        If i = chars.Length - 1 Then
                                            wx.Add((Convert.ToInt16(data(0)) + diff - mono).ToString)
                                        End If
                                        hy.Add(data(1).ToString)
                                    ElseIf i = 0 Then
                                        text += (Convert.ToInt16(data(0)) - mono).ToString + " " + (Convert.ToInt16(data(1)) - ldiff - negy + hyph).ToString + ";,"
                                        lx.Add((Convert.ToInt16(data(0)) - mono))
                                        If i = chars.Length - 1 Then
                                            wx.Add((Convert.ToInt16(data(0)) - mono).ToString)
                                        End If
                                        hy.Add((Convert.ToInt16(data(1)) - ldiff).ToString)
                                    Else
                                        text += (Convert.ToInt16(data(0)) + diff - mono).ToString + " " + (Convert.ToInt16(data(1)) - ldiff - negy + hyph).ToString + ";,"
                                        lx.Add((Convert.ToInt16(data(0)) + diff - mono))
                                        If i = chars.Length - 1 Then
                                            wx.Add((Convert.ToInt16(data(0)) + diff - mono).ToString)
                                        End If
                                        hy.Add((Convert.ToInt16(data(1)) - ldiff).ToString)
                                    End If
                                    flag = False
                                End If
                            End If
                            If count = readlines.Length - 2 Then
                                If j = lines.Length - 1 And i = chars.Length - 1 And Not Label1.Text.Contains("Rabies/ID") Then
                                    'text += "SP0;"
                                Else
                                    'text += "SP1;" + Environment.NewLine
                                    flag = True
                                End If
                            End If
                            count = count + 1
                        End While
                    Else
                        logger.Info("File not found for: " + chars(i))
                    End If
                    cflag = False
                    space = 0
                    spaceflag = False
                Next
                'checking if the length of the text exceeds the width of the tag, if so font is reduced
                text = CheckLength(text, lx)
                xlist.Add(text)
                CalculateWidthHeight(wx, hy)
                cflag = True
                wx.Clear()
                hy.Clear()
                lx.Clear()
            Next
            'centering the lines around the center point of the tag
            Dim t As String = CenterLines(xlist, lines)
            'checking if the height of the text exceeds the height of the tag, if so font is reduced
            t = CheckHeight(t)
            If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
                dest = dir + "\" + filecount.ToString + ".plt"
            End If
            'writing the resulting plt to a file
            Dim fs As FileStream = File.Create(dest)
            fs.Close()
            If IO.File.Exists(dest) Then
                Dim objWriter As New System.IO.StreamWriter(dest)
                objWriter.Write(t)
                objWriter.Close()
            End If
            Return text
        Catch ex As Exception
            logger.Error("exception in GeneratePlt():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    'Method to check if the height of the entire text exceeds the height of the tag
    'if exceeds, decreases the font of the text
    Public Function CheckHeight(ByVal text As String) As String
        Dim params() As String
        Dim ylist As New ArrayList
        Dim temptext As String = ""
        Dim ratio As Double = 0
        Dim dest As String = dir + "\temp.plt"
        'retrieving dimensions and co-ordinates of the tag
        If multiflag Then
            params = ReadDimensions("multi").Split(",")
        Else
            params = ReadDimensions("").Split(",")
        End If
        'calculating the height of the tag
        Dim height As Integer = (Convert.ToInt16(params(params.Length - 1)) / 1000) * 400
        Dim fs As FileStream = File.Create(dest)
        fs.Close()
        If IO.File.Exists(dest) Then
            Dim objWriter As New System.IO.StreamWriter(dest)
            objWriter.Write(text)
            objWriter.Close()
        End If
        Dim sr = New StreamReader(dir + "\temp.plt")
        'retrieving the y-axis co-ordinates from the plt file
        Do While sr.Peek() >= 0
            Dim line As String = sr.ReadLine
            If line.StartsWith("PU") Or line.StartsWith("PD") And line.Length > 4 Then
                Dim coord As String() = line.Split(" ")
                Dim y As Double = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                ylist.Add(Convert.ToInt16(y))
            End If
        Loop
        sr.Close()
        'calculating the height of the text entered from the plt file
        Dim th As Integer = MaxCheck(ylist) - MinCheck(ylist)
        'reducing the height if it is greater than the height of the tag
        While th > height
            ratio = th / height
            temptext = ""
            ylist.Clear()
            sr = New StreamReader(dir + "\temp.plt")
            Do While sr.Peek() >= 0
                Dim line As String = sr.ReadLine
                If line.StartsWith("PU") Or line.StartsWith("PD") And line.Length > 4 Then
                    Dim coord As String() = line.Split(" ")
                    Dim x As Double = Convert.ToInt16(coord(0).Substring(2, coord(0).Length - 2))
                    Dim y As Double = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                    x = Math.Floor(Convert.ToDouble(x) / ratio)
                    y = Math.Floor(Convert.ToDouble(y) / ratio)
                    'logger.Info("Afterx:" + x.ToString + "y:" + y.ToString)
                    temptext += coord(0).Substring(0, 2) + Convert.ToInt16(x).ToString + " " + Convert.ToInt16(y).ToString + ";" + Environment.NewLine
                    'logger.Info(coord(0).Substring(0, 2) + Convert.ToInt16(x).ToString + " " + Convert.ToInt16(y).ToString + ";" + Environment.NewLine)
                    ylist.Add(Convert.ToInt16(y))
                Else
                    temptext += line + Environment.NewLine
                End If
            Loop
            sr.Close()
            th = MaxCheck(ylist) - MinCheck(ylist)
        End While
        If temptext.Length > 0 Then
            Return temptext
        Else
            Return text
        End If
    End Function

    'Method to check if the width of the entire text exceeds the width of the tag
    'if exceeds, decreases the width of the text
    Private Function CheckLength(ByVal text As String, ByVal lenx As ArrayList) As String
        Try
            logger.Info("wx length:" + lenx.Count.ToString)
            If lenx.Count > 0 Then
                Dim min As Integer = MinCheck(lenx)
                Dim flag As Boolean = False
                Dim max As Integer = MaxCheck(lenx)
                Dim params() As String
                'retrieving dimensions and co-ordinates of the tag
                If multiflag Then
                    params = ReadDimensions("multi").Split(",")
                Else
                    params = ReadDimensions("").Split(",")
                End If
                Dim data() As String = text.Split(",")
                Dim sw As StreamWriter = New StreamWriter(dir + "\char.plt")
                Dim sr As StreamReader
                logger.Info("params length:" + params.Length.ToString)
                'calculating the width of the tag
                Dim width As Integer = (Convert.ToInt16(params(params.Length - 2)) / 1000) * 400
                Dim diff As Integer = (max - min)
                Dim ratio As Double = diff / width
                lenx.Clear()
                If ratio > 1 Then
                    'logger.Info("Before text:" + text)
                    For i = 0 To data.Length - 1
                        sw.WriteLine(data(i))
                        'text += data(i) + Environment.NewLine
                    Next
                End If
                sw.Close()
                logger.Info("diff:" + diff.ToString + ",width:" + width.ToString)
                'if the width of the text is greater than the tag, reduce the x co-ordinates 
                While diff - width >= 0
                    ratio = (diff / width)
                    If ratio <= 1 Then
                        Exit While
                    End If
                    logger.Info("ratio:" + ratio.ToString)
                    text = ""
                    sr = New StreamReader(dir + "\char.plt")
                    Do While sr.Peek() >= 0
                        Dim line As String = sr.ReadLine
                        If line.StartsWith("PU") Or line.StartsWith("PD") And line.Length > 4 Then
                            Dim coord As String() = line.Split(" ")
                            Dim x As Double = Convert.ToInt16(coord(0).Substring(2, coord(0).Length - 2))
                            Dim y As Double = Convert.ToInt16(coord(1).Substring(0, coord(1).Length - 1))
                            x = Math.Floor(Convert.ToDouble(x + 2000) / ratio)
                            text += coord(0).Substring(0, 2) + Convert.ToInt16(x).ToString + " " + Convert.ToInt16(y).ToString + ";" + Environment.NewLine
                            lenx.Add(Convert.ToInt16(x))
                        End If
                    Loop
                    sr.Close()
                    diff = MaxCheck(lenx) - MinCheck(lenx)
                    lenx.Clear()
                    flag = True
                End While
                'write the modified plt to a file
                If flag Then
                    sw = New StreamWriter(dir + "\char.plt")
                    sw.WriteLine(text)
                    sw.Close()
                    Normalize(dir + "\char.plt", 1)
                    sr = New StreamReader(dir + "\char.plt")
                    text = ""
                    Do While sr.Peek() >= 0
                        Dim line As String = sr.ReadLine
                        If line.StartsWith("PU") Or line.StartsWith("PD") And line.Length > 4 Then
                            text += line + ","
                        End If
                    Loop
                    sr.Close()
                    'logger.Info("text:" + text)
                End If
                Return text
            End If
        Catch ex As Exception
            logger.Error("exception in CheckLength():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    'Method to calculate maximum and minimum width and height positions of the text in plt file
    Sub CalculateWidthHeight(ByVal x As ArrayList, ByVal y As ArrayList)
        Try
            xmax.Add(MaxCheck(x))
            ymax.Add(MaxCheck(y))
            xmin.Add(MinCheck(x))
            ymin.Add(MinCheck(y))
        Catch ex As Exception
            logger.Error("exception in CalcWandH():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to check if an offset on y-axis needs to be added to a character
    'For characters like 'y',',','g' etc.
    Function DecreaseY(ByVal i As Integer) As Integer
        Try
            Dim diff As Integer = 0
            Dim file1 As String = dir + "\char.plt"
            Dim count As Integer = 0
            Dim y As New ArrayList
            'retrieving the y co-ordinates from the plt file
            If IO.File.Exists(file1) Then
                Dim readlines() As String = IO.File.ReadAllLines(file1)
                While readlines IsNot Nothing And count < readlines.Length - 1
                    Dim line As String = readlines(count)
                    Dim ins As String = ""
                    If line.Length > 2 Then
                        ins = line.Substring(0, 2)
                    End If
                    If ins = "PU" Or ins = "PD" Then
                        line = line.Substring(2, line.Length - 3)
                        Dim data As String() = line.Split(" ")
                        y.Add(data(1))
                    End If
                    count = count + 1
                End While
            End If
            'for characters like g,,q,p and y
            If i = 0 Then
                diff = (MaxCheck(y) - MinCheck(y)) / 4
            ElseIf i = 1 Then
                'for characters like -,= etc
                diff = (MaxCheck(y) - MinCheck(y)) / 2
            ElseIf i = 2 Then
                'for comma
                diff = Math.Abs(MaxCheck(y) - MinCheck(y))
            ElseIf i = 3 Then
                'for characters like ^,", etc
                diff = MaxCheck(y)
            End If
            Return diff
        Catch ex As Exception
            logger.Error("exception in DecreaseY():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    'Method to center the lines of text in the plt file
    Function CenterLines(ByVal textlist As ArrayList, ByVal lines As String()) As String
        Try
            logger.Info("In center lines function")
            Dim max As Integer = 0
            Dim x, xdist As New ArrayList()
            Dim text As String = "IN;" + Environment.NewLine + "SP1;" + Environment.NewLine
            'getting the maximum length line
            For j = 0 To lines.Length - 1
                If max < lines(j).Length Then
                    max = j
                End If
            Next
            'iterating through the lines
            For l = 0 To textlist.Count - 1
                Dim large As String() = textlist.Item(l).ToString.Split(",")
                'retrieving x co-ordinates to calculate the length
                For k = 0 To large.Length - 1
                    If large(k).Length > 2 Then
                        Dim coord As String() = large(k).Substring(2, large(k).Length - 3).Split(" ")
                        x.Add(coord(0))
                    End If
                Next
                xdist.Add(MaxCheck(x) - MinCheck(x))
                x.Clear()
            Next
            Dim dist As Integer = xdist.Item(max)
            'calculating the offset to center the text
            For i = 0 To textlist.Count - 1
                If i = max Then
                    Dim data As String() = textlist.Item(i).ToString.Split(",")
                    For k = 0 To data.Length - 1
                        If data(k).Length > 0 Then
                            text += data(k) + Environment.NewLine
                        End If
                    Next
                Else
                    Dim offset As Integer = (dist - xdist.Item(i)) / 2
                    'offset is zero for tags with cursor at left hand side
                    If tagNo = 20 Or tagNo = 22 Then
                        offset = 0
                    End If
                    Dim data As String() = textlist.Item(i).ToString.Split(",")
                    For k = 0 To data.Length - 1
                        If data(k).Length > 0 Then
                            Dim coord As String() = data(k).Substring(2, data(k).Length - 3).Split(" ")
                            text += data(k).Substring(0, 2) + (Convert.ToInt16(coord(0)) + offset).ToString + " " + coord(1).ToString + ";" + Environment.NewLine
                        End If
                    Next
                End If
            Next
            text += "SP0;"
            Return text
        Catch ex As Exception
            logger.Error("exception in CenterLines():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    'Method to get the font size of the text in a line from the RichTextBox
    Function GetFontSize(ByVal txtbox As Integer, ByVal line As Integer) As Double
        Try
            Dim text As String = ""
            Dim rts As Double = 0
            If txtbox = 1 Then
                If line = 1 Or RichTextBox1.Lines.Length < 2 Then
                    text = RichTextBox1.Lines(0)
                    RichTextBox1.Select(0, text.Length)
                Else
                    text = RichTextBox1.Lines(1)
                    RichTextBox1.Select(RichTextBox1.Lines(0) + 1, text.Length - 1)
                End If
                rts = RichTextBox1.SelectionFont.Size
            ElseIf txtbox = 2 Then
                'getting the font size for line given as input
                Dim start, fin As Integer
                start = 0
                fin = 0
                If line = 1 Then
                    text = RichTextBox2.Lines(0)
                    fin = text.Length
                    RichTextBox2.Select(0, fin)
                ElseIf line = 2 Then
                    text = RichTextBox2.Lines(1)
                    start = RichTextBox2.Lines(0).Length
                    fin = text.Length + 1
                    RichTextBox2.Select(start, fin)
                ElseIf line = 3 Then
                    text = RichTextBox2.Lines(2)
                    start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length
                    fin = text.Length + 1
                    RichTextBox2.Select(start + 1, fin)
                ElseIf line = 4 Then
                    text = RichTextBox2.Lines(3)
                    start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length
                    fin = text.Length + 1
                    RichTextBox2.Select(start + 2, fin)
                ElseIf line = 5 Then
                    text = RichTextBox2.Lines(3)
                    start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 1
                    fin = text.Length + 4
                    RichTextBox2.Select(start + 2, fin)
                End If
                rts = RichTextBox2.SelectionFont.Size
            Else
                If line = 1 Then
                    text = RichTextBox3.Lines(0)
                    RichTextBox3.Select(0, text.Length)
                Else
                    text = RichTextBox3.Lines(1)
                    RichTextBox3.Select(RichTextBox3.Lines(0).Length + 1, text.Length - 1)
                End If
                rts = RichTextBox3.SelectionFont.Size
            End If
            logger.Info("rts:" + rts.ToString)
            Return rts
        Catch ex As Exception
            logger.Error("exception in GetFontSize():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Environment.Exit(0)
    End Sub

    'Method to calculate the font of a character and retrieve appropriate plt file
    Function GetFile(ByVal c As Char, ByVal lnum As Integer, ByVal len As Integer) As String
        Try
            Dim font As String = ""
            Dim size As Integer = 0
            Dim ratio As Double
            Dim params As String() = ReadDimensions("").Split(",")
            Dim width As Double
            Dim height As Double
            Dim lno As Integer = RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length
            'retrieving the dimensions and co-ordinates of the tag
            If Label1.Text.Contains("Rabies/ID") Or Label1.Text.Contains("Info/ID") Then
                params = ReadDimensions("multi").Split(",")
                width = (Convert.ToDouble(params(params.Length - 2)) / 100) * 0.0393
                height = (Convert.ToDouble(params(params.Length - 1)) / 100) * 0.0393
            Else
                width = (Convert.ToDouble(params(params.Length - 2)) / 100) * 0.0393
                height = (Convert.ToDouble(params(params.Length - 1)) / 100) * 0.0393
            End If
            logger.Info("Area of the tag:" + (width * height).ToString)
            Dim area As Double = Math.Round(width * height, 1)
            logger.Info("Area of the tag:" + area.ToString)
            'calculating the ratio with respect to the area of the tag
            If width * height >= 0.9 Then
                ratio = 108 / 48
            ElseIf width * height >= 0.5 And width * height < 0.9 Then
                ratio = 108 / 36
            ElseIf width * height >= 0.4 Then
                If lno < 3 Then
                    logger.Info("in lno less than 3")
                    ratio = (108 * area) / 12
                Else
                    If lnum + 1 > 5 Then
                        ratio = (108 * area) / 10
                    Else
                        ratio = (108 * area) / 16
                    End If
                End If
            Else
                ratio = (108 * area) / 12
            End If
            lnum = lnum + 1
            logger.Info("lnum:" + lnum.ToString)
            If lnum = 1 Then
                RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                'font = rtf1
            ElseIf lnum = 2 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            ElseIf lnum = 3 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                'font = rtf3
            ElseIf lnum = 3 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                'font = rtf3
            ElseIf lnum = 4 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                'font = rtf4
            ElseIf lnum = 5 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                'font = rtf5
            ElseIf lnum = 6 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                'font = rtf6
            ElseIf lnum = 7 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                'font = rtf7
            End If
            'calculating the size and font as per size of the tag
            size = RichTextBox2.SelectionFont.Size / ratio
            font = RichTextBox2.SelectionFont.Name
            Dim style As FontStyle = RichTextBox2.SelectionFont.Style
            If font.Contains("Microsoft") Then
                font = "Arial Narrow"
            End If
            If style = FontStyle.Bold Then
                font = font + " Bold"
            End If
            logger.Info("font name:" + font)
            size = Math.Ceiling(size)
            logger.Info("size:" + size.ToString)
            'calculating the size with respect to the area of the tag
            If tagNo = 8 Or tagNo = 9 Or tagNo = 24 Or tagNo = 27 Or tagNo = 26 Or tagNo = 13 Then
                size = size + 2
            End If
            If tagNo = 21 Or tagNo = 23 Then
                If size <= 7 Then
                    size = size + 5
                Else
                    size = size + 3
                End If
            End If
            If size > 42 Then
                size = 48
            ElseIf size > 30 Then
                size = 36
            ElseIf size > 21 Then
                size = 24
            ElseIf size > 17 Then
                size = 18
            ElseIf size >= 15 Then
                size = 16
            ElseIf size >= 13 Then
                size = 14
            ElseIf size >= 12 Then
                size = 12
            ElseIf size >= 7 And area >= 0.4 Then
                size = 8
            ElseIf size >= 5 Then
                size = 6
            Else
                size = 4
            End If
            finalfontsize = size
            finalfontname = font
            ffsList.Add(finalfontsize)
            'preparing the file name of the plt to be retrieved
            If Char.IsLower(c) Then
                logger.Info("path:" + path + "lower\" + font + "_" + c + "_" + (48 / finalfontsize).ToString + ".plt")
                'Return path + "lower\" + font + "_" + c + "_" + size.ToString + ".plt"
                Return path + "lower\" + font + "_" + c + "_48.plt"
            ElseIf Char.IsUpper(c) Then
                logger.Info("path:" + path + "upper\" + font + "_" + c + "_" + (48 / finalfontsize).ToString + ".plt")
                'Return path + "upper\" + font + "_" + c + "_" + size.ToString + ".plt"
                Return path + "upper\" + font + "_" + c + "_48.plt"
            Else
                Dim temp As String = c
                If temp = """" Then
                    temp = "quote"
                ElseIf temp = "\" Then
                    temp = "slash"
                ElseIf temp = "/" Then
                    temp = "fslash"
                ElseIf temp = "*" Then
                    temp = "ast"
                ElseIf temp = ":" Then
                    temp = "colon"
                ElseIf temp = ">" Then
                    temp = "great"
                ElseIf temp = "<" Then
                    temp = "less"
                ElseIf temp = "|" Then
                    temp = "pipe"
                ElseIf temp = "?" Then
                    temp = "ques"
                End If
                logger.Info("path:" + path + "special\" + font + "_" + temp + "_" + (48 / finalfontsize).ToString + ".plt")
                'Return path + "special\" + font + "_" + c + "_" + size.ToString + ".plt"
                Return path + "special\" + font + "_" + temp + "_48.plt"
            End If
        Catch ex As Exception
            logger.Error("exception in GetFile():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    'Method to retrieve maximum element in an ArrayList
    Public Function MaxCheck(ByVal list As ArrayList) As Integer
        Dim m As Integer = 0
        For i = 0 To list.Count - 1
            If m < list.Item(i) Then
                m = list(i)
            End If
        Next
        Return m
    End Function

    'Method to retrieve minimum element in an ArrayList
    Public Function MinCheck(ByVal list As ArrayList) As Integer
        logger.Info("list length:" + list.Count.ToString)
        Dim min As Integer = list.Item(0)
        For i = 1 To list.Count - 1
            If min > list.Item(i) Then
                min = list(i)
            End If
        Next
        Return min
    End Function

    'Method to get the minimum x co-ordinate from a plt file
    Sub GetMin(ByVal path As String)
        Dim count As Integer = 0
        Dim list As New ArrayList()
        If IO.File.Exists(path) Then
            Dim readlines() As String = IO.File.ReadAllLines(path)
            While readlines IsNot Nothing And count < readlines.Length
                Dim line As String = readlines(count)
                Dim ins As String = ""
                If line.Length > 2 Then
                    ins = line.Substring(0, 2)
                End If
                If ins = "PU" Or ins = "PD" Then
                    line = line.Substring(2, line.Length - 3)
                    Dim data As String() = line.Split(" ")
                    logger.Info("data:" + data(0))
                    list.Add(Convert.ToInt16(data(0)).ToString)
                End If
                count = count + 1
            End While
        End If
        minc = MinCheck(list)
    End Sub

    'Method to check if there is any overlap between the characters
    'if any, increases the x position of the overlapping character
    Sub CheckOverLap(ByVal path As String, ByVal max As Integer, ByVal character As String, ByVal character2 As String)
        Try
            Dim count As Integer = 0
            Dim diff As Integer
            Dim flag As Boolean = True
            Dim list As New ArrayList()
            If IO.File.Exists(path) Then
                Dim readlines() As String = IO.File.ReadAllLines(path)
                'retrieving x co-ordinates of the character to check for overlap
                While readlines IsNot Nothing And count < readlines.Length
                    Dim line As String = readlines(count)
                    Dim ins As String = ""
                    If line.Length > 2 Then
                        ins = line.Substring(0, 2)
                    End If
                    If ins = "PU" Or ins = "PD" Then
                        line = line.Substring(2, line.Length - 3)
                        Dim data As String() = line.Split(" ")
                        If flag Then
                            diff = max - minc + finalfontsize * 3
                            flag = False
                        End If
                        list.Add((Convert.ToInt16(data(0)) + diff).ToString)
                    End If
                    count = count + 1
                End While
            End If
            'getting the minimum element in x co-ordinates
            Dim min As Integer = list.Item(0)
            For i = 1 To list.Count - 1
                If min > list.Item(i) Then
                    min = list(i)
                End If
            Next
            logger.Info("max:" + max.ToString + ",min:" + min.ToString)
            Dim temp As Int16 = 0
            'increasing the space for below characters
            If character = "i" Or character = "I" Or character = "l" Or character2 = "i" Or character2 = "I" Or character2 = "l" Then
                temp = finalfontsize * 3
            End If
            'calculating the offset for placing the characters side by side 
            If min - temp <= max Then
                odiff = Math.Abs(max - min) + finalfontsize * 3
                If Not temp = 0 Then
                    odiff = Math.Abs(max - min) - finalfontsize
                End If
                logger.Info("in odiff:" + odiff.ToString)
            End If
        Catch ex As Exception
            logger.Error("exception in CheckOverlap():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to check for overlapping by height in case of multiple lines
    'if any, adds an offset to the y co-ordinates of the line
    Sub CheckMultipleLines(ByVal chars As Char(), ByVal y As ArrayList, ByVal lnum As Integer, ByVal len As Integer)
        Try
            Dim lno As Integer = RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length
            Dim y2 As New ArrayList()
            'calculating the minimum y co-ordinate for the line to add an offset 
            For i = 0 To chars.Length - 1
                If chars(i) = " " Or chars(i) = "g" Or chars(i) = "," Then
                    Continue For
                End If
                'get the name of the plt file
                Dim name As String = GetFile(chars(i), lnum, len)
                Normalize(name, 0)
                name = dir + "\char.plt"
                Dim count As Integer
                count = 0
                'get the y co-ordinates in plt file of a line
                If IO.File.Exists(name) Then
                    Dim readlines() As String = IO.File.ReadAllLines(name)
                    While readlines IsNot Nothing And count < readlines.Length - 1
                        Dim line As String = readlines(count)
                        Dim ins As String = ""
                        If line.Length > 2 Then
                            ins = line.Substring(0, 2)
                        End If
                        If ins = "PU" Or ins = "PD" Then
                            line = line.Substring(2, line.Length - 3)
                            Dim data As String() = line.Split(" ")
                            y2.Add(data(1))
                        End If
                        count = count + 1
                    End While
                End If
            Next
            'getting the min of previous line and maximum y co-ordinate of current line
            Dim min As Integer = MinCheck(y)
            Dim max As Integer = MaxCheck(y2)
            Dim mul As Integer = 4
            'changing the y axis offset according to the tag and characters present in the line
            If finalfontsize >= 12 Then
                mul = 3
            End If
            mul = 6
            If chars.Contains(",") Then
                mul = 10
            End If
            If chars.Contains(",") And (tagNo = 6 Or tagNo = 32) Then
                mul = 6
            End If
            Dim temp As Integer = 1
            If commaFlag And Not (tagNo = 2 Or tagNo = 8 Or tagNo = 28 Or tagNo = 27) Then
                temp = 3
                commaFlag = False
            End If
            If gFlag And (tagNo = 10 Or tagNo = 14 Or tagNo = 6 Or tagNo = 7 Or tagNo = 32) Then
                temp = 3
                gFlag = False
            End If
            If chars.Contains(",") And (tagNo = 3 Or tagNo = 13 Or tagNo = 25 Or tagNo = 26) Then
                mul = 10
            End If
            'mul = 6
            'if maximum in current line is greater than minimum y co-ordinate increase the offset
            If max > min Then
                logger.Info("max:" + max.ToString + "min:" + min.ToString)
                If lno = 0 Then
                    ldiff = Math.Abs(max) + (MaxCheck(ffsList) * mul) / temp
                Else
                    ldiff = Math.Abs(max - min) + (MaxCheck(ffsList) * mul) / temp
                End If
                ldiff = Math.Abs(max - min) + (MaxCheck(ffsList) * mul) / temp
            Else
                ldiff = MaxCheck(ffsList) * mul / temp
            End If
            ldiff = Math.Abs(ldiff)
            logger.Info("ldiff in checkmultiplelines:" + ldiff.ToString + " ldiff count:" + ldiffList.Count.ToString)
            If ldiffList.Count > 1 Then
                Dim diff As Integer = ldiff - ldiffList(ldiffList.Count - 1)
                If diff > ldiffList(0) Then
                    ldiff = ldiff - (diff - ldiffList(0))
                ElseIf diff < ldiffList(0) Then
                    ldiff = ldiff + ldiffList(0) - diff
                End If
            End If
            ldiffList.Add(ldiff)
            logger.Info("ldiff in checkmultiplelines:" + ldiff.ToString)
        Catch ex As Exception
            logger.Error("exception in CheckMultipleLines():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to retrieve the text entered in the RichTextBox
    Function GetText() As String()
        Dim rt(RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length - 1) As String
        Dim c As Integer
        Dim i As Integer = 0
        For c = 0 To RichTextBox1.Lines.Length - 1
            rt(i) = RichTextBox1.Lines(c)
            logger.Info("RT1:" + rt(i))
            i = i + 1
        Next
        For c = 0 To RichTextBox2.Lines.Length - 1
            rt(i) = RichTextBox2.Lines(c)
            logger.Info("RT2:" + rt(i))
            i = i + 1
        Next
        For c = 0 To RichTextBox3.Lines.Length - 1
            rt(i) = RichTextBox3.Lines(c)
            logger.Info("RT3:" + rt(i))
            i = i + 1
        Next
        logger.Info("rt length:" + rt.Length.ToString)
        Return rt
    End Function

    'Method to generate parameters to be set for HpglToBin Software to transfer the plt file to the machine
    Private Sub Hpgltobin()
        Try
            Dim filePath As String = dir + "\new2.plt"
            Dim txt As String = System.IO.File.ReadAllText(filePath)
            Dim i As Integer
            Dim param As String = ""
            param = "!JH 328;" + filePath
            Dim nullCount As Integer = 0
            nullCount = 64 - filePath.Length
            'SoftName
            Dim decode As String = Base64Decode("AA==")
            For i = 1 To nullCount
                param += decode
            Next
            'PrinterName
            param += "GT Smartstream"
            For i = 1 To 50
                param += decode
            Next
            'MachineName
            param += "TAGCUBE"
            For i = 1 To 57
                param += decode
            Next
            'width of the holder
            param += IntToLittleEndian(7000)
            'height of the holder
            param += IntToLittleEndian(5999)
            'x origin
            param += IntToLittleEndian(0)
            'y origin
            param += IntToLittleEndian(0)
            For i = 1 To 68
                param += decode
            Next
            param += IntToLittleEndian(1)
            For i = 1 To 32
                param += decode
            Next
            param += IntToLittleEndian(0)
            'clamping percentage
            param += IntToLittleEndian(50)
            'number of plates to be engarved
            param += IntToLittleEndian(plates)
            param += IntToLittleEndian(2)
            txt = param + txt
            'Dim pltPath As String = dir+"\tags\meta\plt\20.plt"
            'My.Computer.FileSystem.CopyFile(pltPath, destPath, True)
            If System.IO.File.Exists(destPath) = True Then
                'write the parameters to the top of the plt file
                Dim objWriter As New System.IO.StreamWriter(destPath)
                logger.Info("in txt:" + txt)
                objWriter.Write(txt)
                objWriter.Close()
            End If
        Catch ex As Exception
            logger.Error("exception in HpglTobin():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to convert Base64 String to ASCII Encoding
    Function Base64Decode(ByVal text As String) As String
        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(text)
        Dim decodedText As String
        decodedText = Encoding.ASCII.GetString(decodedBytes)
        logger.Info("decoded text:" + decodedText)
        Return decodedText
    End Function

    'Method to convert an integer to LittleEndian format
    Function IntToLittleEndian(ByVal param As Integer) As String
        Dim i As Byte() = BitConverter.GetBytes(param)
        If Not BitConverter.IsLittleEndian Then
            Array.Reverse(i)
        End If
        logger.Info("int to endian:" + Convert.ToBase64String(i))
        Dim b As String = Base64Decode(Convert.ToBase64String(i))
        Return b
    End Function

    'Method to retrieve dimensions of the tag and its position(containing x and y co-ordinates) on the holder
    Function ReadDimensions(ByVal desc As String) As String
        Try
            Dim count As Integer = 0
            Dim textfile As String = dir + "\tags\meta\dimensions.txt"
            'dimensions and co-ordinates of multi tag engraving
            If desc = "multi" Then
                textfile = dir + "\tags\meta\dimensions_multi.txt"
            End If
            logger.Info("tag no:" + tagNo.ToString)
            'iterating through the text file until appropriate tag number is encountered
            If IO.File.Exists(textfile) Then
                Dim readlines() As String = IO.File.ReadAllLines(textfile)
                While readlines IsNot Nothing And count < readlines.Length
                    Dim s As String() = readlines(count).Split(":")
                    logger.Info("s:" + s(0) + "tag:" + tagNo.ToString)
                    If s(0) = tagNo Then
                        logger.Info("in tag match")
                        Return s(1)
                    End If
                    count = count + 1
                End While
            End If
            Return 0
        Catch ex As Exception
            logger.Error("exception in ReadDimensions():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Function

    'checking for the minimum font
    'if minimum font is encountered on any of the lines return false
    Private Function checkFont() As Boolean
        If RichTextBox2.Lines.Count >= 1 Then
            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        If RichTextBox2.Lines.Count >= 2 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        If RichTextBox2.Lines.Count >= 3 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        If RichTextBox2.Lines.Count >= 4 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        If RichTextBox2.Lines.Count >= 5 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        If RichTextBox2.Lines.Count >= 6 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        If RichTextBox2.Lines.Count >= 7 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        Return True
    End Function

    'checking for font of line containing the cursor
    'retruns false if font less than 12 else true
    Private Function checkLineFont() As Boolean
        If cursorPos2 = 0 Then
            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        ElseIf cursorPos2 = 1 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        ElseIf cursorPos2 = 2 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        ElseIf cursorPos2 = 3 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        ElseIf cursorPos2 = 4 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        ElseIf cursorPos2 = 5 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        ElseIf cursorPos2 = 6 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            If RichTextBox2.SelectionFont.Size < 12 Then
                Return False
            End If
        End If
        Return True
    End Function

    'Button Click event to increase the font of the text
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        'calculating the dimesions of the text
        CalculateDim()
        HeightRtb()
        Try
            logger.Info("Focus:" + t2Focus.ToString)
            logger.Info("total:" + total.ToString + "height:" + RichTextBox2.Height.ToString)
            If t2Focus And RichTextBox2.Text.Length > 0 Then
                'if height of the text is greater than the height of the textbox, increase the font
                If total < RichTextBox2.Height + 11 Then
                    Dim text As String = ""
                    Dim start, fin As Integer
                    start = 0
                    fin = 0
                    logger.Info("cursor:" + cursorPos2.ToString)
                    'to test for minimum font
                    If Not checkFont() And checkLineFont() Then
                        'if there is minimum font no more font reduction
                        Exit Sub
                    End If
                    If cursorPos2 = 0 Then
                        'if cursor is on first line
                        text = RichTextBox2.Lines(0)
                        fin = text.Length
                        logger.Info("width:" + currentw.ToString + "rtbwidth:" + RichTextBox2.Width.ToString)
                        'if the width of the first line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            logger.Info("in font2:" + RichTextBox2.SelectionFont.Size.ToString)
                            'decreasing the font of other lines to autosize
                            If RichTextBox2.Lines.Count >= 2 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts2 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 3 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts3 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 4 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts4 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'increasing the font of current line
                            RichTextBox2.Select(start, fin)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            logger.Info("in font2:" + RichTextBox2.SelectionFont.Size.ToString)
                            rts1 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 1 Then
                        'if cursor is on the second line
                        text = RichTextBox2.Lines(1)
                        start = RichTextBox2.Lines(0).Length + 1
                        fin = text.Length
                        logger.Info("width:" + currentw.ToString)
                        'if width of the current line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            'decrasing the font of first line
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            'decreasing the font of the other lines
                            If RichTextBox2.Lines.Count >= 3 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts3 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 4 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts4 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            logger.Info("start:" + start.ToString + "fin:" + fin.ToString)
                            'increasing the font of the current line
                            RichTextBox2.Select(start, fin)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 2 Then
                        'if cursor is on third line
                        text = RichTextBox2.Lines(2)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 1
                        fin = text.Length
                        logger.Info("width:" + currentw.ToString)
                        'if width of the current line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            'decrease the font of other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 4 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts4 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'increase the font of current line
                            RichTextBox2.Select(start + 1, fin)
                            start = start + 1
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 3 Then
                        'if the cursor is on fourth line
                        text = RichTextBox2.Lines(3)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 1
                        fin = text.Length
                        logger.Info("width:" + currentw.ToString)
                        'if width of the current line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            'decrease the font of other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'increase the font of the current line
                            RichTextBox2.Select(start + 2, fin)
                            start = start + 2
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 4 Then
                        'if the cursor is on fifth line
                        text = RichTextBox2.Lines(4)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 1
                        fin = text.Length
                        logger.Info("width:" + currentw.ToString)
                        'if width of the current line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            'decrease the font of other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'increase the font of the current line
                            RichTextBox2.Select(start + 3, fin)
                            start = start + 3
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            rts5 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 5 Then
                        'if the cursor is on sixth line
                        text = RichTextBox2.Lines(5)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 1
                        fin = text.Length
                        logger.Info("width:" + currentw.ToString)
                        'if width of the current line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            'decrease the font of the other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts5 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'increase the font of the cuuren line
                            RichTextBox2.Select(start + 4, fin)
                            start = start + 4
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            rts6 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 6 Then
                        'if the cursor is on seventh line
                        text = RichTextBox2.Lines(6)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 1
                        fin = text.Length + 1
                        logger.Info("width:" + currentw.ToString)
                        'if width of the current line is less than the width of the textbox
                        If currentw < RichTextBox2.Width - 5 Then
                            'decrease the font of other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            logger.Info("FONT:" + rts1.ToString)
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts5 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                            rts6 = RichTextBox2.SelectionFont.Size
                            'increase the font of current line
                            RichTextBox2.Select(start + 5, fin)
                            start = start + 5
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 2, RichTextBox2.SelectionFont.Style)
                            rts7 = RichTextBox2.SelectionFont.Size
                        End If
                    End If
                    RichTextBox2.Select()
                    RichTextBox2.SelectionStart = start
                    logger.Info("selection start:" + RichTextBox2.SelectionStart.ToString)
                End If
            End If
        Catch ex As Exception
            logger.Error("exception in Button16_Click():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Button Click event to decrease the font of the text
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        CalculateDim()
        HeightRtb()
        Try
            If t2Focus And RichTextBox2.Text.Length > 0 Then
                'if the total height of the text is greater than one-fourth of the height of the textbox
                If total > RichTextBox2.Height / 4 Then
                    Dim text As String = ""
                    Dim start, fin As Integer
                    start = 0
                    fin = 0
                    'check for minimum font while reducing the font
                    'stop reducing if minimum font is reached
                    If Not checkFont() And Not checkLineFont() Then
                        Exit Sub
                    End If
                    If cursorPos2 = 0 Then
                        'if the cursor is in first line
                        text = RichTextBox2.Lines(0)
                        fin = text.Length
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 10 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increasing the font on other lines
                            If RichTextBox2.Lines.Count >= 2 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts2 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 3 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts3 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 4 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts4 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'decrease the font of current line
                            RichTextBox2.Select(0, fin)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 1 Then
                        'if the cursor is in the second line
                        text = RichTextBox2.Lines(1)
                        start = RichTextBox2.Lines(0).Length + 1
                        fin = text.Length
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 20 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increase the font on other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 3 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts3 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 4 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts4 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'decrease the font of the current line
                            RichTextBox2.Select(start, fin)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 2 Then
                        'if the cursor is in third line
                        text = RichTextBox2.Lines(2)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 1
                        fin = text.Length
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 20 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increase the font on all other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 4 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts4 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'decrease the font of the current line
                            RichTextBox2.Select(start + 1, fin)
                            start = start + 1
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            logger.Info("selection font size:" + RichTextBox2.SelectionFont.Size.ToString)
                        End If
                    ElseIf cursorPos2 = 3 Then
                        'if the cursor is in fourth line
                        text = RichTextBox2.Lines(3)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 1
                        fin = text.Length
                        RichTextBox2.Select(start + 2, fin)
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 20 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increase the font on all other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 5 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts5 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'decrease the font on the current line
                            RichTextBox2.Select(start + 2, fin)
                            start = start + 2
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 4 Then
                        'if the cursor is on fifth line
                        text = RichTextBox2.Lines(4)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 1
                        fin = text.Length
                        RichTextBox2.Select(start + 3, fin)
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 20 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increase the font on all other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 6 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts6 = RichTextBox2.SelectionFont.Size
                            End If
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'decrease the font on the current line
                            RichTextBox2.Select(start + 3, fin)
                            start = start + 3
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts5 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 5 Then
                        'if the cursor is in sixth line
                        text = RichTextBox2.Lines(5)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 1
                        fin = text.Length
                        RichTextBox2.Select(start + 4, fin)
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 10 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increase the font on all other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts5 = RichTextBox2.SelectionFont.Size
                            If RichTextBox2.Lines.Count >= 7 Then
                                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                                RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                                rts7 = RichTextBox2.SelectionFont.Size
                            End If
                            'decrease the font on the current line
                            RichTextBox2.Select(start + 4, fin)
                            start = start + 4
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts6 = RichTextBox2.SelectionFont.Size
                        End If
                    ElseIf cursorPos2 = 6 Then
                        'if the cursor is in seventh line
                        text = RichTextBox2.Lines(6)
                        start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 1
                        fin = text.Length + 1
                        RichTextBox2.Select(start + 5, fin)
                        'if total height is less than the textbox height and maximum width of the text is less than the width of the textbox
                        If total < RichTextBox2.Height + 10 And RichTextBox2.SelectionFont.Size > 6 And maxw < RichTextBox2.Width - 5 Then
                            'increase the font on all other lines
                            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts1 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts2 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts3 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts4 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts5 = RichTextBox2.SelectionFont.Size
                            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
                            rts6 = RichTextBox2.SelectionFont.Size
                            'decrease the font on the current line
                            RichTextBox2.Select(start + 5, fin)
                            start = start + 5
                            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 2, RichTextBox2.SelectionFont.Style)
                            rts7 = RichTextBox2.SelectionFont.Size
                        End If
                    End If
                    RichTextBox2.Select()
                    RichTextBox2.SelectionStart = start
                End If
            End If

        Catch ex As Exception
            logger.Error("exception in Button17_Click():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to calculate the Dimensions like Height and Width of the text and retrieving the cursor position in the Richtextbox
    Private Sub CalculateDim()
        Try
            Dim g1 As Graphics = RichTextBox1.CreateGraphics
            Dim orgFont1 As New Font(RichTextBox1.Font.Name, RichTextBox1.Font.Size, RichTextBox1.Font.Style)
            Dim sz1 As SizeF = g1.MeasureString(RichTextBox1.Text, orgFont1)
            h1 = sz1.Height
            w1 = sz1.Width
            'get the height and width of the text in the richtextbox
            Dim g2 As Graphics = RichTextBox2.CreateGraphics
            Dim orgFont2 As New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size, RichTextBox2.Font.Style)
            Dim sz2 As SizeF = g2.MeasureString(RichTextBox2.Text, orgFont2)
            h2 = sz2.Height
            w2 = sz2.Width
            Dim g3 As Graphics = RichTextBox3.CreateGraphics
            Dim orgFont3 As New Font(RichTextBox3.Font.Name, RichTextBox3.Font.Size, RichTextBox3.Font.Style)
            Dim sz3 As SizeF = g3.MeasureString(RichTextBox3.Text, orgFont3)
            h3 = sz3.Height
            w3 = sz3.Width
            'get the cursor position in the textbox
            cursorPos1 = RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)
            cursorPos2 = RichTextBox2.GetLineFromCharIndex(RichTextBox2.SelectionStart)
            cursorPos3 = RichTextBox3.GetLineFromCharIndex(RichTextBox3.SelectionStart)
            'logger.Info("font:" + RichTextBox2.Font.Size.ToString)
        Catch ex As Exception
            logger.Error("exception in CalculateDim():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'TextChanged event for richtextbox to decrease the width of the text if exceeds the width of the textbox i.e., Autosizing of text
    Private Sub RichTextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox2.TextChanged
        CalculateDim()
        'HeightRtb()
        Try
            If RichTextBox2.Font.Size > 5 Then
                'reduce the font of the text until its width is less than the width of the textbox
                Do Until w2 < RichTextBox2.Size.Width - 20
                    'to decrease font when deleting text
                    If rtb2flag Then
                        Exit Do
                    End If
                    RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size - 1, RichTextBox2.Font.Style)
                    logger.Info("font:" + RichTextBox2.Font.Size.ToString)
                    CalculateDim()
                    'HeightRtb()
                Loop
            End If
        Catch ex As Exception
            logger.Error("exception in rtb2_tc():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'RichTextBox Click event to display the type of font on the line selected
    Private Sub RichTextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox2.Click
        CalculateDim()
        If RichTextBox2.Text.Length > 0 Then
            fontNo = 0
            Label21.Text = ""
            Dim name As String = ""
            Dim style As String = ""
            'selecting the line conatining the text on which the cursor is placed
            If cursorPos2 = 0 Then
                RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            End If
            If cursorPos2 = 1 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            End If
            If cursorPos2 = 2 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            End If
            If cursorPos2 = 3 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            End If
            If cursorPos2 = 4 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            End If
            If cursorPos2 = 5 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            End If
            If cursorPos2 = 6 Then
                RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            End If
            name = RichTextBox2.SelectionFont.Name
            style = RichTextBox2.SelectionFont.Style
            'change the font type label accordingly
            If name = "Arial Narrow" And style = FontStyle.Regular Then
                Label21.Text = "Compressed"
            ElseIf name = "Arial Narrow" And style = FontStyle.Bold Then
                Label21.Text = "Compressed" + Environment.NewLine + "Double line"
            ElseIf name = "Arial" And style = FontStyle.Regular Then
                Label21.Text = "Spread"
            ElseIf name = "Arial" And style = FontStyle.Bold Then
                Label21.Text = "Spread" + Environment.NewLine + "Double line"
            ElseIf name.Contains("Monotype") Then
                Label21.Text = "Cursive"
            End If
        End If
    End Sub

    'Key up Event for autosizing the text when used Enter and Backspace keys
    Private Sub RichTextBox2_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles RichTextBox2.KeyUp
        CalculateDim()
        Dim lines As Integer = 7
        'limiting lines for tags
        If tagNo = 1 Or tagNo = 2 Or tagNo = 3 Or tagNo = 25 Or tagNo = 28 Or tagNo = 30 Then
            lines = 5
        ElseIf tagNo = 10 Or tagNo = 11 Or tagNo = 12 Or tagNo = 14 Or tagNo = 17 Or tagNo = 21 Then
            lines = 4
        End If
        Try
            'if text is deleted using backspace
            If e.KeyCode = Keys.Back And RichTextBox2.Text.Length > 0 Then
                HeightRtb()
                rtb2flag = True
                logger.Info("in backspace:" + RichTextBox2.Size.Width.ToString + "," + RichTextBox2.Size.Height.ToString)
                logger.Info("maxw:" + maxw2.ToString + ",total:" + total.ToString + ",fontsize:" + RichTextBox2.Font.Size.ToString)
                'increase the font when text is being deleted until width does not exceed the width of the richtextbox 
                Do Until maxw2 > RichTextBox2.Size.Width - 30 Or total > RichTextBox2.Size.Height - (RichTextBox2.Font.Size * 2)
                    RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size + 2, RichTextBox2.Font.Style)
                    CalculateDim()
                    HeightRtb()
                Loop
                'increase the font to maximum when text is deleted completely
                If RichTextBox2.Text.Length = 1 Then
                    RichTextBox2.Font = New Font(RichTextBox2.Font.Name, Convert.ToSingle(108), RichTextBox2.Font.Style)
                End If
                RichTextBox2.SelectionStart = caretpos - 1
            Else
                rtb2flag = False
            End If
            'if return key is used and number of lines is less than the maximum
            If e.KeyCode = Keys.Return And RichTextBox2.Lines.Length <= lines And enterflag Then
                Dim flag As Boolean = False
                If cursorPos2 < RichTextBox2.Lines.Length - 1 Then
                    flag = True
                End If
                logger.Info("text:" + RichTextBox2.Text.Length.ToString)
                saveFont()
                logger.Info("In height loop")
                HeightRtb()
                Dim val As Integer
                val = RichTextBox2.Height - RichTextBox2.Font.Size
                logger.Info("val:" + val.ToString + "rtb height:" + RichTextBox2.Height.ToString)
                Dim temp As Integer = 0
                If tagNo = 4 Or tagNo = 31 Or tagNo = 10 Or tagNo = 7 Then
                    temp = 40
                ElseIf tagNo = 13 Or tagNo = 26 Or tagNo = 5 Then
                    temp = 35
                Else
                    temp = 25
                End If
                'reduce the font when return key is used, to accomodate space for new line
                If total >= val / 2 And RichTextBox2.Lines.Count <= lines Then
                    Do Until total < val - temp
                        'DecreaseFont(2)
                        RichTextBox2.Font = New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size - 2, RichTextBox2.Font.Style)
                        CalculateDim()
                        HeightRtb()
                    Loop
                End If
                If flag Then
                    RichTextBox2.SelectionStart = caretpos + 1
                End If
                enterflag = True
            End If
            'no function if maximum lines are reached
            If e.KeyCode = Keys.Enter And RichTextBox2.Lines.Length = lines Then
                e.Handled = True
            End If
            If e.KeyCode = Keys.Down Then
                Dim line As Int16 = cursorPos2
                HeightRtb()
                While total > RichTextBox2.Height
                    If RichTextBox2.Lines.Count >= 1 Then
                        RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    If RichTextBox2.Lines.Count >= 2 Then
                        RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    If RichTextBox2.Lines.Count >= 3 Then
                        RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    If RichTextBox2.Lines.Count >= 4 Then
                        RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    If RichTextBox2.Lines.Count >= 5 Then
                        RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    If RichTextBox2.Lines.Count >= 6 Then
                        RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    If RichTextBox2.Lines.Count >= 7 Then
                        RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                        RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - 1, RichTextBox2.SelectionFont.Style)
                    End If
                    CalculateDim()
                    HeightRtb()
                End While
                RichTextBox2.SelectionStart = RichTextBox2.GetFirstCharIndexFromLine(line) + RichTextBox2.Lines(line).Length
            End If
        Catch ex As Exception
            logger.Error("exception in rtb2_ku():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Key Down Event to limit the number of lines in the textbox
    Private Sub RichTextBox2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles RichTextBox2.KeyDown
        CalculateDim()
        Try
            Dim lines As Integer = 7
            If My.Computer.Keyboard.CapsLock Then
                Label22.Text = "Caps Lock:" & Environment.NewLine & "ON"
            Else
                Label22.Text = "Caps Lock:" & Environment.NewLine & "OFF"
            End If
            'limiting number of lines on tags
            If tagNo = 1 Or tagNo = 2 Or tagNo = 3 Or tagNo = 25 Or tagNo = 28 Or tagNo = 30 Then
                lines = 5
            ElseIf tagNo = 10 Or tagNo = 11 Or tagNo = 12 Or tagNo = 14 Or tagNo = 17 Or tagNo = 21 Then
                lines = 4
            End If
            'no function if maximum lines are reached
            If e.KeyCode = Keys.Enter And RichTextBox2.Lines.Length = lines Then
                e.Handled = True
                enterflag = False
            End If
            'allow to create new line if number of lines less than maximum
            If e.KeyCode = Keys.Return And RichTextBox2.Lines.Length < lines Then
                e.Handled = False
                caretpos = RichTextBox2.SelectionStart
            End If
            'disable deleting the id during set up of rabies tags
            If Label1.Text.Contains("set up") And Label1.Text.Contains("Rabies/ID") And e.KeyCode = Keys.Back And cursorPos2 = RichTextBox2.Lines.Length - 1 Then
                e.Handled = True
            End If
            If (e.KeyCode And Not Keys.Modifiers) = Keys.Up AndAlso e.Modifiers = Keys.Shift And textFlag Then
                e.Handled = True
            End If
            If (e.KeyCode And Not Keys.Modifiers) = Keys.Down AndAlso e.Modifiers = Keys.Shift And textFlag Then
                e.Handled = True
            End If

            If e.KeyCode = Keys.Back And RichTextBox2.Text.Length > 0 Then
                e.Handled = False
                caretpos = RichTextBox2.SelectionStart
            End If
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then

            End If

        Catch ex As Exception
            logger.Error("exception in rtb2_kd():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to indicate if the Richtextbox2 is focused
    Private Sub RichTextbox2_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox2.GotFocus
        logger.Info("inf rtb2 focus")
        t1Focus = False
        t2Focus = True
        t3Focus = False
    End Sub

    'Method to save the text in a richtextbox and its font type,style and size to a file
    Private Sub WriteText()
        Try
            File.SetAttributes(dir + "\tags\meta\textdata.txt", FileAttributes.Normal)
            Dim objWriter As New System.IO.StreamWriter(dir + "\tags\meta\textdata.txt")
            'retrieving the text in richtextbox
            If RichTextBox1.Text.Length > 0 Then
                For i As Integer = 0 To RichTextBox1.Lines.Count - 1
                    objWriter.WriteLine(RichTextBox1.Lines(i))
                Next
            End If
            If RichTextBox2.Text.Length > 0 Then
                For i As Integer = 0 To RichTextBox2.Lines.Count - 1
                    objWriter.WriteLine(RichTextBox2.Lines(i))
                Next
            End If
            If RichTextBox3.Text.Length > 0 Then
                For i As Integer = 0 To RichTextBox3.Lines.Count - 1
                    objWriter.WriteLine(RichTextBox3.Lines(i))
                Next
            End If
            'objWriter.WriteLine(TextBox5.Text)
            objWriter.Close()
            saveFont()
            objWriter = New System.IO.StreamWriter(dir + "\tags\meta\textdata_meta.txt")
            Dim text As String = ""
            'saving the font type, size and style of the text
            text = fs1.ToString + "," + fs2.ToString + "," + fs3.ToString + "," + fs4.ToString + "," + fs5.ToString + "," + fs6.ToString + "," + fs7.ToString + Environment.NewLine
            text += ft1 + "," + ft2 + "," + ft3 + "," + ft4 + "," + ft5 + "," + ft6 + "," + ft7 + Environment.NewLine
            text += fst1.ToString + "," + fst2.ToString + "," + fst3.ToString + "," + fst4.ToString + "," + fst5.ToString + "," + fst6.ToString + "," + fst7.ToString
            objWriter.WriteLine(text)
            objWriter.Close()
            If RichTextBox1.TextLength > 0 Or RichTextBox2.TextLength > 0 Or RichTextBox3.TextLength > 0 Then
                tFlag = True
            End If
        Catch ex As Exception
            logger.Error("exception in WriteText():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to adjust the position of the RichTextBox so that it is placed inside the area of the tag
    Private Sub AdjustTextBox(ByVal pic As String)
        Try
            logger.Info("Integer adjust textbox:" + pic)
            Dim count As Integer = 0
            Dim flag As Boolean
            Dim line As Integer = 0
            Dim textfile As String
            textfile = dir + "\textmeasures.txt"
            If IO.File.Exists(textfile) Then
                Dim readlines() As String = IO.File.ReadAllLines(textfile)
                While readlines IsNot Nothing And count < readlines.Length
                    'retrieve the text measures of the required tag
                    If pic.Equals(readlines(count)) Or flag Then
                        If Not flag Then
                            count += 1
                            flag = True
                        Else
                            Dim data As String = readlines(count).Substring(readlines(count).LastIndexOf(":") + 1)
                            Dim param() As String = data.Split(",")
                            'set the position of richtextbox inside the tag
                            If line = 0 Then
                                If param(3) < 150 And RichTextBox2.Text.Length = 0 Then
                                    RichTextBox2.Font = New Font(RichTextBox2.Font.Name, Convert.ToSingle(70), RichTextBox2.Font.Style)
                                End If
                                RichTextBox2.Location = New Drawing.Point(Convert.ToInt32(param(0)), Convert.ToInt32(param(1)))
                                RichTextBox2.Size = New Size(Convert.ToInt32(param(2)), Convert.ToInt32(param(3)))
                                p1 = param(3)
                                lines = 3
                            ElseIf line = 1 Then
                                If param(0).Equals("NULL") Then
                                    RichTextBox1.Visible = False
                                    lines += 2
                                Else
                                    RichTextBox1.Location = New Drawing.Point(Convert.ToInt32(param(0)), Convert.ToInt32(param(1)))
                                    RichTextBox1.Size = New Size(Convert.ToInt32(param(2)), Convert.ToInt32(param(3)))
                                    p2 = param(3)
                                End If
                            ElseIf line = 2 Then
                                If param(0).Equals("NULL") Then
                                    RichTextBox3.Visible = False
                                    lines += 2
                                Else
                                    RichTextBox3.Location = New Drawing.Point(Convert.ToInt32(param(0)), Convert.ToInt32(param(1)))
                                    RichTextBox3.Size = New Size(Convert.ToInt32(param(2)), Convert.ToInt32(param(3)))
                                    p3 = param(3)
                                End If
                                Exit While
                            End If
                            count += 1
                            line += 1
                        End If
                    Else
                        count += 1
                    End If
                End While
            End If
        Catch ex As Exception
            logger.Error("exception in AdjustTxtbox():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to display the saved text in the richtextbox and adjusts the font type and size according to the saved data when a new tag is selected
    'in case of Rabies tag, displays the saved text and the id
    Private Sub AdjustRichText(ByVal action As Integer)
        logger.Info("tflag:" + tFlag.ToString + ",rflag:" + rFlag.ToString)
        Dim sflag As Boolean = False
        Dim line As String = ""
        Try
            'if the appication is in text mode
            If tFlag Or rFlag Or engraveFlag Then
                Dim textfile, text, metafile As String
                Dim textdata As String() = {"", "", "", "", "", "", ""}
                Dim count As Integer = 0
                Dim lines As Integer = RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length
                text = ""
                RichTextBox1.Text = ""
                RichTextBox2.Text = ""
                RichTextBox3.Text = ""
                metafile = ""
                Dim linecount As Int16 = 0
                If lines = 0 Then
                    'Exit Sub
                End If
                logger.Info("lines length:" + lines.ToString)
                'get the appropriate file for the type of engraving tag selected
                If rFlag Then
                    textfile = dir + "\tags\meta\rabid_" + tagNo.ToString + ".txt"
                    metafile = dir + "\tags\meta\rabid_" + tagNo.ToString + "_meta.txt"
                ElseIf engraveFlag Then
                    textfile = dir + "\tags\meta\infoid_" + tagNo.ToString + ".txt"
                    metafile = dir + "\tags\meta\infoid_" + tagNo.ToString + "_meta.txt"
                Else
                    textfile = dir + "\tags\meta\textdata.txt"
                    metafile = dir + "\tags\meta\textdata_metafile.txt"
                End If
                'retrieve the text stored in the file selected
                If IO.File.Exists(textfile) And action = 0 Then
                    Dim readlines() As String = IO.File.ReadAllLines(textfile)
                    While readlines IsNot Nothing And count < readlines.Length
                        'logger.Info("in readLines:" + readlines(count))
                        If readlines(count).Length > 0 Then
                            If count = 0 Then
                                textdata(count) = readlines(count)
                                text = readlines(count)
                            Else
                                textdata(count) = readlines(count)
                                text += vbNewLine + readlines(count)
                            End If
                        End If
                        count += 1
                    End While
                End If
                linecount = count
                If rFlag And text.Length >= 0 Then
                    Dim sr = New StreamReader(dir + "\tags\meta\rabidcount.txt")
                    Do While sr.Peek() >= 0
                        line = sr.ReadLine
                        Dim zeros As String = ""
                        'append zeros before the number if it is less than 4
                        If line.Length < 4 Then
                            For l = 1 To 4 - line.Length
                                zeros += "0"
                            Next
                            line = zeros + line
                        End If
                    Loop
                    sr.Close()
                    'if text is empty for rabies tag, an empty line is present before the rabies id
                    If text.Length = 0 Then
                        sflag = True
                        emptyFlag = True
                        text = vbNewLine + line
                    Else
                        text += vbNewLine + line
                    End If
                End If
                Dim lblText As String = ""
                If IO.File.Exists(metafile) Then
                    count = 0
                    Dim readlines() As String = IO.File.ReadAllLines(metafile)
                    'retrieve the font style, size and type for the text stored in the file
                    While readlines IsNot Nothing And count < readlines.Length
                        If readlines(count).Length > 0 Then
                            Dim temp As String() = readlines(count).Split(",")
                            'font sizes on the first line
                            If count = 0 Then
                                fs1 = Convert.ToInt16(temp(0))
                                fs2 = Convert.ToInt16(temp(1))
                                fs3 = Convert.ToInt16(temp(2))
                                fs4 = Convert.ToInt16(temp(3))
                                fs5 = Convert.ToInt16(temp(4))
                                fs6 = Convert.ToInt16(temp(5))
                                fs7 = Convert.ToInt16(temp(6))
                            ElseIf count = 1 Then
                                'font types on the second line
                                ft1 = temp(0)
                                ft2 = temp(1)
                                ft3 = temp(2)
                                ft4 = temp(3)
                                ft5 = temp(4)
                                ft6 = temp(5)
                                ft7 = temp(6)
                                lblText = temp(6)
                            ElseIf count = 2 Then
                                'font style on the third line
                                If temp(0) = "Regular" Then
                                    fst1 = FontStyle.Regular
                                ElseIf temp(0) = "Bold" Then
                                    fst1 = FontStyle.Bold
                                ElseIf temp(0) = "Italic" Then
                                    fst1 = FontStyle.Italic
                                End If
                                If temp(1) = "Regular" Then
                                    fst2 = FontStyle.Regular
                                ElseIf temp(1) = "Bold" Then
                                    fst2 = FontStyle.Bold
                                ElseIf temp(1) = "Italic" Then
                                    fst2 = FontStyle.Italic
                                End If
                                If temp(2) = "Regular" Then
                                    fst3 = FontStyle.Regular
                                ElseIf temp(2) = "Bold" Then
                                    fst3 = FontStyle.Bold
                                ElseIf temp(2) = "Italic" Then
                                    fst3 = FontStyle.Italic
                                End If
                                If temp(3) = "Regular" Then
                                    fst4 = FontStyle.Regular
                                ElseIf temp(3) = "Bold" Then
                                    fst4 = FontStyle.Bold
                                ElseIf temp(3) = "Italic" Then
                                    fst4 = FontStyle.Italic
                                End If
                                If temp(4) = "Regular" Then
                                    fst5 = FontStyle.Regular
                                ElseIf temp(4) = "Bold" Then
                                    fst5 = FontStyle.Bold
                                ElseIf temp(4) = "Italic" Then
                                    fst5 = FontStyle.Italic
                                End If
                                If temp(5) = "Regular" Then
                                    fst6 = FontStyle.Regular
                                ElseIf temp(5) = "Bold" Then
                                    fst6 = FontStyle.Bold
                                ElseIf temp(5) = "Italic" Then
                                    fst6 = FontStyle.Italic
                                End If
                                If temp(6) = "Regular" Then
                                    fst7 = FontStyle.Regular
                                ElseIf temp(6) = "Bold" Then
                                    fst7 = FontStyle.Bold
                                ElseIf temp(6) = "Italic" Then
                                    fst7 = FontStyle.Italic
                                End If
                            End If
                        End If
                        count += 1
                    End While
                End If
                If rFlag Then
                    'setting the fonts to default ones for rabies 
                    If linecount = 1 Then
                        ft2 = "Arial Narrow"
                        fs2 = fs1
                        fst2 = FontStyle.Regular
                        logger.Info("fs1:" + fs1.ToString)
                    ElseIf linecount = 2 Then
                        ft3 = "Arial Narrow"
                        fs3 = fs2
                        fst3 = FontStyle.Regular
                    ElseIf linecount = 3 Then
                        ft4 = "Arial Narrow"
                        fs4 = fs3
                        fst4 = FontStyle.Regular
                    ElseIf linecount = 4 Then
                        ft5 = "Arial Narrow"
                        fs5 = fs4
                        fst5 = FontStyle.Regular
                    ElseIf linecount = 5 Then
                        ft6 = "Arial Narrow"
                        fs6 = fs5
                        fst6 = FontStyle.Regular
                    ElseIf linecount = 6 Then
                        ft6 = "Arial Narrow"
                        fs6 = fs5
                        fst6 = FontStyle.Regular
                    End If
                End If
                logger.Info("text in richtext:" + text)
                logger.Info("ft1:" + ft1 + ",fst1:" + fst1.ToString)
                Label21.Text = "Compressed"
                RichTextBox2.Text = text
                'for tags with 5 lines as limit for rabies tags
                If tagNo = 1 Or tagNo = 2 Or tagNo = 3 Or tagNo = 25 Or tagNo = 28 Or tagNo = 30 Or tagNo = 10 Or tagNo = 11 Or tagNo = 12 Or tagNo = 14 Or tagNo = 17 Or tagNo = 21 Then
                    Dim txt As String = ""
                    tagText = text
                    Dim l As Int16 = 4
                    If rFlag Then
                        rFlag = False
                        l = 3
                    End If
                    'displaying the lines limited for the tag
                    If RichTextBox2.Lines.Length > l + 1 Then
                        For i = 0 To l
                            If i = l Then
                                If l = 3 Then
                                    txt += RichTextBox2.Lines(i) + Environment.NewLine
                                    txt += line
                                Else
                                    txt += RichTextBox2.Lines(i)
                                End If
                            Else
                                txt += RichTextBox2.Lines(i) + Environment.NewLine
                            End If
                        Next
                        logger.Info("text:" + txt)
                        RichTextBox2.Text = ""
                        RichTextBox2.Text = txt
                    End If
                Else
                    RichTextBox2.Text = text
                End If
                'setting the font type, style and size for each line
                If RichTextBox2.Lines.Count >= 1 Then
                    RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
                    If sflag Then
                        ft1 = "Arial Narrow"
                        fs1 = 35
                    End If
                    RichTextBox2.SelectionFont = New Font(ft1, fs1, fst1)
                    'For military tags, cursor is on the left hand side
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If RichTextBox2.Lines.Count >= 2 Then
                    RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
                    If fs2 = 0 Then
                        fs2 = 35
                    End If
                    RichTextBox2.SelectionFont = New Font(ft2, fs2, fst2)
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If RichTextBox2.Lines.Count >= 3 Then
                    RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
                    If fs3 = 0 Then
                        fs3 = 35
                    End If
                    RichTextBox2.SelectionFont = New Font(ft3, fs3, fst3)
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If RichTextBox2.Lines.Count >= 4 Then
                    RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
                    RichTextBox2.SelectionFont = New Font(ft4, fs4, fst4)
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If RichTextBox2.Lines.Count >= 5 Then
                    RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
                    RichTextBox2.SelectionFont = New Font(ft5, fs5, fst5)
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If RichTextBox2.Lines.Count >= 6 Then
                    RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
                    RichTextBox2.SelectionFont = New Font(ft6, fs6, fst6)
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If RichTextBox2.Lines.Count >= 7 Then
                    RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
                    RichTextBox2.SelectionFont = New Font(ft7, fs7, fst7)
                    If tagNo = 22 Or tagNo = 20 Then
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
                    Else
                        RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
                    End If
                End If
                If sflag Then
                    RichTextBox2.SelectionStart = 0
                End If
            End If
        Catch ex As Exception
            logger.Error("exception in AdjustRichText():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to autosize the text once the saved text is placed in the richtextbox
    Private Sub AdjustHeight()
        HeightRtb()
        CalculateDim()
        Try
            Dim lines As Integer = RichTextBox1.Lines.Length + RichTextBox2.Lines.Length + RichTextBox3.Lines.Length
            If lines = 0 Then
                Exit Sub
            End If
            Dim val As Integer
            val = RichTextBox2.Height - RichTextBox2.Font.Size
            logger.Info("val:" + val.ToString)
            'decrease the font if the height of the text is greater than the height of the textbox
            If total >= RichTextBox2.Size.Height / 2 And RichTextBox2.Lines.Count <= lines Then
                logger.Info("in adjust if loop")
                'decrease the font until width and height are less than the textbox
                Do Until total < val - 10 And maxw2 < RichTextBox2.Width - 5
                    logger.Info("in adjust until loop")
                    logger.Info("total:" + total.ToString + " w2:" + w2.ToString)
                    DecreaseFont(1)
                    CalculateDim()
                    HeightRtb()
                Loop
            End If
            RichTextBox2.SelectAll()
            'For military tags, the cursor is on the left hand side
            If tagNo = 22 Or tagNo = 20 Then
                RichTextBox2.SelectionAlignment = HorizontalAlignment.Left
            Else
                RichTextBox2.SelectionAlignment = HorizontalAlignment.Center
            End If
            RichTextBox2.SelectionStart = RichTextBox2.Text.Length + 1
            'if the width of the text is less than the textbox increase the font until it does not exceed the width of the textbox
            If maxw2 < RichTextBox2.Width - 10 And Not RichTextBox2.Text.Length = 0 Then
                Do Until maxw2 > RichTextBox2.Width - 30 Or total > RichTextBox2.Height - RichTextBox2.Font.Size - 30
                    IncreaseFont()
                    CalculateDim()
                    HeightRtb()
                Loop
            End If
            logger.Info("rtb1:" + RichTextBox1.SelectionFont.Size.ToString)
            logger.Info("rtb2:" + RichTextBox2.SelectionFont.Size.ToString)
            logger.Info("rtb3:" + RichTextBox3.SelectionFont.Size.ToString)
            RichTextBox2.SelectionStart = RichTextBox2.Text.Length
            If emptyFlag Then
                RichTextBox2.SelectionStart = 0
                emptyFlag = False
            End If
        Catch ex As Exception
            logger.Error("exception in AdjustHeight():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'Method to decrease the font of a line by 1 point, line number is given as input
    Private Sub DecreaseFont(ByVal x As Int16)
        'for first line
        If RichTextBox2.Lines.Count >= 1 Then
            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts1 = RichTextBox2.SelectionFont.Size
        End If
        'for second line
        If RichTextBox2.Lines.Count >= 2 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts2 = RichTextBox2.SelectionFont.Size
        End If
        'for third line
        If RichTextBox2.Lines.Count >= 3 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts3 = RichTextBox2.SelectionFont.Size
        End If
        'for fourth line
        If RichTextBox2.Lines.Count >= 4 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts4 = RichTextBox2.SelectionFont.Size
        End If
        'for fifth line
        If RichTextBox2.Lines.Count >= 5 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts5 = RichTextBox2.SelectionFont.Size
        End If
        'for sixth line
        If RichTextBox2.Lines.Count >= 6 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts6 = RichTextBox2.SelectionFont.Size
        End If
        'for seventh line
        If RichTextBox2.Lines.Count >= 7 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size - x, RichTextBox2.SelectionFont.Style)
            rts7 = RichTextBox2.SelectionFont.Size
        End If
    End Sub

    'Method to increase the font of the lines by 1 point
    Private Sub IncreaseFont()
        'for first line
        If RichTextBox2.Lines.Count >= 1 Then
            RichTextBox2.Select(0, RichTextBox2.Lines(0).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts1 = RichTextBox2.SelectionFont.Size
        End If
        'for second line
        If RichTextBox2.Lines.Count >= 2 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + 1, RichTextBox2.Lines(1).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts2 = RichTextBox2.SelectionFont.Size
        End If
        'for third line
        If RichTextBox2.Lines.Count >= 3 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + 2, RichTextBox2.Lines(2).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts3 = RichTextBox2.SelectionFont.Size
        End If
        'for fourth line
        If RichTextBox2.Lines.Count >= 4 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + 3, RichTextBox2.Lines(3).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts4 = RichTextBox2.SelectionFont.Size
        End If
        'for fifth line
        If RichTextBox2.Lines.Count >= 5 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + 4, RichTextBox2.Lines(4).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts5 = RichTextBox2.SelectionFont.Size
        End If
        'for sixth line
        If RichTextBox2.Lines.Count >= 6 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + 5, RichTextBox2.Lines(5).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts6 = RichTextBox2.SelectionFont.Size
        End If
        'for seventh line
        If RichTextBox2.Lines.Count >= 7 Then
            RichTextBox2.Select(RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length + 6, RichTextBox2.Lines(6).Length)
            RichTextBox2.SelectionFont = New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size + 1, RichTextBox2.SelectionFont.Style)
            rts7 = RichTextBox2.SelectionFont.Size
        End If
    End Sub

    'Method to calculate the height of the entire text in the richtextbox
    Private Sub HeightRtb()
        Try
            Dim g As Graphics = RichTextBox2.CreateGraphics
            Dim orgFont1 As New Font(RichTextBox2.Font.Name, RichTextBox2.Font.Size, RichTextBox2.Font.Style)
            Dim sz As SizeF
            Dim start, fin As Integer
            rh1 = 0
            rh2 = 0
            rh3 = 0
            rh4 = 0
            rh5 = 0
            rh6 = 0
            rh7 = 0
            Dim rw, mw2 As New ArrayList
            start = 0
            fin = 0
            total = 0
            'calculate the height and width for the first line
            If RichTextBox2.Lines.Count >= 1 Then
                fin = RichTextBox2.Lines(0).Length
                RichTextBox2.Select(start, fin)
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh1 = sz.Height
                If cursorPos2 = 0 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            'calculate the height and width for the second line
            If RichTextBox2.Lines.Count >= 2 Then
                start = RichTextBox2.Lines(0).Length
                fin = RichTextBox2.Lines(1).Length
                'logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                RichTextBox2.Select(start + 1, fin)
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                'logger.Info("selection font size in ht rtb4:" + RichTextBox2.SelectionFont.Size.ToString)
                'Dim g1 As Graphics = RichTextBox2.CreateGraphics
                'RichTextBox2.Select(0, RichTextBox2.Text.Length)
                'Dim orgFont As New Font(RichTextBox2.SelectionFont.Name, RichTextBox2.SelectionFont.Size, RichTextBox2.SelectionFont.Style)
                'Dim sz1 As SizeF = g.MeasureString(RichTextBox2.SelectedText, orgFont)
                'logger.Info("rh2 using my:" + sz1.Height.ToString + "," + RichTextBox2.SelectionFont.Size.ToString)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh2 = sz.Height
                logger.Info("rh2:" + rh2.ToString)
                If cursorPos2 = 1 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            'calculate the height and width for the third line
            If RichTextBox2.Lines.Count >= 3 Then
                start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length
                fin = RichTextBox2.Lines(2).Length
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                RichTextBox2.Select(start + 2, fin)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh3 = sz.Height
                If cursorPos2 = 2 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            'calculate the height and width for the fourth line
            If RichTextBox2.Lines.Count >= 4 Then
                start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length
                fin = RichTextBox2.Lines(3).Length
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                RichTextBox2.Select(start + 3, fin)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh4 = sz.Height
                If cursorPos2 = 3 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            'calculate the height and width for the fifth line
            If RichTextBox2.Lines.Count >= 5 Then
                start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length
                fin = RichTextBox2.Lines(4).Length
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                RichTextBox2.Select(start + 4, fin)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh5 = sz.Height
                If cursorPos2 = 4 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            'calculate the height and width for the sixth line
            If RichTextBox2.Lines.Count >= 6 Then
                start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length
                fin = RichTextBox2.Lines(5).Length
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                RichTextBox2.Select(start + 5, fin)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh6 = sz.Height
                If cursorPos2 = 5 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            'calculate the height and width for the seventh line
            If RichTextBox2.Lines.Count >= 7 Then
                start = RichTextBox2.Lines(0).Length + RichTextBox2.Lines(1).Length + RichTextBox2.Lines(2).Length + RichTextBox2.Lines(3).Length + RichTextBox2.Lines(4).Length + RichTextBox2.Lines(5).Length
                fin = RichTextBox2.Lines(6).Length
                logger.Info("start:" + start.ToString + ",fin:" + fin.ToString)
                RichTextBox2.Select(start + 6, fin)
                f1 = RichTextBox2.SelectionFont.Size
                orgFont1 = New Font(RichTextBox2.SelectionFont.Name, Convert.ToSingle(f1), RichTextBox2.SelectionFont.Style)
                logger.Info("selcted text:" + RichTextBox2.SelectedText.ToString)
                sz = g.MeasureString(RichTextBox2.SelectedText, orgFont1)
                rh7 = sz.Height
                If cursorPos2 = 6 Then
                    currentw = sz.Width
                Else
                    rw.Add(sz.Width)
                End If
                mw2.Add(sz.Width)
            End If
            logger.Info("total len:" + RichTextBox2.Text.Length.ToString)
            'calculating the maximum width among the lines in the textbox
            maxw = MaxCheck(rw)
            maxw2 = MaxCheck(mw2)
            logger.Info("max width:" + maxw.ToString)
            logger.Info("current width:" + currentw.ToString)
            For i = 1 To rw.Count - 1
                logger.Info("rw:" + rw(i).ToString)
            Next
            rw.Clear()
            mw2.Clear()
            logger.Info("h1 and h2 and h3:" + rh1.ToString + "," + rh2.ToString + "," + rh3.ToString)
            logger.Info("h4 and h5 and h6 and h7:" + rh4.ToString + "," + rh5.ToString + "," + rh6.ToString + "," + rh7.ToString)
            'calculating the total height of text from each line
            total = rh1 + rh2 + rh3 + rh4 + rh5 + rh6 + rh7
            logger.Info("total:" + total.ToString)
            RichTextBox2.SelectionStart = RichTextBox2.Text.Length + 1
        Catch ex As Exception
            logger.Error("exception in HeightRtb():" + ex.ToString)
            MessageBox.Show("An unexpected error occured.", "Error")
            Environment.Exit(0)
        End Try
    End Sub

    'PictrueBox Click events to load the enlarged image into a picturebox
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        EnlargeImage(PictureBox2.ImageLocation)
    End Sub
    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click
        EnlargeImage(PictureBox12.ImageLocation)
    End Sub
    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        EnlargeImage(PictureBox9.ImageLocation)
    End Sub

    Private Sub PictureBox15_Click(sender As Object, e As EventArgs) Handles PictureBox15.Click
        EnlargeImage(PictureBox15.ImageLocation)
    End Sub
    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
        EnlargeImage(PictureBox11.ImageLocation)
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
        EnlargeImage(PictureBox10.ImageLocation)
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        EnlargeImage(PictureBox8.ImageLocation)
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        EnlargeImage(PictureBox7.ImageLocation)
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        EnlargeImage(PictureBox6.ImageLocation)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        EnlargeImage(PictureBox3.ImageLocation)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        EnlargeImage(PictureBox5.ImageLocation)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        EnlargeImage(PictureBox4.ImageLocation)
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click
        EnlargeImage(PictureBox13.ImageLocation)
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        EnlargeImage(PictureBox14.ImageLocation)
    End Sub

    Private Sub PictureBox16_Click(sender As Object, e As EventArgs) Handles PictureBox16.Click
        EnlargeImage(PictureBox16.ImageLocation)
    End Sub

    Private Sub PictureBox17_Click(sender As Object, e As EventArgs) Handles PictureBox17.Click
        EnlargeImage(PictureBox17.ImageLocation)
    End Sub

    Private Sub PictureBox18_Click(sender As Object, e As EventArgs) Handles PictureBox18.Click
        EnlargeImage(PictureBox18.ImageLocation)
    End Sub

    Private Sub PictureBox19_Click(sender As Object, e As EventArgs) Handles PictureBox19.Click
        EnlargeImage(PictureBox19.ImageLocation)
    End Sub

    Private Sub PictureBox20_Click(sender As Object, e As EventArgs) Handles PictureBox20.Click
        EnlargeImage(PictureBox20.ImageLocation)
    End Sub

    Private Sub PictureBox21_Click(sender As Object, e As EventArgs) Handles PictureBox21.Click
        EnlargeImage(PictureBox21.ImageLocation)
    End Sub

    Private Sub PictureBox22_Click(sender As Object, e As EventArgs) Handles PictureBox22.Click
        EnlargeImage(PictureBox22.ImageLocation)
    End Sub

    Private Sub PictureBox23_Click(sender As Object, e As EventArgs) Handles PictureBox23.Click
        EnlargeImage(PictureBox23.ImageLocation)
    End Sub

    Private Sub PictureBox24_Click(sender As Object, e As EventArgs) Handles PictureBox24.Click
        EnlargeImage(PictureBox24.ImageLocation)
    End Sub

    Private Sub PictureBox26_Click(sender As Object, e As EventArgs) Handles PictureBox26.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox26.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox27_Click(sender As Object, e As EventArgs) Handles PictureBox27.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox27.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox28_Click(sender As Object, e As EventArgs) Handles PictureBox28.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox28.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox29_Click(sender As Object, e As EventArgs) Handles PictureBox29.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox29.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox30_Click(sender As Object, e As EventArgs) Handles PictureBox30.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox30.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox31_Click(sender As Object, e As EventArgs) Handles PictureBox31.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox31.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox32_Click(sender As Object, e As EventArgs) Handles PictureBox32.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox32.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox33_Click(sender As Object, e As EventArgs) Handles PictureBox33.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox33.ImageLocation)
        End If
    End Sub

    Private Sub PictureBox34_Click(sender As Object, e As EventArgs) Handles PictureBox34.Click
        If Panel1.Visible Or Label1.Text.Contains("Rabies/ID") Then
            EnlargeImage(PictureBox34.ImageLocation)
        End If
    End Sub
    Private Sub PictureBox43_Click(sender As Object, e As EventArgs) Handles PictureBox43.Click
        EnlargeImage(PictureBox43.ImageLocation)
    End Sub
    Private Sub PictureBox35_Click(sender As Object, e As EventArgs) Handles PictureBox35.Click
        EnlargeImage(PictureBox35.ImageLocation)
    End Sub
    Private Sub PictureBox36_Click(sender As Object, e As EventArgs) Handles PictureBox36.Click
        EnlargeImage(PictureBox36.ImageLocation)
    End Sub

End Class

