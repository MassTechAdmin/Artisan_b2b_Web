<%@ Page Title="" Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="B2B_DM_select.aspx.cs" Inherits="Trip_B2B_DM_select" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
       <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="B2B_DM_Regit.aspx" Text="新增"></asp:HyperLink>
        <br />
        
        <br>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
       <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" EmptyDataText="<p>尚未建立資料或未找到您所搜尋的資料</p>"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound" 
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#FFFFFF" ForeColor="Black" />
                    <Columns>
                         <asp:TemplateField HeaderText="編輯">
                            <ItemTemplate>
                                <asp:HyperLink ID="linkABR_No" NavigateUrl='<%# Eval("pic_num","B2B_DM_Regit.aspx?no={0}") %>' Text="編輯" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="編號" ControlStyle-Width="50"  >
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("pic_num")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="標題" ControlStyle-Width="100">
                            <ItemTemplate>
                               <asp:Label ID="Label2" runat="server" Text='<%# Eval("pic_name", "{0}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="超連結" ControlStyle-Width="300">
                            <ItemTemplate>
                               <asp:Label ID="labelLink" runat="server" Text='<%# Eval("pic_num", "http://b2b.artisan.com.tw/DMShow.aspx?no={0}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="圖片" ControlStyle-Width="80" Visible="false">
                            <ItemTemplate>
                               <asp:Label ID="Label3" runat="server" Text='<%#Eval("pic_add", "{0}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="刪除" ControlStyle-Width="80">
                            <ItemTemplate>
                             <asp:Button ID="Button1" runat="server" Text="刪除"  CommandName="del" CommandArgument='<%# Eval("pic_num", "{0}")%>' />   
                            </ItemTemplate>
                        </asp:TemplateField>         
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="black" />
                    <HeaderStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="black" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                </asp:GridView>
</asp:Content>

