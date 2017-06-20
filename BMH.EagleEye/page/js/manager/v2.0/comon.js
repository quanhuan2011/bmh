var yyCommon = function() {
		return {
			autoHeight: function() {
				var winh = $(window).height() - 105;
				$('#YY-main').height(winh);
				$('.tableBody').height(winh - 60 - 49 - 70-15)
			},
			leftSideDown: function() { //左侧导航下拉效果
				$('.leftSide').find('li>a').click(function() {
					$(this).next('dl').stop().slideToggle()
					if ($(this).parent().hasClass('off')) {
						$(this).parent().removeClass('off')
					} else {
						$(this).parent().addClass('off')
					}
					return false;
				})
			},
			selectBox: function() { //模拟select
				var triangle = $('.slidebox').find('.sanjiao');
				var $w;
				$('.slidebox .box').each(function() {
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
				
				//点击body隐藏
				$('html').on('click', function(e) {
					$('.slide').slideUp(300);
					$('.slidebox').removeClass('is-open');
					$(this).find('.triangle').removeClass('open');

				});
				$('.slide dd').click(function() {
					var $txt = $(this).find("em").text();
					$(this).parents('.slide').prev().find('.title').text($txt);
					$(this).parents('.slide').slideUp();
					$('.triangle').removeClass('open');
				})
			},
			tableEdit: function() {
				//表格编辑左移效果
				$('.edit').each(function() {
					var $h = $(this).parents('td').outerHeight() - 1;
					$(this).height($h).find('a').css('lineHeight', $h + 'px')
				})
				var $edit = $('.editBox').find('img');
				$edit.click(function() {
					$(this).parents('.editBox').addClass('on');
				})
				$('.editBox .edit').hover(function() {}, function() {
					$(this).parents('.editBox').removeClass('on');
				})
			},
			popoupShow: function() { //弹出弹出层
				var k = !0;
				$(".loginmask").css("opacity", 0.8);

				if ($(".popup").css("top") != 0) {
					$(".popup").show(), $(".loginmask").fadeIn(500);
					$(".popup").animate({
						top: 0
					}, 400)
				}
				yyCommon.popupInner();
				var _html1 = '<div class="uploadImg uploadTxt"><p class="fl">内容</p><img class="fr" src="../images/pic1.png" alt=""></div>'
				var _html2 = '<div class="uploadImg"><img src="../images/pic1.png" alt=""></div>'
				var _html3 = '<div class="uploadImg"><img src="../images/pic1.png" alt=""></div>'
				var _html4 = '<div class="uploadImg pop"><h3>二级标题</h3><p>内容</p><div><button>确定</button><button>取消</button></div></div>'
				$('.popup').find('.top a').click(function() {
					$(this).addClass('on').siblings().removeClass('on');
					$('.popup').find('li').addClass('hide');
					$('.uploadImg').remove();
					if ($(this).index() == 0) {
						$('.popup').find('.wlmc,.picFile,.size,.link,.targetW').removeClass('hide');
						$('.popup').find('.btn').after(_html2)
					}
                     else if($(this).index() == 1) {
						$('.popup').find('.wlmc,.picFile,.size,.textarea,.link,.targetW').removeClass('hide');
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
				$('.popup .tab-bd,.tableBody').niceScroll({
					cursorcolor: "#12bdce",
					cursoropacitymin: 1,
					cursoropacitymax: 1,
					cursorwidth: "5px",
					cursorborder: "0",
					cursorborderradius: "5px",
					//horizrailenabled:false  
				}).resize()

			},
			popoupHide: function() { //隐藏弹出层
				var k = !0;

				$(".popup").animate({
					top: -640
				}, 400, function() {
					$(".popup").hide();
					k = !0
				}), $(".loginmask").fadeOut(500)

				yyCommon.popupInner()

			},
			popupInner: function() { //弹出层内部事件
				$(function() {
					//模拟单选框

					$('.targetW').find('.checkbox').click(function() { //模拟点击链接  安装包单选
						$(this).addClass('cur').prev().attr('checked', 'checked').parents('.checkbox-con').siblings('.checkbox-con').find('.ipt-hide').attr('checked', '').next().removeClass('cur');
					})
					// $('.link').find('.checkbox').click(function(){
					//     $(this).addClass('cur').prev().attr('checked','checked').parents('.link').next('.package').find('.ipt-hide').attr('checked','').next().removeClass('cur');
					//     $('.package').find('.upload').removeClass('on')
					//     $(this).parents('.link').next().find('.fileupload').addClass('forbidden')
					//     $(this).parents('.link').find('.txt').removeAttr('disabled')

					// })
					// $('.package').find('.checkbox').click(function(){//模拟目标窗口单选
					//     $(this).addClass('cur').prev().attr('checked','checked').parents('.package').prev('.link').find('.ipt-hide').attr('checked','').next().removeClass('cur');
					//     $('.package').find('.upload').addClass('on')
					//     $(this).parents('.package').prev().find('.fileupload').removeClass('forbidden')
					//     $(this).parents('.package').siblings().find('.txt').attr('disabled','disabled')
					// })

				});
				//yyCommon.tabs(".tab-hd","on",".tab-bd");//弹出层选项卡调用

				var maxLength = 30,
					num; //限制input输入字数
				$('.textCounter').bind('input propertychange', function() {
					$(this).next().html('还能输入 <em style="color:red">' + (maxLength - $(this).val().length) + '</em> 个字符');
					if ($(this).val().length > maxLength - 1) {
						num = $(this).val().substr(0, maxLength - 1);
						$(this).val(num);

					}
					if ($(this).val().length == 0) {
						$(this).next().html('最多可输入30个字符')
					}
				});
			},
			// tabs:function(tabTit,on,tabCon){//选项卡
			//    $(tabCon).each(function(){
			//      $(this).children().eq(0).show().siblings().hide();
			//      });
			//    $(tabTit).each(function(){
			//      $(this).children().eq(0).addClass(on).siblings().removeClass(on);
			//      });
			//    $(tabTit).children().click(function(){
			//        $(this).addClass(on).siblings().removeClass(on);
			//         var index = $(tabTit).children().index(this);
			//         $(tabCon).children().eq(index).show().siblings().hide();
			//    });
			// },
			init: function() {
				$(window).resize(function() {
					yyCommon.autoHeight()
				})
				yyCommon.autoHeight();
				yyCommon.selectBox();
				yyCommon.tableEdit();
				//yyCommon.popoup();
				yyCommon.leftSideDown()

			}
		}

	}()
	yyCommon.init();