<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadimg.aspx.cs" Inherits="BMH.EagleEye.page.test.uploadimg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div>
      上传   <input type="file" id="fileuploadimg" name="file" class="fileupload " onchange="fileChange(this)" accept="图片/png,gif,jpg" size="100" /> 
    </div>
    
    <script src="../js/jquery/jquery-1.11.1.min.js"></script>
    <script src="../js/base/v2.0/ajaxfileupload.js" type="text/javascript"></script>  
    <script>

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
            //判断文件格式
            var fileFormat = name.substring(name.lastIndexOf(".") + 1).toLowerCase();
            if (fileFormat != "jpg" && fileFormat != "gif" && fileFormat != "png") {
                alert("请选择图片格式文件上传！");
                target.value = "";
                return
            }
            //判断文件大小
            if (fileSize <(1024 * 10)) {
                alert("请选择大于10K的图片！");
                target.value = "";
                return;
            }
            if (fileSize > (1024 * 500))
            {
                alert("请选择小于500K的图片！");
                target.value = "";
                return;
            }
            
            
            ajaxFileUpload(id);
            
        }
        function ajaxFileUpload(contralid) {
            $.ajaxFileUpload({
                url: '/page/test/uploadimg.ashx',
                //用于文件上传的服务器端请求地址
                secureuri: false,
                //一般设置为false
                fileElementId: contralid,
                //文件上传空间的id属性  <input type="file" id="file" name="file" />
                dataType: 'json',
                //返回值类型 一般设置为json
                success: function (data, status) //服务器成功响应处理函数
                {
                    //if (contralid == "fileuploadimg") {
                    //    $(".uploadImg img").attr("src", data.fileurl);
                    //    $("#imgprewurl").attr("src", data.fileurl);
                    //    $("#imgtexturl").attr("src", data.fileurl);
                    //    $("#imageurl").val(data.fileurl);

                    //} else {
                    //    $("#linkurl").val(data.fileurl);
                    //}

                    //if (typeof (data.error) != 'undefined') {
                    //    if (data.error != '') {
                    //        alert(data.error);
                    //    } else { }
                    //}
                },
                error: function (data, status, e) //服务器响应失败处理函数
                {
                    alert(e);
                }
            })
            return false;
        }



    </script>
</body>
</html>
