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
        var _html1 = '<div class="uploadImg uploadTxt"><p class="fl">测试测试测试测试测试测试测试测试</p><img class="fr" src="../images/pic1.png" alt=""></div>'
        var _html2 = '<div class="uploadImg"><img src="../images/pic1.png" alt=""></div>'
        var _html3 = '<div class="uploadImg"><img src="../images/pic1.png" alt=""></div>'
        var _html4 = '<div class="uploadImg pop"><h3>二级标题</h3><p>测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试</p><div><button>确定</button><button>取消</button></div></div>'
        $('.popup').find('.top a').click(function() {
					$(this).addClass('on').siblings().removeClass('on');
					$('.popup').find('li').addClass('hide');
					$('.uploadImg').remove();
					if ($(this).index() == 0) {
						$('.popup').find('.wlmc,.picFile,.size,.link,.targetW').removeClass('hide');
						$('.popup').find('.btn').after(_html2)
					}
                     else if($(this).index() == 1) {
						$('.popup').find('.wlmc,.picFile,.size,li.title,.link,.targetW').removeClass('hide');
					    $('.popup').find('.btn').after(_html1)
					} 
                    else if ($(this).index() == 2) {
						$('.popup').find('.wlmc,.picFile,.size,.link,.popupTime,.targetW').removeClass('hide');
						$('.popup').find('.btn').after(_html3)
					} else if ($(this).index() == 3) {
						$('.popup').find('.wlmc,li.title,.textarea,.btnArea,.link,.popupTime,.targetW').removeClass('hide');
						$('.popup').find('.btn').after(_html4)
					} else {
						$('.popup').find('.wlmc,.picFile,.size,.textarea,.link,.targetW').removeClass('hide');
						$('.popup').find('.btn').after(_html1)
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


    //定向设置
    //alert($('.regionalOrientation .box1 .checkbox').length)
    var provinceList = [
        {
            name:'中国',
            cityList:[         
                
            ]
        },
        {
            name:'华北地区', 
            cityList:[         
                {name:'北京',value:'北京'},{name:'天津',value:'天津'},{name:'河北',value:'河北'},{name:'内蒙古',value:'内蒙古'},{name:'山西',value:'山西'}
            ]
        },
        {
            name:'东北地区', 
            cityList:[         
                {name:'黑龙江',value:'黑龙江'},{name:'吉林',value:'吉林'},{name:'辽宁',value:'辽宁'}
            ]
        },
        {
            name:'华东地区', 
            cityList:[         
                {name:'上海',value:'shanghai'},{name:'安徽',value:'anhui'},{name:'福建',value:'fujian'},{name:'江苏',value:'jiangsu'},{name:'江西',value:'jiangxi'},{name:'山东',value:'shandong'},{name:'浙江',value:'zhejiang'}
            ]
        },
        {
            name:'华中地区', 
            cityList:[         
                {name:'河南',value:'henan'},{name:'湖北',value:'hubei'},{name:'湖南',value:'hunan'}
            ]
        },
        {
            name:'华南地区', 
            cityList:[         
                {name:'广东',value:'guangdong'},{name:'广西',value:'guangxi'},{name:'海南',value:'hainan'}
            ]
        },
        {
            name:'西南地区', 
            cityList:[         
                {name:'贵州',value:'guizhou'},{name:'贵州',value:'guizhou'},{name:'四川',value:'sichuan'},{name:'西藏',value:'xizang'},{name:'云南',value:'yunnan'}
            ]
        },
        {
            name:'西北地区', 
            cityList:[         
                {name:'广东',value:'guangd'},{name:'宁夏',value:'ningxia'},{name:'青海',value:'qinghai'},{name:'陕西',value:'shanxi2'},{name:'新疆',value:'xinjiang'}
            ]
        },
        {
            name:'港澳台', 
            cityList:[         
                {name:'澳门',value:'aomen'},{name:'香港',value:'hongkong'},{name:'台湾',value:'taiwan'}
            ]
        },
        {
            name:'其他',
            cityList:[         
                
            ]
        }
    ];

//    var $html='';
//    for(var i=0;i<provinceList.length;i++){
//        if(i == 0){
//            $html +='<dl class="region"><dt><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox china"></label></span><em>'+provinceList[i].name+'</em></div></dt></dl>'
//        }else{
//            $html +='<dl class="region"><dt><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox"></label></span><em>'+provinceList[i].name+'</em></div></dt>'
//            for(var k=0;k<provinceList[i].cityList.length;k++){
//                $html += '<dd><div class="checkbox-con checkBoxFather"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox"></label></span><em data-name="'+provinceList[i].cityList[k].value+'">'+provinceList[i].cityList[k].name+'</em><div class="number"><span class="selected">0</span>/<span class="all">5</span></div></div></dd>';
//            }
//            $html +='</dl>';
//        }
//        

//        
//    }
//    $('.area .fill').after($html);
    //$('.area').find('dl:even').addClass('add');
    //var hb_html = '<table cellspacing="0" border="0" ><tbody>';//河北
    //    hb_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>杭州</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>萧山</td></tr>'         
    //hb_html +='</tbody></table>'
    //$('.region dd em').after(hb_html)
    // var citys = [
    //     /*河北*/ ['石家庄','唐山市','秦皇岛','邯郸','邢台','保定','张家口','承德','沧州','廊坊','衡水',''],
    //     ['呼和浩特','包头','乌海市','赤峰市','通辽市','鄂尔多斯','呼伦贝尔','巴彦淖尔','乌兰察布','兴安','锡林郭勒','阿拉善'],
    //     ['太原市','大同市','阳泉市','长治市','晋城市','朔州市','晋中市','运城市','忻州市','临汾市','吕梁市',''],
    //     ['哈尔滨','齐齐哈尔','鸡西市','鹤岗市','双鸭山','大庆市','伊春市','佳木斯','七台河','牡丹江','黑河','绥化','大兴安岭'],
    //     ['长春市','吉林市','深圳市','四平市','辽源市','通化市','白山市','松原市','白城市','延边市'],
    //     ['沈阳市','大连市','鞍山市','抚顺市','本溪市','丹东市','锦州市','营口市','阜新市','辽阳市','盘锦市','铁岭市','朝阳市','葫芦岛'],
    //     ['合肥市','芜湖市','蚌埠市','淮南市','马鞍山','淮北市','铜陵市','安庆市','黄山市','滁州市','阜阳市','宿州市','巢湖市','六安市','亳州市','池州市','宣城市'],
    //     ['福州市','厦门市','莆田市','三明市','泉州市','漳州市','南平市','龙岩市','宁德市'],
    //     ['南京市','无锡市','徐州市','苏州市','南通市','连云港','淮安市','盐城市','扬州市','镇江市','泰州市','宿迁市'],
    //     ['南昌市','景德镇','萍乡市','九江市','新余市','鹰潭市','赣州市','吉安市','宜春市','抚州市','上饶市'],
    //     ['济南市','青岛市','淄博市','枣庄市','东营市','烟台市','随访市','济宁市','泰安市','辽阳市','盘锦市','铁岭市','朝阳市','葫芦岛']
    //     ['浙江','大连市','鞍山市','抚顺市','本溪市','丹东市','锦州市','营口市','阜新市','辽阳市','盘锦市','铁岭市','朝阳市','葫芦岛']]
    // var hb_html = '<table cellspacing="0" border="0" ><tbody>';//河北
    // for(var i=0,_length = citys[0].length;i<_length;i=i+2){
    //     hb_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[0][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[0][i+1]+'</td></tr>'       
    // }    
    // hb_html +='</tbody></table>'
    // $('em[data-name="河北"]').after(hb_html)

    // var nmg_html = '<table cellspacing="0" border="0" ><tbody>';//内蒙古
    // for(var i=0,_length = citys[1].length;i<_length;i=i+2){
    //     nmg_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[1][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[1][i+1]+'</td></tr>'
    // }   
    // nmg_html +='</tbody></table>'
    // $('em[data-name="内蒙古"]').after(nmg_html)

    // var shanxi_html = '<table cellspacing="0" border="0" ><tbody>';//山西
    // for(var i=0,_length = citys[2].length;i<_length;i=i+2){
    //     shanxi_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[2][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[2][i+1]+'</td></tr>'
    // }   
    // shanxi_html +='</tbody></table>'
    // $('em[data-name="山西"]').after(shanxi_html)

    // var hlj_html = '<table cellspacing="0" border="0" ><tbody>';//黑龙江
    // for(var i=0,_length = citys[3].length;i<_length;i=i+2){
    //     hlj_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[3][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[3][i+1]+'</td></tr>'
    // }   
    // hlj_html +='</tbody></table>'
    // $('em[data-name="黑龙江"]').after(hlj_html)

    // var jilin_html = '<table cellspacing="0" border="0" ><tbody>';//吉林
    // for(var i=0,_length = citys[2].length;i<_length;i=i+2){
    //     jilin_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[2][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[2][i+1]+'</td></tr>'
    // }   
    // jilin_html +='</tbody></table>'
    // $('em[data-name="吉林"]').after(jilin_html)

    // var liaoning_html = '<table cellspacing="0" border="0" ><tbody>';//辽宁
    // for(var i=0,_length = citys[2].length;i<_length;i=i+2){
    //     liaoning_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[2][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[2][i+1]+'</td></tr>'
    // }   
    // liaoning_html +='</tbody></table>'
    // $('em[data-name="辽宁"]').after(liaoning_html)

    // var gd_html = '<table cellspacing="0" border="0" ><tbody>';
    // for(var i=0,_length = citys[0].length;i<_length;i=i+2){
    //     gd_html += '<tr><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[0][i]+'</em></div></td><td><td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox cur"></label></span><em>'+citys[0][i+1]+'</td></tr>'
    // }   
    // gd_html +='</tbody></table>'
    // $('em[data-name="guangdong"]').after(gd_html)
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
	
	
	
    //$('.china').click(function(){//点击中国 全选
    //    if($(this).hasClass('cur')){
    //        $(this).removeClass('cur').parents('dl').siblings(':not(":last")').find('.checkbox').removeClass('cur');
    //    }else{
    //        $(this).addClass('cur').parents('dl').siblings(':not(":last")').find('.checkbox').addClass('cur');
    //    }
        
    //})
//    $('.region').each(function(){//点击每个区域全选或不全选
//        var that = $(this);
//        var num,selectNum;
//        that.find('dd').each(function(){//轮询每个省份的城市数量
//            num = $(this).find('table').find('td').length;

//            $(this).find('.all').text(num);
//        })
//        var _length = that.find('dd .checkbox').length;
//        that.find('dt .checkbox').click(function(event){
//            if($(this).hasClass('cur')){
//                $(this).removeClass('cur').parents('dt').siblings('dd').find('.checkbox').removeClass('cur')
//                $(this).parents('.checkbox-con').find('table').hide();
//            }else{
//                $(this).addClass('cur').parents('dt').siblings('dd').find('.checkbox').addClass('cur');
//                $(this).parents('.checkbox-con').find('table').show();
//            }  
//            check();
//        })
//        that.find('.checkBoxFather > .wrap .checkbox').click(function(event){//点击每个省
//            if($(this).hasClass('cur')){
//                $(this).removeClass('cur')
//                $('.checkBoxFather table').hide();
//                $(this).parents('.checkbox-con').find('table').hide();
//                $(this).parents('.checkBoxFather').find('.number').removeClass('show')//数量隐藏
//            }else{
//                $(this).addClass('cur')
//                $('.checkBoxFather table').hide();
//                $(this).parents('.checkbox-con').find('table').show();
//                $(this).parents('.checkBoxFather').find('table .checkbox').addClass('cur')//当点击省份前的label时 城市弹出 默认全选中
//                $(this).parents('.checkBoxFather').find('.number').addClass('show').find('.selected').text(num)//数量显示
//            }  
//         check();
//         event.stopPropagation()
//        })
//        that.find('.checkBoxFather > em[data-name]').click(function(event){//点击每个省
//            //$(this).prev().find('.checkbox').trigger('click')
//            $(this).parents('.checkbox-con').find('table').show();
//            event.stopPropagation()
//        })
//        that.find('.checkBoxFather table .checkbox').click(function(event){//点击每个城市
//            alert(2)
//            if($(this).hasClass('cur')){
//                $(this).removeClass('cur')                
//            }else{
//                $(this).addClass('cur')               
//            }  
//selectNum = $(this).parents('table').find('.cur').length;//获取城市选中数量
//            $(this).parents('.checkBoxFather').find('.number').addClass('show').find('.selected').text(selectNum)//选中城市数量输入到num里
//            if($(this).parents('table').find('.checkbox').length == $(this).parents('table').find('.cur').length){//判断城市是否全选来确定省是否选中
//                $(this).parents('table').prev().prev().find('label').addClass('cur');
//            }else{
//                $(this).parents('table').prev().prev().find('label').removeClass('cur');
//            }
//         check();
//         event.stopPropagation()
//        })
//        function check(){//判断每个区域内的城市是否全部选中，全部选中则该区域选中，反之亦然
//            if(that.find('dd .cur').length == _length){
//                that.find('dt .checkbox').addClass('cur');
//                
//            }else{
//                that.find('dt .checkbox').removeClass('cur');
//                
//            }
//            if($('.regionalOrientation .area .region:not(":last-child")').find('.cur').length == $('.regionalOrientation .area .region:not(":last-child")').find('.checkbox').length){
//                $('.china').addClass('cur');
//            }else{
//                $('.china').removeClass('cur');
//            }
//        }
//    })
    //$('.checkBoxFather table').find('.checkbox').addClass('cur')
    $('body').click(function(){
        $('.checkBoxFather table').hide();
    })

//alert($('.regionalOrientation .area .region:not(":last-child")').find('.cur').length)

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

 
    

