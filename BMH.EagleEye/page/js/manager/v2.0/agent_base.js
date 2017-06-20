var yyCommon = function () {
    return {
        autoHeight: function () {
            var winh = $(window).height() - 69;
            $('#YY-main').height(winh);
            $('.tableBody').height(winh - 96-35-45)

        },
        leftSideDown: function () {//左侧导航下拉效果
            $('.leftSide').find('li>a').click(function () {
                $(this).next('dl').stop().slideToggle()
                if ($(this).parent().hasClass('off')) {
                    $(this).parent().removeClass('off')
                } else {
                    $(this).parent().addClass('off')
                }
                return false;
            })
        },
        selectBox: function () {//模拟select
            var triangle = $('.slidebox').find('.sanjiao');
            var $w;
            $('.slidebox .box').each(function () {
                $w = $(this).outerWidth();
                $(this).next().width($w)
            })
            $('.slidebox').on('click', '.box', function (e) {
                var parent = $(this).closest('.slidebox');
                if (!parent.hasClass('is-open')) {
                    $(this).next().slideDown(300);
                    parent.addClass('is-open')
                    $(this).find('.triangle').addClass('open');
                } else {
                    $(this).next().slideUp(300);
                    parent.removeClass('is-open');
                    $(this).find('.triangle').removeClass('open');
                }
                e.stopPropagation();
            })
            $('.slidebox').on('click', '.slide', function (e) {
                e.stopPropagation();
            })
            //点击body隐藏
            $('html').on('click', function (e) {
                $('.slide').slideUp(300);
                $('.slidebox').removeClass('is-open');
                $(this).find('.triangle').removeClass('open');

            });
            $('.slide dd').click(function () {
                var $txt = $(this).find("em").text();
                $(this).parents('.slide').prev().find('.title').text($txt);
                $(this).parents('.slide').slideUp();
                $('.triangle').removeClass('open');
            })
        },
        popoupShow: function () {//弹出弹出层
            var k = !0;
            $(".loginmask").css("opacity", 0.8);

            if ($(".popup").css("top") != 0) {
                $(".popup").show(), $(".loginmask").fadeIn(500);
                $(".popup").animate({ top: 0 }, 400)
            }

            $('.popup .tab-bd,.tableBody').niceScroll({ cursorcolor: "#12bdce",
                cursoropacitymin: 1,
                cursoropacitymax: 1,
                touchbehavior: true,
                cursorwidth: "5px",
                cursorborder: "0",
                cursorborderradius: "5px"
            }).resize()

        },

        popoupHide: function () {//隐藏弹出层
            var k = !0;
            $(".popup").animate({ top: -640 }, 400, function () { $(".popup").hide(); k = !0 }), $(".loginmask").fadeOut(500)

        },

        events: function () {
            $(".contentSide .new").click(function () {
                yyCommon.popoupShow();
            });
            $('.cancel,.loginmask').click(function(){
                yyCommon.popoupHide();
                $('#newAgent .last').hide();
                $('#newAgent .aptitude').hide();
                $('#newAgent .main').show()
                $('.popbutton').html('<button class="next nextOne">下一步</button><button class="next nextTwo" style="display:none;background:#12bdcf">下一步</button><button class="cancel">取消</button>')
                $('#newAgent .top').css('background', '#f6f6f6 url(../images/lc1.jpg) center center no-repeat');
            })
            //$('.popup').on('click','.nextOne',function(event){
                
            //    $('#newAgent .top').css('background','#f6f6f6 url(../images/lc2.jpg) center center no-repeat');
            //    $('#newAgent .main').hide()
            //    $('#newAgent .aptitude').show();
            //    $(this).hide();
            //    $('.nextTwo').show()
                
            //})
            $('.popup').on('click','.nextTwo',function(event){
                $('#newAgent .top').css('background','#f6f6f6 url(../images/lc3.jpg) center center no-repeat');
                $('#newAgent .aptitude').hide();
                $('#newAgent .last').show()
                $('.popbutton').html('<button style="width:138px" class="complate">完成</button>')
            })
            $('.popup').on('click','.complate',function(event){ 
                yyCommon.popoupHide();
                $('#newAgent .last').hide();
                $('#newAgent .main').show()
                $('.popbutton').html('<button class="next nextOne">下一步</button><button class="next nextTwo" style="display:none;background:#12bdcf">下一步</button><button class="cancel">取消</button>')
                $('#newAgent .top').css('background','#f6f6f6 url(../images/lc1.jpg) center center no-repeat');
            })
            $('#newAgent .aptitude').niceScroll({ cursorcolor: "#12bdce",
                cursoropacitymin: 1,
                cursoropacitymax: 1,
                touchbehavior: true,
                cursorwidth: "5px",
                cursorborder: "0",
                cursorborderradius: "5px"
            }).resize()
        },
        init: function () {
            $(window).resize(function () {
                yyCommon.autoHeight()
            })
            
            yyCommon.autoHeight();
            yyCommon.selectBox();
            yyCommon.events();
            //yyCommon.popoup();
            yyCommon.leftSideDown()

        }
    }

} ()
yyCommon.init();

