﻿var bmhAdData = function () {
    var changeItem; var itemsName = [], colors = [];
    return {
        changeClassify: function () {

            //按分类切换图表样式
            $('.user-main .tip').find('li').click(function () {
                $(this).addClass('on').siblings().removeClass('on');
                _dimensionType = $(this).attr("name");
                bmhAdData.wlData(_dimensionType, _startTime, _endTime);
            })
        },
        calendar: function () {
            //头部日历
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

            //设置日历默认时间为昨天
            function GetDateStr(AddDayCount) {
                var dd = new Date();
                dd.setDate(dd.getDate() + AddDayCount); //获取AddDayCount天后的日期
                var y = dd.getFullYear();
                var m = dd.getMonth() + 1; //获取当前月份的日期
                var d = dd.getDate();
                var toDouble = function (num) {
                    return num = num < 10 ? '0' + num : '' + num
                }
                return toDouble(y) + "-" + toDouble(m) + "-" + toDouble(d);
            }
            var startdate = GetDateStr(-7);
            var enddate = GetDateStr(-0);
            _startTime = startdate.replace(/-/g, "");
            _endTime = enddate.replace(/-/g, "");
            $('.dateBox input').val(startdate + ' 至 ' + enddate);
            //           
            $(".overall .head").find("em").text("（截止至" + GetDateStr(-1) + "）");
        },
        choice: function () {
            //模拟select下拉效果
            var slideW = $('.user-main').find('.choice').outerWidth();
            $('.slide').width(slideW);
            $('.user-main').on('click', '.box', function (e) {
                var parent = $(this).closest('.user-main');
                if (!parent.hasClass('is-open')) {
                    $('.slide').slideDown(300);
                    parent.addClass('is-open')
                    $('.triangle').addClass('open');
                } else {
                    $('.slide').slideUp(300);
                    parent.removeClass('is-open');
                    $('.triangle').removeClass('open');
                }
                e.stopPropagation();
            })
            $('.user-main').on('click', '.slide', function (e) {
                e.stopPropagation();
            })
            //点击body隐藏
            $('html').on('click', function (e) {
                $('.slide').slideUp(300);
                $('.user-main').removeClass('is-open');
                $('.triangle').removeClass('open');

            });

            //select框内容动态生成
            $("input[name='apk[]']:checked").parents('dd').addClass('cur');
            $('.checkbox').on('click', function () {

                var _html = ""; var _txt, classN;
                if ($("input[name='apk[]']:checked").length < 2) {//数量少于2个时可点击多选框

                    if ($(this).siblings("input[type='checkbox']").attr('checked')) {//如果已选，切换为未选

                        $(this).removeClass('cur').parents('dd').removeClass('cur');
                        $(this).siblings("input[type='checkbox']").removeAttr('checked');
                        $("input[name='apk[]']:checked").each(function () {
                            _txt = $(this).prev().val();
                            classN = $(this).parents('.checkbox-con').parent().attr('class');
                            _html += "<dd class='" + classN + "'>" + _txt + "</dd>";

                        })
                        $("input[name='apk[]']").next().css('cursor', 'pointer').parents('dd').css('cursor', 'default');
                    }
                    else {//如果未选 则切换为已选
                        $(this).addClass('cur');
                        //$(this).siblings("input[type='checkbox']").attr('checked','checked');
                        var $val = $(this).prev().val();
                        $(this).siblings("input[type='checkbox']").remove();
                        $(this).before('<input type="checkbox" class="ipt-hide" name="apk[]" value="' + $val + '" checked="checked">');
                        $(this).parents('dd').addClass('cur');
                        $("input[name='apk[]']:checked").each(function () {
                            _txt = $(this).parents('.checkbox-con').prev().text();
                            classN = $(this).parents('.checkbox-con').parent().attr('class');
                            _html += "<dd class='" + classN + "'>" + _txt + "</dd>";
                        })

                        $("input[name='apk[]']:not(:checked)").next().css('cursor', 'not-allowed').parents('dd').css('cursor', 'not-allowed')
                        if ($("input[name='apk[]']:checked").length < 2) {
                            $("input[name='apk[]']:not(:checked)").next().css('cursor', 'pointer').parents('dd').css('cursor', 'default')
                        }
                    }
                } else {

                    //数量大于2个时，如果已选，点击切换为未选
                    if ($(this).siblings("input[type='checkbox']").attr('checked')) {
                        $(this).removeClass('cur').parents('dd').removeClass('cur'); ;
                        $(this).siblings("input[type='checkbox']").removeAttr('checked');

                        //如果点击的选中的，那么其他所有的都可点击
                        $("input[name='apk[]']").next().css('cursor', 'pointer').parents('dd').css('cursor', 'default')

                    } else {
                        //如果点击的是未选的，那么所有的未选择的都不可点击
                        $("input[name='apk[]']:not(:checked)").next().css('cursor', 'not-allowed').parents('dd').css('cursor', 'default');
                    }
                    var a = $("input[name='apk[]']:checked").length;
                    $("input[name='apk[]']:checked").each(function () {//拼接字符串
                        _txt = $(this).parents('.checkbox-con').prev().text();
                        classN = $(this).parents('.checkbox-con').parent().attr('class');
                        _html += "<dd class='" + classN + "'>" + _txt + "</dd>";

                    })

                }

                $('.choice .box').find('dl').html(_html) //加载 

            });

            //默认已选择2个的情况下其他未选择的不可点击
            $("input[name='apk[]']:not(:checked)").next().css('cursor', 'not-allowed').parents('dd').css('cursor', 'not-allowed')
            $('.checkbox-con').click(function () {
                event.stopPropagation(); //防止冒泡
            })

        },
        //总量数据
        sumData: function () {
            var data1;
            var ajaxData1 = { "aduserid": adUserId };
            $.ajax({
                type: "post",
                url: "../../api/Report.asmx/GetAdSumOfAdUser",
                data: ajaxData1,
                dataType: "json",
                async: true,
                success: function (result) {
                    data1 = result;
                    //总量数据
                    var $dataTop = $(".user-top").find(".cash-num");
                    $dataTop.eq(0).find('.num').text(data1.data.balancesum); //展现量
                    $dataTop.eq(1).find('.num').text(data1.data.clicksum); //点击量
                    $dataTop.eq(2).find('.num').text(data1.data.deductsum.toFixed(2)); //昨日收入保留两位小数 
                }
            });

        },

        //填充数据
        wlData: function (dimensionType, startTime, endTime) {
            //数据源
            var data1;
            var data2;
            var dimensionName;

            var ajaxData1 = { "aduserid": adUserId, "starttime": startTime, "endtime": endTime };
            $.ajax({
                type: "post",
                url: "../../api/Report.asmx/GetAdSumOfAdUserByTime",
                data: ajaxData1,
                dataType: "json",
                async: true,
                success: function (result) {
                    data1 = result;
                    //总量数据
                    var $dataTop = $(".user-main").find(".overall .num li");
                    $dataTop.eq(0).find('em').text(data1.data.clicksum);
                    $dataTop.eq(1).find('em').text(data1.data.deductsum);
                }
            });

            var ajaxData2 = { "aduserid": adUserId, "starttime": startTime, "endtime": endTime, "dimensiontype": dimensionType };
            $.ajax({
                type: "post",
                url: "../../api/Report.asmx/GetAdListOfAdUser",
                data: ajaxData2,
                dataType: "json",
                async: false,
                success: function (result) {
                    data2 = result;
                    // alert(JSON.stringify(data2));
                }
            });

            //return;
            //填充表格数据
            var tableHtml = []//,datas =data2.data[i] ;//拼接字符串
            var dateBox = []; //日期
            var dataNum = [], fillInDataNum;
            var theadHtml = [];
            //            $('.choice .slide').find('.title').each(function () {
            //                theadHtml.push($(this).text())
            //            })
            //            //获取维度名称
            //            switch (dimensionType) {
            //                case 'day': dimensionName = "日期";
            //                    break;
            //                case 'area': dimensionName = "区域";
            //                    break;
            //                case 'hour': dimensionName = "小时";
            //                    break;
            //                case 'class': dimensionName = "分类";
            //                    break;
            //                default: dimensionName = "维度";
            //                    break;
            //            }

            //            tableHtml.push('<thead><tr><td>' + dimensionName + '</td>');
            //            for (var i = 0, _length = theadHtml.length; i < _length; i++) {
            //                //拼装表格数据

            //                tableHtml.push('<td>' + theadHtml[i] + '</td>')

            //            }
            //            tableHtml.push('</tr></thead><tbody>');

            if (dimensionType == "day") {
                for (var i = 0, _length = data2.data.length; i < _length; i++) {
                    dateBox.push(data2.data[i].date);
                }
            }
            if (dimensionType == "area") {
                for (var i = 0, _length = data2.data.length; i < _length; i++) {
                    dateBox.push(data2.data[i].area);
                }

            }
            if (dimensionType == "hour") {
                for (var i = 0, _length = data2.data.length; i < _length; i++) {
                    dateBox.push(data2.data[i].hour);
                }
            }
            if (dimensionType == "class") {
                for (var i = 0, _length = data2.data.length; i < _length; i++) {
                    dateBox.push(data2.data[i].classname);
                }
            }
            //
            //            tableHtml.push('</tbody>')
            //            $('.data-table').html(tableHtml.join('')); //加载进页面   

            //项目类型填充进echarts
            function changeItem() {
                itemsName = [];
                $('.choice').find(':checked').each(function () {
                    itemsName.push($(this).parents('.checkbox-con').prev().text());
                    colors.push($(this).parents('.checkbox-con').prev().data('color'))
                })
            }
            changeItem(); //项目类型初始化
            //点击切换项目类型，echarts同步变化
            $('.choice').find('.checkbox').click(function () {

                itemsName = []; itemsData = []; colors = []; //点击后清空数组
                $('.choice .box').find('dd').each(function () {
                    itemsName.push($(this).text())//循坏拼装项目name数组
                })
                $('.choice .slide').find(':checked').each(function () {
                    colors.push($(this).parents('.checkbox-con').prev().data('color'))//循环拼装颜色数组
                    //dataNum.push($(this))
                })
                group = [];
                fillInDataNum();
                console.log(group)
                buildInformation(); //构建项目初始内容
                queryDataTest()//点击后重新执行加载

            })

            //动态填充数字
            var numGroup = [], group = [];
            function fillInDataNum() {
                numGroup = []; //清空
                $('.choice .slide').find(':checked').each(function () {
                    //把右侧select已选的标题建组
                    numGroup.push($(this).parents('dd').find('.title').data('name'))
                })
                dataNumber = []; //清空
                for (var j = 0, _length = numGroup.length; j < _length; j++) {//遍历数据类型
                    var dataNumber = [];
                    if (numGroup[j] == 'djl') {
                        for (var i = 0, length = data2.data.length; i < length; i++) {
                            dataNumber[i] = (data2.data[i].clickcnt / data2.data[i].showcnt)//.toPercent()
                        }
                    } else {
                        for (var i = 0, length = data2.data.length; i < length; i++) {//遍历数据
                            dataNumber[i] = data2.data[i][numGroup[j]]; //把已经选择了的数据类型的数据从数据源中取出来，分别拼成组                  
                        }
                    }
                    group[j] = dataNumber   //将2个数组拼接为 [[ ],[ ]]格式，后面传给echarts提供的数据接口                     
                }
            }

            fillInDataNum()//初始化

            //动态构建echarts图标基本信息
            var itemsData = [];

            function buildInformation() {
                for (var i = 0; i < itemsName.length; i++) {
                    if (i == 0) {
                        itemsData[i] = {//循环拼装项目内容数组
                            name: itemsName[i], //name
                            type: 'line', //类型 线条                   
                            //areaStyle: { normal: {} }, //区域
                            itemStyle: {//样式
                                normal: {
                                    color: colors[i]//颜色
                                }
                            },
                            data: group[i]//数据
                        }
                    }
                    else {
                        itemsData[i] = {//循环拼装项目内容数组
                            name: itemsName[i], //name
                            type: 'line', //类型 线条
                            yAxisIndex: 1,
                            //areaStyle: { normal: {} }, //区域
                            itemStyle: {//样式
                                normal: {
                                    color: colors[i]//颜色
                                }
                            },
                            data: group[i]//数据
                        }
                    }
                }
            }
            buildInformation(); //初始化项目内容

            var myChart, option;
            function queryDataTest() {
                //获取图表位置
                myChart = echarts.init(document.getElementById("myEchart"));
                option = {
                    title: {
                        text: ''
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data: itemsName
                    },
                    toolbox: {
                        feature: {
                            saveAsImage: {}
                        }
                    },
                    grid: {
                        left: '1%',
                        right: '2%',
                        bottom: '1%',
                        containLabel: true
                    },
                    xAxis: [
                            {
                                type: 'category',
                                boundaryGap: false,
                                data: dateBox
                            }
                        ],
                    yAxis: [
                            {
                                type: 'value',
                                position: 'left'
                            },
                            {
                                type: 'value'
                            }
                        ],
                    series: itemsData

                };
                myChart.setOption(option); //将图表内容格式内容放入到myChart位置               
                //myChart.hideLoading();                
            }

            queryDataTest()//初始化
        },
        init: function () {
            bmhAdData.changeClassify();
            bmhAdData.choice();
            bmhAdData.calendar();
            bmhAdData.sumData();
            bmhAdData.wlData(_dimensionType, _startTime, _endTime);
        }

    }// return end

} ()
bmhAdData.init();


