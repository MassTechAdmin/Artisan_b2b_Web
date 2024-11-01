<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="Index_Chose.aspx.cs" Inherits="Trip_index_chose" Title="Untitled Page" %>
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
                            欄位：<asp:DropDownList ID="ddchose_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddchose_type_SelectedIndexChanged">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem Value="1234">無</asp:ListItem>
                            </asp:DropDownList>
                        </td>
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
                                <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" />
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
                        BorderWidth="5px" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <PagerSettings Position="TopAndBottom"></PagerSettings>
                        <RowStyle BackColor="#b1cce7" ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField HeaderText="號碼">
                                <ItemTemplate>
                                <asp:HiddenField ID="hidChose_No" runat="server" Value='<%# Eval("Chose_No") %>' />
                                    <asp:Label ID="lbl_No" runat="server" Text='<%# Eval("Chose_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="圖片">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# "../" + Eval("Chose_Pic") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="超連結" ControlStyle-Width="400px">
                                <ItemTemplate>
                                    <div style='width:400px;'><asp:Label ID="lblChose_Link" runat="server" Text='<%# Eval("Chose_Link") %>'></asp:Label></div>
                                </ItemTemplate>
                                <ItemStyle Width="400px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="排序">
                                <ItemTemplate>
                                    <asp:TextBox ID="txbchose_order" runat="server" Text='<%# Eval("chose_order") %>' Width="20px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="欄位">
                                <ItemTemplate>
                                    <asp:TextBox ID="txbchose_type" runat="server" Text='<%# Eval("chose_type") %>' Width="20px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "Index_Chose_Region.aspx?no=" + Eval("Chose_No") %>' >Edit</asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="hlDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete the record?')"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
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

