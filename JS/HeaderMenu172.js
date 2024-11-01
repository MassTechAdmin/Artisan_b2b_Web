//初始
$(function () {  
        $('.navTool .main_navTool2').css('right','auto');
        
        //20170314 主題企劃,口碑熱銷起始隨機
        var max = $('.SpecialEvent_tool div ul > li').length;
        var rand = Math.floor(Math.random() * (max))
        $('.SpecialEvent_tool div ul > li').eq(rand).children('a').addClass("selected");
        $('.SpecialEvent_tool .showbox li').eq(rand).css('display', '');

        max = $('.RecentlyHot_tool div ul > li').length;
        rand = Math.floor(Math.random() * (max))
        $('.RecentlyHot_tool div ul > li').eq(rand).children('a').addClass("selected");
        $('.RecentlyHot_tool .showbox li').eq(rand).css('display', '');

        $('.SpotLight_tool div ul > li').eq(0).children('a').addClass("selected");
        $('.SpotLight_tool .showbox li').eq(0).css('display', '');
        $('.ExceptionalChoices_tool img').eq(0).show();      
});
// 判斷是否為手機
function isMobile() {
    var a = navigator.userAgent || navigator.vendor || window.opera;
    return /(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s)|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg(g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4));
}

//主選單
$(function () {
    //選單展開
    if (isMobile()) {
        $('#main_nav li').click(function (e) {
            var _this = $(this).children('ul').children('.navTool').children('.main_navTool2');
            var id = $(this).index() - 1;

            if ($(this).children('ul').css('display') == 'none') {
                $('#main_nav li > ul').css('display', 'none');
                $(this).children('ul').css('display', '');

                //下方選單位移
                switch (id) {
                    case 0:
                        _this.css({ left: 102 });
                        break;
                    case 1:
                        _this.css({ left: 166 });
                        break;
                    case 2:
                        _this.css({ left: 230 });
                        break;
                    case 3:
                        _this.css({ left: 294 });
                        break;
                    case 4:
                        _this.css({ left: 358 });
                        break;
                    case 5:
                        _this.css({ left: 422 });
                        break;
                    case 6:
                        _this.css({ left: 486 });
                        break;
                    case 7:
                        _this.css({ left: 550 });
                        break;
                    case 8:
                        _this.css({ left: 614 });
                        break;
                    case 9:
                        _this.css({ left: 686 });
                        break;
                    case 10:
                        _this.css({ left: 758 });
                        break;
                    case 11:
                        _this.css({ left: 794 });
                        break;
                    case 12:
                        _this.css({ left: 830 });
                        break;
                    case 13:
                        _this.css({ left: 856 });
                        break;
                    default:
                        break;
                }
            }
        })
    } else {
        $('#main_nav li').hover(function (e) {
            var _this = $(this).children('ul').children('.navTool').children('.main_navTool2');
            var id = $(this).index() - 1;

            if ($(this).children('ul').css('display') == 'none') {
                $('#main_nav li > ul').css('display', 'none');
                $(this).children('ul').css('display', '');

                //下方選單位移
                switch (id) {
                    case 0:
                        _this.css({ left: 102 });
                        break;
                    case 1:
                        _this.css({ left: 166 });
                        break;
                    case 2:
                        _this.css({ left: 230 });
                        break;
                    case 3:
                        _this.css({ left: 294 });
                        break;
                    case 4:
                        _this.css({ left: 358 });
                        break;
                    case 5:
                        _this.css({ left: 422 });
                        break;
                    case 6:
                        _this.css({ left: 486 });
                        break;
                    case 7:
                        _this.css({ left: 550 });
                        break;
                    case 8:
                        _this.css({ left: 614 });
                        break;
                    case 9:
                        _this.css({ left: 686 });
                        break;
                    case 10:
                        _this.css({ left: 758 });
                        break;
                    case 11:
                        _this.css({ left: 794 });
                        break;
                    case 12:
                        _this.css({ left: 830 });
                        break;
                    case 13:
                        _this.css({ left: 856 });
                        break;
                    default:
                        break;
                }
            }
        })
    }


    //$('#main_nav li').click(function () {
    //    //$('#main_nav > li > ul').css('display', 'none');
    //    alert('111');
    //})

    //$('body').on('touchstart', '[data-hover]', function (e) {
    //    $(e.target).addClass('touching')
    //});
        
    /*/點擊消除
    $('.cancel_button > span').click
    (function () { $('#main_nav li > ul').css('display', 'none'); })
    $('.main_navTool').click
    (function () { $('#main_nav > li > ul').css('display', 'none'); })*/
    $('aside').click
    (function () { $('#main_nav > li > ul').css('display', 'none'); })
    $('#content').click
    (function () { $('#main_nav > li > ul').css('display', 'none'); })
    $('header').click
    (function () { $('#main_nav > li > ul').css('display', 'none'); })
    
    
    //移到選單外消除
    $('#main_nav  > li > ul > li').hover (
    function () { },
    function () { $('#main_nav > li > ul').css('display', 'none'); })
    $('nav').hover (
    function () { $('#main_nav > li > ul').css('display', 'none'); })
    
});
//主題企劃
$(function () {  
    //hover
    $('.SpecialEvent_tool div ul li').hover (
    function () {
        var no = $(this).index();
        $('.SpecialEvent_tool div ul li > a' ).removeClass("selected");
        $('.SpecialEvent_tool .showbox > li').css('display', 'none') 
        $(this).children('a').addClass("selected");
        $('.SpecialEvent_tool .showbox li').eq(no).show();
    },
    function () { 
        //$(this).children('a').removeClass("selected");
        //$('.SpecialEvent_tool .showbox li').eq($(this).index()).css('display', 'none');
    })
});


//口碑熱銷
$(function () {  
    //hover
    $('.RecentlyHot_tool div ul li').hover (
    function () {
        var no = $(this).index();
        $('.RecentlyHot_tool div ul li > a').removeClass("selected");
        $('.RecentlyHot_tool .showbox > li').css('display', 'none')
        $(this).children('a').addClass("selected");
        $('.RecentlyHot_tool .showbox li').eq(no).show();
    },
    function () { 
        //$(this).children('a').removeClass("selected"); 
    })
});
//注目焦點 
$(function () {  
    //hover
    $('.SpotLight_tool div ul li').hover (
    function () {
        var no = $(this).index();
        $('.SpotLight_tool div ul li > a').removeClass("selected");
        $('.SpotLight_tool .showbox > li').css('display', 'none')
        $(this).children('a').addClass("selected");
        $('.SpotLight_tool .showbox li').eq(no).show();
    },
    function () { 
        //$(this).children('a').removeClass("selected"); 
    })
});
//典藏&新視界
$(function () {  
    //hover
    $('.LinkWeb_tool .link li').hover (
    function () {
        $('.LinkWeb_tool .showbox > li').css('display', 'none');
        $('.LinkWeb_tool .link > li').removeClass("active");  
        $(this).addClass("active");
        //$('.LinkWeb_tool .showbox li').eq($(this).index()).slideToggle('400');
        $('.LinkWeb_tool .showbox li').eq($(this).index()).show();
    },
    function () {
    })
});

//選單固定上方
$(function () {
  $(window).scroll(function () {
    var scrollVal = $(this).scrollTop();
    
    $('nav').css('top',scrollVal);
    //$('nav').animate({top: scrollVal},100);
  });
});

//精選行程
$(function () {
    var timer, speed = 5000;    //計時器秒數 (毫秒)
    var id = 0;                 //起始位置編號
    var $span = $('.b2b-exceptional_button > span');
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

