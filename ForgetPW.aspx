<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPW.aspx.cs" Inherits="ForgetPW" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_sale.css" rel="stylesheet" type="text/css" />

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

            <div id="forget_pw_content">
                    <div class="style3">請輸入以下資料，讓我們幫助您取得密碼：</div>
                    <div class="forget_pw_Captcha_title" id="forget_pw_IDnumber">身分證字號： <asp:TextBox ID="TextBox1" runat="server" Width="200px" />
                    </div>
                    <div class="forget_pw_Captcha_title" id="forget_pw_email">電&nbsp;子&nbsp;郵&nbsp;件： <asp:TextBox ID="TextBox2" runat="server" Width="200px" />
                    </div>
<div class="member_pw_button">
                    <asp:Button ID="Button1" runat="server" Text="送出" OnClick="Button1_Click" />  
                    <asp:Button ID="Button2" runat="server" Text="取消" OnClick="Button2_Click" Visible="false" />
                </div></div>

            <div id="banner_tool">
			    <div id="carousel" class="es-carousel-wrapper">
				    <div class="es-carousel">
                        <asp:Literal ID="litAds" runat="server"></asp:Literal>
				    </div>
			    </div>
            </div>

            <div id="sale">
                <div class="sale_title">
                    <asp:Literal ID="litSale_News_Items" runat="server"></asp:Literal>
                </div>
                <div class="tab_container">
                  <asp:Literal ID="litSale_News" runat="server"></asp:Literal>
                </div>
              </div>
              
              <div id="special_selection">
            <div class="special_selection_title"></div>
            <div id="special_banner">
                <script type="text/javascript">
                AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','235','height','270','src','http://www.artisan.com.tw/images/special-flash','quality','high','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','http://www.artisan.com.tw/images/special-flash' ); //end AC code
                </script>
                <noscript>
                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0" width="235" height="270">
                        <param name="movie" value="images/special-flash.swf" />
                        <param name="quality" value="high" />
                        <embed src="images/special-flash.swf" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" width="235" height="270"></embed>
                    </object>
                </noscript></div>
            <div id="special_buttom"><img src="images/index/special_icon02.jpg" align="absmiddle" />&nbsp;&nbsp;<img src="images/index/special_icon01.jpg" align="absmiddle" />&nbsp;&nbsp;<img src="images/index/special_icon02.jpg" align="absmiddle" />&nbsp;&nbsp;<img src="images/index/special_icon02.jpg" align="absmiddle" />&nbsp;&nbsp;<img src="images/index/special_icon02.jpg" align="absmiddle" /></div>
            </div>

        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
	<script type="text/javascript" src="js/jquery.elastislide.js"></script>
	<script type="text/javascript">
		$('#carousel').elastislide({
			imageW : 221,
			minItems : 4,
			onClick : true
		});
	</script>
    </form>
</body>
</html>
