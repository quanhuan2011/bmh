<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recharge.aspx.cs" Inherits="BMH.EagleEye.page.manager.recharge" %>

<!DOCTYPE html>

<html>
<head>
    <title>鹰眼充值</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="鹰眼，广告销售，大数据，人工智能，爆米花，移动营销平台，自助，竞价，营销服务" />
    <meta name="description" content="作为公司“全面移动”战略最重要的一部分，鹰眼(Eagle Eye)承载着爆米花商业模式升级的重要使命，我们的广告销售将从按位置、按CPM计价的模式，全面调整到按效果、按CPC/CPD/CPA计费、自助、竞价的模式，我们致力于把“鹰眼”打造成一个基于大数据和人工智能的精准、高效、透明的移动营销平台，未来，鹰眼平台还将对爆米花以外的第三方媒体开放，整合更多资源、服务更多的用户。" />
    <link rel="stylesheet" href="/page/css/normalize.css" />
    <%--<link rel="stylesheet" href="/page/css/style.css" />    --%>
    <style type="text/css">
        .enable {
            background: #12bdce !important;
        }

        body {
            background: #e6eaed;
            color: #3b1d1f;
        }
        /*提醒ie10以下浏览器用户升级*/
        .browser-happy {
            width: 1230px;
            margin: 0 auto;
            line-height: 30px;
            height: 30px;
            font-size: 14px;
        }

            .browser-happy a {
                color: #04c7c9;
                font-size: 14px;
                display: inline-block;
                margin-left: 10px;
            }

                .browser-happy a:after {
                    content: '↑';
                    display: inline-block;
                    margin-left: 2px;
                    font-size: 18px;
                    position: relative;
                    top: 1px;
                }
        /*head*/
        #YY-head {
            width: 100%;
            height: 88px;
            background: #f6f6f6;
            line-height: 88px;
            color: #0cbcc9;
        }

            #YY-head .logo {
                width: 177px;
                height: 88px;
                background: url(/page/images/YY-pic.png) no-repeat 0 0;
            }

            #YY-head .personInfo img {
                display: inline-block;
                width: 51px;
                height: 51px;
                border-radius: 50%;
                position: relative;
                top: 17px;
            }

            #YY-head .personInfo .name {
                margin: 0 9px;
            }

            #YY-head .personInfo .exit {
                margin-left: 9px;
            }

            #YY-head .personInfo .line {
                position: relative;
                top: -1px;
            }

        /*物料主体区域*/
        #YY-main {
            border-left: 1px solid #dcdddf;
            border-right: 1px solid #dcdddf;
            background: #fff;
            padding-top: 15px;
            height: 600px;
        }

        .set-platform {
            padding-bottom: 112px;
        }

        .basic-info {
            margin: 30px 0;
        }

        #YY-main h2 {
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

        #YY-main .info-chpwd {
            width: 620px;
            height: 300px;
            margin: 0 auto;
            margin-top: 60px;
            padding: 0px;
        }

            #YY-main .info-chpwd li {
                width: 100%;
                line-height: 30px;
                list-style: none;
                padding: 5px 0px;
                margin: 10px 0px;
            }

                #YY-main .info-chpwd li:nth-child(2) em:hover {
                    color: #12bdce;
                    text-decoration: underline;
                    cursor: pointer;
                }

                #YY-main .info-chpwd li label {
                    width: 160px;
                    font-size: 16px;
                    text-align: center;
                }
                #YY-main .info-chpwd li select {
                    width: 200px;
                    height: 30px;
                    border-bottom: thin solid gray;
                    font-size: 14px;
                }
                #YY-main .info-chpwd li input {
                    width: 200px;
                    height: 30px;
                    border-bottom: thin solid gray;
                    font-size: 14px;
                }

                #YY-main .info-chpwd li .button {
                    width: 110px;
                    height: 36px;
                    background: #c0c0c0;
                    color: #fff;
                    border-radius: 10px;
                    font-size: 16px;
                    margin: 50px 0;
                }

                #YY-main .info-chpwd li .btn-confirm {
                    margin: 0 38px 0 40px;
                }

                #YY-main .info-chpwd li .btn-cancel {
                    margin-right: 58px;
                }

        .btn_click {
            background: #12bdce !important;
        }

        .color_error {
            color: Red;
        }

        .color_ok {
            color: #12bdce;
        }
    </style>
</head>
<body>
        <div id="YY-head">
            <div class="w1230 clearfix">
                <div class="logo fl">
                </div>
            </div>
        </div>
        <!-- YY-head end -->
        <div class="w1230 set-platform" id="YY-main">
            <div class="basic-info">
                <h2>广告主充值</h2>
                <ul class="info-chpwd">
                    <li>
                        <label class="fl">
                            广告主：</label><select id="sltAdu"><%=adUserList %>
                            </select></li>
                    
                    <li>
                        <label class="fl">
                            余额：</label><input type="text"  id="txtBalance" value=<%=balance %> readonly="readonly" /></li>
                    <li>
                        <label class="fl">
                            充值金额：</label><input type="text" placeholder="请输入充值金额" id="txtMoney" value='0.0' /></li>

                    <li>
                        <button  class="btn-confirm button btn_click" id="btnConfirm">充值</button>
                        <button  class="btn-cancel button btn_click" id="btnCancel">返回</button>                        
                    </li>
                </ul>
            </div>
        </div>
    <script src="/page/js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var _accountId='<%=strBindAccountId %>';
        function GetAduBalance(tempId)
        {
           // var tempId = $(this).val();            
            var ajaxData = {
                method: "GetBalanceData",
                aduid: tempId
            };  
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {                    
                    if (result.errcode != "1") return;                    
                    $("#txtBalance").val(result.data.balance);
                    $("#txtMoney").val("0.0");
                }
            });
        }
        //切换广告主
        $("#sltAdu").change(function () {
            var tempId = $(this).val();            
            GetAduBalance(tempId);
        })
        //提交修改
        $("#btnConfirm").click(function () {
            var tempId = $("#sltAdu").val();
            var tempVal = $("#txtMoney").val();            
            if (!CheckDataValid("充值金额", tempVal, "1,2"))
                return;
            tempVal = parseFloat(tempVal);
            if (tempVal <= 0)
            {
                alert("请输入大于零的充值金额");
                return;

            }
            var ajaxData = {
                method: "InsertRechargeData",
                acctid:_accountId,
                aduid: tempId,
                money:tempVal
            };
            $.ajax({
                url: "/api/ManagerHandler.ashx",
                type: "post",
                data: ajaxData,
                dataType: "json",
                success: function (result) {
                    alert(result.errmsg);
                    if (result.errcode != "1")
                        return;
                    else
                        GetAduBalance(tempId);
                    // $("#txtBalance").val(result.data.balance);
                }
            });
            //var tempId =  $("#sltAdu").val();
            //var tempVal = $("#txtPutMaxByDay").val();
            //if(!CheckDataValid("投放量上限",tempVal,"1,2"))
            //{
            //    return;
            //}
            //var ajaxData = {
            //    method: "SetPutMaxInfoByAdUData",
            //    aduid: tempId,
            //    putmaxbyday:tempVal
            //};
            //$.ajax({
            //    url: "/api/ManagerHandler.ashx",
            //    type: "post",
            //    data: ajaxData,
            //    dataType: "json",
            //    success: function (result) {                   
            //        alert(result.errmsg);                    
            //    }
            //});

        })
        $("#btnCancel").click(function () {
            history.go(-1);
            //location.href="/page/manager/index.aspx";
        })

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

    </script>
</body>
</html>

