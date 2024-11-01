<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="ThisWeek_Region.aspx.cs" Inherits="Trip_ThisWeek_Region" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tr id="okd" runat="server">
        <td>標題：<asp:TextBox ID="txbTitle" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="Image1" Width="122px" Height="91px" runat="server" />&nbsp;
            <asp:FileUpload ID="FileUpload1" runat="server" Width="205px" /><br />
            連結：
            <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>&nbsp;<asp:Button
        ID="Button1" runat="server" Text="修改" OnClick="Button1_Click" />
        </td>
    </tr>
    <tr>
        <td>
        <asp:Button ID="Button2" runat="server" Text="新增" OnClick="Button2_Click" />
        </td>
    </tr>
</table>
    <asp:HiddenField ID="HidNo1" runat="server" />
</asp:Content>

