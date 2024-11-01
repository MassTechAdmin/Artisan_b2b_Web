<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="WebControl_Header" %>

        <div id="head">
            <div id="logo">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/index/logo.jpg" PostBackUrl="~/index.aspx" />
            </div>
	    <!--<img src="/images/line_banner01.gif" border="0" style="padding: 10px;">-->
	    <a href="http://b2b.artisan.com.tw/DMShow.aspx?no=65"><img src="/images/line_banner05.gif" border="0" style="padding: 10px;"></a>
            <div style="float:right;">
            <div style="float:left;margin-top:30px;margin-right:10px;"><a href="http://b2b.artisan.com.tw/DMShow.aspx?no=5" style="border:0px;"><img src="http://b2b.artisan.com.tw/images/line_button.jpg"></a></div>
            <div id="top_link">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Rraveler.aspx">旅客查詢</asp:HyperLink> ｜ 
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/order_list.aspx">報名管理</asp:HyperLink> ｜ 
                <asp:Literal ID="Literal1" runat="server"></asp:Literal> 
                <asp:LinkButton ID="LinkLoginOut" runat="server" OnClick="LinkLoginOut_Click">登出</asp:LinkButton>
            </div>
            </div>

        </div>
        <div id="menu_area">
			<div class="mainNav">

                <asp:Literal ID="litAreaCountry" runat="server"></asp:Literal>
			</div>
            <div><asp:HyperLink ID="HyperLink6" runat="server" class="menu_link">自由行</asp:HyperLink></div>
            <div class="menu_buttom">　<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/index/List_bottom.jpg" PostBackUrl="~/ClassifyProduct.aspx" /></div>
            <div class="buttom_line"></div>
            <div class="menu_buttom"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/index/member_buttom.jpg" PostBackUrl="~/Member.aspx" /></div>
        </div>
        <div id="submenu">
            <div class="secondNav">
                <asp:Literal ID="listAreaSub" runat="server"></asp:Literal>
            </div>
        </div>