function autoHeight() {
    var winh = $(window).height() - 105;
    $('#YY-main').height(winh);
}
autoHeight();

//获取需要保存的数据，拼装json字符串

function GetJsonData() {
    var data = null;
    var materialtype = $("#materialtype").val();
    var name = "";
    var imageurl = "";
    var linkurl = "";
    var display = "";
    var ismark = "";
    var title = "";
    var remark = "";
    var popuptime = "";
    var confirmtext = "";
    var canceltext = "";
    var aduserId = 0;

    data = CheckData($("#wlmc").val(), "string", "物料名称不能为空！");
    if (data[1] == false) {
        return;
    } else {
        name = data[0];
    }
    if (materialtype == "1" || materialtype == "2" || materialtype == "3" || materialtype == "4" || materialtype == "5" || materialtype == "6") {
        data = CheckData($("#imageurl").val(), "string", "请上传图片或者填写远程链接！");
        if (data[1] == false) {
            return;
        } else {
            imageurl = data[0];
        }
    }
    data = CheckData($("#linkurl").val(), "string", "请上传安装包或者填写点击链接！");
    if (data[1] == false) {
        return;
    } else {
        linkurl = data[0];
    }
    data = CheckData($("#display").val(), "string", "请选择目标窗口！");
    if (data[1] == false) {
        return;
    } else {
        display = data[0];
    }
//    data = CheckData($("#ismark").val(), "string", "请选择是否显示广告！");
//    if (data[1] == false) {
//        return;
//    } else {
//        ismark = data[0];
    //    }
    ismark = 1;  
    if (materialtype == "2") {
        title = $("#infoflowtext").val();        
    }
    if (materialtype == "3" || materialtype == "4" || materialtype == "6") {
        data = CheckData($("#info_popuptime").val(), "string", "请填写弹出时间！");
        if (data[1] == false) {
            return;
        } else {
            popuptime = data[0];
        }
    }
    if (materialtype == "4") {
        confirmtext = $("#info_confirmtext").val();
        if (confirmtext == "") {
            confirmtext = "确定"
        }
        canceltext = $("#info_canceltext").val();
        if (canceltext == "") {
            canceltext = "取消"
        }
        
        data = CheckData($("#info_title").val(), "string", "请填写文字内容！");
        if (data[1] == false) {
            return;
        } else {
            title = data[0];
        }


    }
    if (materialid == "" || datatype == "AddMaterial") {
        if ($("#info_aduser .slide .cur").find("span").text() == "" || $("#info_aduser .slide .cur").find("span").text() == "all") {
            alert("请选择广告主");
            return;
        }
        aduserId = parseInt($("#info_aduser .slide .cur").find("span").text());
    }    
    var width = $("#tpcckd").val();
    var height = $("#tpccgd").val();
    var sizes = $("#sizes").val();
    if (sizes == "")
        sizes = 0;
    if (height == "")
        height = 0;
    if (width == "")
        width = 0;
    var format = $("#format").val();

    var data = {
        aduserid:aduserId,
        materialid: materialid,
        materialtype: materialtype,
        name: name,
        imageurl: imageurl,
        width: width,
        height: height,
        format: format,
        sizes: sizes,
        linkurl: linkurl,
        display: display,
        ismark: ismark,
        title: title,
        operationid: operationid,
        showtime: popuptime,
        remark: remark,
        confirmtext: confirmtext,
        canceltext:canceltext
    };
    GetMaterialId(data);
}

//判断物料ID是否存在
function GetMaterialId(data) {
    var materialid = $("#materialid").val();
    if (materialid == "" || datatype == "AddMaterial") {
        $.ajax({
            type: "POST",
            url: "../../api/Material/MaterialHand.ashx",
            data: {
                action: "GetMaterialId"
            },
            dataType: 'json',
            success: function (re) {
                if (re.errcode == 1) {
                    data.materialid = re.data;
                    SaveData(data);
                } else {

                }
            },
            error: function (e) {
                alert(JSON.stringify(e));
            }
        });
    } else {
        data.materialid = materialid;
        SaveData(data);
    }
}

//确认保存

