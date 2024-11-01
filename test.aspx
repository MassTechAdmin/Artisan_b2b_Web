<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>凱旋旅行社(巨匠旅遊)同業網</title>

</head>
<body>
    <form id="form1" runat="server">
        <!--大廣告-->
        <div id="AD">
            <div class="AD_mask">
                <h5 class="AD_close">X</h5>
                <asp:Literal runat="server" ID="litAD" />
            </div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            姓名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <!-- 大廣告 end-->

    </form>
</body>
</html>
