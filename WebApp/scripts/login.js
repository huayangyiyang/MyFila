$(function(){
	//换验证码
	$(document).on("click","img#change-count,a.change-count",function(){
		var timenow = new Date().getTime();
        		$("img#change-count").attr("src","/Index/verify.html?t="+timenow);
        		return true;
	});
	//登录验证
	$(document).on('click','.sign button',function(){
		var login = "0";
		var Laccount = $('#Laccount').val();
		var Lpassword = $('#Lpassword').val();
		var reg = /^[a-zA-Z0-9]{6,20}$/;
		$("#Lpassword").prev('.prompt').remove();//删除提示语
		$("#Laccount").prev('.prompt').remove();//删除提示语
		if( !Laccount){ 
			$("#Laccount").before("<p class='prompt'>* 帐号不能为空</p>");//增加提示语
			return false; 
		}
		if(!Lpassword){
			$("#Lpassword").before("<p class='prompt'>* 密码不能为空</p>");//增加提示语
			return false; 
		}
		if(!reg.test(Lpassword)){
			$("#Lpassword").before("<p class='prompt'>* 请输入6-20位数字与字母组合密码</p>");//增加提示语
			return false; 
		}
		if( !$("div.statement b").hasClass("read") ) login = 1;
		$.post(loginUrl,{acc:Laccount,pwd:Lpassword,nologin:login,loginfrom:loginfrom},function( result ){
			if( result.status == '1' ){
				$("#Laccount").before("<p class='prompt'>* "+result.msg+"</p>");//增加提示语
				return false;
			} else {
				if( loginfrom == 'login') window.location.href = result.msg; else{ window.location.reload( true ); $("div.signPopup").remove(); }
				return true;
			}
		},"json");
	});
	//nologin
	$(document).on("click","div.statement b",function(){
		$(this).toggleClass("read"); return true;
	})
	//注册帐号验证
	$(document).on('click','.register-btn',function(){
		var obj="",nickname = $('#nickname').val(), mobile=$('#mobile').val(),code = $('#sms_code').val(),pwd = $('#password').val(),repwd = $('#repassword').val(),isread = "1";
		var reg = /^1[3|4|5|7|8]\d{9}$/;
		$("#nickname,#password,#repassword,div.statement").prev('.prompt').remove();
		$('#mobile,#sms_code').parent().prev('.prompt').remove();
		if(!nickname){ obj = $('#nickname'); $('#nickname').before("<p class='prompt'>* 昵称不能为空</p>"); return false; }
		if (!mobile) { obj = $('#mobile').parent();  $('#mobile').parent().before("<p class='prompt'>* 手机号不能为空</p>"); return false; }
		if(!reg.test(mobile)){ obj = $('#mobile').parent();  $('#mobile').parent().before("<p class='prompt'>* 请输入正确的手机号</p>"); return false; }
		if(!code){ obj = $('#sms_code').parent();  $('#sms_code').parent().before("<p class='prompt'>* 验证码不能为空</p>"); return false; }
		if(code.length < 6){ obj = $('#sms_code').parent();  $('#sms_code').parent().before("<p class='prompt'>* 验证码错误</p>"); return false; }
		if(!pwd){ obj = $('#password');  $('#password').before("<p class='prompt'>* 密码不能为空</p>"); return false; }
		if(pwd.length < 6 || pwd.length > 16){ obj = $('#password');  $('#password').before("<p class='prompt'>* 请输入6至16位密码</p>"); return false; }
		if( !repwd ){ obj = $('#repassword'); $('#repassword').before("<p class='prompt'>* 请重复输入密码</p>"); return false; 
		} else if(pwd != repwd){ obj = $('#repassword');  $('#repassword').before("<p class='prompt'>* 两次输入密码不一致</p>"); return false; }
		if( $("div.statement b").hasClass("read") ) isread = 0;
		if( !isread ){ obj = $('div.statement');  $("div.statement").before("<p class='prompt'>* 请阅读并同意《361°商城协议》</p>"); return false; }
		$.post(regUrl,{nickname:nickname,mobile:mobile,code:code,pwd:pwd,repwd:repwd,isread:isread},function( result ){
			if( result.status == '1' ){
				$('#nickname').before("<p class='prompt'>* "+result.msg+"</p>");  return false;
			} else if( result.status == '2' ) {
				$('#nickname').before("<p class='prompt'>* "+result.msg+"</p>"); return false;
			} else {
				window.location.href = result.data; return true;
			}
		},"json");
	})
	//找回密码验证
	$(document).on('click','#checkforgetcode',function(){
		var mobile = $('#mobile').val(),
			code = $('#sms_code').val();
		var reg = /^1[3|4|5|7|8]\d{9}$/;	
		$('#mobile').parent().prev('.prompt').remove();
		$('#sms_code').parent().prev('.prompt').remove();
		if (!mobile) {
			$('#mobile').parent().before("<p class='prompt'>* 手机号不能为空</p>");
			return false;
		}
		if(!reg.test(mobile)){
			$('#mobile').parent().before("<p class='prompt'>* 请输入正确的手机号</p>");
			return false;
		}
		if(!code){
			$('#sms_code').parent().before("<p class='prompt'>* 验证码不能为空</p>");
			return false;
		}
		if(code.length < 6){
			$('#sms_code').parent().before("<p class='prompt'>* 请输入6位短信验证码</p>");
			return false;
		}
		$.post(checkSendcode,{mobile:mobile,code:code,stype:stype},function( result ){
			if( result.status == '0' ){
				window.location.href = result.msg; return true;
			} else {
				$('#mobile').parent().before("<p class='prompt'>* "+result.msg+"</p>"); return false;
			}
		},"json")
	})
	//获取短信验证码
	$(document).on("click","#message",function(){
		var mobile = $('#mobile').val();
		var jscode = $("#jscode").val();
		var reg = /^1[3|4|5|7|8]\d{9}$/;
		$('#mobile').parent().prev('.prompt').remove();
		$('#sms_code').parent().prev('.prompt').remove();
		$('#jscode').parent().prev('.prompt').remove();
		$('#nickname').prev('.prompt').remove();
		if (!mobile) { $('#mobile').parent().before("<p class='prompt'>* 手机号不能为空</p>");return false; }
		if(!reg.test(mobile)){ $('#mobile').parent().before("<p class='prompt'>* 请输入正确的手机号</p>"); return false; }
		if (!jscode) { $('#jscode').parent().before("<p class='prompt'>* 请输入右边算法结果</p>");return false; }
		$.post(sendCode,{mobile:mobile,stype:stype,jscode:jscode},function( result ){
			if( result.status == '0' ){
				changeTimeDown( result.data ); return true;
			} else {
				$('#nickname').before("<p class='prompt'>* "+result.msg+"</p>"); return false;
			}
		},"json");
	})
	//修改密码 @Ljp 20170817
	$(document).on("click","#forgeteditpwd",function(){
		var pwd = $("#password").val(), repwd= $("#repassword").val();
		$('#password').prev('.prompt').remove();
		$('#repassword').prev('.prompt').remove();
		if(!pwd){
			$('#password').before("<p class='prompt'>* 密码不能为空</p>");
			return false;
		}
		if(pwd.length < 6 || pwd.length > 20){
			$('#password').before("<p class='prompt'>* 请输入6至20位数字、字母</p>");
			return false;
		}
		if( !repwd ){
			$('#repassword').before("<p class='prompt'>* 请重复输入密码</p>");
			return false; 
		}else if(pwd != repwd){
			$('#repassword').before("<p class='prompt'>* 两次密码不一致</p>");
			return false; 
		}
		$.post(changepwd,{passwd:pwd,repasswd:repwd,ctype:ctype},function( result ){
			if( result.status == '1' ){
				$('#password').before("<p class='prompt'>* "+result.msg+"</p>");
				return false;
			} else {
				$('#password').before("<p class='prompt' style='color:green !important'>"+result.msg+"</p>");
				setTimeout(window.location.href=result.data,2000);
				return true;
			}
		},"json")
	});
})

/**
 * 倒计时 @Ljp 20170817
 */
function changeTimeDown( stime ){
	if( !stime ) stime = "60";
	time = stime;
	if (validCode) {
		validCode=false;
		code.addClass("msgs1");
		var t = setInterval(function  () {
			time--;
			code.html("重新获取 ( "+time+"S ) ");
			$(".show").hide();
			$(".hide").show();
			if (time==0) {
				time = 60;
				$(".hide").hide();
				$(".show").show();
				clearInterval(t);
				code.html("重新获取");
				validCode=true;
				code.removeClass("msgs1");
			}
		},1000);
  	 }
}