using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// 商城  启始页
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            Userinfo userinfo = new Models.Userinfo();
            Session.Add("user", userinfo);
            
            return View();
        }

        /// <summary>
        /// 商城商品展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Shop()
        {
            return View();
        }

        /// <summary>
        /// 商品详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }
    }
}