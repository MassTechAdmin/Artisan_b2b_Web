<%@ Page Language="C#"%>

<%@ Register Src="../../WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="../../WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register src="../../WebControl/Island_Tab.ascx" tagname="Island_Tab" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../css/web.css" rel="stylesheet" type="text/css" />
    <link href="../../css/color.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../../js/Artisan_SubMenu.js" type="text/javascript"></script>
    <link href="../../css/Artisan_SubMenu.css" rel="stylesheet" type="text/css" /></head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
            
<div id="island_topBanner"><img src="../../images/island/banner.jpg" /></div>

<uc3:Island_Tab ID="Island_Tab1" runat="server" />

<div class="island_hotel_info"><a href="http://reethirah.oneandonlyresorts.com/" target="_blank"><img src="../../images/island/hotel/ReethiRah_info.jpg" border="0" /></a>

</div>




        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
