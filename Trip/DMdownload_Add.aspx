﻿<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="DMdownload_Add.aspx.cs" Inherits="DMdownload_Add" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    地區：<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
    <br /><br />
    DM標題：<asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
    <br /><br />
    <radCln:RadCalendar ID="RadCalendar1" runat="server" Width="200px" Height="200px" TitleAlign="Center" TitleFormat="yyyy/MMM/dd  ▼" BackColor="White" CssClass="12" BorderColor="#b3b3b3" BorderWidth="1px" BorderStyle="Solid">
    </radCln:RadCalendar>
    到期日：<radCln:RadDatePicker id="RadDatePicker1" SharedCalendarID="RadCalendar1" runat="server" Width="100px"></radCln:RadDatePicker>
    <br /><br />
    檔案：<asp:FileUpload ID="FileUpload1" runat="server" />
    <br /><br />
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="新增" OnClick="Button1_Click" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Button ID="Button2" runat="server" Text="取消"  PostBackUrl="../Trip/DMdownload_select.aspx" />
</asp:Content>