<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/page/manager/MainPage.Master" CodeBehind="material_info.aspx.cs" Inherits="BMH.EagleEye.page.manager.material_info" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="Server">
    物料管理
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="SubContent" runat="Server">

    <div class="contentSide fr">
        <div class="content">
            <div class="top clearfix">
                <button onclick="addMaterial()" class="new fl unclick">新建广告物料</button>  
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
                
                       <div class="slidebox stateBox  fl" id="info_adtype">
                    <span class="fl">物料类型： </span>
                    <div class="choice fl">
                        <div class="box">
                            <span class="title">
                                <%=adTypeDefault%>
                            </span>
                            <div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide">
                            <dl>
                                <%=adTypeList%>                               
                            </dl>
                        </div>
                    </div>
                </div>                       
                <button id="btnSearch" class="searchBtn fr"">搜索</button>
                <input type="text" id="key" name="" class="searchTxt fr" placeholder="请输入物料名称" />
            </div>
            <!-- top end -->

            <div class="tableHead">
                <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                    <thead>
                        <tr>
                            <th style="width: 149px">广告物料名称</th>
                            <th style="width: 336px">广告物料预览</th>
                            <th style="width: 124px">尺寸</th>
                            <th style="width: 112px">类型</th>
                            <th style="width: 145px">上传时间</th>
                            <th style="width: 121px">操作人</th>
                            <th style="width: 96px">操作</th>
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
                                    <td style="width: 149px"><p><%#Eval("materialname")%></p><p style="color:#b8b8b8;">ID:<%#Eval("materialid")%></p></td>
                                    <%#GetmaterialPreview(Eval("materialtype").ToString(), Eval("title").ToString(), Eval("imageurl").ToString(), Eval("remark").ToString(), Eval("confirmtext").ToString(), Eval("canceltext").ToString())%>
                                    <td style="width: 124px"><%#Eval("sizepic")%></td>
                                    <td style="width: 112px"><%#Eval("materialtypename")%></td>
                                    <td style="width: 145px"><%#Eval("createtime")%></td>
                                    <td style="width: 121px"><%#Eval("username")%></td>
                                    <td class="editBox" style="width: 96px">
                                        <img src="../images/edit.png" height="18" width="18" alt="" />
                                        <div class="edit">
                                            <a href="#" onclick="editMaterial('<%#Eval("materialid") %>' )">修改</a>
                                            <%--<a href="" >复制</a>--%>
                                            <a href="../report/material_data.aspx?materialid=<%#Eval("materialid") %>" target="_blank">报告</a>
                                            <%--<a href="" >预览</a>--%>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        
                        </asp:Repeater>
                        <asp:Literal ID="litNoInfo" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>

            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                <tr>
                    <td style="height: 20px; margin-top: 15px;" align="center">
                        <div class="pageInfoManu">
                            <asp:Literal ID="litPageInfo" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 25px; margin-top: 5px;" align="center">
                        <div class="manu">
                            <asp:Literal ID="litPage" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- contentSide end -->
    <input type="hidden" id="hfAduserid" value="<%=hfAduserIdValue %>" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ExtendContent" runat="Server">
    <div class="loginmask"></div>
    <div class="popup">

        <div id="selectMatType" class="top clearfix tab-hd">
            <a id="typeimg" href="#" class="on">banner<span class="hide">1</span></a>
            <a id="typeinfo" href="#">信息流<span class="hide">2</span></a>
            <a href="#">悬浮式<span class="hide">3</span></a>
            <a href="#">弹层<span class="hide">4</span></a>
            <a href="#">开屏<span class="hide">5</span></a>
            <a href="#">前贴<span class="hide">6</span></a>
        </div>

        <div class="tab-bd clearfix">

            <!-- 物料-图片-开始-->

            <div class="content">
                <ul class="clearfix">

                    <li class="wlmc">
                        <label for="wlmc">物料名称：</label>
                        <input type="text" id="wlmc" class="textCounter" />
                        <span class='tip'>最多可输入100个字符</span>
                    </li>
                    <li class="title hide">
                        <label>标题：</label>
                        <input type="text" id="info_title" class="textCounter">
                    </li>
                    <li class="textarea hide">
                        <label for="tpwj">文字内容：</label>
                        <textarea name="" id="infoflowtext" cols="30" rows="10"></textarea>
                    </li>
                    <li class="btnArea clearfix hide">
                        <label class="fl">按键文本：</label>
                        <input class="fl btn1" id="info_confirmtext" type="text" placeholder="默认确认">
                        <input class="fl btn2" id="info_canceltext" type="text" placeholder="默认取消">
                    </li>
                    <li class="clearfix picFile">
                        <label for="tpwj" class="fl">图片文件：</label>
                        <div class="fl">
                            <input type="text" id="imageurl" class="txt fl">
                            <div id="fileimg" class="upload on">
                                点击上传                          
                          <!--图片文件上传：根据 物料类型 控制：尺寸、15K以内 -->
                                <input type="file" id="fileuploadimg" name="file" class="fileupload " onchange="fileChange(this)" accept="图片/png,gif,jpg" size="100" />
                            </div>
                            <span class='tip'>图片大小不超过15KB</span>

                        </div>

                    </li>

                    <li class="size clearfix">
                        <label class="fl">图片尺寸：</label>
                        <div class="wid fl">
                            <label for="tpcckd" class="fl">宽度</label>
                            <!--图片宽度：172 or 400 -->
                            <input class="fl" readonly="readonly" type="text" id="tpcckd" style="text-align: center;" value="400" /><span>px</span>
                        </div>
                        <div class="hei fl">
                            <!--图片高度：128 or 60 -->
                            <label for="tpccgd" class="fl">高度</label>
                            <input class="fl" readonly="readonly" type="text" id="tpccgd" style="text-align: center;" value="60" /><span>px</span>
                        </div>
                    </li>

                    <%--  <!--信息流文字 开始-->
            <li id="textarea" class="textarea" style="display:none;">
                <label for="tpwj">文字内容：</label>
                <textarea name="" id="infoflowtext" style="padding-top: 5px;" cols="30" rows="10"></textarea>
            </li>--%>
                    <!--信息流文字 结束-->


                    <li class="link clearfix">
                        <label for="djlj">点击链接：</label>
                        <input type="text" class="txt fl" id="linkurl">
                        <div id="fileapp" class="upload on">
                            安装包
                        
                        <!--安装文件上传：控制：大小 10M以内 -->
                            <input type="file" id="fileuploadapp" name="file" onchange="fileChange(this)" class="fileupload" size="100" />

                        </div>
                    </li>
                    <li class="popupTime hide">
                        <label class="fl">弹出时间：</label>
                        <input type="text" id="info_popuptime" class="txt fl txt-right">
                        <span>秒后弹出</span>
                    </li>

                    <li class="targetW">
                        <label for="mbck">目标窗口：</label>
                        <div class="checkbox-con">
                            <span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="" />
                                <!--选择样式添加 cur -->
                                <label id="displayold" class="checkbox"></label>
                            </span>
                            <em>原窗口</em>
                        </div>
                        <div class="checkbox-con">
                            <span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="" />
                                <!--选择样式添加 cur -->
                                <label id="displaynew" class="checkbox cur"></label>
                            </span>
                            <em>新窗口</em>
                        </div>
                    </li>


                </ul>
                <!--隐藏域 开始-->
                <input id="display" type="hidden" />
                <input id="ismark" type="hidden" />
                <input id="materialtype" type="hidden" value="1" />
                <input id="materialid" type="hidden" />
                <input id="sizes" type="hidden" value="0" />
                <input id="format" type="hidden" value="png" />
                <!--隐藏域 结束-->


            </div>

            <!-- 物料-图片-结束-->

        </div>
        <!--确认or取消 开始-->
        <div class="btn">
            <button id="btnSave" onclick="GetJsonData()" class='confirm'>确定</button>
            <button onclick="Cancle()" class='cancel'>取消</button>
        </div>
        <!--确认or取消 结束-->
        <div class="uploadImg">
            <img src="../images/pic1.png" alt=""></div>

    </div>
    <script src="../js/base/v2.0/ajaxfileupload.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="../js/manager/v2.0/comon.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/page.base.js"></script>
    <script type="text/javascript">
        var pLevel='<%=permLevel %>';
        if (pLevel == "-1") {
            $(".content .new").addClass("unclick");
            $(".content .new").attr("disabled", true);
            $(".tableBody table tr").each(function () {
                $(this).find(".editBox .edit").find("a").eq(0).remove();
            })
        }
        else {
            $(".content .new").removeClass("unclick");
        }
        var operationid = '<%= strBindAccountId %>';
        //新增 或 修改
        var datatype = "";

        //搜索
        function SearchDataByKey() {
            material_info.GetSearchUrl($('#key').val(), operationid, f_srarchback);
        }

        /*监听输入框的回车操作*/
        $('#key').bind('keypress', function (event) {
            if (event.keyCode == "13") {
                material_info.GetSearchUrl($('#key').val(), operationid, f_srarchback);
            }
        });

        //回调函数
        function f_srarchback(re) {
            window.location.href = re.value;
        }

        //搜索
        $(".searchBtn").click(function () {
            var searchVal = $(this).parent().find(".searchTxt").val();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "key", searchVal);
        })
        //菜单列表选中样式
        $(".leftSide dd").eq(3).addClass("cur");
        //广告主
        $("#info_aduser .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var tempVal = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "aduid", tempVal);
        })
        //物料类型
        $("#info_adtype .slide dd").click(function () {
            $(this).closest(".slide").find("dd").removeClass("cur");
            $(this).addClass("cur");
            var tempVal = $(this).find("span").text();
            var url = location.href;
            url = getMatchUrl(url, "page", "1");
            location.href = getMatchUrl(url, "mattype", tempVal);
        })
        var keyVal = getUrlVal("key");
        $(".searchTxt").val(keyVal);
        var aduIdVal = getUrlVal("aduid");
        if (aduIdVal != "") {
            $("#info_aduser .slide dd").removeClass("cur");
            $("#info_aduser .slide dd").each(function () {
                if ($(this).find("span").text() == aduIdVal) {
                    $(this).addClass("cur");
                    $("#info_aduser").find(".title").text($(this).find("em").text());
                }
            })
        }
        var matTypeVal = getUrlVal("mattype");
        if (matTypeVal != "") {
            $("#info_adtype .slide dd").removeClass("cur");
            $("#info_adtype .slide dd").each(function () {
                if ($(this).find("span").text() == matTypeVal) {
                    $(this).addClass("cur");
                    $("#info_adtype").find(".title").text($(this).find("em").text());
                }
            })
        }
    </script>
    <script src="../js/manager/v2.0/material.Manger.js" type="text/javascript"></script>

</asp:Content>
