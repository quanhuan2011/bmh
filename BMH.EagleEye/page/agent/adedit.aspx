<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adedit.aspx.cs" Inherits="BMH.EagleEye.page.agent.adedit" %>

<!DOCTYPE html>
<html>

<head>
    <title>鹰眼广告管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务" />
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。" />
    <link rel="stylesheet" href="/page/css/normalize.css" />
    <link rel="stylesheet" href="/page/css/style-1.css" />
    <link rel="stylesheet" type="text/css" href="/page/css/jquery.datetimepicker.css" />
    <style type="text/css">
        .enable {
            background: #12bdce !important;
        }
    </style>
</head>

<body>
    <!--[if lt IE 10]>
        <style></style>
        <div class="browser-happy">
          <div class="content">您正在使用ie浏览器版本太老，本页面的显示效果可能不佳，建议您升级到ie10及以上。
            <a href="http://browsehappy.com/" target="_blank">立即更新</a></div>
        </div>
      <![endif]-->
    <div id="YY-head">
        <div class="w1230 clearfix">
            <div class="logo fl"></div>
            <div class="personInfo fr">
                <div class="set-area">
                    <a href="/page/logout.aspx" class="exit">退出</a>
                </div>
                <p class="set fr">设置</p>
                <span class="line fr">|</span>
                <span class="name fr"><%=accountName %></span>
                <img src='<%=headImageUrl %>' height="51" width="51" alt="" class="head fr">
            </div>
        </div>
    </div>
    <!-- YY-head end -->
   
    <div class="w1230 set-platform" id="YY-main">
        <div class="basic-info clearfix">
            <h2>基本信息</h2>
            <ul>
                <li class="fl" id="info_adtype">
                    <div class="slidebox slidebox2 fl">
                        <span class="fl">广告类型：</span>
                        <div class="choice fl">
                            <div class="box">
                                <span class='title'>
                                    <%=typeDefault %></span>
                                <div class="sanjiao">
                                    <span class="triangle"></span>
                                </div>
                            </div>
                            <div class="slide">
                                <dl>
                                    <%=typeList %>
                                </dl>
                            </div>
                        </div>
                        <em class='red'>（必填*）</em>
                    </div>
                </li>

                <li class="fl" id="info_subadtype">
                    <div class="slidebox slidebox2 fl">
                        <span class="fl">广告形式：</span>
                        <div class="choice fl">
                            <div class="box">
                                <span class='title'>
                                    <%=subAdTypeDefault %></span>
                                <div class="sanjiao">
                                    <span class="triangle"></span>
                                </div>
                            </div>
                            <div class="slide">
                                <dl>
                                    <%=subAdTypeList %>
                                </dl>
                            </div>
                        </div>
                        <em class='red'>（必填*）</em>
                    </div>
                </li>
                <div style="height: 0; clear: left"></div>
                <li class="name">
                    <label class="fl">广告名称：</label>
                    <input class="ad-name fl" id="info_adname" type="text" placeholder="少于130个字符" value="">
                    <em class='red'>（必填*）</em></li>
                <div style="clear: left"></div>
                <li class="fl" style="display: inline-block" id="info_term">
                    <label class="fl">投放终端：</label>
                    <div class="change checkbox-con term_h5" style="margin-left: 10px; margin-right: 10px;">
                        <span class="wrap">
                            <input type="radio" class="ipt-hide" checked="">
                            <label class="checkbox cur"></label>
                        </span>
                        <em>H5</em>
                    </div>   
                     <div class="change checkbox-con term_app"  style="margin-left: 10px;" >
                        <span class="wrap">
                            <input type="radio" class="ipt-hide" checked="">
                            <label class="checkbox"></label>
                        </span>
                        <em>APP</em>
                    </div>                      
                </li>
                <div style="clear: left"></div>
                <li class="fl" style="display: inline-block" id="info_btype">
                    <label class="fl">计费方式：</label>
                    <div class="change checkbox-con btype_cpc" style="margin-left: 10px; margin-right: 30px;">
                        <span class="wrap">
                            <input type="radio" class="ipt-hide" checked="">
                            <label class="checkbox cur"></label>
                        </span>
                        <em>CPC</em>
                    </div>                  
                </li>
                <div style="clear: left"></div>
                <li>
                    <label class="fl">广告价格：</label>
                    <input type="text" class="ad-price txt-right" id="info_price" placeholder="价格越高效果越好" value="">元/次
              <em class='red'>（必填*）</em></li>
            </ul>
        </div>
        <!-- 基本信息end -->
        <div class="setArea clearfix">
            <h2>投放设置</h2>
            <div class="setTime fl">
                <div class="box">
                    <div class="checkbox-con clearfix">
                        <em>投放时间：</em>
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="">
                            <label class="checkbox cur"></label>
                        </span>
                        <em>连续时间</em>
                    </div>
                    <!--时间框-->
                    <div class="date-time-box">
                        <div class="fill"></div>
                        <dl class="clearfix">
                            <dd>
                                <div class="checkbox-con clearfix" id="starttimeckbox">
                                    <span class="wrap">
                                        <input type="checkbox" class="ipt-hide" checked="checked">
                                        <label class="checkbox cur"></label>
                                    </span>
                                    <em>不设置结束时间</em>
                                </div>
                            </dd>
                            <dd>
                                <div class="checkbox-con second clearfix" id="endtimeckbox">
                                    <span class="wrap">
                                        <input type="checkbox" class="ipt-hide">
                                        <label class="checkbox"></label>
                                    </span>
                                    <em>设置结束时间</em>
                                </div>
                            </dd>
                        </dl>
                        <div class="starttime" id="starttime">
                            开始时间：
                  <input type="text" class="some_class" value="" id="some_class_1" />
                        </div>
                        <div class="endtime" id="endtime">
                            结束时间：
                  <input type="text" class="some_class" value="" id="some_class_2" />
                        </div>
                    </div>
                    <!--时间框 end-->
                </div>
            </div>
            <div class="setNum fl">
                <p class="fl">投放量设置：</p>
                <div class="fl limitBox">
                    <div class="disables limitmax" id="info_putmax">
                        <input class="limit" type="checkbox">
                        <label for="">设置投放上限</label>点击
                <input type="text" class="txt-right" disabled="disabled" onkeyup='Limit(this,9)' value="999999999">次
                <em class="red">（必填*）</em>
                    </div>
                    <div class="disables limitmaxbyday" id="info_putmaxbyday">
                        <input class="limit" type="checkbox" >
                        <label for="" style="margin-right: 62px">设置每日投放数量</label>点击
                <input type="text" class="txt-right" disabled="disabled" onkeyup='Limit(this,9)' value="999999999">次
                <em class="red">（必填*）</em>
                    </div>
                </div>
                <div style="height: 0; clear: both"></div>
            </div>
        </div>
        <!-- 投放设置end -->
        <div class="setMaterial clearfix">
            <h2>广告物料</h2>
            <div class="one clearfix">
                <div class="slidebox fl">
                    <span class="fl">广告物料轮换：</span>
                    <div class="choice fl" id="info_weighttype">
                        <div class="box">
                            <span class="title">自动权重</span>
                            <div class="sanjiao">
                                <span class="triangle"></span>
                            </div>
                        </div>
                        <div class="slide" style="width: 131px; display: none;">
                            <dl>
                                <dd class="cur"><em>自动权重</em>
                                    <span style="display: none">1</span></dd>
                                <dd><em>手动权重</em>
                                    <span style="display: none">2</span></dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </div>
            <div class="two">
                新建物料：
            <button class="newAdwl">新建广告物料</button>
                <button class="wlBox">从物料库选择</button>
            </div>
            <div class="three clearfix">
                <span class="fl">广告物料：</span>
                <table class="fl materialtable" border="0" cellpadding="0" cellspacing="0" width="836">
                </table>
            </div>
        </div>
    </div>
    <div class="w1230 atLast">
        <button class="save" style="background: #12bdce;">保存</button>
        <button class="cencel">取消</button>
    </div>
    <div class="loginmask"></div>
    <div class="popup">
        <div id="selectMatType" class="top clearfix tab-hd">
            <a class="on">banner<span class="hide">1</span></a>
            <a>信息流<span class="hide">2</span></a>
            <a>悬浮式<span class="hide">3</span></a>
            <a>弹层<span class="hide">4</span></a>
          <%--  <a>开屏<span class="hide">5</span></a>
            <a>视频前帖<span class="hide">6</span></a>--%>
        </div>
        <div class="tab-bd clearfix">
            <!-- 物料-图片-开始-->
            <div class="content">
                <ul  class="clearfix"> 
                    <li class="fl" id="matinfo_subadtype">
                        <div class="slidebox slidebox2 fl" style ="width:400px !important;">
                            <span class="fl">广告形式：</span>
                            <div class="choice fl">
                                <div class="box">
                                    <span class='title'>
                                        <%=subAdTypeDefault %></span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide">
                                    <dl>
                                        <%=subAdTypeList %>
                                    </dl>
                                </div>
                            </div>                          
                        </div>
                    </li>
                </ul>
                <ul class="clearfix">
                    
                    <li class="wlmc">
                        <label for="wlmc">物料名称：</label>
                        <input type="text" class="textCounter" />
                        <span class='tip'>最多可输入100个字符</span>
                    </li>
                    <li class="title hide">
                        <label>标题：</label>
                        <input type="text" class="textCounter">
                    </li>
                    <li class="textarea hide">
                        <label for="tpwj">文字内容：</label>
                        <textarea name="" cols="30" rows="10"></textarea>
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
                    <li class="link clearfix">
                        <label for="djlj">点击链接：</label>
                        <input type="text" class="txt fl" id="linkurl">
                        <div id="fileapp" class="upload on">
                            安装包                        
                        <!--安装文件上传：控制：大小 10M以内 -->
                            <input type="file" id="fileuploadapp" name="file" onchange="fileChange(this)" class="fileupload" size="100" />
                        </div>
                        <span class='tip'>上传安装包不超过5M或自填链接</span>
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
            </div>
            <input id="sizes" type="hidden" value="0" />
            <input id="format" type="hidden" value="png" />
            <!-- 物料-图片-结束-->

        </div>
        <!--确认or取消 开始-->
        <div class="btn">
            <button class='confirm'>确定</button>
            <button class='cancel'>取消</button>
        </div>
        <!--确认or取消 结束-->
        <div class="uploadImg">
            <img src="../images/pic1.png" alt="">
        </div>

    </div>
    <!-- popup2 start -->
    <div class="loginmask2"></div>
    <div class="popup2">
        <div class="table-head clearfix">
            <ul>
                <li style="width: 15%" class="head1">
                    <div class="checkbox-con">
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="">
                            <label class="checkbox"></label>
                        </span>
                        <em>广告物料名称</em>
                    </div>
                </li>
                <li style="width: 35%">广告物料预览</li>
                <li style="width: 10%">尺寸</li>
                <li style="width: 10%">类型</li>
                <li style="width: 15%">上传时间</li>
                <li style="width: 15%">操作人</li>
        </div>
        <div class="table-content" id="material_list">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tbody>
                </tbody>
            </table>
        </div>
        <button class="confirmT">确定</button>
        <button class="cancelT">取消</button>
    </div>
    <script src="/page/js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="/page/js/base/v2.0/page.base.js" type="text/javascript"></script>
    <script src="/page/js/jquery/jquery.easing.min.js" type="text/javascript"></script>
    <script src="/page/js/jquery/jquery.datetimepicker.full.js" type="text/javascript"></script>
    <script src="/page/js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
    <script src="/page/js/base/v2.0/ajaxfileupload.js" type="text/javascript"></script>
    <script>
        $('.some_class').datetimepicker();
        $(".one,.two").hide();
        $(".three").css("margin-top", "20px");
        var _adId = '<%= adId %>';
        var _eType = '<%= eType %>';
        var _accountType = '<%=accountType %>';
        var _accountId = '<%=accountId %>';
        var _adUserId = '<%=adUserId %>';
        $(function () {
            if (_eType == "2") {
                $(".save").addClass("enable");
                $("#info_adname").attr("disabled", "disabled");              
                $("#starttimeckbox input").attr("disabled", "disabled");
                $("#endtimeckbox input").attr("disabled", "disabled");
                $("#starttime input").attr("disabled", "disabled");
                $("#endtime input").attr("disabled", "disabled");
                $("#info_putmax input").attr("disabled", "disabled");
                $("#info_putmaxbyday input").attr("disabled", "disabled");               
                GetAdInfo(_adId);
            }
            else {
                $(".one,.two").show();
                $(".three").css("margin-top", "0px");
            }
            $(".save").click(function () {
                if (_eType == "1") {
                    InsertData(0);
                } else if (_eType == "2") {
                    UpdataData(_adId);
                } else { }
            })
        });
        //返回主页
        $(".logo").click(function () {
            location.href = "/page/agent/index.aspx";
        })
    </script>
    <script src="/page/js/agent/newAd.js" type="text/javascript"></script>
    <script src="/page/js/agent/adedit.js?t=20170619" type="text/javascript"></script>
</body>

</html>
