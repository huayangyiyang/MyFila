	$(function(){
		//底部二维码移入效果
		$('.footerConcern-us .WeChat').hover(function(){
			$('.footerConcern-us .code').fadeIn();
		},function(){
			$('.footerConcern-us .code').fadeOut();
		});
		$(window).scroll(function() {
			var scroHei = $(window).scrollTop();//滚动的高度
			if (scroHei > 400) {
				$('.return-top').fadeIn();
			} else {
				$('.return-top').fadeOut();
			}
		})
		var swiper = new Swiper('.banner', {
			pagination: '.swiper-pagination',
			paginationClickable: '.swiper-pagination',
			spaceBetween: 30,
			centeredSlides: true,
			autoplay: 4000,
			effect: 'fade'
		});
		/*点击返回顶部*/
		$('.return-top').click(function() {
			$('body,html').animate({
			scrollTop: 0
			}, 600);
		})

	});
	var swiper = new Swiper('.swiper-navactivelist', {
	        pagination: '.swiper-pagination',
	        paginationClickable: '.swiper-pagination',
	        spaceBetween: 30,
	        effect: 'fade'
	    });

	var swiper = new Swiper('.swiper-technology', {
	        pagination: '.swiper-pagination',
	        paginationClickable: '.swiper-pagination',
	        spaceBetween: 30,
	        effect: 'fade',
	        nextButton: '.swiper-button-next',
	        prevButton: '.swiper-button-prev'
	    });

	var heights = $('.nav').height();
	function navHeight(){
		$('.science').mouseenter(function(){
			$('.shoppingCart').css({"height":"0"})
			$('.personal-con').css({"height":"0"})
			$('.mall-nav').stop().animate({"height":"0"})
			$('.activity').stop().animate({"height":"0"})
			$('.dropdown').css({"top":heights})
			$('.dropdown').stop().animate({"height":"195px"},function(){
				$(this).stop().animate({"height":"185px"})
			})
		})
		$('.dropdown').mouseenter(function(){
			$('.dropdown').css({"bottom":"-185px","height":"185px"})
		})
		$('.dropdown').mouseleave(function(){
			$('.dropdown').stop().animate({"bottom":"0","height":"0"})
		})
		$('#activity').mouseenter(function(){
			$('.shoppingCart').css({"height":"0"})
			$('.personal-con').css({"height":"0"})
			$('.mall-nav').stop().animate({"height":"0"})
			$('.dropdown').stop().animate({"height":"0"})
			$('.activity').css({"top":heights})
			$('.activity').stop().animate({"height":"195px"},function(){
				$(this).stop().animate({"height":"185px"})
			})
		})
		$('.activity').mouseenter(function(){
			$('.activity').css({"height":"195px","top":heights})
		})
		 $('.activity').mouseleave(function(){
			$('.activity').stop().animate({"bottom":"0","height":"0"})
		})
		    
		$('.mall').mouseenter(function(){
			$('.shoppingCart').css({"height":"0"})
			$('.personal-con').css({"height":"0"})
			$('.dropdown').stop().animate({"bottom":"0","height":"0"})
			$('.activity').stop().animate({"bottom":"0","height":"0"})
			$('.mall-nav').stop().animate({"height":"340px"},function(){
				$(this).stop().animate({"height":"330px"})
			})
		})
		$('.mall-nav').mouseleave(function(){
			$('.mall-nav').stop().animate({"height":"0"})
		})
		    
		$('.personal').mouseenter(function(){
			$('.mall-nav').stop().animate({"height":"0"})
			$('.dropdown').stop().animate({"bottom":"0","height":"0"})
			$('.activity').stop().animate({"bottom":"0","height":"0"})
			$('.shoppingCart').css({"height":"0"})
			$('.personal-con').css({"height":"auto"})
		})
		$('.personal-con').mouseleave(function(){
			$('.personal-con').css({"height":"0"})
		})
		$('.shopping').mouseenter(function(){
			$('.mall-nav').stop().animate({"height":"0"})
			$('.dropdown').stop().animate({"bottom":"0","height":"0"})
			$('.activity').stop().animate({"bottom":"0","height":"0"})
			$('.personal-con').css({"height":"0"})
			$('.shoppingCart').css({"height":"auto"})
		})
		$('.shoppingCart').mouseleave(function(){
			$('.shoppingCart').css({"height":"0"})
		})
	}
	navHeight();
	   
	$(function(){ 
		$(document).on("mouseenter",'#myscroll li',function(){
			var imgSrc = $(this).find('img').attr("data-bimg");
			$(this).parent().parent().parent().siblings("a").children("img").attr('src',imgSrc);
			return true;
		});
		/**-------------JS-------------**/
		$(document).on("click","#left",function(){
			var now = $(this).parent().attr("data-now");
			var num = $(this).parent().attr("data-number");
			var width = parseInt($(this).parent().siblings("div#myscroll").find("li").width())+8;
			$(this).parent().siblings("div#myscroll").find("ul").css({"position":"relative","width":parseInt(width*num)});
			now--;
			if( now < 0 ){ now = 0;  return false; }
			if( now == 0 ) $(this).css({"display":"none"});
			$(this).siblings("#right").css({"display":"block"});
			$(this).parent().attr("data-now",now);
			$(this).parent().siblings("div#myscroll").find("ul").animate({"left":-parseInt(now*width)});
			return true;
		});
		$(document).on("click","#right",function(){
			var now = $(this).parent().attr("data-now");
			var num = $(this).parent().attr("data-number");
			var width = parseInt($(this).parent().siblings("div#myscroll").find("li").width())+8;
			$(this).parent().siblings("div#myscroll").find("ul").css({"position":"relative","width":parseInt(width*num)});
			now++;
			if( now > parseInt(num-4) ){ now = num;  return false; }
			if( now == parseInt(num-4) ) $(this).css({"display":"none"});
			$(this).siblings("#left").css({"display":"block"});
			$(this).parent().attr("data-now",now);
			$(this).parent().siblings("div#myscroll").find("ul").animate({"left":-parseInt(now*width)});
			return true;
		});
      	})

      	var nav = $('.nav'),
	doc = $(document),
	win = $(window);
	win.scroll(function() {
		if (doc.scrollTop() > 80) {
			nav.addClass('scrolled');
			$('.nav-top').hide();
			heights = '65px';
			navHeight();
			$('.personal-con').css({"top":"65px"})
			$('.shoppingCart').css({"top":"65px"})
			$('.shoppingCart').css({"height":"0"})
	    		$('.personal-con').css({"height":"0"})
	    		$('.mall-nav').css({"height":"0"})
	    		$('.dropdown').css({"bottom":"0","height":"0"})
	    		$('.activity').css({"bottom":"0","height":"0"})
		} else {
			$('.nav-top').show();
			nav.removeClass('scrolled');
			heights = '110px';
			navHeight();
			$('.personal-con').css({"top":"110px"})
			$('.shoppingCart').css({"top":"110px"})
			$('.shoppingCart').css({"height":"0"})
	    		$('.personal-con').css({"height":"0"})
	    		$('.mall-nav').css({"height":"0"})
	    		$('.dropdown').css({"bottom":"0","height":"0"})
	    		$('.activity').css({"bottom":"0","height":"0"})
		}
	})