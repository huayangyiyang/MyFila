/**
 * @Ljp 产品页面
 */
$(function(){
	$(".jqzoom").imagezoom();
	$(document).on("mouseover",".smallpic a",function(){
		$(this).addClass('selected').siblings().removeClass("selected");
		var imgSrc = $(this).find('img').attr("data-big");
		$('.jqzoom').attr('src',imgSrc);
		$('.jqzoom').attr('rel',imgSrc);
	});
	$(document).on("click","ul.product-color-choice li",function(){
		$(this).addClass("select").siblings().removeClass("select");
		var nowColor = $(this).attr("data-color");
		$("p.product-color span").html( nowColor );
		changePicList(nowColor);
		$("div.product-size button.size").attr("data-size","").html("尺码选择   +");
		$('.product-size-choice').fadeOut("fast",function(){ $("div.product-size-choice ul").html(""); });
		return true;
	});
	//choise
	$(document).on("click",".product-size .size",function(){
		getSizeListFromColor();
		if( $('.product-size-choice').css("display") == "block" ){
			$('.product-size-choice').fadeOut();
		} else {
			$('.product-size-choice').fadeIn();
		}
		$(this).html('尺码选择 -');
		return true;
	});
	//choise size
	$(document).on("click",'.product-size-choice li:not(.on)',function(){
		var nsize = $(this).children('a').html();
		var maxnum = $(this).attr("data-number");
		$(".product-size .size").attr("data-size",nsize).attr("data-count",maxnum).html('尺码选择('+nsize+') +');
		$('.product-size-choice').fadeOut();
		$(".product-size").find("input[name=number]").val("1");
		return true;
	});
	//number add or less
	$(document).on("click","div.product-size button#changenumber",function(){
		$('div.product-buy').prev('.prompt').remove();
		var obj = $(this),type = obj.attr("data-type"),num = obj.siblings("input[name=number]").val(),mnum = $(".product-size .size").attr("data-count"),size = $(".product-size .size").attr("data-size");
		if( !size ){ $("div.product-buy").before("<p class='prompt' style='width:270px; margin:10px auto 20px !important;'>请选择商品尺码</p>");  setTimeout("$('div.product-buy').prev('.prompt').remove()",2000); return true;  }
		if( type == 'less' ) num --; else num++;
		if( type == 'less' && num <= 0 ){ return false; }
		if( type == 'add' && num > mnum ){ 
			$("div.product-buy").before("<p class='prompt' style='width:270px; margin:10px auto 20px !important;'>宝贝库存不足</p>");  
			setTimeout("$('div.product-buy').prev('.prompt').remove()",2000);
			return false;
		}
		obj.siblings("input[name=number]").val(num);
		return true;
	});
	//addcart or buy now
	$(document).on("click","div.product-buy button",function(){
		$('div.product-buy').prev('.prompt').remove();
		var color = $("ul.product-color-choice li.select").attr("data-color"),size = $("div.product-size button.size").attr("data-size"),number = $("div.product-size input").val(),proid = $(this).parent().attr("data-pid"),type = $(this).attr("data-type");
		if( !size ){ $("div.product-buy").before("<p class='prompt' style='width:270px; margin:10px auto 20px !important;'>请选择商品尺码</p>");  setTimeout("$('div.product-buy').prev('.prompt').remove()",2000); return false; }
		if( !color ){ $("div.product-color-choice").before("<p class='prompt' style='width:270px; margin:10px auto 20px !important;'>请选择商品颜色</p>"); setTimeout("$('div.product-buy').prev('.prompt').remove()",2000); return false; }
		if( !number || number<=0 ) return false;
		$.post(buyItemsUrl,{color:color,size:size,number:number,proid:proid,type:type},function( result ){
			if( result.status == '0' ){
				if( type == 'addcart' ){
					$("div.shoppingCart").remove();
					$("div.nav").append(result.data);
					$("div.product-buy").before("<p class='prompt' style='color:green !important; width:270px; margin:10px auto 20px !important;'>宝贝已加入购物车</p>");
					setTimeout("$('div.product-buy').prev('.prompt').remove()",2000);
					$("a.shopping span").html(result.msg); return true;
				} else window.location.href = result.msg;
				 return true;
			} else if( result.status == '2' ) {
				iframeForLogin(); return false;
			} else {
				$("div.product-buy").before("<p class='prompt' style='width:270px; margin:10px auto 20px !important;'> "+result.msg+"</p>");
				return false;
			}
		},"json");
	});
	/**
	 * collect
	 */
	$(document).on("click","div.product-buy-operation a#collect",function(){
		var proId = $(this).attr("data-pid");
		if( !proId ) { return false; }
		$.post(collectUrl,{proid:proId},function( result ){
			if( result.status == '1' ){
				return false;
			}else if( result.status == '2' ){
				iframeForLogin(); return false;
			} else{
				$("div.product-buy-operation a#collect").addClass("select").attr("title","取消收藏");
				return true;
			}
		},"json");
	});
	/**
	 * @Ljp close login iframe
	 */
	$(document).on("click",".sign img.close",function(){
		$("div.signPopup").remove(); return true;
	});
	//运费详情
	$(".postage-express-btn").hover(function(){
		$(".postage-express").fadeIn();
	},function(){
		$(".postage-express").fadeOut();
	})
	//default size
	//getSizeListFromColor(true);
});
/*
* change piclist 2015/5/13 @Ljp
*/
function changePicList(color){
	var picStr = '';
	var optStr = $.parseJSON(skulist); //解析json
	if( optStr ){
		for( var i=0; i<optStr.length; i++ ){
			if( optStr[i].pro_color == color ){
				if(optStr[i].pic1) picStr += '<a class="selected"><img src="'+imgCdbUrl+'/product'+optStr[i].pic1+'" data-mig="'+imgCdbUrl+'/product'+optStr[i].pic1+'" data-big="'+imgCdbUrl+'/product'+optStr[i].pic1+'"/></a>';
				if(optStr[i].pic2) picStr += '<a><img src="'+imgCdbUrl+'/product'+optStr[i].pic2+'" data-mig="'+imgCdbUrl+'/product'+optStr[i].pic2+'" data-big="'+imgCdbUrl+'/product'+optStr[i].pic2+'"/></a>';
				if(optStr[i].pic3) picStr += '<a><img src="'+imgCdbUrl+'/product'+optStr[i].pic3+'" data-mig="'+imgCdbUrl+'/product'+optStr[i].pic3+'" data-big="'+imgCdbUrl+'/product'+optStr[i].pic3+'"/></a>';
				if(optStr[i].pic4) picStr += '<a><img src="'+imgCdbUrl+'/product'+optStr[i].pic4+'" data-mig="'+imgCdbUrl+'/product'+optStr[i].pic4+'" data-big="'+imgCdbUrl+'/product'+optStr[i].pic4+'"/></a>';
				$("div.smallpic").html(picStr);
				var defaultImg = imgCdbUrl+'/product/images/default.jpg';
				if( optStr[i].pic1 ) defaultImg = imgCdbUrl+'/product'+optStr[i].pic1;
				else if( optStr[i].pic2 ) defaultImg = imgCdbUrl+'/product'+optStr[i].pic2;
				else if( optStr[i].pic3 ) defaultImg = imgCdbUrl+'/product'+optStr[i].pic3;
				else if( optStr[i].pic4 ) defaultImg = imgCdbUrl+'/product'+optStr[i].pic4;
				$("div.bigpic").find("img").attr("src",defaultImg);
				$("div.bigpic").find("img").attr("rel",defaultImg);
			}
		}
	}
}

