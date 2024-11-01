//精選行程-登入頁
$(function () {
    var timer, speed = 5000;    //計時器秒數 (毫秒)
    var id = 0;                 //起始位置編號
    var $span = $('.b2b-exceptional-button > span');
    var $_ul = $('.b2b-exceptional-right > ul');
    var $img = $('.b2b-exceptional-left > img');

    //設定起始位置
    $span.eq(id).addClass("active");
    $_ul.eq(id).css({ 'display': '' });

    timer = setTimeout(autoClick, speed);   //啟動計時器
    //自動切換
    function autoClick() {
        var no = id;

        clearTimeout(timer);    //清除計時器

        // 計算出下一個要展示選項
        no = (no + 1) % $span.length;
        $span.removeClass("active");
        $span.eq(no).addClass("active");
        imgOutput2(no);

        if (no == 0 && id > 1000) { id = 0; }
        else { id++; }

        timer = setTimeout(autoClick, speed);   //啟動計時器
    }

    //hover
    $(function () {
        $span.hover(
        function () {
            $span.removeClass("active");
            $(this).addClass("active");
            id = $(this).index();   //紀錄滑鼠碰觸位置
            imgOutput2(id);
            clearTimeout(timer)  //清除計時器
            ;
        },
        function () {
            id = $(this).index();   //紀錄滑鼠碰觸位置
            timer = setTimeout(autoClick, speed);  //啟動計時器
        })
    });
    $(function () {
        $('.b2b-exceptional-right > ul > li').hover(
        function () {
            clearTimeout(timer);  //清除計時器
        },
        function () {
            timer = setTimeout(autoClick, speed);  //啟動計時器
        })
    });
    //圖片顯現
    function imgOutput2(no) {
        $_ul.css({ 'display': 'none' });
        $img.css({ 'display': 'none' });
        $img.eq(no).css({ 'display': '' });
        $_ul.eq(no).css({ 'display': '' });
    }
});

