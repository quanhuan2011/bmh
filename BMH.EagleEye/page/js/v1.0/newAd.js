(function(){

    function selectBox(){//模拟下拉框
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
                var $txt = $(this).find("em").text();
                $(this).removeClass('cur').parents('.slide').prev().find('.title').text($txt);
                $(this).parents('.slide').slideUp();
                $('.slidebox').removeClass('is-open')
                $('.triangle').removeClass('open');
                changeEqualWeight();
            })
            function changeEqualWeight (){
                if($('.setMaterial .title').text() == '手动权重'){
                    $('.manual').css('visibility','visible');
                }else{
                    $('.manual').css('visibility','hidden');
                }
            }
    };
    selectBox()

    //定向设置
    //alert($('.regionalOrientation .box1 .checkbox').length)
    $('.regionalOrientation .box1').find('.checkbox-con').each(function(){
        var that = $(this);
       var index = $(this).index();
        that.find('.checkbox').click(function(){
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            $('.regionalOrientation .area').find('.fill').removeClass('on')
            if(index == 1){
                $('.regionalOrientation .area').hide();
            }else if(index == 3){
                $('.regionalOrientation .area').show();
                $('.regionalOrientation .area').find('.fill').addClass('on')
            }else{
                $('.regionalOrientation .area').show();
            }
        })
    })
    $('.china').click(function(){//点击中国 全选
        if($(this).hasClass('cur')){
            $(this).removeClass('cur').parents('dl').siblings(':not(".otherArea")').find('.checkbox').removeClass('cur');
        }else{
            $(this).addClass('cur').parents('dl').siblings(':not(".otherArea")').find('.checkbox').addClass('cur');
        }
        
    })
    $('.region').each(function(){//点击每个区域全选或不全选
        var that = $(this);
        var _length = that.find('dd .checkbox').length;
        that.find('dt .checkbox').click(function(event){
            if($(this).hasClass('cur')){
                $(this).removeClass('cur').parents('dt').siblings('dd').find('.checkbox').removeClass('cur')
                $(this).parents('.checkbox-con').find('table').hide();
            }else{
                $(this).addClass('cur').parents('dt').siblings('dd').find('.checkbox').addClass('cur');
                $(this).parents('.checkbox-con').find('table').show();
            }  
            check();
        })
        that.find('.checkBoxFather > .wrap .checkbox').click(function(event){
            if($(this).hasClass('cur')){
                $(this).removeClass('cur')
                $(this).parents('.checkbox-con').find('table').hide();
            }else{
                $(this).addClass('cur')
                $(this).parents('.checkbox-con').find('table').show();
            }  
         check();
         event.stopPropagation()
        })
        that.find('.checkBoxFather table .checkbox').click(function(event){
            if($(this).hasClass('cur')){
                $(this).removeClass('cur')
                
            }else{
                $(this).addClass('cur')
                
            }  
         check();
         event.stopPropagation()
        })
        function check(){//判断每个区域内的城市是否全部选中，全部选中则该区域选中，反之亦然
            if(that.find('dd .cur').length == _length){
                that.find('dt .checkbox').addClass('cur');
            }else{
                that.find('dt .checkbox').removeClass('cur');
            }
        }
    })
    $('.checkBoxFather table').find('.checkbox').addClass('cur')
    $('body').click(function(){
        $('.checkBoxFather table').hide();
    })



//时间定向
$('.regionalOrientation .box2').find('.checkbox-con').each(function(){
        var that = $(this);
       var index = $(this).index();
        that.find('.checkbox').click(function(){
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            $('.regionalOrientation .timeBox').find('.fill').removeClass('on')
            if(index == 1){
                $('.regionalOrientation .timeBox').hide();
            }else{
                $('.regionalOrientation .timeBox').show();
            }
        })
    })
