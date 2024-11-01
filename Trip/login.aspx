<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Trip_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>凱旋旅行社(巨匠旅遊)-行程上架登入</title>
    <link rel="stylesheet" type="text/css" href="../image/stylesheet.css" />
    <meta name="robots" content="noindex" />
    <meta name="googlebot" content="noindex" />
<style type="text/css">
<!--
#admin_login_tool {
	background-color: #EEEEEE;
	text-align: center;
	width: 350px;
	margin-right: auto;
	margin-left: auto;
	padding: 20px;
	line-height: 28px;
	font-size: 15px;
	margin-top: 20px;
}
.style1 {
	background-color: #666666;
	color: #FFFFFF;
	padding: 10px;
	font-size: 16px;
	font-weight: bold;
	line-height: 20px;
}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="admin_login_tool">
        &nbsp;<table>
            <tr>
                <td colspan="2">
                    <div align="center" class="style1">使用者登入</div></td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right">
                    帳號：</td>
                <td style="width: 250px; text-align: left">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right">
                    密碼：</td>
                <td style="width: 250px; text-align: left">
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right">
                    驗證碼：</td>
                <td style="width: 250px; text-align: left">
                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="5" Width="61px"></asp:TextBox>                    
                <img id="MzImgExpPwd" alt="請輸入驗證碼,看不清楚？請重新點擊" height="30" name="MzImgExpPwd" onclick="javascript:document.getElementById('MzImgExpPwd').src='gif.aspx?temp='+ (new Date().getTime().toString(36));  return false"
                        src="gif.aspx" width="100" /><br />
                    看不清楚？請重新點擊圖<br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 250px; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 250px; text-align: left">
                    <asp:Button ID="Button1" runat="server" Text="登入" OnClick="Button1_Click" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
