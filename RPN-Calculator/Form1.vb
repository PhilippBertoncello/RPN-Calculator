Public Class Form1

#Region "Variables"
    Dim bufferSize As Integer = 19
    Dim numbers(bufferSize) As Double
    Dim decimalPosition As Integer = 0
    Dim editing As Boolean = True

    Public Shared BASE As UInteger = 10
    Public Shared PRECISION As Integer = 10
    Public Shared FLOPLEN As Integer = 6 'how many decimals after the floating point
    Public Shared FLOPLENOUT As Integer = 0 'how many decimals after the floating point are visible; 0 for not fixed
    Public Shared FLOPCHR As Char = "." 'which character is being used as a floating point
    Public Shared DRGMODE As Integer = 0

    Dim Drgmodes() As Integer = {1, 180 / Math.PI, 200 / Math.PI} ' 0 = RADIANS; 1 = DEGREES; 2 = GRAD

    Public Shared INPUTINT As Integer
    Public Shared INPUTSTRING As String

    Public Shared inputStrings() As String = {
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "sin",
        "cos",
        "tan",
        "sec",
        "arcsin",
        "arccos",
        "arctan",
        "arcsec",
        "^",
        "sqrt",
        "root",
        "ln",
        "log",
        "base",
        "pi",
        "eul"
    }
#End Region

