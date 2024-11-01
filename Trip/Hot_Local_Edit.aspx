<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="Hot_Local_Edit.aspx.cs" Inherits="Trip_Hot_Local_Edit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table style="width: 700px">
        <tr>
            <td colspan="2" style="text-align: center"><asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: right">內容</td>
            <td style="width: 400px">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: right">
            </td>
            <td style="width: 400px">
                <asp:Button ID="Button1" runat="server" Text="確認"  OnClick="Button1_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button
               ID="Button2" runat="server" Text="取消" PostBackUrl="~/Trip/Sales_News_select.aspx" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="HidNo" runat="server" />
</asp:Content>

