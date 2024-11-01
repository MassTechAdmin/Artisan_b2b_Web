<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="WebControl_Search" %>

<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>

                <div id="TabbedPanels1" class="TabbedPanels">
                  <ul class="TabbedPanelsTabGroup">
                    <li class="TabbedPanelsTab" tabindex="0">團體旅遊</li>
                    <li class="TabbedPanelsTab" tabindex="0">熱門旅遊地</li>
                  </ul>
                  <div class="TabbedPanelsContentGroup">
                    <div class="TabbedPanelsContent">
                        <div class="route_search_pic"><asp:Image ID="Image3" runat="server" ImageUrl="~/images/index/icon01.png" Width="15" Height="13" />出發日期</div>
                        <div class="route_data">
                            <radCln:RadDatePicker ID="RadDatePicker1" runat="server" Width="175px" MaxDate="2099-12-31">
                            </radCln:RadDatePicker>
                        </div>
                        <div class="route_search_pic">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;至</div>  
                        <div class="route_data">
                            <radCln:RadDatePicker ID="RadDatePicker2" runat="server" Width="175px" MaxDate="2099-12-31">
                            </radCln:RadDatePicker>
                        </div>
                        <div class="route_search_pic"><asp:Image ID="Image2" runat="server" ImageUrl="~/images/index/icon01.png" Width="15" Height="13" />&nbsp;&nbsp;目的地</div>  
                        <div class="route_select">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="75px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"/>
                            <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="SecClass_Name" DataValueField="SecClass_No" Width="100px"/>
                        </div>
                        <div class="route_search_pic">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;關鍵字</div>  
                        <span class="route_data">
                            <asp:TextBox ID="txbKey" runat="server" Width="175px"></asp:TextBox></span>
                        <div style="clear: both;padding-top:5px;padding-bottom:10px;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/index/line_route_search.jpg" /></div>
                        <div style="clear: both;">
                            <div style="float:left;padding-left:180px;">
                                <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/images/index/search.jpg" OnClick="imgButton_Click" />
                            </div>
                        </div>  
                    </div>
                    <div class="TabbedPanelsContent">
                      <table width="92%" height="159" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td style="border-bottom: dashed #a1a1a1 1px;"><div class="keyword">主　題：　<a href="#"><span class="keyword_blue" style="font-size:14px;"><strong>跨年</strong></span></a>　<a href="#">迎曙光</a>　<a href="#"><span class="keyword_blue" style="font-size:14px;">威尼斯嘉年華</span></a></div></td>
                        </tr>
                        <tr>
                          <td style="border-bottom: dashed #a1a1a1 1px;"><div class="keyword">季　節：　<a href="#"><span class="keyword_blue" style="font-size:14px;">美國免簽</span></a>　<a href="#">雪季</a>　<a href="#">折扣季</a></div></td>
                        </tr>
                        <tr>
                          <td style="border-bottom: dashed #a1a1a1 1px;"><div class="keyword">城　市：　<a href="#">瑞士</a>　<a href="#">荷蘭</a>　<a href="#">巴黎</a>　<a href="#"><span class="keyword_blue" style="font-size:14px;"><strong>義大利</strong></span></a></div></td>
                        </tr>
                        <tr>
                          <td><div class="keyword">風　情：　<a href="#">蜜月</a>　<a href="#">郵輪</a>　<a href="#"><span class="keyword_blue">少年pi</span></a>　<a href="#">韓國</a></div></td>
                        </tr>
                      </table>
                    </div>
                  </div>
                </div>