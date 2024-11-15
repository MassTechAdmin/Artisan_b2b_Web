﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreasureTravel.aspx.cs" Inherits="TreasureTravel" %>

<!DOCTYPE html>
<html>

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>珍藏旅遊(凱旋旅行社) - 巨匠旅遊</title>
    <meta name="description" content="珍藏旅遊">
    <meta name="keywords" content="精緻團體旅遊,歐洲團體">
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/owl.carousel.css">
    <link rel="stylesheet" href="css/owl.theme.default.css">
    <!-- aos -->
    <link href="https://unpkg.com/aos@2.3.1/dist/aos.css" rel="stylesheet">
    <link rel="stylesheet" href="css/style.css">

<!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','wis_dataLayer','GTM-WXV8LX');</script>
<!-- End Google Tag Manager -->
</head>

<body>
    <header>
        <div id="logo"><img src="images/logo.png" alt="logo" title="珍藏旅遊"></div>
        <div class="header-bg"></div>
        <div class="header-gradual"></div>
        <ul class="owl-carousel fadeOut owl-theme" id="Slider-owl-carousel">
            <li>
                <a href="https://www.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20190628000002">
                    <div class="banner" style="background-image: url(images/banner01.jpg);">
                    </div>
                    <h3>哈斯達特湖</h3>
                </a>
            </li>
            <li>
                <a href="https://www.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20190814000004">
                    <div class="banner" style="background-image: url(images/banner02.jpg);">
                    </div>
                    <h3>少女峰的齒軌火車</h3>
                </a>
            </li>
        </ul>
    </header>

    <div id="responsiveTabs">
        <ul class="tab-ul">
            <li class="tab-left">
                <a href="#tab-1">貼心好禮<span>SPECIAL GIFTS</span></a>
            </li>
            <li class="tab-right">
                <a href="#tab-2">早鳥優惠<span>EARLY BIRD</span></a>
            </li>
        </ul>

        <div id="tab-1">
            <div class="tab-area">
                <dis class="tab-figure">
                    <figure>
                        <img src="images/tab_icon01.png" alt="">
                        <figcaption>含小費.行李搬運費</figcaption>
                    </figure>
                    <figure>
                        <img src="images/tab_icon02.png" alt="">
                        <figcaption>二人一台WIFI機</figcaption>
                    </figure>
                    <figure>
                        <img src="images/tab_icon03.png" alt="">
                        <figcaption>每人一台耳機導覽器</figcaption>
                    </figure>
                    <figure>
                        <img src="images/tab_icon04.png" alt="">
                        <figcaption>歐洲規格轉換插頭</figcaption>
                    </figure>
                    <figure>
                        <img src="images/tab_icon05.png" alt="">
                        <figcaption>行李束帶</figcaption>
                    </figure>
                    <figure>
                        <img src="images/tab_icon06.png" alt="">
                        <figcaption>每人每天一瓶水</figcaption>
                    </figure>
                </dis>
                <div class="tab-note">
                    <h6>●WIFI機使用須知 /<span>若因郊區或山區WIFI有時無訊號或接收不穩，敬請見諒。</span></h6>
                    <div class="note-ul">
                        <div class="note-ul-tool">
                            <span>1. 贈送兩人使用一台網路分享器，如不使用亦不退費。</span>
                            <span>2. 此網路分享器皆與歐洲各不同電信公司配合，各國或各區的頻率不同故速度亦不同。</span>
                            <span>3. 歐洲電信於鄉村及郊外電信訊號較弱，此時分享器會有一直處於搜尋訊號狀態，請耐心等候。</span>
                        </div>
                        <div class="note-ul-tool">
                            <span>4. 為避免超量造成網路服務中斷，建議在出國前先將Dropbox、Cloud、APP自動更新,等程式關閉，也應避免觀看網路電視、Youtube等大量數據之服務。短時間內過大流量可能造成電信公司主動斷線或限速，本公司將無法對此情形退費。</span>
                            <span>5. 貼心提供網路分享器，敬請貴賓妥善使用。如有遺失之情況，需賠償每台機器費用NT3980元。</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tab-2">
            <div class="tab-area tab-flex">
                <div class="tab-pic">
                    <img src="images/tab_pic.png" alt="">
                </div>
                <ul class="tab-brid">
                    <li>於團體出發日期前 3 個月(或以上)報名並繳交訂金者，<br>每人可享新台幣<span>5,000</span>元優惠。</li>
                    <li>於團體出發日期前 2 個月(未滿三個月)報名並繳交訂金者，<br>每人可享新台幣<span>3,000</span>元優惠。</li>
                    <li>於團體出發日期前 1 個月(未滿二個月)報名並繳交訂金者，<br>每人可享新台幣<span>2,000</span>元優惠。</li>
                    <li>◈ 本公司保留最終解釋、修改或終止之權利。</li>
                </ul>
            </div>
        </div>
    </div>

    <div id="main" class="container">
        <section>
            <h1 data-aos="zoom-out-down" data-aos-duration="700"><span>◆</span>當月精選<span>◆</span>
                <hr>
                <hr><b>BEST SELLERS</b></h1>
            <div class="owl-carousel fadeOut owl-theme" id="Featured-owl-carousel">
                <div class="item">
                    <div class="featured-block">
                        <div class="featured-tool-img"><img src="images/featured01.jpg" alt=""></div>
                        <div class="featured-tool-pc">
                            <div class="featured-txt">
                                <h2><span>珍藏旅遊</span>饗宴西班牙~古根漢 里歐哈酒莊<br>遇見･W･珍饌方舟13天</h2>
                                <img src="images/line_treasure.png" alt="">
                                <p>■七晚五星旅館+直布羅陀5星方舟<br>■保證入住Hotel W Barcelona<br>■造訪古根漢美術館+里斯卡酒莊</p>
                                <button data-aos="fade-right" data-aos-duration="700"><a href="https://www.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20190705000002" target="_blank">前往行程→</a></button>
                            </div>
                        </div>
                        <div class="featured-tool-mb">
                            <div class="featured-txt">
                                <h2><span>珍藏旅遊</span>饗宴西班牙~古根漢 里歐哈酒莊<br>遇見･W･珍饌方舟13天</h2>
                                <p>■七晚五星旅館+直布羅陀5星方舟<br>■保證入住Hotel W Barcelona<br>■造訪古根漢美術館+里斯卡酒莊</p>
                                <button data-aos="fade-right" data-aos-duration="700"><a href="https://www.artisan.com.tw/ClassifyProduct.aspx?TripNo=T20190705000002" target="_blank">前往行程→</a></button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>
        <section>
            <h1 data-aos="zoom-out-down" data-aos-duration="700"><span>◆</span>珍藏行程<span>◆</span>
                <hr>
                <hr><b>POPULAR DESTINATIONS</b></h1>
            <div class="block-tool">                    
                <asp:Literal ID="Exh" runat="server"></asp:Literal>
            </div>
        </section>
    </div>

    <aside>
        <a href="https://zh-tw.facebook.com/artisantour" target="_blank" class="icon-fb"></a>
        <a href="http://line.me/ti/p/%40rgm1660e" target="_blank" class="icon-line"></a>
        <a href="javascript:;" id="gotop" class="icon-top"></a>
    </aside>

    <footer>
        <div class="footer-link">
            <div class="container">
                <ul id="bottom-menu">
                    <li><a href="https://www.artisan.com.tw/" target="_blank">回首頁</a></li>
                    <li><a href="https://www.artisan.com.tw/About.aspx" target="_blank">關於巨匠</a></li>
                    <li><a href="https://www.artisan.com.tw/About_Artisan.aspx" target="_blank">連絡我們</a></li>
                    <li><a href="http://rate.bot.com.tw/Pages/Static/UIP003.zh-tw.htm" target="_blank">匯率查詢</a></li>
                    <li><a href="http://www.cwb.gov.tw/V7/forecast/world/world_aa.htm" target="_blank">天氣查詢</a></li>
                    <li><a href="https://www.artisan.com.tw/tour_escort.aspx" target="_blank">專業領隊</a></li>
                    <li><a href="https://www.artisan.com.tw/Notes.aspx" target="_blank">報名.機票退票.行李托運規定</a></li>
                    <li><a href="https://www.artisan.com.tw/Credit_card.doc">刷卡授權書</a></li>
                    <li><a href="https://www.artisan.com.tw/Travel_Contract.doc" target="_blank">旅遊合約書</a></li>
                    <li><a href="https://www.artisan.com.tw/Passenger.doc" target="_blank">旅客保險基本資料表</a></li>
                </ul>
            </div>
        </div>
        <div class="foot-wrap container">
            <div class="footer-left">
                <h4>凱旋旅行社(股份)有限公司(交觀綜字第2133號)</h4>
                <p><span class="triangle"></span><span class="triangle"></span><span class="triangle"></span>珍藏旅遊 / 巨匠旅遊 / 新視界假期</p>
                <p>經營業務範圍：綜合旅行社經營業務範圍</p>
                <p>代表人：項國棟｜聯絡人：劉恪宏</p>
                <p>法律顧問：信和國際法律事務所 梁堯清律師</p>
                <p>E-MAIL：<span style="font-size: 11px; font-family: Verdana;"><a href="mailto:artisantour@artisan.com.tw">ARTISANTOUR@ARTISAN.COM.TW</a></span>
                </p>
            </div>
            <div class="footer-line"></div>
            <div class="footer-right">
                <div class="contact-box-warp">
                    <div class="contact-box">
                        <p>台北&nbsp;&nbsp;<span>TEL</span>:02-2507-5000</p>
                        <p>FAX:02-2504-6199</p>
                    </div>
                    <div class="contact-box">
                        <p>桃園&nbsp;&nbsp;<span>TEL</span>:03-3561-755</p>
                        <p>FAX:03-3561-970</p>
                    </div>
                    <div class="contact-box">
                        <p>新竹&nbsp;&nbsp;<span>TEL</span>:03-5283-088</p>
                        <p>FAX:03-5283-389</p>
                    </div>
                    <div class="contact-box">
                        <p>台中&nbsp;&nbsp;<span>TEL</span>:04-2255-0229</p>
                        <p>FAX:04-2255-1169</p>
                    </div>
                    <div class="contact-box">
                        <p>員林&nbsp;&nbsp;<span>TEL</span>:04‐8365-288</p>
                        <p>FAX:04‐8365-399</p>
                    </div>
                    <div class="contact-box">
                        <p>台南&nbsp;&nbsp;<span>TEL</span>:06-222-6627</p>
                        <p>FAX:06-222-6731</p>
                    </div>
                    <div class="contact-box">
                        <p>高雄&nbsp;&nbsp;<span>TEL</span>:07-2419-872</p>
                        <p>FAX:07-2154-690</p>
                    </div>
                </div>
            </div>
        </div>
    </footer>

    <script type="text/javascript" src="https://code.jquery.com/jquery-latest.min.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/jquery.responsiveTabs.js"></script>
    <!-- aos -->
    <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script>

    <script>
        $(document).ready(function () {
            /* slider */
            $('#Slider-owl-carousel').owlCarousel({
                animateOut: 'fadeOut',
                animateIn: 'fadeInRight',
                items: 1,
                nav: false,
                responsiveClass: true,
                loop: true,
                smartSpeed: 450,
                autoplay: true,
                autoplayTimeout: 5000
            })
            /*featured*/
            $('#Featured-owl-carousel').owlCarousel({
                animateOut: 'fadeOut',
                animateIn: 'fadeInRight',
                items: 1,
                nav: false,
                responsiveClass: true,
                loop: true,
                smartSpeed: 450,
                autoplay: true,
                autoplayTimeout: 5000
            })
            // 網頁go top
            $("#gotop").click(function () {
                $("html,body").animate({
                    scrollTop: 0
                }, 500);
            });


            // responsiveTabs 
            $('#responsiveTabs').responsiveTabs({
                startCollapsed: 'accordion',
                animation: 'fade',
                duration: 200,
                active: 0, // Opens the second tab on load
                accordionTabElement: '<div></div>'
            });


            //AOS
            AOS.init();

            $(window).resize(function () { });
        });

    </script>
<!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WXV8LX"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->
</body>

</html>

