//获取url参数值
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}
//获取匹配地址 
//inUrl原地址
//inKey参数名
//inVal参数值
function getMatchUrl(inUrl, inKey, inVal) {
    var url = inUrl;
    //var tempVal = getUrlParam(inKey);
    if (url.indexOf('?') > -1) {
        if (url.length == url.indexOf('?') + 1) {
            url = url + inKey + "=" + inVal;
        }
        else {
            var tempStr = url.substring(url.indexOf('?') + 1);
            if (tempStr.indexOf(inKey) > -1) {               
                tempStr = tempStr.substring(tempStr.indexOf(inKey));
                if (tempStr.indexOf("&") > -1) {
                    tempStr = tempStr.substring(0, tempStr.indexOf("&"));
                }
                url = url.replace(tempStr, inKey + "=" + inVal);
            }
            else {
                url = url + "&" + inKey + "=" + inVal;
            }
        }
    }
    else {
        url = url + "?" + inKey + "=" + inVal;
    }
    return url;
}
//清除地址上匹配参数
function clearMatchUrl(inUrl, inKey) {
    var url = inUrl;    
    if (url.indexOf('?') > -1) {
        if (url.length == url.indexOf('?') + 1) {
           
        }
        else {
            var tempStr = url.substring(url.indexOf('?') + 1);
            if (tempStr.indexOf(inKey) > -1) {
                tempStr = tempStr.substring(tempStr.indexOf(inKey));
                if (tempStr.indexOf("&") > -1) {
                    tempStr = tempStr.substring(0, tempStr.indexOf("&")+1);
                }
                url = url.replace(tempStr, "");
            }           
        }
    }  
    return url;
}
//获取时间
//AddDayCount 
function GetDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount); //获取AddDayCount天后的日期
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1; //获取当前月份的日期
    var d = dd.getDate();
    var toDouble = function (num) {
        return num = num < 10 ? '0' + num : '' + num
    }
    return toDouble(y) + "-" + toDouble(m) + "-" + toDouble(d);
}
//获取地址参数值-裁剪方式
function getUrlVal(inKey) {
    var tempVal = "";
    var url = decodeURI(window.location);
    var key = getUrlParam(inKey);
    if (url.indexOf('?') > 0) {
        if (url.length != url.indexOf('?') + 1) {
            if (key != null) {
                var keyStr = url.substring(url.indexOf(inKey), url.length);
                if (keyStr.indexOf("&") > 0) {
                    keyStr = keyStr.substr(0, keyStr.indexOf("&"));
                }
                if (keyStr.indexOf("=") > 0) {
                    keyStr = keyStr.substring(keyStr.indexOf("=")+1, keyStr.length);
                    tempVal = keyStr;
                }
            }
        }
    }
    return tempVal;
}
//加载效果-需加载样式
var load = function () {
    return {
        loadInit: function () {
            //获取浏览器页面可见高度和宽度
            var _PageHeight = document.documentElement.clientHeight,
     _PageWidth = document.documentElement.clientWidth;
            document.body.style.overflow = 'hidden';

            var _LoadingHtml = '<div id="loadingDiv" style="background:#fff;overflow:hidden;width:' + _PageWidth + 'px;height:' + _PageWidth + 'px;z-text-align: center;font-family: 微软雅黑;color:#12bdce;"><div style="position:absolute;index:999999999;left:50%;top:50%;margin-left:-55px;margin-top:-42px;"><div class="loader-inner pacman"><div></div><div></div><div></div><div></div><div></div></div><p style="font-size:16px;letter-spacing:1px;">页面加载中，请稍等...</p></div></div></div>';
            //呈现loading效果
            document.write(_LoadingHtml);

            //监听加载状态改变
            document.onreadystatechange = load.completeLoading;
        },
        //加载状态为complete时移除loading效果
        completeLoading: function () {
            if (document.readyState == "complete") {
                var loadingMask = document.getElementById('loadingDiv');
                loadingMask.parentNode.removeChild(loadingMask);
                document.body.style.overflow = 'auto';
            }
        }
    }
} ();
//输入值替换-（限制只能输入数字）
function Num(obj) {
    //obj.value = obj.value.replace(/\D/gi, "");
    alert(obj.value);
    alert(obj.value.length);
}
//输入值替换-（限制只能输入浮点型数字）
function Float(obj) {
    obj.value = obj.value.replace(/[^\d.]/g, ""); //清除"数字"和"."以外的字符
    obj.value = obj.value.replace(/^\./g, ""); //验证第一个字符是数字
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个, 清除多余的
}
function Limit(obj, num) {
    if (obj.value.length > num) {
        obj.value = obj.value.substring(0, num);
     }
}