<%@ Page Language="C#" %>

<%@ Register Src="WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web.css" rel="stylesheet" type="text/css" />
    <link href="css/color.css" rel="stylesheet" type="text/css" />
    
    <script src="css/SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <link href="css/SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-artisan-130221.js"></script>
    <link rel="stylesheet" type="text/css" href="css/elastislide.css" />
    <script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Artisan_SubMenu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
<div align="center">
            <asp:Image ID="Image1" runat="server" ImageUrl="images/notes1.jpg" />
            <asp:Image ID="Image2" runat="server" ImageUrl="images/notes2.jpg" /></div>
        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>