$('.regionalOrientation .timeBox').find('td').each(function(){
    alert($(this).index())
})
// $('.regionalOrientation .timeBox').find('td').click(function(){
//     if($(this).hasClass('on')){
//         $(this).removeClass('on');
//     }else{
//         $(this).addClass('on');
//     }
//     alert($(this).index())
// })




    function  popoupShow(){//弹出弹出层
        var k=!0;
        $(".loginmask").css("opacity",0.8);  

        if($(".popup").css("top") != 0){
            $(".popup").show(),$(".loginmask").fadeIn(500);
            $(".popup").animate({top:0},400)
        }
        popupInner();
        //var _html = '<li class="textarea"><label for="tpwj">文字内容：</label><textarea name="" id="" cols="30" rows="10"></textarea></li>';
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
               // $('.popup').find('.size').after(_html);
                
                $('.popup').find('.btn').after(_html2)
            }
        })
        $('.popup .tab-bd,.table-content').niceScroll({cursorcolor:"#12bdce", 
                    cursoropacitymin:1, 
                    cursoropacitymax:1,             
                    cursorwidth:"5px",  
                    cursorborder:"0",  
                    cursorborderradius:"5px" ,
                    //horizrailenabled:false  
                }).resize()
        popupInner();
    };
    function popoupHide(){//隐藏弹出层
            
       $(".popup").animate({top:-640},400,function(){$(".popup").hide();k=!0}),$(".loginmask").fadeOut(500)
        popupInner()
    };
        function popupInner(){//弹出层内部事件
                //模拟单选框
                $('.picFile').each(function(){//模拟图片文件单选
                    var that = $(this);
                    that.find('.checkbox').eq(0).click(function(){
                        $(this).parent().next().addClass('on')
                        $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').next().find('.ipt-hide').attr('checked','').next().removeClass('cur');
                        
                       
                    })
                    that.find('.checkbox').eq(1).click(function(){
                        $(this).parents('.checkbox-con').prev().find('.upload').removeClass('on')
                        $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').prev().find('.ipt-hide').attr('checked','').next().removeClass('cur');
                        
                    })
                })
                

                $('.targetW').find('.checkbox').click(function(){//模拟点击链接  安装包单选
                    $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').siblings('.checkbox-con').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                })
                $('.link').find('.checkbox').click(function(){
                    $(this).addClass('cur').prev().attr('checked','checked').parents('.link').next('.package').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                    $('.package').find('.upload').removeClass('on')

                })
                $('.package').find('.checkbox').click(function(){//模拟目标窗口单选
                    $(this).addClass('cur').prev().attr('checked','checked').parents('.package').prev('.link').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                    $('.package').find('.upload').addClass('on')
                });
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
        };

        //投放设置单选框
        $('.date-time-box').find('.checkbox').click(function(){
            $(this).addClass('cur').prev().attr('checked','checked').parents('dd').siblings().find('.checkbox').removeClass('cur').prev().attr('checked','')
        })
        $('.date-time-box').find('.checkbox').eq(0).click(function(){
            $('.endtime').hide()
        })
        $('.date-time-box').find('.checkbox').eq(1).click(function(){
            $('.endtime').show()
        })

        //投放量设置是否可用切换
        $('.limitBox').find('.limit').click(function(){
            if($(this).attr('checked')){
                $(this).removeAttr('checked').next().next().attr('disabled','disabled').parent().addClass('disables')                       
            }else{
                $(this).attr('checked','checked').next().next().removeAttr('disabled').parent().removeClass('disables')
            }
        })

         //其他设置单选框
        $('.otherSet').find('.checkbox').click(function(){
            $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur').prev().attr('checked','')
        })
        $('.otherSet').find('.checkbox').eq(0).click(function(){
            $('.otherSet').find('.choices').hide()
        })
        $('.otherSet').find('.checkbox').eq(1).click(function(){
            $('.otherSet').find('.choices').show()
        })

//        //广告物料选择
//        var $system = $('.otherSet').find('.system'),
//             $already = $('.otherSet').find('.already');
//        var flag = 0;
//        $system.find('p').click(function(){
//            var txt = $(this).find("em").text();
//            var strId=$(this).find("span").text();                        
//            if($(this).hasClass('on')){
//                return false;


//            }
//            $already.append('<p class="on"><span style="display:none">' + strId + '</span><em>'+txt+'</em><em class="del"></em></p>');  
//            $(this).addClass('on');                   




//            
//        })
//        $already.on('click','.del',function(){
//            $(this).parent().remove();
//            var txt = $(this).prev().text();
//            $system.find('p').each(function(){
//                if($(this).text() == txt){$(this).removeClass('on')}




//            })
//        })

        //新建物料
        $(".newAdwl").click(function(){
            popoupShow();
        });

        //弹出物料库
        function popupHeight(){
            $('.popup2').height($(window).height());//自适应高度
            var $h= $(window).height()-54-49-96;
            $('.table-content').height($h);
        }
        popupHeight()
        $(window).resize(function(){
            popupHeight()
        })
        
        $('.wlBox').click(function(){
            $('.loginmask2').css('opacity','.8').fadeIn();
            $('.popup2').animate({'top':0},500)
        })
        $('.table-head').find('.checkbox').click(function(){
            if($(this).hasClass('cur')){
                $(this).removeClass('cur');
                $('.table-content').find('.checkbox').removeClass('cur')
            }else{
                $(this).addClass('cur');
                $('.table-content').find('.checkbox').addClass('cur')
            }
            
        })            
        $('.table-content').find('.checkbox').click(function(event){
            if($(this).hasClass('cur')){
                $(this).removeClass('cur');
            }else{
                $(this).addClass('cur');
            }
            var _length1 = $('.table-content').find('.checkbox').length;
            var _length2 = $('.table-content').find('.cur').length;
            if(_length1 == _length2){
                $('.table-head').find('.checkbox').addClass('cur')
            }else{
                $('.table-head').find('.checkbox').removeClass('cur')
            }
            event.stopPropagation()
        })
        $(".loginmask2,.confirmT,.cancelT").click(function(){
            $('.loginmask2').css('opacity','.8').fadeOut();
            $('.popup2').animate({'top':-1000},500)
         });

        //点击弹出框隐藏
     $(".loginmask,.confirm,.cancel").click(function(){
            popoupHide();
    });

     //点击删除
     $('.setMaterial .three').find('button').click(function(){
        $(this).parents('tr').remove()
     })
//     var $option = '';//权重select赋值
//     for(var i=1;i<10;i++){
//        $option += '<option value="'+i+'">'+i+'</option>'
//     }
//     $('.setMaterial .three .weight').html($option)

     $(".popup .tab-bd,.table-content").niceScroll({  //调用滚动
        cursorcolor:"#12bdce", 
        cursoropacitymin:1, 
        cursoropacitymax:1,          

        cursorwidth:"5px",  
        cursorborder:"0",  
        cursorborderradius:"5px",
        horizrailenabled:false  
    }); 

      $(function(){  
      //日历
      $('.some_class').datetimepicker();
    })

})()

 
    

