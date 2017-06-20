<%@ Page Title="" Language="C#" MasterPageFile="~/page/users/UsersPage.Master" AutoEventWireup="true" CodeBehind="addeduct_info.aspx.cs" Inherits="BMH.EagleEye.page.users.addeduct_info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
广告消费明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubContent" runat="server">

<div class="user-main fr">
        <div class="content">
            <div class="top clearfix">
                 
                <span>用户名：<%=adUser.name %></span>

               <%-- <button class="searchBtn fr">搜索</button>
                <input type="text" name="" class="searchTxt fr" placeholder="请输入广告主或名称">--%>
                <div class="dateBox open fr"><input id="date-range0" size="30" value="2016-10-10 至 2016-10-17"></div>             
            </div>
            <!-- top end -->
            <div class="tableHead operationLog">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                      <tr>
                        <th width="10%">广告编号</th>
                        <th width="40%">广告名称</th>
                        <th width="25%">点击次数/下载次数</th>
                        <th width="25%">消费金额</th>
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
                          <td width="10%"><%#Eval("adid") %></td>
                          <td width="40%"><%#Eval("adname") %></td>
                          <td width="25%"><%#Eval("clickcnt") %></td>
                          <td width="25%"><%#Eval("deductsum")%>元</td>
                        </tr>
                         </ItemTemplate>                        
                        </asp:Repeater>
                        <asp:Literal ID="litNoInfo" runat="server">
                        </asp:Literal>                           
                      </tbody>
                  </table>

            </div>
              <div class="data-output">
          <span>总消费金额：<%=deductSum %></span>
        </div> 
               <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                    <tr>
                        <td style="height:20px;margin-top: 15px;" align="center">
                            <div class="pageInfoManu">
                                <asp:Literal ID="litPageInfo" runat="server">
                                </asp:Literal>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height:25px;margin-top: 5px;" align="center">
                            <div class="manu">
                                <asp:Literal ID="litPage" runat="server">
                                </asp:Literal>
                            </div>
                        </td>
                    </tr>
                </table>
            
        </div>
            
      </div>

    <link href="../css/page.css" rel="stylesheet" type="text/css" />
    <script src="../js/users/v2.0/users.base.js" type="text/javascript"></script>
    <script src="../js/moment/moment.min.js" type="text/javascript"></script>
    <script src="../js/users/v2.0/jquery.daterangepicker.users.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/demo.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>     
    <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
     <script>
         $(".leftSide li").eq(2).addClass("on"); //菜单列表选中样式
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
         $(".searchBtn").click(function () {
             var searchVal = $(this).parent().find("input").val();
             var url = location.href;
             location.href= getMatchUrl(url, "key", searchVal);
         })

         var startTime = getUrlParam("starttime");
         var endTime = getUrlParam("endtime");
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
     </script>
   
</asp:Content>