function SaveData(data) {
    $.ajax({
        type: "POST",
        url: "/api/Material/MaterialHand.ashx",
        data: {
            action: "SaveMaterial",
            datatype: datatype,
            data: JSON.stringify(data)
        },
        dataType: 'json',
        success: function (re) {
            if (re.errcode == 1) {
                yyCommon.popoupHide();
                //alert(re.data);
                window.location.reload()
            } else {
                alert(re.errmsg);
            }
        },
        error: function (e) {
            alert(JSON.stringify(e));
        }
    });
}

//校验数据并返回

function CheckData(inVal, inType, errMsg) {
    var arr = new Array();
    var isOk = true;
    if (inType == "string") {
        if (inVal == undefined || inVal.trim() == "") {
            isOk = false;
            alert(errMsg);
        }
    }
    /*if (inType == "int") {
    if (inVal != undefined && inVal.trim() != "") {
    data= parseInt(inVal);
    }
    }*/

    arr.push(inVal);
    arr.push(isOk);
    return arr;
}

//取消

function Cancle() {
    yyCommon.popoupHide();
}

//新建物料

function addMaterial() {
    ShowMaterialPage();
    $("#selectMatType").show();
    ClearData();
    datatype = "AddMaterial";
    $('#selectMatType a').removeClass("on");
    $('#selectMatType a').eq(0).addClass('on');
    GetTabDefaultSet(1);
}
//tab默认设置
function GetTabDefaultSet(inType) {
    $('.popup').find('li').hide()
    $("#materialtype").val(inType);
    if (inType == 1) {
        $("#tpcckd").val("400");
        $("#tpccgd").val("60");
        $('.popup').find('.wlmc,.picFile,.size,.link,.targetW').show();
    }
    else if (inType == 2) {
        $("#tpcckd").val("172");
        $("#tpccgd").val("128");
        $('.popup').find('.wlmc,.picFile,.size,.textarea,.link,.targetW').show();
    }
    else if (inType == 3) {
        $('.popup').find('.wlmc,.picFile,.size,.link,.popupTime,.targetW').show();
    }
    else if (inType == 4) {
        $("#tpcckd").val("360");
        $("#tpccgd").val("270");
        $('.popup').find('.wlmc,li.title,.picFile,.size,.btnArea,.link,.popupTime,.targetW').show();
    }
    else if (inType == 5) {
        $("#tpcckd").val("1080");
        $("#tpccgd").val("1920");
        $('.popup').find('.wlmc,.picFile,.size,.link,.targetW').show();
    }
    else if (inType == 6) {
        $("#tpcckd").val("360");
        $("#tpccgd").val("270");
        $('.popup').find('.wlmc,.picFile,.size,.link,.targetW,.popupTime').show();
    }
}


//图片选择

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
    //转小写 toLowerCase()
    var fileName = name.substring(name.lastIndexOf(".") + 1).toLowerCase();
    if (id == "fileuploadimg") { //图片 15KB=15360B

        if (fileSize / 1024 > 30) {
            alert("请选择小于30K的图片！");
            target.value = "";
            return
        }
        if (fileName != "jpg" && fileName != "gif" && fileName != "png") {
            alert("请选择图片格式文件上传！");
            target.value = "";
            return
        }
        $("#sizes").val(parseInt(fileSize / 1024));
        $("#format").val(fileName);
        ajaxFileUpload(id, "pic");
    } else { //安装包：fileuploadapp  10240KB=10M
        if (fileSize / 1024 > 10240) {
            alert("请选择小于10M的安装包！");
            target.value = "";
            return
        }
        if (fileName != "apk") {
            alert("请选择apk格式文件上传！");
            target.value = "";
            return
        }
        ajaxFileUpload(id, "app");
    }

}

//Ajax实现本地上传

function ajaxFileUpload(contralid, type) {
    $.ajaxFileUpload({
        url: '../upload.aspx?type=' + type,
        //用于文件上传的服务器端请求地址
        secureuri: false,
        //一般设置为false
        fileElementId: contralid,
        //文件上传空间的id属性  <input type="file" id="file" name="file" />
        dataType: 'json',
        //返回值类型 一般设置为json
        success: function (data, status) //服务器成功响应处理函数
        {
            if (contralid == "fileuploadimg") {
                $(".uploadImg img").attr("src", data.fileurl);
                $("#imgprewurl").attr("src", data.fileurl);
                $("#imgtexturl").attr("src", data.fileurl);
                $("#imageurl").val(data.fileurl);
                
            } else {
                $("#linkurl").val(data.fileurl);
            }

            if (typeof (data.error) != 'undefined') {
                if (data.error != '') {
                    alert(data.error);
                } else { }
            }
        },
        error: function (data, status, e) //服务器响应失败处理函数
        {
            alert(e);
        }
    })
    return false;
}

