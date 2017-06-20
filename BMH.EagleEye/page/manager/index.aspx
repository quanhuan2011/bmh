<%@ Page Title="" Language="C#" MasterPageFile="~/page/manager/ManagerPage.Master"
    AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BMH.EagleEye.page.manager.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    首页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubContent" runat="server">
    <div class="user-main fr">
        <div class="head">
            <h3>
                数据报告</h3>
        </div>
        <div class="user-top">
            <div class="cash-num clearfix">
                <div class="inner">
                    <div>
                        <p class="title">
                            昨日收入</p>
                        <span class="num">
                            <%=incomSum %></span><span class="unit">元</span>
                    </div>
                </div>
            </div>
            <div class="cash-num clickInt clearfix">
                <div class="inner">
                    <div>
                        <p class="title">
                            昨日点击量</p>
                        <span class="num">
                            <%=clickCnt %></span><span class="unit">次</span>
                    </div>
                </div>
            </div>
            <div class="cash-num expend clearfix">
                <div class="inner">
                    <div>
                        <p class="title">
                            昨日报表</p>
                        <span class="amount" style="width: 144px">请求量：<em class="green"><%=requestCnt %></em></span><span
                            class="amount">点击率：<em class="green"><%=clickRate %>%</em></span>
                    </div>
                </div>
            </div>
        </div>
        <%--  <div class="head">
                <h3>昨日收入明细</h3>
        </div>--%>
        <div class="filter">
            <div class="box select fl">
                <input type="text" value="广告位">
                <button>
                </button>
                <div class="slide-down select_type">
                    <p>
                        <em>广告位</em><span class="hide">1</span></p>
                    <p>
                        <em>广告主</em><span class="hide">2</span></p>
                </div>
            </div>
            <%=pageHtml%>
            <div class="clearfix fl">
                <button class="btn_range fl btn_yesterday">
                    昨日</button>
                <button class="btn_range fl btn_last7day">
                    最近七天</button>
                <div class="dateBox fl">
                    <input id="date-range0" size="30" value="今天 2016年11月1日">
                </div>
            </div>
            <div class="box search fr">
                <input type="text" placeholder="输入广告主名称/广告位名称">
                <button>
                    搜索</button>
            </div>
        </div>
        <div class="tableHead operationLog">
            <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                <thead>
                    <tr>
                        <%=tableHeadHtml%>
                    </tr>
                </thead>
            </table>
        </div>
        <div class="tableBody operationLog">
            <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                <tbody>
                    <asp:Repeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="40%">
                                    <%#Eval("statname") %>
                                </td>
                                <td width="30%">
                                    <%#Eval("clickcnt") %>
                                </td>
                                <td width="30%">
                                    <%# Convert.ToDecimal(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "incomsum"))).ToString("f2") %>&ensp;
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="repListByAdUser" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="30%">
                                    <%#Eval("statname") %>
                                </td>
                                <td width="20%">
                                    <%#Eval("clickcnt") %>
                                </td>
                                <td width="25%">
                                    <%#Eval("balance")%>
                                </td>
                                <td width="25%">
                                    <%# Convert.ToDecimal(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "incomsum"))).ToString("f2") %>&ensp;
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litNoInfo" runat="server">
                    </asp:Literal>
                </tbody>
            </table>
        </div>
           <div class="datasum-output">
          <span>总点击：<%=clickTotal%></span><span>总收入：<%=incomeTotal %></span>
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
    <style>
        .tableHead th
        {
            background: #fff;
        }
        .datalist thead tr
        {
            color: #737373;
        }
    </style>
    <link href="../css/page.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="../js/moment/moment.min.js" type="text/javascript"></script>
    <script src="../js/manager/v2.0/jquery.daterangepicker.users.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/demo.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script>
        $(".leftSide dd").eq(0).addClass("cur"); //菜单列表选中样式
        var winh = $(window).height();
        $('#user-main').height(winh);
        $('.tableBody').height(winh - 60 - 124 - 60 - 52 - 50 - 15);
        var sType = getUrlVal("stype");
        if (sType != "") {
            $('.filter .select_type p').each(function () {
                if ($(this).find("span").text() == sType) {
                    $(this).closest(".select").find("input").val($(this).find("em").text());                    
                }
            })
        }
        var pid = getUrlVal("pid");
        if (pid != "") {
            $('.filter .select_page p').each(function () {
                if ($(this).find("span").text() == pid) {
                    $(this).closest(".select").find("input").val($(this).find("em").text());                    
                }
            })
        }
        var startTime = getUrlParam("starttime");
        var endTime = getUrlParam("endtime");
        var timeRange;
        if (startTime != null && endTime != null && startTime.length == 8) {
            timeRange = startTime.substr(0, 4) + "-" + startTime.substr(4, 2) + "-" + startTime.substr(6, 2) + " 至 " + endTime.substr(0, 4) + "-" + endTime.substr(4, 2) + "-" + endTime.substr(6, 2);
            $("#date-range0").val(timeRange);
            if (startTime == GetDateStr(-1).replace(/-/g, "") && endTime == GetDateStr(-1).replace(/-/g, "")) {
                $('.filter .btn_yesterday').addClass("on");
                $('.filter .btn_last7day').removeClass("on");
            }
            else if (startTime == GetDateStr(-8).replace(/-/g, "") && endTime == GetDateStr(-1).replace(/-/g, "")) {
                $('.filter .btn_yesterday').removeClass("on");
                $('.filter .btn_last7day').addClass("on");
            }
            else {
                $('.filter .btn_yesterday').removeClass("on");
                $('.filter .btn_last7day').removeClass("on");
            }
        }
        else {
            var startdate = GetDateStr(-1);
            var enddate = GetDateStr(-1);
            $("#date-range0").val(startdate + ' 至 ' + enddate);
            $('.filter .btn_yesterday').addClass("on");
            $('.filter .btn_last7day').removeClass("on");
        }
        $('.filter .btn_yesterday').click(function () {
            var url = location.href;
            url = getMatchUrl(url, "starttime", GetDateStr(-1).replace(/-/g, ""));
            url = getMatchUrl(url, "endtime", GetDateStr(-1).replace(/-/g, ""));
            location.href = getMatchUrl(url, "page", "1");
        })
        $('.filter .btn_last7day').click(function () {
            var url = location.href;
            url = getMatchUrl(url, "starttime", GetDateStr(-8).replace(/-/g, ""));
            url = getMatchUrl(url, "endtime", GetDateStr(-1).replace(/-/g, ""));
            location.href = getMatchUrl(url, "page", "1");
        })

        $(".user-main .tableBody").niceScroll({  //调用滚动条
            cursorcolor: "#12bdce",
            cursoropacitymin: 1,
            cursoropacitymax: 1,
            touchbehavior: true,
            cursorwidth: "5px",
            cursorborder: "0",
            cursorborderradius: "5px",
            horizrailenabled: false
        });
        //搜索
        $(".search button").click(function () {
            var searchVal = $(".search").find("input").val();
            if ($.trim(searchVal) == "") {
                alert("请输入搜索内容");
                return;
            }
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "key", $.trim(searchVal));
        })
        var searchVal = getUrlVal("key");
        if (searchVal != null && searchVal != "") {
            $(".search").find("input").val(searchVal);
        }
        $('.filter .select input').click(function (event) {
            if ($(this).parent().hasClass('open')) {
                $(this).parent().find('.slide-down').slideUp(200);
                $(this).parent().removeClass('open');
            } else {
                $(this).parent().find('.slide-down').slideDown(200);
                $(this).parent().addClass('open');
            }

            event.stopPropagation()
        })
        //类型
        $('.filter .select_type p').click(function () {
            var sType = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            url = clearMatchUrl(url, "key");
            url = clearMatchUrl(url, "pid");
            location.href = getMatchUrl(url, "stype",sType);
        })
        //广告页
        $('.filter .select_page p').click(function () {
            var pid = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "pid", pid);
        })
        $('.filter .select button').click(function (event) {
           $(this).parent().find('input').trigger('click');
            event.stopPropagation()
        })
        $('body').click(function () {
            $('.slide-down').slideUp();
            $('.filter .select').removeClass('open')
        })
   
</script>
</asp:Content>
