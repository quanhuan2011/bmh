<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BMH.EagleEye.page.login" %>

<!DOCTYPE html>
<!-- saved from url=(0028)http://yingyan.baomihua.com/ -->
<html lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	
	<meta name="keywords" content="移动广告平台／广告平台／DSP广告／鹰眼推送／专业APP推广／P@P推广/精准推广／移动营销／杭州鹰眼">
	<meta name="description" content="鹰眼广告营销平台，是专注于做国内最智能，最高效的移动广告智能营销平台，为广告主提供基于大数据的移动精准的产品推广和品牌营销服务！移动营销，首选鹰眼！">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<link rel="stylesheet" type="text/css" href="/page/css/login.css">	
    <script src="/page/js/jquery/jquery-1.11.1.min.js"></script>
	<title>鹰眼-精准高效的智能广告营销平台-主页</title>
</head>
<body>
<!--header-->
<div class="header-bg">
	<div class="logo">
		<img src="/page/images/logo.png" alt="logo" class="fadeInUp animated" width="320">
		<!-- <h1 class="fadeInDown animated">精准移动 营销平台</h1> -->
	</div>
    <form runat="server">
	<div class="loginArea fadeInUp animated">
		<input ID="txtUserName" type="text" class="username" placeholder="请输入您的账号" runat="server">
		<input id="txtPassWord" type="password" class="username" placeholder="请输入密码" runat="server">
		
     <asp:Button ID="btnLogin" class="submit_btn"  runat="server" Text="登录" 
         onclick="btnLogin_Click" />
	</div>	 
      </form>
	<div class="targers animated fadeInRightBig"></div>
	<div id="recommend" class="animated bounceInLeft">
<!-- 		<div id="recommend-title">
	                <span class="title">鹰眼特色</span>
	                <span id="left-line"></span>
	                <span id="right-line"></span>
	            </div> -->
		<ul id="recommend-content" class="clearfix">
			<li class="animated bounceInLeft" >
				<h3>精准投放</h3>
				精准的定向投放能力，帮助您瞄准活跃的商业目标人群！
			</li>
			<li class="two animated bounceInLeft">
				<h3>高效点击</h3>
				通过大数据运算让您的广告对用户更有价值，有效的提高转化！
			</li>
			<li class="three animated bounceInLeft">
				<h3>透明成本</h3>
				您可以对您每一笔广告的每一笔支出和收益了如指掌！
			</li>
		</ul>
	</div>
	
</div>
<!--/header-->

<!--鹰眼特色-->
<!-- <div class="container character">
	<h2 class="tit">鹰眼特色</h2>
	<ul class="manage-type clearfix">
		<li class="first revealOnScroll animated fadeInUp" data-animation="fadeInUp" data-timeout="500">
			<h3>精准投放</h3>
			<p>精准的定向投放能力，帮助您瞄准活跃的商业目标人群！</p>
		</li>
		<li class="second revealOnScroll animated fadeInUp" data-animation="fadeInUp" data-timeout="1000">
			<h3>高效点击</h3>
			<p>通过大数据运算让您的广告对用户更有价值，有效的提高转化！</p>
		</li>
		<li class="third revealOnScroll animated fadeInUp" data-animation="fadeInUp" data-timeout="1500">
			<h3>透明成本</h3>
			<p>您可以对您每一笔广告的每一笔支出和收益了如指掌！</p>
		</li>
	</ul>
