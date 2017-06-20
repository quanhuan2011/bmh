<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agent_edit.aspx.cs" Inherits="BMH.EagleEye.page.manager.agent_edit" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>广告投放管理

    </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">

    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务">
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。">
    <link rel="stylesheet" href="../css/agent-style.css">
    <script src="../js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.easing.min.js" type="text/javascript"></script>
    <style>
        #info_base a {
            text-decoration: none;
            color: #232323;
        }
    </style>
</head>
<body>

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
    <div class="w1220" id="allInfor">
        <div class="basic public clearfix" id="info_base">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <th colspan="20">基本资料</th>
                </tr>
                <tr>
                    <td width="40%" class="base_name"><span>代理商名称：</span><a></a></td>
                    <td width="60%" class="base_cname"><span>代理商公司：</span><a></a></td>
                </tr>
                <tr>
                    <td class="base_contact"><span>代理商联系人：</span><a></a></td>
                    <td class="base_tel"><span>代理商手机：</span><a></a></td>
                </tr>
                <tr>
                    <td class="base_account"><span>代理商账户：</span><a></a></td>
                    <td class="base_idno"><span>身份证号：</span><a></a></td>
                </tr>
                <tr>
                    <td class="base_email"><span>E-mail：</span><a></a></td>
                    <td class="base_address"><span>办公地址：</span><p><a></a></p>
                    </td>
                </tr>
                <tr>
                    <td class="base_regionname"><span>代理商区域：</span><a></a></td>
                    <td class="base_isparent"><span>挂靠状态：</span>未挂靠</td>
                </tr>
                <tr>
                    <td class="base_usertype"><span>代理商等级：</span><a></a><em>提升</em><em>下降</em></td>
                    <td></td>
                </tr>
            </table>
            <div class="btn-area">
                <button class="more">更多联系人</button>
                <button class="edit">编辑资料</button>
            </div>
        </div>
        <%-- <div class="rebates public">
        <table  cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <th>返点情况</th>
            </tr>
            <tr>
                <td><span>返点情况：</span>15%</td>
            </tr>
        </table> 
  
    </div>--%>
        <div class="aptitude" id="info_file">
            <div class="top">资质证明</div>
            <div class="middle">
                <div class="left clearfix file_idno">
                    <p class="title">代理商负责人身份证照片</p>
                    <img src="" height="152" width="240" alt="">
                    <span>身份证正面</span>
                    <div id="fileapp" class="upload">
                        修改图片                                             
                        <input type="file" id="fileuploadidno1" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        <span style="display: none;" class="file_fno"></span>
                    </div>
                    <img src="" height="152" width="240" alt="">
                    <span>身份证反面</span>
                    <div id="fileapp" class="upload">
                        修改图片                          
                   
                        <input type="file" id="fileuploadidno2" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        <span style="display: none;" class="file_fno"></span>
                    </div>
                </div>
                <div class="right clearfix file_license">
                    <p class="title">代理商营业执照</p>
                    <img src="" height="337" width="236" alt="">
                    <span>图片名称</span>
                    <div id="fileapp" class="upload">
                        修改图片                          
                   
                        <input type="file" id="fileuploadlicense" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        <span style="display: none;" class="file_fno"></span>
                    </div>
                </div>
            </div>
            <!--middle end-->
            <div class="bottom clearfix file_contract">
                <p class="picbox">合作合同照片</p>
                <ul>
                    <%--<li>
                        <img src="/page/test/showimg.aspx?fileno=035d5167-46a3-4acf-80ce-0e675b9c5c38" height="337" width="235" alt="">
                        <span>图片名称</span>
                        <div id="fileapp" class="upload">
                            修改图片                          
                       
                            <input type="file" id="fileuploadapp" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        </div>
                    </li>
                    <li>
                        <img src="../images/ht.jpg" height="337" width="235" alt="">
                        <span>图片名称</span>
                        <div id="fileapp" class="upload">
                            修改图片                          
                       
                            <input type="file" id="fileuploadapp" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        </div>
                    </li>
                    <li>
                        <img src="../images/ht.jpg" height="337" width="235" alt="">
                        <span>图片名称</span>
                        <div id="fileapp" class="upload">
                            修改图片                          
                       
                            <input type="file" id="fileuploadapp" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                        </div>
                    </li>--%>
                </ul>
            </div>
            <div class="addHT">
                <button>
                    新增合作合同                          
                   <input type="file" id="fileuploadadd" name="file" onchange="fileChange(this)" class="fileupload" size="100">
                </button>

            </div>
        </div>
        <div id="returnBack">
            <button>返回</button>
        </div>
    </div>
    <div class="loginmask"></div>
    <div class="popup" id="contacts">
        <button class="closebtn">x</button>
        <p>
            <button class="add">新增联系人</button>
            通讯录（普通代理）
       
        </p>
        <div class="tableHead">
            <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                <thead>
                    <tr>
                        <th width="24%">名称
                        </th>
                        <th width="24%">职位
                        </th>

                        <th width="24%">联系电话
                        </th>
                        <th width="28%">邮箱
                        </th>

                    </tr>
                </thead>
            </table>
        </div>
        <div class="tableBody" tabindex="0">
            <table class="datalist" cellpadding="0" cellspacing="0" border="0" width="100%">
                <tbody>
                    <%--<tr>
                        <td width="24%">赵三进</td>
                        <td width="24%">销售经理</td>
                        <td width="24%">158658/656</td>
                        <td width="28%">zhaosanjin@sina.com</td>

                    </tr>--%>
                </tbody>
            </table>
        </div>
        <div class="returnBack">
            <button>返回</button>
        </div>
    </div>
    <div class="popup2" id="add-contact">
        <button class="closebtn">x</button>
        <div class="box">
            <h4>新增联系人（普通代理）</h4>
            <ul>
                <li>
                    <span>名称:</span>
                    <div>
                        <input type="text">
                        <em>需要再2-10个字之间</em>
                    </div>

                </li>
                <li>
                    <span>职位:</span>
                    <div>
                        <input type="text">
                        <em>需要再2-10个字之间</em>
                    </div>

                </li>
                <li>
                    <span>联系方式:</span>
                    <div>
                        <input type="text">
                        <em>手机号码或者座机号码</em>
                    </div>

                </li>
                <li>
                    <span>E-mail:</span>
                    <div>
                        <input type="text">
                        <em></em>
                    </div>

                </li>

            </ul>

        </div>
        <div class="popbtn">
            <button>确认</button>
            <button>取消</button>
        </div>
    </div>
    <div class="popup3" id="info_edit">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tbody>
                <tr>
                    <th colspan="20">基本资料</th>
                </tr>
                <tr>
                    <td width="40%" class="edit_name"><span>代理商名称：</span><input type="text" value=""></td>
                    <td width="60%" class="edit_cname"><span>代理商公司：</span><input type="text" value=""></td>
                </tr>
                <tr>
                    <td class="edit_contact"><span>代理商联系人：</span><input type="text" value=""></td>
                    <td class="edit_tel"><span>代理商手机：</span><input type="text" value=""></td>

                </tr>
                <tr>
                    <td class="edit_account"><span>代理商账户：</span><input type="text" value=""></td>
                    <td class="edit_idno"><span>身份证号：</span><input type="text" value=""></td>
                </tr>

                <tr>
                    <td class="edit_email"><span>E-mail：</span><input type="text" value=""></td>
                    <td rowspan='2' class="edit_address"><span>办公地址：</span><textarea name=""></textarea></td>
                </tr>
                <tr>
                    <td class="edit_region">
                        <span>代理商区域：</span>
                        <div id="distpicker5" data-toggle="distpicker">
                            <select class="edit_province"></select>
                            <select class="edit_city"></select>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><span>挂靠状态：</span>未挂靠</td>
                    <td></td>
                </tr>
            </tbody>
        </table>
        <div class="popbtn">
            <button>确定</button>
            <button>取消</button>
        </div>
        <button class="closebtn">x</button>
    </div>
    <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="../js/manager/v2.0/agent_edit.js"></script>
    <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/ajaxfileupload.js" type="text/javascript"></script>
    <script>
        //读取资料
        function GetAdUserInfo(adUserId) {
            if (adUserId == "") {
                alert("无效id,请检查");
                return;
            }
            var ajaxData = {
                method: "GetAdUserData",
                aduserid: adUserId
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {
                    if (result.errcode != "1") return;
                    //基本资料
                    var infoData = result.data.infodata;
                    //图片资料
                    var fileData = result.data.filedata;
                    $(".base_account").find("a").text(infoData.username);
                    $(".base_name").find("a").text(infoData.name);
                    $(".base_cname").find("a").text(infoData.companyname);
                    $(".base_contact").find("a").text(infoData.contact);
                    $(".base_tel").find("a").text(infoData.tel);
                    $(".base_email").find("a").text(infoData.email);
                    $(".base_address").find("a").text(infoData.address);
                    //待修改
                    if (infoData.usertype == "1") {
                        $(".base_usertype").find("em").eq(0).hide();
                        $(".base_usertype").find("em").eq(1).show();
                    } else if (infoData.usertype == "2") {
                        $(".base_usertype").find("em").eq(0).show();
                        $(".base_usertype").find("em").eq(1).hide();
                    }
                    else {
                        $(".base_usertype").find("em").eq(0).hide();
                        $(".base_usertype").find("em").eq(1).hide();
                    }

                    $(".base_usertype").find("a").text(infoData.usertypename);
                    $(".base_regionname").find("a").text(infoData.regionname);
                    $(".base_idno").find("a").text(infoData.idno);

                    $.each(fileData, function (index, item) {
                        if (item.filetype == "1") {
                            if (item.fileseq == "1") {
                                $("#info_file .file_idno").find("img").eq(0).attr("src", "/page/test/showimg.aspx?fileno=" + item.fileno);
                                $("#info_file .file_idno").find(".file_fno").eq(0).text(item.fileno);
                            }
                            else {
                                $("#info_file .file_idno").find("img").eq(1).attr("src", "/page/test/showimg.aspx?fileno=" + item.fileno);
                                $("#info_file .file_idno").find(".file_fno").eq(1).text(item.fileno);
                            }
                        }
                        else if (item.filetype == "2") {
                            $("#info_file .file_license").find("img").attr("src", "/page/test/showimg.aspx?fileno=" + item.fileno);
                            $("#info_file .file_license").find(".file_fno").text(item.fileno);
                        }
                        else if (item.filetype == "3") {
                            var trHtml = [];
                            trHtml.push("<li>");
                            trHtml.push("<img src='/page/test/showimg.aspx?fileno=" + item.fileno + "' height='337' width='235' alt=''>");
                            trHtml.push("<span style='display: none;' class='file_fno'>" + item.fileno + "</span>");
                            trHtml.push("<span>合同细节" + item.fileseq + "</span>");
                            trHtml.push("<div id='fileapp' class='upload'>");
                            trHtml.push("修改图片");
                            trHtml.push("<input type='file' id='fileuploadcontract" + index + "' name='file' onchange='fileChange(this)' class='fileupload' size='100'>");
                            trHtml.push("</div>");
                            trHtml.push("</li>");
                            $("#info_file .file_contract ul").append(trHtml.join(''));
                        }
                        else {

                        }
                    })

                }
            });
        }
        var sAdUserId = getUrlVal("aduid");
        //alert(sAdUserId);
        GetAdUserInfo(sAdUserId);
        //修改资料
        $(".btn-area .edit").click(function () {

            $(".edit_account").find("input").val($(".base_account").find("a").text());
            $(".edit_name").find("input").val($(".base_name").find("a").text());
            $(".edit_cname").find("input").val($(".base_cname").find("a").text());
            $(".edit_contact").find("input").val($(".base_contact").find("a").text());
            $(".edit_tel").find("input").val($(".base_tel").find("a").text());
            $(".edit_email").find("input").val($(".base_email").find("a").text());
            $(".edit_address").find("textarea").val($(".base_address").find("a").text());
            //$("edit_name").find("input").val($(".base_regionname").find("a").text(infoData.regionname);
            $(".edit_idno").find("input").val($(".base_idno").find("a").text());
            var pronvinceId, cityId = "";
            GetProvinceList(pronvinceId);
            GetCityList("1", cityId);
            //if (pronvinceId == "")
            //    pronvinceId == "1";


        });

        //defaultVal 默认广告位id值,为空则第一个
        function GetProvinceList(provinceId) {
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
                        $("#info_edit .edit_province").html(selectHtml.join(''));
                    }
                }
            });

        }

        function GetCityList(provinceId, cityId) {
            var ajaxData = {
                method: "GetCityList",
                provinceid: provinceId
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
                            if (i == 0 || cityId != "") {
                                selectHtml.push("<option selected='selected' value=" + item.cityid + ">");
                            }
                            else {
                                selectHtml.push("<option value=" + item.cityid + ">");
                            }

                            selectHtml.push(item.cityname);
                            selectHtml.push("</option>");
                        })
                        $("#info_edit .edit_city").html(selectHtml.join(''));
                    }
                }
            });

        }
        $(".edit_province").change(function () {
            var provinceId = $(this).val();
            GetCityList(provinceId);
        })
        //确定保存
        function SaveBaseInfo() {
            var sName = $(".edit_name").find("input").val();
            var sCname = $(".edit_cname").find("input").val();
            var sContact = $(".edit_contact").find("input").val();
            var sTel = $(".edit_tel").find("input").val();
            var sEmail = $(".edit_email").find("input").val();
            var sAddress = $(".edit_address").find("textarea").val();
            var sRegionId = $(".edit_city").val();
            var sIdno = $(".edit_idno").find("input").val();

            var data = { aduid: sAdUserId, name: sName, cname: sCname, contact: sContact, tel: sTel, email: sEmail, address: sAddress, idno: sIdno, regionid: sRegionId };
            var ajaxData = {
                method: "SaveAdUserBaseInfo",
                data: JSON.stringify(data)
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {
                    if (result.errcode != "1") return;
                    location.href = location.href;
                }
            });

        }
        //保存基本资料
        $("#info_edit .popbtn").find("button").eq(0).click(function () {
            SaveBaseInfo();

        })

        $(".base_usertype em").click(function () {
            alert("此功能正在加紧开发中");
        })
        $("#info_base .more").click(function () {
            GetLinkManList();
        })
        //联系人
        function GetLinkManList() {

            var ajaxData = {
                method: "GetLinkManList",
                aduid: sAdUserId
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {
                    if (result.errcode != "1") return;
                    var linkManData = result.data.infodata;

                    if (linkManData != null) {
                        var trHtml = [];
                        $.each(linkManData, function (index, item) {
                            trHtml.push("<tr>");
                            trHtml.push("<td width='24%'>" + item.name + "</td>");
                            trHtml.push("<td width='24%'>" + item.position + "</td>");
                            trHtml.push("<td width='24%'>" + item.tel + "</td>");
                            trHtml.push("<td width='28%'>" + item.email + "</td>");
                            trHtml.push("</tr>");
                        })


                        $("#contacts .tableBody tbody").html(trHtml.join(""));
                    }
                }
            });
        }
        //新增联系人


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
            if (fileSize > (1024 * 1024 * 2)) {
                alert("请选择小于2M的图片！");
                target.value = "";
                return;
            }


            ajaxFileUpload(id);

        }
        //图片新增
        function InsertAdUserFile(aduserid, fileno, filetype) {
            var data = { fno: fileno, ftype: filetype, aduid: aduserid };
            var ajaxData = {
                method: "InsertAdUserFile",
                data: JSON.stringify(data)
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {

                }
            })
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
                    //var $thisparent = $("#" + contralid).parent();
                    //$thisparent.css('background', '#f6f6f6 url(/page/test/showimg.aspx?fileno=' + data.data + ') center center no-repeat');
                    //$thisparent.find(".file_no").text(data.data);
                    //代表新增
                    if (contralid == "fileuploadadd") {
                        var trHtml = [];
                        var index = $("#info_file .file_contract ul").find("li").length;
                        trHtml.push("<li>");
                        trHtml.push("<img src='/page/test/showimg.aspx?fileno=" + data.data + "' height='337' width='235' alt=''>");
                        trHtml.push("<span style='display: none;' class='file_fno'>" + data.data + "</span>");
                        trHtml.push("<span>合同细节</span>");
                        trHtml.push("<div id='fileapp' class='upload'>");
                        trHtml.push("修改图片");
                        trHtml.push("<input type='file' id='fileuploadcontract" + index + "' name='file' onchange='fileChange(this)' class='fileupload' size='100'>");
                        trHtml.push("</div>");
                        trHtml.push("</li>");
                        $("#info_file .file_contract ul").append(trHtml.join(''));
                        InsertAdUserFile(sAdUserId, data.data, "3");
                    }
                    else if (contralid == "fileuploadidno1") {
                        $("#info_file .file_idno").find("img").eq(0).attr("src", "/page/test/showimg.aspx?fileno=" + data.data);
                        var oldfileno = $("#info_file .file_idno").find(".file_fno").eq(0).text();
                        if (oldfileno == "")
                            InsertAdUserFile(sAdUserId, data.data, "1");
                        else
                            UpdateAdUserFile(sAdUserId, data.data, "1", oldfileno);
                    }
                    else if (contralid == "fileuploadidno2") {

                        $("#info_file .file_idno").find("img").eq(1).attr("src", "/page/test/showimg.aspx?fileno=" + data.data);
                        var oldfileno = $("#info_file .file_idno").find(".file_fno").eq(1).text();
                        if (oldfileno == "")
                            InsertAdUserFile(sAdUserId, data.data, "1");
                        else
                            UpdateAdUserFile(sAdUserId, data.data, "1", oldfileno);
                    }
                    else if (contralid == "fileuploadlicense") {
                        $("#info_file .file_license").find("img").attr("src", "/page/test/showimg.aspx?fileno=" + data.data);
                        var oldfileno = $("#info_file .file_license").find(".file_fno").eq(0).text();
                        if (oldfileno == "")
                            InsertAdUserFile(sAdUserId, data.data, "2");
                        else
                            UpdateAdUserFile(sAdUserId, data.data, "2", oldfileno);
                    }
                    else if (contralid.substring(0,"fileuploadcontract".length) == "fileuploadcontract") {
                        $("#" + contralid).closest("li").find("img").attr("src", "/page/test/showimg.aspx?fileno=" + data.data);
                        var oldfileno = $("#" + contralid).closest("li").find(".file_fno").eq(0).text();
                        if (oldfileno == "")
                            InsertAdUserFile(sAdUserId, data.data, "3");
                        else
                            UpdateAdUserFile(sAdUserId, data.data, "3", oldfileno);
                    }                        
                    else {
                        $("#" + contralid).closest("li").find("img").attr("src", "/page/test/showimg.aspx?fileno=" + data.data);
                    }                   
                    //保存对应关系
                }, error: function (data, status, e) //服务器响应失败处理函数
                {
                    alert(e);
                }
            })
            //return false;
        }

        //图片保存或者更新
        function UpdateAdUserFile(aduserid, fileno, filetype, oldfileno) {
            var data = { fno: fileno, ftype: filetype, aduid: aduserid, oldfno: oldfileno };
            var ajaxData = {
                method: "UpdateAdUserFile",
                data: JSON.stringify(data)
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {

                }
            })

        }

    </script>



</body>
</html>
