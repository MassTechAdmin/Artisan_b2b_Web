<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header_Menu_17.ascx.cs" Inherits="WebControl_Header_Menu_17" %>

<!--調整用css-->
<style type="text/css">
.SpecialEvent_left .link .selected:after {left: 128%;}
.RecentlyHot_left .link .selected:after {left: 138%;}
.SpotLight_left .link .selected:after {left: 140%;}
.LinkWeb_tool .link li {padding-bottom: 7px;}
</style>

<!--選單指標調整用css-->
<style type="text/css">
.navTool #main12:before {left: 215px;}
.navTool #main13:before {left: 222px;}
.navTool #main14:before {left: 230px;}
.navTool #main15:before {left: 240px;}
.navTool #main16:before {left: 295px;}
.navTool #main17:before {left: 350px;}
</style>


<!--head公司LOGO連結開始--> 
<div id="logo"><a href="http://b2b.artisan.com.tw/index.aspx"><img src="/images/logo_b2b.png" alt="logo"></a></div>
<!--head公司LOGO連結結束-->  

<!--head右側連結開始-->
<div id="nav_link">
    <a id="login1" runat="server" href="http://b2b.artisan.com.tw/Member.aspx"><img src="http://www.artisan.com.tw/images/link_icon01.png" alt="會員修改"> 會員修改</a>
    <a id="login3" runat="server" href="http://b2b.artisan.com.tw/Loginout.aspx"><img src="http://www.artisan.com.tw/images/link_icon01.png" alt="登出"> 登出</a>
    <a id="login2" style="display:none;" runat="server" href="https://www.artisan.com.tw/order_list.aspx"><img src="/images/link_icon02.png" alt="會員專區"> 會員專區</a>
    <a href="http://b2b.artisan.com.tw/order_list.aspx"><img src="http://www.artisan.com.tw/images/link_icon02.png" alt=""> 報名管理</a>
    <a href="http://b2b.artisan.com.tw/Rraveler.aspx"><img src="http://www.artisan.com.tw/images/link_icon05.png" alt=""> 旅客查詢</a>
    
</div>
<!--head右側連結結束-->

<div style="float: right; margin: 15px -15px 15px 15px;">
    <a href="http://b2b.artisan.com.tw/DMShow.aspx?no=5"><img src="/images/line_button.jpg" alt="加入好友"></a>
</div>

<!--banner廣告 -->
<div style="float: left; margin: 15px 35px 0; display: none;">
    <a href="http://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20190109000001"><img src="/images/20190227.gif" alt=""></a>
</div>  
  

<ul id="main_nav">
    <asp:Literal ID="main_all" runat="server"></asp:Literal>
</ul> 