$(function() {
    var $ad = $('#AD'),
        $adMask = $('#AD_mask'),
        $closeBtn = $('.AD_close'),
        $adImg = $('.AD_img');

    var winHeight = $(window).height(),
        winWidth = $(window).width(),
        ADheight = $adImg.height(),
        ADwidth = $adImg.width();
    
    //alert(winHeight + "\n" + winWidth + "\n" + ADheight + "\n" + ADwidth);

    if (winWidth < 500) {
        $('#AD_mask').css({
            'top': (winHeight) * 0.2
        });
    }
    else if (winWidth < 768) {
        $('#AD_mask').css({
            'top': (winHeight) * 0.1
        });
    }
    else {
        $('#AD_mask').css({
            'top': (winHeight) * 0.25,
            'left': (winWidth) * 0.35
        });
    }
    
    

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
});
