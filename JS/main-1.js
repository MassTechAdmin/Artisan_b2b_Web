$(function() {
    var $ad = $('#AD'),
        $adMask = $('.AD_mask'),
        $closeBtn = $('.AD_close'),
        $adImg = $('.AD_img');

    $adImg.ready(function(){
        var winHeight = $(window).height(),
            winWidth = $(window).width(),
            ADheight = $adImg.height(),
            ADwidth = $adImg.width();

        //設定廣告垂直水平置中
        $adMask.css({
            'top': (winHeight - ADheight) / 2,
            'left': (winWidth - ADwidth) / 2
        });
    });
    
    var timer = null;
    var num = $('#adnum').val();

    if (num != 0) { setTimer(); }
    

    //5秒後自動消失動畫
    function setTimer() {
        timer = setTimeout(function() {
        	$ad.slideUp(300).fadeOut(0);
        }, num);
    }

    $adMask.hover(function() {
        // 滑鼠移入廣告，停止消失動畫，避免觀看時廣告消失
        clearTimeout(timer);
    }, function() {
        // 重新啟動消失動畫
        if (num != 0) { setTimer(); }
    });

    //點選關閉按鈕
    $closeBtn.click(function(event) {
        event.preventDefault();
        $ad.slideUp(300).fadeOut(0);
    });
	
	
	var t_img; // 定時器
	var isLoad = true; // 控制變數
	// 判斷圖片載入狀況，載入完成後回撥
	isImgLoad(function(){
	// 載入完成
	});
	// 判斷圖片載入的函式
	function isImgLoad(callback){
		// 注意我的圖片類名都是cover，因為我只需要處理cover。其它圖片可以不管。
		// 查詢所有封面圖，迭代處理
		//alert('111');
		$('.AD_img').each(function(){
			// 找到為0就將isLoad設為false，並退出each
			if(this.height === 0){
				isLoad = false;
				return false;
			}
		});
		// 為true，沒有發現為0的。載入完畢
		if(isLoad){
			clearTimeout(t_img); // 清除定時器
			// 回撥函式
			callback();
		// 為false，因為找到了沒有載入完成的圖，將呼叫定時器遞迴
		}else{
			isLoad = true;
			t_img = setTimeout(function(){
			isImgLoad(callback); // 遞迴掃描
			},500); // 我這裡設定的是500毫秒就掃描一次，可以自己調整
		}
	}
});
