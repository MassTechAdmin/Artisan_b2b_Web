<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Apply4.aspx.cs" Inherits="OLApply_Apply4" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
            <div style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;">
                <div class="form_title">報名單摘要</div>
                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">報名單號</td>
                            <td width="155" align="left" valign="middle"><asp:Label ID="Lal_Numb" runat="server" /></td>
                            <td width="85" align="left" valign="middle" bgcolor="#EEE">團　　號</td>
                            <td width="623" align="left" valign="middle"><asp:Label ID="Lbl_Grop_Numb" runat="server" />　出發：<asp:Label ID="Lbl_Grop_Depa" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">訂  購  人</td>
                            <td align="left" valign="middle"><asp:Label ID="Lbl_Name" runat="server" /></td>
                            <td align="left" valign="middle" bgcolor="#EEE">團　　名</td>
                            <td align="left" valign="middle"><asp:Label ID="Lbl_Grop_Name" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">報名狀態</td>
                            <td align="left" valign="middle"><asp:Label ID="Lbl_Check" runat="server" /></td>
                            <td colspan="2" align="left" valign="middle" style="font-size:12px;line-height:16px;letter-spacing:0px;">
                                <asp:Literal ID="msg_tk" runat="server">
                                    <span style="color: #ef8406">(已取得機位，請於訂金DL之前完成訂金付款，業務確認訂單收取訂金後，此訂單才成立)</span><br />
                                    <span style="color: #FF0000">因須配合航空公司作業，將不定時入開票名單，故請報名後即刻提供正確的旅客中、英文姓名以免名單上鎖，造成作業困擾。</span>
                                </asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">報名人數</td>
                            <td colspan="3" align="left" valign="middle">
                                大人：<asp:Label ID="R0" runat="server" Text="0" />　　
                                小孩佔床：<asp:Label ID="R1" runat="server" Text="0" />　　
                                小孩加床：<asp:Label ID="R2" runat="server" Text="0" />　　
                                小孩不佔床：<asp:Label ID="R3" runat="server" Text="0" />　　
                                <%--嬰兒：０　　
                                Join：0--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">報名訂金</td>
                            <td colspan="3" align="left" valign="middle"><asp:Label ID="Lbl_BookPax" runat="server" />　　　<asp:Label ID="Lbl_DL1" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">訂單需求</td>
                            <td colspan="3" align="left" valign="middle" style="color: #FF0000"><asp:Label ID="Lbl_DL2" runat="server" /></td>
                        </tr>
                    </table>
                </div>


                <div class="form_title">報名單計價
                    <asp:TextBox ID="ppUsePoint" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="ppMustPoint" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="ppRemainPoint" runat="server" Visible="False"></asp:TextBox>

                </div>
                <div class="form_tool">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="90" align="center" valign="middle" bgcolor="#EEEEEE">旅客型別</td>
                            
                            <td width="167" align="center" valign="middle">單價</td>
                            <td width="167" align="center" valign="middle">人數</td>
                            <td width="168" align="center" valign="middle">小計</td>
                            <td width="167" align="center" valign="middle">紅利折扣</td>
                            
                            
                            <td width="189" align="center" valign="middle">銷售總額</td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">大　　人</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_0" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_0" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_0" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_0" runat="server" Text="0" /></td>
                            
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_0" runat="server" Text="0" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">小孩佔床</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_1" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_1" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_1" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_1" runat="server" Text="0" /></td>
                            
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_1" runat="server" Text="0" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">小孩加床</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_2" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_2" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_2" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_2" runat="server" Text="0" /></td>
                            
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_2" runat="server" Text="0" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">小孩不佔床</td>
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Single_3" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_People_3" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Total_3" runat="server" Text="0" /></td>
                            <td align="center" valign="middle"><asp:Label ID="Price_Discount_3" runat="server" Text="0" /></td>
                            
                            
                            <td align="center" valign="middle"><asp:Label ID="Price_Show_3" runat="server" Text="0" /></td>
                        </tr>
                        <%--<tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">嬰兒</td>
                            <td align="center" valign="middle">3,000+0+0</td>
                            <td align="center" valign="middle">3,000</td>
                            <td align="center" valign="middle">&nbsp;</td>
                            <td align="center" valign="middle">０</td>
                            <td align="center" valign="middle">０</td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">JoinTour<br /></td>
                            <td align="center" valign="middle"><label for="textfield6">無此報價</label></td>
                            <td align="center" valign="middle">無此報價</td>
                            <td align="center" valign="middle">&nbsp;</td>
                            <td align="center" valign="middle">０</td>
                            <td align="center" valign="middle">無此報價</td>
                        </tr>--%>
                        <tr>
                            <td colspan="4" align="center" valign="middle">&nbsp;</td>
                            <td colspan="2" align="center" valign="middle" style="padding-top:20px;padding-bottom:20px;"><span style="color: #FF0000;line-height:20px;">　<asp:Label ID="PT1" runat="server" Visible="false" /><br />
                            金額合計　　<asp:Label ID="PT2" runat="server" /></span></td>
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


                <div style="margin-bottom:40px;float:right;">
                    <%--<input name="button" type="button" id="button" value="列印存底" />
　                  <input name="button" type="button" id="button" value="MALL存底" />
　                  <a href="05.html"><input name="button" type="button" id="button" value="報名單管理" /></a>--%>
                    <asp:Button ID="Button1" runat="server" Text="繼續下單" Width="93" Height="42" PostBackUrl="~/index.aspx" />
                </div>


            </div>
        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
        <asp:HiddenField ID="hidcomp_name" runat="server" />
        <asp:HiddenField ID="hidConnPhone" runat="server" />
        <asp:HiddenField ID="hidReg_Status" runat="server" />
        <asp:HiddenField ID="hidBookPax" runat="server" />
        <asp:HiddenField ID="hidDepa" runat="server" />
        <asp:HiddenField ID="hidSaleCode" runat="server" />
    </form>
</body>
</html>
