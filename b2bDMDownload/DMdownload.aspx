<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DMdownload.aspx.cs" Inherits="DMdownload" %>

<%@ Register Src="/WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="/WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport">
    <title>凱旋旅行社(巨匠旅遊)同業網</title>
	<script src="../js/jquery-1.6.min.js"></script>
    <link rel="stylesheet" href="../css/layout_b2b.css">
    <!-- main css -->
    <link rel="stylesheet" href="css/dm_b2b.css">
	<script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
</head>
<body>
	<form id="form1" runat="server">
		<!--b2b header-->
            <header>
                <nav>
                    <uc1:Header ID="Header_Menu1" runat="server" />
                </nav>
            </header>
        <!--/b2b header-->
		<div id="wrapper">
					<div id="b2b-content-tool" class="dm-content">
				<div class="dm-top">
					<h1 class="dm-title">直客DM下載</h1>
					<asp:DropDownList runat="server" class="dm-select" ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
				</div>
				<!--DM-->
				<asp:Literal ID="Literal1" runat="server"></asp:Literal>
				<!--DM end-->
			</div>
		</div>
		<!--b2b footer-->
            <uc2:Foot ID="Foot1" runat="server" />
        <!--/b2b footer-->
    </form>
</body>
</html>