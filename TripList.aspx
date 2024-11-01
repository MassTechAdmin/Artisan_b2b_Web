<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripList.aspx.cs" Inherits="TripList" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_sale.css" rel="stylesheet" type="text/css" />
    <link href="css/old_web.css" rel="stylesheet" type="text/css" />
    <link href="css/color.css" rel="stylesheet" type="text/css" />
    <script src="SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <link href="SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-artisan-130221.js"></script>
    <link rel="stylesheet" type="text/css" href="css/elastislide.css" />
    <script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Artisan_SubMenu.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div id="wrapper">
	 <uc1:Header ID="Header1" runat="server" />
     <div id="content"> 
   <!-----修改地區-----> 
    <div class="NoNewline">
        <asp:Literal ID="titlepiclit" runat="server"></asp:Literal>        
        </div>
    	<!--左清單-->
    	<div id="left_button">    	
        	<!--按鈕-->
        	<asp:Literal ID="btnlit" runat="server"></asp:Literal>           
            <!--按鈕結束-->            
          <br>
            <br>
            <div class="list_flight"><img src="img/flight-1.jpg" width="178" height="92" /></div>
            <div id="list_flight_tool">
            	<div class="tour_text"><span class="tour_blue">【去程】</span></div>
            	<div class="tour_text">
            	<asp:Literal ID="plango" runat="server"></asp:Literal>
            	</div>
            	<div class="tour_text"><span class="tour_blue">【回程】</span></div>
            	<div class="tour_text">
            	<asp:Literal ID="plancome" runat="server"></asp:Literal><br>以上為參考航班，實際航班<br>時間以航空公司為最終確認。<br>
若因航空公司或不可抗力因素，變動航班時間或轉機點，<br>造成團體行程變更、增加餐食，本公司將不另行加價。<br>若行程變更、減少餐食，<br>則酌於退費。
            </div>	
            </div>
            <div class="list_flight"><img src="images/old/flight-3.jpg" width="178" height="3" /></div>
            <br>
            <div class="list_pic">
                <asp:Literal ID="printlit" runat="server"></asp:Literal>
                </div>
            <div class="list_pic"><a href="#"><img src="images/old/helper.jpg" width="178" height="70" border="0" /></a></div><br>
            <div class="list_pic"><img src="images/old/recommend_link.jpg" width="178" height="40" /></div>
            <div class="list_pic"><a href="http://www.facebook.com/pages/%E5%87%B1%E6%97%8B%E6%97%85%E8%A1%8C%E7%A4%BE%E5%B7%A8%E5%8C%A0%E6%97%85%E9%81%8A/305852735217?sk=info"><img src="images/old/facebook.jpg" width="178" height="125" border="0"/></a></div>
            <div class="list_pic"><img src="images/old/tourist_blog.jpg" width="178" height="11" /></div>
            <div class="list_blog">
            	<div><img src="images/old/tourist_blog-2.jpg" width="176" height="54" /></div>
                <div  class="list_blog_info">
                    <asp:Panel ID="Panel1" runat="server">
                    </asp:Panel>
                </div>
                <br>
            </div>
            <div class="list_pic"><img src="images/old/tour_artisan_blog.jpg" width="178" height="27" /></div>
            <div class="artisan_blog">
                <asp:Panel ID="Panel2" runat="server">
                </asp:Panel>
            </div>  <br><asp:Literal ID="twopic" runat="server"></asp:Literal>
            <!-------------------------------0517結束----------------------------------------->
<br><br>
<br>
            <asp:Literal ID="lit_triplist" runat="server"></asp:Literal>
        </div>
        <!--左清單 end-->
        <!--右行程資料-->
        <div id="main_right_tool">
                    <div id="early_bird"><asp:Literal ID="Lit_early_bird" runat="server"></asp:Literal></div>
        	<!--行程特色-->
        	<div class="tour_divide"><script src="http://connect.facebook.net/zh_TW/all.js#xfbml=1"></script><fb:like href="http://www.artisan.com.tw<% Response.Write(Request.RawUrl); %>" send="false" width="450"  show_faces="true" font=""></fb:like></div>
            <asp:Literal ID="litAp" runat="server"></asp:Literal>

            <div id="sale_point2">            
                <asp:Literal ID="LitSale_point" runat="server"></asp:Literal>
            </div>
            <div class="tour_characteristic">     <!--最大-->
                <!-- Flash -->
                <div class="tour_characteristic_tool2"><asp:Literal ID="flashlit" runat="server"></asp:Literal></div>
                <asp:Literal ID="TripFeat" runat="server"></asp:Literal>
            </div>
            <!--行程特色 end-->
             <!--行程規劃-->
            <div class="tour_divide2"><img src="img/tour2.jpg" width="753" height="33" /></div>
            <div class="tour_characteristic">
            	<div class="tour_characteristic_tool">
            	 <asp:Literal ID="maplit" runat="server"></asp:Literal></div>            	
            	<!-- 行程介紹 -->               
            	<asp:Literal ID="maintour" runat="server"></asp:Literal>   
            	   <div id="tour_explain"><span class="tour_characteristic_text3">網路行程內容僅供參考，內容會依出發日期不同或因班機變動將有所調整，正確行程、
            	   航班及旅館依行前說明會資料為準，歡迎來電索取當團詳細行程表。行程服務專線(02)2518-0011</span></div>     
					<div id="tour_other_tour">
                    	<div id="tour_other_tour_text">其他相關行程</div>
                        <div id="tour_other_tour_info">
                        <asp:Literal ID="otherlit" runat="server"></asp:Literal>                     
                        </div>
                    </div>
          </div>
            <!--行程規劃 end-->
        </div>
        <!--右行程資料 end-->
    <!-----修改地區 end ----->
    </div>
     <uc2:Foot ID="uc2" runat="server" ></uc2:Foot>
        <asp:Label ID="Label1" runat="server" Text="1" Visible="false"></asp:Label>  
</div>
</form>
</body>
</html>
