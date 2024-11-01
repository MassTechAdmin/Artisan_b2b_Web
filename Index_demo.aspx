<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index_demo.aspx.cs" Inherits="Index_demo" %>

<%@ Register Src="WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport" />
    <title>凱旋旅行社(巨匠旅遊)同業網</title>
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <link rel="stylesheet" href="./css/owl.theme.default.css" />
    <link rel="stylesheet" href="./css/layout_b2b.css?20201015" />
    <!-- flatpicker -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css" />
    <!-- font -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans+TC:wght@100..900&display=swap" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <!-- /flatpicker -->
    <script type="text/javascript" src="./js/jquery-2.1.3.min.js"></script>
    <script src="./js/owl.carousel.min.js"></script>
    <script src="./js/jquery.scrollbox.min.js"></script>
    <style type='text/css'>
        .go_box img,
        .back_box img {
            position: absolute;
            width: 15px;
            height: 15px;
            top: 13px;
            right: 15px;
            pointer-events: none;
        }

        img {
            border: 0;
            width: 100%;
            max-width: 100%;
            height: auto;
        }
    </style>
</head>
<body>
    <div id="top-bar" style="bottom: 0px;">
        <div>
            <div id="abgne_float_ad">
                <span class="abgne_close_ad">[X] Close</span>
                <p class="lineService">線上客服</p>
                <a href="https://lin.ee/4nNoP2Z" target="_blank">
                    <img src="./Images/line_QRcode_2.jpg" alt="">
                </a>
            </div>
        </div>
    </div>

    <form id="form1" runat="server">
        <div id="wrapper">
            <!--b2b header-->
            <header>
                <nav>
                    <uc1:Header ID="Header_Menu1" runat="server" />
                </nav>
            </header>
        </div>
        <!-- search + banner -->
        <div class="area-1 animation__el in">
            <section class="search-area">
                <div class="b2b-search-tool">
                    <h1>團體旅遊</h1>
                    <div class="b2b-search-area">
                        <div width="0" border="0" cellspacing="0" cellpadding="0">
                            <ul>
                                <li class="search-title">出發日期</li>
                                <li class="search-box search-datepicker">
                                    <input class="search-textBox" id="txtDatePicker1" runat="server" /><img src="./images/calendar.svg" alt="" class="date-icon" />
                                    <span>到</span>
                                    <input class="search-textBox" id="txtDatePicker2" runat="server" /><img src="./images/calendar.svg" alt="" class="date-icon" />
                                </li>
                            </ul>

                            <ul>
                                <li class="search-title">系列</li>
                                <li>
                                    <asp:DropDownList ID="DropDownList0" runat="server" class="search-textBox">
                                        <asp:ListItem Value="" Selected>全部系列</asp:ListItem>
                                        <asp:ListItem Value="巨匠">巨匠</asp:ListItem>
                                        <asp:ListItem Value="新視界">新視界</asp:ListItem>
                                        <asp:ListItem Value="典藏">典藏</asp:ListItem>
                                    </asp:DropDownList>
                                    <img src="./images/selectArrow.svg" alt="" class="select-icon">
                                </li>
                            </ul>
                            <ul>
                                <li class="search-title">出發地</li>
                                <li>
                                    <asp:DropDownList ID="ddlAirport" runat="server" class="search-textBox">
                                        <asp:ListItem Value="" Selected>全部出發地</asp:ListItem>
                                        <asp:ListItem Value="TSA">台北</asp:ListItem>
                                        <asp:ListItem Value="TPE">桃園</asp:ListItem>
                                        <asp:ListItem Value="RMQ">台中</asp:ListItem>
                                        <asp:ListItem Value="KHH">高雄</asp:ListItem>
                                    </asp:DropDownList>
                                    <img src="./images/selectArrow.svg" alt="" class="select-icon">
                                </li>
                            </ul>
                            <ul>
                                <li class="search-title">目的地</li>
                                <li>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" class="search-textBox" />
                                    <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="SecClass_Name" DataValueField="SecClass_No" class="search-textBox" />
                                </li>
                            </ul>
                            <ul>
                                <li class="search-title">關鍵字</li>
                                <li>
                                    <asp:TextBox ID="txbKey" runat="server" class="search-textBox" />
                                </li>

                            </ul>
                            <ul class="radio-area">
                                <li class="chk-title"></li>
                                <li>
                                    <asp:CheckBox ID="chkOK" runat="server" Text="已成團" />
                                </li>
                                <li>
                                    <asp:CheckBox ID="chkSign" runat="server" Text="可報名" />
                                </li>
                                <li style="display:none;">
                                    <asp:CheckBox ID="chkB2B_Only" runat="server" Text="同業專賣" Checked="true" />
                                </li>
                            </ul>
                            <ul>
                                <li colspan="2" align="center">
                                    <asp:Button ID="imgButton" runat="server" OnClick="imgButton_Click" Text="搜尋" class="search-box-btn" />
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- /search -->
                <!--b2b top-->
                <div id="b2b-top" class="top-mobile animation__el in">
                    <div class="b2b-banner-news">
                        <span>Final Call ‣‣‣ </span>
                        <marquee direction="left" width="100%" align="botton" scrollamount="3">
                            <asp:Literal ID="litnews" runat="server"></asp:Literal>
                        </marquee>
                    </div>
                    <div id="user-info">
                        <asp:Literal ID="litUserInfo" runat="server" />
                        <%--                        <div class='user-info-list'>負責業務：<span>王承御</span></div>
                        <div class='user-info-list'>電子郵件：<span>max_wang@artisan.com.tw</span></div>
                        <div class='user-info-list'>公司電話：<span>02-2518-0011</span></div>
                        <div class='user-info-list'>傳真：<span>02-2518-5488</span></div>
                        <div class='user-info-list'>手機：<span>0916-643-109</span></div>--%>
                    </div>
                </div>
                <!--/b2b top-->

                <!--banner -->
                <div id="b2b-alert">
                    <asp:Literal ID="litHomeNews" runat="server"></asp:Literal>
                    <asp:Literal ID="litHomeBanner" runat="server"></asp:Literal>
                    <%--                    <div class="b2b-alert-AD">
                        <div class="owl-carousel owl-theme" id="B2bAD-owl-carousel">
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20200408000007' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20230808105758.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20200407000002' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20230808105758.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231020000001' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20230808105758.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&RadDatePicker1=2024-03-21&RadDatePicker2=2024-09-21&area=35&sgcn=153&tp=%e7%b4%ab%e8%89%b2' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20240321160855.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&RadDatePicker1=2024-04-01&RadDatePicker2=2024-06-30&area=35&sgcn=158' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20240321160012.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240119000005' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20240130100159.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&tp=%E8%92%94%E5%85%89' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20231114103914.jpg" alt=""></a>
                            <a href='https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=7&sgcn=58' target='_bank'>
                                <img src="./NewPageFile/Zupload/HomeBanner/20230808105758.jpg" alt=""></a>
                        </div>
                        <span class="alert-AD-btn">收合▲</span>
                    </div>--%>
                </div>

                <!--banner-->
            </section>
        </div>



        <!--b2b top-->
        <section id="b2b-top" class="top-web animation__el in">
            <div class="b2b-banner-news">
                <span>Final Call ‣‣‣ </span>
                <marquee direction="left" width="100%" align="botton" scrollamount="3">
                </marquee>
            </div>
            <div id="user-info">
                <div class='user-info-list'>負責業務：<span>王承御</span></div>
                <div class='user-info-list'>電子郵件：<span>max_wang@artisan.com.tw</span></div>
                <div class='user-info-list'>公司電話：<span>02-2518-0011</span></div>
                <div class='user-info-list'>傳真：<span>02-2518-5488</span></div>
                <div class='user-info-list'>手機：<span>0916-643-109</span></div>
            </div>
        </section>
        <div id="b2b-content-index">
            <!--/b2b top-->
            <!-- AD-scroll -->
            <section id="AD-Sale-scroll" class="scroll-text">
                <ul id="AD-Sale-scroll2" class="owl-carousel owl-theme">
                    <asp:Literal ID="litSB1" runat="server" />
                    <%--<li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&RadDatePicker1=2023-12-29&RadDatePicker2=2024-06-29&area=37"
                        target="_blank">
                        <img src="./Images/b2b_dm_download.jpg" alt="" width="100%"></a>
                    </li>
                    <li><a href="https://www.luxetravel.com.tw/" target="_blank">
                        <img
                            src="./NewPageFile/20210929152156.jpg" alt="" width="100%"></a></li>
                    <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=7&sgcn=58"
                        target="_blank">
                        <img src="./NewPageFile/20221123150458.jpg" alt="" width="100%"></a>
                    </li>
                    <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=1&sgcn=3" target="_blank">
                        <img
                            src="./NewPageFile/20230308182357.jpg" alt="" width="100%"></a>
                    </li>
                    <li><a href="https://b2b.artisan.com.tw/exh/Exhibition.aspx?area=6" target="_blank">
                        <img
                            src="./NewPageFile/20221123162749.jpg" alt="" width="100%"></a></li>
                    <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=2&sgcn=28"
                        target="_blank">
                        <img src="./NewPageFile/20230113150126.jpg" alt="" width="100%"></a>
                    </li>--%>
                </ul>
            </section>
            <!-- hot local -->
            <section class="hot-local">
                <div class="Artisan-Recommend-tool animation__el in">
                    <h1>巨匠系列推薦行程</h1>
                    <ul class="Artisan-Recommend-area">
                        <asp:Literal ID="RecSale" runat="server"></asp:Literal>
                        <%--                        <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20220808000001">義大利
                                卡不里島
                                阿瑪菲海岸11天(333專案)</a></li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231227000003">五星方舟西葡雙宿雙飛13天</a>
                        </li>
                        <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20220815000002">金色夢幻
                                奧地利･捷克10/11天</a></li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230704000004">葡萄牙·藍瓷山水11天</a>
                        </li>
                        <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231213000002">黃金德瑞
                                景觀列車
                                雙峰 國王湖 13天</a></li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231212000009">北歐五國．冰島縮影．挪威經典雙峽灣15天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230913000002">《星心相印》秘魯『蘆葦浮島．馬丘比丘．穿越安地斯山脈』１３天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20220923000006">紐航直飛『雙皇后．珍饌哈比村．庫克山隱士廬．南北島』１０天</a>
                        </li>--%>
                    </ul>
                </div>
                <div class="b2b-hotLocal animation__el in">
                    <h1>日本精選</h1>
                    <ul class="b2b-hotLocal_area">
                        <asp:Literal ID="RecSale2" runat="server"></asp:Literal>
                        <%--<li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240227000001">花海富良野．AOAO水族館．哈蜜瓜三大蟹吃到飽．北海道５天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240302000001">函館限定小丑漢堡．羅曼蒂克小樽．螃蟹吃到飽．北海道5天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240309000002">【來去離島住一晚】禮文利尻北海道跳島5天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231117000001">指宿砂浴．玉手箱列車．保證入住宮崎喜來登．九洲5天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231011000002">【越捷直飛】五星富國島、水都威尼斯、跨海纜車、跳島6天</a>
                        </li>--%>
                    </ul>
                </div>
                <div class="b2b-hotLocal animation__el in">
                    <h1>亞洲熱銷</h1>
                    <ul class="b2b-hotLocal_area">
                        <asp:Literal ID="litBannerRight" runat="server"></asp:Literal>
                        <%--
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240227000001">花海富良野．AOAO水族館．哈蜜瓜三大蟹吃到飽．北海道５天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240302000001">函館限定小丑漢堡．羅曼蒂克小樽．螃蟹吃到飽．北海道5天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20240309000002">【來去離島住一晚】禮文利尻北海道跳島5天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231117000001">指宿砂浴．玉手箱列車．保證入住宮崎喜來登．九洲5天</a>
                        </li>
                        <li><a
                            href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231011000002">【越捷直飛】五星富國島、水都威尼斯、跨海纜車、跳島6天</a>
                        </li>--%>
                    </ul>
                </div>
            </section>


            <!-- Trip List -->
            <div class="b2b-trip-list">
                <div class="trip-hand" style="display: none;">
                    <div class="gaint-select">
                        <a href="exh/Exhibition.aspx?tourtype=3">
                            <h2>巨匠旅遊系列</h2>
                        </a>
                    </div>
                    <div class="luxe-select">
                        <a href="exh/Exhibition.aspx?tourtype=4">
                            <h2>珍藏旅遊系列</h2>
                        </a>
                    </div>
                </div>
                <div class="trip-wrap">
                    <asp:Literal ID="litNot" runat="server"></asp:Literal>
