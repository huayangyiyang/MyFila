/**
 * 20180905
 */
/* 小能客服 */
var is_NTKF = false;
function xiaoneng_isExitsFunction() {
    NTKF_PARAM = {
        siteid: "at_1000",                       /*企业id*/
        settingid: "at_6666_9999",      /*应答客服组id，在客户端生成代码界面创建*/
        uid: $.cookie('MEMBER_IDENT'),           /*用户id，登录用户必填，游客为空字符串*/
        uname: $.cookie('UNAME'),               /*用户名，登录用户必填，游客为空字符串*/
        isvip: $.cookie('MEMBER_IDENT') ? 1 : 0,    /*是否为vip用户，登录用户可选，游客为"0"*/
        userlevel: $.cookie('MEMBER_LEVEL_ID')  /*网站用户级别，登录用户可选，游客不填*/
    };

    if(is_NTKF){
        xiaoneng_submit();
    }else{
        $.getScript('http://dl.ntalker.com/js/xn6/ntkfstat.js?siteid=at_1000',function(){
            is_NTKF = true;
            xiaoneng_submit();
        });
    }
}
function xiaoneng_submit() {
    try {
        if (typeof(eval(xiaoneng_openInPageChat)) == "function") {
            xiaoneng_openInPageChat();
            NTKF.im_openInPageChat();
        }
    }catch(e){
        NTKF.im_openInPageChat();
    }
}

$(function(){
	
    /*搜索历史*/
    var khst = ($.cookie('KEYWORD_HST') || '').split(',');
    if(!khst || !khst.push){
        khst = [];
    }else{
        khst.reverse();
        $.each(khst,function(i,item){
            if(item == '')return;
            $('#search_hst_list').append('<li class="table-view-cell"><a href="<{link app=b2c ctl=mobile_list}>?keyword='+item+'">'+item+'</a></li>');
        });
    }
    $('#search_form').on('submit',function(e){
        e.stopPropagation();
        var keyword = $(this).find('input[name=keyword]').val();
        if($.trim(keyword) == ''){
            return false;
        }
        khst.push(keyword);
        $.cookie('KEYWORD_HST', khst.join(','), { expires: 30});
    });
    /**
     * 迷你购物车
     */
     /*购物车数量加载*/
    $.getJSON('/index.php/openapi/cart/count',function(re){
         try{
             if(re.data.count>0){
                 $('.cart-count').removeClass('hidden').text(re.data.quantity);
             }
         }catch(e){

         }
    });
    /**
     * 登录\注册切换
     */
    if($.cookie('UNAME') && $.cookie('MEMBER_IDENT')){
        $('.is-unlogin').addClass('hidden');
        $('.is-login').removeClass('hidden');
        $('.is-login .uname').text($.cookie('UNAME'));

        /*重置密码底部登录入口*/
        $('.passport-gologin').hide();
    }
    $('.sidebar-cat').removeClass('hidden');

    /*内购商品区域限制用户显示 
    $.getJSON('/index.php/openapi/purchase_incode/check_member',function(re){
        if(re.result=='success'){
            $('#purchase_list').removeClass('hide');
        }else{
            $('#purchase_list').addClass('hide');
        }
    });*/
});

$( document ).ready(function() {
    /* 侧边菜单栏
    $( 'body .sidebar' ).simpleSidebar({
        settings: {
            opener: 'body #open-menu',
            wrapper: 'body .wrapper',
            animation: {
                duration: 500,
                easing: 'easeOutQuint'
            }
        },
        sidebar: {
            align: 'left',
            width: 327,
            closingLinks: 'a',
        }
    }); */
    /* 侧边菜单栏  new*/
	var $sidebar=new mSlider({dom:"#sidebar",distance:"311px",zIndex:3000});
	$("#open-menu").click(function(){$sidebar.open();})
});