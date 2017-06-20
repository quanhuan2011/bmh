<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bidding_info.aspx.cs" Inherits="BMH.EagleEye.page.manager.bidding_info" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>竞价管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no, minimal-ui" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务">
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。">
    <link rel="stylesheet" href="/page/css/ad-bidding.css">
    <script src="/page/js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="/page/js/jquery/jquery.easing.min.js" type="text/javascript"></script>
</head>
<body>
    <div id="wrap">
        <!--[if lt IE 10]>
  <div class="browser-happy">
      <div class="content">
          您正在使用ie浏览器版本太老，本页面的显示效果可能不佳，建议您升级到ie10及以上。
          <a href="http://browsehappy.com/" target="_blank"> 立即更新</a>
      </div>
  </div>
<![endif]-->
        <div id="YY-head">
            <div class="clearfix w1230">
                <div class="logo fl">
                </div>
                <div class="personInfo fr">
                    <div class="set-area">
                        <em>
                            <img src="/page/images/tips.png" height="7" width="20" alt=""></em>
                        <a href="/page/logout.aspx" class="exit">退出</a>
                    </div>
                    <p class="set fr">设置</p>
                    <span class="line fr">|</span>
                    <span class="name fr">管理员</span>
                    <img src="/page/images/head.jpg" height="51" width="51" alt="" class="head fr">
                </div>
            </div>
        </div>
    </div>
    <!-- YY-head  end -->
    <div class="w1230" id="YY-main">
        <div class="leftSide fl">
            <ul>

                <li class="on  subcontent"><a href="/page/manager/ad_info.aspx#">后台管理</a>
                    <dl>
                        <dd>
                            <a href="/page/manager/index.aspx">首页</a></dd>
                        <dd>
                            <a href="/page/manager/ad_info.aspx">广告投放管理</a></dd>
                        <dd>
                            <a href="/page/manager/adlocation_info.aspx">广告位报表</a></dd>
                        <dd>
                            <a href="/page/manager/material_info.aspx">物料管理</a></dd>
                        <dd>
                            <a href="/page/manager/material_analy.aspx">物料分析</a></dd>
                        <dd>
                            <a href="/page/manager/agent_info.aspx">代理商管理</a>
                        </dd>
                        <dd class="cur">
                            <a href="/page/manager/bidding_info.aspx">竞价管理</a>
                        </dd>
                    </dl>
                </li>
            </ul>
        </div>
        <!--内容 -->

        <div class="contentSide fl">
            <div class="content" id="bid_adu">
                <h1 class="page-name">竞价管理</h1>
                <div class="topNewBtn">
                    <a class="new" href="/page/manager/ad_edit.aspx?etype=1"  target="_blank">新建竞价广告</a>
                </div>

                <div class="top">
                    代理商权重管理
           
                </div>
                <!-- top end -->
                <div class="tableHead">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <thead>
                            <tr>
                                <th width="20%">代理商编号</th>
                                <th width="16%">代理商名称</th>
                                <th width="16%">联系方式</th>
                                <th width="16%">代理商区域</th>
                                <th width="20%">代理商级别（所属上级）</th>
                                <th width="12%">权重</th>
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
                                        <td width="20%"><%#Eval("aduserno") %><span class="adu_id" style="display:none;"><%#Eval("aduserid") %></span></td>
                                        <td width="16%"><%#Eval( "adusername") %></td>
                                        <td width="16%"><%#Eval( "tel") %></td>
                                        <td width="16%"><%#Eval( "cityname") %></td>
                                        <td width="20%" class="per"><%#Eval( "aduserdesc") %></td>
                                        <td width="12%" class="view"><%#Eval("weight") %><a>修改</a></td>
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
            <!--1 end-->
            <div class="content" id="bid_adl">

                <div class="top">
                    <button class="addnew advertising">+增加</button>
                    竞价广告位管理
           
                </div>
                <!-- top end -->
                <div class="tableHead">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <thead>
                            <tr>
                                <th width="15%">广告位编码</th>
                                <th width="26%">广告位名称</th>
                                <th width="15%">页面信息</th>
                                <th width="11%">广告位形式</th>
                                <th width="11%">广告位级别</th>
                                <th width="11%">是否竞价</th>
                                <th width="11%">操作</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="tableBody" tabindex="0" style="overflow-y: hidden; outline: none;">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tbody>
                            <asp:Repeater ID="repList2" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td width="15%"><%#Eval("bcode") %><span class="adl_id" style="display:none;"><%#Eval("adlocationid") %></span></td>
                                        <td width="26%"><%#Eval( "adlocationname") %></td>
                                        <td width="15%"><%#Eval( "pagename") %></td>
                                        <td width="11%"><%#Eval( "subadtypename") %></td>
                                        <td width="11%" class="per"><%#Eval( "alevel") %></td>
                                        <td width="11%" class="view"><%#Eval( "isbid") %><a>修改</a></td>
                                        <td width="11%" class="watch"><a href="/page/report/adlocation_data.aspx?adlocationid=<%#Eval( "adlocationid") %>" target="_blank">查看报表</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litNoInfo2" runat="server">
                            </asp:Literal>

                        </tbody>
                    </table>
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                    <tbody>
                        <tr>
                            <td style="height: 20px; margin-top: 15px;" align="center">

                                <div class="manu" style="display: inline-block">
                                    <asp:Literal ID="litPage2" runat="server">
                                    </asp:Literal>
                                </div>
                                <div class="pageInfoManu" style="display: inline-block">
                                    <asp:Literal ID="litPageInfo2" runat="server">
                                    </asp:Literal>
                                </div>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
            <!--2 end-->
            <div class="content" id="bid_adv">
                <div class="top">
                    <button class="addnew addAd">+增加</button>
                    竞价广告管理
           
                </div>
                <!-- top end -->
                <div class="tableHead">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <thead>
                            <tr>
                                <th width="10%">广告ID</th>
                                <th width="31%">广告名称</th>
                                <th width="17%">代理商 </th>
                                <th width="14%">广告单价</th>
                                <th width="14%">是否竞价</th>
                                <td width="14%">操作</td>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="tableBody" tabindex="0" style="overflow-y: hidden; outline: none;">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tbody>
                            <asp:Repeater ID="repList3" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td width="10%"><%#Eval("adid") %></td>
                                        <td width="31%"><%#Eval("adname") %></td>
                                        <td width="17%"><%#Eval("adusername") %></td>
                                        <td width="14%"><%#Eval("price") %></td>
                                        <td width="14%" class="view">是<a href="/page/manager/ad_edit.aspx?etype=2&adid=<%#Eval("adid") %>" target="_blank">修改</a></td>
                                        <td width="14%" class="watch"><a href="/page/report/ad_data.aspx?adid=<%#Eval("adid") %>" target="_blank">查看报表</a></td>
                                    </tr>
                                    </tr>
                               
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litNoInfo3" runat="server">
                            </asp:Literal>

                        </tbody>
                    </table>
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                    <tbody>
                        <tr>
                            <td style="height: 20px; margin-top: 15px;" align="center">

                                <div class="manu" style="display: inline-block">
                                    <asp:Literal ID="litPage3" runat="server">
                                    </asp:Literal>
                                </div>
                                <div class="pageInfoManu" style="display: inline-block">
                                    <asp:Literal ID="litPageInfo3" runat="server">
                                    </asp:Literal>
                                </div>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
            <!--3 end-->
        </div>

    </div>
    </div>
    <div class="loginmask"></div>
    <div class="loginmask3"></div>
    <div class="popup popup1" id="popup_adl">
        <div class="top">添加竞价广告位</div>
        <table class="table-head clearfix" style="width: 100%; height: 42px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="width: 3%; position: relative">
                    <div class="checkbox-con">
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="">
                            <label class="checkbox"></label>
                        </span>
                    </div>
                </td>
                <td style="width: 21%">
                    <em>广告位编码</em>
                </td>
                <td style="width: 20%">广告位名称</td>
                <td style="width: 18%">页面信息</td>
                <td style="width: 14%">广告位形式</td>
                <td style="width: 14%">广告位级别</td>
                <td style="width: 10%">是否竞价</td>
            </tr>
        </table>
        <div class="table-content">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tbody>
                   <%-- <tr>
                        <td style="width: 3%">
                            <div class="checkbox-con"><span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="checked"><label class="checkbox"></label></span></div>
                        </td>
                        <td style="width: 21%; padding: 0 4px"><em class="list_name">热门排行banner位</em><span class="list_materialid">63</span></td>
                        <td style="width: 20%"></td>
                        <td style="width: 18%">170*128</td>
                        <td style="width: 14%">banner</td>
                        <td style="width: 14%">I级广告位</td>
                        <td style="width: 10%">否</td>
                    </tr>--%>

                </tbody>
            </table>
            <%--<table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                <tbody>
                    <tr>
                        <td style="height: 20px; margin-top: 15px;" align="center">

                            <div class="manu" style="display: inline-block">
                                <span class="disabled">首页 </span><span class="disabled">上一页 </span><span class="current">1</span><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2">2</a><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2"> 下一页 </a><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2">尾页 </a>
                            </div>
                            <div class="pageInfoManu" style="display: inline-block">
                                共有<font style="color: red;">31</font>条&nbsp;&nbsp;<font style="color: red;">1</font>/<span>2</span>页
                       
                            </div>
                        </td>
                    </tr>

                </tbody>
            </table>--%>
        </div>

        <div class="btn">
            <button class="confirmT">确定</button>
            <button class="cancelT">取消</button>
        </div>
    </div>
    <div class="popup popup2" id="popup_adv">
        <div class="top">添加竞价广告</div>
        <table class="table-head clearfix" style="width: 100%; height: 42px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="width: 3%; position: relative">
                    <div class="checkbox-con">
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="">
                            <label class="checkbox"></label>
                        </span>
                    </div>
                </td>
                <td style="width: 10%" class="head1">
                    <em>广告id</em>
                </td>
                <td style="width: 31%">广告名称</td>
                <td style="width: 17%">广告形式</td>
                <td style="width: 14%">广告单价</td>
                <td style="width: 14%">投放范围</td>
                <td style="width: 14%">操作</td>
            </tr>
        </table>
        <div class="table-content">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tbody>
                   <%-- <tr>
                        <td style="width: 3%">
                            <div class="checkbox-con"><span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="checked"><label class="checkbox"></label></span></div>
                        </td>
                        <td style="width: 10%;"><em class="ad_id">63</em></td>
                        <td style="width: 31%">巧思科技直播APP 02</td>
                        <td style="width: 17%">横幅400-60</td>
                        <td style="width: 14%">0.2</td>
                        <td style="width: 14%">
                            <button class="range">+</button></td>
                        <td style="width: 14%" class="view-report"><a href="#" target="_blank">查看报表</a></td>
                    </tr>--%>
                    
                </tbody>
            </table>
            <%--<table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                <tbody>
                    <tr>
                        <td style="height: 20px; margin-top: 15px;" align="center">

                            <div class="manu" style="display: inline-block">
                                <span class="disabled">首页 </span><span class="disabled">上一页 </span><span class="current">1</span><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2">2</a><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2"> 下一页 </a><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2">尾页 </a>
                            </div>
                            <div class="pageInfoManu" style="display: inline-block">
                                共有<font style="color: red;">31</font>条&nbsp;&nbsp;<font style="color: red;">1</font>/<span>2</span>页
                       
                            </div>
                        </td>
                    </tr>

                </tbody>
            </table>--%>
        </div>

        <div class="btn">
            <button class="confirmT">确定</button>
            <button class="cancelT">取消</button>
        </div>
    </div>
    <div class="popup popup3" id="popup_range">
        <span style="display:none" class="adv_id">1</span>
        <div class="top">投放范围选择</div>
        <table class="clearfix" style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
           <%-- <tr>
                <td width="10%">
                    <div class="checkbox-con clearfix">
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="">
                            <label class="checkbox"></label>
                        </span>
                        <em>全部</em>
                    </div>
                </td>
                <td width="18%"></td>
                <td width="18%"></td>
                <td width="18%"></td>
                <td width="18%"></td>
                <td width="18%"></td>
            </tr>--%>
          
        </table>
        <div class="btn">
            <button class="rangeConfirm">确定</button>
            <button class="rangeRancel">取消</button>
        </div>
    </div>
    <div class="popup popup4 clearfix" id="adl_bid">
        <h4>竞价修改</h4>
        <span style="display:none" class="adl_id">1</span>
        <div class="checkbox-con" style="margin-left: 63px">
            <span class="wrap">
                <input type="radio" class="ipt-hide" checked="">
                <label class="checkbox cur"></label>
            </span>
            <em>是</em>
        </div>
        <div class="checkbox-con" style="margin-left: 47px">
            <span class="wrap">
                <input type="radio" class="ipt-hide" checked="">
                <label class="checkbox"></label>
            </span>
            <em>否</em>
        </div>
        <div class="popfour-btn">
            <button>确认</button>
            <button>取消</button>
        </div>
    </div>
    <div class="popup popup5 clearfix" id="adu_weight">
        <h4>权重修改</h4>
        <span style="display:none" class="adu_id">1</span>
        <div class="box">
            <div class="checkbox-con">
                <span class="wrap">
                    <input type="radio" class="ipt-hide" checked="">
                    <label class="checkbox cur"></label>
                </span>
                <em>Ⅰ级</em>
            </div>
            <div class="checkbox-con">
                <span class="wrap">
                    <input type="radio" class="ipt-hide" checked="">
                    <label class="checkbox"></label>
                </span>
                <em>Ⅱ级</em>
            </div>
            <div class="checkbox-con">
                <span class="wrap">
                    <input type="radio" class="ipt-hide" checked="">
                    <label class="checkbox"></label>
                </span>
                <em>Ⅲ级</em>
            </div>
            <div class="checkbox-con">
                <span class="wrap">
                    <input type="radio" class="ipt-hide" checked="">
                    <label class="checkbox"></label>
                </span>
                <em>Ⅳ级</em>
            </div>
            <div class="checkbox-con">
                <span class="wrap">
                    <input type="radio" class="ipt-hide" checked="">
                    <label class="checkbox"></label>
                </span>
                <em>Ⅴ级</em>
            </div>
        </div>

        <div class="popfour-btn">
            <button>确认</button>
            <button>取消</button>
        </div>
    </div>

