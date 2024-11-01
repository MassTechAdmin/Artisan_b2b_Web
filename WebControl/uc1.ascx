<%@ Control Language="VB" AutoEventWireup="false" CodeFile="uc1.ascx.vb" Inherits="uc1" %>
<link href="web.css" rel="stylesheet" type="text/css" />

	<!--頭-->
	<div id="head">
    	<div id="logo"><a href="http://www.artisan.com.tw"><img src="img/logo.jpg" width="320" height="83" border="0" /></a></div>
        <div id="head_button_tool">
        	<div id="head_button_search">
                <table border="0" align="right">
              <tr>
                    	<td height="14"><a href="http://www.artisan.com.tw">首頁</a></td>
                        <td><img src="img/spacing.jpg" width="5" height="19" /></td>
                        <td><a href="about.aspx">關於巨匠</a></td>
                        <td><img src="img/spacing.jpg" width="5" height="19" /></td>
                        <td><a href="http://www.artisan.com.tw/2011_DM/about_artisan2.htm">連絡我們</a></td>
                        <td><img src="img/spacing.jpg" width="5" height="19" /></td>
                        <td><a href="http://www.artisan.com.tw">線上客服</a></td>
                        <td width="8"></td>
                        <!-- 修改 -->
                        <td><input id="textfield" name="TripSearch" runat="server" type="text" value="巨匠旅遊行程快速搜尋" class="style_keying" onfocus="if(this.value=='巨匠旅遊行程快速搜尋'){this.value=''}" onblur="if(this.value==''){this.value='巨匠旅遊行程快速搜尋'}"/></td>
          	            <td><asp:ImageButton ID="ImageButton1" src="img/search.jpg" title="搜尋"  ImageAlign="absbottom" style="border:0;" align="top" runat="server" OnClick = "ImageButton1_Click" /></td>
                        <!-- /修改 -->
                    </tr>
                </table>
          </div>
            <div id="head_button">
            	<table border="0" align="left" cellpadding="0" cellspacing="0">
                	<tr>
                    	<td align="left" class="head_button_bg" height="41" width="110"><a href="http://www.artisan.com.tw/AllProduct.aspx">總團表</a><br><span class="head_button_e">Master List</span></td>
                        <td align="left" class="head_button_bg" height="41" width="110"><a href="http://www.artisan.com.tw/mov/video.html">線上發表 </a><br><span class="head_button_e">Web TV</span></td>
                        <td align="left" class="head_button_bg" height="41" width="110"><a href="http://www.artisan.com.tw">行前說明 </a><br><span class="head_button_e">Briefing</span></td>
                        <td align="left" class="head_button_bg" height="41" width="125"><a href="http://www.artisan.com.tw/mbook/default.asp">留言版</a><br><span class="head_button_e">Guest book</span></td>
                        <td align="left" class="head_button_bg" height="41"><a href="http://www.facebook.com/pages/%E5%87%B1%E6%97%8B%E6%97%85%E8%A1%8C%E7%A4%BE%E5%B7%A8%E5%8C%A0%E6%97%85%E9%81%8A/305852735217">facebook粉絲專區</a><br><span class="head_button_e">facebook fans</span></td>
                    </tr>
              </table>
          </div>
        </div>        
    </div>
    <!--頭 end-->