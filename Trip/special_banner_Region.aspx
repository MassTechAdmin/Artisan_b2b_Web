﻿<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="special_banner_Region.aspx.cs" Inherits="Trip_special_banner_Region" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Image ID="Image1" Width="235px" Height="70px" runat="server" />&nbsp;
    <asp:FileUpload ID="FileUpload1" runat="server" Width="205px" /><br />
    連結：
    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>&nbsp;<asp:Button
        ID="Button1" runat="server" Text="修改" OnClick="Button1_Click" /><asp:Button ID="Button2"
            runat="server" Text="新增" OnClick="Button2_Click" />
    <asp:HiddenField ID="HidNo1" runat="server" />
</asp:Content>

