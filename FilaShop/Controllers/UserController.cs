using Comman;
using FilaShop.Filter;
using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{

    public class UserController : Controller
    {
        private MyShopEntities db = new MyShopEntities();
        /// <summary>
        /// 打开登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnurl)
        {
            if (Session["errormessage"] !=null )
            {
                ViewBag.errormessage = Session["errormessage"];
            }

            var regreturnurl = Session["returnurl"] as string;
            if (regreturnurl != null && regreturnurl !="" && returnurl==null || returnurl =="")
            {
                ViewBag.returnurl = regreturnurl;
            }else
            {
                ViewBag.returnurl = returnurl;
            }
            
           
            //赋值后，清空session
            Session["returnurl"] = "";
            return View();
        }

        /// <summary>
        /// 登录页面提交用户名和密码后的判断
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string login_username, string login_password,string returnurl)
        {
            var jiamipwd = Comman.Md5.JiaMi(login_password);
            var user = db.Userinfo.Where(u => u.Username == login_username && u.Password == jiamipwd);


            //解析url地址转化为control  和action
          
            var count = user.Count();
            if (count == 1)
            {
                //登陆成功
                Session.Add("user", user.FirstOrDefault());
                if (returnurl != null && returnurl != "")
                {
                    return Redirect("~"+returnurl);
                    //RedirectToAction(actionstr, controlstr);
                }else
                {
                    
                 return RedirectToAction("List", "Product");
                }
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
        public ActionResult Register(string returnurl)
        {
            ViewBag.returnurl = returnurl;
            return View();
        }

        public ActionResult Register(FilaShop.User.User user)
        {
            Models.Userinfo u = new Models.Userinfo
            {
                Username = user.Username,
                Password = Comman.Md5.JiaMi(user.Password),
                Nickname = user.Nickname
            };
            db.Userinfo.Add(u);
            db.SaveChanges();
            if (user.returnurl!=null && user.returnurl != "")
            {
                Session.Add("returnurl", user.returnurl);
            }

            return RedirectToAction("Login");
        }

        // 通过 ajax 验证表单是否正确
        [HttpPost]
        public ActionResult AjaxRegister(FilaShop.User.User user)
        {

            // View() ViewResult            响应一个 html 视图页
            // File() FileContentResult     响应一个文件
            // Json() JsonResult            响应json 字符串 "[ {} , {} , {} ]"
            // Content() ContentResult      响应一个 纯 字符串

            // 判断 验证码是否正确
            if (user.VCode.ToLower() != Session["VCode"].ToString().ToLower())
            {
                return Json(new Comman.JsonResult
                {
                    Result = ResultType.Error,
                    ErrorMessage = "验证码错误"
                });
            }

            // 判断 用户名是否已被使用
            int userCount = db.Userinfo.Where(u => u.Username == user.Username).Count();
            if (userCount == 1)
            {
                return Json(new Comman.JsonResult
                {
                    Result = ResultType.Error,
                    ErrorMessage = "用户名已被使用"
                });
            }

            return Json(new Comman.JsonResult
            {
                Result = ResultType.Success,
                ErrorMessage = string.Empty
            });
        }

        /// <summary>
        /// 生成验证码的控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            Comman.VerifyCode vCode = new Comman.VerifyCode();

            string verifyCodeString = vCode.GetVerifyCode(4);
            Session.Add("VCode", verifyCodeString);

            Image image = vCode.GetVerifyImage(verifyCodeString);

            // ViewResult
            // FileContextResult
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return File(ms.ToArray(), "image/jpeg");
            //Response.ContentType = "image/jpeg";
            //Response.BinaryWrite(ms.ToArray());
        }

        /// <summary>
        /// 用户详情页面
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Detail(int? Id)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user==null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.user = user;
            //判断用户名是否存在
            
            var userself = db.Userinfo.Where(u => u.Id == Id && u.Id == user.Id).FirstOrDefault();

            return View(userself);
        }


        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult LoginOut(string returnUrl)
        {
            //用户退出登录就是清空session
            Session["user"] = "";
            if (returnUrl ==null || returnUrl=="")
            {
                return RedirectToAction("List", "Product");
            }else
            {
                return Redirect("~" + returnUrl);
            }
            
                //Redirect("~"+returnUrl);
        }


        /// <summary>
        /// 用户管理中心  ：包含用户信息的修改查看，订单信息展示，查看，部分权限修改；收货地址展示修改
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult CommandCenter(int? stateid,int? id)

        {//进入用户管理中心，首先得是登录用户

            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
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


        public ActionResult CheckUserName(string Username)
        {
            return Json(db.Userinfo.Where(u => u.Username == Username).Select(u => new
            {
                Id = u.Id,
                Name = u.Username,
                pwd=u.Password,
                nickname=u.Nickname
            }), JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user1"></param>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Update(int? Id, string Username,string Nickname,string uPassword)
        {

            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }


            if (Id==user.Id)
            {
                Userinfo u = db.Userinfo.Find(Id);
                u.Username = Username;
                u.Nickname = Nickname;
                u.Password =uPassword;
                db.SaveChanges();
                return RedirectToAction("Detail", new { Id = Id });
            }else
            {
                return RedirectToAction("Login", "User");
            }
                


        }
    }
}