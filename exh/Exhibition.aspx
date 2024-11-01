<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exhibition.aspx.cs" Inherits="Exh_Exhibition" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="/WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="/WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport">
    <title>凱旋旅行社(巨匠旅遊)同業網</title>
    <script src="../js/jquery-1.6.min.js"></script>
    <link rel="stylesheet" href="../css/layout_b2b.css">
    <link rel="stylesheet" href="../css/Exh_b2b.css">
    <script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper" style="padding-top: 110px;">
            <!--b2b header-->
            <header>
                <nav>
                    <uc1:Header ID="Header_Menu1" runat="server" />
                </nav>
            </header>
            <!--/b2b header-->
            <div id="Exhibition_filter">
                <div class="Exhibition_filter_head">
                    <!--搜尋-->
                    <div class="Exhibition_filter_tool">
                        <div class="Exhibition_filter_kind">
                            <div class="search-title">系列</div>
                            <asp:DropDownList ID="DropDownList0" runat="server" AutoPostBack="True" class="search-textBox" OnSelectedIndexChanged="DropDownList0_SelectedIndexChanged">
                                <asp:ListItem Value="3">巨匠</asp:ListItem>
                                <asp:ListItem Value="2">新視界</asp:ListItem>
                                <asp:ListItem Value="1">典藏</asp:ListItem>
                                <asp:ListItem Value="4">珍藏</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="Exhibition_filter_kind">
                            <div class="search-title">區域</div>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" class="search-textBox" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" />
                        </div>
                    </div>
                    <!--搜尋-->
                    <div class="Exhibition_filter_title">
                        <img src="../images/Exh_search_icon.png" alt=""><span>篩選(可複選)</span>　旅遊地點
                    </div>
                    <!--CheckBox-->
                    <div class="Exhibition_filter_type">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        <asp:TextBox ID="TextBox1" runat="server" Style="display: none"></asp:TextBox>
                    </div>
                    <!--CheckBox-->
                </div>
                <!--地區標題-->
                <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                <!--地區標題-->
                <!--大館行程-->
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <!--大館行程-->
            </div>
            <!--b2b footer-->
            <uc2:Foot ID="Foot1" runat="server" />
            <!--/b2b footer-->
        </div>
    </form>
</body>
</html>
