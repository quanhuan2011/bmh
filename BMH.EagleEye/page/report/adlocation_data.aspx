<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adlocation_data.aspx.cs" Inherits="BMH.EagleEye.page.report.adlocation_data" %>

<!DOCTYPE html>
<html>
<head>
    <title>广告位数据表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务" />
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。" />
    <link rel="stylesheet" href="../css/normalize-report.css"/>
    <link rel="stylesheet" href="../css/home-min.css"/>      
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
<div class="bmhAdTop">
    <div class="top-center clearfix">
        <div class="left">
            广告位数据表 <span>></span> <a href="#" class="title"><%=adLocationName %></a>
        </div>
        <div class="right">
            <div class="dateBox"><input id="date-range0" size="30" value=""></div>
            <a href="#" class="download">下载</a>
        </div>  
    </div>
    <!--adtop end-->
    <!--主体内容 bmhAdTable start-->
    <div class="bmhAdTable">
        <!-- 数据数字展示 start -->
        <div class="adNum">
            <div class="top"><div class="title">广告位数据表</div></div>
            <ul class="clearfix">
                <li>
                    <div class="icon icon1 fl"></div>
                    <div class="cont fl">
                        <span class="timer count-title num"  data-to="0" data-speed="1500"></span>
                        <p>展现量<a href="#" title="一段时间内广告获得的展现次数"></a></p>
                    </div>
                </li>
                <li>
                    <div class="icon icon2 fl"></div>
                    <div class="cont fl">
                        <span class="timer count-title num" data-to="0" data-speed="1500"></span>
                        <p>点击量</p>
                    </div>
                </li>
                <li>
                    <div class="icon icon3 fl"></div>
                    <div class="cont fl" style="position:relative">
                        <!-- <span class="num">22.5%</span> -->
                        <span class="num" ></span>
                        <p>点击率</p>
                    </div>
                </li>
                <li class="last">
                    <div class="icon icon4 fl"></div>
                    <div class="cont fl">
                        <span class="timer count-title num" data-to="0" data-speed="1500"></span>
                        <p>收入<a href="#" title="通过广告展现个点击带来的效益"></a></p>
                    </div> 
                </li>
            </ul>
        </div>
        <!--数据数字展示 end-->
        <!--bmhDataPic start -->
        <div class="bmhDataPic clearfix">
            <div class="top clearfix">
                <ul class="tip">
                    <li class="on" name="day">按日</li><li name="area">按地域</li><li name="hour">按时段</li><li name="class">按分类</li>
                </ul>
                <div class="choice fr" style="position:relative">
                    <div class="box clearfix">
                        <dl class="fl">
                             <dd class="ggwqqs">广告位请求数</dd> 
                            <dd class="zxl">展现量</dd>
                            <%--<dd class="djliang">点击量</dd>--%>
                        </dl>
                        <div style="position:absolute;right:10px;height:34px;width:40px">
                            <div class="interval"></div>
                        <div class="triangle fl"> </div>
                        </div>
                        
                    </div>
                    <div class="slide">
                        <dl>

                            <dd class="ggwqqs">
                                <span class="title" data-color='#08c4c4' data-name="requestcnt">广告位请求数</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='广告位请求数' checked>
                                        <label class="checkbox cur"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="zxl">
                                <span class="title" data-color='#ff00ff' data-name="showcnt">展现量</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='展现量' checked>
                                        <label class="checkbox cur"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="djliang">
                                <span class="title" data-color='#7ccff1' data-name="clickcnt">点击量</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='点击量'>
                                        <label class="checkbox"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="ecpm">
                                <span class="title" data-color='#b2d466' data-name="ecpm">eCPM</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='eCPM'>
                                        <label class="checkbox"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="cpc">
                                <span class="title" data-color='#eb66a5' data-name="cpc">CPC</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='CPC'>
                                        <label class="checkbox"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="sr">
                                <span class="title" data-color='#ff00ff' data-name="income">收入</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='收入'>
                                        <label class="checkbox"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="djl">
                                <span class="title" data-color='#fdf200' data-name="djl">点击率</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value='点击率'>
                                        <label class="checkbox"></label>
                                   </span>
                                </div>
                            </dd>
                            <dd class="tcl">
                                <span class="title" data-color='#f19049' data-name="tcl">填充率</span>
                                <div class="checkbox-con">
                                    <span>
                                    <input type="checkbox"  class="ipt-hide" name="apk[]" value=''填充数>
                                        <label class="checkbox"></label>
                                   </span>
                                </div>
                            </dd>
                        </dl> 
                    </div>
                    
                </div>
            </div>
            <!-- bmhDataPic end -->
            <!--数据图像展示 开始-->
            <div class="dataBox" id="myEchart" >
                        
            </div>
            <!--数据图像展示 end-->
        </div>
        <!--表格开始-->

        <div class="datailData TabBox">
            <table class="data-table clearfix" width="100%" cellpadding="0" cellspacing="0" border="0" id="tableNeed">

            </table>

        </div>
        <!--表格结束-->
        
    </div>
    <!-- bmhAdTable -->
    
</div>
<!--ad-top end-->
</div>
    <script src="../js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../js/echarts/echarts.js" type="text/javascript"></script>

<script>
     var  adLocationId=<%=adLocationId %>; 
    var _dimensionType = "day", _starttime, _endtime;
    $(document).ready(function () {
        $('.data-table').dataTable({
            "searching": false,  //是否允许Datatables开启本地搜索
            //"paging": false,  //是否开启本地分页
            "pagingType": "simple_numbers",
            "bPaginate": true, //翻页功能
            "bLengthChange": true, //改变每页显示数据数量
            "iDisplayLength": 50,   //一页显示条数
            "lengthChange": false,  //是否允许用户改变表格每页显示的记录数
            "info": false,   //控制是否显示表格左下角的信息
            "columnDefs": [{
                "targets": 'nosort',  //列的样式名
                "orderable": false    //包含上样式名‘nosort’的禁止排序
            }],
             "aaSorting": [[ 0, "desc" ]]
        });

    });


</script>
    <script src="../js/moment/moment.min.js" type="text/javascript"></script>
    <script src="../js/report/v2.0/jquery.daterangepicker.report.js" type="text/javascript"></script>
    <script src="../js/report/v2.0/advertisingCommon.js" type="text/javascript"></script>
    <script src="../js/base/v2.0/demo.js" type="text/javascript"></script>
</body>
</html>

