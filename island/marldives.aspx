<%@ Page Language="C#" %>

<%@ Register Src="../WebControl/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>

<%@ Register Src="../WebControl/Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register src="../WebControl/Island_Tab.ascx" tagname="Island_Tab" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" >
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>凱旋旅行社(巨匠旅遊),歐洲旅遊,東歐旅遊,西歐旅遊,南歐旅遊,北歐旅遊,日本旅遊,紐西蘭旅遊,澳洲旅遊,中東旅遊,美國旅遊,加拿大旅遊,郵輪,非洲旅遊,歐洲自由行,南亞旅遊</title>
    <link href="../css/web.css" rel="stylesheet" type="text/css" />
    <link href="../css/color.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../js/Artisan_SubMenu.js" type="text/javascript"></script>
    <link href="../css/Artisan_SubMenu.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/elastislideIsLand.css" />
    <style type="text/css"> 
        div#abgne_marquee {
	       position: relative;
	        overflow: hidden;	/* 超出範圍的部份要隱藏 */
	        width: 800px;
	        height: 25px;
        	
	        left:240px;
	        top:10px;
	        
        }
        div#abgne_marquee ul, div#abgne_marquee li {
	        margin: 0;
	        padding: 0;
	        list-style: none;
        }
        div#abgne_marquee ul {
	        position: absolute;
	        left: 30px;			/* 往後推個 30px */
        }
        div#abgne_marquee ul li a {
	        display: block;
	        overflow: hidden;	/* 超出範圍的部份要隱藏 */
	        font-size:12px;
	        height: 55px;
	        line-height: 25px;
	        text-decoration: none;
        }
        div#abgne_marquee div.marquee_btn {
	        position: absolute;
	        cursor: pointer;
        }
        div#abgne_marquee div#marquee_next_btn {
	        left: 5px;
	        top:3px;
        }
        div#abgne_marquee div#marquee_prev_btn {
	        left: 700px;
	        top:3px;
        }
    </style>
    <script type="text/javascript">
        $(function(){
	    var $marqueeUl = $('div#abgne_marquee ul'),
		    $marqueeli = $marqueeUl.append($marqueeUl.html()).children(),
		    _height = $('div#abgne_marquee').height() * -1,
		    scrollSpeed = 600,
		    timer,
		    speed = 3000 + scrollSpeed,
		    direction = 0,	// 0 表示往上, 1 表示往下
		    _lock = false;
     
	    // 先把 $marqueeli 移動到第二組
	    $marqueeUl.css('top', $marqueeli.length / 2 * _height);
     
	    // 幫左邊 $marqueeli 加上 hover 事件
	    // 當滑鼠移入時停止計時器；反之則啟動
	    $marqueeli.hover(function(){
		    clearTimeout(timer);
	    }, function(){
		    timer = setTimeout(showad, speed);
	    });
     
	    // 判斷要往上還是往下
	    $('div#abgne_marquee .marquee_btn').click(function(){
		    if(_lock) return;
		    clearTimeout(timer);
		    direction = $(this).attr('id') == 'marquee_next_btn' ? 0 : 1;
		    showad();
	    });
     
	    // 控制跑馬燈上下移動的處理函式
	    function showad(){
		    _lock = !_lock;
		    var _now = $marqueeUl.position().top / _height;
		    _now = (direction ? _now - 1 + $marqueeli.length : _now + 1)  % $marqueeli.length;
     
		    // $marqueeUl 移動
		    $marqueeUl.animate({
			    top: _now * _height
		    }, scrollSpeed, function(){
			    // 如果已經移動到第二組時...則馬上把 top 設回到第一組的最後一筆
			    // 藉此產生不間斷的輪播
			    if(_now == $marqueeli.length - 1){
				    $marqueeUl.css('top', $marqueeli.length / 2 * _height - _height);
			    }else if(_now == 0){
				    $marqueeUl.css('top', $marqueeli.length / 2 * _height);
			    }
			    _lock = !_lock;
		    });
     
		    // 再啟動計時器
		    timer = setTimeout(showad, speed);
	    }
     
	    // 啟動計時器
	    timer = setTimeout(showad, speed);
     
	    $('a').focus(function(){
		    this.blur();
	    });
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
        <uc1:Header ID="Header1" runat="server" />
        <div id="content">
            <div id="island_topBanner"><img src="../images/island/banner.jpg" /></div>
             
            <uc3:Island_Tab ID="Island_Tab1" runat="server" />
         
            <div id="island_fit_tool">
                <div class="island_fit_title"><img src="../images/island/fit_title.jpg" width="1065" height="49" /></div>
                <div id="banner_tool">
                    <div id="carousel" class="es-carousel-wrapper">
				        <div class="es-carousel">
				            <ul>
                            <li><div class="island_fit_list"><img src="../images/island/01.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/02.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/03.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/04.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/01.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/02.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/03.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/04.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/01.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/02.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/03.jpg" width="237" height="145" /></div></li>
                            <li><div class="island_fit_list"><img src="../images/island/04.jpg" width="237" height="145" /></div></li>
                            </ul>
				        </div>
			        </div>
			    </div>
            </div>
         <!--
            <div id="hotNews_tool">
                <div id="abgne_marquee" >
                    <span class="up_button"><div class="marquee_btn" id="marquee_next_btn"><img src="../images/island/marquee_next_btn.gif" width="19" height="19"  /></div></span>
		            <ul style="left: 30px; top: -30px">
                        <li style="top: -30px"><div class="txt_news">10月31前check out，馬爾地夫住5晚(Hulhule Island Hotel*1晚+Constance Halaveli*4晚)86,500/人!</div></li>	
                        <li style="top: -30px"><div class="txt_news">10月31前check out，馬爾地夫6天4夜(Six Senses Laamu)64,500/人!</div></li>	
                        <li style="top: -30px"><div class="txt_news">10月31前check out，馬爾地夫6天4夜(悅榕庄-瓦賓法魯島)67,500/人!</div></li>	
                        <li style="top: -30px"><div class="txt_news">10月31前check out，馬爾地夫6天4夜(香格里拉)85,000/人!</div></li>			        
                    </ul>
                    <span class="down_button"><div class="marquee_btn" id="marquee_prev_btn"><img src="../images/island/marquee_prev_btn.gif" width="19" height="19" /></div></span>
	            </div>
            </div>
      -->
 <div id="hotHotel_tool">
            <div class="hotHotel_title"><img src="../images/island/hotel_list_title.jpg" width="1065" height="49" /></div>
            <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/Whotel.aspx"><img src="../images/island/001.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">W Retreat and Spa Maldives</div>
            <div class="data_title2">馬爾地夫W Hotel度假村</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/Whotel.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">碧藍的印度洋深處，坐落于寧靜優美的懷斯圖島上，這裏氣候宜人，終年陽光明媚，馬爾地夫W酒店是理想的療養勝地和水上運動樂園。在水下七彩光暈的籠罩下，海龜和海星遊曳嬉戲，構成一幅美麗的畫卷...</div>
            </div>
            </div>
            </div>
            
            <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/Vabbinfaru.aspx"><img src="../images/island/002.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Banyan Tree Maldives  Vabbinfaru</div>
            <div class="data_title2">馬爾地夫瓦賓法魯島-悅榕庄</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/Vabbinfaru.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">在馬爾地夫天堂般的珊瑚礁群，你將體會到獨一無二的馬爾地夫瓦賓法魯悅榕庄之神奇、浪漫，與寧靜。時間在這裡是靜止的，溫暖的陽光讓你尋回一度以為曾經失去的夢...
            </div></div>
            </div>
      </div>
      
      
      
      <div class="hotel_list">
        <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/FullMoon.aspx"><img src="../images/island/003.jpg" width="470" height="129" border="0" /></a></span>
        <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Sheraton Maldives  Full Moon Resort and Spa</div>
            <div class="data_title2">馬爾地夫 - 馬爾地夫喜來登芙夢島</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/FullMoon.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">
              從您一踏上我們風光秀麗的私人島嶼，您的放鬆之旅即告開始。從酒店乘船，短時間內即可抵達馬列國際機場和馬列市，酒店還可針對所有航班時間安排接送服務。
            </div></div>
        </div>
      </div>
      
      
      <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/IruFushi.aspx"><img src="../images/island/004.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Iru Fushi Resort and Spa</div>
            <div class="data_title2">馬爾地夫 - 伊露島度假村</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/IruFushi.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">伊露島渡假村位於Noonu Atoll諾努環礁。島上周圍有52英畝白色柔軟的沙灘，和茂密的熱帶綠色植物，美麗而遺世獨立。從馬列機場搭水上飛機只需花費45分鐘即可抵達。</div></div>
        </div>
      </div>
      
      
      <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/ShangriLa.aspx"><img src="../images/island/005.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Shangri-La   Villingili Resort Spa Maldives</div>
            <div class="data_title2">馬爾地夫 - 香格里拉-薇寧姬莉島</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/ShangriLa.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">
             在馬爾地夫，醉人的風景輕鬆地在眼前掠過，潔白的細沙輕柔地穿過腳趾，季候風帶著流水祝福的聲音飄過海洋……馬爾地夫展現在世人面前的不僅是視覺與聽覺魅力，還有觸覺的無限魅力...</div></div>
        </div>
      </div>
      
      <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/Velassaru.aspx"><img src="../images/island/006.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Velassaru Maldives</div>
            <div class="data_title2">馬爾地夫 - 薇拉莎露島</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/Velassaru.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">
             只有渡假行家才知道的Velassaru Maldives，是電影「藍色珊瑚礁」的拍攝地，因此島上的童話色彩特別濃，當然，更多的是不食人間煙火的感覺即以晶瑩剔透的海水藍與潔白珊瑚沙细膩著稱,拍攝&quot;藍色珊瑚礁...</div></div>
        </div>
      </div>
      
      
      
      <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/KudaHuraa.aspx"><img src="../images/island/007.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Four Seasons Resort Maldives at Kuda Huraa</div>
            <div class="data_title2">馬爾地夫四季酒店 - 庫達呼拉島</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/KudaHuraa.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">
             歡迎來到馬爾地夫的四季酒店-庫達呼拉島。從馬列機場碼頭,乘坐私人汽艇，約25分鐘的船程即可抵達。蘊藏在這個藍綠交錯的礁湖海域中的一座私人珊瑚島上,您會發現一個Maldivian式渡假村的魅力...</div></div>
        </div>
      </div>
      
  <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/ReethiRah.aspx"><img src="../images/island/008.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">One & Only Maldives Reethi Rah</div>
            <div class="data_title2">馬爾地夫 - 瑞堤拉島</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/ReethiRah.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">
             位於馬爾地夫群島的馬列北環礁。Reethi Rah距離馬列國際機場約35公里，搭乘豪華快艇約50 分鐘即可抵達。Reethi Rah的水上屋有別於一般飯店水上屋，單有正前方的單一海洋景緻...</div></div>
        </div>
      </div>

      <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/Halaveli.aspx"><img src="../images/island/009.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Constance Halaveli Resort Maldives</div>
            <div class="data_title2">馬爾地夫 - 康斯丹/哈拉薇麗度假酒店</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/Halaveli.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">
             康斯丹-哈拉薇麗度假酒店位於亞里北環礁，全島形狀猶如彎曲的dhoni馬爾地夫小船。 蔚藍的海洋、潔淨的沙灘和生氣蓬勃的熱帶植物叢林，在這裡時間彷彿靜止了，夢想也變得更真實...</div></div>
        </div>
      </div>
      
      <div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/Moofushi.aspx"><img src="../images/island/010.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Constance Moofushi Resort Maldives</div>
            <div class="data_title2">馬爾地夫 - 康斯丹/慕芙席度假酒店</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/Moofushi.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">康斯丹-慕芙席度假酒店坐落在亞里南環礁，被廣泛認為是在世界上最好的潛水點。精緻優雅的豪華度假村是擁有最高標準的酒店經驗的康斯丹集團所經營管理...</div></div>
        </div>
      </div>


<div class="hotel_list">
              <span class="hotel_pic"><a href="http://www.artisan.com.tw/island/hotel/SixSenses.aspx"><img src="../images/island/011.jpg" width="470" height="129" border="0" /></a></span>
              <div class="hotel_data_info">
            <div class="data_title_line">
            <div class="data_title1">Six Senses Laamu Maldives</div>
            <div class="data_title2">馬爾地夫 - 第六感拉姆度假村</div>
            <div class="hotel_moreButton"><a href="http://www.artisan.com.tw/island/hotel/SixSenses.aspx"><img src="../images/island/more_icon1.jpg" width="69" height="16" border="0" align="absmiddle" /></a>
            </div>
            <div class="hotel_txt_info">拉姆拉提杜渡假村位於馬爾地夫群島的南部的Laamu環礁中隱密的Olhuveli島上。由馬列國際機場搭40分鐘的飛機前往Kadhdhoo機場，然後搭15分鐘的小船即可抵達...</div></div>
        </div>
      </div>
      
    </div>
        <uc2:Foot ID="Foot1" runat="server" />
    </div>
    <script type="text/javascript" src="../js/jquery.elastislideIsLand.js"></script>
	<script type="text/javascript">
		$('#carousel').elastislide({
			imageW : 227,
			minItems : 4,
			onClick : true
		});
	</script>
    </form>
</body>
</html>