</body>
<!--扩展内容 弹出效果 js等 -->
<script src="/page/js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
<script src="/page/js/manager/v2.0/ad_bidding.js"></script>
  <script>
       var pLevel='<%=permLevel %>';
        if (pLevel == "-1") {
            $(".topNewBtn").addClass("unclick");
            $(".advertising").addClass("unclick");
            $(".addAd").addClass("unclick");
            $(".topNewBtn").attr("disabled", true);
            $(".advertising").attr("disabled", true);
            $(".addAd").attr("disabled", true);
            $("#bid_adu table tr").each(function () {
                $(this).find("td").eq(5).find("a").remove();                
            })
            $("#bid_adl table tr").each(function () {
                $(this).find("td").eq(5).find("a").remove();
            })
            $("#bid_adv table tr").each(function () {
                $(this).find("td").eq(4).find("a").remove();
            })
        }
        else {
            $(".topNewBtn").removeClass("unclick");
            $(".advertising").removeClass("unclick");
            $(".addAd").removeClass("unclick");
        }
      //代理商竞价
      $("#bid_adu").find('.view a').click(function(){
          $("#adu_weight .adu_id").text($(this).closest("tr").find(".adu_id").text());         
          });
      
      $("#adu_weight .popfour-btn button").eq(0).click(function () {
          var aduid = $("#adu_weight .adu_id").text();
          if (aduid == "")
              return;
          var weight="";
          $("#adu_weight .box .checkbox-con").each(function () {
              if ($(this).find("label").hasClass("cur")) {
                  weight = $(this).index()+1;
                  return false;
              }
          })
          if (weight == "")
              return;
          var ajaxData = {
              method: "UpdateAduWeight",
              aduid: aduid,
              weight:weight
          };
          $.ajax({
              url: "/api/BidHandler.ashx",
              type: "post",
              data: ajaxData,
              dataType: "json",
              success: function (result) {
                  location.href = location.href;
              }
          });

      })
      //广告位竞价
      $("#bid_adl").find('.view a').click(function () {
          $("#adl_bid .adl_id").text($(this).closest("tr").find(".adl_id").text());         
      });
      $("#adl_bid .popfour-btn button").eq(0).click(function () {
          var adlid = $("#adl_bid .adl_id").text();
          if (adlid == "")
              return;
          var isbid = "";
          $("#adl_bid .checkbox-con").each(function () {
              if ($(this).find("label").hasClass("cur")) {
                  if ($(this).index() == 0)
                      isbid = "1";
                  else
                      isbid = "0";                 
                  return false;
              }
          })          
          if (isbid == "")
              return;
          var ajaxData = {
              method: "UpdateAdlBid",
              adlid: adlid,
              isbid:isbid
          };          ;
          $.ajax({
              url: "/api/BidHandler.ashx",
              type: "post",
              data: ajaxData,
              dataType: "json",
              success: function (result) {
                  location.href = location.href;
              }
          });

      })
      $("#bid_adl .advertising").click(function () {//竞价广告位管理弹出
          GetAdlByNoBidList();
      });     
      //获取未竞价广告位列表
      function GetAdlByNoBidList() {
          //分页待定
          var ajaxData = {
              method: "GetAdlListByNoBid"
          };
          $.ajax({
              url: "/api/BidHandler.ashx",
              type: "post",
              data: ajaxData,
              dataType: "json",
              success: function (result) {
                  if (result.errcode == "-1") alert(result.errmsg);
                  var listData = result.data;
                  $("#popup_adl .table-content table tbody tr").remove();
                  //物料信息  materialtype 
                  if (listData != null && listData.length>0) {
                      var trHtml = [];
                      $.each(listData, function (index, item) {
                          trHtml.push("<tr>");
                          trHtml.push("<td style='width: 3%'>");
                          trHtml.push("<div class='checkbox-con'><span class='wrap'>");
                          trHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'><label class='checkbox'></label></span></div>");
                          trHtml.push("</td>");
                          trHtml.push("<td style='width: 21%; padding: 0 4px'><em class='list_name'>" + item.bcode + "</em><span class='adl_id' style='display:none;'>" + item.adlocationid + "</span></td>");
                          trHtml.push("<td style='width: 20%'>" + item.adlocationname + "</td>");
                          trHtml.push("<td style='width: 18%'>" + item.pagename + "</td>");
                          trHtml.push("<td style='width: 14%'>" + item.subadtypename + "</td>");
                          trHtml.push("<td style='width: 14%'>" + item.alevel + "</td>");
                          trHtml.push("<td style='width: 10%'>" + item.isbid + "</td>");
                          trHtml.push("</tr>");
                      })
                      $("#popup_adl .table-content table tbody").append(trHtml.join(""));
                      $('#popup_adl .table-content').find('.checkbox').click(function (event) {
                          if ($(this).hasClass('cur')) {
                              $(this).removeClass('cur');
                          } else {
                              $(this).addClass('cur');
                          }
                          var _length1 = $('#popup_adl .table-content').find('.checkbox').length;
                          var _length2 = $('#popup_adl .table-content').find('.cur').length;
                          if (_length1 == _length2) {
                              $('#popup_adl .table-head').find('.checkbox').addClass('cur')
                          } else {
                              $('#popup_adl .table-head').find('.checkbox').removeClass('cur')
                          }
                          event.stopPropagation()
                      })

                      $("#popup_adl .confirmT").unbind("click");
                      $("#popup_adl .confirmT").click(function () {
                          var adData = new Array();
                          $('#popup_adl .table-content').find('.cur').each(function () {
                              var adlId = $(this).closest("tr").find(".adl_id").text();
                              adData.push(adlId);
                          })
                          if (adData.length > 0)
                              UpdateAdlIsBid("y", adData);
                          yyCommon.popoupHide($('.popup'));
                      })


                  }
                  else {
                      // alert(1);
                      //var _height=$("#popup_adl .table-content").css("height");
                      var trHtml = [];
                      trHtml.push("<tr>");
                      trHtml.push("<td><p style='width:100%; line-height:400px; text-align:center'>无数据...</p></td>");
                      trHtml.push("<tr>");
                      $("#popup_adl .table-content table tbody").append(trHtml.join(""));
                  }
              }
          });
      }
      //修改广告位是否竞价
      function UpdateAdlIsBid(inType, inData) {
          var ajaxData = { method: "UpdateAdlIsBid", updatetype: inType, data: JSON.stringify(inData) };

          $.ajax({
              url: "/api/BidHandler.ashx",
              type: "post",
              data: ajaxData,
              dataType: "json",
              success: function (result) {
                  if (result.errcode == "-1")
                      alert(result.errmsg);
                  else {
                      //alert(result.errmsg);
                      location.href = location.href;
                  }
              }
          });
      }
      $("#bid_adv .addAd").click(function () {//竞价广告管理弹出
          GetAdvListByNoBid();
      });
      //获取未竞价广告列表
      function GetAdvListByNoBid() {
          //分页待定
          var ajaxData = {
              method: "GetAdvListByNoBid"
          };
          $.ajax({
              url: "/api/BidHandler.ashx",
              type: "post",
              data: ajaxData,
              dataType: "json",
              success: function (result) {
                  if (result.errcode == "-1") alert(result.errmsg);
                  var listData = result.data;
                  $("#popup_adv .table-content table tbody tr").remove();
                  //物料信息  materialtype 
                  if (listData != null && listData.length > 0) {
                      var trHtml = [];
                      $.each(listData, function (index, item) {
                          trHtml.push("<tr>");
                          trHtml.push("<td style='width:3%'>");
                          trHtml.push("<div class='checkbox-con'><span class='wrap'>");
                          trHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'><label class='checkbox'></label></span></div>");
                          trHtml.push("</td>");
                          trHtml.push("<td style='width:10%;'><em class='ad_id'>"+item.adid+"</em></td>");
                          trHtml.push("<td style='width:31%'>"+item.adname+"</td>");
                          trHtml.push("<td style='width:17%'>"+item.subadtypename+"</td>");
                          trHtml.push("<td style='width:14%'>"+item.price+"</td>");
                          trHtml.push("<td style='width:14%'>");
                          trHtml.push("<span style='display:none' class='page_id'></span>");
                          trHtml.push("<span class='page_name'></span>");
                          trHtml.push("<button class='range'>+</button></td>");
                          trHtml.push("<td style='width:14%' class='view-report'><a href='/page/report/ad_data.aspx?adid="+item.adid+"' target='_blank'>查看报表</a></td>");
                          trHtml.push("</tr>");
                      })
                      $("#popup_adv .table-content table tbody").append(trHtml.join(""));
                      //选择
                      $('#popup_adv .table-content').find('.checkbox').click(function (event) {
                          if ($(this).hasClass('cur')) {
                              $(this).removeClass('cur');
                          } else {
                              $(this).addClass('cur');
                          }
                          var _length1 = $('#popup_adv .table-content').find('.checkbox').length;
                          var _length2 = $('#popup_adv .table-content').find('.cur').length;
                          if (_length1 == _length2) {
                              $('#popup_adv .table-head').find('.checkbox').addClass('cur')
                          } else {
                              $('#popup_adv .table-head').find('.checkbox').removeClass('cur')
                          }
                          event.stopPropagation()
                      })

                      //$("#popup_adv .confirmT").unbind("click");
                      //$("#popup_adv .confirmT").click(function () {
                      //    //var adData = new Array();
                      //    //$('#popup_adl .table-content').find('.cur').each(function () {
                      //    //    var adlId = $(this).closest("tr").find(".adl_id").text();
                      //    //    adData.push(adlId);
                      //    //})
                      //    //if (adData.length > 0)
                      //    //    UpdateAdlIsBid("y", adData);
                      //    alert(2);
                        
                      //})
                      //投放范围弹出
                      $("#popup_adv .range").click(function () {
                          yyCommon.popoupShow($('#popup_range'));
                          $('.loginmask3').fadeIn(400);
                          var adid = $(this).closest("tr").find(".ad_id").text();
                          GetPageListByBid(adid);
                      });
                      $(".loginmask3,#popup_range .btn").click(function () {
                          var k = !0;
                          $('#popup_range').animate({ top: -2000 }, 400, function () { $('#popup_range').hide(); k = !0 }),
                          $('.loginmask3').fadeOut(400)
                      });
                      

                  }
                  else {
                      // alert(1);
                      //var _height=$("#popup_adl .table-content").css("height");
                      var trHtml = [];
                      trHtml.push("<tr>");
                      trHtml.push("<td><p style='width:100%; line-height:400px; text-align:center'>无数据...</p></td>");
                      trHtml.push("<tr>");
                      $("#popup_adv .table-content table tbody").append(trHtml.join(""));
                  }
              }
          });
      }
      //竞价广告确认
      $("#popup_adv .confirmT").click(function () {
          alert(1);
          var bidList = new Array();
          $("#popup_adv tr").each(function () {
              var pageId= $(this).find(".page_id").text();
              var adId= $(this).find(".ad_id").text();
              if (pageId != "")
              {
                  var bidItem = { adid: adId, pageidstr: pageId };
                  bidList.push(bidItem);
              }
          })
          if (bidList.length > 0) {
              UpdateIsBidByAdList(bidList);
          }
          yyCommon.popoupHide($('.popup'));
      })
      function UpdateIsBidByAdList(bidList)
      {          
          var ajaxData = { method: "UpdateIsBidByAdList",  data:JSON.stringify(bidList) };

          $.ajax({
              url: "/api/BidHandler.ashx",
              type: "post",
              data: ajaxData,
              dataType: "json",
              success: function (result) {
                  if (result.errcode == "-1")
                      alert(result.errmsg);
                  else {
                      //alert(result.errmsg);
                      location.href = location.href;
                  }
              }
          });

      }

      //投放范围选择确认
      $("#popup_range .btn button").eq(0).click(function () {
          var adId = $("#popup_range .adv_id").text();
          var pageIdStr = "";
          var pageNameStr = "";
          //投放范围
          $("#popup_range td").each(function () {
              //判断是否选中
              if ($(this).find("label").hasClass("cur")) {
                  var pageId = $(this).find(".page_id").text();
                  if (pageId != "") {
                      //var putItem = { pid: TransDataToInt(pageId) };
                      //putRange.push(putItem);  
                      pageIdStr += pageId+",";
                      pageNameStr += $(this).find("em").text()+",";
                  }
              }
          })
          if (pageNameStr != "") {
              pageIdStr = pageIdStr.substr(0, pageIdStr.length - 1);
              pageNameStr = pageNameStr.substr(0, pageNameStr.length - 1);
              if (pageNameStr.length > 10)
              {
                  pageNameStr = pageNameStr.substr(0, 10) + "...";
              }
              $("#popup_adv tr").each(function () {
                  if (adId == $(this).find(".ad_id").text()) {

                      $(this).find(".page_name").text(pageNameStr);
                      $(this).find(".page_id").text(pageIdStr);
                      return false;
                  }
              })
          }
         

      })
          //根据广告获取页面列表
          function GetPageListByBid(adId) {
              //分页待定
              var ajaxData = {
                  method: "GetPageListByBid",
                  adid: adId
              };
              $.ajax({
                  url: "/api/BidHandler.ashx",
                  type: "post",
                  data: ajaxData,
                  dataType: "json",
                  success: function (result) {
                      if (result.errcode == "-1") alert(result.errmsg);
                      var listData = result.data;
                      $("#popup_range table tr").remove();
                      $("#popup_range .adv_id").text(adId);
                      //物料信息  materialtype 
                      if (listData != null && listData.length > 0) {
                          var trHtml = [];
                          var tempId;
                          var tdLength = 5;
                          var tempLength = 0;
                          trHtml.push("<tr>");
                          trHtml.push("<td width='10%'>");
                          trHtml.push("<div class='checkbox-con clearfix'>");
                          trHtml.push("<span class='wrap'>");
                          trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                          trHtml.push("<label class='checkbox'></label>");
                          trHtml.push("</span>");
                          trHtml.push("<em>全部</em>");
                          trHtml.push("</div>");
                          trHtml.push("</td>");
                          trHtml.push("<td width='18%'></td>");
                          trHtml.push("<td width='18%'></td>");
                          trHtml.push("<td width='18%'></td>");
                          trHtml.push("<td width='18%'></td>");
                          trHtml.push("<td width='18%'></td>");
                          trHtml.push("</tr>");

                          $.each(listData, function (index, item) {
                              tempLength++;
                              if(index==0||item.termid!=tempId)
                              {                                                          
                                  if (item.termid != tempId) {
                                      trHtml.push("</tr>");
                                      tempLength = 0;
                                  }
                                  trHtml.push("<tr>");
                                  trHtml.push("<td width='10%'>");
                                  trHtml.push("<div class='checkbox-con clearfix'>");
                                  //trHtml.push("<span class='wrap'>");
                                  //trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                                  //trHtml.push("<label class='checkbox'></label>");
                                  //trHtml.push("</span>");
                                  trHtml.push("<em>"+item.termname+"</em>");
                                  trHtml.push("</div>");
                                  trHtml.push("</td>");

                                  trHtml.push("<td width='18%'>");
                                  trHtml.push("<div class='checkbox-con clearfix'>");
                                  trHtml.push("<span class='wrap'>");
                                  trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                                  trHtml.push("<label class='checkbox'></label>");
                                  trHtml.push("</span>");
                                  trHtml.push("<em>" + item.pagename + "<span style='display:none;' class='page_id'>" + item.pageid + "</span></em>");
                                  trHtml.push("</div>");
                                  trHtml.push("</td>");

                              }
                              else
                              {
                                  if (tempLength >= tdLength) {
                                      trHtml.push("</tr>");
                                      trHtml.push("<tr>");
                                      trHtml.push("<td></td>");
                                      tempLength = 0;
                                  }
                                  trHtml.push("<td width='18%'>");
                                  trHtml.push("<div class='checkbox-con clearfix'>");
                                  trHtml.push("<span class='wrap'>");
                                  trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                                  trHtml.push("<label class='checkbox'></label>");
                                  trHtml.push("</span>");
                                  trHtml.push("<em>" + item.pagename + "<span style='display:none;' class='page_id'>" + item.pageid + "</span></em>");
                                  trHtml.push("</div>");
                                  trHtml.push("</td>");

                              }
                              //最后一个
                              if(index==listData.length-1)
                              {
                                  trHtml.push("</tr>");
                              }
                              tempId=item.termid;                                                    
                          })
                          $("#popup_range table").append(trHtml.join(""));
                      
                      }
                      else {
                          // alert(1);
                          //var _height=$("#popup_adl .table-content").css("height");
                          var trHtml = [];
                          trHtml.push("<tr>");
                          trHtml.push("<td><p style='width:100%; line-height:100px; text-align:center'>未找到有效数据</p></td>");
                          trHtml.push("<tr>");
                          $("#popup_range table").append(trHtml.join(""));
                      }
                      rangeChoice();
                  }
              });
          }

          //投放范围事件 选完之后把id和名称填充到广告列表
          function rangeChoice() {//广告页选择
              var all = $('#popup_range table tr:first-child').find('label'),
                   checkcon = $('#popup_range table').find('.checkbox');
              all.click(function () {
                  var other = $(this).parents('tr').nextAll();
                  if ($(this).hasClass('cur')) {
                      other.find('.checkbox').removeClass('cur')
                  } else {
                      other.find('.checkbox').addClass('cur')
                  }
              })
              checkcon.click(function () {
                  if ($(this).hasClass('cur')) {
                      $(this).removeClass('cur');
                  } else {
                      $(this).addClass('cur');
                  }
              })

          }
  </script>


</html>
