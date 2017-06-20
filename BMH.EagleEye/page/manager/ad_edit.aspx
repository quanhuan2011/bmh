<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ad_edit.aspx.cs" Inherits="BMH.EagleEye.page.manager.ad_edit" %>

<!DOCTYPE html>
<html>

<head>
    <title>鹰眼广告管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务" />
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。" />
    <link rel="stylesheet" href="../css/normalize.css" />
    <link rel="stylesheet" href="../css/style-1.css" />
    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css" />
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
                    <em>
                        <img src="images/tips.png" height="7" width="20" alt=""></em>
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
        <div id="newAd">
            <div class="w1230">新建广告</div>
        </div>
        <div class="w1230 set-platform" id="YY-main">
            <div class="basic-info clearfix">
                <h2>基本信息</h2>
                <ul>
                    <li>
                        <div class=" slidebox fl" id="info_aduser">
                            <span class="fl">广告主：</span>
                            <div class="choice fl">
                                <div class="box">
                                    <span class='title'>
                                        <%=adUserNameDefault %></span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide">
                                    <dl>
                                        <%=adUserNameList %>
                                    </dl>
                                </div>
                            </div>
                            <em class='red'>（必填*）</em>
                        </div>
                    </li>
                    <div style="clear: left"></div>
                    <li class="fl" id="info_adtype">
                        <div class="slidebox slidebox2 fl" >
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
                        <div class="slidebox slidebox2 fl" >
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
                       <div style="clear: left"></div>
                     <li class="fl" style="display: inline-block" id="info_isbottom">
                        <label class="fl">是否打底：</label>
                        <div class="change checkbox-con isbottom_n" style="margin-left: 10px; margin-right: 30px;">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox cur"></label>
                            </span>
                            <em>否</em>
                        </div>
                        <div class="change checkbox-con isbottom_y">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox"></label>
                            </span>
                            <em>是</em>
                        </div>
                    </li>
                     <div style="clear: left"></div>
                    <li class="fl" style="display: inline-block" id="info_isbid">
                        <label class="fl">是否竞价：</label>
                        <div class="change checkbox-con" style="margin-left: 10px; margin-right: 30px;">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox cur"></label>
                            </span>
                            <em>否</em>
                        </div>
                        <div class="change checkbox-con">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox"></label>
                            </span>
                            <em>是</em>
                        </div>
                    </li>
                    <li class="ad-page" id="info_putrange">
                        <div class=" slidebox fl">
                            <span class="fl">投放范围：</span>
                            <div class="choice fl">
                                <div class="box">
                                    <span class='title'></span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide" style="background: none; border: 0;">
                                    <div class="area">
                                        <div class="fill"></div>
                                        <table class="clearfix" style="width: 100%;" cellpadding="0" cellspacing="0" border="0">                                          
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <em class='red'>（必填*）</em>
                        </div>
                    </li>
                    <li class="info_page">
                        <div class=" slidebox fl" id="info_page">
                            <span class="fl">广告页：</span>
                            <div class="choice fl">
                                <div class="box">
                                    <span class='title'>
                                        <%=pageNameDefault %></span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide">
                                    <dl>
                                        <%=pageNameList %>                                       
                                    </dl>
                                </div>
                            </div>
                            <em class='red'>（必填*）</em>
                        </div>
                    </li>
                    <li class="middle">
                        <div class=" slidebox slidebox2 fl" id="info_adlocation">
                            <span class="fl">广告位：</span>
                            <div class="choice fl">
                                <div class="box">
                                    <span class='title'>
                                        <%=adLocationNameDefault%></span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide">
                                    <dl>
                                        <%=adLocationNameList %>                                      
                                    </dl>
                                </div>
                            </div>
                            <em class='red'>（必填*）</em>
                        </div>
                    </li>
                    <div style="height:0;clear:left"></div>                    
                    <li class="name">
                        <label class="fl">广告名称：</label>
                        <input class="ad-name fl" id="info_adname" type="text" placeholder="少于130个字符" value="">
                        <em class='red'>（必填*）</em></li>
                    <div style="clear: left"></div>
                    <li class="fl" style="display: inline-block" id="info_btype">
                        <label class="fl">计费方式：</label>
                        <div class="change checkbox-con btype_cpc" style="margin-left: 10px; margin-right: 20px;">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox cur"></label>
                            </span>
                            <em>CPC</em>
                        </div>
                        <div class="change checkbox-con btype_cpd" style="margin-right: 20px;">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox"></label>
                            </span>
                            <em>CPD</em>
                        </div>
                        <div class="change checkbox-con btype_cpm">
                            <span class="wrap">
                                <input type="radio" class="ipt-hide" checked="">
                                <label class="checkbox"></label>
                            </span>
                            <em>CPM</em>
                        </div>
                    </li>
                    <div style="clear: left"></div>
                    <li>
                        <label class="fl">广告价格：</label>
                        <input type="text" class="ad-price txt-right" id="info_price" value="">元/次
                        <em class='red'>（必填*）</em>
                    </li>
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
                            <input class="limit" type="checkbox">
                            <label for="" style="margin-right: 62px">设置每日投放数量</label>点击
                <input type="text" class="txt-right" disabled="disabled" onkeyup='Limit(this,9)' value="999999999">次
                <em class="red">（必填*）</em>
                        </div>
                    </div>
                     <div style="height:0;clear:both"></div>           
                </div>
            </div>
            <!-- 投放设置end -->
            <!-- 投放设置end -->
            <div class="setDirect clearfix">
                <h2>定向设置</h2>
                <div class="regionalOrientation">
                    <div class="box box1 clearfix">
                        <label class="fl">目标窗口：</label>
                        <div class="checkbox-con">
                            <span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="">
                                <label class="checkbox"></label>
                            </span>
                            <em>不限地域</em>
                        </div>
                        <div class="checkbox-con">
                            <span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="">
                                <label class="checkbox cur"></label>
                            </span>
                            <em>定向地域</em>
                        </div>                       
                    </div>
                    <!--选择end-->
                    <div class="area">
                        <div class="fill"></div>
                    </div>
                    <!--area end-->
                    <div class="box box2 clearfix">
                        <label class="fl">时间定向：</label>
                        <div class="checkbox-con">
                            <span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="">
                                <label class="checkbox"></label>
                            </span>
                            <em>全日日程</em>
                        </div>
                        <div class="checkbox-con">
                            <span class="wrap">
                                <input type="checkbox" class="ipt-hide" checked="">
                                <label class="checkbox cur"></label>
                            </span>
                            <em>制定时间段</em>
                        </div>
                    </div>
                    <!--选择end-->
                    <div class="timeBox clearfix">
                        <div class="fill"></div>
                        <div class="clearfix" style="position: relative">
                            <ul class="fl">
                                <li>周一</li>
                                <li>周二</li>
                                <li>周三</li>
                                <li>周四</li>
                                <li>周五</li>
                                <li>周六</li>
                                <li>周日</li>
                            </ul>
                            <table cellspacing="0" cellpadding="0" border="0" width="648" class="fl">
                            </table>                            
                        </div>
                        <div class="setting clearfix">
                            <span>快速设定：</span>
                            <button class="allWeek">全周投放</button>
                            <button class="monToFri">周一到周五投放</button>
                            <button class="weekend">周末投放</button>                           
                            <div class="checkbox-con fr" style="margin-right: 16px">
                                <span class="wrap">
                                    <input type="checkbox" class="ipt-hide" checked="">
                                    <label class="checkbox cur"></label>
                                </span>
                                <em>投放时间段</em>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- 定向设置end -->
            <div class="otherSet clearfix">
                <h2>其他设置</h2>
                <div class="box clearfix ">
                    <div class="checkbox-con clearfix nonelimit">
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="">
                            <label class="checkbox"></label>
                        </span>不限
                    </div>
                    <div class="checkbox-con clearfix oslimit">
                        <span class="wrap">
                            <input type="checkbox" class="ipt-hide" checked="checked">
                            <label class="checkbox cur"></label>
                        </span>设定其他定向
                    </div>
                </div>
                <div class="choices">
                    <div class="top">
                        <div class="slidebox stateBox fl">
                            <span class="fl">定向方式：</span>
                            <div class="choice fl" id="info_others">
                                <div class="box">
                                    <span class="title"><%=directTypeDefault %></span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide" style="width: 131px; display: none;">
                                    <dl>
                                        <%=directTypeList %>
                                    </dl>                                  
                                </div>
                            </div>
                            <div class="choice choice2 fl" id="info_os_equalstatus">
                                <div class="box">
                                    <span class="title">等于</span>
                                    <div class="sanjiao">
                                        <span class="triangle"></span>
                                    </div>
                                </div>
                                <div class="slide" style="width: 131px; display: none;">
                                    <dl>
                                        <dd class="cur"><em>等于</em></dd>
                                    </dl>                                   
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- top end -->
                    <div class="down clearfix">
                        <div class="system">
                            <div class="title">选择操作系统：</div>                           
                        </div>
                        <div class="already">
                            <div class="title">已选择：</div>                           
                        </div>                        
                        <div class="add_all">
                            <span class="triangle_border_right_all1"></span><span class="triangle_border_right_all2"></span>
                        </div>
                        <div class="delete_all">
                            <span class="triangle_border_left_all1"></span><span class="triangle_border_left_all2"></span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 其他设置 end -->
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
                        <%-- <tr>
                <td>领域C3-wap-3</td>
                <td class="img" width="258">
                  <img src="../images/pic1.png" height="71" width="235"></td>
                <td>235*71</td>
                <td>图片</td>
                <td width="150">
                  <button class='list_removebtn'>移除</button></td>
                </tr>
                <tr>
                  <td>领域C3-wap-3</td>
                  <td class="txt">
                    <p>领域C3领域C3领域C3-wap-3</p>
                    <img src="../images/pic1.png" height="71" width="88"></td>
                  <td>235*71</td>
                  <td>图片</td>
                  <td>
                    <button>移除</button></td>
                </tr>--%>
                    </table>
                </div>
            </div>
        </div>
        <div class="w1230 atLast">
            <button class="saveAndUse">保存并启用</button>
            <button class="save">保存</button>
            <button class="cencel">取消</button>
        </div>
        <div class="loginmask"></div>
        <div class="popup">

            <div id="selectMatType" class="top clearfix tab-hd">
                <a class="on">banner<span class="hide">1</span></a>
                <a>信息流<span class="hide">2</span></a>
                <a>悬浮式<span class="hide">3</span></a>
                <a>弹层<span class="hide">4</span></a>
            </div>

            <div class="tab-bd clearfix">

                <!-- 物料-图片-开始-->

                <div class="content">
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
                                    <label id="displaynew" class="checkbox"></label>
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
                <img src="../images/pic1.png" alt=""></div>

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
        <script src="../js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
        <script src="../js/base/v2.0/page.base.js" type="text/javascript"></script>
        <script src="../js/jquery/jquery.easing.min.js" type="text/javascript"></script>
        <script src="../js/jquery/jquery.datetimepicker.full.js" type="text/javascript"></script>
        <script src="../js/jquery/jquery.nicescroll.js" type="text/javascript"></script>
        <script src="../js/base/v2.0/ajaxfileupload.js" type="text/javascript"></script>
        <script>
            $('.some_class').datetimepicker();
            var _adId = '<%= adId %>';
              var _eType = '<%= eType %>';
            var _accountType = '<%=accountType %>';
            var _accountId = '<%=accountId %>';
            $(function () {
                if (_eType == "2") {
                    $(".saveAndUse").css("display", "none");
                    $(".save").addClass("enable");
                    GetAdInfo(_adId);
                } else if (_eType == "1") {
                    GetDirectTypeData();
                    GetPutRangeBySubAdType("1", "");

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
                location.href = "/page/manager/index.aspx";
            })
        </script>
        <script src="../js/manager/v2.0/newAd.js" type="text/javascript"></script>
        <script src="../js/manager/v2.0/ad_edit.js?t=20170619" type="text/javascript"></script>
</body>

</html>
