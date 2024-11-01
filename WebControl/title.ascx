<%@ Control Language="C#" AutoEventWireup="false" CodeFile="title.ascx.cs" Inherits="WebUserControl" %>

<style type="text/css">
    #headertitle {
	background: url(/2010/margin_line.jpg) bottom repeat-x;
	height: 100px;
	}
	#headertitle h1 {
		float: left;
		padding-top: 0px;
	}
	#headertitle h2 {
		font: 13px normal;
		margin-top: 10px;
		float: right;
		padding-right: 20px;
	}
	#headertitle h3 {
		float: right;
		padding-right: 10px;
		margin-top: 10px;
	}
	#headertitle h2 a{
		color: #888;
	}
	#headertitle h2 a:hover {
		color: #f90;
	}
	.hd_btn {
		float: left;
		margin: 0 5px;
		}
</style>
    <div id="headertitle">
    	    <h1><a href="http://www.artisan.com.tw"><img src="/2010/2010logo.jpg" alt="前往首頁" width="160" height="60" style="border:0" /></a></h1>
            <h2>
               <span class="blue">
                   <%
                        string weekname = "";
                        switch (DateTime.Today.DayOfWeek)
                        {
                            case DayOfWeek.Sunday:
                                weekname = "星期日";
                                break;
                            case DayOfWeek.Monday:
                                weekname = "星期一";
                                break;
                            case DayOfWeek.Tuesday:
                                weekname = "星期二";
                                break;
                            case DayOfWeek.Wednesday:
                                weekname = "星期三";
                                break;
                            case DayOfWeek.Thursday:
                                weekname = "星期四";
                                break;
                            case DayOfWeek.Friday:
                                weekname = "星期五";
                                break;
                            case DayOfWeek.Saturday:
                                weekname = "星期六";
                                break;
                        }
                        Response.Write(DateTime.Today.Year + "年" + DateTime.Today.Month + "月" + DateTime.Today.Day + "日" + " " + weekname + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                   %>
               </span>　　　　　　　　
          	      |   
          	    <a href="http://www.artisan.com.tw/hotel_list/2010_month/right13_2010_jan.html">行前說明</a>   |   
          	    <a href="http://www.artisan.com.tw/AllProduct.aspx">總團表</a>   |   
          	    快速搜尋
          	    <input id="TripSearch" name="TripSearch" type="text" value="巨匠旅遊行程快速搜尋" class="style_keying" onfocus="if(this.value=='巨匠旅遊行程快速搜尋'){this.value=''}" onblur="if(this.value==''){this.value='巨匠旅遊行程快速搜尋'}"/>
          	    <asp:ImageButton ID="ImageButton1" title="搜尋" PostBackUrl="~/Search.aspx" ImageUrl="2010/gosearch.jpg" ImageAlign="absbottom" style="border:0;" align="top" runat="server" />			
          	</h2>
            <h3>
        	<div class="hd_btn"><a target='_self' href='http://www.artisan.com.tw/mov/video.aspx'><img alt="" src="/2010/btn_video.jpg" style="border:0" /></a></div>
		<div class="hd_btn"><a target='_blank' href='http://www.artisan.com.tw/blog/'><img alt="" src="/2010/btn_blog-1.jpg" style="border:0" /></a></div>
                <div class="hd_btn"><a target='_blank' href='http://www.artisan.com.tw/album/'><img alt="" src="/2010/btn_album-1.jpg" style="border:0" /></a></div>
                <div class="hd_btn"><a target='_blank' href='http://www.artisan.com.tw/mbook/default.asp'><img alt="" src="/2010/btn_gb-1.jpg" style="border:0" /></a></div>
                <div class="hd_btn"><a target='_blank' href='http://www.facebook.com/pages/kai-xuan-lu-xing-she-ju-jiang-lu-you/305852735217'><img alt="" src="/2010/btn_fb-1.jpg" style="border:0" /></a></div>
            </h3>
	    </div>