/**
 *根据颜色获取尺码 @Ljp 20170817
 */
function getSizeListFromColor(){
	var color = $("ul.product-color-choice li.select").attr("data-color");
	var size = $("div.product-size button.size").attr("data-size");
	var optStr = $.parseJSON(skulist);
	var reStr = "",flag = false;
	if( optStr ){
		for(var i=0; i<optStr.length;i++){
			if( optStr[i].pro_color == color ){
				if( optStr[i].number > 0 ){
					for(var j=0; j<optStr[i].number; j++){
						var classStr = "";
						if( size && optStr[i].colorsize[j].pro_sizes == size) classStr = ' doselected';
						if( !size && !flag && optStr[i].colorsize[j].pro_count > 0){ classStr = 'doselected'; flag = true; }
						if(optStr[i].colorsize[j].pro_count <=0) classStr = 'on';
						reStr += '<li class="'+classStr+'" data-number="'+optStr[i].colorsize[j].pro_count+'" data-sizes="'+optStr[i].colorsize[j].pro_sizes+'"><a>'+optStr[i].colorsize[j].pro_sizes+'</a></li>';
					}
				}
			}
		}
	}
	$("div.product-size-choice ul").html(reStr);
	/*if( loading ){
		var nsize = $("li.doselected").children('a').html();
		var maxnum = $("li.doselected").attr("data-number");
		$(".product-size .size").attr("data-size",nsize).attr("data-count",maxnum).html('尺码选择('+nsize+') +');
		$('.product-size-choice').fadeOut();
	}*/
}

/**
 * loginiFrame  @Ljp 20170817
 */
function iframeForLogin(){
	$("div.signPopup").remove();
	var optStr = "";
	optStr += '<div class="signPopup">';
	optStr += '<div class="sign data-table">';
	optStr += '<img src="'+baseUrl+'/images/close.png" class="close">';
	optStr += '<img src="'+baseUrl+'/images/nav/logo.png" class="logo"/>';
	optStr += '<input type="text" placeholder="请输入帐号或手机号码" id="Laccount"/>';
	optStr += '<input type="password" placeholder="输入密码" id="Lpassword"/>';
	optStr += '<div class="statement">';
	optStr += '<b class="fl"></b>';
	optStr += '<a href="javascript:;">（记住我）两周内自动登录</a>';
	optStr += '</div>';
	optStr += '<button>立即登录</button>';
	optStr += '<div>';
	optStr += '<a href="/Forget/index.html" class="forgotPassword">忘记密码</a>';
	optStr += '<a href="/Member/register.html" class="sign-register">注册361°帐号</a>';
	optStr += '</div>';
	optStr += '</div>';
	$("body").append( optStr );
}