<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetpwd.aspx.cs" Inherits="BMH.EagleEye.page.resetpwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>鹰眼重置密码</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务" />
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。" />
    <link rel="stylesheet" href="/page/css/normalize.css" />
    <%--<link rel="stylesheet" href="/page/css/style.css" />    --%>
    <style type="text/css">
        .enable
        {
            background: #12bdce !important;
        }
        body
        {
            background: #e6eaed;
            color: #3b1d1f;
        }
        /*提醒ie10以下浏览器用户升级*/
        .browser-happy
        {
            width: 1230px;
            margin: 0 auto;
            line-height: 30px;
            height: 30px;
            font-size: 14px;
        }
        .browser-happy a
        {
            color: #04c7c9;
            font-size: 14px;
            display: inline-block;
            margin-left: 10px;
        }
        .browser-happy a:after
        {
            content: '↑';
            display: inline-block;
            margin-left: 2px;
            font-size: 18px;
            position: relative;
            top: 1px;
        }
        /*head*/
        #YY-head
        {
            width: 100%;
            height: 88px;
            background: #f6f6f6;
            line-height: 88px;
            color: #0cbcc9;
        }
        #YY-head .logo
        {
            width: 177px;
            height: 88px;
            background: url(/page/images/YY-pic.png) no-repeat 0 0;
        }
        #YY-head .personInfo img
        {
            display: inline-block;
            width: 51px;
            height: 51px;
            border-radius: 50%;
            position: relative;
            top: 17px;
        }
        #YY-head .personInfo .name
        {
            margin: 0 9px;
        }
        #YY-head .personInfo .exit
        {
            margin-left: 9px;
        }
        #YY-head .personInfo .line
        {
            position: relative;
            top: -1px;
        }
        
        /*物料主体区域*/
        #YY-main
        {
            border-left: 1px solid #dcdddf;
            border-right: 1px solid #dcdddf;
            background: #fff;
            padding-top: 15px;
            height: 600px;
        }
        .set-platform
        {
            padding-bottom: 112px;
        }
        .basic-info
        {
            margin: 30px 0;
        }
        #YY-main h2
        {
            width: 106px;
            height: 30px;
            line-height: 30px;
            background: #12bdcf;
            font-size: 16px;
            font-weight: normal;
            color: #fff;
            text-align: center;
            border-radius: 0 30px 30px 0;
        }
        
        #YY-main .info-chpwd
        {
            width: 420px;
            height: 300px;
            margin: 0 auto;
            margin-top: 60px;
            padding: 0px;
        }
        #YY-main .info-chpwd li
        {
            width: 100%;
            line-height: 30px;
            list-style: none;
            padding: 5px 0px;
            margin: 10px 0px;
        }
        #YY-main .info-chpwd li:nth-child(1) em:hover
        {
            color: #12bdce;
            text-decoration: underline;
            cursor: pointer;
        }
        #YY-main .info-chpwd li label
        {
            width: 100px;
            font-size: 16px;
            text-align: center;
        }
        #YY-main .info-chpwd li input
        {
            width: 200px;
            height: 30px;
            border-bottom: thin solid gray;
            font-size: 14px;
        }
        
        #YY-main .info-chpwd li .button
        {
            width: 110px;
            height: 36px;
            background: #c0c0c0;
            color: #fff;
            border-radius: 10px;
            font-size: 16px;
            margin: 50px 0;
        }
        #YY-main .info-chpwd li .btn-confirm
        {
            margin: 0 38px 0 40px;
        }
        #YY-main .info-chpwd li .btn-cancel
        {
            margin-right: 58px;
        }
        .btn_click
        {
            background: #12bdce !important;
        }
        .color_error
        {
            color: Red;
        }
        .color_ok
        {
            color: #12bdce;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div id="YY-head">
        <div class="w1230 clearfix">
            <div class="logo fl">
            </div>
        </div>
    </div>
    <!-- YY-head end -->
    <div class="w1230 set-platform" id="YY-main">
        <div class="basic-info">
            <h2>
                重置密码</h2>
            <ul class="info-chpwd">
                <li>
                    <label class="fl">
                        当前帐号：</label><input type="text" id="txtUserName" readonly="readonly" runat="server" /></li>
                <li>
                    <label class="fl">
                        管理员密码：</label><input type="password" placeholder="请输入管理员密码" id="txtPwd" runat="server" /><em
                            class="hide">长度太短</em></li>
                <li>
                    <label class="fl">
                        重置帐号：</label><input type="text" placeholder="请输入重置帐号" id="txtNewUserName" runat="server" /></li>
                <li>
                    <label class="fl">
                        重置后密码：</label><input type="text" id="txtNewPwd" readonly="readonly" runat="server" /></li>
                <li>
                    <asp:Button class="btn-confirm button btn_click" ID="btnConfirm" runat="server" Text="确认重置"
                        OnClick="btnConfirm_Click" />
                    <asp:Button class="btn-cancel button btn_click" ID="btnCancel" runat="server" Text="返回登录"
                        OnClick="btnCancel_Click" />
                </li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
