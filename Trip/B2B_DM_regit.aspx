<%@ Page Title="" Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="B2B_DM_regit.aspx.cs" Inherits="Trip_B2B_DM_regit" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:Label ID="Label1" runat="server" Text="標題"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox3" runat="server" Height="21px"></asp:TextBox>
        <br>
        <br
        <br>
        <FCKeditorV2:FCKeditor ID="TextBox2" Width="780px" Height="400px" runat="server" BasePath="~/fckEditor/" />
        <br>
        <br>
        <asp:Button ID="Button3" runat="server" Text="修改" OnClick="Button3_Click" />
        <asp:Button ID="Button2" runat="server" Text="SAVE" OnClick="Button2_Click"/>
</asp:Content>

