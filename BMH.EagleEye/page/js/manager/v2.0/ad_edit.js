//全局数据用于保存数据
var directArray = new Array();
var matchArray = new Array();
var _price=0;

//设置其他定向数据填充
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
             $already = $('.otherSet').find('.already'),
             $addAll = $(".otherSet").find(".add_all"),
             $deleteAll = $(".otherSet").find(".delete_all");
    var flag = 0;
    //点击选中
    $system.find('p').click(function () {
        var txt = $(this).find("em").text();
        var strId = $(this).find("span").text();
        if ($(this).hasClass('on')) {
            return false;
        }
        $already.append('<p class="on"><span style="display:none">' + strId + '</span><em>' + txt + '</em><em class="del"></em></p>');
        $(this).addClass('on');
        var directTypeId = $("#info_others .slide .cur").find("span").text();        
        //如果数据不为空
        if (matchArray != null) {
            var macthcBool = false;
            //匹配数据是否存在
            $.each(matchArray, function (index, item) {
                if (directTypeId == item.directtypeid) {
                    $.each(item.directtypedata, function (p, k) {
                        if (strId == k.directid) {
                            macthcBool = true;
                            return false;
                        }
                    })
                    if (!macthcBool) {
                        macthcBool = true;
                        var tempData = { directid: strId, directname: txt };
                        item.directtypedata.push(tempData);
                    }
                    return false;
                }
            })
            //无则插入数据
            if (!macthcBool) {
                var directTypeName = $("#info_others").find(".title").text();
                var directTypeData = new Array();
                var tempData = { directid: strId, directname: txt };
                directTypeData.push(tempData);
                var tempTypeData = { directtypeid: directTypeId, directtypename: directTypeName, directtypedata: directTypeData };
                matchArray.push(tempTypeData);
            }
        }
        else {
            var directTypeName = $("#info_others").find(".title").text();
            var directTypeData = new Array();
            var tempData = { directid: strId, directname: txt };
            directTypeData.push(tempData);
            var tempTypeData = { directtypeid: directTypeId, directtypename: directTypeName, directtypedata: directTypeData };
            matchArray.push(tempTypeData);
        }
    })
    //全选 增加
    $addAll.click(function () {
        $system.find("p").each(function () {
            var txt = $(this).find("em").text();
            var strId = $(this).find("span").text();
            if ($(this).hasClass('on')) {
                return;
            }
            $already.append('<p class="on"><span style="display:none">' + strId + '</span><em>' + txt + '</em><em class="del"></em></p>');
            $(this).addClass('on');
            var directTypeId = $("#info_others .slide .cur").find("span").text();
            //如果数据不为空
            if (matchArray != null) {
                var macthcBool = false;
                //匹配数据是否存在
                $.each(matchArray, function (index, item) {
                    if (directTypeId == item.directtypeid) {
                        $.each(item.directtypedata, function (p, k) {
                            if (strId == k.directid) {
                                macthcBool = true;
                                return false;
                            }
                        })
                        if (!macthcBool) {
                            macthcBool = true;
                            var tempData = { directid: strId, directname: txt };
                            item.directtypedata.push(tempData);
                        }
                        return false;
                    }
                })
                //无则插入数据
                if (!macthcBool) {
                    var directTypeName = $("#info_others").find(".title").text();
                    var directTypeData = new Array();
                    var tempData = { directid: strId, directname: txt };
                    directTypeData.push(tempData);
                    var tempTypeData = { directtypeid: directTypeId, directtypename: directTypeName, directtypedata: directTypeData };
                    matchArray.push(tempTypeData);
                }
            }
            else {
                var directTypeName = $("#info_others").find(".title").text();
                var directTypeData = new Array();
                var tempData = { directid: strId, directname: txt };
                directTypeData.push(tempData);
                var tempTypeData = { directtypeid: directTypeId, directtypename: directTypeName, directtypedata: directTypeData };
                matchArray.push(tempTypeData);
            }                
        })
    })
    //删除
    $already.on('click', '.del', function () {
        $(this).parent().remove();
        var strId = $(this).parent().find("span").text();
        var txt = $(this).parent().find("em").eq(0).text();
        $system.find('p').each(function () {
            if ($(this).find("span").text() == strId) { $(this).removeClass('on') }
        })
        var directTypeId = $("#info_others .slide .cur").find("span").text();        
        //如果数据不为空
        if (matchArray != null) {
            //匹配数据是否存在
            $.each(matchArray, function (index, item) {
                if (directTypeId == item.directtypeid) {                    
                    $.each(item.directtypedata, function (p, k) {
                        if (strId == k.directid) {
                            item.directtypedata.splice(p, 1);
                            return false;
                        }
                    })
                    //如果为空则移除
                    if (item.directtypedata.length == 0)
                    {
                        matchArray.splice(index, 1);
                    }
                }
            })
        }
    })
    //全选 删除
    $deleteAll.click(function () {
        $already.find('.del').each(function () {
            $(this).parent().remove();
            var strId = $(this).parent().find("span").text();
            var txt = $(this).parent().find("em").eq(0).text();
            $system.find('p').each(function () {
                if ($(this).find("span").text() == strId) { $(this).removeClass('on') }
            })
            var directTypeId = $("#info_others .slide .cur").find("span").text();
            //如果数据不为空
            if (matchArray != null) {
                //匹配数据是否存在
                $.each(matchArray, function (index, item) {
                    if (directTypeId == item.directtypeid) {
                        $.each(item.directtypedata, function (p, k) {
                            if (strId == k.directid) {
                                item.directtypedata.splice(p, 1);
                                return false;
                            }
                        })
                        //如果为空则移除
                        if (item.directtypedata.length == 0) {
                            matchArray.splice(index, 1);
                        }
                    }
                })
            }
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
        url: "/api/Manager.asmx/GetAdInfo",
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
            var directDateData = result.data.directdatedata;
            var directAreaData = result.data.directareadata;
            var putRange = result.data.putrange;
            _price=infoData.price;
            //是否打底
            //$('#info_isbottom').find('.checkbox-con').each(function () {
            //    var that = $(this);
            //    var index = $(this).index();                
            //    that.find('.checkbox').unbind("click");
            //    //待确定
            //    if (index.toString() == infoData.isbottom) {
            //      that.find('.checkbox').addClass('cur').parents('.checkbox-con').siblings().remove();
            //    }                
            //});
            
            if (infoData.isbottom == "" || infoData.isbottom == "0") {
                $('#info_isbottom .checkbox-con').eq(0).find('.checkbox').addClass('cur');
                $('#info_isbottom .checkbox-con').eq(1).remove();
                
                //是否竞价
                if (infoData.isbid == "0" || infoData.isbid == "")
                {
                    //页面列表
                    $("#info_page .slide dd").removeClass("cur");
                    $("#info_page .slide dd").each(function () {
                        // $(this).removeClass("cur");
                        if (infoData.pageid != "")
                        {
                            if ($(this).find("span").text() == infoData.pageid) {
                                $(this).addClass("cur");
                                $("#info_page").find(".title").text(infoData.pagename);
                            }
                            else {
                                $(this).remove();
                            }
                        }                        
                    })
                    var pageId = infoData.pageid;
                    if (pageId == "")
                        pageId = "1";
                    GetAdLocationByPage(pageId, infoData.adlocationid);
                    $("#info_putrange").hide();
                    $('.basic-info li.middle,.basic-info li.info_page,#info_isbid').show();
                    $("#info_isbid .checkbox-con").eq(0).find(".checkbox").addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
                }
                else
                {
                    //投放范围
                    //var 
                    GetPutRangeBySubAdType(infoData.subadtypeid, putRange);
                    $("#info_putrange,#info_isbid").show();
                    $('.basic-info li.middle,.basic-info li.info_page').hide();
                    $("#info_isbid .checkbox-con").eq(1).find(".checkbox").addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
                }
               
            }
            else {
                $('#info_isbottom .checkbox-con').eq(1).find('.checkbox').addClass('cur');
                $('#info_isbottom .checkbox-con').eq(0).remove();
                $('.basic-info li.middle,.basic-info li.info_page,#info_isbid,#info_putrange').hide();
            }
            //计费方式
            if (infoData.billingtype == "" || infoData.billingtype == "1") {
                $('#info_btype .btype_cpc').find('.checkbox').addClass('cur');
                $('#info_btype .btype_cpd').remove();
                $('#info_btype .btype_cpm').remove();
            }
            else if (infoData.billingtype == "2") {
                $('#info_btype .btype_cpc').remove();
                $('#info_btype .btype_cpd').find('.checkbox').addClass('cur');
                $('#info_btype .btype-cpm').remove();
            }
            else if (infoData.billingtype == "3") {
                $('#info_btype .btype_cpc').remove();
                $('#info_btype .btype_cpd').remove();
                $('#info_btype .btype_cpm').find('.checkbox').addClass('cur');
            }
            //遍历列表 三个 并选中成当前
           
            ////广告位列表
            //$("#info_adlocation .slide dd").removeClass("cur");
            //$("#info_adlocation .slide dd").each(function () {
            //    if ($(this).find("span").text() == infoData.adlocationid) {
            //        $(this).addClass("cur");
            //        $("#info_adlocation").find(".title").text(infoData.adlocationname);
            //    }
            //})
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
            ////广告主
            $("#info_aduser .slide dd").removeClass("cur");
            $("#info_aduser .slide dd").each(function () {
                if ($(this).find("span").text() == infoData.aduserid) {
                    $(this).addClass("cur");
                    $("#info_aduser").find(".title").text(infoData.adusername);
                } else {
                    $(this).remove();
                }
            })
            //if (typeof (_accountType) != undefined) {
            //    if (_accountType == "2") {
            //        $(".info_aduser .slide dd").removeClass("cur");
            //        $(".info_aduser .slide dd").each(function () {
            //            if ($(this).find("span").text() == infoData.aduserid) {
            //                $(this).addClass("cur");
            //                $(".info_aduser").find(".title").text(infoData.adusername);
            //            } else {
            //                $(this).remove();
            //            }
            //        })
            //    }
            //    else {
            //        sAdUserId = _adUserId
            //    }
            //}
            $("#info_adtype .slide dd").removeClass("cur");
            $("#info_adtype .slide dd").each(function () {
                if ($(this).find("span").text() == infoData.adtypeid) {
                    $(this).addClass("cur");
                    $("#info_adtype").find(".title").text(infoData.adtypename);
                } else {
                    $(this).remove();
                }

            })                       
            //时间定向
            if (directDateData != null ) {
                if (directDateData.datetype == "1") {
                    $(".regionalOrientation .box2 .checkbox-con").eq(0).find("label").click();
                }
                else {
                    if (directDateData.hourdata != null) {
                        $.each(directDateData.hourdata, function (index, item) {
                            for (var i = parseInt(item.starthour); i <= parseInt(item.endhour); i++) {

                                $(".regionalOrientation .timeBox table tr").eq(parseInt(item.week)).find("td").eq(i).addClass("on");
                            }
                        })
                    }
                }
            } else {
                $(".regionalOrientation .box2 .checkbox-con").eq(0).find("label").click();
            }
            //地域定向
            if (directAreaData != null && directAreaData.length > 0)
            {
                $.each(directAreaData, function (index,item) {
                    if (item.clevel == "1"&&item.cid=="0")
                    {
                        $(".regionalOrientation .area .region").find('label').addClass('cur');
                        return false;
                    }
                    else 
                    {
                        $(".regionalOrientation .area .region").each(function () {
                            var $that = $(this); 
                            var areaId= $that.find("dt em span").text();
                            //区域
                            if (item.clevel == "2"||item.cid=="-1")
                            {
                                if (item.cid ==areaId)
                                {
                                    $that.find(".wrap label").addClass("cur");
                                }
                            }
                            //省、市
                            else  {
                                if (item.areaid == areaId) {
                                    $that.find("dd").each(function () {
                                        var $this = $(this);
                                        var provinceId = $this.find(".checkBoxFather > em span").text();
                                        if (item.clevel == "3") {
                                            if (item.cid == provinceId) {
                                                $this.find(".wrap label").addClass("cur");
                                            }
                                        }
                                        else {
                                            if (item.provinceid == provinceId)
                                            {
                                                $this.find("table td").each(function(){
                                                    var cityId = $(this).find("em span").text();
                                                    if (item.cid == cityId)
                                                    {
                                                        $(this).find(".wrap label").addClass("cur");
                                                        $this.find(".checkBoxFather > .wrap label").addClass("cur");
                                                        var selectNum = $this.find("table .cur").length;
                                                        if ($this.find('table .checkbox').length == selectNum) {//判断城市是否全选来确定省是否选中                   
                                                            $this.find('.checkBoxFather').find('.number').removeClass('show');
                                                        } else {
                                                            $this.find('.checkBoxFather').find('.number').addClass('show').find('.selected').text(selectNum)//选中城市数量输入到num里
                                                        }
                                                    }                                                
                                                })                                                
                                            }
                                        }
                                    })                                   
                                }                                
                            }                                                       
                        })
                    }
                    //else if (item.clevel == "3")
                    //{
                    //    $(".regionalOrientation .area .region").each(function () {
                    //        var $that = $(this);
                    //        if (item.cid = $that.find("dt em span").text()) {
                    //            $that.find("dt .wrap label").addClass("cur");
                    //        }
                    //    })
                    //}
                    //else if (item.clevel == "4")
                    //{
                    //    $(".regionalOrientation .area .region").each(function () {
                    //        var $that = $(this);
                    //        if (item.cid = $that.find("dt em span").text()) {
                    //            $that.find("dt .wrap label").addClass("cur");
                    //        }
                    //    })
                    //}
                })
            }


            //定向设置列表
            if (matchData != null&&matchData.length>0) {
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
                    $(this).parent().find("dd").removeClass("cur");
                    $(this).addClass("cur");
                    var $txt = $(this).find("em").text();
                    $(this).parents('.slide').prev().find('.title').text($txt);
                    $(this).parents('.slide').slideUp();
                    $('.triangle').removeClass('open');
                    $('.slidebox').removeClass('is-open')
                    var directTypeId = $(this).find("span").text();
                    SetOthersDirect(directTypeId, directArray, matchArray);
                })
                //匹配列表
                var directTypeId = directData[0].directtypeid;
                SetOthersDirect(directTypeId, directData, matchData);
                directArray = directData;
                matchArray = matchData;

            } else {
                $(".nonelimit").find("label").click();
                GetDirectTypeData();
            }

            $(".ad-name").val(infoData.adname);
            $(".ad-price").val(infoData.price);              
            //投放时间
            if (infoData.putstarttime != "") $("#starttime input").val(infoData.putstarttime);
            if (infoData.putendtime != "" && infoData.putendtime != "2099/12/31 0:00:00" && infoData.putendtime != "2099-12-31 0:00:00") {
                $("#endtimeckbox label").click();
                $("#endtime input").val(infoData.putendtime);
            }
            //投放量           
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
            if (materialData != null && materialData.length>0) {
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
                        trHtml.push("<td>" + item.format + "</td>");
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
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");
                        trHtml.push("<td>" + item.format + "</td>");
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
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td width='35%' class='case' ><h3>" + item.title + "</h3><p>" + item.remark + "</p><button>" + item.canceltext + "</button><button>" + item.confirmtext + "</button></td>");
                        trHtml.push("<td></td>");
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
                        trHtml.push("<td>" + item.materialname + "<span style='display:none' class='materialid'>" + item.materialid + "</span></td>");
                        trHtml.push("<td class='txt' ><p>" + item.title + "</p><img src='" + item.imageurl + "' height='71' width='88' ></td>");
                        trHtml.push("<td>" + item.width + "*" + item.height + "</td>");
                        trHtml.push("<td>" + item.format + "</td>");
                        trHtml.push("<td width='150'><button class='del list_removebtn'>移除</button></td>");
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

                    //移除
                    $(".list_removebtn").click(function () {
                        $(this).closest("tr").remove();
                    })
                })
            }


        }

    })
}
//新增广告

