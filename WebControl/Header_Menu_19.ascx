<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header_Menu_19.ascx.cs" Inherits="WebControl_Header_Menu_19" %>

<div class="container">
    <div id="logo"><a href="http://b2b.artisan.com.tw/index.aspx">
        <img src="/images/logo_b2b.png" alt="logo"></a></div>
    <div id="nav_link">
        <div class="nav_social_tool">
            <a href="http://b2b.artisan.com.tw/DMShow.aspx?no=5" target="_blank"><img src="/images/social_icon01.png" alt="line"></a>
            <a href="https://www.facebook.com/artisantour/" target="_blank"><img src="/images/social_icon02.png" alt="fb"></a>
            <a href="https://www.youtube.com/channel/UC0RvsJSeoq2gM_z5gyd6mWA/featured" target="_blank"><img src="/images/social_icon03.png" alt="youtube"></a>
        </div>
        <div class="nav_head_tool">
            <a id="login1" runat="server" href="http://b2b.artisan.com.tw/Rraveler.aspx"><img src="/images/head_icon01.png" alt="">旅客查詢</a>
            <a id="login2" runat="server" href="http://b2b.artisan.com.tw/order_list.aspx"><img src="/images/head_icon02.png" alt="">報名管理</a>
            <a id="login3" runat="server" href="/Loginout.aspx"><img src="/images/head_icon03.png" alt="">會員登出</a>
        </div>

    </div>
    <span id="menubtn" runat="server"><button class="menu-btn">&#9776;</button></span>
</div>
<!-- Pushy Menu -->
<div class="pushy pushy-right" data-focus="#first-link">
    <div class="pushy-content">
        <span class="pushy-close">
            <img src="/images/pushy_close.svg" alt=""></span>
        <ul>
			<li class="pushy-title"><a href="javascript:;">國內旅遊</a></li>
            <asp:Literal runat="server" ID="litAreaTW" />
			<li class="pushy-title"><a href="javascript:;">國外旅遊</a></li>
            <asp:Literal ID="main_all" runat="server"></asp:Literal>
        </ul>
    </div>
</div>

<!-- Site Overlay -->
<div class="site-overlay"></div>
<!-- Pushy JS -->
<script src="/js/pushy.min.js"></script>

<%--<ul>
<asp:Literal ID="main_all" runat="server"></asp:Literal>
</ul>--%>
