<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepwd.aspx.cs" Inherits="BMH.EagleEye.page.changepwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>鹰眼修改密码</title>
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
        #YY-main .info-chpwd li:nth-child(2) em:hover
        {
            color: #12bdce;
            text-decoration: underline;
            cursor:pointer;
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
            height:30px;
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
    <form runat="server">
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
                修改密码</h2>
            <ul class="info-chpwd">
                <li>
                    <label class="fl">
                        当前帐号：</label><input type="text" id="txtUserName"  readonly="readonly" runat="server" /></li>                
                <li>
                    <label class="fl">
                        旧密码：</label><input type="password" placeholder="请输入旧密码" id="txtOldPwd" runat="server" /><em>忘记密码？</em></li>
                <li>
                    <label class="fl">
                        新密码：</label><input type="password" placeholder="请输入新密码" id="txtNewPwd" runat="server"
                            onkeyup='ComfirmOld(this)' /><em class="hide">长度太短</em></li>
                <li>
                    <label class="fl">
                        确认新密码：</label><input type="password" placeholder="请重新输入新密码" onkeyup='ComfirmNew(this)' /><em
                            class="hide">一致</em></li>
                <li>
                    <asp:Button class="btn-confirm button" ID="btnConfirm" runat="server" Text="确认修改"
                        OnClick="confirm_Click" disabled="disabled" />
                    <asp:Button class="btn-cancel button btn_click" ID="btnCancel" runat="server" Text="返回登录"
                        OnClick="cancel_Click" />
                </li>
            </ul>
        </div>
    </div>
    </form>
    <script src="js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ComfirmOld(obj) {
            var $newPwd = $(".info-chpwd").find("li").eq(2);
            $newPwd.find("em").addClass("hide").removeClass("color_error").removeClass("color_ok");
            var oldPwd = $(".info-chpwd").find("li").eq(1).find("input").val();
            $(".info-chpwd").find("li").eq(3).find("input").val("");
            $("#btnConfirm").attr("disabled", "disabled");
            $("#btnConfirm").removeClass("btn_click");      
            if ($.trim(obj.value) == "" && obj.value.length > 0) {
                $newPwd.find("em").removeClass("hide").addClass("color_error").text("不得输入空字符");
                return;
            }
            if (obj.value == oldPwd) {
                $newPwd.find("em").removeClass("hide").addClass("color_error").text("不与旧密码一致");
                return
            }
            if (obj.value.length < 8) {
                $newPwd.find("em").removeClass("hide").addClass("color_error").text("密码长度太短");
                return;
            }
            if (obj.value.length > 16) {
                $newPwd.find("em").removeClass("hide").addClass("color_error").text("密码长度太长");
                return;
            }
            $newPwd.find("em").removeClass("hide").addClass("color_ok").text("符合要求");
        }
        function ComfirmNew(obj) {
            $("#btnConfirm").attr("disabled", "disabled");
            $("#btnConfirm").removeClass("btn_click");
            var $newPwd = $(".info-chpwd").find("li").eq(2);
            var $surePwd = $(".info-chpwd").find("li").eq(3);
            $surePwd.find("em").addClass("hide").removeClass("color_error").removeClass("color_ok");
            var newPwd = $newPwd.find("input").val();
           
            if ($.trim(obj.value) == "" && obj.value.length > 0) {
                $surePwd.find("em").removeClass("hide").addClass("color_error").text("不得输入空字符");
                return;
            }
            if (!$newPwd.find("em").hasClass("hide") && $newPwd.find("em").hasClass("color_ok")) {
                if (obj.value == newPwd) {
                    $surePwd.find("em").removeClass("hide").addClass("color_ok").text("一致");
                    $("#btnConfirm").removeAttr("disabled");
                    $("#btnConfirm").addClass("btn_click");
                    return;
                }
                else {
                    $surePwd.find("em").removeClass("hide").addClass("color_error").text("密码不一致");
                    return;
                }
            }

        }
        $(".info-chpwd").find("li").eq(1).find("em").click(function () {
            alert("请联系管理员，进行密码重置");
        })
    </script>
</body>
</html>