function InsertData(status) {

    //广告主
    var sAdUserId = $("#info_aduser .cur").find("span").text();
    if (!CheckDataValid("广告主", sAdUserId, "1,2")) {
        return;
    }
    
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
    
    var sIsBottom = "0";    
    var sIsBid = "0";
    var sPageId = "0";
    var sAdLocationId = "0";
    var putRange = new Array();
    //是否打底
    //否
    if ($("#info_isbottom .isbottom_n").eq(0).find("label").hasClass("cur")) {
        sIsBottom = "0";
        //是否竞价
        //否
        if ($("#info_isbid .change").eq(0).find("label").hasClass("cur")) {
            sIsBid = "0";
            //页面列表
            sPageId = $("#info_page .cur").find("span").text(); //$()        
            if (!CheckDataValid("页面列表", sPageId, "1,2")) {
                return;
            }
            //广告位
            sAdLocationId = $("#info_adlocation .cur").find("span").text();
            if (!CheckDataValid("广告位", sAdLocationId, "1,2")) {
                return;
            }
        }
            //是
        else {
            sIsBid = "1";
            sPageId = "0";
            sAdLocationId = "0";
            //投放范围
            $("#info_putrange td").each(function () {
                //判断是否选中
                if ($(this).find("label").hasClass("cur")) {
                    var pageId = $(this).find(".page_id").text();
                    if (pageId != "") {
                        var putItem = { pid: TransDataToInt(pageId) };
                        putRange.push(putItem);
                    }
                }
            })
            if (putRange.length == 0) {
                alert("请选择投放范围!");
                return false;
            }

        }
    }
    else {
        sIsBottom = "1";
    }
    
    //广告名称
    var sAdName = $("#info_adname").val();
    if (!CheckDataValid("广告名称", sAdName, "1")) {
        return;
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
    ////广告权重
    //var sAdWeight = $("#info_adlocation .cur").find("span").text();
    //if (!CheckDataValid("广告位", sAdLocationId, "1,2")) {
    //    return;
    //}

    var sAccountId = _accountId;
    //转换数据
    sPageId = TransDataToInt(sPageId);
    sAdLocationId = TransDataToInt(sAdLocationId);
    sAdTypeId = TransDataToInt(sAdTypeId);
    sPrice = TransDataToFloat(sPrice);
    sAdUserId = TransDataToInt(sAdUserId);
    sAccountId = TransDataToInt(sAccountId);
    sIsBid = TransDataToInt(sIsBid);
    sSubAdTypeId = TransDataToInt(sSubAdTypeId);
    sIsBottom = TransDataToInt(sIsBottom);
    sBType = TransDataToInt(sBType);
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
        status: status,
        aduserid: sAdUserId,
        accountid: sAccountId,
        isbid: sIsBid,
        adweight: 1,
        subadtypeid: sSubAdTypeId,
        isbottom: sIsBottom,
        billingtype:sBType
    };
    //时间定向
    //  var dateData = new Array();
    var weekData = new Array();
    var sDateType = "1";
    $(".regionalOrientation .box2 .checkbox-con").each(function () {
        if ($(this).find("label").hasClass("cur")) {
            sDateType = $(this).index();
            return false;
        }
    })
    sDateType = TransDataToInt(sDateType);
    if (sDateType == 2) {
        $(".regionalOrientation .timeBox table tr").each(function () {
            var $that = $(this);
            var week = $that.index();
            if ($that.find(".on").length > 0) {
                var hourData = new Array();
                var sHour = null;
                var eHour = null;

                $that.find("td").each(function () {
                    if ($(this).hasClass("on")) {
                        if (null == sHour) {
                            sHour = $(this).index();
                            eHour = $(this).index();
                        }
                        else {

                            eHour = $(this).index();
                        }
                        if ($(this).index() == $that.find("td").length - 1) {
                            if (sHour != null) {
                                var hourItem = { shour: sHour, ehour: eHour };
                                hourData.push(hourItem);
                                sHour = null;
                                eHour = null;
                            }
                        }
                    }
                    else {
                        if (sHour != null) {
                            var hourItem = { shour: sHour, ehour: eHour };
                            hourData.push(hourItem);
                            sHour = null;
                            eHour = null;
                        }
                    }
                })
                var weekItem = { week: week, hourdata: hourData };
                weekData.push(weekItem);
            }
        })
        if (weekData == null || weekData.length == 0) {
            alert("请选择定向日期明细");
            return;
        }
    }
    var dateData = {
        datetype: sDateType,
        weekdata: weekData
    };

    //物料信息
    var materialData = new Array();
    var materialBool = true;
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

    //其他设置-定向
    var systemData = new Array();
    var classData = new Array();
    var mediaData = new Array();
    var    directBool = true;
    if ($(".oslimit label").hasClass("cur")) {
        $.each(directArray, function (index, item) {
            directBool = false;
            $.each(matchArray, function (k, p) {
                if (item.directtypeid == p.directtypeid) {
                    directBool = true;
                    var directTypeData = p.directtypedata;
                    if (p.directtypeid == "1") {
                        $.each(directTypeData, function (m, n) {
                            var systemItem = {
                                systemid: TransDataToInt(n.directid)
                            };
                            systemData.push(systemItem);
                        })
                    }
                    else if (p.directtypeid == "2") {
                        $.each(directTypeData, function (m, n) {
                            var classItem = {
                                classid: TransDataToInt(n.directid)
                            };
                            classData.push(classItem);
                        })
                    }
                    else if (p.directtypeid == "7") {
                        $.each(directTypeData, function (m, n) {
                            var mediaItem = {
                                mediaid: TransDataToInt(n.directid)
                            };
                            mediaData.push(mediaItem);
                        })
                    }
                    else {
                    }
                }
            })
            if (!directBool) {
                alert("你未选择" + item.directtypename);
                return false;
            }
        })
    }
    if (!directBool) {
        return;
    }
    //地域定向
    var areaData = new Array();
    //是否选择地域定向-不选则为全国通投    
    if ($('.regionalOrientation .box1').find(".checkbox-con").eq(1).find("label").hasClass("cur"))
    {
        //判断是否选择
        if ($(".regionalOrientation .area .region").find(".cur").length > 0) {
            $(".regionalOrientation .area .region").each(function () {
                var $that=$(this);
                var index=$(this).index();
                //全国及区域                       
                if ($that.find("dt .wrap label").hasClass("cur")) {
                    var tempId = $that.find("dt em span").text();
                    var areaItem = { cityid: TransDataToInt(tempId) };
                    areaData.push(areaItem);
                    //全国
                    if (tempId == "0") {
                        return false;
                    }
                }
                else {
                    //省份
                    $that.find("dd").each(function () {
                        var $this = $(this);
                        //判断是否选中
                        if ($this.find(".checkBoxFather > .wrap label").hasClass("cur")) {
                            //判断城市是否全选
                            if ($this.find("table .wrap label").length == $this.find("table .wrap .cur").length) {
                                var tempId = $this.find(".checkBoxFather > em span").text();
                                var areaItem = { cityid: TransDataToInt(tempId) };
                                areaData.push(areaItem);
                            }
                            else {
                                $this.find("table td").each(function () {                                   
                                    if ($(this).find(".wrap label").hasClass("cur")) {                                    
                                        var tempId = $(this).find("em span").text();
                                        var areaItem = { cityid: TransDataToInt(tempId) };
                                        areaData.push(areaItem);
                                    }
                                })
                            }
                        }
                    })
                }
            })
        }
        else {
            alert("你忘了选择投放的地区");
            return;
        }
    }
 
    var data = {
        infodata: infoData,
        materialdata: materialData,
        systemdata: systemData,
        areadata: areaData,
        classdata: classData,
        datedata: dateData,
        putrange: putRange,
        mediadata:mediaData
    };
    var ajaxData = {
        method: "InsertAdData",
        data: JSON.stringify(data)
    };   
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert("没有执行成功，请稍后重试");
            else location.href = "ad_info.aspx";
        }
    });

}

