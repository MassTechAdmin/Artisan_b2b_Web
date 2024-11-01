<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rraveler.aspx.cs" Inherits="Rraveler" %>
<!DOCTYPE html>

<%@ Register Src="WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="WebControl/Foot_17.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header_Menu_17.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,Chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_17.css" rel="stylesheet" type="text/css" />
    <link href="20151124.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/HeaderMenu17.js"></script>
    
    <%--特效JS--%>
    <script src="js/HeaderMenu17.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="images/artisan.ico"> 
<style>
#content {
    width: 1400px;
    height: auto;
    float: left;
    margin-top: 100px;
    padding: 50px;
    background: #fff;
    border: solid 1px #dcdcdc;
    box-sizing: border-box;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <nav>
            <uc1:Header ID="Header1" runat="server" />
        </nav>
<div id="content">
<!--表單-->
<div style="width: 950px; margin: 0 auto;"> 
        <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
    <asp:DropDownList
        ID="DropDownList1" runat="server">
        <asp:ListItem Value="2">團名</asp:ListItem>
        <asp:ListItem Value="3">報名日期</asp:ListItem>
        <asp:ListItem Value="4">姓名</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server" Width="224px"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="搜尋" OnClick="Button1_Click" />
    <br /><br />
    </asp:Panel> 
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" EmptyDataText="<p>尚未建立資料或未找到您所搜尋的資料</p>"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:TemplateField HeaderText="自動編號" Visible="false" SortExpression="自動編號">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("自動編號") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
</div>
<!--/表單-->    
</div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
