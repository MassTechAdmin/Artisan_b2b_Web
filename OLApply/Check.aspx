<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Check.aspx.cs" Inherits="OLApply_Check" %>

<%@ Register Src="~/WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="~/WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="~/WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    
    <link href="../../css/web.css" rel="stylesheet" type="text/css" />
    <link href="../../css/color.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../../js/jquery-artisan-130221.js"></script>
    <link rel="stylesheet" type="text/css" href="../../css/elastislide.css" />
    <link href="20151124.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/Artisan_SubMenu.js"></script>
    <script type="text/javascript">
        window.history.forward(1);
    </script>
    <script type="text/javascript">
        function BlockNumber(e) {
            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            reg = /[0-9]/;
            return reg.test(keychar);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <uc1:Header ID="Header1" runat="server" />
            <div id="content">
                <div style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;">
                    <div class="form_tool">
                        <div class="form_title">審核資料</div>
                        <div class="form_notice">
                            <p>為保障您的權益，申請前請詳細閱讀下列事項： <br />
                            1.同業會員需為旅遊從業人員。 <br />
                            2.同業會員註冊時，應提供完整詳實且正確的個人資料，如有變更時，請於線上更新或通知貴公司所屬巨匠業務人員。 <br />
                            3.巨匠Ｂ２Ｂ同業網保有審核其身分之權益，決定發予申請人帳號及密碼之權利。 <br />
                            4.本同業會員申請加入屬免費性質，巨匠Ｂ２Ｂ同業網保留隨時更改或停止所提供各項服務之內容，或終止任一同業會員帳戶服務之權利，且無需事先告知同業會員，<br />
        　                            同業會員不得因此要求任何補償或賠償。 <br />
                            5.為維護同業會員自身權益，同業會員應妥善保管帳號及密碼，請勿洩露或提供予他人使用，並為帳號及密碼進入系統後，所從事之一切活動負責。 <br />
                            6.同業會員所提供之個人資料內容，巨匠Ｂ２Ｂ同業網會遵守個人資料保護法之規範。對於同業會員所登錄之個人資料，同業會員同意巨匠Ｂ２Ｂ同業網及關係企業或<br />
        　                            合作對象，得於合理合法之範圍內蒐集、傳遞及使用此資料，以作為統計、研究或提供其他服務。 <br />
                            7.巨匠Ｂ２Ｂ同業網保留隨時更改本同業會員規範之權利，且將更改之內容公告於巨匠Ｂ２Ｂ同業網會員中心，不另作同業會員個別通知，如同業會員繼續使用本網站<br />
        　                            服務，即視為同意新修正之同業會員規範。 <br />
                            8.同業加入巨匠Ｂ２Ｂ同業網成為會員後，網站會主動寄送會員電子報及各項業務活動訊息。 <br />
                            9.其他未盡事宜，均依照中華民國法律規定及網路規範辦理，同業會員同意若因本同業會員規範條款有所爭議或糾紛，以台灣台北地方法院為第一審管轄法院。 <br />
                            </p>
                        </div>
                        <div style="font-size: 13px; text-align:center;"><asp:CheckBox ID="CheckBox1" runat="server" />我同意以上條款 。</div>
                    </div>
      
                    <div class="form_title">請確認您的B2B帳號資料</div>
                    <div class="form_tool">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="110" align="center" valign="middle" bgcolor="#EEEEEE">任職旅行社</td>
                                <td width="255" align="left" valign="middle"><asp:Label ID="Lbl_Comp" runat="server" /></td>
                                <td width="296" align="center" valign="middle" bgcolor="#EEEEEE">統一編號</td>
                                <td width="287" align="left" valign="middle"><asp:Label ID="Lbl_Comp_No" runat="server" /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">帳號(身分證ID)</td>
                                <td align="left" valign="middle"><asp:TextBox ID="Lbl_Account" runat="server" MaxLength="10" /></td>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">密碼(04-15碼英文或阿拉伯數字)</td>
                                <td align="left" valign="middle"><asp:TextBox ID="Lbl_PassWord" runat="server" MaxLength="15" /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">中文姓名</td>
                                <td align="left" valign="middle"><asp:TextBox ID="Lbl_Name" runat="server" /></td>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">性別</td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                                        <asp:ListItem Value="M">男</asp:ListItem>
                                        <asp:ListItem Value="F">女</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">出生日期(西元)</td>
                                <td align="left" valign="middle"><asp:TextBox ID="Lbl_Birthday1" runat="server" Width="40" MaxLength="4" OnKeyPress="return BlockNumber(event);" onpaste="return false" /> 年 
                                                                 <asp:TextBox ID="Lbl_Birthday2" runat="server" Width="30" MaxLength="2" OnKeyPress="return BlockNumber(event);" onpaste="return false" /> 月 
                                                                 <asp:TextBox ID="Lbl_Birthday3" runat="server" Width="30" MaxLength="2" OnKeyPress="return BlockNumber(event);" onpaste="return false" /> 日 </td>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">電子郵件</td>
                                <td align="left" valign="middle"><asp:TextBox ID="Lbl_EMail" runat="server" /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">公司電話</td>
                                <td align="left" valign="middle">( <asp:TextBox ID="Lbl_Tel1" runat="server" Width="23" MaxLength="3" OnKeyPress="return BlockNumber(event);" onpaste="return false" /> ) - 
                                                                  <asp:TextBox ID="Lbl_Tel2" runat="server" Width="70" MaxLength="8" OnKeyPress="return BlockNumber(event);" onpaste="return false" /> 分機
                                                                  <asp:TextBox ID="Lbl_Tel3" runat="server" Width="45" MaxLength="6" OnKeyPress="return BlockNumber(event);" onpaste="return false" /></td>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">行動電話</td>
                                <td align="left" valign="middle"><asp:TextBox ID="Lbl_Phone" runat="server" MaxLength="10" OnKeyPress="return BlockNumber(event);" onpaste="return false" /></td>
                            </tr>
                        </table>
                    </div>
                    <div style="font-size: 14px">若任職旅行社或統一編號資料有誤，請<a href="http://b2b.artisan.com.tw/A_Join.aspx"><span style="color:red;">按此</span></a>重新申請</div><br />
                    <div style="font-size: 13px"><asp:CheckBox ID="CheckBox2" runat="server" />我已確認資料為正確。</div><br />
                    <div style="margin-bottom:40px;"><asp:Button ID="Button1" runat="server" Text="下一步" Height="41" Width="62" OnClick="button_Click" /></div>
                </div>
            </div>
            <uc2:Foot ID="Foot1" runat="server" />
        </div>
    </form>

    <script type="text/javascript">
    <!--
        var TabbedPanels1 = new Spry.Widget.TabbedPanels("TabbedPanels1");
        //-->
    </script>

</body>
</html>