//修改
function UpdataData(adId) {
    var sAdId = adId;    
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
    var sIsBottom = "0";
    var sIsBid = "0";
    var sPageId = "0";
    var sAdLocationId = "0";
    var putRange = new Array();
    //是否打底
    //否
    //只有一个
    if ($("#info_isbottom .isbottom_n").find("label").hasClass("cur")) {
        sIsBottom = "0";
        //是否竞价
        //否
        if ($("#info_isbid .change").eq(0).find("label").hasClass("cur")) {
            sIsBid = "0";
            //页面列表
            sPageId = $("#info_page .cur").find("span").text(); //$()        
            if (!CheckDataValid("页面列表", sPageId, "1,2")) {
                return false;
            }
            //广告位
            sAdLocationId = $("#info_adlocation .cur").find("span").text();
            if (!CheckDataValid("广告位", sAdLocationId, "1,2")) {
                return false;
            }
        }
            //是
        else {
            sIsBid = "1";
            sPageId = "0";
            sAdLocationId = "0";
            //投放范围
            $("#info_putrange td").each(function () {
                //判断是否选中
                if ($(this).find("label").hasClass("cur")) {
                    var pageId = $(this).find(".page_id").text();
                    if (pageId != "") {
                        var putItem = { pid: TransDataToInt(pageId) };
                        putRange.push(putItem);
                    }
                }
            })
            if (putRange.length == 0) {
                alert("请选择投放范围!");
                return false;
            }

        }
    }
    else {
        sIsBottom = "1";
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
        sPutMax = TransDataToInt(sPutMax);
    }
    //投放上限每日
    var sPutMaxByDay = "";
    if ($("#info_putmaxbyday input[type='checkbox']").attr("checked")) {
        sPutMaxByDay =$("#info_putmaxbyday input[type='text']").val();
        if (!CheckDataValid("投放上限每日", sPutMaxByDay, "1,2")) {
            return;
        }
        sPutMaxByDay = TransDataToInt(sPutMaxByDay);
    }
    //广告主
    var sAdUserId = $("#info_aduser .cur").find("span").text();
    if (!CheckDataValid("广告主", sAdUserId, "1,2")) {
        return;
    }
    var sAccountId = _accountId;
    var sFlag="1";
    //转换数据
    sAdId = TransDataToInt(sAdId);
    sPageId = TransDataToInt(sPageId);
    sAdLocationId = TransDataToInt(sAdLocationId);
    sAdTypeId = TransDataToInt(sAdTypeId);
    sPrice = TransDataToFloat(sPrice);    
    if (_price != "0" && sPrice != TransDataToFloat(_price)) {
        sFlag = "0";
    }
    sAdUserId = TransDataToInt(sAdUserId);
    sAccountId = TransDataToInt(sAccountId);
    sIsBid = TransDataToInt(sIsBid);
    sSubAdTypeId = TransDataToInt(sSubAdTypeId);    
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
        putmaxbyday: sPutMaxByDay,
        aduserid: sAdUserId,
        accountid: sAccountId,
        isbid: sIsBid,
        adweight: 1,
        subadtypeid: sSubAdTypeId
    };
    //时间定向
  //  var dateData = new Array();
    var weekData = new Array();
    var sDateType="1";
    $(".regionalOrientation .box2 .checkbox-con").each(function () {
        if ($(this).find("label").hasClass("cur")) {
            sDateType = $(this).index();
            return false;
        }
    })
    sDateType = TransDataToInt(sDateType);
    if (sDateType == 2) {
        $(".regionalOrientation .timeBox table tr").each(function () {
            var $that = $(this);
            var week = $that.index();
            if ($that.find(".on").length > 0) {
                var hourData = new Array();
                var sHour = null;
                var eHour = null;

                $that.find("td").each(function () {
                    if ($(this).hasClass("on")) {                        
                        if (null == sHour) {                            
                            sHour = $(this).index();
                            eHour = $(this).index();
                        }
                        else {
                            
                            eHour = $(this).index();
                        }
                        if ($(this).index() == $that.find("td").length - 1) {
                            if (sHour != null) {
                                var hourItem = { shour: sHour, ehour: eHour };
                                hourData.push(hourItem);
                                sHour =null;
                                eHour = null;
                            }
                        }
                    }
                    else {
                        if (sHour != null) {
                            var hourItem = { shour: sHour, ehour: eHour };
                            hourData.push(hourItem);
                            sHour = null;
                            eHour = null;
                        }
                    }
                })
                var weekItem = { week: week, hourdata: hourData };
                weekData.push(weekItem);
            }
        })
        if (weekData == null||weekData.length==0) {
            alert("你忘了选择定向日期明细");
            return;
         }
    }
    var dateData = {
        datetype: sDateType,
        weekdata: weekData
    };

    //权重
    //var sWeightType = $("#info_weighttype .cur").find("span").text();
    //var sWeight = $("#info_weight input").val();
    //物料信息
    var materialData = new Array();
    var materialBool = true;
    $(".materialtable tr").each(function () {
        var materialId = $(this).find(".materialid").text()
        if (!CheckDataValid("物料信息", materialId, "1,2")) {
            materialBool = false;
            return;
        }        
        var weightType = $("#info_weighttype .cur").find("span").text();
        var weight = "";
        if (weightType == "1") {
            weight = "1";
        }
        else {
            weight = $(this).find(".manual").find("select").val();
            //weight = $(this).find(".weight").val();
            
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
    //其他设置-定向
    var systemData = new Array();
    var classData = new Array();
    var mediaData = new Array();
    var directBool = true;
    if ($(".oslimit label").hasClass("cur")) {
        $.each(directArray, function (index, item) {
            directBool = false;
            $.each(matchArray, function (k, p) {
                if (item.directtypeid == p.directtypeid) {
                    directBool = true;
                    var directTypeData = p.directtypedata;
                    if (p.directtypeid == "1") {
                        $.each(directTypeData, function (m, n) {
                            var systemItem = {
                                systemid: TransDataToInt(n.directid)
                            };
                            systemData.push(systemItem);
                        })
                    }
                    else if (p.directtypeid == "2") {
                        $.each(directTypeData, function (m, n) {
                            var classItem = {
                                classid: TransDataToInt(n.directid)
                            };
                            classData.push(classItem);
                        })
                    }
                    else if (p.directtypeid == "7") {
                        $.each(directTypeData, function (m, n) {
                            var mediaItem = {
                                mediaid: TransDataToInt(n.directid)
                            };
                            mediaData.push(mediaItem);
                        })
                    }
                    else {
                    }
                }
            })
            if (!directBool) {
                alert("你忘了选择" + item.directtypename);
                return false;
            }
        })
    }
    if (!directBool) {
        return;
    }
   // return;
    //地域定向
    var areaData = new Array();
    //是否选择地域定向-不选则为全国通投    
    if ($('.regionalOrientation .box1').find(".checkbox-con").eq(1).find("label").hasClass("cur")) {
        //判断是否选择
        if ($(".regionalOrientation .area .region").find(".cur").length > 0) {
            $(".regionalOrientation .area .region").each(function () {
                var $that = $(this);
                var index = $(this).index();
                //全国及区域                       
                if ($that.find("dt .wrap label").hasClass("cur")) {
                    var tempId = $that.find("dt em span").text();
                    var areaItem = { cityid: TransDataToInt(tempId) };
                    areaData.push(areaItem);
                    //全国
                    if (tempId == "0") {
                        return false;
                    }
                }
                else {
                    //省份
                    $that.find("dd").each(function () {
                        var $this = $(this);
                        //判断是否选中
                        if ($this.find(".checkBoxFather > .wrap label").hasClass("cur")) {
                            //判断城市是否全选
                            if ($this.find("table .wrap label").length == $this.find("table .wrap .cur").length) {
                                var tempId = $this.find(".checkBoxFather > em span").text();
                                var areaItem = { cityid: TransDataToInt(tempId) };
                                areaData.push(areaItem);
                            }
                            else {
                                $this.find("table td").each(function () {
                                    if ($(this).find(".wrap label").hasClass("cur")) {
                                        var tempId = $(this).find("em span").text();
                                        var areaItem = { cityid: TransDataToInt(tempId) };
                                        areaData.push(areaItem);
                                    }
                                })
                            }
                        }
                    })
                }
            })
        }
        else {
            alert("你忘了选择投放的地区");
            return;
        }
    }

    var data = {
        infodata: infoData,
        materialdata: materialData,
        systemdata: systemData,
        areadata: areaData,
        classdata: classData,
        datedata: dateData,
        putrange: putRange,        
        mediadata:mediaData
    };  
    var ajaxData = {
        method: "UpdateAdData",
        data: JSON.stringify(data),
        flag: sFlag
    };   
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert("没有执行成功，请稍后重试");
            else location.href = "ad_info.aspx";
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
        method: "GetSubAdTypeData",
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
                    tableHtml.push("</dd>");

                })
                $("#info_subadtype .title").text(defaultName);
                $("#info_subadtype .slide dl").html(tableHtml.join(''));
                $('#info_subadtype .slide dd').click(function () {
                    var $txt = $(this).find("em").text();
                    $(this).removeClass('cur').parents('.slide').prev().find('.title').text($txt);
                    $(this).parents('.slide').slideUp();
                    $('.slidebox').removeClass('is-open')
                    $('.triangle').removeClass('open');
                    $(this).closest(".slide").find("dd").removeClass("cur");
                    $(this).addClass("cur");
                })
                //广告形式点击
                $("#info_subadtype dl dd").click(function () {
                    //竞价选择
                    //if ($('#info_isbid .checkbox-con').eq(1).find('.checkbox').hasClass('cur'))
                    //  {
                    var subAdTypeId = $(this).find("span").text();
                    GetPutRangeBySubAdType(subAdTypeId, "");
                    //}    
                })
            }
        }
    });

}

