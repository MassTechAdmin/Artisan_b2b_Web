<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_list2.aspx.cs" Inherits="order_list2" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_sale.css" rel="stylesheet" type="text/css" />
    <link href="20151124.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-artisan-130221.js"></script>
    <link rel="stylesheet" type="text/css" href="css/elastislide.css" />
    <script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Artisan_SubMenu.js"></script>
</head>

<body>
     <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:header ID="Header1" runat="server" />
        <div id="content">
        <div style="text-align: center; width: 1050px; margin-left: auto; margin-right: auto;">
<div class="form_title">報名單摘要</div>
<div class="form_tool">
<table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">報名單號</td>
    <td width="240" align="left" valign="middle">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      </td>
    <td width="85" align="left" valign="middle" bgcolor="#EEEEEE">團　　號</td>
    <td width="620" align="left" valign="middle">
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        　出發：<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
      </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">訂  購  人</td>
    <td align="left" valign="middle">
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    </td>
    <td align="left" valign="middle" bgcolor="#EEEEEE">團　　名</td>
    <td align="left" valign="middle">
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
      </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">報名狀態</td>
    <td align="left" valign="middle">
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Literal ID="litStatus" runat="server"></asp:Literal>
      </td>
    <td colspan="2" align="left" valign="middle" style="font-size:12px;line-height:16px;letter-spacing:0px;"><span style="color: #ef8406">(已取得機位，請於訂金DL之前完成訂金付款，業務確認訂單收取訂金後，此訂單才成立)</span><br />
      <span style="color: #FF0000">因須配合航空公司作業，將不定時入開票名單，故請報名後即刻提供正確的旅客中、英文姓名以免名單上鎖，造成作業困擾。</span></td>
    </tr>
    <tr>
        <td align="center" valign="middle" bgcolor="#EEEEEE">報名人數</td>
        <td colspan="3" align="left" valign="middle">大人：<asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
        　　      佔床：<asp:Label ID="Label38" runat="server" Text="Label"></asp:Label>
        　　      加床：<asp:Label ID="Label39" runat="server" Text="Label"></asp:Label>
        　　      不佔：<asp:Label ID="Label40" runat="server" Text="Label"></asp:Label>
        　　      嬰兒：<asp:Label ID="Label41" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">報名訂金</td>
    <td colspan="3" align="left" valign="middle">
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
        　席 *
        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
&nbsp;= 
        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
        　　　付款DL：<asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
      </td>
    </tr>
    <tr>
        <td align="center" valign="middle" bgcolor="#EEEEEE">報名狀態</td>
        <td colspan="3" align="left" valign="middle">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" valign="middle" bgcolor="#EEEEEE">訂單需求</td>
        <td colspan="3" align="left" valign="middle" style="color: #FF0000">請於<asp:Label ID="Label42" runat="server" Text="Label"></asp:Label>
            之前完成訂金付款，逾時未繳款恕無法為您保留團體機位。</td>
    </tr>
</table>
</div>


<div class="form_title">報名單計價</div>
<div class="form_tool">
<table border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="90" align="center" valign="middle" bgcolor="#EEEEEE">旅客型別</td>
    <td width="189" align="center" valign="middle">團體售價+簽證費+稅險</td>
    <td width="167" align="center" valign="middle">單價</td>
    <td width="167" align="center" valign="middle">折扣</td>
    <td width="167" align="center" valign="middle">人數</td>
    <td width="168" align="center" valign="middle">銷售金額</td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">大　　人</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label12" runat="server" Text="0"></asp:Label>
        +0+0</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label13" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">０</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label14" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">
        <asp:Label ID="Label15" runat="server" Text="0"></asp:Label>
      </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">小孩佔床</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label16" runat="server" Text="0"></asp:Label>
        +0+0</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label17" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">０</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label18" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">
        <asp:Label ID="Label19" runat="server" Text="0"></asp:Label>
      </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">小孩加床</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label20" runat="server" Text="0"></asp:Label>
        +0+0</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label21" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">０</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label22" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">
        <asp:Label ID="Label23" runat="server" Text="0"></asp:Label>
      </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE">小孩不佔床</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label24" runat="server" Text="0"></asp:Label>
        +0+0</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label25" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">０</td>
    <td align="center" valign="middle">
        <asp:Label ID="Label26" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle">
        <asp:Label ID="Label27" runat="server" Text="0"></asp:Label>
      </td>
    </tr>
  <tr>
    <td align="center" valign="middle" bgcolor="#EEEEEE" class="auto-style1">嬰兒</td>
    <td align="center" valign="middle" class="auto-style1">
        <asp:Label ID="Label28" runat="server" Text="0"></asp:Label>
        +0+0</td>
    <td align="center" valign="middle" class="auto-style1">
        <asp:Label ID="Label29" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle" class="auto-style1"></td>
    <td align="center" valign="middle" class="auto-style1">
        <asp:Label ID="Label30" runat="server" Text="0"></asp:Label>
      </td>
    <td align="center" valign="middle" class="auto-style1">
        <asp:Label ID="Label31" runat="server" Text="0"></asp:Label>
      </td>
    </tr>

  <tr>
    <td colspan="4" align="center" valign="middle">&nbsp;</td>
    <td colspan="2" align="center" valign="middle" style="padding-top:20px;padding-bottom:20px;"><span style="color: #FF0000;line-height:20px;">
        <br />
金額合計　　<asp:Label ID="Label37" runat="server" Text="Label"></asp:Label>
        </span></td>
    </tr>
</table>
</div>


  <div class="form_title">洽詢服務窗口</div>
                <div class="form_tool">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">業務代表</td>
                            <td width="210" align="center" valign="middle"><asp:Label ID="Lbl_Sales" runat="server" /></td>
                            <td width="310" align="left" valign="middle">公司電話：<asp:Label ID="Lbl_TEL" runat="server" /></td>
                            <td width="343" align="left" valign="middle">手機號碼：<asp:Label ID="Lbl_Phone" runat="server" /></td>
                        </tr>
                    </table>
                </div>


        <div style="margin-bottom:40px;float:right;">&nbsp;
            　<asp:Button ID="Button2" runat="server" Text="mail存底" OnClick="Button2_Click" Visible="false" />
            <asp:Button ID="Button1" runat="server" Text="回上一頁" OnClick="Button1_Click" />　
        </div>
    </form>
</body>
</html>

