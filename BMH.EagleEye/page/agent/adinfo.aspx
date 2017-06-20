<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adinfo.aspx.cs" Inherits="BMH.EagleEye.page.agent.adinfo" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="renderer" content="webkit" />
    <meta name="description" content="鹰眼移动广告投放系统代理商后台" />
    <meta name="keyword" content="鹰眼移动广告投放系统代理商后台" />
    <title>代理商后台-广告投放</title>
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
        .head button {
            height: 24px;
            line-height: 24px;
            color: rgb(255, 255, 255);
            margin-top: 17px;
            font-family: 微软雅黑;
            border-radius: 6px;
            background: rgb(18, 189, 206);
            padding: 0px 8px;
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
                            <dd>
                                <a href="/page/agent/deduct.aspx">消费</a></dd>
                            <dd >
                                <a href="/page/agent/link.aspx">到达链接</a></dd>
                            <dd>
                                <a href="/page/agent/rechargedetail.aspx">充值记录</a></dd>
                            <dd class="cur">
                                <a href="/page/agent/adinfo.aspx">广告列表</a></dd>
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
                <div class="head">
                      <button class="new fl">
                        新建广告投放
                    </button>
                </div>                   
                <div class="tableHead">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <thead>
                            <tr>                                
                                <th width="14%">
                                    <em>名称</em>
                                </th>
                                <th width="8%">状态
                                </th>                               
                                <th width="12%">物料
                                </th>
                                <th width="7%">计费
                                </th>
                                <th width="15%">投放时间
                                </th>
                               
                                <th width="8%">操作
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="tableBody">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tbody>
                            <asp:Repeater ID="repMaterialList" runat="server">
                                <ItemTemplate>
                                    <tr>                                       
                                        <td width="14%">
                                            <em>
                                                <%#Eval( "adname") %><span class="adid" style="display: none"><%#Eval( "adid") %></span></em>
                                            <p class="advertisingPageId">
                                                ID:
                                            <%#Eval( "adid") %>
                                            </p>
                                        </td>
                                        <td width="8%" class="state on">
                                            <span class="adstatus" style="display: none">
                                                <%#Eval( "status") %></span><span><%#Eval("statusname")%></span>
                                        </td>                                       
                                        <td width="12%">
                                            <%#Eval( "materialname") %>
                                        </td>
                                        <td width="7%">
                                            <%#Eval( "price") %>
                                        </td>
                                        <td width="15%" class="time">
                                            <%#Eval( "putrangetime") %>
                                        </td>                                       
                                        <td width="8%" class="editBox">
                                            <a href="/page/agent/adedit.aspx?etype=2&adid=<%#Eval("adid") %>" target="_blank">修改</a>                                          
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litNoInfo" runat="server">
                            </asp:Literal>
                        </tbody>
                    </table>
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                    <tr>
                        <td style="height: 20px; margin-top: 15px;" align="center">
                            <div class="pageInfoManu">
                                <asp:Literal ID="litPageInfo" runat="server">
                                </asp:Literal>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px; margin-top: 5px;" align="center">
                            <div class="manu">
                                <asp:Literal ID="litPage" runat="server">
                                </asp:Literal>
                            </div>
                        </td>
                    </tr>
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
            $('.tableBody').height(winh - 60 - 49 - 28 - 10);
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

        $(".new").click(function () {
            window.open('/page/agent/adedit.aspx?etype=1');
        })
    </script>
</body>
</html>
