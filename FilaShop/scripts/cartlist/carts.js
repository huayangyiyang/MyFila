/**
 * Created by Administrator on 2017/5/24.
 */

$(function () {

    //全局的checkbox选中和未选中的样式
    var $allCheckbox = $('input[type="checkbox"]'),     //全局的全部checkbox
        $wholeChexbox = $('.whole_check'),
        $cartBox = $('.cartBox'),                       //每个商铺盒子
        $shopCheckbox = $('.shopChoice'),               //每个商铺的checkbox
        $sonCheckBox = $('.son_check');                 //每个商铺下的商品的checkbox
    $allCheckbox.click(function () {
        if ($(this).is(':checked')) {
            $(this).next('label').addClass('mark');
        } else {
            $(this).next('label').removeClass('mark')
        }
    });

    //===============================================全局全选与单个商品的关系================================
    $wholeChexbox.click(function () {
        var $checkboxs = $cartBox.find('input[type="checkbox"]');
        if ($(this).is(':checked')) {
            $checkboxs.prop("checked", true);
            $checkboxs.next('label').addClass('mark');
        } else {
            $checkboxs.prop("checked", false);
            $checkboxs.next('label').removeClass('mark');
        }
        totalMoney();
    });


    $sonCheckBox.each(function () {
        $(this).click(function () {
            if ($(this).is(':checked')) {
                //判断：所有单个商品是否勾选
                var len = $sonCheckBox.length;
                var num = 0;
                $sonCheckBox.each(function () {
                    if ($(this).is(':checked')) {
                        num++;
                    }
                });
                if (num == len) {
                    $wholeChexbox.prop("checked", true);
                    $wholeChexbox.next('label').addClass('mark');
                }
            } else {
                //单个商品取消勾选，全局全选取消勾选
                $wholeChexbox.prop("checked", false);
                $wholeChexbox.next('label').removeClass('mark');
            }
        })
    })

    
    //=================================================商品数量==============================================

    /*
    var $plus = $('.plus'), //加号
        $reduce = $('.reduce'),//减号
        $all_sum = $('.sum');  //求和
        //+号
    $plus.click(function () {
        //购物车编号
        $cartid = $(this).prev().prev().prev('input');
        
        //获取当前行的数量
        var $inputVal = $(this).prev('input'),
            $count = parseInt($inputVal.val()) + 1,
        
            $obj = $(this).parents('.amount_box').find('.reduce'),  
            $priceTotalObj = $(this).parents('.order_lists').find('.sum_price'),//单行总价
            $price = $(this).parents('.order_lists').find('.price').html(),  //单价
            $priceTotal = $count * parseInt($price.substring(1));
        $inputVal.val($count);
        $priceTotalObj.html($priceTotal);//单行总价
        
        if($inputVal.val()>1 && $obj.hasClass('reSty')){
            $obj.removeClass('reSty');
        }
        totalMoney();
        
       // $.get(tourl, { 'Id': $cartid, 'stryunsuan': '+' }, function () { });
        //alert('加法==购物车编号：' + $cartid.val());
    });

    //-号
    $reduce.click(function () {
        $cartid = $(this).prev('input');
    	//输入框中的数量
        var $inputVal = $(this).next('input'),
            $count = parseInt($inputVal.val())-1,
            $priceTotalObj = $(this).parents('.order_lists').find('.sum_price'),//单行总价
            $price = $(this).parents('.order_lists').find('.price').html(),  //单价
            $priceTotal = $count * parseInt($price.substring(1));

        if($inputVal.val()>1){
            $inputVal.val($count);
            $priceTotalObj.html($priceTotal);//单行总价
        }
        if($inputVal.val()==1 && !$(this).hasClass('reSty')){
            $(this).addClass('reSty');
        }
        totalMoney();
        //alert('购物车编号' + Id);
        //$.get(tourl, { 'Id': $cartid, 'stryunsuan': '-' }, function () { });
        
    });

    $all_sum.keyup(function () {
        var $count = 0,
            $priceTotalObj = $(this).parents('.order_lists').find('.sum_price'),//单行总价
            $price = $(this).parents('.order_lists').find('.price').html(),  //单价
            $priceTotal = 0;
        if($(this).val()==''){
            $(this).val('1');
        }
        $(this).val($(this).val().replace(/\D|^0/g,''));
        $count = $(this).val();
        $priceTotal = $count*parseInt($price.substring(1));
        $(this).attr('value',$count);
        $priceTotalObj.html($priceTotal);
        totalMoney();
    })
    */
    //======================================移除商品========================================

    var $order_lists = null;
    var $order_content = '';
    
    $('.delBtn').click(function () {
       // cartnowid = $(this).parent().parent().prev().prev().find('input[name="Cartid"]').val();
        $order_lists = $(this).parents('.order_lists');
        $order_content = $order_lists.parents('.order_content');
        $('.model_bg').fadeIn(300);
        $('.my_model').fadeIn(300);
    });

    //关闭模态框
    $('.closeModel').click(function () {
        closeM();
    });
    $('.dialog-close').click(function () {
        closeM();
    });
    function closeM() {
        $('.model_bg').fadeOut(300);
        $('.my_model').fadeOut(300);
    }
    //确定按钮，移除商品
    $('.dialog-sure').click(function () {
        $order_lists.remove();
        if($order_content.html().trim() == null || $order_content.html().trim().length == 0){
            $order_content.parents('.cartBox').remove();
        }
        closeM();
        $sonCheckBox = $('.son_check');
        totalMoney();
         //数据库使用ajax移除
       // $.post(deleterul, { 'id': cartnowid }, function () { });
    })

    //======================================总计==========================================

    function totalMoney() {
        var total_money = 0;
        var total_count = 0;
        var calBtn = $('.calBtn a');
        $sonCheckBox.each(function () {
            //.substring(1)
            if ($(this).is(':checked')) {
                var goods = parseInt($(this).parents('.order_lists').find('.sum_price').html().substring(1));
                var num =  parseInt($(this).parents('.order_lists').find('.sum').val());
                total_money += goods;
                total_count += num;
            }
        });
        //总价的
        $('.total_text').html(total_money);
        $('.piece_num').html(total_count);

        // console.log(total_money,total_count);

        if(total_money!=0 && total_count!=0){
            if(!calBtn.hasClass('btn_sty')){
                calBtn.addClass('btn_sty');
            }
        }else{
            if(calBtn.hasClass('btn_sty')){
                calBtn.removeClass('btn_sty');
            }
        }
    }


});