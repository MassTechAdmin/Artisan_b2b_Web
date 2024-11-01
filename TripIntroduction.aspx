<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripIntroduction.aspx.cs" Inherits="TripIntroduction" %>

<%@ Register Src="WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>凱旋旅行社(巨匠旅遊)｜Artisan Tour</title>
    <asp:Literal ID="description" runat="server"></asp:Literal>
    <meta name="keywords" content="高人氣評價,歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊">
    <link rel="stylesheet" href="https://www.artisan.com.tw/css/normalize.css">
    <!-- owl -->
    <link rel="stylesheet" href="https://www.artisan.com.tw/css/owl.carousel.min.css">
    <link rel="stylesheet" href="https://www.artisan.com.tw/css/owl.theme.default.min.css">
    <!-- fontawesome(social use) -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/solid.css" integrity="sha384-+0VIRx+yz1WBcCTXBkVQYIBVNEFH1eP6Zknm16roZCyeNg2maWEpk/l/KsyFKs7G" crossorigin="anonymous">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/brands.css" integrity="sha384-1KLgFVb/gHrlDGLFPgMbeedi6tQBLcWvyNUN+YKXbD7ZFbjX6BLpMDf0PJ32XJfX" crossorigin="anonymous">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/fontawesome.css" integrity="sha384-jLuaxTTBR42U2qJ/pm4JRouHkEDHkVqH0T1nyQXn1mZ7Snycpf6Rl25VBNthU4z0" crossorigin="anonymous">
    <!-- style -->
    <link rel="stylesheet" href="https://www.artisan.com.tw/pic_css/fade_pic.css">
    <link rel="stylesheet" href="https://www.artisan.com.tw/css/webTrip_2020.css">
    <link rel="stylesheet" href="css/layout_b2b.css">
    <link rel="stylesheet" href="css/webTripB2b_2020.css">

    <link rel="stylesheet" href="css/bootstrap.css" />

    <!-- JQ -->
    <script type="text/javascript" src="js/jquery-2.1.3.min.js"></script>
    <style>
        .web-share-bttom a {
            border: 1px solid #EAB600;
            border-radius: 5px;
            display: inline-block;
            font-family: Arial, Helvetica, sans-serif, 微軟正黑體;
            font-size: 13px;
            padding: 3px 2px 5px 2px;
            color: #FFFFFF;
            text-decoration: none;
            line-height: normal;
            background-color: #EAB600;
        }

            .web-share-bttom a:hover {
                border: 1px solid #ffc600;
                text-decoration: none;
                color: #CC6600;
                background-color: #FFFFFF;
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="wrapper-main">
            <!-- Header -->
            <header>
                <nav>
                    <uc1:Header ID="Header_Menu1" runat="server" />
                </nav>
            </header>
            <!-- /Header -->

            <!-- 行程資料 -->
            <div id="TripMain" class="container">
                <div class="TripMain-navigate">
                    <asp:Literal runat="server" ID="litTitleUrl" />
                </div>

                <h1 class="tripTitle">
                    <asp:Literal runat="server" ID="litTitle" /></h1>

                <div class="TripMain-tool">
                    <!-- 月曆 -->
                    <!-- 行程上方資料 -->
                    <div class="TripMain-info">
                        <ul class="TripMain-infoDetail">
                            <asp:Literal runat="server" ID="litTopGrop" />
                        </ul>
                        <hr class="TripMain-hr">

                        <!-- 行程優惠(無資料隱藏) -->
                        <asp:Literal runat="server" ID="litDiscount" />

                        <div class="TripMain-flexOrder">
                            <div class="TripMain-list TripMain-point TPO1">航班資訊</div>
                            <div class="TripMain-list TPO2">
                                <a href="javascript: void(0)" class="PrintSchedule" id="LinkButton1">列印及下載</a>
                                <%--<asp:LinkButton runat="server" class="PrintSchedule" ID="LinkButton1">列印及下載</asp:LinkButton>--%>
                            </div>
                            <div class="TripMain-list TPO3"><a href="https://www.artisan.com.tw/Notes.aspx" target="_blank">旅客報名須知</a></div>
                            <div class="TripMain-list TripMain-noline TPO4 TripMain-shareBtn">
                                <a href="javascript:;">分享專欄</a>
                                <div class="TripMain-share">
                                    <div id="shareBlock"></div>
                                </div>
                            </div>
                            <asp:Literal runat="server" ID="litMap" />
                            <div class="TripMain-plant TPO5">
                                <div class="plant-tool">
                                    <div class="plant-go">去程</div>
                                    <div class="plant-list-tool">
                                        <asp:Literal runat="server" ID="litPlanGo" />
                                    </div>
                                </div>
                                <div class="plant-tool">
                                    <div class="plant-back">回程</div>
                                    <div class="plant-list-tool">
                                        <asp:Literal runat="server" ID="litPlanEnd" />
                                    </div>
                                </div>
                                <div class="plant-note">
                                    因應航空公司開票及保險的需求，及為了提供您在旅途中更完善的服務，請務必在繳交訂金的時候填妥客戶基本資料表。<a href="http://www.grp.com.tw/data/" target="_blank">旅客資料表下載</a><span class="hidden-pc"><br>
                                        <br>
                                        上為參考航班，實際航班時間以航空公司為最終確認。若因航空公司或不可抗力因素，變動航班時間或轉機點，造成團體行程變更、增加餐食，本公司將不另行加價。若行程變更、減少餐食，則酌於退費。<br>
                                        <br>
                                        特別說明：幾乎所有外籍航空公司之團體機票(含燃油附加稅)一經開票後，均無退票價值，此點基於航空公司之規定，敬請見諒。</span>
                                </div>
                            </div>
                        </div>


                        <asp:Literal runat="server" ID="litJoin" />
                    </div>
                </div>
            </div>
            <!-- /行程資料 -->

            <!-- 查看行程 -->
            <div id="TripContent">
                <nav id="TripNav">
                    <ul class="TripNav-list container">
                        <li data-target="#sectionfeature" class="active"><a href="javascript:;">特色介紹</a></li>
                        <li data-target="#section-day"><a href="javascript:;">行程內容</a></li>
                        <li data-target="#section-remind" class="hidden-m"><a href="javascript:;">貼心提醒</a></li>
                        <li data-target="#section_detail" runat="server" id="nav_GroupPrice">
                            <asp:LinkButton ID="linkGroupPrice" runat="server" OnClientClick="return false;">團費資訊</asp:LinkButton></li>
                        <li data-target="#section_FitPriceList" runat="server" id="nav_FitPrice" visible="false"><a href="javascript:;" class="TripNav-noline">自由加價購</a></li>
                    </ul>
                </nav>

                <!-- 行程地圖 -->
                <asp:Literal runat="server" ID="litMap2" />

                <!-- 行程特色 -->
                <section class="section-target" id="sectionfeature">
                    <div id="sectionfeaturedata" runat="server">
                        <h3 class="main-title">行程特色</h3>

                        <!-- 舊特色(無資料隱藏) -->
                        <asp:Literal runat="server" ID="TripFeat" />

                        <!-- 行程大圖 -->
                        <asp:Literal runat="server" ID="litTripFeat" />

                        <!-- 行程重點(無資料隱藏) -->
                        <asp:Literal runat="server" ID="litHighlights" />

                        <asp:Literal runat="server" ID="litIntro" />
                    </div>
                </section>


                <!-- 行程內容 -->
                <section class="section-target" id="section-day">
                    <h3 class="main-title">行程內容</h3>
                    <div class="container">
                        <div class="trip_day_tool">
                            <div id="trip_day_slider">
                                <div class="trip_day_anchor">
                                    <asp:Literal runat="server" ID="litDay" />
                                </div>
                            </div>
                        </div>

                        <asp:Literal runat="server" ID="maintour" />
                    </div>
                </section>

                <!-- 解除天數選單 -->
                <div id="unstick-day"></div>

                <!-- 貼心提醒 -->
                <section class="section-target" id="section-remind">
                    <h3 class="main-title">貼心提醒</h3>
                    <div class="container">
                        <div class="trip_remind">
                            <asp:Literal runat="server" ID="litRemind" />
                        </div>
                    </div>
                </section>

                <!-- 團費詳細資訊 -->
                <section class="section-target" runat="server" id="section_detail">
                    <h3 class="main-title">團費資訊</h3>
                    <div class="container">
                        <div class="trip-detail">
                            <div class="TP_top_list_table">
                                <asp:Literal runat="server" ID="litGrop" />
                            </div>
                            <div id="TP_center_list">
                                <div id="TP_center_list_left">
                                    <div class="TP_left_description">團費說明</div>
                                    <div class="TP_left_title">大人</div>
                                    <div class="TP_left_money">
                                        <asp:Literal ID="litPay1" runat="server" Text="不提供"></asp:Literal>
                                    </div>
                                    <div class="TP_left_title">小孩占床</div>
                                    <div class="TP_left_money">
                                        <asp:Literal ID="litPay2" runat="server" Text="不提供"></asp:Literal>
                                    </div>
                                    <div class="TP_left_title">小孩不占床</div>
                                    <div class="TP_left_money">
                                        <asp:Literal ID="litPay3" runat="server" Text="不提供"></asp:Literal>
                                    </div>
                                    <div class="TP_left_title">小孩加床</div>
                                    <div class="TP_left_money">
                                        <asp:Literal ID="litPay4" runat="server" Text="不提供"></asp:Literal>
                                    </div>
                                    <div class="TP_left_title">嬰兒</div>
                                    <div class="TP_left_money">
                                        <asp:Literal ID="litPay5" runat="server" Text="不提供"></asp:Literal>
                                    </div>
                                    <div class="TP_left_title">JoinTour</div>
                                    <div class="TP_left_money">
                                        <asp:Literal ID="litPay6" runat="server" Text="不提供"></asp:Literal>
                                    </div>
                                </div>
                                <div id="TP_center_list_right">
                                    <div class="TP_center_list_right_top">
                                        <div class="TP_center_list_right_top_tip">稅金 / 小費</div>
                                        <div class="TP_center_list_right_top_content">
                                            <asp:Literal ID="litVisa" runat="server" Text="不提供"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="TP_center_list_right_bottom">
                                        <div class="TP_center_list_right_bottom_ps">備註</div>
                                        <div class="TP_center_list_right_bottom_content">
                                            <p>
                                                <asp:Literal runat="server" ID="litContent" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="TP_bottom_list">
                                ※ 年齡說明 ：【嬰兒】 <span style="color: #d71e1e; font-weight: bold;">未滿2歲之幼兒</span> ； 【小孩】 <span style="color: #d71e1e; font-weight: bold;">2-12歲之小朋友</span> ； 【成人】<span style="color: #d71e1e; font-weight: bold;"> 滿12歲以上之旅客</span>。
                                <br />
                                費用不包括 ：機場來回接送、護照工本費、旅行袋、床頭與行李等禮貌性質小費、私人費用等。
                            </div>
                        </div>
                    </div>
                </section>


                <!-- 詳細費用 -->
                <section class="section-target" runat="server" id="section_FitPriceList" visible="false">
                    <h3 class="main-title">自由行加價購</h3>
                    <div class="container">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="FitAgt-tableList" GridLines="Horizontal" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="fa fa-check-square" aria-hidden="true"></i>行程內容包含
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="FitAgt-count"><%#Eval("n", "{0}")%></span><%#Eval("FName", "{0}")%>
                                        <asp:HiddenField runat="server" ID="hiddNumber" Value='<%#Eval("Number", "{0}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="單　位" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Eval("FUnit", "{0}")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="FitAgt-unit" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="費　用" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#"$" + Eval("FPrice", "{0:N0}")%>
                                        <asp:HiddenField runat="server" ID="hiddFPrice" Value='<%#Eval("FPrice", "{0}")%>' />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="FitAgt-price" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="FitAgt-tableList" GridLines="Horizontal" AllowPaging="True" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="fa fa-cart-plus" aria-hidden="true"></i>自費加購項目
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="FitAgt-count"><%#Eval("n", "{0}")%></span>
                                        <%#Eval("FName", "{0}")%>
                                        <asp:HiddenField runat="server" ID="hiddNumber2" Value='<%#Eval("Number", "{0}")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="單　位" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#Eval("FUnit", "{0}")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="FitAgt-unit" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="費　用" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# "$" + Eval("FPrice", "{0:N0}")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="FitAgt-price" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>
            </div>
            <!-- /查看行程 -->

            <!-- footer -->
            <uc2:Foot ID="Foot" runat="server" />
            <!-- /footer -->
        </div>
        <asp:HiddenField runat="server" ID="sa" />
        <!----------------------------------------------------------------------------------------------------->

        <script src="js/jquery-1.10.1.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="name">公司名稱&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                            <asp:TextBox ID="CompanyName" runat="server" class="form-control" Width="500px" Height="20px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="name">業務代表&nbsp;&nbsp;</label>
                            <asp:TextBox ID="ContactName" runat="server" class="form-control" Width="500px" Height="20px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="name">電　　話&nbsp;&nbsp;</label>
                            <asp:TextBox ID="ContactPhone" runat="server" class="form-control" Width="500px" Height="20px"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="name">LINE ID</label>
                            <asp:TextBox ID="ContactEmail" runat="server" class="form-control" Width="500px" Height="20px"></asp:TextBox>
                        </div>
                        <asp:Button ID="PrintScheduleSubmit" runat="server" Text="行程下載" class="btn btn-primary" OnClick="PrintScheduleSubmit_Click" />
                        <label for="name">&nbsp;&nbsp; (如不需顯示聯絡資訊，請直接按下行程下載按鈕進行下載！)</label>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hidGropNumb" />
        <asp:HiddenField runat="server" ID="hidGropName" />

        <!----------------------------------------------------------------------------------------------------->

        <div id="myTourCopy2" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <table id="CopyWebSite" border="0" cellspacing="4" cellpadding="2" width="100%" align="center">
                <tbody>
                    <tr>
                        <td class="colorbox" align="left" style="color: #0000FF;">2024/05/13　售價 43,900 / 每席訂金 25,000元</td>
                    </tr>
                    <tr>
                        <td class="colorbox" align="left" style="color: #0000FF;">快閃土耳其(桃園出/松山回)～特洛伊傳奇、鄂圖曼小鎮、世界遺產旅行、卡帕多奇亞連泊10日</td>
                    </tr>
                    <tr>
                        <td class="colorbox" align="left" style="color: #0000FF;">https://agt.tw/9dcqWf</td>
                    </tr>
                    <tr>
                        <td class="colorbox" align="left" style="color: #0000FF;" courier="">旅市旅行社</td>
                    </tr>
                    <tr>
                        <td class="colorbox" align="left" style="color: #0000FF;" courier="">李慶圓 / 0928351236 / 02-25063038</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="myTourCopy" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    </div>
                    <div class="modal-body" id="myTourCopyDetail">
                        <asp:Literal ID="litTourCopyData" runat="server"></asp:Literal>
                        <div class="form-group">
                            <asp:Button ID="Button1" runat="server" Text="行程複製" class="btn btn-primary" OnClientClick='CopyTextToClipboard("myTourCopyDetail"); return false;' />
                        </div>
                        <div class="form-group"><font id='ShowContent' style='color: red; display: none'>已複製內容，請貼上。</font></div>
                    </div>
                </div>
            </div>
        </div>
        <script>
            // 列印及下載
            $('.PrintSchedule').click(function () {
                $('#myModal').modal('show');
                document.getElementById("CompanyName").value = '';
                document.getElementById("ContactName").value = '';
                document.getElementById("ContactPhone").value = '';
                document.getElementById("ContactEmail").value = '';
                $('#icon_bar_close').fadeIn('slow');
                $('#icon_bar_open').fadeOut('slow');
            });

            $(document).delegate('#PrintScheduleSubmit', 'click', function () {
                $('#myModal').modal('hide');
            });

            // 複製網址
            $("#CopyURL").click(function () {
                //$("#myModal").dialog('open');
                $('#myTourCopy').modal('show');
                //$("#ctl00_ContentPlaceHolder1_btnCloseWindow").focus();
                return false;
            });

            // 一鍵複製
            function CopyTextToClipboard(id) {
                var TextRange = document.createRange();
                TextRange.selectNode(document.getElementById(id));
                var sel = window.getSelection();
                sel.removeAllRanges();
                sel.addRange(TextRange);

                document.execCommand("copy");
                $("#ShowContent").css("display", "");
                sel.removeAllRanges();
            }
        </script>
        <!----------------------------------------------------------------------------------------------------->
    </form>

    <!-- owl -->
    <script src="https://www.artisan.com.tw/js/owl.carousel.min.js"></script>
    <!-- flatpickr -->
    <script src="https://www.artisan.com.tw/js/flatpickr.js"></script>
    <!-- 讓picture能在ie執行 -->
    <script src="https://www.artisan.com.tw/js/picturefill.min.js"></script>
    <!-- tab -->
    <script src="https://www.artisan.com.tw/js/jQuery.easyTabs.js"></script>
    <script src="https://www.artisan.com.tw/js/simpletab.js"></script>
    <!-- js -->
    <script src="https://www.artisan.com.tw/js/index_2020.js"></script>


    <!-- TripIntroduction js -->
    <script type="text/javascript" src="https://www.artisan.com.tw/pic_css/fade_pic.js"></script>
    <script src="https://www.artisan.com.tw/js/jquery.c-share.js"></script>
    <script src="https://www.artisan.com.tw/js/jquery.sticky.js"></script>
    <script src="https://www.artisan.com.tw/js/webTrip_2020.js"></script>
</body>
</html>
