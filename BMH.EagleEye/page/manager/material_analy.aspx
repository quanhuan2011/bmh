<%@ Page Title="" Language="C#" MasterPageFile="~/page/manager/MainPage.Master" AutoEventWireup="true"
    CodeBehind="material_analy.aspx.cs" Inherits="BMH.EagleEye.page.manager.material_analy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubContent" runat="server">
    <div class="contentSide data-analysis fr">
        <div class="content">
            <div class="top clearfix">
                <h3 class="new fl">
                    物料分析</h3>
                <button class="searchBtn fr">
                    搜索</button>
                <input type="text" name="" class="searchTxt fr" placeholder="请输入广告名称">
            </div>
            <div class="top clearfix">
                <div class=" slidebox stateBox fl " id="analy_mattype">
                    <div class="choice fl">
                        <div class="box">
                            <span class='title gray'>物料类型</span><div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=typeList%>
                                <%--  <dd class="cur">信息流</dd><dd>BANNER流</dd>--%>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class=" slidebox stateBox fl " id="analy_page">
                    <div class="choice fl">
                        <div class="box">
                            <span class='title gray'>广告页</span><div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=pageList%>
                                <%--  <dd class="cur">信息流</dd><dd>BANNER流</dd>--%>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class=" slidebox slideAd stateBox fl " id="analy_adl">
                    <div class="choice fl">
                        <div class="box" style="width: 194px">
                            <span class='title gray'>广告位<em>（可多选）</em></span><div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide" style="background: none">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <%=adLocationList %>
                            </table>
                        </div>
                    </div>
                </div>
                <%--<div class=" slidebox slideChannel stateBox fl " id="analy_class">
                    <div class="choice fl">
                        <div class="box" style="width: 174px">
                            <span class='title gray'>频道分类<em>（可多选）</em></span><div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide" style="background: none">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <%=classList %>                                
                            </table>
                        </div>
                    </div>
                </div>--%>
                <%-- <div class=" slidebox stateBox fl">
                    <div class="choice fl">
                        <div class="box"  style="width:148px;"><span class='title gray'>广告类型</span><div class="sanjiao"><span class="triangle"></span></div></div>
                        <div class="slide">
                            <dl>
                                <dd class="cur">金融类</dd><dd>游戏类</dd>
                            </dl> 
                        </div>                   
                    </div>
                </div>--%>
                <div class=" slidebox stateBox fl " id="analy_status">
                    <div class="choice fl">
                        <div class="box">
                            <span class='title gray'>状态类型</span><div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=statusList%>
                                <%-- <dd class="cur">正在投放</dd><dd>暂停投放</dd>--%>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="dateBox fr">
                    <input id="date-range0" size="30" value="2016-11-05 至 2016-11-10">
                </div>
            </div>
            <!-- top end -->
            <div class="tableHead">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                        <tr>
                            <th width="15%">
                                创建时间
                            </th>
                            <th width="15%">
                                物料名称
                            </th>
                            <th width="25%">
                                物料预览
                            </th>
                            <th width="10%">
                                平均点击率
                            </th>
                            <th width="10%">
                                昨日点击率
                            </th>
                            <th width="5%">
                                EMPC
                            </th>
                            <th width="10%">
                                状态
                            </th>
                            <th width="10%">
                                操作
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
                                    <td width="15%">
                                        <%#Eval("createtime") %>
                                    </td>
                                    <td width="15%">
                                        <%#Eval("materialname") %>
                                    </td>
                                     <%#GetmaterialPreview(Eval("materialtype").ToString(), Eval("title").ToString(), Eval("imageurl").ToString(), Eval("remark").ToString(), Eval("confirmtext").ToString(), Eval("canceltext").ToString())%>                                                                      
                                    <td width="10%">
                                        <%#Eval("avgclickrate") %>%
                                    </td>
                                    <td width="10%">
                                        <%#Eval("yestclickrate") %>%
                                    </td>
                                    <td width="5%">
                                        <%#Eval("ecpm") %>
                                    </td>
                                    <td width="10%" class="forbidden">
                                        <%#Eval("statusname") %>
                                    </td>
                                    <td width="10%">
                                        <a href='/page/report/material_data.aspx?materialid=<%#Eval("materialid") %>' target="_blank">
                                            详细报告</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Literal ID="litNoInfo" runat="server">
                        </asp:Literal>
                        <%-- <tr>
                          <td width="15%">
                            2016-11-22 14:22
                          </td>
                          <td width="15%">信息流信息流信息流</td>
                          <td width="25%" class="pic">
                            <p>睡觉觉会计师会计健康是计算机睡觉觉会计师会计健康是计算机</p>
                            <img src="images/pic1.png" alt="" class="textImg" width="90" height="58">
                          </td>
                          <td width="10%">66.66%</td>
                          <td width="10%">66.66%</td>
                          <td width="5%">10</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%">
                                  <a href="#">详细报告</a>
                          </td>
                        </tr>
                        <tr>
                          <td width="15%">仁泽wap-C1</td>
                          <td width="15%">580*435</td>
                          <td class="case" width="25%">
                              <h3>标题</h3>
                              <p>手机电池名考生进女厕所肯定是你空间的呢手机</p>
                              <button>确认</button><button>取消</button>
                          </td>
                          
                          <td width="10%">弹窗</td>
                          <td width="10%">2016-10-10</td>
                          <td width="5%">9</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%">
                                  <a href="#">详细报告</a>
                          </td>
                        </tr>
                        <tr>
                          <td width="15%">
                            2016-11-22 14:22
                          </td>
                          <td width="15%">信息流信息流信息流</td>
                          <td width="25%">
                            <img src="images/pic1.png" height="58" width="250" alt="" class="imgs">
                          </td>
                          <td width="10%">66.66%</td>
                          <td width="10%">66.66%</td>
                          <td width="5%">10</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%">
                                  <a href="#">详细报告</a>
                          </td>
                        </tr>
                        <tr>
                          <td width="15%">
                            2016-11-22 14:22
                          </td>
                          <td width="15%">信息流信息流信息流</td>
                          <td width="25%" class="pictxt">
                            <p>睡觉觉会计师会计健康是计算机睡觉觉会计师会计健康是计算机</p>
                            <img src="images/pic1.png" height="58" width="250" alt="" class="imgs">
                          </td>
                          <td width="10%">66.66%</td>
                          <td width="10%">66.66%</td>
                          <td width="5%">10</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%" >
                                  <a href="#">详细报告</a>
                          </td>
                        </tr>
                        <tr>
                          <td width="15%">
                            2016-11-22 14:22
                          </td>
                          <td width="15%">信息流信息流信息流</td>
                          <td width="25%" class="pic">
                            <p>睡觉觉会计师会计健康是计算机睡觉觉会计师会计健康是计算机</p>
                            <img src="images/pic1.png" alt="" class="textImg" width="90" height="58">
                          </td>
                          <td width="10%">66.66%</td>
                          <td width="10%">66.66%</td>
                          <td width="5%">10</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%">
                                  <a href="#">详细报告</a>
                          </td>
                        </tr>
                         <tr>
                          <td width="15%">
                            2016-11-22 14:22
                          </td>
                          <td width="15%">信息流信息流信息流</td>
                          <td width="25%">
                            <img src="images/pic1.png" height="58" width="250" alt="" class="imgs">
                          </td>
                          <td width="10%">66.66%</td>
                          <td width="10%">66.66%</td>
                          <td width="5%">10</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%">
                                  <a href="#">详细报告</a>
                          </td>
                        </tr> 
                         <tr>
                          <td width="15%">
                            2016-11-22 14:22
                          </td>
                          <td width="15%">信息流信息流信息流</td>
                          <td width="25%" class="pictxt">
                            <p>睡觉觉会计师会计健康是计算机睡觉觉会计师会计健康是计算机</p>
                            <img src="images/pic1.png" height="58" width="250" alt="" class="imgs">
                          </td>
                          <td width="10%">66.66%</td>
                          <td width="10%">66.66%</td>
                          <td width="5%">10</td>
                          <td width="10%" class="forbidden">暂无</td>
                          <td width="10%" >
                                  <a href="#">详细报告</a>
                          </td>
                        </tr> 
                        --%>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ExtendContent" runat="server">
    <style>
        .slidebox .slide .cur
        {
            background: #12bdce;
        }
    </style>
    <link href="../css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="../js/manager/v2.0/data-analysis.js" type="text/javascript"></script>
    <script src="../js/moment/moment.min.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.daterangepicker.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/demo.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>    
    <script>
        $(".leftSide dd").eq(4).addClass("cur"); //菜单列表选中样式
        //下拉框
        $(".stateBox .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
        })
        //搜索 并且是其他条件的确认
        $(".searchBtn").click(function () {
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            //搜索
            var searchVal = $(this).parent().find(".searchTxt").val();
            url = getMatchUrl(url, "key", searchVal);
            //物料类型
            var matType = $("#analy_mattype").find(".cur").find("span").text();
            if (matType != "all") {
                url = getMatchUrl(url, "mattype", matType);
            }
            else {
                url = clearMatchUrl(url, "mattype");
            }
            //页面
            var pageid = $("#analy_page").find(".cur").find("span").text();
            if (pageid != "all") {
                url = getMatchUrl(url, "pid", pageid);
            }
            else {
                url = clearMatchUrl(url, "pid");
            }
            //广告位
            var adLocationId = "";
            $("#analy_adl table td").each(function () {
                if ($(this).find("label").hasClass("cur")) {
                    adLocationId += $(this).find("span").eq(1).text() + ",";
                }
            })
            if (adLocationId != "") {
                url = getMatchUrl(url, "adlid", adLocationId.substr(0, adLocationId.length - 1));
            }
            else {
                url = clearMatchUrl(url, "adlid");
            }
            //状态
            var statusid = $("#analy_status").find(".cur").find("span").text();
            if (statusid != "all") {
                url = getMatchUrl(url, "sid", statusid);
            }
            else {
                url = clearMatchUrl(url, "sid");
            }
            var timeRange = $("#date-range0").val();
            var startTime = $.trim(timeRange.split('至')[0].replace("-", "").replace("-", ""));
            var endTime = $.trim(timeRange.split('至')[1].replace("-", "").replace("-", ""));
            url = getMatchUrl(url, "stime", startTime);
            url = getMatchUrl(url, "etime", endTime);

            location.href = getMatchUrl(url, "key", searchVal);
        })
        //默认填充
        var keyVal = getUrlVal("key");
        $(".searchTxt").val(keyVal);
        var matTypeVal = getUrlVal("mattype");
        if (matTypeVal != "") {
            $("#analy_mattype .slide dd").removeClass("cur");
            $("#analy_mattype .slide dd").each(function () {
                if ($(this).find("span").text() == matTypeVal) {
                    $(this).addClass("cur");
                    $("#analy_mattype").find(".title").text($(this).find("em").text()).removeClass("gray");
                }
            })
        }
        var pageVal = getUrlVal("pid");
        if (pageVal != "") {
            $("#analy_page .slide dd").removeClass("cur");
            $("#analy_page .slide dd").each(function () {
                if ($(this).find("span").text() == pageVal) {
                    $(this).addClass("cur");
                    $("#analy_page").find(".title").text($(this).find("em").text()).removeClass("gray");
                }
            })
        }
        //广告位
        var adLocationId = getUrlVal("adlid");
        if (adLocationId != "") {
            $("#analy_adl table td label").removeClass("cur");
            var adlArr = new Array();
            adlArr = adLocationId.split(",");
            for (var i = 0; i < adlArr.length; i++) {
                var tempId = adlArr[i];
                $("#analy_adl table td").each(function () {
                    if (tempId == $(this).find("span").eq(1).text()) {
                        $(this).find("label").addClass("cur");
                    }
                })
            }
        }

        var statusVal = getUrlVal("sid");
        if (statusVal != "") {
            $("#analy_status .slide dd").removeClass("cur");
            $("#analy_status .slide dd").each(function () {
                if ($(this).find("span").text() == statusVal) {
                    $(this).addClass("cur");
                    $("#analy_status").find(".title").text($(this).find("em").text()).removeClass("gray");
                }
            })
        }
        var startTime = getUrlParam("stime");
        var endTime = getUrlParam("etime");
        var timeRange;
        if (startTime != null && endTime != null && startTime.length == 8) {
            timeRange = startTime.substr(0, 4) + "-" + startTime.substr(4, 2) + "-" + startTime.substr(6, 2) + " 至 " + endTime.substr(0, 4) + "-" + endTime.substr(4, 2) + "-" + endTime.substr(6, 2);
            $("#date-range0").val(timeRange);
        }
        else {
            var startdate = GetDateStr(-8);
            var enddate = GetDateStr(-1);
            $("#date-range0").val(startdate + ' 至 ' + enddate);
        }
        //广告页切换广告位
        $("#analy_page .slide dd").click(function () {
            var pageId = $("#analy_page").find(".cur").find("span").text();
            var ajaxData = {
                method: "GetAdLocationData",
                pageid: pageId
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {
                    if (result.errcode == "1") {
                        var adLocationList = result.data;
                        var tableHtml = [];
                        $.each(adLocationList, function (i, item) {
                            if (i == 0) {
                                tableHtml.push("<tr>");
                            }
                            tableHtml.push("<td>");
                            tableHtml.push("<div class='checkbox-con clearfix'>");
                            tableHtml.push("<span class='wrap'>");
                            tableHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'>");
                            tableHtml.push("<label class='checkbox'></label></span><em>");
                            tableHtml.push(item.adlocationname);
                            tableHtml.push("</em></div></td>");
                            if ((i + 1) % 3 == 0 && (i + 1) != adLocationList.length) {
                                tableHtml.push("</tr><tr>");
                            }
                            if ((i + 1) == adLocationList.length) {
                                tableHtml.push("</tr>");
                            }
                        })
                        $("#analy_adl table").html(tableHtml.join(''));
                    }
                }
            });

        })

    </script>
</asp:Content>