<%--                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=1" class="trip-box-more" target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>中西歐</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231127000003" target='_bank'>
                                <p class="ellipsis">瑞士 懸崖奇景菲斯特 二大遊船 三大纜車 四大觀景列車 五大名峰 13天</p>
                                <span class="price">199,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231213000002" target='_bank'>
                                <p class="ellipsis">黃金德瑞 景觀列車 雙峰 13天</p>
                                <span class="price">148,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230818000005" target='_bank'>
                                <p class="ellipsis">小英倫 湖區彼得兔 巨石陣 OUTLET趣 9 天</p>
                                <span class="price">94,900</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=2" class="trip-box-more" target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>日本</h3>
                        </div>
                        <ul>
                            <li><a href="#" target='_bank'>
                                <p class="ellipsis">關東系列</p>
                                <span class="price">33,000</span>
                            </a></li>
                            <li><a href="#" target='_bank'>
                                <p class="ellipsis">關西系列</p>
                                <span class="price">33,000</span>
                            </a></li>
                            <li><a href="#" target='_bank'>
                                <p class="ellipsis">北海道系列</p>
                                <span class="price">40,000</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=5" class="trip-box-more" target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>南歐</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20221227000002"
                                target='_bank'>
                                <p class="ellipsis">相約義大利10天</p>
                                <span class="price">78,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231123000001"
                                target='_bank'>
                                <p class="ellipsis">Hola！España 西班牙 藝術建築11天</p>
                                <span class="price">119,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231207000006"
                                target='_bank'>
                                <p class="ellipsis">摩洛哥 金色撒哈拉沙漠11天</p>
                                <span class="price">89,900</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=6" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>大陸</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231214000008"
                                target='_bank'>
                                <p class="ellipsis">新甘絲路喀納斯+禾木村、火星一號基地13天</p>
                                <span class="price">70,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231124000001"
                                target='_bank'>
                                <p class="ellipsis">玩轉北疆、喀納斯+禾木村、烤全羊+歌舞宴12天</p>
                                <span class="price">70,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231213000001"
                                target='_bank'>
                                <p class="ellipsis">饗宴絲路敦煌3晚、飛天歌舞秀、摘星閣早餐13天</p>
                                <span class="price">69,900</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=44" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>東歐</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20220815000002"
                                target='_bank'>
                                <p class="ellipsis">金色夢幻 奧地利．捷克10天</p>
                                <span class="price">109,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230107000001"
                                target='_bank'>
                                <p class="ellipsis">醉心 奧．捷．斯．匈 13天</p>
                                <span class="price">114,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230106000002"
                                target='_bank'>
                                <p class="ellipsis">山朗水秀 克羅埃西亞‧斯洛文尼亞‧波士尼亞‧蒙地內哥羅 13天</p>
                                <span
                                    class="price">114,900</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=37" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>紐澳</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=8&sgcn=71"
                                target='_bank'>
                                <p class="ellipsis">紐西蘭系列</p>
                                <span class="price">92900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=9&sgcn=80"
                                target='_bank'>
                                <p class="ellipsis">尼泊爾系列</p>
                                <span class="price">52900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=9&sgcn=82"
                                target='_bank'>
                                <p class="ellipsis">斯里蘭卡系列</p>
                                <span class="price">54900</span>
                            </a>
                            </li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=10" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>北歐</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231212000009"
                                target='_bank'>
                                <p class="ellipsis">北歐五國．冰島縮影．挪威經典雙峽灣15天</p>
                                <span class="price">213,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231101000002"
                                target='_bank'>
                                <p class="ellipsis">北歐四國．挪威三大峽灣全覽13天</p>
                                <span class="price">168,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20231108000001"
                                target='_bank'>
                                <p class="ellipsis">北歐四國．三峽灣．三遊船．景觀列車．夜臥遊輪13天</p>
                                <span class="price">145,900</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=8" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>遊輪</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=7&sgcn=57"
                                target='_bank'>
                                <p class="ellipsis">阿拉斯加系列</p>
                                <span class="price">86900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=7&sgcn=56"
                                target='_bank'>
                                <p class="ellipsis">地中海系列</p>
                                <span class="price">108900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=7&sgcn=58"
                                target='_bank'>
                                <p class="ellipsis">南北極系列</p>
                                <span class="price">410000</span>
                            </a></li>
                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=7" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>中東</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20180205000001"
                                target='_bank'>
                                <p class="ellipsis">埃及紅海五星尼羅河遊輪10天</p>
                                <span class="price">73,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20230103000002"
                                target='_bank'>
                                <p class="ellipsis">土耳其與希臘希俄斯島 浪漫相遇12天</p>
                                <span class="price">64,900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20181109000003"
                                target='_bank'>
                                <p class="ellipsis">希臘愛琴海三島 秘境札金索斯 天空之城 米其林一星 全覽13天</p>
                                <span
                                    class="price">141,900</span>
                            </a></li>

                        </ul>
                    </div>
                    <div class="trip-box animation__el in">
                        <a href="exh/Exhibition.aspx?area=11" class="trip-box-more"
                            target='_bank'>更多行程+</a>
                        <div id="west-europe" class="trip-box-tittle">
                            <h3>美洲</h3>
                        </div>
                        <ul>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=11&sgcn=104"
                                target='_bank'>
                                <p class="ellipsis">秘魯系列</p>
                                <span class="price">198000</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=11&sgcn=101"
                                target='_bank'>
                                <p class="ellipsis">加拿大系列</p>
                                <span class="price">114900</span>
                            </a></li>
                            <li><a href="https://b2b.artisan.com.tw/ClassifyProduct.aspx?l=l&area=11&sgcn=102"
                                target='_bank'>
                                <p class="ellipsis">美東、美加東系列</p>
                                <span class="price">119900</span>
                            </a></li>
                        </ul>
                    </div>--%>
                </div>
            </div>
            <!-- /Trip List -->

            <!-- ADs mobile -->
            <div class="b2b-ads-m">
                <div class="season_sale_m" style="display: none;">
                    <!--<a href="http://b2b.artisan.com.tw/DMShow.aspx?no=156" target="_blank">
                            <img src="./images/b2b_season_sale.jpg" alt="">
                        </a>-->
                    <!-- 套程式版 -->
                    <a href="http://b2b.artisan.com.tw/b2bsale/OnSale.aspx" target="_blank">
                        <img src="./images/b2b_season_sale.jpg" alt="">
                    </a>
                </div>
            </div>
            <!-- /ADs mobile -->
        </div>
        <!--/b2b main-->


        <!--b2b footer-->
        <uc2:Foot ID="Foot1" runat="server" />
        <!--/b2b footer-->
        <asp:HiddenField ID="hidArea_no" runat="server" />
        </div>

        <!--大廣告-->

        <!-- 大廣告 end-->
        <script>
            // 網頁浮動LINEQRcode
            var $win = $(window),
                $ad = $('#abgne_float_ad').css('opacity', 0).show(),
                _width = $ad.width(),
                _height = $ad.height(),
                _diffY = 10,
                _diffX = 10, //距離右及下方邊距
                _moveSpeed = 800, // 移動的速度
                currentNavNo = 0;

            $ad.css({
                top: $(document).height(),
                left: $win.width() - _width - _diffX,
                opacity: 1
            });

            $win.bind('scroll', function () {
                var $this = $(this);

                $ad.stop().animate({
                    top: $this.scrollTop() + $this.height() - _height - _diffY,
                    left: $this.scrollLeft() + $this.width() - _width - _diffX
                }, _moveSpeed);
            }).scroll();

            $('#abgne_float_ad .abgne_close_ad').click(function () {
                $ad.hide();
            });



            $('#AD_mask').owlCarousel({
                items: 1,
                nav: true,
                dots: false,
                responsiveClass: true,
                loop: true,
                autoplay: true,
                autoplayTimeout: 5000,
                navigationText: false,
                autoplayHoverPause: true
            });

            $('#AD-Sale-scroll2').owlCarousel({
                loop: true,
                margin: 10,
                nav: false,
                dots: true,
                items: 5,
                responsiveClass: true,
                autoplay: true,
                autoplayTimeout: 5000,
                responsive: {
                    0: {
                        items: 1
                    },
                    600: {
                        items: 3
                    },
                    900: {
                        items: 4
                    },
                    1200: {
                        items: 5
                    },
                }
            });

            $('#AD .AD_mask .AD_img').removeClass('AD_img');

            $(document).ready(function () {

                //alart 開關
                $(".alert-list-btn").click(function () {
                    $(this).parent().slideUp();
                });

                //banner
                $('#B2bAD-owl-carousel').owlCarousel({
                    loop: true,
                    margin: 10,
                    nav: false,
                    dots: true,
                    items: 1,
                    responsiveClass: true,
                    autoplay: true,
                    autoplayTimeout: 5000,
                });
                // sale-ad
                $('#AD-Sale-scroll2').owlCarousel({
                    loop: true,
                    margin: 10,
                    nav: false,
                    dots: true,
                    items: 5,
                    responsiveClass: true,
                    autoplay: true,
                    autoplayTimeout: 5000,
                });

                //廣告收合
                var AD_main = $(".b2b-alert-AD");
                var AD_wrap = $(".b2b-alert-AD-wrap");
                var AD_wrap_btn = $(".alert-AD-btn");
                AD_wrap_btn.click(function () {
                    if (AD_wrap.is(":hidden")) {
                        AD_wrap_btn.text("收合▲");
                        AD_main.css("padding-top", "0");
                    } else {
                        AD_wrap_btn.text("展開▼");
                        AD_main.css("padding-top", "30px");
                    };
                    AD_wrap.slideToggle();
                });
                // Hot Local
                $('.owl-local').owlCarousel({
                    loop: false,
                    margin: 10,
                    nav: false,
                    dots: true,
                    items: 1,
                    responsiveClass: true,
                    responsive: {
                        1023: {
                            dots: false
                        }
                    }
                });
                var owl_local = $('.owl-local');
                owl_local.owlCarousel();
                // Go to the next item
                $('.loacl_1').hover(function () {
                    owl_local.trigger('to.owl.carousel', 0);
                });
                $('.loacl_2').hover(function () {
                    owl_local.trigger('to.owl.carousel', 1);
                });
                $('.loacl_3').hover(function () {
                    owl_local.trigger('to.owl.carousel', 2);
                });
                $('.loacl_4').hover(function () {
                    owl_local.trigger('to.owl.carousel', 3);
                });
                $('.loacl_5').hover(function () {
                    owl_local.trigger('to.owl.carousel', 4);
                });
                $('.loacl_6').hover(function () {
                    owl_local.trigger('to.owl.carousel', 5);
                });
                $('.loacl_7').hover(function () {
                    owl_local.trigger('to.owl.carousel', 6);
                });
                $('.loacl_8').hover(function () {
                    owl_local.trigger('to.owl.carousel', 7);
                });
                $('.loacl_9').hover(function () {
                    owl_local.trigger('to.owl.carousel', 8);
                });

                var contral_on = $('.owl-local-contral ul li');
                contral_on.hover(function () {
                    $(this).addClass('on');
                    $(this).siblings('li').removeClass('on');
                });
                //不輪播 ads scrollbox
                //$('#AD-Sale-scroll').scrollbox({
                //    distance: 120,
                //    switchItems: 1
                //});
                $('.ad_backward').click(function () {
                    $('#AD-Sale-scroll').trigger('backward');
                });

                $('.ad_forward').click(function () {
                    $('#AD-Sale-scroll').trigger('forward');
                });

                //日期datepicker
                document.getElementById("txtDatePicker1").flatpickr({});
                document.getElementById("txtDatePicker2").flatpickr({});

                // ===== DropDownList1 start =====
                $('#DropDownList1').change(
                    function () {
                        fn_group();
                    });

                $('#DropDownList2').change(
                    function () {
                        $('#hidArea_no').val($('#DropDownList2 option:selected').val());
                    });

                var _areano = $('#DropDownList1').val();
                if (_areano != "0") {
                    fn_group();
                } else {
                    $('#DropDownList2').empty();
                    $('#DropDownList2').append($("<option></option>").val("0").html("全選"));
                }

                function fn_group() {
                    var _areano = $('#DropDownList1').val();
                    $.ajax({
                        type: 'post', //POST傳遞 GET取得
                        dataType: "JSON",
                        url: "Index_DDL_JSON.aspx", //傳遞參數到該頁面做處理
                        data: {
                            'DropDownList1': _areano
                        }, //傳過去的參數
                        success: function (data) { //若成功執行以下這段
                            $('#DropDownList2').empty();
                            $('#DropDownList2').append($("<option></option>").val("0").html("全選"));

                            for (var i = 0; i < data.length; i++) {
                                $('#DropDownList2').append($("<option></option>").val(data[i]["code"]).html(data[i]["name"]));
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alert(xhr.responseText);
                        }
                    })
                }
                // ===== DropDownList1 end =====
            });
        </script>
    </form>
</body>
</html>
