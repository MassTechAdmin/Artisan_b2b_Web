<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="ThisWeek.aspx.cs" Inherits="Trip_ThisWeek" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="2" style="color:#002469">
                                <asp:Label ID="LblTitle" runat="server" Font-Size="16px" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                標題： 
                                <asp:TextBox ID="TxbTitle" runat="server"></asp:TextBox>
                                &nbsp;<asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                                <span style="color:Black;">
                                    Show
                                    <asp:DropDownList ID="DdlPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlPage_SelectedIndexChanged">
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;items per page
                                </span>
                            </td>
                            <td style="text-align:right;">
                                <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        AllowPaging="True" PagerSettings-Position="TopAndBottom" CellPadding="4" 
                        ForeColor="#333333" BorderColor="#0066CC" BorderStyle="Double" 
                        BorderWidth="5px" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="15">
                        <PagerSettings Position="TopAndBottom"></PagerSettings>
                        <RowStyle BackColor="#B1CCE7" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField HeaderText="號碼">
                                <ItemTemplate>
                                <asp:HiddenField ID="hidChose_No" runat="server" Value='<%# Eval("Week_No") %>' />
                                    <asp:Label ID="lbl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
			                <asp:TemplateField HeaderText="排序">
                                <ItemTemplate>
                                    <asp:TextBox ID="txbchose_order" runat="server" Text='<%# Eval("Week_Order") %>' Width="20px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "ThisWeek_Region.aspx?no=" + Eval("Week_No") %>' >Edit</asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="hlDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete the record?')"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="圖片">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "../images/simple/" + Eval("Week_Pic") %>' Width="122px" />
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="標題">
                                <ItemTemplate>
                                    <asp:Label ID="lblWeekTitle" runat="server" Text='<%# Eval("Week_Title") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="超連結" ControlStyle-Width="500px">
                                <ItemTemplate>
                                    <asp:Label ID="lblChose_Link" runat="server" Text='<%# Eval("Week_Link") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="500px" />
                            </asp:TemplateField>
                            
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    <asp:Button ID="Button1" runat="server" Text="確認" OnClick="Button1_Click" />
                    <span style="color:Black;">
                        Show
                        <asp:DropDownList ID="DdlPage2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlPage2_SelectedIndexChanged">
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;items per page
                    </span>
                </td>
            </tr>
        </table>
</asp:Content>

