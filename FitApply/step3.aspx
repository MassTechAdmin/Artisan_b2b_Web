<%@ Page Language="C#" AutoEventWireup="true" CodeFile="step3.aspx.cs" Inherits="OLApply_step3" %>
<%@ Register Src="~/WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>
<%@ Register Src="~/WebControl/Foot_17.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/WebControl/Header_Menu_17.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>凱旋旅行社(巨匠旅遊)</title>
    <link href="/OLApply/20151124.css" rel="stylesheet" type="text/css" />
	<link href="/css/web_17.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.12.4.min.js" type="text/javascript" ></script>
    	<style type="text/css">
        #content {
			width: 1400px;
			height: auto;
			float: left;
			margin-top: 100px;
			background: #fff;
			border: solid 1px #dcdcdc;
			padding: 10px;
			box-sizing: border-box;
		}
    </style>
</head>
<body onkeydown="if (event.keyCode == 13){return false;}">
  <form id="form1" runat="server">
    <div id="wrapper">
        <nav>
            <uc1:Header ID="Header1" runat="server" />
        </nav>
        <div id="content">
            <!-----修改地區----->
            <div style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;margin-top:30px;">

                <div id="stepList_tool">
                    <div class="stepList01">1.填寫旅客人數</div>
                    <div class="stepList01">2.填寫訂購資料</div>
                    <div class="stepList01">3.填寫旅客名單</div>
                    <div class="stepList02">4.完成報名</div>
                </div>

                <div style="text-align:left;border:solid 1px #CCC;font-size:13px;line-height:22px;letter-spacing:1px;padding:20px 200px 20px 200px;margin-top:30px;margin-bottom:30px;">
                    親愛的會員 <span style="color: #06C"><asp:Label ID="Lab_name" runat="server"/></span> 您好：<br />
                    <asp:Label ID="Lab_DL1" runat="server"/>
                    感謝您使用巨匠旅遊網站，請稍候至您的電子郵件信箱，收取訂購函。 <br />
                    同時您也可以將訂單
                    <span style="background:#EE0000;padding:2px 5px 2px 5px;width:24px;color:#FFF;"><a href="#" style="text-decoration:none;color:#FFF;">列印存底</a></span>
                    保留
                </div>

                <div class="form_title">報名單摘要</div>
                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="105" align="center" valign="middle" bgcolor="#EEEEEE">訂單編號</td>
                            <td align="left" valign="middle"><span style="color: #FF0000"><asp:Label ID="Lab_No" runat="server"/></span></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">金額總計</td>
                            <td align="left" valign="middle"><asp:Label ID="Lab_Money" runat="server"/></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">服務單位</td>
                            <td align="left" valign="middle"><asp:Label ID="Lab_comp" runat="server"/></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">業務人員</td>
                            <td align="left" valign="middle"><asp:Label ID="Lab_sale" runat="server"/></td>
                        </tr>
                    </table>
                </div>

                <div class="form_title">報名單計價</div>
                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="105" align="center" valign="middle" bgcolor="#EEEEEE">團　名</td>
                            <td colspan="4" align="left" valign="middle" style="line-height:16px;">
                                <asp:Label ID="Lab_GroupName" runat="server"/>
                            </td>
                        </tr>
                        <asp:Literal runat="server" ID="litCostList" />
                    </table>
                </div>

                <div style="margin-bottom:40px;">
                    <%--<a href="#"><input name="button" type="button" id="button" value="列印存底" /></a> 
                    <a href="10.html"><input name="button" type="button" id="button" value="查詢我的訂單" /></a>--%>
                    <asp:Button ID="Button1" runat="server" Text="回首頁" PostBackUrl="http://www.artisan.com.tw" Height="42px" Width="80px" />
                </div>
                <!-----修改地區 end----->  
            </div>
            <uc2:Foot ID="Foot1" runat="server" />
        </div>

        <asp:HiddenField ID="Hid_Phone" runat="server" />
        <asp:HiddenField ID="Hid_GropName" runat="server" />
        <asp:HiddenField ID="Hid_BookPax" runat="server" />
        <asp:HiddenField ID="Hid_RegStatus" runat="server" />
        <asp:HiddenField ID="Hid_ConnPhone" runat="server" />
        <asp:HiddenField ID="Hid_LineCode" runat="server" />
        <asp:HiddenField ID="Hid_LineCode_Agent" runat="server" />
    </div>
  </form>
</body>
</html>
