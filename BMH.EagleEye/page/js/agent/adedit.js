

//获取广告信息
function GetAdInfo(adId) {
    if (adId == "") {
        alert("无效id,请检查");
        return;
    }
    var ajaxData = {       
        adid: adId
    };
    $.ajax({
        url: "/api/Manager.asmx/GetAdInfoByAdu",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode != "1") return;
            var infoData = result.data.infodata;         
            var materialData = result.data.materialdata;                     
                
            //基本信息
            $(".ad-name").val(infoData.adname);
            $(".ad-price").val(infoData.price);          

            //计费方式
            if (infoData.billingtype == "" || infoData.billingtype == "1") {
                $('#info_btype .checkbox-con').eq(0).find('.checkbox').addClass('cur');
                $('#info_btype .checkbox-con').eq(1).remove();
                $('#info_btype .checkbox-con').eq(2).remove();
            }
            else if (infoData.billingtype == "2") {
                $('#info_btype .checkbox-con').eq(1).find('.checkbox').addClass('cur');
                $('#info_btype .checkbox-con').eq(0).remove();
                $('#info_btype .checkbox-con').eq(2).remove();
            }
            else if (infoData.billingtype == "3") {
                $('#info_btype .checkbox-con').eq(2).find('.checkbox').addClass('cur');
                $('#info_btype .checkbox-con').eq(0).remove();
                $('#info_btype .checkbox-con').eq(1).remove();
            }
            if (infoData.termid != "")
            {
                if (infoData.termid == "1")
                {
                    if (!$('#info_term .term_h5').find('label').hasClass("cur"))
                    {
                        $('#info_term .term_h5').find('label').addClass("cur")
                    }
                    $('#info_term .term_app').remove();                 
                }
               else{
                    if (!$('#info_term .term_app').find('label').hasClass("cur")) {
                        $('#info_term .term_app').find('label').addClass("cur")
                    }
                    $('#info_term .term_h5').remove();
                }                
            }
            //广告类型列表
            $("#info_adtype .slide dd").removeClass("cur");
            $("#info_adtype .slide dd").each(function () {
                if ($(this).find("span").text() == infoData.adtypeid) {
                    $(this).addClass("cur");
                    $("#info_adtype").find(".title").text(infoData.adtypename);
                } else {
                    $(this).remove();
                }
            })
            //广告形式
            GetSubAdTypeByAdType(infoData.adtypeid, infoData.subadtypeid);
                       
            $("#info_adtype .slide dd").removeClass("cur");
            $("#info_adtype .slide dd").each(function () {
                if ($(this).find("span").text() == infoData.adtypeid) {
                    $(this).addClass("cur");
                    $("#info_adtype").find(".title").text(infoData.adtypename);
                } else {
                    $(this).remove();
                }
            })                                              
          
            //投放时间
            if (infoData.putstarttime != "") $("#starttime input").val(infoData.putstarttime);
            if (infoData.putendtime != "" && infoData.putendtime != "2099/12/31 0:00:00" && infoData.putendtime != "2099-12-31 0:00:00") {
                $("#endtimeckbox label").click();
                $("#endtime input").val(infoData.putendtime);
            }
            //投放量           
            if (infoData.putmax != "" && infoData.putmax != "999999999") {
                $(".limitmax").find("input").eq(0).click();
                $(".limitmax").find("input").eq(1).val(infoData.putmax);
            }
            //投放量每日
            if (infoData.putmaxbyday != "" && infoData.putmaxbyday != "999999999") {
                $(".limitmaxbyday").find("input").eq(0).click();
                $(".limitmaxbyday").find("input").eq(1).val(infoData.putmaxbyday);
            }

            //物料信息  materialtype 
            if (materialData != null) {
                $.each(materialData, function (index, item) {
                    $("#info_weight input").val(item.weight);
                    //banner或者悬浮
                    if (item.materialtype == "1" || item.materialtype == "3") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='img' width='258'><img src='" + item.imageurl + "' height='71' width='235' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");                                              
                        trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                        trHtml.push("</tr>");
                        $(".materialtable").append(trHtml.join(""));
                    }
                    //信息流
                    else if (item.materialtype == "2") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");                                               
                        trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                        trHtml.push("</tr>");
                        $(".materialtable").append(trHtml.join(""));
                    }
                    //弹层
                    else if (item.materialtype == "4") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td width='35%' class='case' ><h3>" + item.title + "</h3><p>" + item.remark + "</p><button>" + item.canceltext + "</button><button>" + item.confirmtext + "</button></td>");
                        trHtml.push("<td></td>");
                        trHtml.push("<td></td>");                        
                        trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                        trHtml.push("</tr>");
                        $(".materialtable").append(trHtml.join(""));
                    }
                    else {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");                                          
                        trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                        trHtml.push("</tr>");
                        $(".materialtable").append(trHtml.join(""));
                    }
                    //权重绑定
                    var length = $(".materialtable tr").length;
                    var $manual = $(".materialtable tr").eq(length - 1).find(".manual");
                    for (var i = 1; i < 10; i++) {
                        $manual.find("select").append('<option value="' + i + '">' + i + '</option>');
                    }
                    if (item.weighttype == "2") {
                        $("#info_weighttype dd").removeClass("cur");
                        $("#info_weighttype dd").each(function () {
                            if (item.weighttype == $(this).find("span").text()) {
                                $(this).addClass("cur");
                                $("#info_weighttype .title").text($(this).find("em").text());
                                return false;
                            }
                        })
                        $manual.css('visibility', 'visible');
                        $manual.find("select option").eq(item.weight - 1).attr("selected", "selected");
                    }                    
                })
            }
        }
    })
}
//新增广告

