<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sel.aspx.cs" Inherits="Sel" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_sale.css" rel="stylesheet" type="text/css" />
    <link href="css/color.css" rel="stylesheet" type="text/css" />
    <script src="SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <link href="SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
    <uc1:Header ID="Header1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
    <asp:DropDownList
        ID="DropDownList1" runat="server">
        <asp:ListItem Value="2">團名</asp:ListItem>
        <asp:ListItem Value="3">報名日期</asp:ListItem>
        <asp:ListItem Value="4">網路報名單號</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="TextBox1" runat="server" Width="224px"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="搜尋" />
    <br /><br />
    </asp:Panel>    
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" EmptyDataText="<p>尚未建立資料或未找到您所搜尋的資料</p>"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:ButtonField Text="SingleClick"  CommandName="SingleClick" Visible="False"/>
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
    <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
