<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/page/manager/MainPage.Master"
    CodeBehind="adlocation_info.aspx.cs" Inherits="BMH.EagleEye.page.manager.adlocation_info" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="Server">
    广告位管理
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="SubContent" runat="Server">
    <div class="contentSide fr">
        <div class="content">
            <div class="top clearfix">
                <!-- <button class="new fl">新建广告位</button> -->
                <%--  <div class=" slidebox stateBox fl">
                        <span class="fl">
                            广告位类型：
                        </span>
                        <div class="choice fl">
                            <div class="box">
                                <span class='title'>
                                    <%=typeDefault%>
                                </span>
                                <div class="sanjiao">
                                    <span class="triangle">
                                    </span>
                                </div>
                            </div>
                            <div class="slide">                              
                                <dl>
                                    <%=typeList%>
                                </dl>
                            </div>
                        </div>                      
                    </div>--%>
                <div class="slidebox adPage fl">
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
                <button class="searchBtn fr" style="margin-right: 15px;">搜索</button>
                <%-- <asp:LinkButton ID="btnSearch" runat="server" CssClass="searchBtn fr"
                        onclick="btnSearch_Click">
                            搜索
                        </asp:LinkButton>--%>

                <input type="text" name="" class="searchTxt fr" placeholder="广告位ID或名称">
            </div>
            <!-- top end -->
            <div class="tableHead">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                        <tr>
                            <th width="240">广告位编码</th>
                            <th width="300">广告位名称</th>
                            <th width="300">类型</th>
                        </tr>
                    </thead>
                </table>
            </div>

            <div class="tableBody">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tbody>
                        <asp:Repeater ID="repList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td width="240"><%#Eval( "bcode") %></td>
                                    <td width="300"><%#Eval( "adlocationname") %></td>
                                    <td width="300" class="editBox">
                                        <a href='../report/adlocation_data.aspx?adlocationid=<%#Eval("adlocationid") %>' target="_blank">报告
                                        </a>
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
    <script src="../js/manager/v2.0/managerbase.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script>

        $(".leftSide dd").eq(2).addClass("cur");//菜单列表选中样式
        $(".popup .tab-bd,.tableBody").niceScroll({ //调用滚动条 
            cursorcolor: "#12bdce",
            cursoropacitymin: 1,
            cursoropacitymax: 1,
            cursorwidth: "5px",
            cursorborder: "0",
            cursorborderradius: "5px",
            horizrailenabled: false
        });
        //    $('.edit').each(function () {
        //        //alert(1);
        //        $(this).find('a').eq(0).click(function () {
        //            yyCommon.popoupShow();

        //        })
        //    })
        //搜索
        $(".searchBtn").click(function () {
            var searchVal = $(".searchTxt").val();            
            var url = location.href;
            url = getMatchUrl(url, "page", "1");      
            location.href = getMatchUrl(url, "key", $.trim(searchVal));
        })
        var searchVal = getUrlVal("key");
        if (searchVal != null && searchVal != "") {
            $(".searchTxt").val(searchVal);
        }
        //
        var pageId = getUrlVal("pid");
        if (pageId != "") {
            $(".adPage .slide dd").removeClass("cur");
            $(".adPage .slide dd").each(function () {
                if ($(this).find("span").text() == pageId) {
                    $(this).addClass("cur");
                    $(".adPage").find(".title").text($(this).find("em").text());
                }
            })
        }
        //页面
        $(".adPage .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var pageId = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "pid", pageId);
        })
    </script>

</asp:Content>