function InsertData(status) {   
  
    //广告类型
    var sAdTypeId = $("#info_adtype .cur").find("span").text();
    if (!CheckDataValid("广告类型", sAdTypeId, "1,2")) {
        return;
    }
    //广告形式
    var sSubAdTypeId = $("#info_subadtype .cur").find("span").text();
    if (!CheckDataValid("广告形式", sSubAdTypeId, "1,2")) {
        return;
    }            
    //广告名称
    var sAdName = $("#info_adname").val();
    if (!CheckDataValid("广告名称", sAdName, "1")) {
        return;
    }
    //投放终端
    var sTerm = "1";    
    if ($('#info_term .term_h5').find('label').hasClass("cur")) {
        sTerm = "1";
    }
    else {
        sTerm = "2";
    }    
    var sBType = "1";
    //计费方式 1cpc 2cpd
    if ($("#info_btype .btype_cpc").eq(0).find("label").hasClass("cur")) {
        sBType = "1";
    }
    else if ($("#info_btype .btype_cpd").eq(0).find("label").hasClass("cur")) {
        sBType = "2";
    } else if ($("#info_btype .btype_cpm").eq(0).find("label").hasClass("cur")) {
        sBType = "3";
    } else {
        sBType = "1";
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
    if ($("#info_putmax input[type='checkbox']").prop("checked")) {
        sPutMax = $("#info_putmax input[type='text']").val();
        if (!CheckDataValid("投放上限", sPutMax, "1,2")) {
            return;
        }
        sPutMax = TransDataToInt(sPutMax);
    }
    //投放上限每日
    var sPutMaxByDay = "";
    if ($("#info_putmaxbyday input[type='checkbox']").prop("checked")) {
        sPutMaxByDay = $("#info_putmaxbyday input[type='text']").val();
        if (!CheckDataValid("投放上限每日", sPutMaxByDay, "1,2")) {
            return;
        }
        sPutMaxByDay = TransDataToInt(sPutMaxByDay);
    }   
    var sAdUserId = _adUserId;
    var sAccountId = _accountId;
    //转换数据    
    sAdTypeId = TransDataToInt(sAdTypeId);
    sPrice = TransDataToFloat(sPrice);
   
    if ((sTerm=="1"&&sPrice < 0.1)||(sTerm=="2"&&sPrice < 0.01)) {
        alert("你填的价格太低了！！！");
        return ;
    }
    if (sPrice >10) {
        alert("你填的价格太高了！！！");
        return;
    }
    sAdUserId = TransDataToInt(sAdUserId);
    sAccountId = TransDataToInt(sAccountId);    
    sSubAdTypeId = TransDataToInt(sSubAdTypeId);   
    sBType = TransDataToInt(sBType);
    //基本信息
    var infoData = {       
        adtypeid: sAdTypeId,
        adname: sAdName,
        price: sPrice,
        putstarttime: sPutStartTime,
        putendtime: sPutEndTime,
        putmax: sPutMax,
        putmaxbyday: sPutMaxByDay,
        status: status,
        aduserid: sAdUserId,
        accountid: sAccountId,        
        subadtypeid: sSubAdTypeId,       
        billingtype: sBType,
        termid:sTerm
    };

    //物料信息
    var materialData = new Array();
    var materialBool = true;
    if ($(".materialtable tr").length < 1)
    {
        alert("别忘记选择物料！");
        return;
    }
    $(".materialtable tr").each(function () {
        var materialId = $(this).find(".materialid").text()
        if (!CheckDataValid("物料信息", materialId, "1,2")) {
            materialBool = false;
            return false;
        }        
        var weightType = $("#info_weighttype .cur").find("span").text();
        var weight = "";
        if (weightType == "1") {
            weight = "1";
        }
        else {
            weight = $(this).find(".manual").find("select").val();
        }
        var materialItem = {
            materialid: TransDataToInt(materialId),
            weighttype: TransDataToInt(weightType),
            weight: TransDataToInt(weight)
        };
        materialData.push(materialItem);
        materialBool = true;
    })
    if (!materialBool)
        return;

    var data = {
        infodata: infoData,
        materialdata: materialData       
    };
    var ajaxData = {
        method: "InsertAdDataByAdu",
        data: JSON.stringify(data)
    };   
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert(result.errmsg);
            else location.href = "/page/agent/adinfo.aspx";
        }
    });
}

