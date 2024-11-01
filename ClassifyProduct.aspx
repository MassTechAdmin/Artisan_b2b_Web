<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassifyProduct.aspx.cs" Inherits="ClassifyProduct" EnableEventValidation="false"%>
<%@ Register Src="WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport">
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link rel="stylesheet" href="css/owl.carousel.css">
    <link rel="stylesheet" href="css/owl.theme.default.css">
    <link rel="stylesheet" href="css/layout_b2b.css">
    <link rel="stylesheet" href="css/list_b2b.css">
    <!-- flatpicker -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <!-- flatpicker -->
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
	
	<style type="text/css">
        .ps {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
		<div id="wrapper">
			<!--b2b天-->
			<header>
				<nav>
					<uc1:Header ID="Header_Menu1" runat="server" />
				</nav>
			</header>
			<!--/b2b天-->
			
			<div id="b2b-content-index">
				<!-- 提醒標題 -->
				<section class="tip_warp">
					<div class="tip_box">
						<img src="images/Reminding.png" alt="">
						<span>請在以下列表中選擇適合您的出發日期。機位團體人數會因機位數減少或增加變動。機位欄的數字並不代表該團出團時的最終人數，實際情況請來電洽詢服務專員。</span>
					</div>
				</section>
				<!-- 提醒標題 -->

				<!-- 搜尋bar -->
				<section class="search_bar_warp">
					<div class="search_bar_box">
						<div class="go_box">
							<input class="flatpickr go" placeholder="出發日期" id="rdpDate" runat="server">
							<img src="./images/calendar.svg" alt=""> 
							
						</div>
						<div class="back_box">
							<input class="flatpickr back" placeholder="返回日期" id="rdpDate2" runat="server">
							<img src="./images/calendar.svg" alt=""> 
							
						</div>
						<div class="state_box">
							<asp:DropDownList ID="DropDownList1" runat="server" class="search-textBox" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"/>
							<img src="./images/placeholder.svg" alt="">
						</div>
						<div class="area_box">
							<asp:DropDownList ID="DropDownList2" runat="server" class="search-textBox" />
							<img src="./images/placeholder.svg" alt="">
						</div>
						<div class="search_box">
							<asp:TextBox ID="txbKey" runat="server" class="search-textBox" placeholder="關鍵字搜尋" />
							<img src="./images/search.svg" alt="">
						</div>
						<!--<input type="submit" name="imgButton" value="搜尋" id="imgButton" class="search-box-btn search_btn">-->
						<asp:Button runat="server" ID="imgButton" class="search-box-btn search_btn" OnClick="LinkButton1_Click" Text="搜尋" />
					</div>
				</section>
				<!-- 搜尋bar -->

				<!-- 篩選器 -->
				<section class="select_order_warp">
					<div class="select_order_title">
						排列順序
					</div>
					<div class="radio_box">
						<asp:RadioButtonList class="radio_box" runat="server" ID="RadioButtonList1" RepeatLayout="Flow" RepeatDirection="Horizontal" 
							AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
							<asp:ListItem Text="依出發日期" Value="0" Selected="True" />
							<asp:ListItem Text="依團體名稱" Value="1" />
							<asp:ListItem Text="依區域" Value="2" />
							<asp:ListItem Text="依天數" Value="3" />
							<asp:ListItem Text="依售價" Value="4" />
						</asp:RadioButtonList>
					</div>
				</section>
				<!-- 篩選器 -->
				<!-- 搜尋結果 -->
				<section class="table_wrap">
					<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Van_Number"
						AllowPaging="True" PageSize="20"  GridLines="none" CellSpacing="2" OnPageIndexChanging="GridView1_PageIndexChanging">
						<Columns>
							<asp:TemplateField>
								<HeaderTemplate>
									<div class="table_title_desktop">
										<div class="title_departure">出發日</div>
										<div class="title_name">團名</div>
										<div class="title_area">區域</div>
										<div class="title_days">天數</div>
										<div class="title_plant">班機</div>
										<div class="title_seat">機位</div>
										<div class="title_sign">報名</div>
										<div class="title_check">收訂</div>
										<div class="title_wait">候補</div>
										<div class="title_visa">簽證</div>
										<div class="title_tax">稅金</div>
										<div class="title_status">動態</div>
										<div class="title_price_peer">同業價</div>
										<div class="title_price_client">直購價</div>
										<div class="title_order">報名</div>
									</div>
								</HeaderTemplate>
								<ItemTemplate>
									<tr class="table_contant">
										<td class="departure">
    										    <span class="Syear"><%#Eval("Grop_Depa","{0:yyyy}")%></span>
                                                                                    <%#Eval("Grop_Depa","{0:MM/dd}")%>
                                                                                    (<%#Eval("wd","{0}")%>)</td>
										<td class="name">  
											<a href="#">
												<h2><asp:HyperLink ID="hlTripName" runat="server" 
													NavigateUrl='<%# fn_RtnGridViewGrop_Name(Eval("Area_No", "{0}"), Eval("Grop_Pdf", "{0}"), Eval("Trip_No", "{0}"), Eval("Grop_Depa", "{0:yyyy/MM/dd}"), Eval("Grop_Liner", "{0}"), Eval("TourType", "{0}"), Eval("trip_early_bird_url", "{0}"))%>'
													Text='<%#"<h2>" + Eval("Group_Name","{0}" + "</h2>")%>'/>
												</h2>
											</a>
											<table><tr><td>
												<span class="name_descript"><%# getGropIntro(Eval("Grop_Intro", "{0}"))%></span>
											</td></tr></table>
										</td>
										<td class="detail_wrap">
											<div class="area"><%#Eval("Area_Name", "{0}")%></div>
											<div class="days"><%#Eval("Grop_Day", "{0}")%></div>
											<div class="plant"><%#Eval("Grop_Liner", "{0}")%></div>
											<div class="seat"><%#Eval("Grop_Expect", "{0}")%></div>
											<div class="sign"><%# fn_regcheckpeople(Eval("reg_ok", "{0}"), Eval("reg_checkok", "{0}"), Eval("reg_standby", "{0}"), Eval("pak", "{0}"), Eval("Reg_Reserve", "{0}")
																, Eval("Pak_SignUp_Sync", "{0}"), Eval("Pak_ArCheck_Sync", "{0}"), Eval("Source_Agent_No", "{0}"), Eval("Tour_Kind", "{0}"))%></div>
											<div class="check"><%#fn_regcheckOKpeople(Eval("reg_checkok", "{0}"), Eval("reg_standby", "{0}"), Eval("pak", "{0}"), Eval("Pak_ArCheck_Sync", "{0}"), Eval("Source_Agent_No", "{0}"), Eval("Tour_Kind", "{0}"))%></div>
											<div class="wait"><%#Eval("Reg_Reserve", "{0}")%></div>
											<div class="visa"><%# fn_RtnGridViewGrop_Visa(Eval("Grop_Visa", "{0}"))%></div>
											<div class="tax"><%# fn_RtnGridViewGrop_Tax(Eval("Grop_Tax", "{0}"))%></div>
											
												<%# fn_RtnGridViewGrop_Close(Eval("Grop_Close", "{0}"), Eval("Grop_Expect", "{0}"), Eval("Reg_CheckOK", "{0}"), Eval("Reg_Standby", "{0}"), Eval("Pak", "{0}"), Eval("Reg_Ok", "{0}"), Eval("grop_ok", "{0}"), Eval("Grop_JoinTour", "{0}")
												, Eval("Pak_SignUp_Sync", "{0}"), Eval("Pak_ArCheck_Sync", "{0}"), Eval("Source_Agent_No", "{0}"), Eval("Tour_Kind", "{0}"), Eval("group_standby", "{0}"))%>
											
										</td>
										<td class="price_peer"><%# fn_RtnGridViewGrop_Tour(Eval("Agent_tour", "{0}"))%></td>
										<td class="price_client"><%# fn_RtnGridViewGrop_Tour(Eval("Grop_Tour", "{0}"))%></td>
										<td class="order">
											<asp:LinkButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" class="button" Text="報名" Visible='<%# fn_RtnGrop_Close(
											Eval("Grop_Close", "{0}"), Eval("Grop_Expect", "{0}"), Eval("Reg_CheckOK", "{0}"), Eval("Reg_Standby", "{0}"), Eval("Pak", "{0}"), 
											Eval("Reg_Ok", "{0}"), Eval("grop_ok", "{0}") ,Eval("Reg_Reserve", "{0}"), Eval("Grop_Depa", "{0:yyyy/MM/dd}"), Eval("Group_Category_No", "{0}"), 
											Eval("Area_No", "{0}"), Eval("Tour_Kind", "{0}") ,Eval("Van_Number", "{0}"), Eval("Group_Name_No", "{0}"), Eval("TourType", "{0}"))%>'/>
										</td>
									</tr>
								</ItemTemplate>
								
							</asp:TemplateField>
						</Columns>
						<PagerStyle CssClass="ps" />
					</asp:GridView>
					<asp:HiddenField ID="hidOrder" runat="server" />
					
					<!-- pagination -->
					<div role="navigation">
						<ul class="cd-pagination">
							<asp:Literal runat="server" ID="litPag" />
						</ul>
					</div>
					<!-- /pagination -->
					
				</section>
			</div>

			<!--b2b地-->
			<uc2:Foot ID="Foot1" runat="server" />
			<!--/b2b地-->
		</div>
		
		<script type="text/javascript" src="js/owl.carousel.min.js"></script>
        <script type="text/javascript" src="js/clickBlock.js"></script>
        <script type="text/javascript">
            $('#Special-owl-carousel').owlCarousel({
                items: 1,
                nav: true,
                dots: false,
                responsiveClass: true,
                loop: true,
                autoplay: true,
                autoplayTimeout: 5000,
                navigationText: false
            })
            $('#hotSale-owl-carousel').owlCarousel({
                loop: true,
                nav: true,
                dots: true,
                margin: 5,
                autoplay: true,
                autoplayTimeout: 5000,
                responsiveClass: true,
                responsive: {
                    0: {
                        items: 1,
                        nav: true,
                    },
                    500: {
                        items: 3,
                        nav: true,
                        margin: 20
                    },
                    992: {
                        items: 6,
                        nav: true,
                        margin: 20
                    }
                }
            })

            document.getElementsByClassName("flatpickr").flatpickr({
            });

            $(".select_order_title").click(function () {
                $(".radio_box").toggleClass("active")
            });



        </script>
    </form>
</body>
</html>
