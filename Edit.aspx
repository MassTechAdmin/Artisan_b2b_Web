<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <table border="0" cellpadding="0" cellspacing="0" bgcolor="#EEEEEE" style="margin-left:55px;padding-top:10px;padding-bottom:10px;">
        <tr>
            <td colspan="4" style="width: 970px;padding-left:10px;">
                電腦編號：
                <asp:Label ID="Label1" runat="server" BackColor="White" Width="60px"></asp:Label>
                &nbsp; 現在使用者：<asp:Label ID="Label2" runat="server" BackColor="White" Width="80px"></asp:Label>
                &nbsp; &nbsp;&nbsp; 建檔日期：<asp:Label ID="Label3" runat="server" BackColor="White" Width="200px"></asp:Label>
                &nbsp; &nbsp; 建檔人員：<asp:Label ID="Label4" runat="server" BackColor="White" Width="70px"></asp:Label></td>
        </tr>
    </table><br />
    
    
    <div id="add_list_tool">

    <table width="980px" border="0"  style="padding-left:15px;height:38px;">
        <tr>
            <td colspan="3" style="font-weight: bold; font-size: 22px; font-family:微軟正黑體; color:#ad2207;">
                線上報名單</td>
        </tr>
        <tr>
            <td colspan="3" style="font-weight: bold; font-size: 18px">            </td>
        </tr>
        <tr>
            <td style="width: 240px;font-size:13px;">
                網路報名單號：<asp:Label ID="Label6" runat="server" BackColor="WhiteSmoke" Width="110px"></asp:Label>            </td>
            <td style="width: 210px;font-size:13px;">
                報名日期：<asp:Label ID="Label14" runat="server" BackColor="WhiteSmoke"></asp:Label>            </td>
            <td style="width: 260px"><span style="width: 240px;font-size:13px;">出發日期：
                <asp:TextBox ID="TextBox4" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="150px"></asp:TextBox>
            </span>            </td>
        </tr>
        <tr style="height: 10px">
            <td colspan="3" style="font-weight: bold; font-size: 18px">            </td>
        </tr>
        <tr>
            <td colspan="3" style="font-size:13px;">
                團　　名：<asp:Label ID="Label15" runat="server" BackColor="WhiteSmoke" 
                    Width="620px"></asp:Label> </td>
        </tr>
        <tr style="height: 10px">
            <td colspan="3" style="font-weight: bold; font-size: 18px">            </td>
        </tr>
        <tr>
            <td style="width: 240px;font-size:13px;">
                公司簡稱：<asp:TextBox ID="TextBox6" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="125px"></asp:TextBox></td>
            <td style="width: 210px;font-size:13px;">
                公司電話：<asp:TextBox ID="TextBox8" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="100px"></asp:TextBox></td>
            <td style="width: 260px;font-size:13px;">
                公司傳真：<asp:TextBox ID="TextBox9" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="150px"></asp:TextBox></td>
        </tr>
        <tr style="height: 10px">
            <td colspan="3" style="font-weight: bold; font-size: 18px">            </td>
        </tr>
        <tr>
            <td style="width: 240px;font-size:13px;">
                　聯絡人：<asp:TextBox ID="TextBox7" runat="server" BackColor="WhiteSmoke" BorderStyle="None" MaxLength="10" ReadOnly="True" Width="125px"></asp:TextBox></td>
            <td style="width: 210px;font-size:13px;">
                手　　機：<asp:TextBox ID="TextBox19" runat="server" BackColor="WhiteSmoke" BorderStyle="None" MaxLength="10" ReadOnly="True" Width="100px"></asp:TextBox></td>
            <td style="width: 260px;font-size:13px;">
                電子郵件：<asp:TextBox ID="TextBox20" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="150px"></asp:TextBox>            </td>
        </tr>
        <tr style="height: 10px">
            <td colspan="3" style="font-weight: bold; font-size: 18px">            </td>
        </tr>
        <tr>
            <td style="width: 210px;font-size:13px;">
                報名人數：<asp:TextBox ID="TextBox11" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="125px"></asp:TextBox>            </td>
            <td colspan="2" style="width: 500px;font-size:13px;">
                備　　註：<asp:TextBox ID="TextBox16" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="382px"></asp:TextBox>            </td>
        </tr>
        <tr style="height: 10px">
            <td colspan="3" style="font-weight: bold; font-size: 18px">            </td>
        </tr>
        <tr>
            <td style="width: 210px;font-size:13px;">
                團　　費：<asp:TextBox ID="TextBox12" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="125px"></asp:TextBox>            </td>
            <td colspan="2" style="width: 500px;font-size:13px;">
                報名狀態：<asp:TextBox ID="txbState" runat="server" BackColor="WhiteSmoke" BorderStyle="None" ReadOnly="True" Width="100px"></asp:TextBox></td>
        </tr>
    </table>
    </div>

    <br /><br />
    <div style="margin-left: 200px; margin-right: 200px; background-image: url(images/ps_bg.gif);background-repeat: x-repeat; padding:10px; letter-spacing:1px; border:solid #CCCCCC 1px;font-size:13px;width:740px;margin-bottom:30px;height:150px; float:left;">
        <span style="color:red; width: 500px; font-size:16px; font-weight:bold; line-height:32px;">請注意輸入格式：</span><br />
        <span style="color:#555555; width: 500px; line-height:28px; ">
            例：出生日期：19880112&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 護照發照日：19880112&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 護照效期：19880112
            　　<br />
            　　本報名單，將於十分鍾內正式成立，業務員將隨後與您確認本次報名資料。<br />
            　　請詳細填寫旅客明細，如未填寫則本報名單視同無效，本次報名單成功與否，將由本公司業務員通知。<br />
                ＊為必填(紅色的)<br />
        </span>
    </div>
    <br /><br />
    <div style="margin-left:80px;font-weight: bold; font-size: 22px; font-family:微軟正黑體; color:#ad2207; float:left;width:970px;">旅客明細</div>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" EmptyDataText="<p>尚未建立資料或未找到您所搜尋的資料</p>" AutoGenerateColumns="False"
        BorderStyle="None" CellPadding="0" GridLines ="None">
        <Columns>
            <asp:TemplateField HeaderText="序號" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblSEQU_NO" runat="server" Text='<%# Eval("SEQU_NO","{0}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="身份證號"> 
                <ItemTemplate>
                    <asp:TextBox ID="lblCUST_IDNO" runat="server" Text='<%# Eval("CUST_IDNO","{0}") %>' Width="75" MaxLength="20"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="稱呼">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlMRSS_Name" runat="server" Width="60">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidMRSS_Name" runat="server" Value='<%# Eval("MRSS_Name","{0}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<span style='color:red;'>＊</span>旅客姓名">
                <ItemTemplate>
                    <asp:TextBox ID="lblCUST_NAME" runat="server" Text='<%# Eval("CUST_NAME","{0}") %>' Width="65"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="英文姓名">
                <ItemTemplate>
                    <asp:TextBox ID="lblENGL_CUST" runat="server" Text='<%# Eval("Eng_Name","{0}") %>' Width="100"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="出生日期">
                <ItemTemplate>
                    <asp:TextBox ID="lblBIRT_DATE" runat="server" Text='<%# fn_Rtn_DATE(Eval("BIRT_DATE","{0}")) %>' Width="62" MaxLength="10"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="護照號碼">
                <ItemTemplate>
                    <asp:TextBox ID="lblPASS_NO" runat="server" Text='<%# Eval("PASS_NO","{0}") %>' Width="80"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="護照發照日">
                <ItemTemplate>
                    <asp:TextBox ID="lblPASS_ISSU" runat="server" Text='<%# fn_Rtn_DATE(Eval("PASS_ISSU","{0}")) %>' Width="62" MaxLength="10"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="護照效期">
                <ItemTemplate>
                    <asp:TextBox ID="lblPASS_VALI" runat="server" Text='<%# fn_Rtn_DATE(Eval("PASS_VALI","{0}")) %>' Width="62" MaxLength="10"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="手機">
                <ItemTemplate>
                    <asp:TextBox ID="lblCell_Tel" runat="server" Text='<%# Eval("Cell_Tel","{0}") %>' Width="65" MaxLength="10"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="E-Mail">
                <ItemTemplate>
                    <asp:TextBox ID="lblE_Mail" runat="server" Text='<%# Eval("E_Mail","{0}") %>' Width="100"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="佔床">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlBED_TYPE" runat="server" Width="70">
                        <asp:ListItem Value="佔床">佔床</asp:ListItem>
                        <asp:ListItem Value="不佔床">不佔床</asp:ListItem>
                        <asp:ListItem Value="加床">加床</asp:ListItem>
                        <asp:ListItem Value="一大床">一大床</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidBED_TYPE" runat="server" Value='<%# Eval("BED_TYPE","{0}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="飲食">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlEAT" runat="server" Width="80">
                        <asp:ListItem Value="一般">一般</asp:ListItem>
                        <asp:ListItem Value="吃素">吃素</asp:ListItem>
                        <asp:ListItem Value="早齋(機上素)">早齋(機上素)</asp:ListItem>
                        <asp:ListItem Value="不吃牛肉">不吃牛肉</asp:ListItem>
                        <asp:ListItem Value="不吃豬肉">不吃豬肉</asp:ListItem>
                        <asp:ListItem Value="兒童餐">兒童餐</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidEAT" runat="server" Value='<%# Eval("EAT","{0}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="票別">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlTICK_TYPE" runat="server" Width="60">
                        <asp:ListItem Value="大人">大人</asp:ListItem>
                        <asp:ListItem Value="CHD">CHD</asp:ListItem>
                        <asp:ListItem Value="INF">INF</asp:ListItem>
                        <asp:ListItem Value="不開票">不開票</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidTICK_TYPE" runat="server" Value='<%# Eval("TICK_TYPE","{0}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="備註" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="lblMARK" runat="server" Text='<%# Eval("Remark","{0}") %>' Width="90"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="gv_HeaderStyle" />
    </asp:GridView>
    <br />
    <asp:Button ID="Button10" runat="server" OnClientClick="if (confirm('旅客明細確定存檔嗎？')==false) {return false;}" Text="旅客明細確認" OnClick="Button10_Click" />
    <input id="Hidden1" runat="server" name="Hidden1" type="hidden" />
    <br /><br />
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