//修改
function UpdataData(adId) {
    var sAdId = adId;        
    //广告名称
    var sAdName = $("#info_adname").val();
    if (!CheckDataValid("广告名称", sAdName, "1")) {
        return;
    }
    //投放终端
    var sTerm = "1";
    if ($('#info_term .term_h5').find('label').hasClass("cur")) {
        sTerm = "1";
    }
    else {
        sTerm = "2";
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
        sPutMax = TransDataToInt(sPutMax);
    }
    //投放上限每日
    var sPutMaxByDay = "";
    if ($("#info_putmaxbyday input[type='checkbox']").attr("checked")) {
        sPutMaxByDay = $("#info_putmaxbyday input[type='text']").val();
        if (!CheckDataValid("投放上限每日", sPutMaxByDay, "1,2")) {
            return;
        }
        sPutMaxByDay = TransDataToInt(sPutMaxByDay);
    }
    var sAccountId = _accountId;
    //转换数据    
    sAdId = TransDataToInt(sAdId);   
    sPrice = TransDataToFloat(sPrice);
    if ((sTerm == "1" && sPrice < 0.1) || (sTerm == "2" && sPrice < 0.01)) {
        alert("你填的价格太低了！！！");
        return;
    }
    if (sPrice > 10) {
        alert("你填的价格太高了！！！");
        return;
    }
    sAccountId = TransDataToInt(sAccountId);      
    //基本信息
    var infoData = {
        adid: sAdId,              
        adname: sAdName,
        price: sPrice,
        putstarttime: sPutStartTime,
        putendtime: sPutEndTime,
        putmax: sPutMax,
        putmaxbyday: sPutMaxByDay,        
        accountid: sAccountId,
        termid:sTerm
    };
    
    var data = {
        infodata: infoData
    };
    var ajaxData = {
        method: "UpdateAdDataByAdu",
        data: JSON.stringify(data)
    };   
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert("没有执行成功，请稍后重试");
            else location.href = "/page/agent/adinfo.aspx";
        }
    });
}
//根据页面获取广告位
//pageId 页面id
//defaultVal 默认广告位id值,为空则第一个
function GetAdLocationByPage(pageId,defaultVal)
{
    var ajaxData = {
        method: "GetAdLocationDataNew",
        pageid: pageId
    };
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "1") {
                var adLocationList = result.data;
                var tableHtml = [];
                var defaultName = "";
                $.each(adLocationList, function (i, item) {
                    if (i == 0 && defaultVal == "") {
                        tableHtml.push("<dd class='cur'>");
                        defaultName = item.adlocationname;
                    }
                    else {
                        if (defaultVal == item.adlocationid) {
                            tableHtml.push("<dd class='cur'>");
                            defaultName = item.adlocationname;
                        }
                        else
                            tableHtml.push("<dd>");
                    }
                    tableHtml.push("<em>"+ item.adlocationname+"</em>");
                    tableHtml.push("<span style='display:none'>"+item.adlocationid+"</span>");
                    tableHtml.push("</dd>");                  
                                       
                })
                $("#info_adlocation .title").text(defaultName);
                $("#info_adlocation .slide dl").html(tableHtml.join(''));
                $('#info_adlocation .slide dd').click(function () {
                    var $txt = $(this).find("em").text();
                    $(this).removeClass('cur').parents('.slide').prev().find('.title').text($txt);
                    $(this).parents('.slide').slideUp();
                    $('.slidebox').removeClass('is-open')
                    $('.triangle').removeClass('open');
                    $(this).closest(".slide").find("dd").removeClass("cur");
                    $(this).addClass("cur");
                })
            }
        }
    });

}

