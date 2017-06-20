(function(){
    function autoHeight(){
         var winh  =  $(window).height() - 105;
            $('#YY-main').height(winh)
    }
    autoHeight();

    function leftSideDown(){
        $('.leftSide').find('li>a').click(function(){
                $(this).next('dl').stop().slideToggle()
                if($(this).parent().hasClass('off')){
                    $(this).parent().removeClass('off')
                }else{
                    $(this).parent().addClass('off')
                }
        })
    }
    leftSideDown();
    function selectBox(){
            var triangle = $('.slidebox').find('.sanjiao');
            var $w;
            $('.slidebox .box').each(function(){
                $w = $(this).outerWidth();
                $(this).next().width($w)
            })
            $('.slidebox').on('click', '.box', function(e) {
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
            $('.slidebox').on('click', '.slide', function(e) {
                e.stopPropagation();
            })
            //点击body隐藏
            $('html').on('click', function(e) {
                $('.slide').slideUp(300);
                $('.slidebox').removeClass('is-open');
                $(this).find('.triangle').removeClass('open');
                
            });
            $('.slide dd').click(function(){
                var $txt = $(this).text();
                $(this).parents('.slide').prev().find('.title').text($txt);
                $(this).parents('.slide').slideUp();
                $('.triangle').removeClass('open');
            })
    }
    selectBox();

    function popoupShow(){
                $(".loginmask").css("opacity",0.8);  

                if($(".popup").css("top") != 0){
                    $(".popup").show(),$(".loginmask").fadeIn(500);
                    $(".popup").animate({top:0},400)
                }

                popupInner();
                var _html = '<li class="textarea"><label for="tpwj">文字内容：</label><textarea name="" id="" cols="30" rows="10"></textarea></li>';
                var _html2 = '<div class="uploadImg uploadTxt"><p class="fl">士大夫士大夫女生是电池惊魂女声第几次数据采集湖南卫视</p><img class="fr" src="images/pic1.png" alt=""></div>'
                var _html3 = '<div class="uploadImg"><img src="images/pic1.png" alt=""></div>'
                $('.popup').find('.top a').click(function(){
                    $(this).addClass('on').siblings().removeClass('on');
                    if($(this).index() == 0){
                        $('.popup').find('.textarea').remove();
                        $('.popup').find('.uploadTxt').remove()
                        $('.popup').find('.btn').after(_html3)
                    }else{
                        $('.popup').find('.uploadImg,.textarea').remove()
                        $('.popup').find('.size').after(_html);
                        
                        $('.popup').find('.btn').after(_html2)
                    }
                })
                $('.popup .tab-bd,.tableBody').niceScroll({cursorcolor:"#12bdce", 
                    cursoropacitymin:1, 
                    cursoropacitymax:1,   
                    cursorwidth:"5px",  
                    cursorborder:"0",  
                    cursorborderradius:"5px" ,
                    //horizrailenabled:false  
                }).resize()      
    }


    function popoupHide(){//隐藏弹出层
                var k=!0;
                $(".popup").animate({top:-640},400,function(){$(".popup").hide();k=!0}),$(".loginmask").fadeOut(500)        
                popupInner()
                    
    }
     function popupInner(){//弹出层内部事件
        $(function(){
            //模拟单选框
            $('.picFile').each(function(){//模拟图片文件单选
                var that = $(this);
                that.find('.checkbox').eq(0).click(function(){
                    $(this).parent().next().addClass('on')
                    $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').next().find('.ipt-hide').attr('checked','').next().removeClass('cur');
                    $(this).parents('.checkbox-con').find('.fileupload').removeClass('forbidden')
                   
                })
                that.find('.checkbox').eq(1).click(function(){
                    $(this).parents('.checkbox-con').prev().find('.upload').removeClass('on')
                    $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').prev().find('.ipt-hide').attr('checked','').next().removeClass('cur');
                    $(this).parents('.checkbox-con').siblings().find('.fileupload').addClass('forbidden')
                })
            })
            

            $('.targetW').find('.checkbox').click(function(){//模拟点击链接  安装包单选
                $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').siblings('.checkbox-con').find('.ipt-hide').attr('checked','').next().removeClass('cur');
            })
            $('.link').find('.checkbox').click(function(){
                $(this).addClass('cur').prev().attr('checked','checked').parents('.link').next('.package').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                $('.package').find('.upload').removeClass('on')
                $(this).parents('.link').next().find('.fileupload').addClass('forbidden')
                

            })
            $('.package').find('.checkbox').click(function(){//模拟目标窗口单选
                $(this).addClass('cur').prev().attr('checked','checked').parents('.package').prev('.link').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                $('.package').find('.upload').addClass('on')
                $(this).parents('.link').prev().find('.fileupload').removeClass('forbidden')
            })
            
        });
         //yyCommon.tabs(".tab-hd","on",".tab-bd");//弹出层选项卡调用

         var maxLength = 100,num;//限制input输入字数
         $('.textCounter').bind('input propertychange', function() {
            $(this).next().html('还能输入 <em style="color:red">'+(maxLength-$(this).val().length) + '</em> 个字符');
            if($(this).val().length>maxLength-1){
                num=$(this).val().substr(0,maxLength-1); 
                $(this).val(num);

            }
         if($(this).val().length == 0){
                $(this).next().html('最多可输入100个字符')
            }
        });
     }

     $(".newAccount").click(function(){
           $(".loginmask").css("opacity",0.8);  
            if($(".newAccountPopup").css("top") != 0){
                $(".loginmask").fadeIn(500);
                $(".newAccountPopup").animate({top:0},400)
            }
     });
    $(".loginmask,.confirm,.cancel").click(function(){
            $(".loginmask").css("opacity",0.8);  
                $(".loginmask").fadeOut(500);
                $(".newAccountPopup").animate({top:-1500},400)
    });
    $('.accountSettings').find('.checkbox').click(function(){//模拟点击链接  安装包单选
            $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').siblings('.checkbox-con').find('.ipt-hide').attr('checked','').next().removeClass('cur');
    })
    $(".popup .tab-bd,.tableBody").niceScroll({  //调用滚动条
      cursorcolor:"#12bdce", 
      cursoropacitymin:1, 
      cursoropacitymax:1,  
      touchbehavior:true,  
      cursorwidth:"5px",  
      cursorborder:"0",  
      cursorborderradius:"5px",
      horizrailenabled:false  
    });

    $('.editBox .del').click(function(){
        $(this).parents('tr').remove()
        return false;
    }) 



})()

 
    