//修改物料

function editMaterial(maid) {
    ShowMaterialPage(); //EditMaterial
    ClearData();
    
    datatype = "EditMaterial";
    //隐藏tab栏
    $("#selectMatType").hide();
    $.ajax({
        type: "POST",
        url: "../../api/Material/MaterialHand.ashx",
        data: {
            action: "EditMaterial",
            maid: maid
        },
        dataType: 'json',
        success: function (re) {
            if (re.errcode == 1) {
                //物料类型-banner
                if (re.data.materialtype == "1") {
                    $("#wlmc").val(re.data.name);
                    $("#imageurl").val(re.data.imageurl);
                    $("#linkurl").val(re.data.linkurl);
                    $("#tpcckd").val(re.data.width);
                    $("#tpccgd").val(re.data.height);
                    //弹窗选中样式
                    if (re.data.display == 1) {
                       
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displaynew").addClass("cur");
                        $("#display").val("1");
                    } else {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displayold").addClass("cur");
                        $("#display").val("2");
                    }
                    $(".uploadImg img").attr("src", re.data.imageurl);
                    GetTabDefaultSet(1);
                    //$('.popup').find('.wlmc,.picFile,.size,.link,.targetW').removeClass('hide');
                }
                //信息流
                else if (re.data.materialtype == "2") {
                    $("#wlmc").val(re.data.name);
                    $("#infoflowtext").val(re.data.title);
                    $("#imageurl").val(re.data.imageurl);
                    $("#tpcckd").val(re.data.width);
                    $("#tpccgd").val(re.data.height);
                    $("#linkurl").val(re.data.linkurl);
                    //弹窗选中样式
                    if (re.data.display == 1) {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displaynew").addClass("cur");
                        $("#display").val("1");
                    } else {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displayold").addClass("cur");
                        $("#display").val("2");
                    }
                    $(".uploadImg img").attr("src", re.data.imageurl);
                    GetTabDefaultSet(2);
                    //$('.popup').find('.wlmc,.picFile,.size,.textarea,.link,.targetW').removeClass('hide');

                }
                //悬浮
                else if (re.data.materialtype == "3") {
                    $("#wlmc").val(re.data.name);
                    $("#imageurl").val(re.data.imageurl);
                    $("#linkurl").val(re.data.linkurl);
                    $("#tpcckd").val(re.data.width);
                    $("#tpccgd").val(re.data.height);
                    //弹窗选中样式
                    if (re.data.display == 1) {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displaynew").addClass("cur");
                        $("#display").val("1");
                    } else {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displayold").addClass("cur");
                        $("#display").val("2");
                    }
                    $("#info_popuptime").val(re.data.showtime);
                    $(".uploadImg img").attr("src", re.data.imageurl);
                    GetTabDefaultSet(3);
                 //   $('.popup').find('.wlmc,.picFile,.size,.link,.popupTime,.targetW').removeClass('hide');
                }
                //弹窗
                else if (re.data.materialtype == "4") {
                    $("#wlmc").val(re.data.name);
                    $("#info_title").val(re.data.title);
                    $("#infoflowtext").val(re.data.remark);
                    $("#info_confirmtext").val(re.data.confirmtext);
                    $("#info_canceltext").val(re.data.canceltext);
                    $("#linkurl").val(re.data.linkurl);
                    $("#imageurl").val(re.data.imageurl);
                    $("#info_popuptime").val(re.data.showtime);
                    //弹窗选中样式
                    if (re.data.display == 1) {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displaynew").addClass("cur");
                        $("#display").val("1");
                    } else {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displayold").addClass("cur");
                        $("#display").val("2");
                    }
                    GetTabDefaultSet(4);
                 //   $('.popup').find('.wlmc,li.title,.textarea,.btnArea,.link,.popupTime,.targetW').removeClass('hide');
                }
                else if (re.data.materialtype == "5") {
                    $("#wlmc").val(re.data.name);
                    $("#imageurl").val(re.data.imageurl);
                    $("#linkurl").val(re.data.linkurl);
                    $("#tpcckd").val(re.data.width);
                    $("#tpccgd").val(re.data.height);
                    //弹窗选中样式
                    if (re.data.display == 1) {

                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displaynew").addClass("cur");
                        $("#display").val("1");
                    } else {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displayold").addClass("cur");
                        $("#display").val("2");
                    }
                    $("#info_popuptime").val(re.data.showtime);
                    $(".uploadImg img").attr("src", re.data.imageurl);
                    GetTabDefaultSet(5);
                    //   $('.popup').find('.wlmc,li.title,.textarea,.btnArea,.link,.popupTime,.targetW').removeClass('hide');
                }
                else if (re.data.materialtype == "6") {
                    $("#wlmc").val(re.data.name);
                    $("#imageurl").val(re.data.imageurl);
                    $("#linkurl").val(re.data.linkurl);
                    $("#tpcckd").val(re.data.width);
                    $("#tpccgd").val(re.data.height);
                    //弹窗选中样式
                    if (re.data.display == 1) {

                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displaynew").addClass("cur");
                        $("#display").val("1");
                    } else {
                        $("#displaynew").removeClass("cur");
                        $("#displayold").removeClass("cur");
                        $("#displayold").addClass("cur");
                        $("#display").val("2");
                    }
                    $("#info_popuptime").val(re.data.showtime);
                    $(".uploadImg img").attr("src", re.data.imageurl);
                    GetTabDefaultSet(6);
                    //   $('.popup').find('.wlmc,li.title,.textarea,.btnArea,.link,.popupTime,.targetW').removeClass('hide');
                }
                //其他
                else {
                    $("#textarea").show();
                    $("#imgandtext").show();
                    $("#imgonly").hide();
                    $("#imgtexturl").attr("src", re.data.imageurl);
                    $("#imgtext").text(re.data.title);
                    $("#infoflowtext").val(re.data.title);

                }

                $("#materialid").val(re.data.materialid);
                $("#sizes").val(re.data.sizes);
                $("#format").val(re.data.format);
                $("#display").val(re.data.display);
                $("#ismark").val(re.data.ismark);
                $("#materialtype").val(re.data.materialtype);


            } else {
                alert(re.errmsg);
            }
        },
        error: function (e) {
            alert(JSON.stringify(e));
        }
    });
}

