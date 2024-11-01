<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="step2.aspx.cs" Inherits="step2" %>
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
    </style>

    <script type="text/javascript">
        function BlockNumber(e) {
            var key = window.event ? e.keyCode : e.which;
            var keychar = String.fromCharCode(key);
            reg = /[0-9]/;
            return reg.test(keychar);
        }
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
<body onkeydown="if (event.keyCode == 13){return false;}">
  <form id="form1" runat="server">
    <div id="wrapper">
        <nav>
            <uc1:Header ID="Header1" runat="server" />
        </nav>
        <div id="content">
            <!-----修改地區----->
            <div id="div1" runat="server" style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;margin-top:30px;">

                <div id="stepList_tool">
                    <div class="stepList01">1.填寫旅客人數</div>
                    <div class="stepList02">2.填寫訂購資料</div>
                    <div class="stepList01">3.填寫旅客名單</div>
                    <div class="stepList01">4.完成報名</div>
                </div>

                <div class="form_title">請輸入訂購人資料</div>

                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="105" align="center" valign="middle" bgcolor="#EEEEEE">團　名</td>
                            <td colspan="3" align="left" valign="middle" style="line-height:16px;"><asp:Label ID="Label1" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">報名人數</td>
                            <td colspan="3" align="left" valign="middle"><asp:Label ID="Label3" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">中文姓名</td>
                            <td width="185" align="left" valign="middle"><asp:TextBox ID="Label5" runat="server" /></td>
                            <td width="85" align="center" valign="middle" bgcolor="#EEEEEE">公司電話</td>
                            <td width="573" align="left" valign="middle">

                                <asp:TextBox ID="Tel1" runat="server" Width="40px" OnKeyPress="return BlockNumber(event);" MaxLength="5" onpaste="return false" /> -
                                <asp:TextBox ID="Tel2" runat="server" Width="120px" OnKeyPress="return BlockNumber(event);" MaxLength="8" onpaste="return false" /> #
                                <asp:TextBox ID="Tel3" runat="server" Width="50px" OnKeyPress="return BlockNumber(event);" MaxLength="6" onpaste="return false" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="電話區碼格式錯誤!" SetFocusOnError="true"
                                    ControlToValidate="Tel1" Display="Dynamic" ValidationExpression="(0)\d{1,3}" ForeColor="red" BackColor="#ffcccc" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="電話號碼格式錯誤!" SetFocusOnError="true"
                                        ControlToValidate="Tel2" Display="Dynamic" ValidationExpression="\d{6,8}" ForeColor="red" BackColor="#ffcccc" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="電話分機格式錯誤!" SetFocusOnError="true"
                                        ControlToValidate="Tel3" Display="Dynamic" ValidationExpression="\d{0,6}" ForeColor="red" BackColor="#ffcccc" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">手機號碼</td>
                            <td align="left" valign="middle"><asp:TextBox ID="Label6" runat="server" OnKeyPress="return BlockNumber(event);" MaxLength="10"/></td>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">傳真電話</td>
                            <td align="left" valign="middle">
                                <asp:TextBox ID="Fax1" runat="server" Width="40px" OnKeyPress="return BlockNumber(event);" MaxLength="5" onpaste="return false" /> -
                                <asp:TextBox ID="Fax2" runat="server" Width="120px" OnKeyPress="return BlockNumber(event);" MaxLength="8" onpaste="return false" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="傳真區碼格式錯誤!" SetFocusOnError="true"
                                    ControlToValidate="Fax1" Display="Dynamic" ValidationExpression="(0)\d{1,3}" ForeColor="red" BackColor="#ffcccc" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="傳真號碼格式錯誤!" SetFocusOnError="true"
                                    ControlToValidate="Fax2" Display="Dynamic" ValidationExpression="\d{6,8}" ForeColor="red" BackColor="#ffcccc" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">連絡電話</td>
                            <td colspan="3" align="left" valign="middle"><asp:TextBox ID="Label7" OnKeyPress="return BlockNumber(event);" runat="server" Width="280px"/></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">電子郵件</td>
                            <td colspan="3" align="left" valign="middle"><asp:TextBox ID="Label8" runat="server" Width="280px"/></td>
                        </tr>
                    </table>
                </div>

                <div style="margin-bottom: 40px; float: left; ">
                    <asp:Button ID="Button1" runat="server" Text="上一步" Height="42px" Width="60px" OnClick="Button1_Click" />
                </div>

                <div style="margin-bottom:40px;float:right; ">
                    <asp:Button ID="Button2" runat="server" Text="下一步" Height="42px" Width="60px" OnClick="Button2_Click" />
                </div>

            </div>

            <%--==================================我是分隔線==================================--%>

            <div id="div2" runat="server" style="text-align: center; width: 950px; height: auto; margin-left: auto; margin-right: auto;margin-top:30px;">
                <div id="stepList_tool">
                    <div class="stepList01">1.填寫旅客人數</div>
                    <div class="stepList01">2.填寫訂購資料</div>
                    <div class="stepList02">3.填寫旅客名單</div>
                    <div class="stepList01">4.完成報名</div>
                </div>

                <div class="form_title">報名單計價</div>
                <div class="form_tool">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="105" align="center" valign="middle" bgcolor="#EEEEEE">團　名</td>
                            <td align="left" valign="middle" style="line-height:16px;">
                                <asp:Label ID="Label9" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" bgcolor="#EEEEEE">旅客型別</td>
                            <td width="140" align="center" valign="middle" bgcolor="#EEEEEE">人數</td>
                        </tr>
                        <asp:Literal ID="TableItem" runat="server" />
                    </table>
                </div>
                
                <div class="form_title">行程內容</div>
                <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="尚未建立資料或未找到您所搜尋的資料" 
                    Width="950px"  CellPadding="3" GridLines="Vertical" AllowPaging="true"
                    RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid" BorderStyle="Solid" BorderWidth="1px" BorderColor="#cccccc" AutoGenerateColumns="False"
                    OnRowDataBound="GridView2_RowDataBound">
                    <HeaderStyle BackColor="#EEEEEE" ForeColor="Black" Width="100%" Font-Size="13px" Height="40px" />
                    <RowStyle Font-Size="13px" Height="40px" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="50%" />
                            <HeaderTemplate>
                                <i class="fa fa-check-square" aria-hidden="true"></i>行程內容包含
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span><%#Eval("n", "{0}")%></span><%#Eval("FName", "{0}")%>
                                <asp:HiddenField runat="server" ID="hiddNumber" Value='<%#Eval("Number", "{0}")%>' />
                                <asp:HiddenField ID="hidFitName" runat="server" Visible="false" Value='<%# Eval("FName")%>' />
                                <asp:HiddenField ID="hidFPrice" runat="server" Visible="false" Value='<%# Eval("FPrice")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="單　位" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <%#Eval("FUnit", "{0}")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="FitAgt-unit" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="費　用" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#"$" + Eval("FPrice", "{0:N0}")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="FitAgt-price" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="數量/人" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="dlnum" onchange="count()" Width="50%"></asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle CssClass="FitAgt-amount" />
                        </asp:TemplateField>
                    </Columns>                     
                </asp:GridView>     

                <div class="form_title">加價項目</div>
                <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="尚未建立資料或未找到您所搜尋的資料" 
                    Width="950px"  CellPadding="3" GridLines="Vertical" AllowPaging="true"
                    RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid" BorderStyle="Solid" BorderWidth="1px" BorderColor="#cccccc" AutoGenerateColumns="False"
                    OnRowDataBound="GridView3_RowDataBound">
                    <HeaderStyle BackColor="#EEEEEE" ForeColor="Black" Width="100%" Font-Size="13px" Height="40px" />
                    <RowStyle Font-Size="13px" Height="40px" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="50%" />
                            <HeaderTemplate>
                                <i class="fa fa-cart-plus" aria-hidden="true"></i>自費加購項目
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span><%#Eval("n", "{0}")%></span><%#Eval("FName", "{0}")%>
                                <asp:HiddenField runat="server" ID="hiddNumber2" Value='<%#Eval("Number", "{0}")%>' />
                                <asp:HiddenField ID="hidFitName" runat="server" Visible="false" Value='<%# Eval("FName")%>' />
                                <asp:HiddenField ID="hidFPrice" runat="server" Visible="false" Value='<%# Eval("FPrice")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="單　位" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <%#Eval("FUnit", "{0}")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="FitAgt-unit" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="費　用" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# "$" + Eval("FPrice", "{0:N0}")%>
                            </ItemTemplate>
                            <ItemStyle CssClass="FitAgt-price" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="數量/人" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="dlnum2"  onchange="count()" Width="50%"></asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle CssClass="FitAgt-amount" />
                        </asp:TemplateField>
                    </Columns>                     
                </asp:GridView>
                <br /><br />

                <div class="form_title">填寫旅客名單</div>
                <div style="color:#FF0000;font-size:13px; text-align:left;margin-bottom:20px;">
                    ※旅客名單的中文姓名為必填欄位，請務必填入所有旅客的中文姓名※<br />
                    ※請注意!英文姓名必須與護照相同，字母間請勿使用任何符號或空白※<br />
                    ※西元生日格式範例：2016/01/23 ※
                </div>

                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="尚未建立資料或未找到您所搜尋的資料" 
                    Width="950px"  CellPadding="3" GridLines="Vertical" AllowPaging="true"
                    RowStyle-BorderWidth="1px" RowStyle-BorderStyle="Solid" BorderStyle="Solid" BorderWidth="1px" BorderColor="#cccccc" AutoGenerateColumns="False">
                    <HeaderStyle BackColor="#EEEEEE" ForeColor="Black" Width="100%" Font-Size="13px" Height="40px" />
                    <RowStyle Font-Size="13px" Height="40px" />
                    <Columns>
                        <asp:TemplateField HeaderText="旅客型別" HeaderStyle-Width="107px" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:Label ID="Lab_type" runat="server" Text='<%# Eval("type")%>' Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="姓名"  HeaderStyle-Width="178px"  HeaderStyle-Font-Bold="false"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:TextBox ID="Txt_name" maxlength='5' runat="server" Width="158px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="護照英文姓"  HeaderStyle-Width="172px"  HeaderStyle-Font-Bold="false"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:TextBox ID="Txt_Ename1" maxlength='20' runat="server" Width="152px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="護照英文名"  HeaderStyle-Width="259px"  HeaderStyle-Font-Bold="false"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:TextBox ID="Txt_Ename2" maxlength='50' runat="server" Width="239px" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="西元生日"  HeaderStyle-Width="133px"  HeaderStyle-Font-Bold="false"  HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center"  >
                            <ItemTemplate>
                                <asp:TextBox ID="Txt_bri" runat="server" Width="113px" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <div style="margin-bottom:40px;float:left; margin-top:40px;">
                    <asp:Button ID="Button3" runat="server" Text="上一步" Height="42px" Width="70px" OnClick="Button3_Click" />
                </div>
                <div style="margin-bottom:40px;float:right; margin-top:40px;">
                    <asp:Button ID="Button4" runat="server" Text="確認報名" Height="42px" Width="70px" OnClick="Button4_Click" OnClientClick="return (confirm('確定報名嗎?') ? showBlockUI() : false )" />
                </div>
            <!-----修改地區 end----->  
            </div>

            <uc2:Foot ID="Foot1" runat="server" />
        </div>

        <asp:HiddenField ID="N" runat="server" />
        <asp:HiddenField ID="count" Value="0" runat="server" />
        <asp:HiddenField ID="c1" Value="0" runat="server" />
        <asp:HiddenField ID="c2" Value="0" runat="server" />
        <asp:HiddenField ID="c3" Value="0" runat="server" />
        <asp:HiddenField ID="c4" Value="0" runat="server" />
        <asp:HiddenField ID="c5" Value="0" runat="server" />
    </div>
  </form>
</body>
</html>
