<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_list.aspx.cs" Inherits="order_list" %>

<!DOCTYPE html>

<%@ Register Src="WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="WebControl/Foot_17.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header_Menu_17.ascx" TagName="Header" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,Chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_17.css" rel="stylesheet" type="text/css" />
    <link href="20151124.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/HeaderMenu17.js"></script>

    <%--特效JS--%>
    <script src="js/HeaderMenu17.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="images/artisan.ico">
    <style>
        #content {
            width: 1400px;
            height: auto;
            float: left;
            margin-top: 100px;
            padding: 50px 10px 0;
            background: #fff;
            border: solid 1px #dcdcdc;
            box-sizing: border-box;
        }

        #GridView1 tr td {
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <nav>
                <uc1:Header ID="Header1" runat="server" />
            </nav>

            <div id="content">
                <div style="text-align: center; width: 950px; margin-left: auto; margin-right: auto;">
                    <div class="form_title">查詢訂單進度</div>
                    <div class="formSearch_tool">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 80px;"></td>
                                <td></td>
                                <td style="width: 150px;"></td>
                                <td style="width: 110px;"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle">報 名 日 期</td>
                                <td align="left" valign="middle">
                                    <asp:TextBox ID="datepicker1" runat="server"></asp:TextBox>
                                    至
                                    <asp:TextBox ID="datepicker2" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="middle">報名狀態　</td>
                                <td valign="middle">
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>全選</asp:ListItem>
                                        <asp:ListItem Value="HK">機位確認</asp:ListItem>
                                        <asp:ListItem Value="RQ">機位候補</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right" valign="middle">
                                    <asp:Button ID="Button1" runat="server" Text="查詢" Width="65px" OnClick="Button1_Click" Height="28px" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="formSearch_tool">
                        <div style="font-size: 13px; letter-spacing: 1px; margin-bottom: 5px; border-top: 1px solid #CCC; padding-top: 20px;">
                            <br />
                            <asp:GridView ID="GridView1" runat="server" BackColor="Silver" BorderColor="Silver" EmptyDataText="<p>尚未建立資料或未找到您所搜尋的資料</p>" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" PageSize="20"
                                OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting" >
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
                                <Columns>
                                    <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />

                                    <asp:TemplateField HeaderText="下單時間" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("EnliI_Date", "{0:yyyy/MM/dd}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="出發日" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Grop_Depa","{0:yyyy/MM/dd}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="團體旅遊名稱" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink Target="_blank" ID="hlTripName" runat="server" 
                                                NavigateUrl='<%# fn_RtnGridViewGrop_Name(Eval("Area_Code", "{0}"), Eval("Grop_Pdf", "{0}"), Eval("Trip_No", "{0}"), Eval("Grop_Depa", "{0:yyyy/MM/dd}"), Eval("Grop_Liner", "{0}"))%>' 
                                                Text='<%#fn_RtnGridViewGroup_Name(Eval("Grop_Name","{0}"),Eval("Grop_Name","{0}"))%>' Style="color: #00a9ef;">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="備註" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Remark", "{0}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="說明會<br/>資料" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img src="Images/icon-info.png" width="16px" height="16px" align="absmiddle" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="報名<br/>人數" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("BookPax", "{0}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="可收訂" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtVAN_HK" runat="server" Text='<%#Eval("VAN_HK", "{0}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="候補" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtVAN_RQ" runat="server" Text='<%#Eval("VAN_RQ", "{0}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="刪除" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtVAN_Cancel" runat="server" Text='<%#Eval("VAN_Cancel", "{0}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="報名單號</br>報名狀態" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlTripName" runat="server" NavigateUrl='<%#"order_list2.aspx?no="+ Eval("netcustnumb") %>' Text='<%#Eval("netcustnumb","{0}")%>' Style="color: #00a9ef;"></asp:HyperLink>
                                            <br/>
                                            <asp:Label ID="Label11" runat="server" Text='<%#check_reg(Eval("Reg_Status", "{0}"),Eval("BookPax", "{0}"),Eval("VAN_HK", "{0}"),Eval("VAN_RQ", "{0}"),Eval("VAN_Cancel", "{0}"))%>' Style='color: #f76800; font-size: 14px; font-weight: bold;'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="訂金DL" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%#Down_pay_time(Eval("EnliI_Date", "{0:yyyy/MM/dd}"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="總額" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("total", "{0}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="left" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="black" />
                                <HeaderStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="black" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                            <br />
                            <div style="letter-spacing: 1px;">報名單號前有<span style="color: #FF0000">*</span>為訂房需求價格有異動，請點<span style="color: #FF0000">報名單號</span>詳閱報名單計價或與<span style="color: #FF0000">當區業務</span>聯絡。</div>
                            <br />
                        </div>
                    </div>
                    <div style="margin-bottom: 40px; float: right;">
                        <asp:Button ID="Button2" runat="server" Text="兌換券" OnClick="Button2_Click" Visible="false" />
                    </div>
                </div>
            </div>
            <uc2:Foot ID="Foot1" runat="server" />
        </div>

        <script type="text/javascript" src="js/jquery.elastislide.js"></script>
        <script type="text/javascript">
            $('#carousel').elastislide({
                imageW: 221,
                minItems: 4,
                onClick: true
            });
        </script>
    </form>
</body>
</html>