//清空页面数据

function ClearData() {
    $("#wlmc").val("");
    $("#imageurl").val("");
    $("#infoflowtext").val("");
    $("#linkurl").val("");
    $("#imgprewurl").attr("src", "");
    $("#imgtext").text("");
    $("#imgtexturl").attr("src", "");
    $("#display").val("");
    $("#ismark").val("");
    $("#materialtype").val("");
    $("#tpcckd").val("");
    $("#tpccgd").val("");
    $("#materialid").val("");
    $("#sizes").val("");
    $("#format").val("");
    $("#info_title").val("");
    $("#info_confirmtext").val("");
    $("#info_canceltext").val("");
    $("#info_popuptime").val("");
    $("#display").val("1");
    //清除样式 imgupcheck
    $("#imgurlchack").removeClass("cur");
    $("#appcheck").removeClass("cur");
    $("#displaynew").removeClass("cur");
    $("#displayold").removeClass("cur");
    $("#ismarkok").removeClass("cur");
    $("#ismarkno").removeClass("cur");
    $("#appurlcheck").removeClass("cur");
    $("#imgupcheck").removeClass("cur");
    $("#displaynew").addClass("cur");
//    $("#fileapp").removeClass("on");
//    $("#fileimg").removeClass("on");

//    $("#textarea").hide();
//    $("#imgandtext").hide();
//    $("#imgonly").show();
   // $('.uploadImg').remove();

}

//显示物料编辑框

