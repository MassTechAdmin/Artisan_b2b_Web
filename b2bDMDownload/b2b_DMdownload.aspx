<%@ Register Src="/WebControl/Foot_19.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="/WebControl/Header_Menu_19.ascx" TagName="Header" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,Chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	<meta content="width=device-width, initial-scale=1, user-scalable=0" name="viewport">
	<title>凱旋旅行社(巨匠旅遊)同業網</title>
	<link rel="stylesheet" href="/css/layout_b2b.css">
	
	<!-- Plugins -->
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/owl.carousel.css">
    <link rel="stylesheet" href="css/owl.theme.default.css">
	
	<!-- Style -->
    <link rel="stylesheet" href="css/b2b_dm.css">
	
    <script type="text/javascript" src="/js/jquery-2.1.3.min.js"></script>
    <script type="text/javascript" src="/js/b2b.js"></script>
</head>
<body>
<form id="form1" runat="server">
    <div id="wrapper">
        <!--天開始-->
        <!--替換b2b天-->
        <header>
        <nav>
            <uc1:Header ID="Header_Menu1" runat="server" />
        </nav>
        </header>
        <!--天結束-->
        <div id="content">
            <!-----修改地區----->
			<!-- content -->
			<div id="content-tool" class="dm-content">
				<div class="dm-top">
					<h1>當週DM下載</h1>
				</div>
				<hr style="height: 10px;background-color: #acacac">
				
				<div class="owl-carousel owl-theme dm-download">
					<div class="dm-pic">
						<a href="#">
							<img src="images/dm-downloadPic-1.jpg">
							<p>下載DM</p>
						</a>
					</div>
					<div class="dm-pic">
						<a href="#">
							<img src="images/dm-downloadPic-2.jpg">
							<p>下載DM</p>
						</a>
					</div>
					<div class="dm-pic">
						<a href="#">
							<img src="images/dm-downloadPic-3.jpg">
							<p>下載DM</p>
						</a>
					</div>
					<div class="dm-pic">
						<a href="#">
							<img src="images/dm-downloadPic-1.jpg">
							<p>下載DM</p>
						</a>
					</div>
				</div>

				<div class="dm-top flex-tool">
					<h1 class="dm-title">DM全覽</h1>
					<!--CheckBox-->
					<div class="filter_type">
						<label>全選</label>
						<label>台灣</label>
						<label>西歐</label>
						<label>東歐</label>
						<label>南歐</label>
						<label class="checked">北歐</label>
						<label>遊輪</label>
						<label>中國</label>
						<label>東南亞</label>  
						<label>古文明</label>
						<label>美洲</label>
						<label>紐澳</label> 
						<label>南亞</label> 
						<label>大非洲</label>
						<label>海島</label>
						<label>日本</label>
						<label>韓國</label> 
						<label>河輪</label>      
					</div>
					<!--CheckBox-->
				</div>

				<hr style="height: 10px;background-color: #acacac">
				<div class="dm-section">
					<a href="#" class="dm-list">
						<h3>新視界-北歐四國峽灣10天</h3>
						<p>下載DM</p>    
					</a>
					<a href="#" class="dm-list">
						<h3>新視界-北歐四國峽灣10天</h3>
						<p>下載DM</p>    
					</a>
					<a href="#" class="dm-list">
						<h3>新視界-北歐四國峽灣10天</h3>
						<p>下載DM</p>    
					</a>
					<a href="#" class="dm-list">
						<h3>新視界-北歐四國峽灣10天</h3>
						<p>下載DM</p>    
					</a>
					<a href="#" class="dm-list">
						<h3>新視界-北歐四國峽灣10天</h3>
						<p>下載DM</p>    
					</a>
				</div>

			</div>
			<!-- /content -->
            <!-----修改地區 end----->
        </div>
		<!--地開始-->
        <uc2:Foot ID="Foot1" runat="server" />
		<!--地結束-->
    </div>
	
	
	<!-- rwdImageMaps -->
	<script src="js/jquery.rwdImageMaps.min.js"></script>
	
    </form>
	
	<!-- Javascript files -->
    <script src="js/jquery-3.5.0.min.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/fontawesome.min.js"></script>

    <script type="text/javascript">
        $('.dm-download').owlCarousel({
            loop: true,
            autoplay: true,
            nav: true,
            navText: ["<img src='images/20_back.svg'>", "<img src='images/20_next.svg'>"],
            dots: false,
            dotsData: false,
            margin: 25,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 4
                }
            }

        })
    </script>
</body>
</html>