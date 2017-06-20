var yyCommon = function () {
    return {
        autoHeight: function () {
            var winh = $(window).height() + 155;
            $('#user-main').height(winh);
        },        
        select: function () {//模拟select
            $('.choice input').click(function (event) {
                if ($(this).parents('li').hasClass('open')) {
                    $(this).parent().next().slideUp(200).parents('li').removeClass('open');
                } else {
                    $(this).parent().next().slideDown(200).parents('li').addClass('open');
                }
                event.stopPropagation();
            })
            $('.choice em').click(function (event) {
                var $txt = $(this).text();
                $(this).parent('.slide-down').prev().find('input').val($txt)
                $(this).parent('.slide-down').slideUp(200)
                event.stopPropagation();
            })
            $('.choice button').click(function () {
                if ($(this).parents('li').hasClass('open')) {
                    $(this).parent().next().slideUp(200).parents('li').removeClass('open');
                } else {
                    $(this).parent().next().slideDown(200).parents('li').addClass('open');
                }
                event.stopPropagation();
            })
            $('body').click(function () {
                $('.slide-down').slideUp(200)
            })
        },
        calendar: function () {
            $('.dateBox').click(function (e) {
                if ($(this).hasClass('open')) {
                    $(this).removeClass('open');
                } else {
                    $(this).addClass('open');
                }
                e.stopPropagation();

            })
            $('.dateBox input').click(function () {
                $('.dateBox').trigger('click');
            })
            $('body').click(function () {
                $('.dateBox').removeClass('open');
            })

        },

        init: function () {
            $(window).resize(function () {
                yyCommon.autoHeight()
            })
            yyCommon.autoHeight();
            yyCommon.select();            
            yyCommon.calendar();
        }
    }

} ()
yyCommon.init();
 
    

