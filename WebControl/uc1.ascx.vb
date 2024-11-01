
Partial Class uc1
    Inherits System.Web.UI.UserControl

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If Me.textfield.Value <> "" Then
            Dim tt As String = ""
            Dim Url As String = "ClassifyProduct.aspx"
            tt = HttpUtility.UrlEncode(textfield.Value.Replace("'", ""))
            Url += "?tp=" + tt
            Me.Response.Redirect(Url)
        End If
    End Sub
End Class
