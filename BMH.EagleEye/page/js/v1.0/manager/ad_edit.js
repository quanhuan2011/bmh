var directArray = new Array();
var matchArray = new Array();

function SetOthersDirect(directTypeId, directData, matchData) {
    //清空数据
    $(".system").find("p").remove();
    $(".already").find("p").remove();
    //左侧全部列表
    $.each(directData, function (index, item) {
        if (directTypeId == item.directtypeid) {
            var directTypeData = item.directtypedata;
            $.each(directTypeData, function (k, p) {
                var pHtml = "<p><span style='display:none'>" + p.directid + "</span><em>" + p.directname + "</em></p>";
                $(".system").append(pHtml);
            })
        }
    })
    //右侧匹配列表
    $.each(matchData, function (index, item) {
        if (directTypeId == item.directtypeid) {
            var directTypeData = item.directtypedata;
            $.each(directTypeData, function (k, p) {
                var pHtml = "<p><span style='display:none'>" + p.directid + "</span><em>" + p.directname + "</em><em class='del'></em></p>";
                $(".already").append(pHtml);
                $(".system p").each(function () {
                    if ($(this).find("span").text() == p.directid) {
                        $(this).addClass("on");
                    }
                })
            })
        }
    })
    //广告物料选择
    var $system = $('.otherSet').find('.system'),
             $already = $('.otherSet').find('.already');
    var flag = 0;
    $system.find('p').click(function () {
        var txt = $(this).find("em").text();
        var strId = $(this).find("span").text();
        if ($(this).hasClass('on')) {
            return false;
        }
        $already.append('<p class="on"><span style="display:none">' + strId + '</span><em>' + txt + '</em><em class="del"></em></p>');
        $(this).addClass('on');

    })
    $already.on('click', '.del', function () {
        $(this).parent().remove();
        var strId = $(this).parent().find("span").text();
        $system.find('p').each(function () {
            if ($(this).find("span").text() == strId) { $(this).removeClass('on') }
        })
    })
}
//获取广告信息
function GetAdInfo(adId) {
    if (adId == "") {
        alert("无效id,请检查");
        return;
    }
    var ajaxData = {
        userid: "",
        adid: adId
    };
    $.ajax({
        url: "../../api/Manager.asmx/GetAdInfo",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode != "1") return;
            var infoData = result.data.infodata;
            //var systemData = result.data.systemdata;
            var materialData = result.data.materialdata;
            //var othersData = result.data.othersdat
            var directData = result.data.directdata;
            var matchData = result.data.matchdata;
            //遍历列表 三个 并选中成当前
            //页面列表
            $("#info_page .slide dd").removeClass("cur");
            $("#info_page .slide dd").each(function () {
                // $(this).removeClass("cur");
                if ($(this).find("span").text() == infoData.pageid) {
                    $(this).addClass("cur");
                    $("#info_page").find(".title").text(infoData.pagename);
                }
            })
            //广告位列表
            $("#info_adlocation .slide dd").removeClass("cur");
            $("#info_adlocation .slide dd").each(function () {
                if ($(this).find("span").text() == infoData.adlocationid) {
                    $(this).addClass("cur");
                    $("#info_adlocation").find(".title").text(infoData.adlocationname);
                }
            })
            //广告类型列表
            $("#info_adtype .slide dd").removeClass("cur");
            $("#info_adtype .slide dd").each(function () {
                if ($(this).find("span").text() == infoData.adtypeid) {
                    $(this).addClass("cur");
                    $("#info_adtype").find(".title").text(infoData.adtypename);
                }
            })
            //定向设置列表
            if (matchData != null) {
                $(".oslimit").find("label").click();
                $("#info_others .slide dd").removeClass("cur");
                $("#info_others .slide dd").each(function () {
                    if ($(this).find("span").text() == matchData[0].directtypeid) {
                        $(this).addClass("cur");
                        $("#info_others").find(".title").text(matchData[0].directtypename);
                    }
                })
                $("#info_others .slide dd").unbind("click");
                $("#info_others .slide dd").click(function () {
                    var $txt = $(this).find("em").text();
                    $(this).parents('.slide').prev().find('.title').text($txt);
                    $(this).parents('.slide').slideUp();
                    $('.triangle').removeClass('open');
                    var directTypeId = $(this).find("span").text();
                    SetOthersDirect(directTypeId, directData, matchData);
                })
                //匹配列表
                var directTypeId = directData[0].directtypeid;
                SetOthersDirect(directTypeId, directData, matchData);
                directArray = directData;
                matchArray = matchData;

            } else {
                $(".nonelimit").find("label").click();
            }

            $(".ad-name").val(infoData.adname);
            $(".ad-price").val(infoData.price);
            //投放时间
            if (infoData.putstarttime != "") $("#starttime input").val(infoData.putstarttime);
            if (infoData.putendtime != "") {
                $("#endtimeckbox label").click();
                $("#endtime input").val(infoData.putendtime);
            }
            //投放量
            $(".limitmax").find("input").eq(0).click();
            if (infoData.putmax != "") {
                $(".limitmax").find("input").eq(0).click();
                $(".limitmax").find("input").eq(1).val(infoData.putmax);
            }
            //投放量每日
            if (infoData.putmaxbyday != "") {
                $(".limitmaxbyday").find("input").eq(0).click();
                $(".limitmaxbyday").find("input").eq(1).val(infoData.putmaxbyday);
            }

            //物料信息  materialtype 
            if (materialData != null) {
                $.each(materialData, function (index, item) {
                    $("#info_weight input").val(item.weight);
                    if (item.materialtype == "1") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='img' width='258'><img src='" + item.imageurl + "' height='71' width='235' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");
                        trHtml.push("<td>" + item.format + "</td>");
                        trHtml.push("<td width='150'><button  class='list_removebtn'>移除</button></td>");
                        trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                        trHtml.push("</tr>");
                        $(".materialtable").append(trHtml.join(""));
                    } else {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");
                        trHtml.push("<td>" + item.format + "</td>");
                        trHtml.push("<td width='150'><button class='list_removebtn'>移除</button></td>");
                        trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                        trHtml.push("</tr>");
                        $(".materialtable").append(trHtml.join(""));
                    }

                    $(".list_removebtn").click(function () {
                        $(this).closest("tr").remove();
                    })
                })
                var $option = ''; //权重select赋值
                for (var i = 1; i < 10; i++) {
                    $option += '<option value="' + i + '">' + i + '</option>'
                }
                $('.setMaterial .three .weight').html($option)
            }


        }

    })
}
//新增广告

