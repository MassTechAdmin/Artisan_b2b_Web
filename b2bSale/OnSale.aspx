<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnSale.aspx.cs" Inherits="OnSale" %>
<%@ Register Src="~/WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport">
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link rel="stylesheet" href="/css/owl.carousel.css">
    <link rel="stylesheet" href="/css/owl.theme.default.css">
    <link rel="stylesheet" href="/css/layout_b2b.css">
    <link rel="stylesheet" href="/css/list_b2b.css">
    <!-- main css -->
    <link rel="stylesheet" href="css/bttn.min.css">
    <link rel="stylesheet" href="css/sale_b2b.css">
    <!-- flatpicker -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script type="text/javascript" src="/js/jquery-1.9.1.min.js"></script>
    <!-- flatpicker -->
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    
    <script>
        $(window).load(function() {
            //整頁捲動-------?被點
            $('#menu li').click(function() {
                //?被捲動
                indexNo = $(this).index();
                wdth = $(window).width();
                if (wdth < 500) {
                    targetTop = $('#main section').eq(indexNo).position().top - 100;
                } else {
                    targetTop = $('#main section').eq(indexNo).position().top - 115;
                }
                $('html,body').animate({
                    scrollTop: targetTop
                }, 500);
            });
        });

    </script>
</head>
<body>
<form id="form1" runat="server">
    <div id="wrapper">
        <nav>
            <uc1:Header ID="Header1" runat="server" />
        </nav>
        <!-- b2b content -->
        <div id="b2b-content-tool">
            <div id="sale-content">
                <header class="sale-header"><img src="http://b2b.artisan.com.tw/Zupload/000002/00000201.png" alt="header"></header>
                        <!-- 程式抓系統分類 -->
                        <asp:Literal ID="Area" runat="server"></asp:Literal>


                    <div id="main">
                        <!-- 程式抓系統**團 -->
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
            </div>
        </div>
        <!-- /b2b content -->
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    </form>
</body>
</html>