using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// 购物车展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
           
          //  string cgoodsid = Request.QueryString["goodsid"];
            string cgoodsname = Request.QueryString["goodsname"];
            string cgoodsnewprice = Request.QueryString["goodsnewprice"];
            string cgooodscolor = Request.QueryString["gooodscolor"];
            string cgoodssize = Request.QueryString["goodssize"];
            string cgoodsnumber = Request.QueryString["goodsnumber"];
            string cgoodsimage = Request.QueryString["goodsimgae"];

            //将用户选中的商品加入购物车，根据用户是否等，用户登录  直接加入数据库，没有登录直接加入cookies
            cart cart = new cart
            {
                goodsname = cgoodsname,
                goodsimage=cgoodsimage,
                goodsnewprice=Convert.ToDecimal( cgoodsnewprice),
                goodssize=cgoodssize,
                goodsnumber=Convert.ToInt32( cgoodsnumber),
                goodscolor=cgooodscolor
            };

            //定义一个集合存储cart对象
            IList<cart> clist = new List<cart>();
            clist.Add(cart);
           // HttpCookie cartlistcookie = new HttpCookie("cartlist", clist);


            return View();
        }
    }
}