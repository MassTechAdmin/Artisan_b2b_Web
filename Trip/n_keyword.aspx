<%@ Page Title="" Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="n_keyword.aspx.cs" Inherits="Trip_n_keyword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td style="width: 65px">
                </td>
            <td style="width: 530px">
                左邊標題</td>
        </tr>
        <tr>
            <td style="width: 65px">
                <asp:Button ID="Button2" runat="server" Text="新增" OnClick="Button2_Click" /></td>
            <td style="width: 530px">
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" Width="309px"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" PagerSettings-Position="TopAndBottom" CellPadding="4" 
        ForeColor="#333333" BorderColor="#0066CC" BorderStyle="Double" 
        BorderWidth="5px" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging">
        <PagerSettings Position="TopAndBottom"></PagerSettings>
        <RowStyle BackColor="#b1cce7" ForeColor="#333333" />
        <Columns>
            <asp:ButtonField Text="SingleClick"  CommandName="SingleClick" Visible="False"/>
            <asp:TemplateField HeaderText="自動編號" SortExpression="自動編號">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                    <asp:HiddenField ID="HidKeyWord_No" Value='<%# Eval("n_key_No") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="排序">
                 <ItemTemplate>
                     <asp:TextBox ID="TxbOrder" runat="server" Text='<%# Eval("n_key_Order") %>' Width="20px"></asp:TextBox>
                  </ItemTemplate>
                 <ItemStyle Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="標題">
                 <ItemTemplate>
                     <asp:Label ID="lblArea" runat="server" Text='<%# Eval("n_key_Title") %>'></asp:Label>
                  </ItemTemplate>
                 <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="超連結">
                 <ItemTemplate>
                     <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("n_key_Link") %>'></asp:Label>
                  </ItemTemplate>
                 <ItemStyle Width="100px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="編輯">
           <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "n_keyword_Edit.aspx?no=" + Eval("n_key_No") %>'>Edit</asp:HyperLink>
            </ItemTemplate>
           <ItemStyle Width="50px" />
           </asp:TemplateField>
            <asp:TemplateField HeaderText="刪除">
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
</asp:Content>
