<%@ Page Language="C#" MasterPageFile="~/Trip/MasterPage.master" AutoEventWireup="true" CodeFile="Hot_Local_Region.aspx.cs" Inherits="Trip_Hot_Local_Region" Title="Untitled Page" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <br /><br />
    <table>
        <tr>
            <td colspan="1" style="text-align: center">
                特惠促銷</td>
        </tr>
        <tr>
            <td colspan="1">
                特惠促銷：<asp:DropDownList ID="ddlHot_Local" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHot_Local_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="1">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                <FCKeditorV2:FCKeditor ID="fckHL_Content" runat="server" BasePath="~/fckEditor/" Height="500px" Width="600px" ToolbarSet="Default">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" 
                    Text="確認存檔" /></td>
        </tr>
    </table>
</asp:Content>