#Region "etc"

    Private Sub Init() Handles MyBase.Load
        ClearBuffers()
        BaseSwitch.Text = "BASE" + Str(BASE)
        UpdateDRGButton()
    End Sub

    Private Sub ClearBuffers()
        For i = 0 To bufferSize
            numbers(i) = 0.0F
        Next
        RefreshBuffers()
        ExitDecimal()
    End Sub

    Private Sub W_Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles W_Textbox.KeyPress
        If e.KeyChar <> "" Then
            e.Handled = True
        End If
    End Sub

    Private Sub Z_Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Z_Textbox.KeyPress
        If e.KeyChar <> "" Then
            e.Handled = True
        End If
    End Sub

    Private Sub Y_Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Y_Textbox.KeyPress
        If e.KeyChar <> "" Then
            e.Handled = True
        End If
    End Sub

    Private Sub X_Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles X_Textbox.KeyPress
        'If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
        If True Then
            e.Handled = True
        End If
    End Sub

    Private Sub RefreshBuffers()
        W_Textbox.Text = StringFromNum(numbers(3), BASE, PRECISION, FLOPLENOUT, False)
        Z_Textbox.Text = StringFromNum(numbers(2), BASE, PRECISION, FLOPLENOUT, False)
        Y_Textbox.Text = StringFromNum(numbers(1), BASE, PRECISION, FLOPLENOUT, False)
        X_Textbox.Text = StringFromNum(numbers(0), BASE, PRECISION, FLOPLENOUT, editing)

        X_Textbox.Focus()
        X_Textbox.Select(X_Textbox.TextLength, 0)
    End Sub

    Private Function StringFromNum(n As Double, b As Integer, prec As Integer, decLen As Integer, edit As Boolean) As String
        Dim length As Integer = 0
        Dim sign As Boolean = False
        Dim nums(prec) As Integer
        Dim result As Double = 0
        Dim duration As Integer = 0
        Dim charArray(prec) As Char
        Dim curString As String = ""

        'write the sign and remove it before the rest of the process
        If n < 0 Then
            curString = "-"
            sign = True
            n = n * -1
        End If

        'set all nums to zero
        For i = 0 To prec - 1
            nums(i) = 0
        Next

        'get number of digits before the comma
        For i = 1 To prec
            If b ^ i > n Then
                length = i - 1
                Exit For
            End If
        Next

        'get nth digit before the comma
        For pos = length To 0 Step -1
            For i = 1 To b
                If result + (i * (b ^ pos)) > n Then
                    duration = i - 1
                    Exit For
                End If
            Next
            nums(pos) = duration
            result += duration * (b ^ pos)
        Next

        'write numbers to string
        For i = 0 To prec
            curString = Hex(nums(i)) + curString
        Next

        'make a char array and clear the string
        charArray = curString.ToCharArray
        curString = ""

        'search for zeroes and remove them
        For i = 0 To prec
            If charArray(i) <> "0" Then
                duration = i
                Exit For
            End If
        Next

        'only write numbers that aren't the zeroes in front of the number
        For i = duration To prec Step 1
            curString = curString + charArray(i)
        Next

        'only write numbers until the cursor in edit mode
        If edit And Not decimalPosition = 0 Then
            decLen = decimalPosition - 1
        End If

        'add the floating point character
        curString = curString + FLOPCHR

        'write decimals only until the cursor if the decLen != 0
        If (Not decimalPosition = 0 Or Not edit) And Not decLen = 0 Then
            For pos = 1 To decLen Step 1
                For i = 1 To b
                    If n < result + (i / (b ^ pos)) Then
                        curString = curString + Trim(Str(i - 1))
                        result = result + ((i - 1) / (b ^ pos))
                        Exit For
                    End If
                Next
            Next
        End If

        'write decimals if decLen == 0
        If (Not decimalPosition = 0 Or Not edit) And decLen = 0 Then
            For pos = 1 To FLOPLEN Step 1
                For i = 1 To b
                    If n < result + (i / (b ^ pos)) Then
                        curString = curString + Trim(Str(i - 1))
                        result = result + ((i - 1) / (b ^ pos))
                        Exit For
                    End If
                Next
            Next
        End If

        'trim all zeroes if FIX == False
        If FLOPLENOUT = 0 Then
            curString = removeZeroesAfterDP(curString, edit)
            curString = removeZeroesBeforeDP(curString, edit)
        End If

        'return working string
        StringFromNum = curString
    End Function

    Private Function removeZeroesAfterDP(input As String, edit As Boolean) As String
        Dim out As String = input

        For i = input.Length - 1 To 0 Step -1
            If Not input.Chars(i) = "0" And Not input.Chars(i) = FLOPCHR Then
                Exit For
            End If

            out = out.Remove(i)
        Next

        If out = "" Then
            out = "0"
        End If

        If edit = True And decimalPosition = 1 Then
            out = out + FLOPCHR
        End If

        removeZeroesAfterDP = out
    End Function

    Private Function removeZeroesBeforeDP(input As String, edit As Boolean) As String
        Dim out As String = input

        For i = 0 To input.Length - 1 Step 1
            If input.Chars(i) = FLOPCHR Then
                out = "0" + out
                Exit For
            End If

            If Not input.Chars(i) = "0" Then
                Exit For
            End If

            out = out.Remove(0, 1)
        Next

        If out = "" Then
            out = "0"
        End If



        removeZeroesBeforeDP = out
    End Function

    Private Sub KeyboardInput(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles X_Textbox.KeyPress
        Select Case e.KeyChar
            Case "+"
                Add.PerformClick()
            Case "-"
                Subtract.PerformClick()
            Case "*"
                Multiply.PerformClick()
            Case "/"
                Divide.PerformClick()
            Case "0"
                But0.PerformClick()
            Case "1"
                But1.PerformClick()
            Case "2"
                But2.PerformClick()
            Case "3"
                But3.PerformClick()
            Case "4"
                But4.PerformClick()
            Case "5"
                But5.PerformClick()
            Case "6"
                But6.PerformClick()
            Case "7"
                But7.PerformClick()
            Case "8"
                But8.PerformClick()
            Case "9"
                But9.PerformClick()
                'Case "A", "a"
                '    ButA.PerformClick()
                'Case "B", "b"
                '    ButB.PerformClick()
                'Case "C", "c"
                '    ButC.PerformClick()
                'Case "D", "d"
                '    ButD.PerformClick()
                'Case "E", "e"
                '    ButE.PerformClick()
                'Case "F", "f"
                'ButF.PerformClick()
            Case "."
                Point.PerformClick()
            Case ","
                Point.PerformClick()
            Case Microsoft.VisualBasic.ChrW(Keys.Return)
                EnterBuf.PerformClick()
            Case Microsoft.VisualBasic.ChrW(Keys.Back)
                ButDEL.PerformClick()
            Case Microsoft.VisualBasic.ChrW(Keys.PageDown)
                Drop.PerformClick()
            Case Microsoft.VisualBasic.ChrW(Keys.PageUp)
                SwapXY.PerformClick()
            Case Else
                OpenInputMenu(e.KeyChar)
        End Select
    End Sub

    Private Sub OpenInputMenu(inp As Char)
        Dim inputWindow As input = New input()
        INPUTSTRING = inp
        inputWindow.ShowDialog()

        Select Case INPUTSTRING
            Case inputStrings(0)
                ButA.PerformClick()
            Case inputStrings(1)
                ButB.PerformClick()
            Case inputStrings(2)
                ButC.PerformClick()
            Case inputStrings(3)
                ButD.PerformClick()
            Case inputStrings(4)
                ButE.PerformClick()
            Case inputStrings(5)
                ButF.PerformClick()
            Case inputStrings(6)
                sin.PerformClick()
            Case inputStrings(7)
                cos.PerformClick()
            Case inputStrings(8)
                tan.PerformClick()
            Case inputStrings(9)
                sec.PerformClick()
            Case inputStrings(10)
                arcsin.PerformClick()
            Case inputStrings(11)
                arccos.PerformClick()
            Case inputStrings(12)
                arctan.PerformClick()
            Case inputStrings(13)
                arcsec.PerformClick()
            Case inputStrings(14)
                Power.PerformClick()
            Case inputStrings(15)
                Sqrt.PerformClick()
            Case inputStrings(16)
                NthRoot.PerformClick()
            Case inputStrings(17)
                NaturalLog.PerformClick()
            Case inputStrings(18)
                Base10Log.PerformClick()
            Case inputStrings(19)
                BaseSwitch.PerformClick()
            Case inputStrings(20)
                ButPI.PerformClick()
            Case inputStrings(21)
                ButNatNum.PerformClick()
        End Select
    End Sub

    Private Sub WriteNumber(number As Integer)
        If decimalPosition = 0 Then
            numbers(0) = (numbers(0) * BASE) + number
        Else
            numbers(0) = numbers(0) + (number / (BASE ^ decimalPosition))
            decimalPosition += 1
        End If
        editing = True
        RefreshBuffers()
    End Sub

    Private Sub Point_Click(sender As Object, e As EventArgs) Handles Point.Click
        EnterDecimal()
        RefreshBuffers()
    End Sub

    Private Sub EnterDecimal()
        If decimalPosition = 0 Then
            decimalPosition = 1
        End If
        editing = True
    End Sub

    Private Sub ExitDecimal()
        decimalPosition = 0
    End Sub

    Private Sub ButtonEnableAfterBaseChange()
        But0.Enabled = False
        But1.Enabled = False
        But2.Enabled = False
        But3.Enabled = False
        But4.Enabled = False
        But5.Enabled = False
        But6.Enabled = False
        But7.Enabled = False
        But8.Enabled = False
        But9.Enabled = False
        ButA.Enabled = False
        ButB.Enabled = False
        ButC.Enabled = False
        ButD.Enabled = False
        ButE.Enabled = False
        ButF.Enabled = False

        If BASE >= 1 Then But0.Enabled = True
        If BASE >= 2 Then But1.Enabled = True
        If BASE >= 3 Then But2.Enabled = True
        If BASE >= 4 Then But3.Enabled = True
        If BASE >= 5 Then But4.Enabled = True
        If BASE >= 6 Then But5.Enabled = True
        If BASE >= 7 Then But6.Enabled = True
        If BASE >= 8 Then But7.Enabled = True
        If BASE >= 9 Then But8.Enabled = True
        If BASE >= 10 Then But9.Enabled = True
        If BASE >= 11 Then ButA.Enabled = True
        If BASE >= 12 Then ButB.Enabled = True
        If BASE >= 13 Then ButC.Enabled = True
        If BASE >= 14 Then ButD.Enabled = True
        If BASE >= 15 Then ButE.Enabled = True
        If BASE >= 16 Then ButF.Enabled = True
    End Sub

#End Region

#Region "On Buffer Operation Click"

    Private Sub DropBuffers(index As Integer)
        For i = index To bufferSize - 1
            numbers(i) = numbers(i + 1)
        Next
        numbers(bufferSize) = 0
        editing = False
        RefreshBuffers()
    End Sub

    Private Sub EnterBuffer(index As Integer)
        For i = bufferSize To index + 1 Step -1
            numbers(i) = numbers(i - 1)
        Next
        numbers(index) = 0
        editing = True
        RefreshBuffers()
    End Sub

    Private Sub OnSwapXY() Handles SwapXY.Click
        Dim save As Decimal
        save = numbers(0)
        numbers(0) = numbers(1)
        numbers(1) = save
        editing = False
        RefreshBuffers()
        ExitDecimal()
    End Sub

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        ClearBuffers()
        ExitDecimal()
    End Sub

    Private Sub Drop_Click(sender As Object, e As EventArgs) Handles Drop.Click
        DropBuffers(0)
        ExitDecimal()
    End Sub

    Private Sub Enter_Click(sender As Object, e As EventArgs) Handles EnterBuf.Click
        EnterBuffer(0)
        ExitDecimal()
    End Sub

    Private Sub ButDEL_Click(sender As Object, e As EventArgs) Handles ButDEL.Click
        If decimalPosition = 0 Then
            numbers(0) = Math.Floor(numbers(0) / BASE)
        Else
            decimalPosition -= 1
            numbers(0) = Math.Floor(numbers(0) * (BASE ^ (decimalPosition - 1))) / (BASE ^ (decimalPosition - 1))
        End If

        RefreshBuffers()
    End Sub

#End Region

#Region "On Number Click"

    Private Sub But0_Click(sender As Object, e As EventArgs) Handles But0.Click
        WriteNumber(0)
    End Sub

    Private Sub But1_Click(sender As Object, e As EventArgs) Handles But1.Click
        WriteNumber(1)
    End Sub

    Private Sub But2_Click(sender As Object, e As EventArgs) Handles But2.Click
        WriteNumber(2)
    End Sub

    Private Sub But3_Click(sender As Object, e As EventArgs) Handles But3.Click
        WriteNumber(3)
    End Sub

    Private Sub But4_Click(sender As Object, e As EventArgs) Handles But4.Click
        WriteNumber(4)
    End Sub

    Private Sub But5_Click(sender As Object, e As EventArgs) Handles But5.Click
        WriteNumber(5)
    End Sub

    Private Sub But6_Click(sender As Object, e As EventArgs) Handles But6.Click
        WriteNumber(6)
    End Sub

    Private Sub But7_Click(sender As Object, e As EventArgs) Handles But7.Click
        WriteNumber(7)
    End Sub

    Private Sub But8_Click(sender As Object, e As EventArgs) Handles But8.Click
        WriteNumber(8)
    End Sub

    Private Sub But9_Click(sender As Object, e As EventArgs) Handles But9.Click
        WriteNumber(9)
    End Sub

    Private Sub ButA_Click(sender As Object, e As EventArgs) Handles ButA.Click
        WriteNumber(10)
    End Sub

    Private Sub ButB_Click(sender As Object, e As EventArgs) Handles ButB.Click
        WriteNumber(11)
    End Sub

    Private Sub ButC_Click(sender As Object, e As EventArgs) Handles ButC.Click
        WriteNumber(12)
    End Sub

    Private Sub ButD_Click(sender As Object, e As EventArgs) Handles ButD.Click
        WriteNumber(13)
    End Sub

    Private Sub ButE_Click(sender As Object, e As EventArgs) Handles ButE.Click
        WriteNumber(14)
    End Sub

    Private Sub ButF_Click(sender As Object, e As EventArgs) Handles ButF.Click
        WriteNumber(15)
    End Sub

#End Region

#Region "On Constant Click"

    Private Sub ButPI_Click(sender As Object, e As EventArgs) Handles ButPI.Click
        numbers(0) = Math.PI
        editing = False
        RefreshBuffers()
    End Sub

    Private Sub ButNatNum_Click(sender As Object, e As EventArgs) Handles ButNatNum.Click
        numbers(0) = Math.E
        editing = False
        RefreshBuffers()
    End Sub

#End Region

#Region "On Function Click"

    Private Sub Addition_Click(sender As Object, e As EventArgs) Handles Add.Click
        numbers(0) = numbers(1) + numbers(0)
        DropBuffers(1)
        RefreshBuffers()
    End Sub

    Private Sub Subtract_Click(sender As Object, e As EventArgs) Handles Subtract.Click
        numbers(0) = numbers(1) - numbers(0)
        DropBuffers(1)
        RefreshBuffers()
    End Sub

    Private Sub Multiply_Click(sender As Object, e As EventArgs) Handles Multiply.Click
        numbers(0) = numbers(1) * numbers(0)
        DropBuffers(1)
        RefreshBuffers()
    End Sub

    Private Sub Divide_Click(sender As Object, e As EventArgs) Handles Divide.Click
        Try
            numbers(0) = numbers(1) / numbers(0)
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try
        DropBuffers(1)
        RefreshBuffers()
    End Sub

    Private Sub Exponential_Click(sender As Object, e As EventArgs) Handles Exponential.Click
        numbers(0) = Math.E ^ numbers(0)
        editing = False
        RefreshBuffers()
    End Sub

    Private Sub NaturalLog_Click(sender As Object, e As EventArgs) Handles NaturalLog.Click
        Try
            numbers(0) = Math.Log(numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub Base10Log_Click(sender As Object, e As EventArgs) Handles Base10Log.Click
        Try
            numbers(0) = Math.Log10(numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub Power2_Click(sender As Object, e As EventArgs) Handles Power2.Click
        numbers(0) = numbers(0) ^ 2
        editing = False
        RefreshBuffers()
    End Sub

    Private Sub Power_Click(sender As Object, e As EventArgs) Handles Power.Click
        Try
            numbers(0) = numbers(1) ^ numbers(0)
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        DropBuffers(1)
        RefreshBuffers()
    End Sub

    Private Sub Sqrt_Click(sender As Object, e As EventArgs) Handles Sqrt.Click
        Try
            numbers(0) = Math.Sqrt(numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub NthRoot_Click(sender As Object, e As EventArgs) Handles NthRoot.Click
        Try
            numbers(0) = Math.Pow(numbers(1), 1 / numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        DropBuffers(1)
        RefreshBuffers()
    End Sub

    Private Sub sin_Click(sender As Object, e As EventArgs) Handles sin.Click
        Try
            numbers(0) = Math.Sin(numbers(0) / Drgmodes(DRGMODE))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub cos_Click(sender As Object, e As EventArgs) Handles cos.Click
        Try
            numbers(0) = Math.Cos(numbers(0) / Drgmodes(DRGMODE))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub tan_Click(sender As Object, e As EventArgs) Handles tan.Click
        Try
            numbers(0) = Math.Tan(numbers(0) / Drgmodes(DRGMODE))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub sec_Click(sender As Object, e As EventArgs) Handles sec.Click
        Try
            numbers(0) = 1 / Math.Cos(numbers(0) / Drgmodes(DRGMODE))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub arcsin_Click(sender As Object, e As EventArgs) Handles arcsin.Click
        Try
            numbers(0) = Drgmodes(DRGMODE) * Math.Asin(numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub arccos_Click(sender As Object, e As EventArgs) Handles arccos.Click
        Try
            numbers(0) = Drgmodes(DRGMODE) * Math.Acos(numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub arctan_Click(sender As Object, e As EventArgs) Handles arctan.Click
        Try
            numbers(0) = Drgmodes(DRGMODE) * Math.Atan(numbers(0))
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

    Private Sub arcsec_Click(sender As Object, e As EventArgs) Handles arcsec.Click
        Try
            numbers(0) = 2 * Math.Atan(1) - Math.Atan(Math.Sign(numbers(0) / Math.Sqrt(numbers(0) * numbers(0) - 1)))
            numbers(0) = numbers(0) * Drgmodes(DRGMODE)
        Catch
            Dim dialog As invalidOperation = invalidOperation
            dialog.ShowDialog()
        End Try

        editing = False
        RefreshBuffers()
    End Sub

#End Region

#Region "On Toggle Button Click"

    Private Sub BaseSwitch_Click(sender As Object, e As EventArgs) Handles BaseSwitch.Click
        Dim Dialogue As New BaseSwitcherForm
        Dialogue.ShowDialog()
        BaseSwitch.Text = "BASE" + Str(BASE)
        ButtonEnableAfterBaseChange()
    End Sub

    Private Sub DRG_Click(sender As Object, e As EventArgs) Handles DRG.Click
        Select Case DRGMODE
            Case 0
                DRGMODE = 1
                Exit Select
            Case 1
                DRGMODE = 2
                Exit Select
            Case 2
                DRGMODE = 0
                Exit Select
        End Select
        UpdateDRGButton()
    End Sub

    Private Sub UpdateDRGButton()
        Select Case DRGMODE
            Case 0
                DRG.Text = "RAD"
            Case 1
                DRG.Text = "DEG"
            Case 2
                DRG.Text = "GRAD"
        End Select
    End Sub

#End Region

#Region "Toolstrip Selection"

    Private Sub BeforeFloatingPointToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfterCommaToolStripMenuItem.Click
        Dim dialog As enterNumber = enterNumber
        dialog.ShowDialog()
        PRECISION = INPUTINT
    End Sub

    Private Sub FIXEDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeforeCommaToolStripMenuItem.Click
        Dim dialog As enterNumber = enterNumber
        dialog.ShowDialog()
        FLOPLENOUT = INPUTINT
        RefreshBuffers()
    End Sub

    Private Sub BASEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BASEToolStripMenuItem.Click
        Dim Dialogue As New BaseSwitcherForm
        Dialogue.ShowDialog()
        BaseSwitch.Text = "BASE" + Str(BASE)
        ButtonEnableAfterBaseChange()
        RefreshBuffers()
    End Sub

    Private Sub RadiansToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RadiansToolStripMenuItem.Click
        DRGMODE = 0
        UpdateDRGButton()
    End Sub

    Private Sub DegreesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DegreesToolStripMenuItem.Click
        DRGMODE = 1
        UpdateDRGButton()
    End Sub

    Private Sub GradToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GradToolStripMenuItem.Click
        DRGMODE = 2
        UpdateDRGButton()
    End Sub

#End Region

End Class
