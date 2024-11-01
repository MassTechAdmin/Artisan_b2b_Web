<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Send_SMS_Test.aspx.cs" Inherits="OLApply_Send_SMS_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>發送手機：</td>
                <td>
                    <asp:TextBox ID="txbPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">發送內容：</td>
                <td>
                    <asp:TextBox ID="txbContent" runat="server" Height="158px" TextMode="MultiLine" Width="399px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>發送結果：</td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="簡訊發送測試" />
    
    &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="清空" OnClick="Button2_Click" />
    
    </div>
    </form>
</body>
</html>
