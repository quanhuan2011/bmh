(function () {
    //单选全选
    $('.datalist .head1').find('.checkbox').click(function () {
        if ($(this).hasClass('cur')) {
            $(this).removeClass('cur');
            $('.datalist').find('.checkbox').removeClass('cur')
        } else {
            $(this).addClass('cur');
            $('.datalist').find('.checkbox').addClass('cur')
        }

    })
    $('.tableBody').find('.checkbox').click(function (event) {
        if ($(this).hasClass('cur')) {
            $(this).removeClass('cur');
        } else {
            $(this).addClass('cur');
        }
        var _length1 = $('.tableBody').find('.checkbox').length;
        var _length2 = $('.tableBody').find('.cur').length;
        if (_length1 == _length2) {
            $('.datalist .head1').find('.checkbox').addClass('cur')
        } else {
            $('.datalist .head1').find('.checkbox').removeClass('cur')
        }
        event.stopPropagation()
    })
    $(".new").click(function () {
        yyCommon.popoupShow();
    });
    $(".loginmask,.confirm,.cancel").click(function () {
        yyCommon.popoupHide();
    });
    $('.editBox a').eq(0).click(function () {
        yyCommon.popoupShow();
    })
    $(".popup .tab-bd,.tableBody").niceScroll({
        cursorcolor: "#12bdce",
        cursoropacitymin: 1,
        cursoropacitymax: 1,
        // touchbehavior:true,  
        cursorwidth: "5px",
        cursorborder: "0",
        cursorborderradius: "5px",
        horizrailenabled: false
    });
    $(".stop").click(function () {
        var _length = $('.tableBody').find('.cur').length;
        if (_length < 1) {
            alert("请选择广告");
            return;
        }
        var adData = new Array();
        $('.tableBody').find('.cur').each(function () {
            if ($(this).closest("tr").find(".adstatus").text() != "1") {
                alert("请选择正确的广告");
                return;
            }
            var dataItem = $(this).parent().parent().find(".adid").text();
            adData.push(dataItem);
        })
        UpdateAdStatus("stop", adData);
    })
    $(".start").click(function () {

        var _length = $('.tableBody').find('.cur').length;
        if (_length < 1) {
            alert("请选择广告");
            return;
        }
        var adData = new Array();
        $('.tableBody').find('.cur').each(function () {
            if ($(this).closest("tr").find(".adstatus").text() != "0") {
                alert("请选择正确的广告");
                return;
            }
            var dataItem = $(this).parent().parent().find(".adid").text();
            adData.push(dataItem);
        })
        UpdateAdStatus("start", adData);
    })
    //更新状态
    function UpdateAdStatus(inType, inData) {
        var ajaxData = { method: "UpdateAdStatus", updatetype: inType, data: JSON.stringify(inData) };

        $.ajax({
            url: "../../api/ManagerHandler.ashx",
            type: "post",
            data: ajaxData,
            dataType: "json",
            success: function (result) {
                if (result.errcode == "-1")
                    alert(result.errmsg);
                else {
                    //alert(result.errmsg);
                    location.href = location.href;
                }
            }
        });
    }
    //下拉选中
    $(".slide dd").click(function () {
        $(this).closest(".slide").find("dd").removeClass("cur");
        $(this).addClass("cur");
    })


})()
 
    