$(".btn_yesterday").click(function () {

    var startdate = GetDateStr(-1);
    var enddate = GetDateStr(-1);
    _startTime = startdate.replace(/-/g, "");
    _endTime = enddate.replace(/-/g, "");
    $(".btn_last7day").removeClass("on");
    $(".btn_yesterday").addClass("on");
    bmhAdData.wlData(_dimensionType, _startTime, _endTime);

})

$(".btn_last7day").click(function () {
    var startdate = GetDateStr(-7);
    var enddate = GetDateStr(0);
    _startTime = startdate.replace(/-/g, "");
    _endTime = enddate.replace(/-/g, "");
    $(".btn_last7day").addClass("on");
    $(".btn_yesterday").removeClass("on");
    bmhAdData.wlData(_dimensionType, _startTime, _endTime);

})
function GetDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount); //获取AddDayCount天后的日期
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1; //获取当前月份的日期
    var d = dd.getDate();
    var toDouble = function (num) {
        return num = num < 10 ? '0' + num : '' + num
    }
    return toDouble(y) + "-" + toDouble(m) + "-" + toDouble(d);
}

//数字增加效果
$(function () {
    // function formatter(a,b){return a.toFixed(b.decimals)}function count(a){var b=$(this);a=$.extend({},a||{},b.data("countToOptions")||{}),b.countTo(a)}$.fn.countTo=function(a){return a=a||{},$(this).each(function(){function j(){h+=d,g++,k(h),"function"==typeof b.onUpdate&&b.onUpdate.call(e,h),g>=c&&(f.removeData("countTo"),clearInterval(i.interval),h=b.to,"function"==typeof b.onComplete&&b.onComplete.call(e,h))}function k(a){var c=b.formatter.call(e,a,b);f.html(c)}var b=$.extend({},$.fn.countTo.defaults,{from:$(this).data("from"),to:$(this).data("to"),speed:$(this).data("speed"),refreshInterval:$(this).data("refresh-interval"),decimals:$(this).data("decimals")},a),c=b.speed/b.refreshInterval,d=(b.to-b.from)/c,e=this,f=$(this),g=0,h=b.from,i=f.data("countTo")||{};f.data("countTo",i),i.interval&&clearInterval(i.interval),i.interval=setInterval(j,b.refreshInterval),k(h)})},$.fn.countTo.defaults={from:0,to:0,speed:1e3,refreshInterval:100,decimals:0,formatter:formatter,onUpdate:null,onComplete:null},$(".count-title").data("countToOptions",{formatter:function(a,b){return a.toFixed(b.decimals).replace(/\B(?=(?:\d{3})+(?!\d))/g,",")}}),$(".timer").each(count);
})

 
    

