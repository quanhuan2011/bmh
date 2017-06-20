<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agent_info.aspx.cs" Inherits="BMH.EagleEye.page.manager.agent_info" %>


<!DOCTYPE html>
<!-- saved from url=(0053)http://yingyan.baomihua.com/page/manager/ad_info.aspx -->
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>代理商管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no, minimal-ui" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务">
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。">
    <link rel="stylesheet" href="../css/agent-style.css">
    <script src="../js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.easing.min.js" type="text/javascript"></script>
</head>
<body>
    <div id="wrap">
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
        <div id="YY-head">
            <div class="clearfix w1230">
                <div class="logo fl">
                </div>
                <div class="personInfo fr">
                    <div class="set-area">
                        <em>
                            <img src="../images/tips.png" height="7" width="20" alt=""></em>
                        <a href="/page/logout.aspx" class="exit">退出</a>
                    </div>
                    <p class="set fr">设置</p>
                    <span class="line fr">|</span>
                    <span class="name fr">管理员</span>
                    <img src="../images/head.jpg" height="51" width="51" alt="" class="head fr">
                </div>
            </div>
        </div>
    </div>
    <!-- YY-head  end -->
    <div class="w1230" id="YY-main" style="height: 836px;">
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
                        <dd class="cur">
                            <a href="/page/manager/agent_info.aspx">代理商管理</a>
                        </dd>
                         <dd>
                            <a href="/page/manager/bidding_info.aspx">竞价管理</a>
                        </dd>
                    </dl>
                </li>



            </ul>
        </div>
        <!--内容 -->

        <div class="contentSide fr">
            <div class="content">
                <h1 class="page-name">代理商管理</h1>
                <div class="top clearfix">
                    <button class="new fl">
                        新建代理商
                    </button>
                    <div class=" slidebox stateBox fl" id="info_region">
                        <span class="fl">区域： </span>
                        <div class="choice fl">
                            <div class="box">
                                <span class="title">全部
                                </span>
                                <div class="sanjiao">
                                    <span class="triangle"></span>
                                </div>
                            </div>
                            <div class="slide" style="width: 131px;">
                                <dl>
                                    <dd class="cur"><em>全部</em><span style="display: none">all</span></dd>
                                </dl>
                            </div>
                        </div>
                    </div>
                    <div class="slidebox adPage fl" id="info_type">
                        <span class="fl">代理级别： </span>
                        <div class="choice fl">
                            <div class="box">
                                <span class="title">全部
                                </span>
                                <div class="sanjiao">
                                    <span class="triangle"></span>
                                </div>
                            </div>
                            <div class="slide" style="width: 190px;">
                                <dl>
                                    <dd class="cur"><em>全部</em><span style="display: none">all</span></dd>
                                    <dd><em>区域总代</em><span style="display: none">1</span></dd>
                                    <dd><em>普通代理</em><span style="display: none">2</span></dd>
                                    <dd><em>广告主</em><span style="display: none">3</span></dd>
                                </dl>
                            </div>
                        </div>
                    </div>

                    <div class="search-wrap fr">
                        <button class="searchBtn">搜索</button>
                        <input type="text" name="" class="searchTxt" placeholder="请输入搜索内容">
                    </div>
                </div>
                <!-- top end -->
                <div class="tableHead">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <thead>
                            <tr>
                                <th width="20%">代理商编号
                                </th>
                                <th width="16%">代理商名称
                                </th>

                                <th width="16%">联系方式
                                </th>
                                <th width="16%">代理商区域
                                </th>
                                <th width="20%">代理级别（所属上级）
                                </th>
                                <th width="12%">操作
                                </th>

                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="tableBody" tabindex="0" style="overflow-y: hidden; outline: none;">
                    <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tbody>
                            <asp:Repeater ID="repMaterialList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td width="20%"><%#Eval( "aduserno") %></td>
                                        <td width="16%"><%#Eval( "adusername") %></td>
                                        <td width="16%"><%#Eval( "tel") %></td>
                                        <td width="16%"><%#Eval( "cityname") %></td>
                                        <td width="20%" class="per"><%#Eval( "aduserdesc") %></td>
                                        <td width="12%" class="view"><a href="agent_edit.aspx?aduid=<%#Eval("aduserid") %>" target="_blank">查看资料</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litNoInfo" runat="server">
                            </asp:Literal>
                        </tbody>
                    </table>
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="page">
                    <tbody>
                        <tr>
                            <td style="height: 20px; margin-top: 15px;" align="center">

                                <div class="manu" style="display: inline-block">
                                    <asp:Literal ID="litPage" runat="server">
                                    </asp:Literal>
                                </div>
                                <div class="pageInfoManu" style="display: inline-block">
                                    <asp:Literal ID="litPageInfo" runat="server">
                                    </asp:Literal>
                                </div>
                                <%--  <div class="manu" style="display:inline-block">
                            <span class="disabled"> 首页 </span><span class="disabled"> 上一页 </span><span class="current">1</span><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2">2</a><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2"> 下一页 </a><a href="http://yingyan.baomihua.com/page/manager/adlocation_info.aspx?page=2"> 尾页 </a> 
                        </div>
                        <div class="pageInfoManu" style="display:inline-block">
                            共有<font style="color: red;">31</font>条&nbsp;&nbsp;<font style="color: red;">1</font>/<span>2</span>页
                        </div>--%>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </div>

    </div>
    <div class="loginmask"></div>
    <div class="popup" id="newAgent">

        <div class="top">
        </div>
        <div class="main" id="info_base">
            <ul>
                <li class="base_name">
                    <span>代理商名称：</span>
                    <input type="text" placeholder="输入代理商简称（需要在2到10个字之间）">
                </li>
                <li class="base_cname">
                    <span>代理商公司：</span>
                    <input type="text" placeholder="输入代理商公司名称">
                </li>
                <li class="base_contact">
                    <span>代理商联系人：</span>
                    <input type="text" placeholder="输入一个主要负责人姓名">
                </li>
                <li class="base_tel">
                    <span>代理商联系电话：</span>
                    <input type="text" placeholder="输入一个主要负责人的手机号码" class="tel">
                </li>
                <li class="textarea base_address">
                    <span>代理商联系地址：</span>
                    <textarea name="" id="" cols="30" rows="10" placeholder="输入代理商的联系住址，公司地址或者现家庭住址"></textarea>
                </li>
                <li class="base_idno">
                    <span>身份证号：</span>
                    <input type="text" placeholder="输入18位有效身份证号" class="num">
                </li>
                <li>
                    <span>代理区域：</span>
                    <div id="distpicker5" data-toggle="distpicker">
                        <select class="base_province"></select>
                        <select class="base_city">                            
                        </select>
                    </div>
                    <div class="checkbox-con clearfix">
                        <span class="wrap" style="padding-left: 0;">
                            <input type="checkbox" class="ipt-hide" checked="checked">
                            <label class="checkbox cur"></label>
                        </span>
                        <em>是否靠挂</em>
                    </div>
                </li>
            </ul>
        </div>
        <!--第1步结束-->
        <div class="aptitude" id="info_file">
            <div class="middle">
                <div class="left clearfix file_idno">
                    <p class="title">代理商负责人身份证照片</p>
                    <div  class="upload" style="float: none; width: 240px; height: 152px; font-size: 14px; border: 1px solid #e7e7e7; background: #f0f0f0; color: #757575; line-height: 152px; margin-bottom: 0">
                        正面，点击上传                          
                    <input type="file" id="fileuploadidno1"  name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        <span style="display:none;" class="file_no"></span>
                    </div>
                    <span>身份证正面照，不得大于500K，图像清晰</span>

                    <div  class="upload" style="float: none; width: 240px; height: 152px; font-size: 14px; border: 1px solid #e7e7e7; background: #f0f0f0; color: #757575; line-height: 152px; margin-bottom: 0">
                        反面，点击上传                          
                    <input type="file" id="fileuploadidno2"  name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        <span style="display:none;" class="file_no"></span>
                    </div>
                    <span>身份证反面照，不得大于500K，图像清晰</span>

                </div>
                <div class="right clearfix file_license">
                    <p class="title">代理商营业执照</p>
                    <div class="upload" style="float: none; width: 236px; height: 335px; font-size: 14px; border: 1px solid #e7e7e7; background: #f0f0f0; color: #757575; line-height: 335px; margin-bottom: 0">
                        营业执照，点击上传                          
                    <input type="file" id="fileuploadlicense" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        <span style="display:none;" class="file_no"></span>
                    </div>
                    <span>图片不得大于800K，图像清晰</span>

                </div>
            </div>
            <!--middle end-->
            <div class="bottom clearfix">
                <p class="picbox">合作合同照片</p>
                <ul class="file_contract">
                    <li>
                        <div id="fileapp" class="upload" style="float: none; width: 235px; height: 335px; font-size: 14px; border: 1px solid #e7e7e7; background: #f0f0f0; color: #757575; line-height: 335px; margin-bottom: 0">
                            签订合同，点击上传                          
                    <input type="file" id="fileuploadcontract1" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                            <span style="display:none;" class="file_no"></span>
                        </div>
                        <span>图片名称</span>

                    </li>
                    <li>
                        <div id="fileapp" class="upload" style="float: none; width: 235px; height: 335px; font-size: 14px; border: 1px solid #e7e7e7; background: #f0f0f0; color: #757575; line-height: 335px; margin-bottom: 0">
                            签订合同，点击上传                          
                    <input type="file" id="fileuploadcontract2" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                            <span style="display:none;" class="file_no"></span>
                        </div>
                        <span>图片名称</span>

                    </li>
                    <li>
                        <div id="fileapp" class="upload" style="float: none; width: 235px; height: 335px; font-size: 14px; border: 1px solid #e7e7e7; background: #f0f0f0; color: #757575; line-height: 335px; margin-bottom: 0">
                            签订合同，点击上传                          
                    <input type="file" id="fileuploadcontract3" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                            <span style="display:none;" class="file_no"></span>
                        </div>
                        <span>图片名称</span>

                    </li>
                </ul>
            </div>


            <div class="addHT">
                <button>
                    新增合作合同                          
                </button>

            </div>
        </div>
        <!--第2步结束-->
        <div class="last" style="display: none" id="info_submit">
            <div class="box">
                <div>完成创建</div>
                <ul>
                    <li class="submit_name">
                        <span>代理商名称:</span>
                        <div>
                            
                        </div>
                    </li>
                    <li>
                        <span>代理商账号:</span>
                        <div>
                            <input type="text">
                            <em>输入10-30位数字或字母作为账号，不输入则使用默认账户</em>
                        </div>
                    </li>
                    <li>
                        <span>代理商密码:</span>
                        <div>
                            <input type="text">
                            <em>输入10-30位数字或字母作为密码，不输入则使用默认密码</em>
                        </div>
                    </li>

                </ul>

            </div>
        </div>
        <div class="popbutton">
            <button class="next nextOne">下一步</button>
            <button class="next nextTwo" style="display: none; background: #12bdcf">下一步</button>
            <button class="cancel">取消</button>
        </div>
    </div>
