<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/page/manager/MainPage.Master"
    CodeBehind="ad_info.aspx.cs" Inherits="BMH.EagleEye.page.manager.ad_info" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="Server">
    广告投放管理
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="SubContent" runat="Server">
    <div class="contentSide fr">
        <div class="content">
            <div class="top clearfix">
                <button class="new fl unclick">
                    新建广告投放
                </button>
                <button class="stop fl unclick">
                    暂停
                </button>
                <button class="start fl unclick">
                    启用
                </button>
                 <button class="unpass fl unclick"  style="margin-left:20px">
                    审核不通过
                </button>
                <div class=" slidebox stateBox fl" id="info_status">
                    <span class="fl">状态： </span>
                    <div class="choice fl">
                        <div class="box">
                            <span class='title'>
                                <%=statusDefault%>
                            </span>
                            <div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=statusList%>                               
                            </dl>
                        </div>
                    </div>
                </div>                
                <%-- <button class="searchBtn fr" style="margin-right:15px;">
                        搜索
                        </button>
                --%>
                <button class="searchBtn fr" style="margin-right: 15px;">
                    搜索</button>
                <input type="text" name="" class="searchTxt fr" placeholder="请输入广告或名称">
            </div>
            <div class="top clearfix">
                <div class="slidebox stateBox fl" id="info_aduser">
                    <span class="fl">广告主： </span>
                    <div class="choice fl">
                        <div class="box">
                            <span class="title">
                                <%=adUserDefault%>
                            </span>
                            <div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=adUserList%>                               
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="slidebox adPage fl" id="info_page">
                    <span class="fl">广告页： </span>
                    <div class="choice fl">
                        <div class="box">
                            <span class="title">
                                <%=pageDefault%>
                            </span>
                            <div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=pageList%>                               
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="slidebox adPage fl" id="info_adlocation">
                    <span class="fl">广告位： </span>
                    <div class="choice fl">
                        <div class="box">
                            <span class="title">
                                <%=adLocationDefault%>
                            </span>
                            <div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=adLocationList%>                               
                            </dl>
                        </div>
                    </div>
                </div>

            </div>
            <!-- top end -->
            <div class="tableHead">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                        <tr>
                            <th class="head1" width="14%">
                                <div class="checkbox-con">
                                    <span class="wrap">
                                        <input type="checkbox" class="ipt-hide" checked="checked">
                                        <label class="checkbox">
                                        </label>
                                    </span><em>名称</em>
                                </div>
                            </th>
                            <th width="8%">
                                状态
                            </th>
                            <%--<th width="20%">广告页</th>--%>
                            <th width="20%">
                                广告位
                            </th>
                            <th width="12%">
                                物料
                            </th>
                            <th width="7%">
                                计费
                            </th>
                            <th width="15%">
                                投放时间
                            </th>
                            <th width="8%">
                                策略
                            </th>
                            <th width="8%">
                                定向
                            </th>
                            <th width="8%">
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
                                    <td width="14%">
                                        <div class="checkbox-con">
                                            <span class="wrap">
                                                <input type="checkbox" class="ipt-hide" checked="checked">
                                                <label class="checkbox">
                                                </label>
                                            </span><em>
                                                <%#Eval( "adname") %><span class="adid" style="display: none"><%#Eval( "adid") %></span></em>
                                        </div>
                                        <p class="advertisingPageId">
                                            ID:
                                            <%#Eval( "adid") %></p>
                                    </td>
                                    <td width="8%" class="state on">
                                        <span class="adstatus" style="display: none">
                                            <%#Eval( "status") %></span><span><%#Eval("statusname")%></span>
                                    </td>
                                    <td width="20%" class="address">
                                        <p>
                                            <%#Eval("adlocationname")%></p>                                      
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
                                    <td width="8%" class="forbidden">
                                        <a href="#" class="detail">详细设置</a>
                                    </td>
                                    <td width="8%" class="forbidden">
                                        <a href="#" class="detail">详细设置</a>
                                    </td>
                                    <td width="8%" class="editBox">
                                        <img src="../images/edit.png" height="18" width="18" alt="">
                                        <div class="edit">
                                            <a href="ad_edit.aspx?etype=2&adid=<%#Eval("adid") %>" target="_blank">修改</a><a href='../report/ad_data.aspx?adid=<%#Eval("adid") %>' target="_blank">报告</a>
                                        </div>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ExtendContent" runat="Server">
    <script src="../js/manager/v2.0/ad_base.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="../js/manager/v2.0/ad-management.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script>
        var pLevel='<%=permLevel %>';
        if (pLevel == "-1") {
            $(".new").addClass("unclick");
            $(".start").addClass("unclick");
            $(".stop").addClass("unclick");
            $(".unpass").addClass("unclick");
            $(".new").attr("disabled", true);
            $(".start").attr("disabled", true);
            $(".stop").attr("disabled", true);
            $(".unpass").attr("disabled", true);
            $(".tableBody table tr").each(function () {
                $(this).find(".editBox .edit").find("a").eq(0).remove();                
            })
        }
        else {
            $(".new").removeClass("unclick");
            $(".start").removeClass("unclick");
            $(".stop").removeClass("unclick");
            $(".unpass").removeClass("unclick");
        }
      
        $(".leftSide dd").eq(1).addClass("cur"); //菜单列表选中样式
        $(".new").click(function () {

            window.open('ad_edit.aspx?etype=1');
        })
        //$(".slide dd").click(function () {
        //    $(this).closest(".slide").find("dd").removeClass("cur");
        //    $(this).addClass("cur");
        //})
        $(".datalist tr").each(function () {
         //   alert($(this).find("td .adstatus").text());
            if ($.trim($(this).find("td .adstatus").text()) == "1") {
                $(this).find("td .adstatus").parents(".state").addClass("on")
            }
            else {
                $(this).find("td .adstatus").parents(".state").removeClass("on")
            }
        })
        var keyVal = getUrlVal("key");
        $(".searchTxt").val(keyVal);
        var sTypeVal = getUrlVal("stype");
        if (sTypeVal != "") {
            $("#info_status .slide dd").removeClass("cur");
            $("#info_status .slide dd").each(function () {
                if ($(this).find("span").text() == sTypeVal) {
                    $(this).addClass("cur");
                    $("#info_status").find(".title").text($(this).find("em").text());
                }
            })
        }
        var pTypeVal = getUrlVal("ptype");
        if (pTypeVal != "") {
            $("#info_page .slide dd").removeClass("cur");
            $("#info_page .slide dd").each(function () {
                if ($(this).find("span").text() == pTypeVal) {
                    $(this).addClass("cur");
                    $("#info_page").find(".title").text($(this).find("em").text());
                }
            })
        }
        var adUserVal = getUrlVal("aduid");
        if (adUserVal != "") {
            $("#info_aduser .slide dd").removeClass("cur");
            $("#info_aduser .slide dd").each(function () {
                if ($(this).find("span").text() == adUserVal) {
                    $(this).addClass("cur");
                    $("#info_aduser").find(".title").text($(this).find("em").text());
                }
            })
        }
        var adLocationVal = getUrlVal("adlid");
        if (adLocationVal != "") {
            $("#info_adlocation .slide dd").removeClass("cur");
            $("#info_adlocation .slide dd").each(function () {
                if ($(this).find("span").text() == adLocationVal) {
                    $(this).addClass("cur");
                    $("#info_adlocation").find(".title").text($(this).find("em").text());
                }
            })
        }
        //搜索
        $(".searchBtn").click(function () {
            var searchVal = $(this).parent().find(".searchTxt").val();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "key", searchVal);
        })
        //状态
        $("#info_status .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var sTypeVal = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "stype", sTypeVal);
        })
        //页面
        $("#info_page .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var pTypeVal = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            url = clearMatchUrl(url, "adlid");
            location.href = getMatchUrl(url, "ptype", pTypeVal);
        })
        //广告主
        $("#info_aduser .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var tempVal = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "aduid", tempVal);
        })
        //广告位
        $("#info_adlocation .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var tempVal = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "adlid", tempVal);
        })
    </script>
</asp:Content>
