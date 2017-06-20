<%@ Page Title="" Language="C#" MasterPageFile="~/page/users/UsersPage.Master" AutoEventWireup="true" CodeBehind="recharge_info.aspx.cs" Inherits="BMH.EagleEye.page.users.recharge_info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
充值记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubContent" runat="server">
<div class="user-main fr">
        <div class="content">
            <div class="top clearfix">
                <%--<button class="fl">返回</button>  --%>  
                <span>用户名：<%=adUser.name%></span>          
            </div>
            <!-- top end -->
            <div class="tableHead operationLog">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                      <tr>
                        <th width="10%">充值编号</th>
                        <th width="65%">充值金额</th>
                        <th width="25%">充值时间</th>
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
                          <td width="10%"><%#Eval("detailid") %></td>
                          <td width="65%"><%#Eval("money") %></td>
                          <td width="25%"><%#Eval("createtime") %></td>
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
    <script src="../js/base/v2.0/demo.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script>
        $(".leftSide li").eq(1).addClass("on"); //菜单列表选中样式
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
     </script>
</asp:Content>
