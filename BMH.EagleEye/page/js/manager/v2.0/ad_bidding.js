var yyCommon = function () {
    return {
        autoHeight: function () {//适应
            var resizeW = $(window).width();
            var windowH = $(window).height(); 
            $('.popup .tab-bd,.popup .table-content').height(windowH-120-187)
            if(resizeW<1230){                
                $('.contentSide').width(resizeW-114-17).css('float','left')
            }else{
                $('.contentSide').width(1100)
            }
            $('.leftSide').height($('.contentSide').outerHeight())
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
        yyHeadSlide:function () {//头部设置下拉
            $('.personInfo .set').click(function(e){
                $(this).prev().show()
                $('#YY-head .set').addClass('open')
                e.stopPropagation();
            })
            $('body,.personInfo .set-area a').click(function(){
                $('.personInfo .set-area').hide();
                $('#YY-head .set').removeClass('open')
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
        popoupShow: function (n) {//弹出弹出层
            var k=!0;
            $(".loginmask").css("opacity",0.8);   
            var h = ($(window).height()-n.outerHeight()) / 2
            if (n.css("top") != 0) {
                n.show(), $(".loginmask").fadeIn(500);
                n.animate({
                    top: h
                },{
                    duration:500,
                    easing:'easeInOutCirc'
                })

                $('.popup .tab-bd,.tableBody').niceScroll({ cursorcolor: "#12bdce",
                    cursoropacitymin: 1,
                    cursoropacitymax: 1,
                    touchbehavior: true,
                    cursorwidth: "5px",
                    cursorborder: "0",
                    cursorborderradius: "5px"
                }).resize()
            }
        },

        popoupHide: function (n) {//隐藏弹出层
            var k = !0;
            n.animate({ top: -2000 }, 400, function () { n.hide(); k = !0 }), $(".loginmask").fadeOut(500)

        },

        events: function () {
            $(".contentSide .advertising").click(function () {//竞价广告位管理弹出
                yyCommon.popoupShow($('.popup1'));
            });
            $(".contentSide .addAd").click(function () {//竞价广告位管理弹出
                yyCommon.popoupShow($('.popup2'));
            });

            $('.content').eq(0).find('.view a').click(function(){//代理商权重管理修改点击弹出
                yyCommon.popoupShow($('.popup5'));
                return false;
            })
            //代理商权重管理修改点击弹出内单选
            $('.popup5').find('.checkbox-con').each(function(){
                    var that = $(this);
                    that.find('.checkbox').click(function(){
                        $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
                    })
            })
            $('.popup5 button').click(function(){
                yyCommon.popoupHide($('.popup5'));
            })


            $('.content').eq(1).find('.view a').click(function(){//竞价广告位管理修改点击弹出
                yyCommon.popoupShow($('.popup4'));
                return false;
            })
            //竞价广告位管理修改点击弹出内单选
            $('.popup4').find('.checkbox-con').each(function(){
                    var that = $(this);
                    that.find('.checkbox').click(function(){
                        $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
                    })
            })
            $('.popup4 button').click(function(){
                yyCommon.popoupHide($('.popup4'));
            })


            
            $(".loginmask,.confirmT,.cancelT").click(function(){
                yyCommon.popoupHide($('.popup'));
             });
            $('.tableBody,.table-content').niceScroll({ cursorcolor: "#12bdce",//滚动条
                cursoropacitymin: 1,
                cursoropacitymax: 1,
                touchbehavior: true,
                cursorwidth: "5px",
                cursorborder: "0",
                cursorborderradius: "5px"
            }).resize()
            $('.popup .table-head').find('.checkbox').click(function(){
                var content = $(this).parents('.table-head').next();
                if($(this).hasClass('cur')){
                    $(this).removeClass('cur');
                    content.find('.checkbox').removeClass('cur')
                }else{
                    $(this).addClass('cur');
                    content.find('.checkbox').addClass('cur')
                }
                
            })         


            //投放范围弹出
            $(".popup2 .range").click(function () {
                yyCommon.popoupShow($('.popup3'));
                $('.loginmask3').fadeIn(400)
            });
            $(".loginmask3,.popup3 .btn").click(function(){
                var k = !0;
                $('.popup3').animate({ top: -2000 }, 400, function () { $('.popup3').hide(); k = !0 }),
                $('.loginmask3').fadeOut(400)
             });   
            //弹出层内投放范围选择
            $('.popup .table-content').find('.checkbox').click(function(event){
                var head = $(this).parents('.table-table-content').prev()
                if($(this).hasClass('cur')){
                    $(this).removeClass('cur');
                }else{
                    $(this).addClass('cur');
                }
                var _length1 = $(this).parents('.table-head').next().find('.checkbox').length;
                var _length2 = $(this).parents('.table-head').next().find('.cur').length;
                if(_length1 == _length2){
                    head.find('.checkbox').addClass('cur')
                }else{
                    head.find('.checkbox').removeClass('cur')
                }
                event.stopPropagation()
            })

            function rangeChoice(){
                var all = $('.popup3 tr:first-child').find('label'),
                     h5 = $('.popup3 tr').eq(1).find('td:first-child label'),
                     app = $('.popup3 tr').eq(2).find('td:first-child label'),
                     checkcon = $('.popup3').find('.checkbox');
                all.click(function(){
                    var other = $(this).parents('tr').nextAll();
                    if($(this).hasClass('cur')){
                        
                        other.find('.checkbox').removeClass('cur')
                    }else{
                        
                        other.find('.checkbox').addClass('cur')
                    }
                })
                h5.click(function(){
                    var son = $(this).parents('td').siblings();
                    if($(this).hasClass('cur')){
                        
                        son.find('.checkbox').removeClass('cur')
                    }else{
                        
                        son.find('.checkbox').addClass('cur')
                    }
                })
                app.click(function(){
                    var appson = $(this).parents('td').siblings();
                    if($(this).hasClass('cur')){
                        
                        appson.find('.checkbox').removeClass('cur');
                        $(this).parents('tr').nextAll().find('.checkbox').removeClass('cur')
                    }else{
                        
                        appson.find('.checkbox').addClass('cur');
                        $(this).parents('tr').nextAll().find('.checkbox').addClass('cur')
                    }
                })
                checkcon.click(function(){

                    if($(this).hasClass('cur')){
                        $(this).removeClass('cur');
                    }else{
                        $(this).addClass('cur');
                    }
                })


            }
            rangeChoice()

            

            

        },
        init: function () {
            $(window).resize(function () {
                yyCommon.autoHeight()
            })
            
            yyCommon.autoHeight();
            yyCommon.yyHeadSlide();
            yyCommon.selectBox();
            yyCommon.events();
            //yyCommon.popoup();
            yyCommon.leftSideDown()

        }
    }

} ()
yyCommon.init();
