<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="step1.aspx.cs" Inherits="step1" %>

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

	<%--輪播大圖--%>
    <script src="/js/flash_17.js" type="text/javascript"></script>
    <%--特效JS--%>
    <script src="/js/HeaderMenu17.js" type="text/javascript"></script>

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
                <div class="stepList02">1.填寫旅客人數</div>
                <div class="stepList01">2.填寫訂購資料</div>
                <div class="stepList01">3.填寫旅客名單</div>
                <div class="stepList01">4.完成報名</div>
            </div>

            <div class="form_title">您選擇的旅遊行程</div>
            <div class="form_tool">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">團　名</td>
                        <td colspan="5" align="left" valign="middle"><asp:Label ID="Lbl_Grop_Name" runat="server" /></td>
                    </tr>
                </table>
            </div>
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
            <asp:Panel ID="Panel1" runat="server">
                <div class="form_title">請輸入旅客人數</div>
                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">服務人員</td>
                            <td colspan="6" align="left" valign="middle">
                                <asp:Label ID="Lbl_Sales" runat="server" />
                            </td>
                        </tr>
                        <tr>
                                    <td rowspan="6" align="center" valign="middle" bgcolor="#EEEEEE">旅客人數</td>
                                    <td width="120" align="center" valign="middle">型別</td>
                                    <td width="120" align="center" valign="middle">人數</td>
                                    <td width="263" align="center" valign="middle">型別說明</td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">大人</td>
                                    <td align="center" valign="middle">
                                        <asp:TextBox runat="server" ID="txt1" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" Type="Number" min="0"/>
                                    </td>
                                    <td align="center" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">小孩佔床</td>
                                    <td align="center" valign="middle">
                                        <asp:TextBox runat="server" ID="txt2" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" Type="Number" min="0"/>
                                    </td>
                                    <td align="center" valign="middle"><span style="color: #FF0000;">返國當天年滿2~12歲(不含)</span></td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">小孩不佔床</td>
                                    <td align="center" valign="middle">
                                        <asp:TextBox runat="server" ID="txt3" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" Type="Number" min="0"/>
                                    </td>
                                    <td align="center" valign="middle"><span style="color: #FF0000;">返國當天年滿2~12歲(不含)</span></td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">小孩加床</td>
                                    <td align="center" valign="middle">
                                        <asp:TextBox runat="server" ID="txt4" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" Type="Number" min="0"/>
                                    </td>
                                    <td align="center" valign="middle"><span style="color: #FF0000;">返國當天年滿2~12歲(不含)</span></td>
                                </tr>
                                <tr>
                                    <td align="center" valign="middle">嬰兒</td>
                                    <td align="center" valign="middle">
                                        <asp:TextBox runat="server" ID="txt5" CssClass="txtbox" OnKeyPress="return BlockNumber(event);" MaxLength="2" onpaste="return false" Type="Number" min="0"/>
                                    </td>
                                    <td align="center" valign="middle"><span style="color: #FF0000;">返國當天年滿2歲(不含)</span></td>
                                </tr>
                        <asp:Literal runat="server" ID="litCostList" />
                    </table>
                </div>
            </asp:Panel>
            

            <div class="form_title">國外旅遊定型化契約書</div>
            <div class="form_tool">
                <div class="form_notice">
                <label for="textarea"></label>
                <textarea name="textarea" id="textarea" cols="132" rows="10" style="width:945px;">國外旅遊定型化契約書範本
                (中華民國93年11月5日觀業字第0930030216號函修正)
                立契約書人
                （本契約審閱期間1日，    年    月    日由甲方攜回審閱）　
                （旅客姓名）　　　　　　　　　　　        （以下稱甲方）
                （旅行社名稱）　　　　　　　　　　        （以下稱乙方）
                第一條（國外旅遊之意義）
                本契約所謂國外旅遊，係指到中華民國疆域以外其他國家或地區旅遊。
                赴中國大陸旅行者，準用本旅遊契約之規定。
                第二條（適用之範圍及順序）
                甲乙雙方關於本旅遊之權利義務，依本契約條款之約定定之；本契約中未約定者，適用中華民國有關法令之規定。附件、廣告亦為本契約之一部。
                第三條（旅遊團名稱及預定旅遊地）
                本旅遊團名稱為 　　　　　　　　　 
                一、旅遊地區（國家、城市或觀光點）：
                二、行程（起程回程之終止地點、日期、交通工具、住宿旅館、餐飲、遊覽及其所附隨之服務說明）：
                前項記載得以所刊登之廣告、宣傳文件、行程表或說明會之說明內容代之，視為本契約之一部分，如載明僅供參考或以外國旅遊業所提供之內容為準者，其記載無效。
                第四條（集合及出發時地）
                甲方應於民國　　年　　月　　日　　時　　分於　　準時集合出發。甲方未準時到約定地點集合致未能出發，亦未能中途加入旅遊者，視為甲方解除契約，乙方得依第二十七條之規定，行使損害賠償請求權。
                第五條（旅遊費用）
                旅遊費用：
                甲方應依下列約定繳付：
                一、簽訂本契約時，甲方應繳付新台幣　　　　　元。
                二、其餘款項於出發前三日或說明會時繳清。除經雙方同意並增訂其他協議事項於本契約第三十六條，乙方不得以任何名義要求增加旅遊費用。
                第六條（怠於給付旅遊費用之效力）
                甲方因可歸責自己之事由，怠於給付旅遊費用者，乙方得逕行解除契約，並沒收其已繳之訂金。如有其他損害，並得請求賠償。
                第七條（旅客協力義務）
                旅遊需甲方之行為始能完成，而甲方不為其行為者，乙方得定相當期限，催告甲方為之。甲方逾期不為其行為者，乙方得終止契約，並得請求賠償因契約終止而生之損害。
                旅遊開始後，乙方依前項規定終止契約時，甲方得請求乙方墊付費用將其送回原出發地。於到達後，由甲方附加年利率　％利息償還乙方。
                第八條（交通費之調高或調低）
                旅遊契約訂立後，其所使用之交通工具之票價或運費較訂約前運送人公布之票價或運費調高或調低逾百分之十者，應由甲方補足或由乙方退還。
                第九條（旅遊費用所涵蓋之項目）
                甲方依第五條約定繳納之旅遊費用，除雙方另有約定以外，應包括下列項目：
                一、代辦出國手續費：乙方代理甲方辦理出國所需之手續費及簽證費及其他規費。 
                二、交通運輸費：旅程所需各種交通運輸之費用。
                三、餐飲費：旅程中所列應由乙方安排之餐飲費用。
                四、住宿費：旅程中所列住宿及旅館之費用，如甲方需要單人房，經乙方同意安排者，甲方應補繳所需差額。
                五、遊覽費用：旅程中所列之一切遊覽費用，包括遊覽交通費、導遊費、入場門票費。 
                六、接送費：旅遊期間機場、港口、車站等與旅館間之一切接送費用。 
                七、行李費：團體行李往返機場、港口、車站等與旅館間之一切接送費用及團體行李接送人員之小費，行李數量之重量依航空公司規定辦理。
                八、稅捐：各地機場服務稅捐及團體餐宿稅捐。
                九、服務費：領隊及其他乙方為甲方安排服務人員之報酬 。 
                第十條（旅遊費用所未涵蓋項目）
                第五條之旅遊費用，不包括下列項目：
                一、非本旅遊契約所列行程之一切費用。 
                二、甲方個人費用：如行李超重費、飲料及酒類、洗衣、電話、電報、私人交通費、行程外陪同購物之報酬、自由活動費、個人傷病醫療費、宜自行給與提供個人服務者（如旅館客房服務人員）之小費或尋回遺失物費用及報酬。
                三、未列入旅程之簽證、機票及其他有關費用。
                四、宜給與導遊、司機、領隊之小費。
                五、保險費：甲方自行投保旅行平安保險之費用。
                六、其他不屬於第九條所列之開支。
                前項第二款、第四款宜給與之小費，乙方應於出發前，說明各觀光地區小費收取狀況及約略金額。
                第十一條（強制投保保險）
                乙方應依主管機關之規定辦理責任保險及履約保險。
                乙方如未依前項規定投保者，於發生旅遊意外事故或不能履約之情形時，乙方應以主管機關規定最低投保金額計算其應理賠金額之三倍賠償甲方。
                第十二條（組團旅遊最低人數）
                本旅遊團須有　　　人以上簽約參加始組成。如未達前定人數， 乙方應於預定出發之七日前通知甲方解除契約，怠於通知致甲方受損害者，乙方應賠償甲方損害。
                乙方依前項規定解除契約後，得依下列方式之一，返還或移作依第二款成立之新旅遊契約之旅遊費用。
                一、退還甲方已交付之全部費用，但乙方已代繳之簽證或其他規費得予扣除。 
                二、徵得甲方同意，訂定另一旅遊契約，將依第一項解除契約應返還甲方之全部費用，移作該另訂之旅遊契約之費用全部或一部。
                第十三條（代辦簽證、洽購機票）
                如確定所組團體能成行，乙方即應負責為甲方申辦護照及依旅程所需之簽證，並代訂妥機位及旅館。乙方應於預定出發七日前，或於舉行出國說明會時，將甲方之護照、簽證、機票、機位、旅館及其他必要事項向甲方報告，並以書面行程表確認之。乙方怠於履行上述義務時，甲方得拒絕參加旅遊並解除契約，乙方即應退還甲方 所繳之所有費用。
                乙方應於預定出發日前，將本契約所列旅遊地之地區城市、國家或觀光點之風俗人情、地理位置或其他有關旅遊應注意事項儘量提供甲方旅遊參考。
                第十四條（因旅行社過失無法成行）
                因可歸責於乙方之事由，致甲方之旅遊活動無法成行時，乙方於知悉旅遊活動無法成行者，應即通知甲方並說明其事由。怠於通知者，應賠償甲方依旅遊費用之全部計算之違約金；其已為通知者，則按通知到達甲方時，距出發日期時間之長短，依下列規定計算應 賠償甲方之違約金。
                一、通知於出發日前第三十一日以前到達者，賠償旅遊費用百分之十。
                二、通知於出發日前第二十一日至第三十日以內到達者，賠償旅遊費用百分之二十。
                三、通知於出發日前第二日至第二十日以內到達者，賠償旅遊費用百分之三十。
                四、通知於出發日前一日到達者，賠償旅遊費用百分之五十。
                五、通知於出發當日以後到達者，賠償旅遊費用百分之一百。
                甲方如能證明其所受損害超過前項各款標準者，得就其實際損害請求賠償。
                第十五條（非因旅行社之過失無法成行）
                因不可抗力或不可歸責於乙方之事由，致旅遊團無法成行者，乙方於知悉旅遊活動無法成行時應即通知甲方並說明其事由；其怠於通知甲方，致甲方受有損害時，應負賠償責任。
                第十六條（因手續瑕疵無法完成旅遊）
                旅行團出發後，因可歸責於乙方之事由，致甲方因簽證、機票或其他問題無法完成其中之部分旅遊者，乙方應以自己之費用安排甲方至次一旅遊地，與其他團員會合；無法完成旅遊之情形，對全部團員均屬存在時，並應依相當之條件安排其他旅遊活動代之；如無次一旅遊地時，應安排甲方返國。
                前項情形乙方未安排代替旅遊時，乙方應退還甲方未旅遊地部分之費用，並賠償同額之違約金。
                因可歸責於乙方之事由，致甲方遭當地政府逮捕、羈押或留置時，乙方應賠償甲方以每日新台幣二萬元整計算之違約金，並應負責迅速接洽營救事宜，將甲方安排返國，其所需一切費用，由乙方負擔。
                第十七條（領隊）
                乙方應指派領有領隊執業證之領隊。
                甲方因乙方違反前項規定，而遭受損害者，得請求乙方賠償。
                領隊應帶領甲方出國旅遊，並為甲方辦理出入國境手續、交通、食宿、遊覽及其他完成旅遊所須之往返全程隨團服務。
                第十八條（證照之保管及退還）
                乙方代理甲方辦理出國簽證或旅遊手續時，應妥慎保管甲方之各項證照，及申請該證照而持有甲方之印章、身分證等，乙方如有遺失或毀損者，應行補辦，其致甲方受損害者，並應賠償甲方之損失。
                甲方於旅遊期間，應自行保管其自有之旅遊證件，但基於辦理通關過境等手續之必要，或經乙方同意者，得交由乙方保管。
                前項旅遊證件，乙方及其受僱人應以善良管理人注意保管之，但甲方得隨時取回，乙方及其受僱人不得拒絕。
                第十九條（旅客之變更）
                甲方得於預定出發日　　日前，將其在本契約上之權利義務讓與第三人，但乙方有正當理由者，得予拒絕。
                前項情形，所減少之費用，甲方不得向乙方請求返還，所增加之費用，應由承受本契約之第三人負擔，甲方並應於接到乙方通知後 　　日內協同該第三人到乙方營業處所辦理契約承擔手續。
                承受本契約之第三人，與甲方雙方辦理承擔手續完畢起，承繼甲方基於本契約之一切權利義務。
                第二十條（旅行社之變更）
                乙方於出發前非經甲方書面同意，不得將本契約轉讓其他旅行業，否則甲方得解除契約，其受有損害者，並得請求賠償。
                甲方於出發後始發覺或被告知本契約已轉讓其他旅行業，乙方應賠償甲方全部團費百分之五之違約金，其受有損害者，並得請求賠償。
                第二十一條（國外旅行業責任歸屬）
                乙方委託國外旅行業安排旅遊活動，因國外旅行業有違反本契約 或其他不法情事，致甲方受損害時，乙方應與自己之違約或不法行為負同一責任。但由甲方自行指定或旅行地特殊情形而無法選擇受託者，不在此限。
                第二十二條（賠償之代位）
                乙方於賠償甲方所受損害後，甲方應將其對第三人之損害賠償請求權讓與乙方，並交付行使損害賠償請求權所需之相關文件及證據。
                第二十三條（旅程內容之實現及例外）
                旅程中之餐宿、交通、旅程、觀光點及遊覽項目等，應依本契約所訂等級與內容辦理，甲方不得要求變更，但乙方同意甲方之要求而變更者，不在此限，惟其所增加之費用應由甲方負擔。除非有本 契約第二十八條或第三十一條之情事，乙方不得以任何名義或理由變更旅遊內容，乙方未依本契約所訂等級辦理餐宿、交通旅程或遊覽項目等事宜時，甲方得請求乙方賠償差額二倍之違約金。
                第二十四條（因旅行社之過失致旅客留滯國外）
                因可歸責於乙方之事由，致甲方留滯國外時，甲方於留滯期間所支出之食宿或其他必要費用，應由乙方全額負擔，乙方並應儘速依預定旅程安排旅遊活動或安排甲方返國，並賠償甲方依旅遊費用總額除以全部旅遊日數乘以滯留日數計算之違約金。
                第二十五條（延誤行程之損害賠償）
                因可歸責於乙方之事由，致延誤行程期間，甲方所支出之食宿或其他必要費用，應由乙方負擔。甲方並得請求依全部旅費除以全部旅遊日數乘以延誤行程日數計算之違約金。但延誤行程之總日數，以不超過全部旅遊日數為限，延誤行程時數在五小時以上未滿一日者，以一日計算。
                第二十六條（惡意棄置旅客於國外）
                乙方於旅遊活動開始後，因故意或重大過失，將甲方棄置或留滯國外不顧時，應負擔甲方於被棄置或留滯期間所支出與本旅遊契約所訂同等級之食宿、返國交通費用或其他必要費用，並賠償甲方全部旅遊費用之五倍違約金。
                第二十七條（出發前旅客任意解除契約）
                甲方於旅遊活動開始前得通知乙方解除本契約，但應繳交證照費用，並依左列標準賠償乙方：
                一、通知於旅遊活動開始前第三十一日以前到達者，賠償旅遊費用百分之十。
                二、通知於旅遊活動開始前第二十一日至第三十日以內到達者，賠償旅遊費用百分之二十。 
                三、通知於旅遊活動開始前第二日至第二十日以內到達者，賠償旅遊費用百分之三十。 
                四、通知於旅遊活動開始前一日到達者，賠償旅遊費用百分之五十。 
                五、通知於旅遊活動開始日或開始後到達或未通知不參加者，賠償旅遊費用百分之一百。
                前項規定作為損害賠償計算基準之旅遊費用，應先扣除簽證費後計算之。
                乙方如能證明其所受損害超過第一項之標準者，得就其實際損害請求賠償。
                第二十八條（出發前有法定原因解除契約）
                因不可抗力或不可歸責於雙方當事人之事由，致本契約之全部或一部無法履行時，得解除契約之全部或一部，不負損害賠償責任。乙方應將已代繳之規費或履行本契約已支付之全部必要費用扣除後之餘款退還甲方。但雙方於知悉旅遊活動無法成行時應即通知他方並說明事由；其怠於通知致使他方受有損害時，應負賠償責任。
                為維護本契約旅遊團體之安全與利益，乙方依前項為解除契約之一部後，應為有利於旅遊團體之必要措置（但甲方不同意者，得拒絕之），如因此支出必要費用，應由甲方負擔。
                第二十八條之一（出發前有客觀風險事由解除契約）
                出發前，本旅遊團所前往旅遊地區之一，有事實足認危害旅客生命、身體、健康、財產安全之虞者，準用前條之規定，得解除契約。但解除之一方，應按旅遊費用百分之      補償他方（不得超過百分之五）。
                第二十九條 （出發後旅客任意終止契約）
                甲方於旅遊活動開始後中途離隊退出旅遊活動時，不得要求乙方退還旅遊費用。但乙方因甲方退出旅遊活動後，應可節省或無須支付之費用，應退還甲方。
                甲方於旅遊活動開始後，未能及時參加排定之旅遊項目或未能及時搭乘飛機、車、船等交通工具時，視為自願放棄其權利，不得向乙方要求退費或任何補償。
                第三十條（終止契約後之回程安排）
                甲方於旅遊活動開始後，中途離隊退出旅遊活動，或怠於配合乙方完成旅遊所需之行為而終止契約者，甲方得請求乙方墊付費用將其送回原出發地。於到達後，立即附加年利率　％利息償還乙方。
                乙方因前項事由所受之損害，得向甲方請求賠償。
                第三十一條（旅遊途中行程、食宿、遊覽項目之變更）
                旅遊途中因不可抗力或不可歸責於乙方之事由，致無法依預定之旅程、食宿或遊覽項目等履行時，為維護本契約旅遊團體之安全及利益，乙方得變更旅程、遊覽項目或更換食宿、旅程，如因此超過 原定費用時，不得向甲方收取。但因變更致節省支出經費，應將節省部分退還甲方。
                甲方不同意前項變更旅程時得終止本契約，並請求乙方墊付費用將其送回原出發地。於到達後，立即附加年利率　％利息償還乙方。
                第三十二條（國外購物）
                為顧及旅客之購物方便，乙方如安排甲方購買禮品時，應於本契約第三條所列行程中預先載明，所購物品有貨價與品質不相當或瑕疪時，甲方得於受領所購物品後一個月內請求乙方協助處理。 
                乙方不得以任何理由或名義要求甲方代為攜帶物品返國。 
                第三十三條 （責任歸屬及協辦）
                旅遊期間，因不可歸責於乙方之事由，致甲方搭乘飛機、輪船、火車、捷運、纜車等大眾運輸工具所受損害者，應由各該提供服務之業者直接對甲方負責。但乙方應盡善良管理人之注意，協助甲方處理。
                第三十四條（協助處理義務）
                甲方在旅遊中發生身體或財產上之事故時，乙方應為必要之協助及處理。
                前項之事故，係因非可歸責於乙方之事由所致者，其所生之費用，由甲方負擔。但乙方應盡善良管理人之注意，協助甲方處理 。
                第三十五條（誠信原則）
                甲乙雙方應以誠信原則履行本契約。乙方依旅行業管理規則之規定，委託他旅行業代為招攬時，不得以未直接收甲方繳納費用，或以非直接招攬甲方參加本旅遊，或以本契約實際上非由乙方參與簽訂為抗辯。
                第三十六條（其他協議事項）
                甲乙雙方同意遵守下列各項：
                一 、甲方□同意□不同意乙方將其姓名提供給其他同團旅客。
                二、
                三、
                前項協議事項，如有變更本契約其他條款之規定，除經交通部觀光局核准，其約定無效，但有利於甲方者，不在此限。
                訂約人　
                甲方：
