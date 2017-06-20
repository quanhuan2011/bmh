(function(){
   function yyHeadSlide() {//头部设置下拉
            $('.personInfo .set').click(function(e){
                $(this).prev().show()
                $('#YY-head .set').addClass('open')
                e.stopPropagation();
            })
            $('body,.personInfo .set-area a').click(function(){
                $('.personInfo .set-area').hide();
                $('#YY-head .set').removeClass('open')
            })
    }
    yyHeadSlide();
    function selectBox(){//模拟下拉框
        var triangle = $('.slidebox').find('.sanjiao');
            var $w;
            $('.slidebox:not(.ad-page .slidebox) .box').each(function(){
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
            $('#matinfo_subadtype .slide dd').click(function () {
                var $txt = $(this).find("em").text();
                $(this).removeClass('cur').parents('.slide').prev().find('.title').text($txt);
                $(this).parents('.slide').slideUp();
                $('.slidebox').removeClass('is-open')
                $('.triangle').removeClass('open');
                //图片尺寸   
                $(".size .wid input").val($(this).find(".mat_width").text());
                $(".size .hei input").val($(this).find(".mat_height").text());
            })
    };
    selectBox();
    //是否打底
    $('#info_isbottom').find('.checkbox-con').each(function () {
        var that = $(this);
        var index = $(this).index();
        that.find('.checkbox').click(function () {
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            if (index == 1) {                
                $("#info_putrange").hide();
                $('.basic-info li.middle,.basic-info li.info_page,#info_isbid').show();
                $("#info_isbid .checkbox-con").eq(0).find(".checkbox").addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            } else {                
                $('.basic-info li.middle,.basic-info li.info_page,#info_isbid,#info_putrange').hide();
            }
        })
    })
    //是否竞价
    $('#info_isbid').find('.checkbox-con').each(function () {
        
        var that = $(this);
       var index = $(this).index();
        that.find('.checkbox').click(function(){
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            if(index == 1){
                $("#info_putrange").hide();
                $('.basic-info li.middle,.basic-info li.info_page').show();
            }else{
                $("#info_putrange").show();
                $('.basic-info li.middle,.basic-info li.info_page').hide();
            }
        })
    })
    //计费方式
    $('#info_btype').find('.checkbox-con').each(function () {
        var that = $(this);
        var index = $(this).index();
        that.find('.checkbox').click(function () {
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');           
        })
    })
    //终端选择
    $('#info_term').find('.checkbox-con').each(function () {
        var that = $(this);
        var index = $(this).index();
        that.find('.checkbox').click(function () {
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
        })
    })
    function rangeChoice(){//广告页选择
        var all = $('.ad-page .slidebox tr:first-child').find('label'),
             h5 = $('.ad-page .slidebox tr').eq(1).find('td:first-child label'),
             app = $('.ad-page .slidebox tr').eq(2).find('td:first-child label'),
             checkcon = $('.ad-page .slidebox').find('.checkbox');
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
    function  popoupShow(){//弹出弹出层
        var k=!0;
        $(".loginmask").css("opacity",0.8);   

        if($(".popup").css("top") != 0){
            $(".popup").show(),$(".loginmask").fadeIn(500);
            $(".popup").animate({top:0},400)
        }
        popupInner();
        var _html1 = '<div class="uploadImg uploadTxt"><p class="fl">测试测试测试测试测试测试测试测试</p><img class="fr" src="../images/pic1.png" alt=""></div>';
        var _html2 = '<div class="uploadImg"><img src="../images/pic1.png" alt=""></div>';
        var _html3 = '<div class="uploadImg"><img src="../images/pic1.png" alt=""></div>';
        var _html4 = '<div class="uploadImg pop"><h3>二级标题</h3><div><button>确定</button><button>取消</button></div></div>';
        $('.popup').find('.top a').click(function() {
					$(this).addClass('on').siblings().removeClass('on');
					$('.popup').find('li').addClass('hide');
					$('.uploadImg').remove();
					if ($(this).index() == 0) {
					    $('.popup').find('#matinfo_subadtype,.wlmc,.picFile,.size,.link,.targetW').removeClass('hide');
					    $('.popup').find('.btn').after(_html2)
					    GetSubAdTypeByAdType(1, "");
					}
                     else if($(this).index() == 1) {
                         $('.popup').find('#matinfo_subadtype,.wlmc,.picFile,.size,li.title,.link,.targetW').removeClass('hide');
                         $('.popup').find('.btn').after(_html1)
                         GetSubAdTypeByAdType(2, "");
					} 
                    else if ($(this).index() == 2) {
                        $('.popup').find('#matinfo_subadtype,.wlmc,.picFile,.size,.link,.popupTime,.targetW').removeClass('hide');
                        $('.popup').find('.btn').after(_html3)
                        GetSubAdTypeByAdType(3, "");
					} else if ($(this).index() == 3) {
					    $('.popup').find('#matinfo_subadtype,.wlmc,li.title,.picFile,.size,.btnArea,.link,.popupTime,.targetW').removeClass('hide');
					    $('.popup').find('.btn').after(_html4)
					    GetSubAdTypeByAdType(4, "");
					} else {
					    $('.popup').find('#matinfo_subadtype,.wlmc,.picFile,.size,.textarea,.link,.targetW').removeClass('hide');
					    $('.popup').find('.btn').after(_html1)
					    GetSubAdTypeByAdType(1, "");
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
                // $('.picFile').each(function(){//模拟图片文件单选
                //     var that = $(this);
                //     that.find('.checkbox').eq(0).click(function(){
                //         $(this).parent().next().addClass('on')
                //         $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').next().find('.ipt-hide').attr('checked','').next().removeClass('cur');
                        
                       
                //     })
                //     that.find('.checkbox').eq(1).click(function(){
                //         $(this).parents('.checkbox-con').prev().find('.upload').removeClass('on')
                //         $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').prev().find('.ipt-hide').attr('checked','').next().removeClass('cur');
                        
                //     })
                // })
                

                $('.targetW').find('.checkbox').click(function(){//模拟点击链接  安装包单选
                    $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').siblings('.checkbox-con').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                })
                // $('.link').find('.checkbox').click(function(){
                //     $(this).addClass('cur').prev().attr('checked','checked').parents('.link').next('.package').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                //     $('.package').find('.upload').removeClass('on')

                // })
                // $('.package').find('.checkbox').click(function(){//模拟目标窗口单选
                //     $(this).addClass('cur').prev().attr('checked','checked').parents('.package').prev('.link').find('.ipt-hide').attr('checked','').next().removeClass('cur');
                //     $('.package').find('.upload').addClass('on')
                // });
                 var maxLength = 30,num;//限制input输入字数
                 $('.textCounter').bind('input propertychange', function() {
                    $(this).next().html('还能输入 <em style="color:red">'+(maxLength-$(this).val().length) + '</em> 个字符');
                    if($(this).val().length>maxLength-1){
                        num=$(this).val().substr(0,maxLength-1); 
                        $(this).val(num);

                    }
                 if($(this).val().length == 0){
                        $(this).next().html('最多可输入30个字符')
                    }
                });
        };
  
    $('.regionalOrientation .box1').find('.checkbox-con').each(function(){
        var that = $(this);
       var index = $(this).index();
        that.find('.checkbox').click(function(){
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            $('.regionalOrientation .area').find('.fill').removeClass('on')
            if(index == 1){
                $('.regionalOrientation .area').fadeOut();
            }else if(index == 3){
                $('.regionalOrientation .area').fadeIn();
                $('.regionalOrientation .area').find('.fill').addClass('on')
            }else{

                $('.regionalOrientation .area').fadeIn();
            }
        })
    })
			   
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
//时间定向表格加载
var table_html = '<tr>';
for(var i=1;i<25;i++){
    table_html += '<th>'+i+'</th>'
}
table_html += '</tr>'
for(var i=0;i<7;i++){
    table_html  += '<tr>'
    for(var k=0;k<24;k++){
        table_html += '<td></td>'
    }
    table_html  += '</tr>'
}
$('.regionalOrientation .timeBox table').html(table_html)
//鼠标点击/拖动选择
$(document).mousedown(function(){                 
    $('.timeBox td').on('mouseover',function(e){      
          $(this).addClass('on') 
          var ev = e || event;
          ev.preventDefault;
    })  
    $(document).mouseup(function(){
        $('.timeBox td').off('mouseover');
        document.onmouseup=null;
    })

})  
$('.timeBox td').mousedown(function(){
        if(!$(this).hasClass('on')){           
            $(this).addClass('on')
        }else{
            $(this).removeClass('on')
        }
    })


$('.regionalOrientation .setting').find('.checkbox').click(function(){
    $(this).addClass('cur').prev().attr('checked','checked').parents('.checkbox-con').siblings('.checkbox-con').find('.ipt-hide').attr('checked','').next().removeClass('cur');
})
$('.allWeek').click(function(){
    $('.regionalOrientation .timeBox  td').removeClass('on')
    $('.regionalOrientation .timeBox tr:not(:first-child) td').addClass('on')
})
$('.monToFri').click(function(){
    $('.regionalOrientation .timeBox  td').removeClass('on')
    $('.regionalOrientation .timeBox tr:not(:first-child):lt(5) td').addClass('on')
})
$('.weekend').click(function(){
    $('.regionalOrientation .timeBox  td').removeClass('on')
    $('.regionalOrientation .timeBox tr:not(:first-child):gt(4) td').addClass('on')
})
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
//优先级设置
$('.set-platform .setNum .slide dd').click(function(){
    var dd_html = $(this).html();
    $(this).parents('.slide').prev().find('.title').html(dd_html)
})
        //投放量设置是否可用切换
        $('.limitBox').find('.limit').click(function(){
            if($(this).attr('checked')){
                $(this).removeAttr('checked').next().next().attr('disabled','disabled').val("999999999").parent().addClass('disables')                       
            }else{
                $(this).attr('checked','checked').next().next().removeAttr('disabled').val("0").parent().removeClass('disables')

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

        //广告物料选择
        var $system = $('.otherSet').find('.system'),
             $already = $('.otherSet').find('.already');
        var flag = 0;
        $system.find('p').click(function(){
            var txt = $(this).text();
            if($(this).hasClass('on')){
                return false;
            }
            $already.append('<p class="on"><span>'+txt+'</span><em class="del"></em></p>');  
            $(this).addClass('on');                   
            
        })
        $already.on('click','.del',function(){
            $(this).parent().remove();
            var txt = $(this).prev().text();
            $system.find('p').each(function(){
                if($(this).text() == txt){$(this).removeClass('on')}
            })
        })

        //新建物料
        $(".newAdwl").click(function(){
            var adTypeId=$("#info_adtype .cur").find("span").text();
            $("#selectMatType").find("a").eq(adTypeId-1).click();
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
     // var $option = '';//权重select赋值
     // for(var i=1;i<10;i++){
        // $option += '<option value="'+i+'">'+i+'</option>'
     // }
     // $('.setMaterial .three .weight').html($option)

$(".popup .tab-bd,.table-content,body,#info_adlocation .slide,#info_page .slide,.slide").niceScroll({  //调用滚动
cursorcolor:"#979797", 
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

 
    

