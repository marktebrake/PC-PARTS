Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions


Public Class Form1
    Dim iStart As Integer
    Private Sub Scrape()

        'InStr()
        Dim sDescription As String = "<b>Description</b>"
        'Dim iEndofData As Integer

        Try

            '///TEXT BOX FOR SEARCH ENGINE TO SEARCH - STRING CREATED FROM THIS
            Dim sTextentered As String
            Dim sStartOfRecord As String
            sTextentered = txtSearchText.Text


            '///SEARCH ENGINE DEFINED BY BLANK SEARCH URL - STRING ADDED TO THE END OF A BLANK SEARCH URL
            Dim strURL As String = "Http://staticice.com.au/cgi-bin/search.cgi?q=" & sTextentered & "&links=" & nudHowMany.Value
            Dim strOutput As String = ""
            Dim wrResponse As WebResponse
            Dim wrRequest As WebRequest = HttpWebRequest.Create(strURL)

            txtScrape.Text = "Finding you the best Prices..." & Environment.NewLine

            wrResponse = wrRequest.GetResponse()

            Using sr As New StreamReader(wrResponse.GetResponseStream())
                strOutput = sr.ReadToEnd()
                ' Close and clean up the StreamReader
                sr.Close()
            End Using
            iStart = InStr(strOutput, sDescription)


            '///TEXTBOX CONTAINING RAW SCRAPE
            txtScrape.Text = strOutput

            strOutput = Strings.Right(strOutput, Len(strOutput) - iStart) 'truncate rubbish from start

            '//// Index - Produce top 5 results
            For index As Integer = 1 To nudHowMany.Value


                'GET LOCATION/STORE
                sStartOfRecord = "><a alt=" & Chr(34)

                'MessageBox.Show(GetData(strOutput, sStartOfRecord, ":"))

                'iStart = InStr(strOutput, sStartOfRecord)
                'iStart = iStart + Len(sStartOfRecord) + 1
                'iEndofData = InStr(iStart, strOutput, ":")
                Dim sData3 As String
                sData3 = GetData(strOutput, sStartOfRecord, ":")
                'MessageBox.Show("sData3:" & sData3)



                'GET DESCRIPTION
                sStartOfRecord = "Click to see the latest price for"

                'iStart = InStr(strOutput, sStartOfRecord)
                'iStart = iStart + Len(sStartOfRecord)
                'iEndofData = InStr(iStart, strOutput, "title")
                Dim sData2 As String
                sData2 = GetData(strOutput, sStartOfRecord, Chr(34) & " title")
                'MessageBox.Show("sData2:" & sData2)


                'GET PRICE $
                sStartOfRecord = ">$"

                'iStart = InStr(strOutput, sStartOfRecord)
                'iStart = iStart + Len(sStartOfRecord) - 1
                'iEndofData = InStr(iStart, strOutput, "<")
                Dim sData As String
                sData = GetData(strOutput, sStartOfRecord, "<")
                'MessageBox.Show("sData:" & sData)









                'strOutput = Strings.Right(strOutput, Len(strOutput) - iStart) 'truncate rubbish while indexing





                '///FORMATTING, REMOVING JUNK FROM SCRAPE
                ' Remove Doctype ( HTML 5 )
                'strOutput = Regex.Replace(strOutput, "<!(.|\s)*?>", "")

                ' Remove HTML Tags
                'strOutput = Regex.Replace(strOutput, "</?[a-z][a-z0-9]*[^<>]*>", "")

                ' Remove HTML Comments
                'strOutput = Regex.Replace(strOutput, "<!--(.|\s)*?-->", "")

                ' Remove HTML Comments
                'strOutput = Regex.Replace(strOutput, "<|#&>", "")

                ' Remove Script Tags
                'strOutput = Regex.Replace(strOutput, "<script.*?</script>", "", RegexOptions.Singleline Or RegexOptions.IgnoreCase)

                ' Remove Stylesheets
                'strOutput = Regex.Replace(strOutput, "<style.*?</style>", "", RegexOptions.Singleline Or RegexOptions.IgnoreCase)

                'strOutput = strOutput.Replace("&nbsp", " ")

                '///TEXTBOX PRICE , DESCRIPTION and LOCATION
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & " Price = $" & sData & ",  Description = " & sData2 & ",   Location = " & sData3 'write Formatted Output To Separate TB
            Next
        Catch ex As Exception

            Console.WriteLine(ex.Message, "Error")

        End Try

    End Sub

    Function GetData(ByRef strIn As String, sStartOfRecord As String, sEndOfRecord As String)
        'Dim strOutput As String
        'GET LOCATION/STORE
        Dim iEndofData As Integer

        iStart = InStr(strIn, sStartOfRecord)
        iStart = iStart + Len(sStartOfRecord)
        iEndofData = InStr(iStart, strIn, sEndOfRecord)
        GetData = Mid(strIn, iStart, iEndofData - iStart)
        strIn = Strings.Right(strIn, Len(strIn) - iEndofData) 'truncate rubbish from start
        'Dim sData3 As String
        'MessageBox.Show("sData3:" & sData3)
    End Function

    Private Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        RichTextBox1.Text = ""
        Scrape() 'Scrape Text From URL

    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles txtSearchText.Enter
        If txtSearchText.Text = "Enter Model Number or Part Name" Then
            txtSearchText.Text = ""
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If MessageBox.Show("Are you sure you want to close PCPARTS?") Then

        ' End If
        'Application.Exit()

        If MessageBox.Show("Are you sure you want to quit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            Application.Exit()

        End If
    End Sub

    Private Sub txtSearchText_TextChanged(sender As Object, e As EventArgs) Handles txtSearchText.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
