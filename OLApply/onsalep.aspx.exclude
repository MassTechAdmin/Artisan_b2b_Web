<%@ Page Language="C#" AutoEventWireup="true" CodeFile="onsalep.aspx.cs" Inherits="onsalep" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="20151124.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_sale.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-artisan-130221.js"></script>
    <link rel="stylesheet" type="text/css" href="css/elastislide.css" />
    <script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Artisan_SubMenu.js"></script>
     <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/trontastic/jquery-ui.css">
  <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
</head>
<body>
    <form id="form1" runat="server">
         <div id="wrapper">
        <uc1:header ID="Header1" runat="server" />
         <div style="text-align: center; width: 950px; margin-left: auto; margin-right: auto;">
    <div class="form_title">兌換券</div>
    <div class="formSearch_tool">
    <div>
        <asp:GridView ID="GridView1" runat="server" EmptyDataText="尚未建立資料或未找到您所搜尋的資料" AllowSorting="True" style="margin-top: 0px" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="序號"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Coupon_No", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="折抵價格"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Price", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到期日"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Deadline", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="持有人"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("ConnID", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="使用狀態"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("isUse", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="行程"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Trip", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="生產日期"  ControlStyle-Width="75"  >
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Crea_Date", "{0}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

             </Columns>
        </asp:GridView>
    
    </div>
</div>
             <div style="margin-bottom:40px;float:right;">&nbsp;
　<asp:Button ID="Button1" runat="server" Text="回上一頁" OnClick="Button1_Click" />
　</div>
         
             </div>
             <uc2:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