　　　　　　　                 住　　　址：
　　　　　　　                 身分證字號：
　　　　　　　                 電話或電傳：
　　　　　　　                 甲方為未成年旅客，其法定代理人_____________(身分證字號：_____________)已知悉且同意甲方報名參與乙方之旅遊行程。 
　　　　　　　                 法定代理人與甲方之關係： 
　　　　　　　                 聯絡電話：
　　　　                乙方（公司名稱）：
　　　　　　　                 註 冊 編 號：
　　　　　　　                 負　責　人：
　　　　　　　                 住　　　址：
　　　　　　　                 電話或電傳：
                乙方委託之旅行業副署：（本契約如係綜合或甲種旅行業自行組團而與旅客簽約者，下列
                                各項免填）
　　　　　　　                 公 司 名 稱：
　　　　　　　                 註 冊 編 號：
　　　　　　　                 負　責　人：
　　　　　　　                 住　　　址：
　　　　　　　                 電話或電傳：
　
                簽約日期：中華民國      年　　 　月　 　 日
　                   （如未記載以交付訂金日為簽約日期） 

                簽約地點：　   
　　　　　 　　
                （如未記載以甲方住所地為簽約地點）</textarea>
                </div>
            </div>

            <div class="form_title">團體報名須知</div>
            <div class="form_tool">
                <div class="form_notice">
                    1、所有行程皆需隨團進出，團體旅遊法定成團人數為當團滿16位以上始出發！<br />
                    2、報名時請先自行確認護照之有效期限在本行程回國日起算六個月以上，申辦新護照者，護照申辦為四個半工作天(不含假日)，簽證工作天依各國家簽
                    證規定發給。<br />
                    3、報名後，本公司會請業務專員於報名24小時內與您聯絡。經本公司確認訂單後，業務專員將與您洽談相關事宜及收取訂金。旅客簽訂國外旅遊定型化
                    契約書後，<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;若變更或取消行程，依國外旅遊定型化契約書內容辦理。
                    <br />
                    4、凡報名參加特別折扣團型(例如：買一送一或小孩半價或預購折扣優惠團---等)，折扣部份需等客服人員核算後再回覆正確團費售價。<br />
                    5、  若因匯率調整或航空票價調漲，本公司將於收到訂金或團費前保留調整團費之權益。為維護您的消費權益，報名前請務必詳閱「國外旅遊定型化契約書」！ 
                </div>
            </div>

            <div style="font-size: 13px">
                <asp:CheckBox ID="CheckBox1" runat="server" />
　              <span style="color: #FF0000">我已經閱讀「國外團體旅遊定型契約書」、「團體報名須知」，並同意其內容，旅遊契約於雙方簽名或蓋章後始生效。</span>
            </div>

            <div style="margin-bottom:40px; margin-top:20px;">
                <asp:Button ID="Button1" runat="server" Text="下一步" Height="42px" Width="60px" OnClick="Button1_Click" />
            </div>
            <!-----修改地區 end----->   
        </div>
        <uc2:Foot ID="Foot1" runat="server" />
            <asp:HiddenField runat="server" ID="hiddtotal" />
    </div>
    </div>
  </form>
</body>
</html>
