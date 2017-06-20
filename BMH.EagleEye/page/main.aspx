﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="BMH.EagleEye.page.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <head>
        <style type="text/css">
            body{background:#fff}
           
            body{margin:0;}
            a:active,a:hover{outline:0}
            button,input,optgroup,select,textarea{color:inherit;font:inherit;margin:0}
            button,html input[type=button],input[type=reset],input[type=submit]{-webkit-appearance:button;cursor:pointer}
            input,select,button{outline:none;}
            table{border-collapse:collapse;border-spacing:0}
            td,th{padding:0}
            img{vertical-align:middle;border:0}
            @-ms-viewport{width:device-width}
            html{font-size:50px;-webkit-tap-highlight-color:transparent;height:100%;min-width:320px;overflow-x:hidden}
            body{font-family:"Microsoft YaHei";font-size:.28em;line-height:1;color:#333;}
            .h1,.h2,.h3,.h4,.h5,.h6,h1,h2,h3,h4,h5,h6{font-weight:500;line-height:1.1}
            .h1 .small,.h1 small,.h2 .small,.h2 small,.h3 .small,.h3 small,.h4 .small,.h4 small,.h5 .small,.h5 small,.h6 .small,.h6 small,h1 .small,h1 small,h2 .small,h2 small,h3 .small,h3 small,h4 .small,h4                   small,h5 .small,h5 small,h6 .small,h6 small{font-weight:400;line-height:1}
            .h1,.h2,.h3,h1,h2,h3{margin-top:.28rem;margin-bottom:.14rem}
            .h1 .small,.h1 small,.h2 .small,.h2 small,.h3 .small,.h3 small,h1 .small,h1 small,h2 .small,h2 small,h3 .small,h3 small{font-size:65%}
            .h4,.h5,.h6,h4,h5,h6{margin-top:.14rem;margin-bottom:.14rem}
            .h4 .small,.h4 small,.h5 .small,.h5 small,.h6 .small,.h6 small,h4 .small,h4 small,h5 .small,h5 small,h6 .small,h6 small{font-size:75%}
            .h1,h1{font-size:.364rem}
            .h2,h2{font-size:.2996rem}
            .h3,h3{font-size:.238rem}
            .h4,h4{font-size:.175rem}
            .h5,h5{font-size:.14rem}
            .h6,h6{font-size:.119rem}
            h6{margin-top:0;margin-bottom:0}
            button,input,select,textarea{font-family:inherit;font-size:inherit;line-height:inherit}
            a{color:#06c1ae;text-decoration:none;outline:0}
            a:focus{outline:thin dotted;outline:5px auto -webkit-focus-ring-color;outline-offset:-2px}
            a.react,label.react{display:block;color:inherit;height:100%}
            a.react.react-active,a.react:active,label.react:active{background:rgba(0,0,0,.1)}
            ul{margin:0;padding:0;list-style-type:none}
            hr{margin-top:.28rem;margin-bottom:.28rem;border:0;border-top:1px solid #DDD8CE}
            h6,p{line-height:1.41;text-align:justify;margin:-.2em 0;word-break:break-all}
            small,weak{color:#666}
            ::-webkit-input-placeholder {color:#999;line-height:inherit;} 
            :-moz-placeholder {color:#999;line-height:inherit;} 
            ::-moz-placeholder {color:#999;line-height:inherit;}

            /*other public*/
            .iconfont{font-family:'adminthemesregular';}
            .add_icon:before{content:"a";margin:0 5px;font-family:'adminthemesregular';}
            .money_icon:before{content:"$";margin:0 5px;font-family:'adminthemesregular';font-size:20px;}
            .rmb_icon{color:#19a97b;}
            .rmb_icon:before{content:"￥";margin-right:2px;}
            .ellipsis{text-overflow:ellipsis;overflow:hidden;white-space:nowrap;}
            .center{text-align:center;}
            .fl{float:left;}
            .fr{float:right;}
            .mtb{margin:5px 0;overflow:hidden;}
            .mlr{margin:0 5px;overflow:hidden;}
            .admin_login{width:300px;height:auto;overflow:hidden;margin:10% auto 0 auto;padding:40px;box-shadow:0 -15px 30px #0d957a;border-radius:5px;}
            .admin_login dt{font-size:20px;font-weight:bold;text-align:center;color:#45bda6;text-shadow:0 0 1px #0e947a;margin-bottom:15px;}
            .admin_login dt strong{display:block;}
            .admin_login dt em{display:block;font-size:12px;margin-top:8px;}
            .admin_login dd{margin:5px 0;height:42px;overflow:hidden;position:relative;}
            .admin_login dd .login_txtbx{font-size:14px;height:26px;line-height:26px;padding:8px 5%;width:90%;text-indent:2em;border:none;background:#5cbdaa;color:white;}
            .admin_login dd .login_txtbx::-webkit-input-placeholder {color:#f4f4f4;line-height:inherit;} 
            .admin_login dd .login_txtbx:-moz-placeholder {color:#f4f4f4;line-height:inherit;} 
            .admin_login dd .login_txtbx::-moz-placeholder {color:#f4f4f4;line-height:inherit;}
            .admin_login dd .login_txtbx:focus{background:#55b7a4;}
            .admin_login dd:before{font-family:'adminthemesregular';position:absolute;top:0;left:10px;height:42px;line-height:42px;font-size:20px;color:#0c9076;}
            .admin_login dd.user_icon:before{content:"u";}
            .admin_login dd.pwd_icon:before{content:"p";}
            .admin_login dd.val_icon:before{content:"n";}
            .admin_login dd .ver_btn{text-align:right;border:none;color:#f4f4f4;height:42px;line-height:42px;margin:0;z-index:1;position:relative;float:right;background:#48bca5;}
            .admin_login dd .checkcode{float:left;width:182px;height:42px;background:#fff}
            .admin_login dd .checkcode input{width:120px;height:36px;line-height:36px;padding:3px;color:white;outline:none;border:none;text-indent:2.8em;}
            .admin_login dd .checkcode canvas{width:85px;height:36px;padding:3px;z-index:0;background:#5cbdaa;}
            .admin_login dd .submit_btn{width:100%;height:42px;border:none;font-size:16px;background:#048f74;color:#f8f8f8;}
            .admin_login dd .submit_btn:hover{background:#0c9076;color:#f4f4f4;}
            .admin_login dd p{color:#53c6b0;font-size:12px;text-align:center;margin:5px 0;}
            ul{width:200px;margin-left:100px;}
            ul li {list-style:none;padding:5px;}
    </style>
    </head>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ol>
            广告位数据表
        </ol>
        <ul id="adlocationlist">
            <%--<li><a href="adlocation_data.aspx">广告位1</a></li>
            <li><a href="adlocation_data.aspx">广告位2</a></li>
            <li><a href="adlocation_data.aspx">广告位3</a></li>
            <li><a href="adlocation_data.aspx">广告位4</a></li>
            <li><a href="adlocation_data.aspx">广告位5</a></li>
            <li><a href="adlocation_data.aspx">广告位6</a></li>
            <li><a href="adlocation_data.aspx">广告位7</a></li>
            <li><a href="adlocation_data.aspx">广告位8</a></li>--%>
            <!--获取广告位数据表-->
            <%=GetDataInfo(Model.BeeDataType.adlocation)%>
        </ul>
        <ol>
            广告数据表
        </ol>
        <ul>
            <%--<li><a href="ad_data.aspx">广告1</a></li>
            <li><a href="ad_data.aspx">广告2</a></li>
            <li><a href="ad_data.aspx">广告3</a></li>
            <li><a href="ad_data.aspx">广告4</a></li>
            <li><a href="ad_data.aspx">广告5</a></li>
            <li><a href="ad_data.aspx">广告6</a></li>
            <li><a href="ad_data.aspx">广告7</a></li>
            <li><a href="ad_data.aspx">广告8</a></li>--%>
            <%=GetDataInfo(Model.BeeDataType.ad)%>
        </ul>
        <ol>
            物料数据表
        </ol>
        <ul>
            <%--<li><a href="material_data.aspx">物料1</a></li>
            <li><a href="material_data.aspx">物料2</a></li>
            <li><a href="material_data.aspx">物料3</a></li>
            <li><a href="material_data.aspx">物料4</a></li>
            <li><a href="material_data.aspx">物料5</a></li>
            <li><a href="material_data.aspx">物料6</a></li>
            <li><a href="material_data.aspx">物料7</a></li>
            <li><a href="material_data.aspx">物料8</a></li>--%>
            <%=GetDataInfo(Model.BeeDataType.material)%>
        </ul>
    </div>
    </form>

</body>
</html>
