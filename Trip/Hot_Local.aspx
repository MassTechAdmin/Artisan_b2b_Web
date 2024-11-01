<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="Hot_Local.aspx.cs" Inherits="Trip_Hot_Local" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table>
        <tr>
            <td style="width: 65px">
                </td>
            <td style="width: 164px">
                特惠促銷新增</td>
        </tr>
        <tr>
            <td style="width: 65px">
                <asp:Button ID="Button2" runat="server" Text="新增" OnClientClick="if (confirm('確定新增嗎？')==false) {return false;} { if(_docheck() == false) {return false;}}" OnClick="Button2_Click" /></td>
            <td style="width: 164px">
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" Width="145px"></asp:TextBox></td>
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
                    <asp:HiddenField ID="hidNo" runat="server" Value='<%# Eval("Glb_Id") %>' />
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Glb_Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="促銷名稱">
                 <ItemTemplate>
                     <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Descrip") %>'></asp:Label>
                  </ItemTemplate>
                 <ItemStyle Width="100px" />
           </asp:TemplateField>
           <asp:TemplateField HeaderText="排序" >
                 <ItemTemplate>
                     <asp:TextBox ID="txbOrder" runat="server" Text='<%# Eval("glb_Order") %>' Width='40px' ></asp:TextBox>
                  </ItemTemplate>
                 <ItemStyle Width="40px" />
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Edit">
           <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "Hot_Local_Edit.aspx?no=" + Eval("Glb_Id") %>'>Edit</asp:HyperLink>
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
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="取消" PostBackUrl="~/trip/hot_local.aspx" />
</asp:Content>

