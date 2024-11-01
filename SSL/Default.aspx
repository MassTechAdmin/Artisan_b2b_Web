<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SSL_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <br />
        DNS  &nbsp; &nbsp;
        <asp:Button ID="btnGoogle" runat="server" Text="Google" OnClick="btnGoogle_Click" /> &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Button ID="btnArtisan" runat="server" Text="Artisan" OnClick="btnArtisan_Click" />
        <br /><br />
        <hr />
        <br />

<asp:DropDownList class="main" id="WhoisServer" runat="server">
    <asp:ListItem value="whois.networksolutions.com">whois.networksolutions.com (.COM, .NET, .EDU)</asp:ListItem>
    <asp:ListItem value="whois.ripe.net">whois.ripe.net(Europe)</asp:ListItem>
    <asp:ListItem value="whois.cira.ca">whois.cira.ca (.CA)</asp:ListItem>
    <asp:ListItem value="whois.nic.uk">whois.nic.uk(.CO.UK)</asp:ListItem>
    <asp:ListItem value="whois.domain-registry.nl">whois.domain-registry.nl (.NL)</asp:ListItem>

    <asp:ListItem value="whois.internic.net">whois.internic.net</asp:ListItem>
    <asp:ListItem value="whois.arin.net">whois.arin.net</asp:ListItem>
    <asp:ListItem value="whois.twnic.net.tw">whois.twnic.net.tw</asp:ListItem>
    <asp:ListItem value="whois.verisign-grs.net">whois.verisign-grs.net</asp:ListItem>
    <asp:ListItem value="whois.comlaude.com">whois.comlaude.com</asp:ListItem>
</asp:DropDownList><br />
        <input type="text" class="main" name="DomainName" value=""/><br />
        <asp:Button ID="btnDomainName" runat="server" OnClick="btnDomainName_Click" Text="Domain Name" /><br />
        <asp:Label class="main" id="lblResponse" runat="server"/> 
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="IP" Width="131px" />
    </form>
</body>
</html>