//根据广告类型获取广告形式
//adTypeId 广告类型id
//defaultVal 默认广告形式id值,为空则第一个
function GetSubAdTypeByAdType(adTypeId, defaultVal) {
    var ajaxData = {
        method: "GetSubAdTypeDataByAdu",
        adtypeid: adTypeId
    };
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "1") {
                var tempList = result.data;
                var tableHtml = [];
                var defaultName = "";
                $.each(tempList, function (i, item) {
                    if (i == 0 && defaultVal == "") {
                        tableHtml.push("<dd class='cur'>");
                        defaultName = item.subadtypename;
                    }
                    else {
                        if (defaultVal == item.subadtypeid) {
                            tableHtml.push("<dd class='cur'>");
                            defaultName = item.subadtypename;
                        }
                        else
                            tableHtml.push("<dd>");
                    }
                    tableHtml.push("<em>" + item.subadtypename + "</em>");
                    tableHtml.push("<span style='display:none'>" + item.subadtypeid + "</span>");
                    tableHtml.push("<p class='mat_width' style='display:none'>" + item.width + "</p>");
                    tableHtml.push("<p class='mat_height' style='display:none'>" + item.height + "</p>");
                    tableHtml.push("</dd>");
                })
                $("#info_subadtype .title").text(defaultName);
                $("#info_subadtype .slide dl").html(tableHtml.join(''));
                $("#matinfo_subadtype .title").text(defaultName);
                $("#matinfo_subadtype .slide dl").html(tableHtml.join(''));
                $('#info_subadtype .slide dd').click(function () {
                    var $txt = $(this).find("em").text();
                    $(this).removeClass('cur').parents('.slide').prev().find('.title').text($txt);
                    $(this).parents('.slide').slideUp();
                    $('.slidebox').removeClass('is-open')
                    $('.triangle').removeClass('open');
                    $(this).closest(".slide").find("dd").removeClass("cur");
                    $(this).addClass("cur");
                })
                $('#matinfo_subadtype .slide dd').click(function () {
                    var $txt = $(this).find("em").text();
                    $(this).removeClass('cur').parents('.slide').prev().find('.title').text($txt);
                    $(this).parents('.slide').slideUp();
                    $('.slidebox').removeClass('is-open')
                    $('.triangle').removeClass('open');
                    $(this).closest(".slide").find("dd").removeClass("cur");
                    $(this).addClass("cur");
                    //图片尺寸   
                    $(".size .wid input").val($(this).find(".mat_width").text());
                    $(".size .hei input").val($(this).find(".mat_height").text());
                })
            }
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
//转换数据为float
function TransDataToFloat(inVal) {
    if (isNaN(inVal))
        return 0;
    else
        return parseFloat(inVal);
}
//获取物料列表
function GetMaterialList(adTypeId) {
    //广告主
    var sAdUserId = _adUserId;
    //类型
    //var adTypeId = 1;
    //广告主
    var ajaxData = {
        method: "GetMaterialListData",
        aduserid: sAdUserId,
        adtypeid: adTypeId
    };
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert(result.errmsg);
            var materialData = result.data;
            //物料信息  materialtype 
            if (materialData != null) {
                $.each(materialData, function (index, item) {
                    //banner或者悬浮
                    if (item.materialtype == "1" || item.materialtype == "3" || item.materialtype == "5" || item.materialtype == "6") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td style='width:15%'>");
                        trHtml.push("<div class='checkbox-con'>");
                        trHtml.push("<span class='wrap'>");
                        trHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'>");
                        trHtml.push("<label class='checkbox'></label>");
                        trHtml.push("</span>");
                        trHtml.push("<em class='list_name'>" + item.name + "</em><span class='list_materialid' class='hide'>" + item.materialid + "</span></div>");
                        trHtml.push("</td>");
                        trHtml.push("<td style='width:35%'>");
                        trHtml.push("<img src='" + item.imageurl + "' height='63' width='255' alt='' class='imgs'></td>");
                        trHtml.push("<td style='width:10%'>" + item.width + "*" + item.height + "</td>");
                        trHtml.push("<td style='width:10%'>" + item.materialtypename + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.createtime + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.operationname + "</td></tr>");

                        $("#material_list table tbody").append(trHtml.join(""));
                    }
                    //信息流
                     else if (item.materialtype == "2") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td style='width:15%'>");
                        trHtml.push("<div class='checkbox-con'>");
                        trHtml.push("<span class='wrap'>");
                        trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                        trHtml.push("<label class='checkbox'></label>");
                        trHtml.push("</span>");
                        trHtml.push("<em >" + item.name + "</em><span class='list_materialid' class='hide'>" + item.materialid + "</span></div>");
                        trHtml.push("</td>");
                        trHtml.push("<td style='width:35%' class='pic'>");
                        trHtml.push("<p>" + item.title + "</p>");
                        trHtml.push("<img src='" + item.imageurl + "' alt='' class='textImg' width='120' height='90'></td>");
                        trHtml.push("<td style='width:10%'>" + item.width + '*' + item.height + "</td>");
                        trHtml.push("<td style='width:10%'>" + item.materialtypename + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.createtime + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.operationname + "</td></tr>");
                        $("#material_list table tbody").append(trHtml.join(""));
                    }
                    //弹层
                    else if (item.materialtype == "4") {
                        var trHtml = [];
                        trHtml.push("<tr>");
                        trHtml.push("<td style='width:15%'>");
                        trHtml.push("<div class='checkbox-con'>");
                        trHtml.push("<span class='wrap'>");
                        trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                        trHtml.push("<label class='checkbox'></label>");
                        trHtml.push("</span>");
                        trHtml.push("<em >" + item.name + "</em><span class='list_materialid' class='hide'>" + item.materialid + "</span></div>");
                        trHtml.push("</td>");
                        trHtml.push("<td width='35%' class='case'>");
                        trHtml.push("<h3>" + item.title + "</h3>");
                        //trHtml.push("<p>"+item.remark+"</p>");
                        trHtml.push("<button>"+item.canceltext+"</button><button>"+item.confirmtext+"</button></td>");                        
                        trHtml.push("<td style='width:10%'>无</td>");
                        trHtml.push("<td style='width:10%'>" + item.materialtypename + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.createtime + "</td>");
                        trHtml.push("<td style='width:15%'>" + item.operationname + "</td></tr>");
                        $("#material_list table tbody").append(trHtml.join(""));
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
                            //banner或者悬浮
                            if (item.materialtype == "1" || item.materialtype == "3" || item.materialtype == "5" || item.materialtype == "6") {
                                var trHtml = [];
                                trHtml.push("<tr>");
                                trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                                trHtml.push("<td>" + item.name + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                                trHtml.push("<td class='img' width='258'><img src='" + item.imageurl + "' height='71' width='235' ></td>");
                                trHtml.push("<td>" + item.width + "*" + item.height + "</td>");                                
                                trHtml.push("<td width='150'><button  class='del list_removebtn'>移除</button></td>");
                                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                                trHtml.push("</tr>");
                                $(".materialtable").append(trHtml.join(""));
                            }
                            //信息流
                            else if (item.materialtype == "2") {
                                var trHtml = [];
                                trHtml.push("<tr>");
                                trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                                trHtml.push("<td>" + item.name + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                                trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                                trHtml.push("<td>" + item.width + "*" + item.height + "</td>");                                
                                trHtml.push("<td width='150'><button class='del list_removebtn'>移除</button></td>");
                                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                                trHtml.push("</tr>");
                                $(".materialtable").append(trHtml.join(""));

                            }                            
                            //弹层
                            else if (item.materialtype == "4") {
                                var trHtml = [];
                                trHtml.push("<tr>");
                                trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                                trHtml.push("<td>" + item.name + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                                trHtml.push("<td width='35%' class='case' ><h3>" + item.title + "</h3><p>" + item.remark + "</p><button>" + item.canceltext + "</button><button>" + item.confirmtext + "</button></td>");
                                trHtml.push("<td>无</td>");
                                trHtml.push("<td></td>");
                                trHtml.push("<td width='150'><button class='del list_removebtn'>移除</button></td>");
                                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                                trHtml.push("</tr>");
                                $(".materialtable").append(trHtml.join(""));

                            } 
                            
                            else {
                                var trHtml = [];
                                trHtml.push("<tr>");
                                trHtml.push("<td width='100px'>" + item.materialtypename + "</td>");
                                trHtml.push("<td>" + item.name + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                                trHtml.push("<td class='img' width='258'><img src='" + item.imageurl + "' height='71' width='235' ></td>");
                                trHtml.push("<td>" + item.width + "*" + item.height + "</td>");                                
                                trHtml.push("<td width='150'><button  class='del list_removebtn'>移除</button></td>");
                                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                                trHtml.push("</tr>");
                                $(".materialtable").append(trHtml.join(""));
                            }                         
                            var length = $(".materialtable tr").length;
                            var $manual = $(".materialtable tr").eq(length - 1).find(".manual");
                            for (var i = 1; i < 10; i++) {
                                $manual.find("select").append('<option value="' + i + '">' + i + '</option>');
                            }
                            if ($("#info_weighttype .cur").find("span").text() == "2") {
                                $manual.css('visibility', 'visible');
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

//广告类型点击
$("#info_adtype dl dd").click(function () {
    var adTypeId = $(this).find("span").text();    
    GetSubAdTypeByAdType(adTypeId, "");
})
//广告形式点击
$("#info_subadtype dl dd").click(function () {
    var subAdTypeId = $(this).find("span").text();
    GetPutRangeBySubAdType(subAdTypeId, "");
})
//点击物料选择
$(".wlBox").click(function () {
    $("#material_list tr").remove();
    var adTypeId = $("#info_adtype .slide .cur").find("span").text();  
    GetMaterialList(adTypeId);
})
//下拉
$(".slide dd").click(function () {
    $(this).closest(".slide").find("dd").removeClass("cur");
    $(this).addClass("cur");
})
//保存并启用
$(".saveAndUse").click(function () {
    InsertData(1);
})
//取消
$(".cencel").click(function () {
    // InsertData(1);
    if (confirm("确定取消吗")) {
        window.close();
    } else {

    }
})

//新建物料-保存
$(".confirm").click(function () {
    //取数据
    var materialType = $("#selectMatType .on").find("span").text();
    var materialTypeName = $("#selectMatType .on").text();
    $("#info_adtype dd").each(function () {
        if ($(this).find("span").text() == materialType) {
            materialTypeName = $(this).find("em").text();
        }
    })
    var data = GetMaterialDataByInsert(materialType);
    if (data == "")
        return;
    //传数据
    var ajaxData = {
        method: "InsertMaterialData",
        materialtype: materialType,
        data: JSON.stringify(data)
    };
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode != "1") {
                alert(result.errmsg);
                return;
            }
            //新建完成则填充到已选列表--如果类型不匹配则不添加
            var materialId = result.data;
            //banner
            if (data.materialtype == "1" || data.materialtype == "3") {
                var trHtml = [];
                trHtml.push("<tr>");
                trHtml.push("<td width='100px'>" + materialTypeName + "</td>");
                trHtml.push("<td>" + data.name + "<span style='display:none' class='materialid'>" + materialId + "</span></td>");
                trHtml.push("<td class='img' width='258'><img src='" + data.imageurl + "' height='71' width='235' ></td>");
                trHtml.push("<td>" + data.width + "*" + data.height + "</td>");
                trHtml.push("<td>" + data.format + "</td>");
                trHtml.push("<td width='150'><button  class='del list_removebtn'>移除</button></td>");
                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                trHtml.push("</tr>");
                $(".materialtable").append(trHtml.join(""));
            }
            //信息流
            else if (data.materialtype == "2") {
                var trHtml = [];
                trHtml.push("<tr>");
                trHtml.push("<td width='100px'>" + materialTypeName + "</td>");
                trHtml.push("<td>" + data.name + "<span style='display:none' class='materialid'>" + materialId + "</span></td>");
                trHtml.push("<td class='txt' ><p>" + data.title + "</p><img src='" + data.imageurl + "' height='71' width='88' ></td>");
                trHtml.push("<td>" + data.width + "*" + data.height + "</td>");
                trHtml.push("<td>" + data.format + "</td>");
                trHtml.push("<td width='150'><button class='del list_removebtn'>移除</button></td>");
                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                trHtml.push("</tr>");
                $(".materialtable").append(trHtml.join(""));

            }           
            //弹层
            else if (data.materialtype == "4") {
                var trHtml = [];
                trHtml.push("<tr>");
                trHtml.push("<td width='100px'>" + materialTypeName + "</td>");
                trHtml.push("<td>" + data.name + "<span style='display:none' class='materialid'>" + materialId + "</span></td>");
                trHtml.push("<td width='35%' class='case' ><h3>" + data.title + "</h3><p>" + data.remark + "</p><button>" + data.canceltext + "</button><button>" + data.confirmtext + "</button></td>");
                trHtml.push("<td>无</td>");
                trHtml.push("<td></td>");
                trHtml.push("<td width='150'><button class='del list_removebtn'>移除</button></td>");
                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                trHtml.push("</tr>");
                $(".materialtable").append(trHtml.join(""));

            }
            else {
                var trHtml = [];
                trHtml.push("<tr>");
                trHtml.push("<td width='100px'>" + materialTypeName + "</td>");
                trHtml.push("<td>" + data.name + "<span style='display:none' class='materialid'>" + materialId + "</span></td>");
                trHtml.push("<td class='txt' ><p>" + data.title + "</p><img src='" + data.imageurl + "' height='71' width='88' ></td>");
                trHtml.push("<td>" + data.width + "*" + data.height + "</td>");
                trHtml.push("<td>" + data.format + "</td>");
                trHtml.push("<td width='150'><button class='del list_removebtn'>移除</button></td>");
                trHtml.push("<td width='120' class='manual' style='visibility: hidden;'>权重<select class='weight'></select>级</td>");
                trHtml.push("</tr>");
                $(".materialtable").append(trHtml.join(""));

            }

            var length = $(".materialtable tr").length;
            var $manual = $(".materialtable tr").eq(length - 1).find(".manual");
            for (var i = 1; i < 10; i++) {
                $manual.find("select").append('<option value="' + i + '">' + i + '</option>');
            }
            if ($("#info_weighttype .cur").find("span").text() == "2") {
                $manual.css('visibility', 'visible');
            }
            

        }
    });

})
//新建物料-相关
//获取物料
function GetMaterialDataByInsert(materialType) {
    //广告主
    var sAdUserId = _adUserId;
    if (!CheckDataValid("广告主", sAdUserId, "1,2")) {
        return "";
    }
    var returnData = "";
    if (materialType == "1") {
        var sMaterialName = $(".wlmc input").val();
        if (!CheckDataValid("物料名称", sMaterialName, "1")) {
            return "";
        }
        var sImageUrl = $(".picFile input").val();
        if (!CheckDataValid("图片地址", sImageUrl, "1")) {
            return "";
        }
        var sWidth = $(".size").find("input").eq(0).val();
        if (!CheckDataValid("宽度", sWidth, "1")) {
            return "";
        }
        var sHeight = $(".size").find("input").eq(1).val();
        if (!CheckDataValid("高度", sHeight, "1")) {
            return "";
        }
        var sLinkUrl = $(".link input").val();
        if (!CheckDataValid("链接地址", sLinkUrl, "1")) {
            return "";
        }
        var sDisplay = "";
        if ($(".targetW .checkbox-con").eq(0).find("label").hasClass("cur")) {
            sDisplay = "1";
        }
        else if ($(".targetW .checkbox-con").eq(1).find("label").hasClass("cur")) {
            sDisplay = "2";
        }
        if (!CheckDataValid("弹窗方式", sDisplay, "1")) {
            return "";
        }

        var sSizes = $("#sizes").val();
        var sFormat = $("#format").val();
        sAdUserId = TransDataToInt(sAdUserId);
        _accountId = TransDataToInt(_accountId);
        //组装数据
        returnData = {
            aduserid: sAdUserId,
            accountid: _accountId,
            materialtype: materialType,
            name: sMaterialName,
            imageurl: sImageUrl,
            width: sWidth,
            height: sHeight,
            sizes: sSizes,
            format: sFormat,
            linkurl: sLinkUrl,
            display: sDisplay
        };
    }
    else if (materialType == "2") {
        var sMaterialName = $(".wlmc input").val();
        if (!CheckDataValid("物料名称", sMaterialName, "1")) {
            return "";
        }
        var sImageUrl = $(".picFile input").val();
        if (!CheckDataValid("图片地址", sImageUrl, "1")) {
            return "";
        }
        var sTitle = $("li.title input").val();
        if (!CheckDataValid("标题", sTitle, "1")) {
            return "";
        }
        var sWidth = $(".size").find("input").eq(0).val();
        if (!CheckDataValid("宽度", sWidth, "1")) {
            return "";
        }
        var sHeight = $(".size").find("input").eq(1).val();
        if (!CheckDataValid("高度", sHeight, "1")) {
            return "";
        }
        var sLinkUrl = $(".link input").val();
        if (!CheckDataValid("链接地址", sLinkUrl, "1")) {
            return "";
        }
        var sDisplay = "";
        if ($(".targetW .checkbox-con").eq(0).find("label").hasClass("cur")) {
            sDisplay = "1";
        }
        else if ($(".targetW .checkbox-con").eq(1).find("label").hasClass("cur")) {
            sDisplay = "2";
        }
        if (!CheckDataValid("弹窗方式", sDisplay, "1")) {
            return "";
        }
        var sSizes = $("#sizes").val();
        var sFormat = $("#format").val();
        sAdUserId = TransDataToInt(sAdUserId);
        _accountId = TransDataToInt(_accountId);
        //组装数据
        returnData = {
            aduserid: sAdUserId,
            accountid: _accountId,
            materialtype: materialType,
            name: sMaterialName,
            title: sTitle,
            imageurl: sImageUrl,
            width: sWidth,
            height: sHeight,
            sizes: sSizes,
            format: sFormat,
            linkurl: sLinkUrl,
            display: sDisplay
        };
    }
    else if (materialType == "3") {
        var sMaterialName = $(".wlmc input").val();
        if (!CheckDataValid("物料名称", sMaterialName, "1")) {
            return "";
        }
        var sImageUrl = $(".picFile input").val();
        if (!CheckDataValid("图片地址", sImageUrl, "1")) {
            return "";
        }
        var sWidth = $(".size").find("input").eq(0).val();
        if (!CheckDataValid("宽度", sWidth, "1")) {
            return "";
        }
        var sHeight = $(".size").find("input").eq(1).val();
        if (!CheckDataValid("高度", sHeight, "1")) {
            return "";
        }
        var sLinkUrl = $(".link input").val();
        if (!CheckDataValid("链接地址", sLinkUrl, "1")) {
            return "";
        }
        var sShowTime = $(".popupTime input").val();
        if (!CheckDataValid("弹出时间", sShowTime, "1")) {
            return "";
        }
        var sDisplay = "";
        if ($(".targetW .checkbox-con").eq(0).find("label").hasClass("cur")) {
            sDisplay = "1";
        }
        else if ($(".targetW .checkbox-con").eq(1).find("label").hasClass("cur")) {
            sDisplay = "2";
        }
        if (!CheckDataValid("弹窗方式", sDisplay, "1")) {
            return "";
        }
        var sSizes = $("#sizes").val();
        var sFormat = $("#format").val();
        sShowTime = TransDataToInt(sShowTime);
        sAdUserId = TransDataToInt(sAdUserId);
        _accountId = TransDataToInt(_accountId);
        //组装数据
        returnData = {
            aduserid: sAdUserId,
            accountid: _accountId,
            materialtype: materialType,
            name: sMaterialName,
            imageurl: sImageUrl,
            width: sWidth,
            height: sHeight,
            sizes: sSizes,
            format: sFormat,
            linkurl: sLinkUrl,
            display: sDisplay,
            showtime: sShowTime
        };
    }
    else if (materialType == "4") {
        var sMaterialName = $(".wlmc input").val();
        if (!CheckDataValid("物料名称", sMaterialName, "1")) {
            return "";
        }
        var sTitle = $("li.title input").val();
        if (!CheckDataValid("标题", sTitle, "1")) {
            return "";
        }
        var sImageUrl = $(".picFile input").val();
        if (!CheckDataValid("图片地址", sImageUrl, "1")) {
            return "";
        }
        var sWidth = $(".size").find("input").eq(0).val();
        if (!CheckDataValid("宽度", sWidth, "1")) {
            return "";
        }
        var sHeight = $(".size").find("input").eq(1).val();
        if (!CheckDataValid("高度", sHeight, "1")) {
            return "";
        }
        var sConfirmText = $(".btnArea .btn1").val();
        if (sConfirmText.trim() == "") {
            sConfirmText = "确定";
        }
        var sCancelText = $(".btnArea .btn2").val();
        if (sCancelText.trim() == "") {
            sCancelText = "取消";
        }
        var sLinkUrl = $(".link input").val();
        if (!CheckDataValid("链接地址", sLinkUrl, "1")) {
            return "";
        }
        var sShowTime = $(".popupTime input").val();
        if (!CheckDataValid("弹出时间", sShowTime, "1")) {
            return "";
        }
        var sDisplay = "";
        if ($(".targetW .checkbox-con").eq(0).find("label").hasClass("cur")) {
            sDisplay = "1";
        }
        else if ($(".targetW .checkbox-con").eq(1).find("label").hasClass("cur")) {
            sDisplay = "2";
        }
        if (!CheckDataValid("弹窗方式", sDisplay, "1")) {
            return "";
        }
        sShowTime = TransDataToInt(sShowTime);
        sAdUserId = TransDataToInt(sAdUserId);
        _accountId = TransDataToInt(_accountId);
        //组装数据
        returnData = {
            aduserid: sAdUserId,
            accountid: _accountId,
            materialtype: materialType,
            name: sMaterialName,
            title: sTitle,
            imageurl: sImageUrl,
            width: sWidth,
            height: sHeight,
            linkurl: sLinkUrl,
            display: sDisplay,
            showtime: sShowTime,
            confirmtext: sConfirmText,
            canceltext: sCancelText

        };
    }
    return returnData;
}

//文件选择
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

        if (fileSize / 1024 > 15) {
            alert("请选择小于15K的图片！");
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
        if (fileSize / 1024 > (1024*5)) {
            alert("请选择小于5M的安装包！");
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
        url: '/page/upload.aspx?type=' + type,
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

