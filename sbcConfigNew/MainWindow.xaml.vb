Imports System.Console
Imports System.Net
Class MainWindow
    Public platform As String = ""
    Public desired As Integer = 0
    Public list() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    Public squads() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    Public counterP As Integer
    Public counter As Integer = 0
    Public p(100) As Integer
    Public s(100) As Integer
    Public v(100) As Integer
    Public w(100) As String
    Public rTotal As Integer
    Public min = 82
    Public max = 91
    Public ispricecall = False
    Private Sub cmbPlatform_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbPlatform.SelectionChanged
        If cmbPlatform.SelectedIndex = 0 Then
            platform = "pc"
        ElseIf cmbPlatform.SelectedIndex = 1 Then
            platform = "xbox"
        ElseIf cmbPlatform.SelectedIndex = 2 Then
            platform = "ps"
        End If
        cmbPlatformP.SelectedIndex = cmbPlatform.SelectedIndex
    End Sub
    Private Sub btnRun_Click(sender As Object, e As RoutedEventArgs) Handles btnRun.Click
        Dim temp As String = txtNumber.Text
        Dim temp2 As Integer
        Dim check As Boolean = False

        If IsNumeric(temp) Then
            temp2 = temp
            If temp2 > 81 And temp2 < 99 Then
                desired = temp2
                check = True
            Else
                MessageBox.Show("Rating must be between 82 and 99 inclusive.")
            End If
        Else
            MessageBox.Show("Inputted desired rating must be a number between 82 and 99 inclusive.")
        End If
        If platform = "" Then
            check = False
            MessageBox.Show("Select a platform.")
        End If
        If counter < 1 Then
            check = False
            MessageBox.Show("Enter a minimum of one rating.")
        End If
        If check = True Then
            priceseff()
            ispricecall = True
            complete()
            bruteforce42()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click
        Dim temp As String = txtAdd.Text
        Dim temp2 As Integer
        If counter <> 10 Then
            If IsNumeric(temp) Then
                temp2 = temp
                If temp2 > 46 And temp2 < 99 Then
                    list(counter) = temp2
                    counter += 1
                    txtAdd.Text = ""
                Else
                    MessageBox.Show("Rating must be between 47 and 99 inclusive.")
                End If
            Else
                MessageBox.Show("Inputted value must be a number between 47 and 99 inclusive.")
            End If
        Else
            MessageBox.Show("Enter a maximum of 10 values.")
        End If

    End Sub

    Private Sub btnCheck_Click(sender As Object, e As RoutedEventArgs) Handles btnCheck.Click
        Dim message As String = ""
        For i = 0 To counter - 1
            If i = counter - 1 Then
                message = message & list(i)
            Else
                message = message & list(i) & ", "
            End If
        Next
        MessageBox.Show(message)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As RoutedEventArgs) Handles btnReset.Click
        For i = 0 To counter - 1
            list(i) = 0
        Next
        counter = 0
    End Sub
    Sub priceseff()

        Dim pos1 As Long, pos2 As Long, price As String, price2 As String, price3 As String, t2 As Integer, t3 As Integer

        WriteLine("what is your platform? (pc, xbox, ps)")
        Dim WClient As New WebClient
        WClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml")
        WClient.Headers.Add("User-Agent", "Mozilla/ 5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36")
        WClient.Headers.Add("Accept-Language", "pt-BR, pt;q=0.5")
        WClient.Headers.Add("cookie", "platform=" & platform)

        Dim URL As String
        URL = "https://www.futbin.com/stc/cheapest"
        Dim strData As String = WClient.DownloadString(New Uri(URL))
        For value = min To max


            pos1 = InStr(strData, value & " </span>")
            pos1 = InStr(pos1 + 1, strData, "price-holder-row", vbTextCompare)
            pos2 = InStr(pos1 + 1, strData, "<img", vbTextCompare)



            price = strData.Substring(pos1 + 68, pos2 - pos1 - 69)

            pos1 = InStr(strData, value & " </span>")
            pos1 = InStr(pos1 + 1, strData, price, vbTextCompare)
            pos1 = InStr(pos1 + 1, strData, "price-holder-row", vbTextCompare)
            pos2 = InStr(pos1 + 1, strData, "<img", vbTextCompare)

            price2 = strData.Substring(pos1 + 68, pos2 - pos1 - 69)

            pos1 = InStr(strData, value & " </span>")
            pos1 = InStr(pos1 + 1, strData, price2, vbTextCompare)
            pos1 = InStr(pos1 + 1, strData, "price-holder-row", vbTextCompare)
            pos2 = InStr(pos1 + 1, strData, "<img", vbTextCompare)

            price3 = strData.Substring(pos1 + 68, pos2 - pos1 - 69)

            If price(price.Length - 2) = "K" Then
                p(value) = price.Substring(0, price.Length - 2) * 1000
            ElseIf price(price.Length - 2) = "M" Then
                p(value) = (price.Substring(0, price.Length - 2) * 1000000)
            Else
                p(value) = price
            End If
            If price2(price2.Length - 2) = "K" Then
                t2 = (price2.Substring(0, price2.Length - 2) * 1000)
            ElseIf price2(price2.Length - 2) = "M" Then
                t2 = (price2.Substring(0, price2.Length - 2) * 1000000)
            Else
                t2 = price2
            End If
            If price3(price3.Length - 2) = "K" Then
                t3 = (price3.Substring(0, price3.Length - 2) * 1000)
            ElseIf price3(price3.Length - 2) = "M" Then
                t3 = (price3.Substring(0, price3.Length - 2) * 1000000)
            Else
                t3 = price3
            End If

            If p(value) * 1.2 > t3 Then
                p(value) = (p(value) + t3 + t2) / 3
                w(value) = "***"
            ElseIf p(value) * 1.2 > t2 And Not (p(value) * 1.2) > t3 Then
                p(value) = (p(value) + t2) / 2
                w(value) = "**"
            Else
                w(value) = "*"
            End If
        Next
    End Sub
    Sub Bruteforce1()

        For i = min To max
            list(10) = i
            If calc() >= desired Then
                WriteLine(i)
                i = 99
            End If
        Next
    End Sub
    Sub Bruteforce2()
        priceseff()
        Dim cheapest(3, 3) As Integer
        cheapest(2, 0) = 100000000
        cheapest(2, 1) = 100000000
        cheapest(2, 2) = 100000000
        Dim price As Integer
        For i = min To max
            list(9) = i
            For j = min To max
                If Not (i = j) Or w(j) = "**" Or w(j) = "***" Then
                    list(10) = j
                    If Not (j = cheapest(0, 0) And i = cheapest(1, 0)) Then
                        If calc() >= desired Then
                            price = p(i) + p(j)
                            If cheapest(2, 0) > price Then
                                cheapest(0, 0) = i
                                cheapest(1, 0) = j
                                cheapest(2, 0) = price
                                j = 99
                            ElseIf cheapest(2, 1) > price Then
                                cheapest(0, 1) = i
                                cheapest(1, 1) = j
                                cheapest(2, 1) = price
                                j = 99
                            ElseIf cheapest(2, 2) > price Then
                                cheapest(0, 2) = i
                                cheapest(1, 2) = j
                                cheapest(2, 2) = price
                                j = 99
                            End If
                        End If
                    End If
                End If
            Next
        Next
        MessageBox.Show(cheapest(0, 0) & " & " & cheapest(1, 0) & " which costs " & cheapest(2, 0) & "
" & cheapest(0, 1) & " & " & cheapest(1, 1) & " which costs " & cheapest(2, 1) & "
" & cheapest(0, 2) & " & " & cheapest(1, 2) & " which costs " & cheapest(2, 2))
    End Sub
    Sub bruteforce3()
        priceseff()
        Dim cheapest(4, 3) As Integer
        cheapest(3, 0) = 1000000000
        cheapest(3, 1) = 1000000000
        cheapest(3, 2) = 1000000000
        Dim price As Integer
        For h = min To max
            list(8) = h
            For i = min To max
                list(9) = i
                For j = min To max
                    If (Not (i = j) Or w(j) = "**" Or w(j) = "***") And (Not (h = i) Or w(h) = "**" Or w(h) = "***") And (Not (h = j) Or w(h) = "**" Or w(h) = "***") Then
                        list(10) = j
                        If calc() >= desired Then
                            price = p(h) + p(i) + p(j)
                            If cheapest(3, 0) > price Then
                                cheapest(0, 0) = h
                                cheapest(1, 0) = i
                                cheapest(2, 0) = j
                                cheapest(3, 0) = price
                                j = 99
                            ElseIf cheapest(3, 0) = price Then
                                j = 99
                            ElseIf cheapest(3, 1) > price Then
                                cheapest(0, 1) = h
                                cheapest(1, 1) = i
                                cheapest(2, 1) = j
                                cheapest(3, 1) = price
                                j = 99
                            ElseIf cheapest(3, 2) > price Then
                                cheapest(0, 2) = h
                                cheapest(1, 2) = i
                                cheapest(2, 2) = j
                                cheapest(3, 2) = price
                                j = 99
                            End If
                        End If
                    End If
                Next
            Next
        Next
        MessageBox.Show(cheapest(0, 0) & " & " & cheapest(1, 0) & " & " & cheapest(2, 0) & " which costs " & cheapest(3, 0) & "
" & cheapest(0, 1) & " & " & cheapest(1, 1) & " & " & cheapest(2, 1) & " which costs " & cheapest(3, 1) & "
" & cheapest(0, 2) & " & " & cheapest(1, 2) & " & " & cheapest(2, 2) & " which costs " & cheapest(3, 2))
    End Sub
    Sub bruteforce4()
        If Not ispricecall Then
            priceseff()
        End If
        If Not (desired > 0) Then
        End If
        Dim cheapest(5, 3) As Integer
        cheapest(4, 0) = 1000000000
        cheapest(4, 1) = 1000000000
        cheapest(4, 2) = 1000000000
        Dim price As Integer
        For g = min To max
            list(7) = g
            For h = min To max
                list(8) = h
                For i = min To max
                    list(9) = i
                    For j = min To max
                        If (Not (i = j) Or w(j) = "**" Or w(j) = "***") And (Not (h = i) Or w(h) = "**" Or w(h) = "***") And (Not (h = j) Or w(h) = "**" Or w(h) = "***") And (Not (g = h) Or w(h) = "**" Or w(h) = "***") And (Not (g = i) Or w(i) = "**" Or w(i) = "***") And (Not (g = j) Or w(j) = "**" Or w(j) = "***") Then
                            list(10) = j
                            If calc() >= desired Then
                                price = p(g) + p(h) + p(i) + p(j)
                                If cheapest(4, 0) > price Then
                                    cheapest(0, 0) = g
                                    cheapest(1, 0) = h
                                    cheapest(2, 0) = i
                                    cheapest(3, 0) = j
                                    cheapest(4, 0) = price
                                    j = 99
                                ElseIf cheapest(4, 0) = price Or cheapest(4, 1) = price Then
                                    j = 99
                                ElseIf cheapest(4, 1) > price Then
                                    cheapest(0, 1) = g
                                    cheapest(1, 1) = h
                                    cheapest(2, 1) = i
                                    cheapest(3, 1) = j
                                    cheapest(4, 1) = price
                                    j = 99
                                ElseIf cheapest(4, 2) > price Then
                                    cheapest(0, 2) = g
                                    cheapest(1, 2) = h
                                    cheapest(2, 2) = i
                                    cheapest(3, 2) = j
                                    cheapest(4, 2) = price
                                    j = 99
                                End If
                            End If
                        End If
                    Next
                Next
            Next
        Next
        MessageBox.Show(cheapest(0, 0) & " & " & cheapest(1, 0) & " & " & cheapest(2, 0) & " & " & cheapest(3, 0) & " which costs " & cheapest(4, 0) + rTotal & "
" & cheapest(0, 1) & " & " & cheapest(1, 1) & " & " & cheapest(2, 1) & " & " & cheapest(3, 1) & " which costs " & cheapest(4, 1) + rTotal & "
" & cheapest(0, 2) & " & " & cheapest(1, 2) & " & " & cheapest(2, 2) & " & " & cheapest(3, 2) & " which costs " & cheapest(4, 2) + rTotal)
    End Sub
    Sub bruteforce42()
        If Not ispricecall Then
            priceseff()
        End If
        If Not (desired > 0) Then
        End If
        Dim cheapest(5, 3) As Integer
        cheapest(4, 0) = 1000000000
        cheapest(4, 1) = 1000000000
        cheapest(4, 2) = 1000000000
        Dim price As Integer
        For g = min To max
            list(7) = g
            For h = min To max
                list(8) = h
                For i = min To max
                    list(9) = i
                    For j = min To max
                        If (Not (i = j) Or w(j) = "**" Or w(j) = "***") And (Not (h = i) Or w(h) = "**" Or w(h) = "***") And (Not (h = j) Or w(h) = "**" Or w(h) = "***") And (Not (g = h) Or w(h) = "**" Or w(h) = "***") And (Not (g = i) Or w(i) = "**" Or w(i) = "***") And (Not (g = j) Or w(j) = "**" Or w(j) = "***") Then
                            list(10) = j
                            If calc() >= desired Then
                                price = p(g) + p(h) + p(i) + p(j)
                                If cheapest(4, 0) > price Then
                                    cheapest(0, 0) = g
                                    cheapest(1, 0) = h
                                    cheapest(2, 0) = i
                                    cheapest(3, 0) = j
                                    cheapest(4, 0) = price
                                    j = 99
                                ElseIf cheapest(4, 0) = price Or cheapest(4, 1) = price Then
                                    j = 99
                                ElseIf cheapest(4, 1) > price Then
                                    cheapest(0, 1) = g
                                    cheapest(1, 1) = h
                                    cheapest(2, 1) = i
                                    cheapest(3, 1) = j
                                    cheapest(4, 1) = price
                                    j = 99
                                ElseIf cheapest(4, 2) > price Then
                                    cheapest(0, 2) = g
                                    cheapest(1, 2) = h
                                    cheapest(2, 2) = i
                                    cheapest(3, 2) = j
                                    cheapest(4, 2) = price
                                    j = 99
                                End If
                            End If
                        End If
                    Next
                Next
            Next
        Next
        Dim additional As String = ""
        For i = counter To 6
            If i = 6 Then
                additional &= list(i)
            Else
                additional &= list(i) & " & "
            End If
        Next

        MessageBox.Show(cheapest(0, 0) & " & " & cheapest(1, 0) & " & " & cheapest(2, 0) & " & " & cheapest(3, 0) & " & " & additional & " which costs " & cheapest(4, 0) + rTotal & "
" & cheapest(0, 1) & " & " & cheapest(1, 1) & " & " & cheapest(2, 1) & " & " & cheapest(3, 1) & " & " & additional & " which costs " & cheapest(4, 1) + rTotal & "
" & cheapest(0, 2) & " & " & cheapest(1, 2) & " & " & cheapest(2, 2) & " & " & cheapest(3, 2) & " & " & additional & " which costs " & cheapest(4, 2) + rTotal)
    End Sub
    Function ValueLow()
        Dim highest As Integer = 0
        Dim htemp As Integer = 0
        For i = min To desired - 1
            If i > (desired - 0.5) Then
                v(i) = ((i + (2000 * (i - desired + 0.5))) * 1000) / p(i)
            Else
                v(i) = (i * 1000) / p(i)
            End If
            WriteLine("value: " & i & " = " & v(i))
            If v(i) > htemp Then
                htemp = v(i)
                highest = i
            End If
        Next
        rTotal = rTotal + p(highest)
        Return highest
    End Function
    Function ValueHigh()
        Dim highest As Integer = 0
        Dim htemp As Integer = 0
        For i = desired To max
            If i > (desired - 0.5) Then
                v(i) = ((i + (2000 * (i - desired + 0.5))) * 1000) / p(i)
            Else
                v(i) = (i * 1000) / p(i)
            End If
            If v(i) > htemp Then
                htemp = v(i)
                highest = i
            End If
            WriteLine("value: " & i & " = " & v(i))
        Next
        rTotal = rTotal + p(highest)
        Return highest
    End Function
    Function calc()
        Dim total As Decimal
        For i = 0 To 10
            total = total + list(i)
        Next
        Dim average As Decimal
        average = total / 11
        Dim difference As Decimal
        For j = 0 To 10
            If list(j) > average Then
                difference = difference + (list(j) - average)
            End If
        Next
        total = total + difference
        Dim step5 As Decimal
        step5 = (CInt(total)) / 11
        Dim step6 As Integer = Math.Truncate(step5)
        Return step6
    End Function

    Private Sub cmbPlatformP_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbPlatformP.SelectionChanged
        If cmbPlatformP.SelectedIndex = 0 Then
            platform = "pc"
        ElseIf cmbPlatformP.SelectedIndex = 1 Then
            platform = "xbox"
        ElseIf cmbPlatformP.SelectedIndex = 2 Then
            platform = "ps"
        End If
        cmbPlatform.SelectedIndex = cmbPlatformP.SelectedIndex

    End Sub


    Private Sub btnAddP_Click(sender As Object, e As RoutedEventArgs) Handles btnAddP.Click
        Dim temp As String = txtAddP.Text
        Dim temp2 As Integer
        If IsNumeric(temp) Then
            temp2 = temp
            If (temp2 > 74 And temp2 < 94) Or temp2 = 1 Then
                squads(counterP) = temp2
                counterP += 1
                txtAddP.Text = ""
            ElseIf temp2 = 1 Then
                squads(counterP) = temp2
            Else
                MessageBox.Show("Rating must be between 75 and 93 inclusive.")
            End If
        Else
            MessageBox.Show("Inputted value must be a number between 75 and 93 inclusive.")
        End If
    End Sub
    Private Sub btnCheckP_Click(sender As Object, e As RoutedEventArgs) Handles btnCheckP.Click
        Dim message As String = ""
        For i = 0 To counterP - 1
            If i = counterP - 1 Then
                message = message & squads(i)
            Else
                message = message & squads(i) & ", "
            End If
        Next
        MessageBox.Show(message)
    End Sub
    Private Sub btnResetP_Click(sender As Object, e As RoutedEventArgs) Handles btnResetP.Click
        For i = 0 To counterP - 1
            squads(i) = 0
        Next
        counterP = 0
    End Sub

    Private Sub btnRunP_Click(sender As Object, e As RoutedEventArgs) Handles btnRunP.Click
        Dim tempPrice As Integer
        priceseff()
        ispricecall = True
        For i = 83 To 92
            If i > 90 Then
                list = {84, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                counter = 1
                desired = i
                complete()
                tempPrice = bruteforce4Price()
                s(i) = tempPrice
            Else
                list = {83, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                counter = 1
                desired = i
                complete()
                tempPrice = bruteforce4Price()
                s(i) = tempPrice
            End If
        Next
        Dim displayPrice As Integer
        For j = 0 To counterP
            If squads(j) = 1 Then
                displayPrice = displayPrice + 7000
            Else
                displayPrice = s(squads(j)) + displayPrice
            End If
        Next
        Dim temp As String = ""
        For i = 82 To 92
            temp &= "
 " & i & " = " & s(i)
        Next
        MessageBox.Show(temp)

        MessageBox.Show(displayPrice)
    End Sub
    Sub complete()
        rTotal = 0
        Dim lenth As Integer = counter
        If lenth = 10 Then
            Bruteforce1()
        ElseIf lenth = 9 Then
            Bruteforce2()
        ElseIf lenth = 8 Then
            bruteforce3()
        ElseIf lenth = 7 Then
            bruteforce4()
        Else
            Dim average As Decimal
            While lenth < 7
                For i = 0 To lenth - 1
                    average = average + list(i)
                Next
                average = average / lenth
                If average > (desired - 0.5) Then
                    list(lenth) = ValueLow()
                    lenth = lenth + 1
                Else
                    list(lenth) = ValueHigh()
                    lenth = lenth + 1
                End If
            End While
        End If
    End Sub
    Function bruteforce4Price()
        If Not ispricecall Then
            priceseff()
        End If
        If Not (desired > 0) Then
        End If
        Dim cheapest(5) As Integer
        cheapest(4) = 1000000000
        Dim price As Integer
        For g = min To max
            list(7) = g
            For h = min To max
                list(8) = h
                For i = min To max
                    list(9) = i
                    For j = min To max
                        If (Not (i = j) Or w(j) = "**" Or w(j) = "***") And (Not (h = i) Or w(h) = "**" Or w(h) = "***") And (Not (h = j) Or w(h) = "**" Or w(h) = "***") And (Not (g = h) Or w(h) = "**" Or w(h) = "***") And (Not (g = i) Or w(i) = "**" Or w(i) = "***") And (Not (g = j) Or w(j) = "**" Or w(j) = "***") Then
                            list(10) = j
                            If calc() >= desired Then
                                price = p(g) + p(h) + p(i) + p(j)
                                If cheapest(4) > price Then
                                    cheapest(0) = g
                                    cheapest(1) = h
                                    cheapest(2) = i
                                    cheapest(3) = j
                                    cheapest(4) = price
                                    j = 99
                                End If
                            End If
                        End If
                    Next
                Next
            Next
        Next
        Dim additional As String = ""
        For i = counter To 6
            If i = 6 Then
                additional &= list(i)
            Else
                additional &= list(i) & " & "
            End If
        Next
        Dim total As Integer = cheapest(4) + rTotal
        Return total
    End Function
End Class
