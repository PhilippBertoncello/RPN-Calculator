Public Class CustomBaseForm

    Private Sub ButEnter_Click(sender As Object, e As EventArgs) Handles ButEnter.Click
        Form1.INPUTINT = InputBox.Text.ToString()
        Close()
    End Sub

    Private Sub inputBox_TextChanged(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles InputBox.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class