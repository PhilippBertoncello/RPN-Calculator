Public Class BaseSwitcherForm
    Private Sub B2_Click(sender As Object, e As EventArgs) Handles B2.Click
        Form1.BASE = 2
        Close()
    End Sub

    Private Sub B5_Click(sender As Object, e As EventArgs) Handles B5.Click
        Form1.BASE = 5
        Close()
    End Sub

    Private Sub B8_Click(sender As Object, e As EventArgs) Handles B8.Click
        Form1.BASE = 8
        Close()
    End Sub

    Private Sub B10_Click(sender As Object, e As EventArgs) Handles B10.Click
        Form1.BASE = 10
        Close()
    End Sub

    Private Sub B16_Click(sender As Object, e As EventArgs) Handles B16.Click
        Form1.BASE = 16
        Close()
    End Sub

    Private Sub BC_Click(sender As Object, e As EventArgs) Handles BC.Click
        Dim customBase As CustomBaseForm = CustomBaseForm
        customBase.ShowDialog()
        Close()
    End Sub
End Class