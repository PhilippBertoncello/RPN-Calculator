<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class input
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ButEnter = New System.Windows.Forms.Button()
        Me.inputBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ButEnter
        '
        Me.ButEnter.Location = New System.Drawing.Point(37, 38)
        Me.ButEnter.Name = "ButEnter"
        Me.ButEnter.Size = New System.Drawing.Size(75, 23)
        Me.ButEnter.TabIndex = 3
        Me.ButEnter.Text = "Enter"
        Me.ButEnter.UseVisualStyleBackColor = True
        '
        'inputBox
        '
        Me.inputBox.Location = New System.Drawing.Point(12, 12)
        Me.inputBox.Name = "inputBox"
        Me.inputBox.Size = New System.Drawing.Size(100, 20)
        Me.inputBox.TabIndex = 2
        '
        'input
        '
        Me.AcceptButton = Me.ButEnter
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(121, 70)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButEnter)
        Me.Controls.Add(Me.inputBox)
        Me.Name = "input"
        Me.Text = "input"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButEnter As Button
    Friend WithEvents inputBox As TextBox
End Class
