<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_Join.aspx.cs" Inherits="A_Join" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_sale.css" rel="stylesheet" type="text/css" />
    <link href="css/color.css" rel="stylesheet" type="text/css" />
    <script src="SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <link href="SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
    <uc1:Header ID="Header1" runat="server" />
       
<div id="member_join">
<table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 0px" width="755">
        <tr>
            <td align="middle" style="text-align: left" valign="top">
                <div class="bblogin"></div>
                <div class="disdata">
                    <p class="cor1"></p>
                    <p class="cor2"></p>
                    <p class="intro" style="left: 0px; top: 0px;color:#5f5f5f;line-height:24px;">
                        歡迎您申請加入「凱旋旅行社同業會員」，我們將提供各項旅遊產品與您分享，<br />
                            這呢好康ㄟ的代誌，怎麼能讓它錯過呢？！<br />
                            趕緊填寫申請表，加入成為「凱旋旅行社同業會員」喔！ 
                        <br />
                        服務人員將在收到您的申請資料後，將於三個工作日審核完畢並以Email通知並核發同業會員資格。<br />
                        若超過三個工作日未與您聯繫確認，請主動與同業會員審核小組聯絡TEL:(02)2518-0011，謝謝!!
                    </p>
<textarea name="textarea" id="textarea" cols="100" rows="15" style="color:#5f5f5f;line-height:22px;">為保障您的權益，申請前請詳細閱讀下列事項：
1.同業會員需為旅遊從業人員。
2.同業會員註冊時，應提供完整詳實且正確的個人資料，如有變更時，請於線上更新或通知貴公司所屬巨匠業務人員。
3.巨匠Ｂ２Ｂ同業網保有審核其身分之權益，決定發予申請人帳號及密碼之權利。
4.本同業會員申請加入屬免費性質，巨匠Ｂ２Ｂ同業網保留隨時更改或停止所提供各項服務之內容，或終止任一同業會員帳戶服務之權利，且無需事先告知同業會員，同業會員不得因此要求任何補償或賠償。
5.為維護同業會員自身權益，同業會員應妥善保管帳號及密碼，請勿洩露或提供予他人使用，並為帳號及密碼進入系統後，所從事之一切活動負責。
6.同業會員所提供之個人資料內容，巨匠Ｂ２Ｂ同業網會遵守個人資料保護法之規範。對於同業會員所登錄之個人資料，同業會員同意巨匠Ｂ２Ｂ同業網及關係企業或合作對象，得於合理合法之範圍內蒐集、傳遞及使用此資料，以作為統計、研究或提供其他服務。
7.巨匠Ｂ２Ｂ同業網保留隨時更改本同業會員規範之權利，且將更改之內容公告於巨匠Ｂ２Ｂ同業網會員中心，不另作同業會員個別通知，如同業會員繼續使用本網站服務，即視為同意新修正之同業會員規範。
8.同業加入巨匠Ｂ２Ｂ同業網成為會員後，網站會主動寄送會員電子報及各項業務活動訊息。
9.其他未盡事宜，均依照中華民國法律規定及網路規範辦理，同業會員同意若因本同業會員規範條款有所爭議或糾紛，以台灣台北地方法院為第一審管轄法院。
  </textarea>
