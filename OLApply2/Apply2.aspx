<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Apply2.aspx.cs" Inherits="Apply2" %>

<%@ Register Src="~/WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="~/WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="~/WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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
    <style type="text/css">
        .txtbox
        {
            width: 40px;
            ime-mode: disabled;
            -moz-ime-mode: disabled;
            -ms-ime-mode: disabled;
            -webkit-ime-mode: disabled;
        }
    </style>
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
                    <div class="form_title">請輸入您的聯絡資料</div>
                    <div class="form_tool">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">客戶帳號</td>
                                <td width="199" align="left" valign="middle"><asp:Label ID="Lbl_ID" runat="server" /></td>
                                <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">公司電話</td>
                                <td width="297" align="left" valign="middle"><asp:Label ID="Lbl_TEL" runat="server" /></td>
                                <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">手機號碼</td>
                                <td width="197" align="left" valign="middle"><asp:Label ID="Lbl_Phone" runat="server" /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">會員姓名</td>
                                <td align="left" valign="middle"><asp:Label ID="Lbl_Name" runat="server" /></td>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">電子郵件</td>
                                <td colspan="3" align="left" valign="middle"><asp:Label ID="Lbl_EMail" runat="server" /></td>
                            </tr>
                        </table>
                    </div>

                    <div class="form_title">請輸入旅客人數</div>
                    <div class="form_tool">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                            <td width="90" align="center" valign="middle" bgcolor="#EEEEEE">業務代表</td>
                            <td width="858" align="left" valign="middle"><asp:Label ID="Lbl_Sales" runat="server" /></td>
                            </tr>
                             <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE" rowspan="2">紅利</td>
                                
                                <td align="left" valign="middle">本行程每人可扣除的紅利積點：<asp:Label ID="MustPoint" runat="server" />點
                                    <asp:TextBox ID="txtMustPoint" runat="server" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                
                                <td align="left" valign="middle">你目前剩餘的紅利積點：<asp:Label ID="RemainPoint" runat="server" />點
                                    <asp:TextBox ID="txtRemainPoint" runat="server" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">大　　人</td>
                                <td align="left" valign="middle"><asp:Label ID="litPay0" runat="server" />　
                                <asp:TextBox ID="TextBox0" runat="server" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" /> 位 
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="ConfirmUsePoint" runat="server" TextAlign="Left" OnCheckedChanged="ConfirmUsePoint_CheckedChanged"/>
                                    <asp:TextBox ID="txtConfirmUsePoint" runat="server" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">小孩佔床</td>
                                <td align="left" valign="middle"><asp:Label ID="litPay1" runat="server" />　
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" /> 位 </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">小孩加床</td>
                                <td align="left" valign="middle"><asp:Label ID="litPay2" runat="server" />　
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" /> 位 </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">小孩不佔床</td>
                                <td align="left" valign="middle"><asp:Label ID="litPay3" runat="server" />　
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" /> 位 </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">高北單程</td>
                                <td align="left" valign="middle">　
                                <asp:TextBox ID="Tai_Kao1" runat="server" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" /> 位 </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">高北來回</td>
                                <td align="left" valign="middle">　
                                <asp:TextBox ID="Tai_Kao2" runat="server" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" /> 位 </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">備註</td>
                                <td align="left" valign="middle"><label for="textfield6"></label>
                                <asp:TextBox ID="txt_Remark1" runat="server" Width="400px" MaxLength="50" />
       　                       <span style="color: #FF0000">備註欄位為註記訂單資訊，便於快速分辨訂單來源。(限制50個字)</span></td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle" bgcolor="#EEEEEE">訂　　單<br />特殊需求</td>
                                <td align="left" valign="middle" style="padding-top:20px;padding-bottom:20px;">
                                    <asp:TextBox ID="txt_Remark2" runat="server" Width="800px" MaxLength="200" TextMode="MultiLine" Rows="4" /><br />
                                    <div style="line-height:20px;">★請告知旅客，出國當日需攜帶有效出國證件前往基扯完成登機報到手續。<br />
                                    <span style="color: #FF0000">★未滿12歲兒童，開票時會先預定機上兒童餐，如不需預定兒童餐，請告知您當區的業務人員。</span></div></td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin-bottom:40px;float:left;"><asp:Button ID="btn_previous" runat="server" Text="上一步" OnClick="btn_previous_Click" Height="37px"/></div>
                    <div style="margin-bottom:40px;float:right;"><asp:Button ID="btn_next" runat="server" Text="下一步" OnClick="btn_next_Click" Height="37px" /></div>
                </div>
            </div>
            <uc2:Foot ID="Foot1" runat="server" />
        </div>
    </form>
</body>
</html>
