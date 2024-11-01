
Partial Class webcontrol_account
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            account()
        End If
    End Sub

    Private Sub account()
        Dim constring As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings("count_ConnectionString").ToString
        Dim connect As New Data.OleDb.OleDbConnection(constring)
        Try
            connect.Open()
            Dim newip As String = IPAddress()
            Dim strsql As String = ""
            Dim strCompany_id As String = ""

            strCompany_id = Me.Request.QueryString("tripno")

            'If Me.Request.QueryString("area_no") IsNot Nothing Then
            '    Select Case Request("area_no")
            '        Case "1"
            '            strCompany_id = "T00000000000010"
            '            Exit Select
            '        Case "2"
            '            strCompany_id = "T00000000000020"
            '            Exit Select
            '        Case "5"
            '            strCompany_id = "T00000000000050"
            '            Exit Select
            '        Case "6"
            '            strCompany_id = "T00000000000060"
            '            Exit Select
            '        Case "7"
            '            strCompany_id = "T00000000000070"
            '            Exit Select
            '        Case "8"
            '            strCompany_id = "T00000000000080"
            '            Exit Select
            '        Case "9"
            '            strCompany_id = "T00000000000090"
            '            Exit Select
            '        Case "10"
            '            strCompany_id = "T00000000000100"
            '            Exit Select
            '        Case "11"
            '            strCompany_id = "T00000000000110"
            '            Exit Select
            '        Case "12"
            '            strCompany_id = "T00000000000120"
            '            Exit Select
            '        Case "13"
            '            strCompany_id = "T00000000000130"
            '            Exit Select
            '    End Select
            'End If

            strsql = "Select * From bindextop10 where ip ='" + newip + "' and company_id = '" + strCompany_id + "' "
            Dim comm As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(strsql, connect)
            Dim reader As System.Data.OleDb.OleDbDataReader = comm.ExecuteReader()
            '當一個ip-a加1之後，其ip-a要在加1必須等十個人看過網頁之後，總數才會在加1
            If (reader.Read() = False) Then
                strsql = "Select count(*) as abc From bindextop10 where company_id = '" + strCompany_id + "' "
                Dim comm2 As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(strsql, connect)
                Dim reader2 As System.Data.OleDb.OleDbDataReader = comm2.ExecuteReader()
                If (reader2.Read() = True) Then
                    If (Convert.ToInt16(reader2.GetValue(0).ToString()) > 9) Then
                        strsql = "Select top 1 id From bindextop10 where company_id = '" + strCompany_id + "' order by id"
                        Dim comm3 As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(strsql, connect)
                        Dim reader3 As System.Data.OleDb.OleDbDataReader = comm3.ExecuteReader()

                        If (reader3.Read() = True) Then
                            strsql = "delete From bindextop10 where id =" + reader3.GetValue(0).ToString()
                            Dim comm4 As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(strsql, connect)
                            comm4.ExecuteNonQuery()
                            comm4.Dispose()
                        End If
                        comm3.Dispose()
                        reader3.Close()
                    End If
                End If
                comm2.Dispose()
                reader2.Close()

                strsql = " insert into bindextop10 (ip,indexdate,inputdate,company_id) values ("
                strsql += " '" + newip + "',"
                strsql += " '" + Today.ToShortDateString() + "',"
                strsql += " getdate(),"
                strsql += " '" + strCompany_id + "'"
                strsql += " )"
                Dim comm5 As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(strsql, connect)
                comm5.ExecuteNonQuery()
                comm5.Dispose()


                strsql = " insert into B" & Today.Month & " (ip,indexdate,inputdate,company_id) values ("
                strsql += " '" + newip + "',"
                strsql += " '" + Today.ToShortDateString() + "',"
                strsql += " getdate(),"
                strsql += " '" + strCompany_id + "'"
                strsql += " )"
                Dim comm7 As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(strsql, connect)
                comm7.ExecuteNonQuery()
                comm7.Dispose()

                strsql = "Select top 1 total From Bindexcounter where indexdate='" + Today.ToString("yyyy/MM/dd") & "' and company_id = '" & strCompany_id & "'"
                Dim comm8 As New System.Data.OleDb.OleDbCommand(strsql, connect)
                Dim reader8 As System.Data.OleDb.OleDbDataReader = comm8.ExecuteReader()
                If reader8.Read() = True Then
                    strsql = " update Bindexcounter "
                    strsql += " set total=" & reader8("total").ToString() & "+1"
                    strsql += " ,indexdate='" + Today.ToShortDateString() & "'"
                    strsql += " ,inputdate=getdate()"
                    strsql += " ,company_id='" & strCompany_id & "'"
                    strsql += " where indexdate='" + Today.ToString("yyyy/MM/dd") & "'"
                    strsql += " and company_id = '" & strCompany_id & "'"
                    Dim comm9 As New System.Data.OleDb.OleDbCommand(strsql, connect)
                    comm9.ExecuteNonQuery()
                    comm9.Dispose()
                Else
                    strsql = "insert into Bindexcounter (total,indexdate,inputdate,company_id) values ("
                    strsql += " 1,"
                    strsql += " '" + Today.ToShortDateString() & "',"
                    strsql += " getdate(),"
                    strsql += " '" & strCompany_id & "'"
                    strsql += " )"
                    Dim comm10 As New System.Data.OleDb.OleDbCommand(strsql, connect)
                    comm10.ExecuteNonQuery()
                    comm10.Dispose()
                End If
                reader8.Close()
                comm8.Dispose()

            End If
            comm.Dispose()
            reader.Close()
        Catch ex As Exception
        Finally
            connect.Close()
        End Try
    End Sub

    Public Function IPAddress() As String
        Dim result As String = String.Empty
        result = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If (Not result Is Nothing AndAlso result <> String.Empty) Then
            '可能有代理    
            If (result.IndexOf(".") = -1) Then '沒有，肯定是非IPv4格式  
                result = Nothing
            Else
                If (result.IndexOf(",") <> -1) Then
                    '有，估計有多個代理，取第一個不是內網的ip。
                    result = result.Replace(" ", "").Replace("""", "")
                    Dim temparyip() As String = result.Split(",;".ToCharArray())
                    For i As Integer = 0 To temparyip.Length - 1
                        If (isIPAddress(temparyip(i)) AndAlso temparyip(i).Substring(0, 3) <> "10." AndAlso temparyip(i).Substring(0, 7) <> "172.16.") Then
                            Return temparyip(i)    '找到不是內網的地址
                        End If
                    Next
                ElseIf (isIPAddress(result)) Then '代理即是IP格式    
                    Return result
                Else
                    result = Nothing    '代理中的内容 非IP，取IP    
                End If
            End If
        End If

        'Dim IpAddress As String = IIf((Not HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") Is Nothing AndAlso HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") <> String.Empty), HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR"), HttpContext.Current.Request.ServerVariables("REMOTE_ADDR"))
        If (result Is Nothing OrElse result = String.Empty) Then
            result = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            If (result Is Nothing OrElse result = String.Empty) Then
                result = HttpContext.Current.Request.UserHostAddress
            End If
        End If

        Return result
    End Function

    Public Function isIPAddress(ByVal strAddress As String) As Boolean
        Dim bResult As Boolean = True
        Dim ch As Char
        For Each ch In strAddress
            If ((False = Char.IsDigit(ch)) AndAlso (ch <> ".")) Then
                bResult = False
                Exit For
            End If
        Next

        Return bResult
    End Function
End Class
