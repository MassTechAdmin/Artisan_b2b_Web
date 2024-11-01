<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Apply3.aspx.cs" Inherits="OLApply_Apply3" %>

<%@ Register Src="~/WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="~/WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="~/WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript">
        function showBlockUI() {
            $.blockUI({
                message: '<table><tr><td valign="middle" style="height:50px" >　<img src="loading0.gif" /></td><td valign="middle">　資料處理中...請稍候</td></tr></table>',
                css: { width: '250px', height: '50px', top: ($(window).height() / 2) - (50 / 2), left: ($(document).width() / 2) - (250 / 2) }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
            <div style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;">
                <div class="form_title">報名單計價</div>
                <div class="form_tool">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="90" align="center" valign="middle" bgcolor="#EEEEEE">團　　名</td>
                            <td colspan="5" align="left" valign="middle">
                                <asp:Label ID="Lbl_Grop_Numb" runat="server" />　<asp:Label ID="Lbl_Grop_Name" runat="server" />
                                <asp:TextBox ID="ppUsePoint" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="ppMustPoint" runat="server" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="ppRemainPoint" runat="server" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">旅客型別</td>
                            
                            <td width="119" align="center" valign="middle" bgcolor="#EEEEEE">單價</td>
                            <td width="178" align="center" valign="middle" bgcolor="#EEEEEE">人數</td>
                            <td width="183" align="center" valign="middle" bgcolor="#EEEEEE">小計</td>
                            <td width="178" align="center" valign="middle" bgcolor="#EEEEEE">紅利折扣</td>
                            <td width="200" align="center" valign="middle" bgcolor="#EEEEEE">銷售總額</td>
                            
                        </tr>
                        <tr>
                            <td align="center" valign="middle">大　　人</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_0" runat="server" /><asp:TextBox ID="txtSingle0" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_0" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_0" runat="server" /><asp:TextBox ID="txtPriceTotal0" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_0" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_0" runat="server" /></td>
                            
                        </tr>
                        <tr>
                            <td align="center" valign="middle">小孩佔床</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_1" runat="server" /><asp:TextBox ID="txtSingle1" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_1" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_1" runat="server" /><asp:TextBox ID="txtPriceTotal1" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_1" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_1" runat="server" /></td>
                            
                        </tr>
                        <tr>
                            <td align="center" valign="middle">小孩加床</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_2" runat="server" /><asp:TextBox ID="txtSingle2" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_2" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_2" runat="server" /><asp:TextBox ID="txtPriceTotal2" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_2" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_2" runat="server" /></td>
                            
                        </tr>
                        <tr>
                            <td align="center" valign="middle">小孩不佔床</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_3" runat="server" /><asp:TextBox ID="txtSingle3" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_3" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_3" runat="server" /><asp:TextBox ID="txtPriceTotal3" runat="server" Visible="False"></asp:TextBox></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_3" runat="server" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_3" runat="server" /></td>
                            
                        </tr>
                        <tr>
                            <td colspan="6" align="left" valign="middle">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" align="left" valign="middle">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6" align="left" valign="middle">此次使用的紅利積點為：<asp:Label ID="UsePoint" runat="server" Text="Label"></asp:Label>點</td>
                        </tr>
                        <tr>
                            <td colspan="6" align="left" valign="middle">你剩餘的紅利積點為：<asp:Label ID="RemainPoint" runat="server" Text="Label"></asp:Label>點</td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE" style="line-height:20px;">訂　　單<br />特殊需求</td>
                            <td colspan="3" align="left" valign="middle" style="padding-top:5px;padding-bottom:5px;">
                                <asp:Label ID="txt_Remark2" runat="server" /></td>
                            <td colspan="2" align="center" valign="middle" style="color: #FF0000;line-height:20px;">
                                　　<asp:Label ID="PT1" runat="server" Visible="false" /><br />
                                金額合計　　<asp:Label ID="PT2" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <div style="margin-bottom:40px;float:left;"><asp:Button ID="btn_previous" runat="server" Text="上一步" OnClick="btn_previous_Click" Height="37px"/></div>
                <div style="margin-bottom:40px;float:right;"><asp:Button ID="btn_next" runat="server" Text="確認報名" OnClick="btn_next_Click" Height="37px" OnClientClick="return (confirm('確定報名嗎?') ? showBlockUI() : false )" /></div>
            </div>
        </div>
        <asp:HiddenField ID="hid_Remark1" runat="server" />
        <asp:HiddenField ID="hid_kt1" runat="server" Value="0" />
        <asp:HiddenField ID="hid_kt2" runat="server" Value="0" />
        <asp:HiddenField ID="strCustNumb" runat="server" />
        <asp:HiddenField ID="strGroupNumb" runat="server" />
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