//获取定向类型对应的列表数据-新增广告时
function GetDirectTypeData() {
    var ajaxData = { method: "GetDirectTypeData" };
    $.ajax({
        url: "/api/ManagerHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {

            if (result.errcode != "1") {
                alert(result.errmsg)
                return;
            }
            var directData = result.data.directdata;
            var matchData = new Array();
            var directTypeId = directData[0].directtypeid;
            SetOthersDirect(directTypeId, directData, matchData);
            $("#info_others .slide dd").unbind("click");
            $("#info_others .slide dd").click(function () {
                $(this).parent().find("dd").removeClass("cur");
                $(this).addClass("cur");
                var $txt = $(this).find("em").text();
                $(this).parents('.slide').prev().find('.title').text($txt);
                $(this).parents('.slide').slideUp();
                $('.triangle').removeClass('open');
                $('.slidebox').removeClass('is-open')
                var directTypeId = $(this).find("span").text();
                SetOthersDirect(directTypeId, directData, matchData);
            })
            directArray = directData;
            matchArray = matchData;
        }
    });

}
//匹配投放范围
//adId 广告id
//putrange 投放范围 array
function GetPutRangeBySubAdType(subAdTypeId, putRange)
{
    if (subAdTypeId == "")
        subAdTypeId = "1";
    //分页待定
    var ajaxData = {
        method: "GetPageListBySubAdType",
        subadtypeid: subAdTypeId
    };
    $.ajax({
        url: "/api/BidHandler.ashx",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {
            if (result.errcode == "-1") alert(result.errmsg);
            var listData = result.data;
            $("#info_putrange table tr").remove(); 
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
                    //判断是否选中页面
                    var isChecked = false;
                    if (putRange != null && putRange.length > 0)
                    {
                        $.each(putRange, function (p, k) {
                            if (item.pageid == k.pid) {
                                isChecked = true;
                                return false;
                            }
                        })
                    }                  
                    tempLength++;
                    if (index == 0 || item.termid != tempId) {
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
                        trHtml.push("<em>" + item.termname + "</em>");
                        trHtml.push("</div>");
                        trHtml.push("</td>");

                        trHtml.push("<td width='18%'>");
                        trHtml.push("<div class='checkbox-con clearfix'>");
                        trHtml.push("<span class='wrap'>");
                        if (isChecked) {
                            trHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'>");
                            trHtml.push("<label class='checkbox cur'></label>");
                        }
                        else {
                            trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                            trHtml.push("<label class='checkbox'></label>");
                        }
                        trHtml.push("</span>");
                        trHtml.push("<em>" + item.pagename + "<span style='display:none;' class='page_id'>" + item.pageid + "</span></em>");
                        trHtml.push("</div>");
                        trHtml.push("</td>");

                    }
                    else {
                        if (tempLength >= tdLength) {
                            trHtml.push("</tr>");
                            trHtml.push("<tr>");
                            trHtml.push("<td></td>");
                            tempLength = 0;
                        }
                        trHtml.push("<td width='18%'>");
                        trHtml.push("<div class='checkbox-con clearfix'>");
                        trHtml.push("<span class='wrap'>");
                        if (isChecked) {
                            trHtml.push("<input type='checkbox' class='ipt-hide' checked='checked'>");
                            trHtml.push("<label class='checkbox cur'></label>");
                        }
                        else {
                            trHtml.push("<input type='checkbox' class='ipt-hide' checked=''>");
                            trHtml.push("<label class='checkbox'></label>");
                        }
                        trHtml.push("</span>");
                        trHtml.push("<em>" + item.pagename + "<span style='display:none;' class='page_id'>" + item.pageid + "</span></em>");
                        trHtml.push("</div>");
                        trHtml.push("</td>");

                    }
                    //最后一个
                    if (index == listData.length - 1) {
                        trHtml.push("</tr>");
                    }
                    tempId = item.termid;
                })
                $("#info_putrange table").append(trHtml.join(""));
                
            }
            else {
                var trHtml = [];
                trHtml.push("<tr>");
                trHtml.push("<td><p style='width:100%; line-height:100px; text-align:center; color:black;'>没有找到能匹配的页面</p></td>");
                trHtml.push("<tr>");
                $("#info_putrange table").append(trHtml.join(""));
            }
            rangeChoice();
        }
    });    
}
//投放范围事件
function rangeChoice() {//广告页选择
    var all = $('#info_putrange .slidebox tr:first-child').find('label'),
         checkcon = $('#info_putrange .slidebox').find('.checkbox');
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
//获取定向类型对应的列表数据-地域定向
function GetRegionData() {
    var ajaxData = { userid:"" };
    $.ajax({
        url: "/api/Manager.asmx/GetRegionData",
        type: "post",
        data: ajaxData,
        dataType: "json",
        success: function (result) {

            if (result.errcode != "1") {
                alert(result.errmsg)
                return;
            }
            var countryData = result.data.countrydata;
            var areaData = result.data.areadata;
            var provinceData = result.data.provincedata;
            var cityData = result.data.citydata;
            var othersData = result.data.othersdata;
            var regionList = new Array();
            //全国
            $.each(countryData, function (index, item) {
                var regionItemList = new Array();
                var regionItem = { name: item.cname, value: item.cid, regionItemList: regionItemList };
                regionList.push(regionItem);
            })
            //区域-省份
            $.each(areaData, function (index, item) {
                var regionItemList = new Array();
                //省份
                $.each(provinceData, function (p, k) {
                    if (item.cid == k.areaid) {
                        var tempItem = { name: k.cname, value: k.cid };
                        regionItemList.push(tempItem);
                    }
                })
                var regionItem = { name: item.cname, value: item.cid, regionItemList: regionItemList };
                regionList.push(regionItem);
            })
            $.each(othersData, function (index, item) {
                var regionItemList = new Array();
                var regionItem = { name: item.cname, value: item.cid, regionItemList: regionItemList };
                regionList.push(regionItem);
            })
         //   alert(JSON.stringify(regionList));
            var $html = '';
            for (var i = 0; i < regionList.length; i++) {
                if (i == 0) {
                    $html += '<dl class="region"><dt><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox china"></label></span><em>' + regionList[i].name + '<span class="hide">' + regionList[i].value + '</span></em></div></dt></dl>'
                }
                else if(regionList[i].value=="-1")
                {
                    $html += '<dl class="region"><dt><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox others"></label></span><em>' + regionList[i].name + '<span class="hide">' + regionList[i].value + '</span></em></div></dt></dl>'
                }
                else {
                    $html += '<dl class="region"><dt><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox"></label></span><em>' + regionList[i].name + '<span class="hide">' + regionList[i].value + '</span></em></div></dt>'
                    for (var k = 0; k < regionList[i].regionItemList.length; k++) {
                        $html += '<dd><div class="checkbox-con checkBoxFather"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox"></label></span><em data-name="' + regionList[i].regionItemList[k].value + '">' + regionList[i].regionItemList[k].name + '<span class="hide">' + regionList[i].regionItemList[k].value + '</span></em><div class="number"><span class="selected">0</span>/<span class="all">5</span></div></div></dd>';
                    }
                    $html += '</dl>';
                }
            }
            $('.setDirect .area .fill').after($html);
            $('.setDirect .area').find('dl:even').addClass('add');

            $('.setDirect .region dd').each(function () {
                var provinceId = $(this).find("em span").text();
                //城市
                var html_count = 0;
                var city_html = '<table cellspacing="0" border="0" ><tbody>';
              
                city_html += '<tr >';

                $.each(cityData, function (index, item) {
                    if (item.provinceid == provinceId) {
                        html_count++;
                        city_html += '<td><div class="checkbox-con"><span class="wrap"><input type="checkbox" class="ipt-hide" checked=""><label class="checkbox"></label></span><em>' + item.cname + '<span class="hide">' + item.cid + '</span></em></div></td>';
                        if (html_count > 0 && html_count % 2 == 0) {
                            city_html += "</tr>";
                        }
                    }
                })

                if ((html_count % 2 != 0)) {
                    city_html += "<td></td>";
                    city_html += "</tr>";
                }
                city_html += '</tbody></table>';
                $(this).find("em").after(city_html);                
            })

            //城市                    
            RegionDataInit();
            // GetRegionOfCityData("6");
            
        }
    });

}




//地域事件初始化
function RegionDataInit() {
    //区域定向-顶部选择是否定向
    $('.regionalOrientation .box1').find('.checkbox-con').each(function () {
        var that = $(this);
        var index = $(this).index();
        that.find('.checkbox').click(function () {
            $(this).addClass('cur').parents('.checkbox-con').siblings().find('.checkbox').removeClass('cur');
            $('.regionalOrientation .area').find('.fill').removeClass('on')
            if (index == 1) {
                $('.regionalOrientation .area').fadeOut();
            } else if (index == 3) {
                $('.regionalOrientation .area').fadeIn();
                $('.regionalOrientation .area').find('.fill').addClass('on')
            } else {

                $('.regionalOrientation .area').fadeIn();
            }
        })
    })
    //定向地域内容选择
    $('.region').each(function () {//点击每个区域全选或不全选
        var that = $(this);
        var num, selectNum;
        var _length = that.find('dd .checkbox').length;
        
       
        if (that.index() == 1)
        {            
            //全国选择-所有都选择
            that.find('dt .checkbox').click(function (event) {
                if ($(this).hasClass('cur')) {
                    $(this).parents('.area').find('.checkbox').removeClass('cur');                  
                    $(this).parents('.area').find('.number').removeClass('show')//数量隐藏
                } else {
                    $(this).parents('.area').find('.checkbox').addClass('cur');
                    $(this).parents('.area').find('.number').removeClass('show')//数量隐藏
                }             
            })
        }
        ////其他
        //else if (that.find("dt .checkbox-con em span").text()=="-1") {
            
        //    that.find('dt .checkbox').click(function (event) {
        //        if ($(this).hasClass('cur')) {
        //            $(this).removeClass('cur');
        //        } else {
        //            $(this).addClass('cur');
        //        }
        //    })
        //}
        else {
            //区域选择-省联动效果
            that.find('dt .checkbox').click(function (event) {
                if ($(this).hasClass('cur')) {
                    $(this).removeClass('cur').parents('dt').siblings('dd').find('.checkbox').removeClass('cur');
                    //$(this).parents('.checkbox-con').find('table').hide();
                    $(this).parents('dt').siblings('dd').find('.number').removeClass('show')//数量隐藏
                } else {
                    $(this).addClass('cur').parents('dt').siblings('dd').find('.checkbox').addClass('cur');
                    //$(this).parents('.checkbox-con').find('table').show();
                    $(this).parents('dt').siblings('dd').find('.number').removeClass('show')//数量隐藏
                }
                check();
            })
        }

        //鼠标经过省份显示所有城市
        that.find('.checkBoxFather').hover(function (event) {
            $(this).find('table').show();
        }, function () {
            $(this).find('table').hide();
        })
        //点击每个省-选框
        that.find('.checkBoxFather > .wrap .checkbox').click(function (event) {//点击每个省
            if ($(this).hasClass('cur')) {
                $(this).removeClass('cur');
                $(this).parents('.checkBoxFather').find('table').find(".checkbox").removeClass('cur');
                $(this).parents('.checkBoxFather').find('.number').removeClass('show')//数量隐藏
            } else {

                $(this).addClass('cur');
                $(this).parents('.checkBoxFather').find('table').find(".checkbox").addClass('cur');
                $(this).parents('.checkBoxFather').find('.number').removeClass('show')//数量隐藏
            }
            check();
            event.stopPropagation();
        })

        that.find('dd').each(function () {//轮询每个省份的城市数量
            num = $(this).find('table').find('td .checkbox-con').length;
            $(this).find('.all').text(num);
        })
        //点击每个城市
        that.find('.checkBoxFather table .checkbox').click(function (event) {
            if ($(this).hasClass('cur')) {
                $(this).removeClass('cur')
            } else {
                $(this).addClass('cur')
            }
            selectNum = $(this).parents('table').find('.cur').length; //获取城市选中数量
            if (selectNum == 0) {
                $(this).parents('table').prev().prev().find('label').removeClass('cur');
                $(this).parents('.checkBoxFather').find('.number').removeClass('show');
            }
            else {
                $(this).parents('table').prev().prev().find('label').addClass('cur');
                if ($(this).parents('table').find('.checkbox').length == selectNum) {//判断城市是否全选来确定省是否选中                   
                    $(this).parents('.checkBoxFather').find('.number').removeClass('show');
                } else {                    
                    $(this).parents('.checkBoxFather').find('.number').addClass('show').find('.selected').text(selectNum)//选中城市数量输入到num里
                }
            }
            check();
            event.stopPropagation();
        })

        function check() {//判断每个区域内的城市是否全部选中，全部选中则该区域选中，反之亦然          
            if (that.find('dd .cur').length > 0)
            {
                if (that.find('dd .cur').length == that.find('dd .checkbox').length) {
                    that.find('dt .checkbox').addClass('cur');
                } else {
                    that.find('dt .checkbox').removeClass('cur');
                }
            }         
            //待修改         
            if ($(".regionalOrientation .area .region").find("dt").find(".checkbox").not(".china").length == $(".regionalOrientation .area .region").find("dt").find(".cur").not(".china").length) {
                $(".china").addClass('cur');
            }
            else {
                $(".china").removeClass('cur');
            }
            //  if(that.parent(""))
            //if ($('.regionalOrientation .area .region:not(":last-child")').find('.cur').length == $('.regionalOrientation .area .region:not(":last-child")').find('.checkbox').length) {
            //    $('.china').addClass('cur');
            //} else {
            //    $('.china').removeClass('cur');
            //}
        }       

    })
}
//

GetRegionData();
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
    var sAdUserId = $("#info_aduser .cur").find("span").text();
    if (!CheckDataValid("广告主", sAdUserId, "1,2")) {
        return;
    }
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
                        trHtml.push("<p>"+item.remark+"</p>");
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
                                trHtml.push("<td>" + item.format + "</td>");
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
                                trHtml.push("<td>" + item.format + "</td>");
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
                                trHtml.push("<td>" + item.format + "</td>");
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
//广告页点击
$("#info_page dl dd").click(function () {
    var pageId = $(this).find("span").text();    
    GetAdLocationByPage(pageId, "");
})

//广告类型点击
$("#info_adtype dl dd").click(function () {
    var adTypeId = $(this).find("span").text();    
    GetSubAdTypeByAdType(adTypeId, "");
})
//广告形式点击
$("#info_subadtype dl dd").click(function () {
    //竞价选择
    //if ($('#info_isbid .checkbox-con').eq(1).find('.checkbox').hasClass('cur'))
  //  {
        var subAdTypeId = $(this).find("span").text();
        GetPutRangeBySubAdType(subAdTypeId, "");
    //}    
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
        userid: "",
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
    var sAdUserId = $("#info_aduser .cur").find("span").text();
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
        var sRemark = $(".textarea textarea").val();
        if (!CheckDataValid("文本内容", sRemark, "1")) {
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
            remark: sRemark,
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

