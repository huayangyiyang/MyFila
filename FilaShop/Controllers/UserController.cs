using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{

    public class UserController : Controller
    {
        private MyFilaEntities db = new MyFilaEntities();
        /// <summary>
        /// 打开登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录页面提交用户名和密码后的判断
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string login_username, string login_password)
        {
            var user = db.Userinfo.Where(u => u.Username == login_username && u.uPassword == login_password);

            var count = user.Count();
            if (count == 1)
            {
                //登陆成功
                Session.Add("user", user.FirstOrDefault());
                return RedirectToAction("List", "Product");
            }
            else
            {
                ViewBag.wrongMessage = "用户名或密码错误";
                return View();
            }

        }

        /// <summary>
        /// 打开注册页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// 用户详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(int? Id)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user==null)
            {
                return RedirectToAction("Login", "User");
            }

            var userself = db.Userinfo.Where(u => u.Id == Id && u.Id == user.Id).FirstOrDefault();

            return View(userself);
        }


        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            //用户退出登录就是清空session
            Session["user"] = null;
            return RedirectToAction("List", "Product");
        }


        /// <summary>
        /// 用户管理中心  ：包含用户信息的修改查看，订单信息展示，查看，部分权限修改；收货地址展示修改
        /// </summary>
        /// <returns></returns>
        public ActionResult CommandCenter(int? stateid)

        {//进入用户管理中心，首先得是登录用户

            Userinfo user = new Userinfo
            {
                Id = 2,
                Username = "admin",
                Nickname = "王小二"
            };
            //根据用户查找  购物车数量
            ViewBag.user = user;
            if (user != null)
            {
                ViewBag.cartcount = db.Cart.Where(c => c.UserId == user.Id).Sum(c => c.Number);
            }

            //订单列表数据
            IEnumerable<Orders> selforders = db.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.OrderTime);

            //根据订单状态筛选
            if (stateid == null)
            {
                selforders = db.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.OrderTime);
            }


            switch (stateid)
            {
                case 0:
                    //待付款 
                    selforders = selforders.Where(o => o.OrderState == 0);

                    break;
                case 1:
                    //待发货 
                    selforders = selforders.Where(o => o.OrderState == 1);

                    break;
                case 2:
                    // 待收货 
                    selforders = selforders.Where(o => o.OrderState == 2);

                    break;
                case 3:
                    // 取消 
                    selforders = selforders.Where(o => o.OrderState == 3);

                    break;
                case 4:
                    // 退货 
                    selforders = selforders.Where(o => o.OrderState == 4);

                    break;
                case 5:
                    // 交易成功，交易关闭 
                    selforders = selforders.Where(o => o.OrderState == 5);
                    break;

                default:
                    break;
            }


            ViewBag.stateid = stateid;
            return View(selforders.ToList());

        }
    }
}