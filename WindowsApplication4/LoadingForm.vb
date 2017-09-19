Public Class LoadingForm
    Private Sub LoadingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Load("C:\Users\kmgrepos\Downloads\145048-bigthumbnail.jpg")
        Label1.Text = "Loading..."
    End Sub

End Class