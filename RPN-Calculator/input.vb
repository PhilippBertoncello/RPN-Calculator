Public Class input

    Private Sub input_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        inputBox.Text = Form1.INPUTSTRING
        inputBox.Select(1, 0)
    End Sub

    Private Sub ButEnter_Click(sender As Object, e As EventArgs) Handles ButEnter.Click
        Form1.INPUTSTRING = inputBox.Text
        Close()
    End Sub

End Class