<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adu_add.aspx.cs" Inherits="BMH.EagleEye.page.manager.adu_add" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html>
<head>
    <title>广告主创建</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/page/css/bootstrap/bootstrap-3.0.1.min.css" rel="stylesheet" />
    <script src="/page/js/jquery/jquery-1.11.1.min.js"></script>
    <script src="/page/js/bootstrap/bootstrap-3.0.1.min.js"></script>
</head>
<body>

    <div class="container">
        <div class="row" style="margin-top: 10px">
            <div class="col-md-12 column">
                <img alt="130x31" width="92" height="29" src="/page/images/yingyan-logo.png" />
            </div>
        </div>

        <%--创建广告主begin--%>
        <div class="row" style="margin-top: 20px">
            <div class="col-md-12 column">
                <a id="modal-281487" href="#aduInfo" role="button" class="btn btn-primary btn-large" data-toggle="modal">创建广告主</a>
                <div class="modal fade" id="aduInfo" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <h4 class="modal-title" id="myModalLabel">创建广告主
                                </h4>
                            </div>
                            <div class="modal-body">

                                <form class="form-horizontal" role="form">

                                    <div class="form-group">
                                        <label for="inputAduName" class="col-sm-2 control-label">用户名</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="inputAduName" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="inputAduCompName" class="col-sm-2 control-label">公司名称</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="inputAduCompName" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputAduContact" class="col-sm-2 control-label">联系人</label>
                                        <div class="col-sm-10">
                                            <input type="text" class="form-control" id="inputAduContact" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">性别</label>
                                        <div class="col-sm-10 ">
                                            <ul class="nav">
                                                <li class="dropdown" id="aduSex">
                                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">男<strong class="caret"></strong></a>
                                                    <ul class="dropdown-menu">
                                                        <li>
                                                            <a href="#">男</a>
                                                        </li>
                                                        <li class="divider"></li>
                                                        <li>
                                                            <a href="#">女</a>
                                                        </li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputAduTel" class="col-sm-2 control-label">联系方式</label>
                                        <div class="col-sm-10">
                                            <input type="tel" class="form-control" id="inputAduTel" />
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" id="aduCancel" data-dismiss="modal">关闭</button>
                                <button type="button" class="btn btn-primary" id="aduSubmit">创建</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--创建广告主end--%>

         <%--查询最近一个帐号begin--%>
        <div class="row" style="margin-top: 20px">           
            <div class="col-md-12 column">
                  <button type="button" class="btn btn-primary" id="searchAcct">查询最近一个帐号</button>
                <form class="form-horizontal" role="form" style="margin-top: 10px">
                    <div class="form-group">
                        <label for="inputAcctName" class="col-sm-2 control-label">账户名</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="inputAcctName" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputAcctUName" class="col-sm-2 control-label">账号</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="inputAcctUName" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputAduName" class="col-sm-2 control-label">密码</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="inputAcctPwd" />
                        </div>
                    </div>
                </form>
               
            </div>
        </div>
        <%--查询最近一个帐号end--%>

    </div>
    <script>
        //创建帐号
        $("#aduSubmit").click(function () {
            CreateAduInfo();
        })
        //关闭
        $("#aduCancel").click(function () {
            $(".alert").alert("close");
        })
        //性别选择
        $("#aduSex .dropdown-menu li a").click(function () {
            $("#aduSex").find("a").eq(0).html($(this).text() + "<strong class='caret'></strong>");
        })
        //查询最近一个帐号
        $("#searchAcct").click(function () {
            SearchLastAcctInfo();
        })

        function CreateAduInfo() {
            //信息
            var aduName = $("#inputAduName").val();
            var aduCompName = $("#inputAduCompName").val();
            var aduContact = $("#inputAduContact").val();
            var aduSex = $("#aduSex").find("a").eq(0).text();
            var aduTel = $("#inputAduTel").val();
            if (aduName == "" || aduCompName == "" || aduContact == "" || aduSex == "" || aduTel == "") {
                $(".modal-body").prepend("<div class='alert alert-warning'> <a href='#' class='close' data-dismiss='alert'>&times;</a> <strong>警告！</strong>请完善所有信息...</div>");
                return;
            }

            var ajaxData = { method: "CreateAduInfo", aduname: aduName, aducompname: aduCompName, aducontact: aduContact, adusex: aduSex, adutel: aduTel };
            $.ajax({
                type: "post",
                url: "/api/managerhandler.ashx",
                data: ajaxData,
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.errcode != "1") {
                        alert(result.errmsg);
                        return;
                    }
                    $("#aduInfo").modal("hide");
                    SearchLastAcctInfo();
                }
            })
        }

        //查询帐号
        function SearchLastAcctInfo() {
            var ajaxData = { method: "SearchLastAcctInfo" };
            $.ajax({
                type: "post",
                url: "/api/managerhandler.ashx",
                data: ajaxData,
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.errcode != "1") {
                        alert(result.errmsg);
                        return;
                    }
                    $("#inputAcctName").val(result.data.acctname);
                    $("#inputAcctUName").val(result.data.acctuname);
                    $("#inputAcctPwd").val(result.data.acctpwd);
                }
            })
        }

    </script>

</body>
</html>
