<%@ Page Title="" Language="C#" MasterPageFile="~/page/users/UsersPage.Master" AutoEventWireup="true"
    CodeBehind="users_data.aspx.cs" Inherits="BMH.EagleEye.page.users.users_data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    数据报告
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
                            账户余额<em>（截止至前一个小时）</em></p>
                        <span class="num">暂无</span><span class="unit">元</span>
                    </div>
                </div>
            </div>
            <div class="cash-num clickInt clearfix">
                <div class="inner">
                    <div>
                        <p class="title">
                            昨日点击量/下载量</p>
                        <span class="num">暂无</span><span class="unit">次</span>
                    </div>
                </div>
            </div>
            <div class="cash-num expend clearfix">
                <div class="inner">
                    <div>
                        <p class="title">
                            昨日支出</p>
                        <span class="num">暂无</span><span class="unit">元</span>
                    </div>
                </div>
            </div>
        </div>
        <!--top end-->
        <div class="overall">
            <div class="head clearfix">
                <button class="btn_range fl btn_yesterday">
                    昨日</button>
                <button class="btn_range fl btn_last7day">
                        最近七天</button>
                <div class="dateBox fl">
                    <input id="date-range0" size="30" value="今天 2016年11月1日">
                    <button>
                    </button>
                </div>
            </div>
            <div class="chartTop clearfix">
                <ul class="num fl">
                    <li>点击量/下载量（次）<br>
                        <em>暂无</em> </li>
                    <li>支出（元）<br>
                        <em>暂无</em> </li>
                </ul>
            </div>
            <div class="dataTop clearfix">
                <ul class="tip">
                    <li class="on" name="day">按日</li><%--<li name="area">按地域</li><li name="hour">按时段</li><li name="class">按分类</li>--%>
                </ul>
                <div class="choice fr" style="position: relative">
                    <div class="box clearfix">
                        <dl class="fl">
                            <dd class="djliang">点击量/下载量</dd>
                        </dl>
                        <div style="position: absolute; right: 10px; height: 34px; width: 40px">
                            <div class="interval">
                            </div>
                            <div class="triangle fl">
                            </div>
                        </div>
                    </div>
                    <div class="slide">
                        <dl>
                            <%--<dd class="zxl">
                                <span class="title" data-color='#ff00ff' data-name="showcnt">展现量</span>
                                <div class="checkbox-con">
                                    <span>
                                        <input type="checkbox" class="ipt-hide" name="apk[]" value='展现量' checked>
                                        <label class="checkbox cur">
                                        </label>
                                    </span>
                                </div>
                            </dd>--%>
                            <dd class="djliang">
                                <span class="title" data-color='#7ccff1' data-name="clickcnt">点击量/下载量</span>
                                <div class="checkbox-con">
                                    <span>
                                        <input type="checkbox" class="ipt-hide" name="apk[]" value='点击量/下载量' checked>
                                        <label class="checkbox cur">
                                        </label>
                                    </span>
                                </div>
                            </dd>
                            <dd class="zxl">
                                <span class="title" data-color='#ff00ff' data-name="deductsum">支出</span>
                                <div class="checkbox-con">
                                    <span>
                                        <input type="checkbox" class="ipt-hide" name="apk[]" value='支出'>
                                        <label class="checkbox">
                                        </label>
                                    </span>
                                </div>
                            </dd>
                            <%--<dd class="ecpm">
                                <span class="title" data-color='#b2d466' data-name="ecpm">eCPM</span>
                                <div class="checkbox-con">
                                    <span>
                                        <input type="checkbox" class="ipt-hide" name="apk[]" value='eCPM'>
                                        <label class="checkbox">
                                        </label>
                                    </span>
                                </div>
                            </dd>
                            <dd class="cpc">
                                <span class="title" data-color='#eb66a5' data-name="cpc">CPC</span>
                                <div class="checkbox-con">
                                    <span>
                                        <input type="checkbox" class="ipt-hide" name="apk[]" value='CPC'>
                                        <label class="checkbox">
                                        </label>
                                    </span>
                                </div>
                            </dd>
                            <dd class="djl">
                                <span class="title" data-color='#fdf200' data-name="djl">点击率</span>
                                <div class="checkbox-con">
                                    <span>
                                        <input type="checkbox" class="ipt-hide" name="apk[]" value='点击率'>
                                        <label class="checkbox">
                                        </label>
                                    </span>
                                </div>
                            </dd>--%>
                        </dl>
                    </div>
                </div>
            </div>
            <!--chartTop end-->
            <div class="dataBox" id="myEchart">
            </div>
        </div>
    </div>
    <script>
        var _dimensionType = "day", _starttime, _endtime;
        var adUserId = <%=adUserId %>;
        $(".leftSide li").eq(0).addClass("on"); //菜单列表选中样式
    </script>
    <script src="../js/users/v2.0/users_data.base.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/demo.js" type="text/javascript"></script>
    <script src="../js/moment/moment.min.js" type="text/javascript"></script>
    <script src="../js/users/v2.0/jquery.daterangepicker.manager.js" type="text/javascript"></script>
    <script src="../js/echarts/echarts.js" type="text/javascript"></script>
    <script src="../js/users/v2.0/users_data.js" type="text/javascript"></script>
</asp:Content>