function InsertData(status) {
    //页面列表
    var sPageId = $("#info_page .cur").find("span").text(); //$()
    //1不为空，2为数字
    if (!CheckDataValid("页面列表", sPageId, "1,2")) {
        return;
    }
    //广告位
    var sAdLocationId = $("#info_adlocation .cur").find("span").text();
    if (!CheckDataValid("广告位", sAdLocationId, "1,2")) {
        return;
    }
    //广告类型
    var sAdTypeId = $("#info_adtype .cur").find("span").text();
    if (!CheckDataValid("广告类型", sAdTypeId, "1,2")) {
        return;
    }
    //广告名称
    var sAdName = $("#info_adname").val();
    if (!CheckDataValid("广告名称", sAdName, "1")) {
        return;
    }
    //价格
    var sPrice = $("#info_price").val();
    if (!CheckDataValid("价格", sPrice, "1,2")) {
        return;
    }
    //推送开始时间
    var sPutStartTime = $("#some_class_1").val();
    if (!CheckDataValid("推送开始时间", sPutStartTime, "1")) {
        return;
    }
    //推送结束时间
    var sPutEndTime = "";
    if ($("#endtimeckbox label").hasClass("cur")) {
        sPutEndTime = $("#some_class_2").val();
        if (!CheckDataValid("推送结束时间", sPutEndTime, "1")) {
            return;
        }
    }
    //投放上限
    var sPutMax = "";
    if ($("#info_putmax input[type='checkbox']").attr("checked")) {
        sPutMax = $("#info_putmax input[type='text']").val();
        if (!CheckDataValid("投放上限", sPutMax, "1,2")) {
            return;
        }
    }
    //投放上限每日
    var sPutMaxByDay = "";
    if ($("#info_putmaxbyday input[type='checkbox']").attr("checked")) {
        sPutMaxByDay = $("#info_putmaxbyday input[type='text']").val();
        if (!CheckDataValid("投放上限每日", sPutMaxByDay, "1,2")) {
            return;
        }
    }
    //转换数据
    sPageId = TransDataToInt(sPageId);
    sAdLocationId = TransDataToInt(sAdLocationId);
    sAdTypeId = TransDataToInt(sAdTypeId);
    sPrice = TransDataToInt(sPrice);

    //基本信息
    var infoData = {
        pageid: sPageId,
        adlocationid: sAdLocationId,
        adtypeid: sAdTypeId,
        adname: sAdName,
        price: sPrice,
        putstarttime: sPutStartTime,
        putendtime: sPutEndTime,
        putmax: sPutMax,
        putmaxbyday: sPutMaxByDay,
        status: status
    };
    //物料信息
    var materialData = new Array();
    var materialBool = true;
    $(".materialtable tr").each(function () {
        var materialid = $(this).find(".materialid").text()
        if (!CheckDataValid("物料信息", materialid, "1,2")) {
            materialBool = false;
            return;
        }
        materialid = TransDataToInt(materialid);
        var weighttype = $("#info_weighttype .cur").find("span").text();
        var weight = "";
        if (weighttype == "1") {
            weight = "1";
        }
        else {
            weight = $(this).find(".manual").find("select").val();
        }
        var materialItem = {
            materialid: materialid,
            weighttype: weighttype,
            weight: weight
        };
        materialData.push(materialItem);
        materialBool = true;
    })
    if (!materialBool)
        return;
    var systemData = new Array();
    var classData = new Array();
    if ($(".oslimit label").hasClass("cur")) {
        $.each(directArray, function (index, item) {

            var directBool = false;
            $.each(matchArray, function (k, p) {
                if (item.directtypeid == p.directtypeid) {
                    directBool = true;
                    var directTypeData = p.directtypedata;
                    if (p.directtypeid == "1") {
                        $.each(directTypeData, function (m, n) {
                            var systemItem = {
                                systemid: n.directid
                            };
                            systemData.push(systemItem);
                        })
                    }
                    else if (p.dircttypeid == "2") {
                        $.each(directTypeData, function (m, n) {
                            var classItem = {
                                classid: n.directid
                            };
                            classData.push(classItem);
                        })
                    }
                    else {


                    }
                }
            })
            if (!directBool)
                alert("你未选择" + item.directtypename);
        })
    }
    var areaData = "";
    var data = {
        infodata: infoData,
        materialdata: materialData,
        systemdata: systemData,
        areadata: areaData,
        classdata: classData
    };
    var ajaxData = {
        method: "InsertAdData",
        data: JSON.stringify(data)
    };
    alert(JSON.stringify(data));
     return;
    $.ajax({
        url: "../../api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert("操作失败，请重试");
            else location.href = "../ad/ad_info.aspx";
        }
    });

}