</div> -->
<!--/鹰眼特色-->
<!--合作伙伴-->
<div class="graybg">
	<div class="partner container">
		<h2 class="tit">鹰眼合作伙伴</h2>
		<div class="partner-listWrap">
			<ul class="partner-list clearfix">
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="200"><img src="/page/images/yrhy.png" height="70" width="237" alt=""></li>
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="400"><img src="/page/images/hzmh.png" height="62" width="250" alt=""></li>
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="600"><img src="/page/images/bycx.png" height="88" width="232" alt=""></li>
				<li class="parts revealOnScroll" data-animation="fadeInUp" data-timeout="800"><img src="/page/images/xaxy.png" height="77" width="220" alt=""></li>

				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="1000"><img src="/page/images/hzzj.png" height="84" width="250" alt=""></li>				
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="1200"><img src="/page/images/jk.png" height="88" width="239" alt=""></li>
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="1400"><img src="/page/images/qmhd.png" height="88" width="190" alt=""></li>
				<li class="parts revealOnScroll" data-animation="fadeInUp" data-timeout="1600"><img src="/page/images/cjwh.png" height="64" width="200" alt=""></li>

				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="1800"><img src="/page/images/rzgg.png" height="83" width="230" alt=""></li>
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="2000"><img src="/page/images/czxx.png" height="88" width="97" alt=""></li>
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="2200"><img src="/page/images/jxwl.png" height="88" width="124" alt=""></li>				
				<li class="parts revealOnScroll" data-animation="fadeInUp" data-timeout="2400"><img src="/page/images/dd.png" height="88" width="124" alt=""></li>
				
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="2600"><img src="/page/images/xahw.png" height="88" width="88" alt=""></li>
				<li class="revealOnScroll" data-animation="fadeInUp" data-timeout="2800"><img src="/page/images/hzxl.png" height="74" width="74" alt=""></li>
				
			</ul>
		</div>
	</div>
</div>
<!--/合作伙伴-->
<!--商务咨询-->
<div class="container-box">
	<div class="container character consulting" id="contact-us">
		<h2 class="tit">商务咨询</h2>
		<ul class="manage-type clearfix revealOnScroll" data-animation="fadeInUp" data-timeout="500">
			<li class="first">
				<h3>0571-89935005</h3>
			</li>
			<li class="second">
				<h3>441864609</h3>
			</li>
			<li class="third">
				<h3>yingyan@baomihua.com</h3>
			</li>
		</ul>
	</div>	
</div>

<!--/商务咨询-->

<!--联系我们-->
<div class="footer-bg">
	<div class="contact-us container" style="padding:12px 0;">
		<!-- <ul class="clearfix contact-det" >
			<li class="first">
				<h3>联系我们</h3>
				<p>商务QQ：441864609</p>
				<p>合作邮箱：yingyan@baomihua.com</p>
				<p>合作热线：18668011986</p>
			</li>
			<li class="second">
				<h3>关注我们</h3>
				<p>商务QQ：441864609</p>
				<p>微信公众号：yingyan@baomihua.com</p>
				<p>合作热线：18668011986</p>
			</li>
			<li class="third">
				<h3>扫一扫，关注公众号</h3>
				<img src="img/erweima.png" alt="公众号">
			</li>
		</ul> -->
		<p class="txt" style="padding:0;">杭州爆米花科技有限公司  版权所有 @2011-2017   浙B2-20100382</p>
	</div>
</div>
<!--/联系我们-->


<!--侧边栏-->
<div class="sidebar">
	<a class="to-top" id="toTop" href="javascript:;"><span>置顶</span></a>
	<a class="contact-us-link" id="contact-btn" href="javascript:;"><span>联系我们</span></a>
</div>
<!--/侧边栏-->

<script>
var bodyHeight = $(window).height();
$('.header-bg').height(bodyHeight)
if($(window).width()<1150){
	$('body').css('overflowX','auto')
}
var mainHight = $("#contact-us").offset().top;
$("#toTop").click(function() {
        $('body,html').animate({
          scrollTop: 0
        },1000);
        return false;
});
$("#contact-btn").click(function() {
        $("body,html").animate({
          scrollTop: mainHight
        },1000);
        return false;
});
var $window = $(window);
$window.on("scroll", revealOnScroll);
function revealOnScroll() {
    var scrolled = $window.scrollTop(),
    	winHeight = $window.height();
    $(".revealOnScroll:not(.animated)").each(function () {
        var $this = $(this),
            offsetTop = $this.offset().top,
        	itemOuterHeight = $this.outerHeight();
        if (!(scrolled > offsetTop+itemOuterHeight) && !(scrolled < offsetTop-winHeight)) {
        	if ($this.data("timeout")) {
            	window.setTimeout(function(){
            		$this.addClass("animated " + $this.data("animation"));
            	}, parseInt($this.data('timeout'),10));
        	} else {
        		$this.addClass("animated "+ $this.data("animation"));
        	}
        }
   })
}

</script>

</body></html>