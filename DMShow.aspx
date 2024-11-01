<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DMShow.aspx.cs" Inherits="DMShow" %>

<!DOCTYPE html>

<%@ Register Src="WebControl/Search.ascx" TagName="Search" TagPrefix="uc3" %>

<%@ Register Src="WebControl/Foot_17.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="WebControl/Header_Menu_17.ascx" TagName="Header" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,Chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="css/web_17.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/HeaderMenu17.js"></script>
    
    <%--特效JS--%>
    <script src="js/HeaderMenu17.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="images/artisan.ico"> 
<style>
#content {
    width: 1400px;
    height: auto;
    float: left;
    margin-top: 100px;
    text-align: center;
    background: #fff;
    border: solid 1px #dcdcdc;
    padding: 10px;
    box-sizing: border-box;
}
</style>
</head>
<body>
<form id="form1" runat="server">
    <div id="wrapper">
        <nav>
            <uc1:Header ID="Header1" runat="server" />
        </nav>
        <div id="content">
            <!-----修改地區----->
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <!-----修改地區 end----->
        </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>