//修改
function UpdataData(adId) {
    var sAdId = adId;

    //页面列表
    var sPageId = $("#info_page .cur").find("span").text(); //$()
    //1不为空，2为数字
    if (!CheckDataValid("页面列表", sPageId, "1,2")) {
        return;
    }    
    //广告位
    var sAdLocationId = $("#info_adlocation .cur").find("span").text();
    if (!CheckDataValid("广告位", sAdLocationId, "1,2")) {
        return;
    }    
    //广告类型
    var sAdTypeId = $("#info_adtype .cur").find("span").text();
    if (!CheckDataValid("广告类型", sAdTypeId, "1,2")) {
        return;
    }    
    //广告名称
    var sAdName = $("#info_adname").val();
    if (!CheckDataValid("广告名称", sAdName, "1")) {
        return;
    }
    //价格
    var sPrice = $("#info_price").val();
    if (!CheckDataValid("价格", sPrice, "1,2")) {
        return;
    }    
    //推送开始时间
    var sPutStartTime = $("#some_class_1").val();
    if (!CheckDataValid("推送开始时间", sPutStartTime, "1")) {
        return;
    }
    //推送结束时间
    var sPutEndTime = "";
    if ($("#endtimeckbox label").hasClass("cur")) {
        sPutEndTime = $("#some_class_2").val();
        if (!CheckDataValid("推送结束时间", sPutEndTime, "1")) {
            return;
        }
    }
    //投放上限
    var sPutMax = "";
    if ($("#info_putmax input[type='checkbox']").attr("checked")) {
        sPutMax = $("#info_putmax input[type='text']").val();
        if (!CheckDataValid("投放上限", sPutMax, "1,2")) {
            return;
        }
    }
    //投放上限每日
    var sPutMaxByDay = "";
    if ($("#info_putmaxbyday input[type='checkbox']").attr("checked")) {
        sPutMaxByDay =$("#info_putmaxbyday input[type='text']").val();
        if (!CheckDataValid("投放上限每日", sPutMaxByDay, "1,2")) {
            return;
        }
    }
    //转换数据
    sPageId = TransDataToInt(sPageId);
    sAdLocationId = TransDataToInt(sAdLocationId);
    sAdTypeId = TransDataToInt(sAdTypeId);
    sPrice = TransDataToInt(sPrice);
    //基本信息
    var infoData = {
        adid: sAdId,
        pageid: sPageId,
        adlocationid: sAdLocationId,
        adtypeid: sAdTypeId,
        adname: sAdName,
        price: sPrice,
        putstarttime: sPutStartTime,
        putendtime: sPutEndTime,
        putmax: sPutMax,
        putmaxbyday: sPutMaxByDay
    };

    //权重
    //var sWeightType = $("#info_weighttype .cur").find("span").text();
    //var sWeight = $("#info_weight input").val();
    var materialData = new Array();
    var materialBool = true;
    $(".materialtable tr").each(function () {
        var materialid = $(this).find(".materialid").text()
        if (!CheckDataValid("物料信息", materialid, "1,2")) {
            materialBool = false;
            return;
        }
        materialid = TransDataToInt(materialid);
        var weighttype = $("#info_weighttype .cur").find("span").text();
        var weight = "";
        if (weighttype == "1") {
            weight = "1";
        }
        else {
            weight = $(this).find(".manual").find("select").val();
            //weight = $(this).find(".weight").val();
            alert(weight);
        }
        var materialItem = {
            materialid: materialid,
            weighttype: weighttype,
            weight: weight
        };
        materialData.push(materialItem);
        materialBool = true;
    })
    if (!materialBool)
        return;
    var systemData = new Array();
    var classData = new Array();
    if ($(".oslimit label").hasClass("cur")) {
        $.each(directArray,function (index,item) {
            
            var directBool = false;
            $.each(matchArray, function (k, p) {
                if (item.directtypeid== p.directtypeid) {
                    directBool = true;
                    var directTypeData = p.directtypedata;
                    if (p.directtypeid == "1") {
                        $.each(directTypeData, function (m, n) {
                            var systemItem = {
                                systemid: n.directid
                            };
                            systemData.push(systemItem);
                        })
                    }
                    else if (p.dircttypeid == "2") {
                        $.each(directTypeData, function (m, n) {
                            var classItem = {
                                classid: n.directid
                            };
                            classData.push(classItem);
                        })
                    }
                    else {


                    }
                }
            })
            if (!directBool)
                alert("你未选择" + item.directtypename);
        })
    }
   // return;
    var areaData = "";
    var data = {
        infodata: infoData,
        materialdata: materialData,
        systemdata: systemData,
        areadata: areaData,
        classdata: classData
    };
    alert(JSON.stringify(data));
    var ajaxData = {
        method: "UpdateAdData",
        data: JSON.stringify(data)
    };
    return;
    $.ajax({
        url: "../../api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert("操作失败，请重试");
            else location.href = "ad_info.aspx";
        }
    });



}
//检验值是否有效
function CheckDataValid(inKey, inVal, inLevel) {
    var levelArray = new Array();
    levelArray = inLevel.split(',');
    for (var i = 0; i < levelArray.length; i++) {
        if (levelArray[i].toString() == "1") {
            if (!CheckDataEmpty(inVal)) {
                alert(inKey + "参数为空请填写");
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
////校验数据并返回

//function CheckData(inVal, inType, inLevel) {
//    if (inLevel == 1) {
//        if (inVal == undefined || inVal.trim() == "") {
//            //alert("数据不为空");
//            return;
//        }
//    }
//    if (inType == "int") {
//        if (inVal != undefined && inVal.trim() != "") {
//            return parseInt(inVal);
//        }
//    }
//    return inVal;
//}


function GetMaterialList(adTypeId) {
    //类型
    //var adTypeId = 1;
    //广告主
    var ajaxData = {
        method: "GetMaterialListData",
        userid: "",
        adtypeid: adTypeId
    };
    $.ajax({
        url: "../../api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert(result.errmsg);
            var materialData = result.data;
            //物料信息  materialtype 
            if (materialData != null) {
                $.each(materialData, function (index, item) {
                    if (item.materialtype == "1") {
                        var trHtml = [];

                        trHtml.push("<tr>");
                        trHtml.push("<td style='width:15%'>");
                        trHtml.push("<div class='checkbox-con'>");
                        trHtml.push("<span class='wrap'>");
                        trHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'>");
                        trHtml.push("<label class='checkbox'></label>");
                        trHtml.push("</span>");
                        trHtml.push("<em class='list_name'>" + item.name + "</em><span class='list_materialid' style='display:none'>" + item.materialid + "</span></div>");
                        trHtml.push("</td>");
                        trHtml.push("<td style='width:35%'>");
                        trHtml.push("<img src='" + item.imageurl + "' height='63' width='255' alt='' class='imgs'></td>");
                        trHtml.push("<td style='width:10%'>" + item.width + "*" + item.height + "</td>");
                        trHtml.push("<td style='width:10%'>" + item.materialtype + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.createtime + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.operationid + "</td></tr>");

                        $("#material_list").append(trHtml.join(""));
                    } else {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td style='width:15%'>");
                        trHtml.push("<div class='checkbox-con'>");
                        trHtml.push("<span class='wrap'>");
                        trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                        trHtml.push("<label class='checkbox'></label>");
                        trHtml.push("</span>");
                        trHtml.push("<em >" + item.name + "</em></div>");
                        trHtml.push("</td>");
                        trHtml.push("<td style='width:35%' class='pic'>");
                        trHtml.push("<p>" + item.title + "</p>");
                        trHtml.push("<img src='" + item.imageurl + "' alt='' class='textImg' width='120' height='90'></td>");
                        trHtml.push("<td style='width:10%'>" + item.width + '*' + item.height + "</td>");
                        trHtml.push("<td style='width:10%'>" + item.materialtype + "</td>");
                        trHtml.push("<td style='width:15%'>"+item.createtime+"</td>");
                        trHtml.push("<td style='width:15%'>" + item.operationid + "</td></tr>");
                        $("#material_list").append(trHtml.join(""));
                    }

                })
                $('.table-content').find('.checkbox').click(function (event) {
                    if ($(this).hasClass('cur')) {
                        $(this).removeClass('cur');
                    } else {
                        $(this).addClass('cur');
                    }
                    var _length1 = $('.table-content').find('.checkbox').length;
                    var _length2 = $('.table-content').find('.cur').length;
                    if (_length1 == _length2) {
                        $('.table-head').find('.checkbox').addClass('cur')
                    } else {
                        $('.table-head').find('.checkbox').removeClass('cur')
                    }
                    event.stopPropagation()
                })


            }
            $(".confirmT").unbind("click");
            $(".confirmT").click(function () {
                $('.table-content').find('.cur').each(function () {
                    var materialid = $(this).parent().parent().find(".list_materialid").text();
                    $.each(materialData, function (index, item) {
                        if (materialid == item.materialid) {
                            if (item.materialtype == "1") {
                                var trHtml = [];
                                trHtml.push("<tr>");
                                trHtml.push("<td>" + item.name + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                                trHtml.push("<td class='img' width='258'><img src='" + item.imageurl + "' height='71' width='235' ></td>");
                                trHtml.push("<td>" + item.width + "*" + item.height + "</td>");
                                trHtml.push("<td>" + item.format + "</td>");
                                trHtml.push("<td width='150'><button class='list_removebtn'>移除</button></td>");
                                trHtml.push("</tr>");
                                $(".materialtable").append(trHtml.join(""));
                            } else {
                                var trHtml = [];
                                trHtml.push("<tr>");
                                trHtml.push("<td>" + item.name + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                                trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                                trHtml.push("<td>" + item.width + "*" + item.height + "</td>");
                                trHtml.push("<td>" + item.format + "</td>");
                                trHtml.push("<td width='150'><button class='list_removebtn'> 移除</button></td>");
                                trHtml.push("</tr>");
                                $(".materialtable").append(trHtml.join(""));
                            }
                        }
                    })
                })

                $('.loginmask2').css('opacity', '.8').fadeOut();
                $('.popup2').animate({
                    'top': -1000
                }, 500)
                $(".list_removebtn").click(function () {
                    $(this).closest("tr").remove();

                })
            })

        }
    });

}

$(".wlBox").click(function () {
    var adTypeId = $("#info_adtype .slide .cur").find("span").text();
    GetMaterialList(adTypeId);

})

$(".slide dd").click(function () {
    $(this).closest(".slide").find("dd").removeClass("cur");
    $(this).addClass("cur");
})