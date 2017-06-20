<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aduadd.aspx.cs" Inherits="BMH.EagleEye.page.agent.aduadd" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <meta name="description" content="鹰眼移动广告投放系统代理商后台" />
    <meta name="keyword" content="鹰眼移动广告投放系统代理商后台" />
    <title>管理广告</title>
    <link rel="stylesheet" href="/page/css/agent-common.css">
    <link rel="stylesheet" href="/page/css/agent-index-v2.css">
    <style>
        body {
            height: 100%;
            overflow: hidden;
        }

        .rightArea, .leftArea {
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
                            <li class=" day">今天</li>
                            <li class="yesterday">昨天</li>
                            <li class="week">最近7天</li>
                            <li class="month">最近30天</li>
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
                    <tbody>
                        <tr>
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
<%--   var aduId = '<%=aduserid%>';--%>


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

        // 自适应高度
        function autoHeight() {
            var winh = $(window).height() - 72;
            $('.wrap').height(winh);
            $('.tableBody').height(winh - 320 - 49 - 28 - 10)
        }
        autoHeight()

        $('.tableBody').niceScroll({
            cursorcolor: "#12bdce",//调用滚动
            cursoropacitymin: 1,
            cursoropacitymax: 1,
            touchbehavior: true,
            cursorwidth: "5px",
            cursorborder: "0",
            cursorborderradius: "5px"
        }).resize()


    </script>
</body>
</html>