<br />
<div style="font-size: 13px; text-align:center;"><asp:CheckBox ID="CheckBox1" runat="server" />我同意以上條款 。</div>
                    <asp:Panel ID="Panel1" runat="server" CssClass="wrdata">
                        <h3>【旅行社資料】<span class="mini-text"><span style="color:#a40000;">*為必填欄位</span></span></h3>
                                *統一編號：<asp:TextBox ID="txbCOMP_NO" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="images/bbsend2a.jpg" AlternateText="確認送出" OnClick="ImageButton1_Click" />
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" CssClass="wrdata" Visible="false">
                        <h3>【旅行社資料】<span class="mini-text"><span style="color:#a40000;">*為必填欄位</span></span></h3>
                        <ul>
                            <li>*統一編號：<asp:Label ID="lblSN" runat="server"></asp:Label></li>
                            <li>*公司名稱：<asp:Label ID="lblCompName" runat="server"></asp:Label></li>
                            <li>*負 責 人：<asp:Label ID="lblResp" runat="server"></asp:Label></li>
                            <li>*郵遞區號：<asp:Label ID="lblZip" runat="server"></asp:Label></li>
                            <li>*聯絡地址：<asp:Label ID="lblAddr" runat="server"></asp:Label></li>
                            <li>*公司電話：<asp:Label ID="lblCompTel" runat="server"></asp:Label></li>
                        </ul>
                        <asp:HiddenField ID="hidCompName1" runat="server" Visible="false" />
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" CssClass="wrdata" Visible="false">
                        <h3>【聯絡人資料】<span class="mini-text"><span style="color:#a40000;">*為必填欄位</span></span></h3>
                        <ul>
                            <li>
                                *請輸入聯絡人ID：<asp:TextBox ID="txbID" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="images/bbsend2a.jpg" AlternateText="確認送出" OnClick="ImageButton2_Click"  />
                            </li>
                        </ul>
                    </asp:Panel>
                    <asp:Panel ID="Panel4" runat="server" CssClass="wrdata" Visible="false">
							<h3>【申請人資料】<span class="mini-text" >*為必填欄位</span></h3>
							<ul>
								<li>*身份証字號：<asp:Label ID="lblIDNO" runat="server"></asp:Label></li><li>*中文姓名：<asp:TextBox ID="txbAGTC_NM" runat="server" MaxLength="10" Width="115px"></asp:TextBox></li><li>*帳　　號：<asp:TextBox ID="txbUSR_ID" runat="server" MaxLength="10" Width="115px" BorderStyle="None" ReadOnly="True"></asp:TextBox><span class="note-text">　(請至少輸入4 ~ 10碼)</span></li><li>*密　　碼：<asp:TextBox ID="txbUSR_PASSWD" runat="server" MaxLength="8" Width="115px" TextMode="password"></asp:TextBox><span class="note-text">　(請至少輸入4 ~ 8碼)</span></li><li>*確認密碼：<asp:TextBox ID="txbUSR_PASSWD2" runat="server" MaxLength="8" Width="115px" TextMode="password"></asp:TextBox></li><li>*聯絡電話：<asp:TextBox ID="txbCNTA_T3_CCD" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
								        -<asp:TextBox ID="txbCNTA_T3" runat="server" MaxLength="10" Width="120px"></asp:TextBox>　分機<asp:TextBox ID="txbCNTA_T3_ZIP" runat="server" MaxLength="10" Width="50px"></asp:TextBox></li><li>&nbsp; 傳真號碼：<asp:TextBox ID="txbCNTA_T2_CCD" runat="server" MaxLength="3" Width="50px"></asp:TextBox>
								        -<asp:TextBox ID="txbCNTA_T2" runat="server" MaxLength="10" Width="120px"></asp:TextBox></li><li>*靠　　行：<asp:RadioButtonList ID="rbIND_FG" runat="server" RepeatDirection="Horizontal" RepeatLayout="flow">
                                            <asp:ListItem Value="1" Selected="true">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:RadioButtonList></li><li>*手機號碼：<asp:TextBox id="txbCNTA_T8" runat="server" MaxLength="15" Width="120px"></asp:TextBox>　<span class="note-text">(請勿輸入數字以外的任何符號)</span></li><li>*E-Mail：<asp:TextBox id="txbCNTA_E1" runat="server" Width="256px" MaxLength="500"></asp:TextBox><br />
									<span class="note-text">(為保障您的權益，請盡量不要使用免費之郵件信箱，如:yahoo、hotmail、pchome…等。<br />建議使用公司電子信箱為佳。)</span>
								</li>
								<li>*聯絡地址：
                                    <asp:DropDownList ID="ddlAddr_City" runat="server" ToolTip="聯絡地址(縣市)" AutoPostBack="true" OnSelectedIndexChanged="ddlAddr_City_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlAddr_Country" runat="server" ToolTip="聯絡地址(鄉鎮市區)" AutoPostBack="true" OnSelectedIndexChanged="ddlAddr_Country_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txbAddr_ccd" runat="server" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                    <asp:TextBox ID="txbAddr" runat="server" MaxLength="500" Width="210px"></asp:TextBox>
								</li>
								<li><asp:ImageButton ID="ImageButton3" runat="server"  ImageUrl="images/bbsend2a.jpg" AlternateText="確認送出" OnClick="ImageButton3_Click"  /></li></ul>
                    </asp:Panel>
                    
                    <p class="cor3"></p>
                    <p class="cor4"></p>
                </div>
            </td>
        </tr>
    </table></div>
    <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
