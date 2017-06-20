<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deduct.aspx.cs" Inherits="BMH.EagleEye.page.agent.deduct" %>

<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="renderer" content="webkit" />
<meta name="description" content="鹰眼移动广告投放系统代理商后台" />
<meta name="keyword" content="鹰眼移动广告投放系统代理商后台" />
<title>代理商后台-消费</title>
<link rel="stylesheet" href="/page/css/agent-common.css">
<link rel="stylesheet" href="/page/css/agent-index-v2.css">
<style>
body{
    height:100%;
    overflow: hidden;
}
.rightArea, .leftArea {
    height:100%;
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
                    <em><img src="/page/images/tips.png" height="7" width="20" alt=""></em>                      
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
                        <dd>
                            <a href="/page/agent/index.aspx">首页</a></dd>
                        <dd class="cur">
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
        <div class="main custom">
            <!-- 头部数据展示开始 -->
             <div class="head">
                <h3>总消费</h3>
            </div>
            <div class="consumeReport">
                <div class="top clearfix">
                    <span class="fl time">时间：</span>
                    <ul class="fl">
                        <li class=" day">今天</li><li class="yesterday">昨天</li><li class="week">最近7天</li><li class="month">最近30天</li>
                    </ul>
                    <div class="dateBox fl">
                        <input id="date-range0" size="30" value="今天 2017年2月24日">
                    </div>
                </div>
                <div id="consumeReport"></div>
            </div>
            
            <!-- 头部数据展示end -->
            <div class="tableHead">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                        <tr>
                            <th>时间</th>
                            <th>点击/下载</th>
                            <th>消费</th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div class="tableBody" tabindex="0" style="overflow-y: hidden; outline: none;">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tbody>
                         <asp:Repeater ID="repList1" runat="server">
                                <ItemTemplate>
                        <tr>
                            <td><%#Eval("orderid") %></td>
                            <td><%#Eval("clickcnt") %></td>
                            <td><%#Eval("incomesum") %></td>
                        </tr>
                        </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litNoInfo1" runat="server">
                            </asp:Literal>
                    </tbody>
                </table>
            </div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                <tbody><tr>
                    <td style="height: 20px; margin-top: 15px;" align="center">
                        
                         <div class="manu" style="display: inline-block">
                                    <asp:Literal ID="litPage1" runat="server">
                                    </asp:Literal>
                                </div>
                                <div class="pageInfoManu" style="display: inline-block">
                                    <asp:Literal ID="litPageInfo1" runat="server">
                                    </asp:Literal>
                                </div>
                    </td>
                </tr>

              </tbody>
            </table>
        </div>
    </div>
    <!-- 右侧区域结束 -->
</div>



    <script src="/page/js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="/page/js/echarts/echarts-1.2.js"></script>
    <script src="/page/js/jquery/jquery.nicescroll.js"></script>
    <script src="/page/js/moment/moment.min.js" type="text/javascript"></script>
    <script src="/page/js/manager/v2.0/jquery.daterangepicker.agent.js"></script>
    <script src="/page/js/base/v2.0/demo.js" type="text/javascript"></script>
    <script src="/page/js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script>
     var aduId = '<%=aduserid%>';


//头部设置下拉
function yyHeadSlide() {
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

// 自适应高度
function autoHeight() {
    var winh = $(window).height() - 72;
    $('.wrap').height(winh);
    $('.tableBody').height(winh - 320-49-28-10)
}
autoHeight()

$('.tableBody').niceScroll({ cursorcolor: "#12bdce",//调用滚动
    cursoropacitymin: 1,
    cursoropacitymax: 1,
    touchbehavior: true,
    cursorwidth: "5px",
    cursorborder: "0",
    cursorborderradius: "5px"
}).resize()

// 折线图

    
    var consumeReportChart = echarts.init(document.getElementById('consumeReport'));
    var optionConsumeReport = {//折线图数据
        title: {
            // text: '堆叠区域图'
        },
        tooltip : {
            trigger: 'axis'
        },
        legend: {
            // data:['点击量（次）']
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
        xAxis : [
            {
                type : 'category',
                boundaryGap : false,
                data : []
            }
        ],
        yAxis : [
            {
                type : 'value'
            }
        ],
        series : [
            {
                name:'消费(元)',
                type:'line',                
                areaStyle: {},
                data:[],

                itemStyle:{
                    normal:{
                        lineStyle:{color:'#50a9fb'},
                        color: "#50a9fb" //图标颜色
                    }
                    
                }
            }
        ],
       // color:['#50a9fb','#6fc61f']
    };
    //consumeReportChart.setOption(optionConsumeReport);
   
    $('.consumeReport li').click(function(){
        var index = $(this).index();
        $(this).addClass('on').siblings().removeClass('on');
        var startTime, endTime;
        var url = location.href;
        if (index == 0) {
            startTime = GetDateStr(0);
            endTime = GetDateStr(0);
        } else  if (index == 1) {
            startTime = GetDateStr(-1);
            endTime = GetDateStr(-1);
        }
        else if (index == 2) {
            startTime = GetDateStr(-7);
            endTime = GetDateStr(0);
        }
        else {
            startTime = GetDateStr(-30);
            endTime = GetDateStr(0);
        }
              
        url = getMatchUrl(url, "stime", $.trim(startTime.replace("-", "").replace("-", "")));
        url = getMatchUrl(url, "etime", $.trim(endTime.replace("-", "").replace("-", "")));

        location.href = url;        
    })

    var startTime = getUrlParam("stime");
    var endTime = getUrlParam("etime");    
    var timeRange;
    if (startTime != null && endTime != null && startTime.length == 8) {
        timeRange = startTime.substr(0, 4) + "-" + startTime.substr(4, 2) + "-" + startTime.substr(6, 2) + " 至 " + endTime.substr(0, 4) + "-" + endTime.substr(4, 2) + "-" + endTime.substr(6, 2);
        $("#date-range0").val(timeRange);
        if (startTime == endTime && startTime == $.trim(GetDateStr(0).replace("-", "").replace("-", "")))
        { $('.consumeReport li').eq(0).addClass('on').siblings().removeClass('on'); }
        else if (startTime == endTime && startTime == $.trim(GetDateStr(-1).replace("-", "").replace("-", "")))
        { $('.consumeReport li').eq(1).addClass('on').siblings().removeClass('on'); }
        else if (startTime == $.trim(GetDateStr(-7).replace("-", "").replace("-", "")) && endTime == $.trim(GetDateStr(0).replace("-", "").replace("-", "")))
        { $('.consumeReport li').eq(2).addClass('on').siblings().removeClass('on'); }
        else if (startTime == $.trim(GetDateStr(-30).replace("-", "").replace("-", "")) && endTime == $.trim(GetDateStr(0).replace("-", "").replace("-", "")))
        { $('.consumeReport li').eq(3).addClass('on').siblings().removeClass('on'); }
        else {
            $('.consumeReport li').removeClass('on');
        }
    }
    else {
        $('.consumeReport li').eq(0).addClass('on').siblings().removeClass('on');
        startTime = GetDateStr(0);
        endTime = GetDateStr(0);
        $("#date-range0").val(startTime + ' 至 ' + endTime);
    }
    GetDeductDetail(aduId, $.trim(startTime.replace("-", "").replace("-", "")), $.trim(endTime.replace("-", "").replace("-", "")));
    function GetDeductDetail(aduId, sTime, eTime) {
        var ajaxData = { method: "GetDeductDetail", aduserid: aduId, stime: sTime, etime: eTime };
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
                if (data.regiondata != null) {
                    //var nameArray = new Array();
                    //var dataArray = new Array();
                    //$.each(data, function (index, item) {
                    //    nameArray.push(item.orderid);
                    //    dataArray.push(item.incomesum);
                    //})
                    if (data.regiondata.length > 0) {
                        optionConsumeReport.xAxis[0].data = data.regiondata;
                        optionConsumeReport.series[0].data = data.incomedata;
                        consumeReportChart.setOption(optionConsumeReport);

                    }
                    else {
                        $("#consumeReport").html("<p style='width:100%; line-height:200px; text-align:center'>无数据...</p>");
                    }
                }
                else {
                    $("#consumeReport").html("<p style='width:100%; line-height:200px; text-align:center'>无数据...</p>");
                }
            }
        })
    }
</script>
</body>
</html>