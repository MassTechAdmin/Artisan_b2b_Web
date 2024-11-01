b2b-hotLocal<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" EnableEventValidation="false" %>
    <%@ Register Src="WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
        <%@ Register Src="WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>
            <%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>

                <!DOCTYPE html>

                <html xmlns="http://www.w3.org/1999/xhtml">

                <head runat="server">
                    <meta charset="UTF-8" />
                    <meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport" />
                    <title>凱旋旅行社(巨匠旅遊)同業網</title>
                    <link rel="stylesheet" href="css/owl.carousel.css" />
                    <link rel="stylesheet" href="css/owl.theme.default.css" />
                    <link rel="stylesheet" href="css/layout_b2b.css?20201015" />
                    <!-- flatpicker -->
                    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
                    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
                    <!-- /flatpicker -->
                    <script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
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
                                    <img src="/Images/line_QRcode_2.jpg" alt="">
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
                            <!--/b2b header-->

                            <!--b2b alert-->
                            <div id="b2b-alert" style='display:none;'>
                                <asp:Literal ID="litHomeNews" runat="server"></asp:Literal>
                                <asp:Literal ID="litHomeBanner" runat="server"></asp:Literal>
                            </div>
                            <!--/b2b alert-->

                            <!--b2b top-->
                            <div id="b2b-top">
                                <div class="b2b-banner-news">
                                    <span>Final Call ‣‣‣ </span>
                                    <marquee direction="left" width="100%" align="botton" scrollamount="3">
                                        <asp:Literal ID="litnews" runat="server"></asp:Literal>
                                    </marquee>
                                </div>
                                <div id="user-info">
                                    <asp:Literal ID="litUserInfo" runat="server" />
                                </div>
                            </div>
                            <!--/b2b top-->

                            <!--b2b main-->
                            <div id="b2b-content-index">
                                <!-- search -->
                                <div class="b2b-search-tool">
                                    <h1>團體旅遊</h1>
                                    <div class="b2b-search-area">
                                        <table width="0" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="search-title">出發日期</td>
                                                <td class="search-box search-datepicker">
                                                    <input class="search-textBox" id="txtDatePicker1" runat="server" /><img src="./images/calendar.png" alt="" style="width: 16px; height: 15px; margin-left: -25px; margin-right: 8px; pointer-events: none;"
                                                    />
                                                    <input class="search-textBox" id="txtDatePicker2" runat="server" /><img src="./images/calendar.png" alt="" style="width: 16px; height: 15px; margin-left: -25px; margin-right: 8px; pointer-events: none;"
                                                    />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="search-title">系列</td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList0" runat="server" class="search-textBox">
                                                        <asp:ListItem Value="全部系列" Selected>全部系列</asp:ListItem>
                                                        <asp:ListItem Value="巨匠">巨匠</asp:ListItem>
                                                        <asp:ListItem Value="新視界">新視界</asp:ListItem>
                                                        <asp:ListItem Value="典藏">典藏</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="search-title">目的地</td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" class="search-textBox" />
                                                    <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="SecClass_Name" DataValueField="SecClass_No" class="search-textBox" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="search-title">關鍵字</td>
                                                <td>
                                                    <asp:TextBox ID="txbKey" runat="server" class="search-textBox" />
                                                    <asp:CheckBox ID="chkOK" runat="server" Text="已成團" />
                                                    <asp:CheckBox ID="chkSign" runat="server" Text="可報名" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="imgButton" runat="server" OnClick="imgButton_Click" Text="搜尋" class="search-box-btn" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <!-- /search -->


                                <!-- Hot Local -->
                                <div class="b2b-hotLocal">
                                    <h1>亞洲精選</h1>
                                    <ul class="b2b-hotLocal_area">
                                        <asp:Literal ID="litBannerRight" runat="server"></asp:Literal>
                                    </ul>
                                </div>
                                <!-- /Hot Local -->

                                <div class="owl-carousel owl-theme" id="owl-ads">
                                    <asp:Literal ID="litSB2" runat="server" />
                                </div>

                                <%--Gold Sale--%>
                                    <div class="Artisan-Recommend-tool">
                                        <h1>巨匠系列推薦行程</h1>
                                        <ul class="Artisan-Recommend-area">
                                            <asp:Literal ID="RecSale" runat="server"></asp:Literal>
                                        </ul>
                                    </div>
                                    <%-- /Gold Sale--%>



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
                                            </div>
                                        </div>
                                        <!-- /Trip List -->

                                        <!-- ADs PC -->
                                        <div class="b2b-ads">
                                            <div class="ads_slider" style="margin-top: 0;">
                                                <div class="ad_backward"><img src="./images/arrow_up.png" alt=""></div>
                                                <div class="ad_forward"><img src="./images/arrow_down.png" alt=""></div>
                                                <div id="AD-Sale-scroll" class="scroll-text">
                                                    <ul>
                                                        <asp:Literal ID="litSB1" runat="server" />
                                                    </ul>
                                                </div>
                                            </div>

                                            <div class="season_sale" style="margin-top: 0; display: none;">
                                                <!--<a href="http://b2b.artisan.com.tw/DMShow.aspx?no=156" target="_blank">
                            <img src="./images/b2b_season_sale.jpg" alt="">
                        </a>-->
                                                <!-- 套程式版 -->
                                                <a href="http://b2b.artisan.com.tw/b2bsale/OnSale.aspx" target="_blank">
                                                    <img src="./images/b2b_season_sale.jpg" alt="">
                                                </a>
                                            </div>

                                            <!-- 無發表會隱藏 -->
                                            <!--<div class="lecture">
						<a href="http://b2b.artisan.com.tw/DMShow.aspx?no=165">
							<img src="./images/b2b_lecture.jpg" alt="">
						</a>
					</div>-->

                                            <div class="dm_download">
                                                <a href="http://b2b.artisan.com.tw/b2bDMDownload/DMdownload.aspx" target="_blank">
                                                    <img src="./images/b2b_dm_download.jpg" alt="">
                                                </a>
                                            </div>
                                        </div>
                                        <!-- /ADs PC -->

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

                                            <!-- 無發表會隱藏 -->
                                            <!--<div class="lecture_m">
						<a href="http://b2b.artisan.com.tw/DMShow.aspx?no=165">
							<img src="./images/b2b_lecture.jpg" alt="">
						</a>
					</div>-->

                                            <%-- <div class="dm_download_m">
                                                <a href="http://b2b.artisan.com.tw/b2bDMDownload/DMdownload.aspx" target="_blank">
                                                    <img src="./images/b2b_dm_download.jpg" alt="">
                                                </a>
                                            </div> --%>


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
                        <%-- <div id="AD" style="display: block;">
                            <div class="b2b-pics">
                                <h5 class="AD_close">X</h5>
                                <div class="owl-carousel owl-theme" id="AD_mask">
                                    <asp:Literal runat="server" ID="litAD" />
                                </div>
                            </div>
                        </div>
                        <input runat="server" type="hidden" id="adnum" name="adnum" />
                        <link rel="stylesheet" type="text/css" href="css/style_ads_b2b.css?20190717" />
                        <script type="text/javascript" src="js/main.js"></script> --%>
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

                            $win.bind('scroll', function() {
                                var $this = $(this);

                                $ad.stop().animate({
                                    top: $this.scrollTop() + $this.height() - _height - _diffY,
                                    left: $this.scrollLeft() + $this.width() - _width - _diffX
                                }, _moveSpeed);
                            }).scroll();

                            $('#abgne_float_ad .abgne_close_ad').click(function() {
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

                            $('#AD .AD_mask .AD_img').removeClass('AD_img');

                            $(document).ready(function() {

                                //alart 開關
                                $(".alert-list-btn").click(function() {
                                    $(this).parent().slideUp();
                                });

                                //B2bAD 
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

                                //廣告收合
                                var AD_main = $(".b2b-alert-AD");
                                var AD_wrap = $(".b2b-alert-AD-wrap");
                                var AD_wrap_btn = $(".alert-AD-btn");
                                AD_wrap_btn.click(function() {
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
                                $('.loacl_1').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 0);
                                });
                                $('.loacl_2').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 1);
                                });
                                $('.loacl_3').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 2);
                                });
                                $('.loacl_4').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 3);
                                });
                                $('.loacl_5').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 4);
                                });
                                $('.loacl_6').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 5);
                                });
                                $('.loacl_7').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 6);
                                });
                                $('.loacl_8').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 7);
                                });
                                $('.loacl_9').hover(function() {
                                    owl_local.trigger('to.owl.carousel', 8);
                                });

                                var contral_on = $('.owl-local-contral ul li');
                                contral_on.hover(function() {
                                    $(this).addClass('on');
                                    $(this).siblings('li').removeClass('on');
                                });
                                //不輪播 ads scrollbox
                                //$('#AD-Sale-scroll').scrollbox({
                                //    distance: 120,
                                //    switchItems: 1
                                //});
                                $('.ad_backward').click(function() {
                                    $('#AD-Sale-scroll').trigger('backward');
                                });

                                $('.ad_forward').click(function() {
                                    $('#AD-Sale-scroll').trigger('forward');
                                });

                                // ads-m owl
                                $('#owl-ads').owlCarousel({
                                    autoplay: true,
                                    loop: true,
                                    margin: 5,
                                    nav: true,
                                    navText: ["<img src='./images/arrow_prev.png'>", "<img src='./images/arrow_next.png'>"],
                                    dots: false,
                                    items: 1,
                                    responsive: {
                                        768: {
                                            items: 3
                                        }
                                    }
                                })

                                //日期datepicker
                                document.getElementById("txtDatePicker1").flatpickr({});
                                document.getElementById("txtDatePicker2").flatpickr({});

                                // ===== DropDownList1 start =====
                                $('#<%=DropDownList1.ClientID%>').change(
                                    function() {
                                        fn_group();
                                    });

                                $('#<%=DropDownList2.ClientID%>').change(
                                    function() {
                                        $('#<%=hidArea_no.ClientID%>').val($('#<%=DropDownList2.ClientID%> option:selected').val());
                                    });

                                var _areano = $('#<%=DropDownList1.ClientID%>').val();
                                if (_areano != "0") {
                                    fn_group();
                                } else {
                                    $('#<%=DropDownList2.ClientID%>').empty();
                                    $('#<%=DropDownList2.ClientID%>').append($("<option></option>").val("0").html("全選"));
                                }

                                function fn_group() {
                                    var _areano = $('#<%=DropDownList1.ClientID%>').val();
                                    $.ajax({
                                        type: 'post', //POST傳遞 GET取得
                                        dataType: "JSON",
                                        url: "Index_DDL_JSON.aspx", //傳遞參數到該頁面做處理
                                        data: {
                                            'DropDownList1': _areano
                                        }, //傳過去的參數
                                        success: function(data) { //若成功執行以下這段
                                            $('#<%=DropDownList2.ClientID%>').empty();
                                            $('#<%=DropDownList2.ClientID%>').append($("<option></option>").val("0").html("全選"));

                                            for (var i = 0; i < data.length; i++) {
                                                $('#<%=DropDownList2.ClientID%>').append($("<option></option>").val(data[i]["code"]).html(data[i]["name"]));
                                            }
                                        },
                                        error: function(xhr, ajaxOptions, thrownError) {
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