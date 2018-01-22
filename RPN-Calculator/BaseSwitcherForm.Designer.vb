<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BaseSwitcherForm
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
        Me.B2 = New System.Windows.Forms.Button()
        Me.B5 = New System.Windows.Forms.Button()
        Me.B10 = New System.Windows.Forms.Button()
        Me.B8 = New System.Windows.Forms.Button()
        Me.BC = New System.Windows.Forms.Button()
        Me.B16 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'B2
        '
        Me.B2.Location = New System.Drawing.Point(12, 12)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(75, 23)
        Me.B2.TabIndex = 0
        Me.B2.Text = "2"
        Me.B2.UseVisualStyleBackColor = True
        '
        'B5
        '
        Me.B5.Location = New System.Drawing.Point(93, 12)
        Me.B5.Name = "B5"
        Me.B5.Size = New System.Drawing.Size(75, 23)
        Me.B5.TabIndex = 1
        Me.B5.Text = "5"
        Me.B5.UseVisualStyleBackColor = True
        '
        'B10
        '
        Me.B10.Location = New System.Drawing.Point(93, 41)
        Me.B10.Name = "B10"
        Me.B10.Size = New System.Drawing.Size(75, 23)
        Me.B10.TabIndex = 3
        Me.B10.Text = "10"
        Me.B10.UseVisualStyleBackColor = True
        '
        'B8
        '
        Me.B8.Location = New System.Drawing.Point(12, 41)
        Me.B8.Name = "B8"
        Me.B8.Size = New System.Drawing.Size(75, 23)
        Me.B8.TabIndex = 2
        Me.B8.Text = "8"
        Me.B8.UseVisualStyleBackColor = True
        '
        'BC
        '
        Me.BC.Location = New System.Drawing.Point(93, 70)
        Me.BC.Name = "BC"
        Me.BC.Size = New System.Drawing.Size(75, 23)
        Me.BC.TabIndex = 5
        Me.BC.Text = "Custom..."
        Me.BC.UseVisualStyleBackColor = True
        '
        'B16
        '
        Me.B16.Location = New System.Drawing.Point(12, 70)
        Me.B16.Name = "B16"
        Me.B16.Size = New System.Drawing.Size(75, 23)
        Me.B16.TabIndex = 4
        Me.B16.Text = "16"
        Me.B16.UseVisualStyleBackColor = True
        '
        'BaseSwitcherForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(177, 104)
        Me.Controls.Add(Me.BC)
        Me.Controls.Add(Me.B16)
        Me.Controls.Add(Me.B10)
        Me.Controls.Add(Me.B8)
        Me.Controls.Add(Me.B5)
        Me.Controls.Add(Me.B2)
        Me.Name = "BaseSwitcherForm"
        Me.Text = "BaseSwitcherForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents B2 As Button
    Friend WithEvents B5 As Button
    Friend WithEvents B10 As Button
    Friend WithEvents B8 As Button
    Friend WithEvents BC As Button
    Friend WithEvents B16 As Button
End Class
