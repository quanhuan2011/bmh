<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BMH.EagleEye.page.agent.index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <meta name="description" content="鹰眼移动广告投放系统代理商后台" />
    <meta name="keyword" content="鹰眼移动广告投放系统代理商后台" />
    <title>代理商后台-主页</title>
    <link rel="stylesheet" href="/page/css/agent-common.css">
    <link rel="stylesheet" href="/page/css/agent-index-v2.css">
    <style>
       

        .rightArea, .leftSide {
            height: 100%;
        }
    </style>
</head>
<body>
    <!--[if lt IE 10]>
<style>
</style>
  <div class="browser-happy">
      <div class="content">
          您正在使用ie浏览器版本太老，本页面的显示效果可能不佳，建议您升级到ie10及以上。
          <a href="http://browsehappy.com/" target="_blank"> 立即更新</a>
      </div>
  </div>
<![endif]-->
    <!-- 公共头部 开始 -->
    <div id="YY-head">
        <div class="clearfix">
            <div class="logo fl">
            </div>
            <div class="personInfo fr">
                <div class="set-area">
                    <em>
                        <img src="/page/images/tips.png" height="7" width="20" alt=""></em>
                     <a href="/page/changepwd.aspx" target="_blank">修改密码</a>
                    <a href="/page/logout.aspx" class="exit">退出</a>                    
                </div>
                <p class="set fr">设置</p>
                <span class="line fr">|</span>
                <span class="name fr"><%=accountName %></span>
                <img src='<%=headImageUrl %>' height="51" width="51" alt="" class="head fr">
            </div>
        </div>
    </div>
    <!-- 公共头部 结束 -->
    <div class="wrap clearfix">
        <!-- 左侧导航开始 -->
        <div class="leftArea">
            <div class="leftSide fl">
                <ul>
                    <li class="on  subcontent"><a>后台管理</a>
                        <dl>
                            <dd class="cur">
                                <a href="/page/agent/index.aspx">首页</a></dd>
                            <dd>
                                <a href="/page/agent/deduct.aspx">消费</a></dd>
                            <dd>
                                <a href="/page/agent/link.aspx">到达链接</a></dd>
                            <dd>
                                <a href="/page/agent/rechargedetail.aspx">充值记录</a></dd>
                            <%if (accountType == "3")
                                { %>
                            <dd>
                                <a href="/page/agent/adinfo.aspx">广告列表</a></dd>
                            <%} %>
                        </dl>
                    </li>
                </ul>
            </div>
        </div>
        <!-- 左侧导航结束 -->
        <!-- 右侧区域开始 -->
        <div class="rightArea">
            <div class="main">
                <!-- 头部数据展示开始 -->
                <div class="user-main clearfix">
                    <div class="head">
                        <h3>数据报告</h3>
                    </div>
                    <div class="user-top">
                        <div class="cash-num clearfix">
                            <div class="inner">
                                <div class="box">
                                    <p class="title">当前余额</p>
                                    <span class="num">
                                        <%=balanceSum %></span><span class="unit">元</span>
                                </div>
                            </div>
                        </div>
                        <div class="cash-num clickInt clearfix" style ="display:none;" id="top_rebate">
                            <div class="inner">
                                <div class="box">
                                    <p class="title">返点</p>
                                    <div class="month">本月：<span class="num"><%=rebateSum %></span><span class="unit">元</span></div>
                                    <div class="month">上月：<span class="num"><%=lastRebateSum %></span><span class="unit">元</span></div>
                                </div>
                            </div>
                        </div>
                        <div class="cash-num expense clearfix">
                            <div class="inner">
                                <div class="box">
                                    <p class="title">消费</p>
                                    <div class="month">本月：<span class="num"><%=deductSum %></span><span class="unit">元</span></div>
                                    <div class="month">上月：<span class="num"><%=lastDeductSum %></span><span class="unit">元</span></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- 头部数据展示end -->
                <div class="survey" tabindex="0" id="agent_info">
                    <div class="data-box clearfix">
                        <div class="area">
                            <h3>今日流量</h3>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tbody>
                                    <tr>
                                        <th>概况</th>
                                        <th>点击量/下载量（次）</th>
                                        <th>消费（元）</th>
                                    </tr>
                                    <tr class="info_today">
                                        <td>今日</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr class="info_yesterday">
                                        <td>昨日</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr class="info_except">
                                        <td>预计今日</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <%--   <tr>
                                                <td>今日</td>
                                                <td class="day">1083</td>
                                                <td class="day">95</td>
                                            </tr>
                                            <tr>
                                                <td>昨日</td>
                                                <td>11327<i class="iconfont up">&#xe508;</i></td>
                                                <td>102</td>
                                            </tr>
                                            <tr>
                                                <td>预计今日</td>
                                                <td>15555<i class="iconfont down">&#xe64e;</i></td>
                                                <td>123<i class="iconfont up">&#xe508;</i></td>
                                            </tr>--%>
                                </tbody>
                            </table>
                        </div>

                        <div class="chart-box">
                            <h3>趋势图</h3>
                            <div class="toggleBtn">
                                <button class="clicks on">点击量/下载量</button>
                                <button class="consumption">消费</button>
                            </div>
                            <!-- 折线图 -->
                            <div id="profileChart" class="report">
                            </div>
                        </div>
                    </div>



                    <%--<table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tbody>                                 
                             <tr>
                                 <th>概况</th>
                                 <th>点击量（次）</th>
                                 <th>消费（元）</th>
                             </tr>
                             <tr class="info_today">
                                 <td>今日</td>
                                 <td></td>
                                 <td></td>
                             </tr>
                             <tr class="info_yesterday">
                                 <td>昨日</td>
                                 <td></td>
                                 <td></td>
                             </tr>
                             <tr class="info_except">
                                 <td>预计今日</td>
                                 <td></td>
                                 <td></td>
                             </tr>
                      </tbody>
                    </table>
                    <div class="toggleBtn">
                        <button class="clicks on">点击量</button><button class="consumption">消费</button>
                    </div>
                    <!-- 折线图 -->
                    <div id="profileChart"  class="report">
                        
                    </div>--%>
                    <!-- 折线图end -->
                    <!-- TOP 10 开始 -->
                    <div class="top-10-box info_top10">
                        <h3>TOP10入口页面</h3>
                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
                                <th>入口页面</th>
                                <th class="tr">点击量/下载量</th>
                                <th>占比</th>
                            </tr>
                            <asp:Repeater ID="repList1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><a><%#Eval("linkurl") %></a></td>
                                        <td class="tr"><%#Eval("clickcnt") %></td>
                                        <td><%#Eval("clickrto") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litNoInfo1" runat="server">
                            </asp:Literal>
                        </table>
                    </div>
                    <!-- TOP 10 结束 -->
                    <!-- 饼图 -->
                    <%-- <div class="dataStatistics">
                        <div class="title">
                            <h5>终端设备</h5><h5>操作系统</h5><h5>链接</h5>
                        </div>
                        <div id="terminalDevice"></div>
                        <div id="operatingSystem"></div>
                        <div id="link"></div>
                    </div>--%>
                    <!-- 饼图end -->
                </div>
            </div>
        </div>
        <!-- 右侧区域结束 -->
    </div>

    <script src="/page/js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="/page/js/echarts/echarts-1.2.js"></script>
    <script src="/page/js/jquery/jquery.nicescroll.js"></script>
    <script>
        var aduId = '<%=aduserid%>';
        var accType = '<%=accountType%>';
        if (accType != "" && accType == "3")
        {
            $("#top_rebate").css("display", "block");
        }
        //头部设置下拉
        function yyHeadSlide() {
            $('.personInfo .set').click(function (e) {
                $(this).prev().show()
                $('#YY-head .set').addClass('open')
                e.stopPropagation();
            })
            $('body,.personInfo .set-area a').click(function () {
                $('.personInfo .set-area').hide();
                $('#YY-head .set').removeClass('open')
            })
        }
        yyHeadSlide();
        $('.leftArea').height($('.rightArea').outerHeight(true))
        $('body').niceScroll({
            cursorcolor: "#999",//调用滚动
            cursoropacitymin: 1,
            cursoropacitymax: 1,
            touchbehavior: true,
            cursorwidth: "5px",
            cursorborder: "0",
            cursorborderradius: "5px"
        }).resize()
        // 折线图

        var profileChart = echarts.init(document.getElementById('profileChart'));
        var optionProfile = {//折线图数据
            title: {
                // text: '堆叠区域图'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['昨日', '今日']
            },
            toolbox: {
                feature: {
                    saveAsImage: true
                }
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23']
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: '昨日',
                    type: 'line',
                    data: [],
                    itemStyle: {
                        normal: {
                            lineStyle: { color: '#50a9fb' },
                            color: "#50a9fb" //图标颜色
                        }

                    }
                },
                {
                    name: '今日',
                    type: 'line',

                    data: [],
                    itemStyle: {
                        normal: {
                            lineStyle: { color: '#6fc61f', color: '#6fc61f' },
                            color: "#6fc61f" //图标颜色
                        }

                    }
                }
            ],
            // color:['#50a9fb','#6fc61f']
        };
        profileChart.setOption(optionProfile);


        $("#agent_info .info_top10 table td a").click(function () {
            var $linkurl = $(this).text();
            window.open("/page/agent/linkdetail.aspx?lurl=" + encodeURIComponent($linkurl));
        })


        var chartData;

        $('.toggleBtn button').click(function () {
            var index = $(this).index();
            $(this).addClass('on').siblings().removeClass('on');

            if (index == 0) {
                optionProfile.series[1].data = chartData.today.click;//今日数据
                optionProfile.series[0].data = chartData.yesterday.click;//昨日数据
                profileChart.setOption(optionProfile);
            } else {
                optionProfile.series[1].data = chartData.today.income;//今日数据
                optionProfile.series[0].data = chartData.yesterday.income;//昨日数据
                profileChart.setOption(optionProfile);

            }
        })
        GetDetailByDayData(aduId);
        //获取昨日与今日数据
        function GetDetailByDayData(aduId) {
            var ajaxData = { method: "GetDetailData", aduserid: aduId };
            $.ajax({
                type: "post",
                url: "/api/agenthandler.ashx",
                data: ajaxData,
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.errcode != "1") {
                        alert(result.errmsg);
                        return;
                    }
                    var data = result.data;
                    chartData = data;
                    //今日
                    if (data.tclick != "") {
                        $("#agent_info .info_today td").eq(1).text(data.tclick);
                    }
                    else {
                        $("#agent_info .info_today td").eq(1).text("--");
                    }
                    if (data.tincome != "") {
                        $("#agent_info .info_today td").eq(2).text(data.tincome);
                    }
                    else {
                        $("#agent_info .info_today td").eq(2).text("--");
                    }
                    //昨日
                    if (data.yclick != "") {
                        $("#agent_info .info_yesterday td").eq(1).text(data.yclick);
                    }
                    else {
                        $("#agent_info .info_yesterday td").eq(1).text("--");
                    }
                    if (data.yincome != "") {
                        $("#agent_info .info_yesterday td").eq(2).text(data.yincome);
                    }
                    else {
                        $("#agent_info .info_yesterday td").eq(2).text("--");
                    }
                    //预计
                    if (data.eclick != "" && data.eclick > 0) {
                        if (data.yclick != "") {
                            if (data.eclick > data.yclick)
                                $("#agent_info .info_except td").eq(1).html(data.eclick + "<i class='iconfont up'>&#xe508;</i>");
                            else
                                $("#agent_info .info_except td").eq(1).html(data.eclick + "<i class='iconfont down'>&#xe64e;</i>");
                        } else {
                            $("#agent_info .info_except td").eq(1).text(data.eclick);
                        }
                    }
                    else {
                        $("#agent_info .info_except td").eq(1).text("--");
                    }
                    if (data.eincome != "" && data.eincome > 0) {
                        if (data.yincome != "") {
                            if (data.eincome > data.yincome)
                                $("#agent_info .info_except td").eq(2).html(data.eincome + "<i class='iconfont up'>&#xe508;</i>");
                            else
                                $("#agent_info .info_except td").eq(2).html(data.eincome + "<i class='iconfont down'>&#xe64e;</i>");
                        } else {
                            $("#agent_info .info_except td").eq(2).text(data.eincome);
                        }
                    }
                    else {
                        $("#agent_info .info_except td").eq(2).text("--");
                    }


                    optionProfile.series[1].data = data.today.click;//今日数据
                    optionProfile.series[0].data = data.yesterday.click;//昨日数据
                    profileChart.setOption(optionProfile);
                }
            })

        }


        //// 饼图
        //var deviceChart = echarts.init(document.getElementById('terminalDevice'));//终端设备
        //var systemChart = echarts.init(document.getElementById('operatingSystem'));//操作系统
        ////柱状图
        //var linkChart = echarts.init(document.getElementById('link'));//链接

        //    //颜色
        //var colorArray = ["#1695a3", "#ff6c6c", "#acf0f2", "#eb7f00", "#7d8a2e", "#225378"];

        //    //饼图图例
        //var optionPie = {
        //    tooltip: {
        //        trigger: 'item',
        //        formatter: "{a} <br/>{b}: {c} ({d}%)"
        //    },
        //    legend: {
        //        //orient: 'center',
        //        x: 'center',
        //        y: 'bottom',
        //        data: []
        //    },
        //    series: [
        //        {
        //            name: '',
        //            type: 'pie',
        //            radius: ['50%', '70%'],
        //            avoidLabelOverlap: false,
        //            label: {
        //                normal: {
        //                    show: false,
        //                    position: 'center'
        //                },
        //                emphasis: {
        //                    show: true,
        //                    textStyle: {
        //                        fontSize: '30',
        //                        fontWeight: 'bold'
        //                    }
        //                }
        //            },
        //            labelLine: {
        //                normal: {
        //                    show: false
        //                }
        //            },
        //            data: [

        //            ],
        //            color: colorArray
        //        }
        //    ]
        //};

        //optionLink = {
        //    color: ['#3398DB'],
        //    tooltip: {
        //        trigger: 'axis',
        //        axisPointer: {            // 坐标轴指示器，坐标轴触发有效
        //            type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
        //        }
        //    },
        //    grid: {
        //        x: 40,
        //        x2: 100,
        //        y2: 50,// y2可以控制 X轴跟Zoom控件之间的间隔，避免以为倾斜后造成 label重叠到zoom上
        //    },
        //    xAxis: [
        //        {
        //            type: 'category',
        //            data: [],
        //            axisTick: {
        //                alignWithLabel: true
        //            },
        //            axisLabel: {
        //                // formatter:function(val){
        //                //     return val.split("").join("\n");
        //                // }
        //                interval: 0,
        //                rotate: 45,
        //                margin: 2,
        //                textStyle: {
        //                    color: "#222",
        //                    fontSize: '12px',
        //                    textAlign: 'right'
        //                }
        //            }
        //        }
        //    ],
        //    yAxis: [
        //        {
        //            type: 'value'
        //        }
        //    ],
        //    series: [
        //        {
        //            name: '点击量',
        //            type: 'bar',
        //            barWidth: '60%',
        //            data: [],
        //            itemStyle: {
        //                normal: {
        //                    color: function (params) {
        //                        // build a color map as your need.
        //                        var colorList = [
        //                          '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
        //                           '#FE8463', '#9BCA63', , '#FAD860', '#F3A43B', '#60C0DD',
        //                           '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
        //                        ];
        //                        return colorList[params.dataIndex]
        //                    }
        //                }
        //            }
        //        }
        //    ]
        //};
        ////deviceChart.setOption(optionDevice);
        ////systemChart.setOption(optionSystem);
        ////linkChart.setOption(optionLink); //  链接暂时放着

        //GetDetailForPieData(aduId);
        //function GetDetailForPieData(aduId) {
        //    var ajaxData = { method: "GetDetailForPieData", aduserid: aduId };
        //    $.ajax({
        //        type: "post",
        //        url: "/api/agenthandler.ashx",
        //        data: ajaxData,
        //        dataType: "json",
        //        async: false,
        //        success: function (result) {
        //            if (result.errcode != "1") {
        //                alert(result.errmsg);
        //                return;
        //            }
        //            var data = result.data;
        //            //饼图配置
        //            if (data.osdata != null && data.osdata.length>0) {
        //                var osNameArray = new Array();
        //                $.each(data.osdata, function (index, item) {
        //                    osNameArray.push(item.name);
        //                })
        //                optionPie.legend.data = osNameArray;
        //                optionPie.series[0].name = "操作系统";
        //                optionPie.series[0].data = data.osdata;
        //                systemChart.setOption(optionPie);
        //            }
        //            else {
        //                $("#operatingSystem").html("<p style='width:100%; line-height:200px; text-align:center'>无数据...</p>");
        //            }
        //            if (data.termdata != null && data.termdata.length>0) {
        //                var nameArray = new Array();
        //                $.each(data.termdata, function (index, item) {
        //                    nameArray.push(item.name);
        //                })
        //                optionPie.legend.data = nameArray;
        //                optionPie.series[0].name = "终端设备";
        //                optionPie.series[0].data = data.termdata;
        //                deviceChart.setOption(optionPie);
        //            } else {
        //                $("#terminalDevice").html("<p style='width:100%; line-height:200px; text-align:center'>无数据...</p>");
        //            }
        //            if (data.linkurldata != null && data.linkurldata.length>0) {
        //                var nameArray = new Array();
        //                var tempArray = new Array();
        //                $.each(data.linkurldata, function (index, item) {
        //                    nameArray.push(item.name);
        //                    tempArray.push(item.value);
        //                })
        //                optionLink.xAxis[0].data = nameArray;             
        //                optionLink.series[0].data = tempArray;
        //                linkChart.setOption(optionLink);
        //            } else {
        //                $("#link").html("<p style='width:100%; line-height:200px; text-align:center'>无数据...</p>");
        //            }
        //        }
        //    })
        //}
    </script>
</body>
</html>