</body>
<!--扩展内容 弹出效果 js等 -->
     
<script src="../js/jquery/jquery.nicescroll.js"></script>
<script src="../js/manager/v2.0/agent_base.js"></script>
<script src="../js/manager/v2.0/ad-management.js"></script>
<script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/ajaxfileupload.js" type="text/javascript"></script>  

<script>

     var pLevel='<%=permLevel %>';
        if (pLevel == "-1") {
            $(".new").addClass("unclick");           
            $(".new").attr("disabled", true);           
        }
        else {
            $(".new").removeClass("unclick");           
        }
    //info_level
    var keyVal = getUrlVal("key");
    $(".searchTxt").val(keyVal);
    var sTypeVal = getUrlVal("stype");
    if (sTypeVal != "") {
        $("#info_type .slide dd").removeClass("cur");
        $("#info_type .slide dd").each(function () {
            if ($(this).find("span").text() == sTypeVal) {
                $(this).addClass("cur");
                $("#info_type").find(".title").text($(this).find("em").text());
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
    //类型
    $("#info_type .slide dd").click(function () {
        $(this).closest(".slide").find("dd").removeClass("cur");
        $(this).addClass("cur");
        var sTypeVal = $(this).find("span").text();
        var url = location.href;
        url = getMatchUrl(url, "page", "1");
        location.href = getMatchUrl(url, "stype", sTypeVal);
    })
</script>
<script>
   
    var fileNoList = new Array();
    $(".contentSide .new").click(function () {
        GetProvinceList();
        GetCityList(1);
    });
    $('.popup').on('click', '.nextOne', function (event) {

        //校验数据
        if (!CheckDataValid("代理商名称", $(".base_name input").val(), "1")) {
            return;
        }
        if (!CheckDataValid("代理商公司", $(".base_cname input").val(), "1")) {
            return;
        }
        if (!CheckDataValid("代理商联系人", $(".base_contact input").val(), "1")) {
            return;
        }
        if (!CheckDataValid("代理商联系方式", $(".base_tel input").val(), "1")) {
            return;
        }
        if (!CheckDataValid("身份证号", $(".base_idno input").val(), "1")) {
            return;
        }     
        $('#newAgent .top').css('background', '#f6f6f6 url(../images/lc2.jpg) center center no-repeat');
        $('#newAgent .main').hide()
        $('#newAgent .aptitude').show();
        $(this).hide();
        $('.nextTwo').show()
        $("#info_submit .submit_name div").text($("#info_base .base_name input").val()+"(普通代理)");
    })
    $('.popup').on('click', '.nextTwo', function (event) {
        //$('#newAgent .top').css('background','#f6f6f6 url(../images/lc3.jpg) center center no-repeat');
        //$('#newAgent .aptitude').hide();
        //$('#newAgent .last').show()
        //$('.popbutton').html('<button style="width:138px" class="complate">完成</button>')
    })
    $('.popup').on('click', '.complate', function (event) {
        //yyCommon.popoupHide();
        //$('#newAgent .last').hide();
        //$('#newAgent .main').show()
        //$('.popbutton').html('<button class="next nextOne">下一步</button><button class="next nextTwo" style="display:none;background:#12bdcf">下一步</button><button class="cancel">取消</button>')
        //$('#newAgent .top').css('background','#f6f6f6 url(../images/lc1.jpg) center center no-repeat');

        SaveData();
    })
    //上传图片
    function fileChange(target) {
        var fileSize = 0;
        var id = target.id;
        var name = target.value;

        if (!target.files) {
            var filePath = target.value;
            var fileSystem = new ActiveXObject("Scripting.FileSystemObject");
            var file = fileSystem.GetFile(filePath);
            fileSize = file.Size;
        } else {
            //获取文件大小  单位字节
            fileSize = target.files[0].size;
        }
        //判断文件格式
        var fileFormat = name.substring(name.lastIndexOf(".") + 1).toLowerCase();
        if (fileFormat != "jpg" && fileFormat != "gif" && fileFormat != "png") {
            alert("请选择图片格式文件上传！");
            target.value = "";
            return
        }
        //判断文件大小
        if (fileSize < (1024 * 10)) {
            alert("请选择大于10K的图片！");
            target.value = "";
            return;
        }
        if (fileSize > (1024 * 1024*2)) {
            alert("请选择小于2M的图片！");
            target.value = "";
            return;
        }


        ajaxFileUpload(id);

    }
    function ajaxFileUpload(contralid) {
        $.ajaxFileUpload({
            url: '/page/test/uploadimg.ashx',
            //用于文件上传的服务器端请求地址
            secureuri: false,
            //一般设置为false
            fileElementId: contralid,
            //文件上传空间的id属性  <input type="file" id="file" name="file" />
            dataType: 'json',
            //返回值类型 一般设置为json
            success: function (data, status) //服务器成功响应处理函数
            {
                if (data.errcode != "1") {
                    alert(data.errmsg); return;
                }
                var $thisparent = $("#" + contralid).parent();
                $thisparent.css('background', '#f6f6f6 url(/page/test/showimg.aspx?fileno=' + data.data + ') center center no-repeat');
                $thisparent.find(".file_no").text(data.data);
            }, error: function (data, status, e) //服务器响应失败处理函数
            {
                alert(e);
            }
        })
        //return false;
    }
    //保存数据
    function SaveData() {
        var fileList = new Array();
        var infoData, accountData;
        //基本资料
        var name = $("#info_base").find("ul li").eq(0).find("input").val();
        var cname = $("#info_base").find("ul li").eq(1).find("input").val();
        var contact = $("#info_base").find("ul li").eq(2).find("input").val();
        var tel = $("#info_base").find("ul li").eq(3).find("input").val();
        var address = $("#info_base").find("ul li").eq(4).find("textarea").val();
        var idno = $("#info_base").find("ul li").eq(5).find("input").val();
        var regionid = TransDataToInt($(".base_city").val());
        var operationid = 1;
        infoData = { name: name, cname: cname, contact: contact, tel: tel, address: address, idno: idno, regionid: regionid, operationid: operationid };

        //帐号
        var uName = $("#info_submit").find("ul li").eq(1).find("input").val();
        var pwd = $("#info_submit").find("ul li").eq(2).find("input").val();
        accountData = { uname: uName, pwd: pwd };
        //图片文件
        $("#info_file .file_idno").find(".upload").each(function (index) {
            var fileno = $(this).find(".file_no").text();           
            if(fileno!=""){
                var fileItem = { fno: fileno, ftype: 1, flevel: index + 1 };
                fileList.push(fileItem);
            }           
        })
        $("#info_file .file_license").find(".upload").each(function (index) {
            var fileno = $(this).find(".file_no").text();
            if (fileno != "") {
                var fileItem = { fno: fileno, ftype: 2, flevel: index + 1 };
                fileList.push(fileItem);
            }
        })
        $("#info_file .file_contract").find("li").each(function (index) {
            var fileno = $(this).find(".file_no").text();
            if (fileno != "") {
                var fileItem = { fno: fileno, ftype: 3, flevel: index + 1 };
                fileList.push(fileItem);
            }
        });       
        //组装
        var data = {
            infodata: infoData,
            filelist: fileList,
            accountdata: accountData
        };
        var ajaxData = {
            method: "InsertAdUserData",
            data: JSON.stringify(data)
        };
        $.ajax({
            url: "/api/ManagerHandler.ashx",
            type: "post",
            data: ajaxData,
            dataType: "json",
            success: function (result) {
                if (result.errcode == "-1") alert("没有执行成功，请稍后重试");
                else location.href = "agent_info.aspx";
            }
        });

    }
    
    //defaultVal 默认广告位id值,为空则第一个
    function GetProvinceList() {
        var ajaxData = {
            method: "GetProvinceList"           
        };
        $.ajax({
            url: "/api/ManagerHandler.ashx",
            type: "post",
            data: ajaxData,
            dataType: "json",
            success: function (result) {
                if (result.errcode == "1") {
                    var provinceList = result.data;
                    var tableHtml = [];
                    var selectHtml = [];
                    $.each(provinceList, function (i, item) {
                        if (i == 0) {
                            selectHtml.push("<option selected='selected' value=" + item.provinceid + ">");
                        }
                        else {
                            selectHtml.push("<option value=" + item.provinceid + ">");
                        }
                       
                        selectHtml.push(item.provincename);
                        selectHtml.push("</option>");
                       
                    })                   
                    $("#info_base .base_province").html(selectHtml.join(''));                  
                }
            }
        });

    }
   
    function GetCityList(provinceId) {
        var ajaxData = {
            method: "GetCityList",
            provinceid:provinceId
        };
        $.ajax({
            url: "/api/ManagerHandler.ashx",
            type: "post",
            data: ajaxData,
            dataType: "json",
            success: function (result) {
                if (result.errcode == "1") {
                    var cityList = result.data;
                    var tableHtml = [];
                    var selectHtml = [];
                    $.each(cityList, function (i, item) {
                        if (i == 0) {
                            selectHtml.push("<option selected='selected' value=" + item.cityid + ">");
                        }
                        else {
                            selectHtml.push("<option value=" + item.cityid + ">");
                        }
                        
                        selectHtml.push(item.cityname);
                        selectHtml.push("</option>");
                    })
                    $("#info_base .base_city").html(selectHtml.join(''));
                }
            }
        });

    }
    $(".base_province").change(function () {
        var provinceId = $(this).val();       
        GetCityList(provinceId);
    })


    //检验值是否有效
    function CheckDataValid(inKey, inVal, inLevel) {
        var levelArray = new Array();
        levelArray = inLevel.split(',');
        for (var i = 0; i < levelArray.length; i++) {
            if (levelArray[i].toString() == "1") {
                if (!CheckDataEmpty(inVal)) {
                    alert(inKey + "为空请填写");
                    return false;
                }
            }
            if (levelArray[i].toString() == "2") {
                if (!CheckDataInt(inVal)) {
                    alert(inKey + "不为数字请正确填写");
                    return false;
                }
            }
        }
        return true;
    }
    //检验数据是否为空
    function CheckDataEmpty(inVal) {
        if (inVal == null || inVal == "")
            return false;
        else
            return true;
    }
    //检验数据是否为数字
    function CheckDataInt(inVal) {
        if (!isNaN(inVal))
            return true;
        else
            return false;
    }
    //转换数据为int
    function TransDataToInt(inVal) {
        if (isNaN(inVal))
            return 0;
        else
            return parseInt(inVal);
    }
    

</script>
</html>
