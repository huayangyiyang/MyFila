using FilaShop.Filter;
using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{
    [LoginFilter]
    public class OrderDetailController : Controller
    {

        private MyShopEntities db = new MyShopEntities();
        /// <summary>
        ///  用户中心  订单详情页面
        /// </summary>
        /// <returns></returns>
        // GET: OrderNumber
        public ActionResult Detail(int? Id,double? sum)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.user = user;

                IEnumerable<Orders> orderlist = db.Orders.Where(o => o.Id == Id && o.UserId == user.Id);

                //查询到1条数据，由ID查找
                var order = db.Orders.Where(o => o.Id == Id && o.UserId == user.Id).FirstOrDefault();

                return View(order);
            }


               
        }
    }
}