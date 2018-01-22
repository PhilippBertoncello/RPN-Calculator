<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class enterNumber
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
        Me.inputBox = New System.Windows.Forms.TextBox()
        Me.ButEnter = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'inputBox
        '
        Me.inputBox.Location = New System.Drawing.Point(13, 13)
        Me.inputBox.Name = "inputBox"
        Me.inputBox.Size = New System.Drawing.Size(100, 20)
        Me.inputBox.TabIndex = 0
        '
        'ButEnter
        '
        Me.ButEnter.Location = New System.Drawing.Point(38, 39)
        Me.ButEnter.Name = "ButEnter"
        Me.ButEnter.Size = New System.Drawing.Size(75, 23)
        Me.ButEnter.TabIndex = 1
        Me.ButEnter.Text = "Enter"
        Me.ButEnter.UseVisualStyleBackColor = True
        '
        'enterNumber
        '
        Me.AcceptButton = Me.ButEnter
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(120, 72)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButEnter)
        Me.Controls.Add(Me.inputBox)
        Me.Name = "enterNumber"
        Me.Text = "Enter a number..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents inputBox As TextBox
    Friend WithEvents ButEnter As Button
End Class