function ShowMaterialPage() {
    yyCommon.popoupShow();
}
$('.popup').find('.top a').click(function () {
    $(this).addClass('on').siblings().removeClass('on');
    if ($(this).index() == 0) {
        GetTabDefaultSet(1);
    }
    else if ($(this).index() == 1) {
        GetTabDefaultSet(2);
    }
    else if ($(this).index() == 2) {
        GetTabDefaultSet(3);
    }
    else if ($(this).index() == 3) {
        GetTabDefaultSet(4);
    }
    else if ($(this).index() == 4) {
        GetTabDefaultSet(5);
    }
    else if ($(this).index() == 5) {
        GetTabDefaultSet(6);
    }

});
//图片、信息流 切换  控制 文本框、图片预览  样式切换
//$('.popup').find('.top a').click(function () {
//    $(this).addClass('on').siblings().removeClass('on');
//    if ($(this).index() == 0) { //图片
//        $("#materialtype").val("1");
//        $("#textarea").hide();
//        $("#imgandtext").hide();
//        $("#imgonly").show();
//        $("#imgandtext").hide();
//        $("#imgonly").show();
//        $("#tpcckd").val("400");
//        $("#tpccgd").val("60");
//    } else if ($(this).index() == 1) { //信息流
//        $("#materialtype").val("2");
//        $("#textarea").show();
//        $("#imgandtext").show();
//        $("#imgonly").hide();
//        if (datatype == "EditMaterial") {
//            $("#imgonly").hide();
//            $("#imgandtext").show();
//        }
//        $("#tpcckd").val("172");
//        $("#tpccgd").val("128");
//    }
//    else if ($(this).index() == 2) { //信息流
//        $("#materialtype").val("3");
////        $("#textarea").show();
////        $("#imgandtext").show();
////        $("#imgonly").hide();
////        if (datatype == "EditMaterial") {
////            $("#imgonly").hide();
////            $("#imgandtext").show();
////        }
////        $("#tpcckd").val("172");
////        $("#tpccgd").val("128");
//    }
//    else if ($(this).index() == 3) { //信息流
//        $("#materialtype").val("4");
////        $("#textarea").show();
////        $("#imgandtext").show();
////        $("#imgonly").hide();
////        if (datatype == "EditMaterial") {
////            $("#imgonly").hide();
////            $("#imgandtext").show();
////        }
////        $("#tpcckd").val("172");
////        $("#tpccgd").val("128");
//    }
//})


//$('.link').find('.checkbox').click(function () {
//    $(this).addClass('cur').prev().attr('checked', 'checked').parents('.link').prev('.package').find('.ipt-hide').attr('checked', '').next().removeClass('cur');
//    $('.package').find('.upload').removeClass('on')
//    $(this).parents('.link').prev().find('.fileupload').addClass('forbidden')

//})
//$('.package').find('.checkbox').click(function () {//模拟目标窗口单选
//    $(this).addClass('cur').prev().attr('checked', 'checked').parents('.package').next('.link').find('.ipt-hide').attr('checked', '').next().removeClass('cur');
//    $('.package').find('.upload').addClass('on')
//    $(this).parents('.wrap').next().find('.fileupload').removeClass('forbidden')

//})

//显示方式动态 修改 样式 并 赋值
$('.targetW').find('.checkbox').click(function (e) {
    // $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
    var id = $(this)[0].id;
    if (id == "displayold") {
        $("#display").val("2");
    } else if (id == "displaynew") {
        $("#display").val("1");
    } else if (id == "ismarkok") {
        $("#ismark").val("1");
    } else if (id == "ismarkno") {
        $("#ismark").val("0");
    }

})

$('.edit').each(function () {
    var $h = $(this).parents('td').outerHeight() - 1;
    $(this).height($h).find('a').css('lineHeight', $h + 'px')
})
var $edit = $('.editBox').find('img');
$edit.click(function () {
    $(this).parents('.editBox').addClass('on');
})
$('.editBox .edit').hover(function () { }, function () {
    $(this).parents('.editBox').removeClass('on');
})

$(".popup .tab-bd,.tableBody").niceScroll({
    cursorcolor: "#12bdce",
    cursoropacitymin: 1,
    cursoropacitymax: 1,
    cursorwidth: "5px",
    cursorborder: "0",
    cursorborderradius: "5px",
    horizrailenabled: false
});