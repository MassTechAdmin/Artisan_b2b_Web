<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="RecommendSales.aspx.cs" Inherits="RecommendSales" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    推薦行程
    <table>
        <tr style="width: 65px">
            <td>
                <asp:Button ID="Button2" runat="server" Text="新增" OnClick="Button2_Click" /></td>
            <td>
                <asp:DropDownList ID="ddlGCName" runat="server">
                    <asp:ListItem Value="1">巨匠推薦行程</asp:ListItem>
                    <asp:ListItem Value="2">日本精選</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" Width="309px"></asp:TextBox>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
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
            <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="False" />
            <asp:TemplateField HeaderText="自動編號" SortExpression="自動編號">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Style="text-align: center" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                    <asp:HiddenField ID="HidKeyWord_No" Value='<%# Eval("Rec_No") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="排序">
                <ItemTemplate>
                    <asp:TextBox ID="TxbOrder" runat="server" Text='<%# Eval("Rec_Order") %>' Width="30px" Style="text-align: center"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="推薦行程">
                <ItemTemplate>
                    <asp:Label ID="lblRec_GroupName" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="地區">
                <ItemTemplate>
                    <asp:Label ID="lblg_area" runat="server" Text='<%# Eval("Area_Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="標題">
                <ItemTemplate>
                    <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Rec_Title") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" />
            </asp:TemplateField>
            <%--            <asp:TemplateField HeaderText="價格">
                 <ItemTemplate>
                     <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Rec_Pirce") %>'></asp:Label>
                  </ItemTemplate>
                 <ItemStyle Width="100px" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%# "RecommendSales_Edit.aspx?no=" + Eval("Rec_No") %>'>Edit</asp:HyperLink>
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
</asp:Content>

