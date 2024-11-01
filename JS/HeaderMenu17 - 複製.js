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
//主選單
$(function () {
    //選單展開
    $('#main_nav li').hover
    (function (e) {
            var _this = $(this).children('ul').children('.navTool').children('.main_navTool2');
            var id = $(this).index()-1;
            
            if($(this).children('ul').css('display') == 'none') {
            $('#main_nav li > ul').css('display', 'none');
            $(this).children('ul').css('display', '');
            
            //下方選單位移
            //if(id > 8) {_this.css({left: 420+(9*50)+(id-8)*5});}
            //else {_this.css({left: 420+(id*50)});}
            
            //if(id > 10) {_this.css({left: 138+(10*64)+(id-10)*15});}
            //else { _this.css({ left: (138 + (id * 63)) }); }
            switch(id)
            {
                case 0:
                    _this.css({ left: 138 });
                    break;
                case 1:
                    _this.css({ left: 201 });
                    break;
                case 2:
                    _this.css({ left: 264 });
                    break;
                case 3:
                    _this.css({ left: 328 });
                    break;
                case 4:
                    _this.css({ left: 392 });
                    break;
                case 5:
                    _this.css({ left: 458 });
                    break;
                case 6:
                    _this.css({ left: 519 });
                    break;
                case 7:
                    _this.css({ left: 583 });
                    break;
                case 8:
                    _this.css({ left: 647 });
                    break;
                case 9:
                    _this.css({ left: 721 });
                    break;
                case 10:
                    _this.css({ left: 794 });
                    break;
                case 11:
                    _this.css({ left: 830 });
                    break;
                case 12:
                    _this.css({ left: 866 });
                    break;
                case 13:
                    _this.css({ left: 890 });
                    break;
                default:
                    break;
            }
            }
        })
        
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

