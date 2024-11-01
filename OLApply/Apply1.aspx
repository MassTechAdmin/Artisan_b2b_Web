<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Apply1.aspx.cs" Inherits="OLApply_Apply1" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
            <div style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;">
                <div class="form_title">您選擇的旅遊行程</div>
                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">團　　名</td>
                            <td width="863" align="left" valign="middle"><asp:Label ID="Lbl_Grop_Name" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">團　　號</td>
                            <td align="left" valign="middle"><asp:Label ID="Lbl_Grop_Numb" runat="server" />　　出發：<asp:Label ID="Lbl_Grop_Depa" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">團體說明</td>
                            <td align="left" valign="middle"></td>
                        </tr>
                    </table>
                </div>


            <div class="form_title">團體售價說明</div>
            <div class="form_tool">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="90" align="center" valign="middle" bgcolor="#EEEEEE">同業售價</td>
                        <td width="267" align="left" valign="middle"><asp:Label ID="Lbl_Agent_Tour" runat="server" /></td>
                        <td width="190" align="left" valign="middle" bgcolor="#EEEEEE">直客售價</td>
                        <td width="401" align="left" valign="middle"><asp:Label ID="Lbl_Grop_Tour" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" bgcolor="#EEEEEE">簽  證  費</td>
                        <td align="left" valign="middle"><asp:Label ID="Lbl_Grop_Visa" runat="server" /></td>
                        <td align="left" valign="middle" bgcolor="#EEEEEE">小孩佔床(同業價)</td>
                        <td align="left" valign="middle" style="color: #e58718"><asp:Label ID="litPay1" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" bgcolor="#EEEEEE">稅　　險</td>
                        <td align="left" valign="middle"><asp:Label ID="Lbl_Grop_Tax" runat="server" /></td>
                        <td align="left" valign="middle" bgcolor="#EEEEEE">小孩加床(同業價)</td>
                        <td align="left" valign="middle" style="color: #e58718"><asp:Label ID="litPay2" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" bgcolor="#EEEEEE">訂　　金</td>
                        <td align="left" valign="middle" style="color: #00aeff; font-weight: bold;">每席<asp:Label ID="Lbl_Down_Payment" runat="server" />元</td>
                        <td align="left" valign="middle" bgcolor="#EEEEEE">小孩不佔床(同業價)</td>
                        <td align="left" valign="middle" style="color: #e58718"><asp:Label ID="litPay3" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" bgcolor="#EEEEEE">報價說明</td>
                        <td colspan="3" align="left" valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" bgcolor="#EEEEEE">簽證說明</td>
                        <td colspan="3" align="left" valign="middle"><asp:Label ID="Lbl_Grop_Visa_Info" runat="server" />
                            (持台灣簽發之中華民國護照且護照內須有身分證統一編號及護照效期從預定回國日算起尚六個月以上效期</td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" bgcolor="#EEEEEE">北高接駁</td>
                        <td colspan="3" align="left" valign="middle">北高接駁純為服務性質，本公司盡最大努力協助定位，但無法保證一定有機位</td>
                    </tr>
                </table>
            </div>

            <div class="form_title">報名團體須知</div>
            <div class="form_tool">
                <div class="form_notice">
                    1.請在報名時注意訂金D/L時間，並於D/L內繳付訂金或團費 <br />
                    2.當團行程如有特殊優惠折扣(例如:預購省3000)，業務人員會主動幫您扣除，並於收訂金時與您確認價格。
                    <br />
                    3.匯率、燃料稅、團體票價等調整，將導致成本變動，尚未收到訂金或團費前，本公司保留調整團費權利。
                    <br />
                    <span style="color: #FF0000; font-weight: bold;">4.當本團使用外籍航空時，機票一經開立，便無法辦理退票，若開立後取消者，依雙方所簽合約辦理。 </span><br />
                    <span style="color: #FF0000">5.加班機、包機包銷為暫定時刻表，如因各方政府未與批准或班機調度等因素取消，本公司會另行通知，貴公司不得異議或要求任何賠償。</span><br />
                    6.護照自帶機場者，請自行確認護照有效期限須再行程回國日算起六個月以上，並自行確認該行程有效之簽證。    <br />
                    7.團體旅遊或團體自由行之最低出團人數依本公司規定為準，未達最低出團人數時，本公司得通知取消。
                    <br />
                    8.繳付訂金或團費同時雙方須簽訂旅遊契約書，若有變更或取消，則依旅遊契約書內容辦理。<br />
                    9.因需配合航空公司不定期入開票名單等作業，請於報名時提供正確的旅客中、英文姓名，以免名單上鎖，造成作業困擾。<br />
                    <span style="color: #FF0000">10.未滿12歲兒童，開票時會先預訂機上兒童餐，如不需預訂兒童餐，請告知您當區的業務人員。 </span><br />
                    11.報名本行程之旅客若為年滿70歲以上且無家人或友人同行者，為考量該旅客之旅遊安全並顧及其他團員的旅遊權益，本公司恕不接受報名，若有不便之<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;，處尚祈見諒。報名後始知悉者，視為報名無效，貴公司不得異議要求任何賠償。<br />
                    12.本行程報價只適用於本國旅客(即持中華民國護照者)。持他國護照之旅客，須於報名時知會業務人員另行報價，
                    並請自行查明持他國護照或雙重國籍之<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;相關規定。 <br />
                    <span style="color: #FF0000">13.凡包機包銷貨一班的系列團，若以團體機位加價升等為豪華經濟艙或商務艙，其升等後之餐食、行李重量及機場貴賓室使用等相關規定與一般FIT不同，<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;請先自行查明相關規定。</span><br />
                    14.本公司保留接受報名與否之權利。 
                </div>

            </div>

            <div style="font-size: 13px"><asp:CheckBox ID="CheckBox1" runat="server" />我已閱讀「報名須知」，並同意其內容。</div>

            <div style="margin-bottom:40px;"><asp:Button ID="button" runat="server" Text="下一步" OnClick="button_Click" /></div>

            </div>
        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>

<%--<script type="text/javascript">

    var TabbedPanels1 = new Spry.Widget.TabbedPanels("TabbedPanels1");

</script>--%>

</body>
</html>
