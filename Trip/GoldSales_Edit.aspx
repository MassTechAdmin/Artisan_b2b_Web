﻿<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="GoldSales_Edit.aspx.cs" Inherits="Trip_GoldSales_Edit" Title="Untitled Page" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 700px">
        <tr>
            <td colspan="2" style="text-align: center"><asp:Label ID="Label1" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: right">標題</td>
            <td style="width: 400px">
                <asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: right">地區編號</td>
            <td style="width: 400px">
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: right">超連結</td>
            <td style="width: 400px">
                <asp:TextBox ID="TextBox2" runat="server" Width="400px"></asp:TextBox>
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
               ID="Button2" runat="server" Text="取消" PostBackUrl="~/Trip/GoldSales.aspx" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="HidNo" runat="server" />
</asp:Content>

