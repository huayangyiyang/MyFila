
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
    public class CartController : Controller
    {
        private MyShopEntities db = new MyShopEntities();
        /// <summary>
        /// 打开购物车页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(int? id)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            //session中的user不为空
            //查询当前用户的购物车中共有多少件商品
           // ViewBag.cartcount = db.Cart.Where(c => user.Id == c.UserId).Sum(c => c.Number);

            //当前用户的购物车的所有物品
            IEnumerable<Cart> clist = db.Cart.Where(c => c.UserId == user.Id).OrderByDescending(c => c.Id);
            ViewBag.cartcount = clist.Sum(c => c.Number);
           

            //当前用户的收货地址列表
            ViewBag.addresslist = db.Address.Where(a => a.UserId == user.Id).ToList();
            //addrelist;


            //获取所有地址的父级
            ViewBag.areaParent = db.Area.Where(a => a.ParentId == null).ToList();


            //由编辑按钮传值过来的id查找
            ViewBag.editAddress= db.Address.Find(id);

            return View(clist.ToList());
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ActionResult Add(int? productid)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user !=null)
            {
                Cart cart = new Cart();

                cart.UserId = user.Id;
                cart.GoodsId  = productid;
                //添加到购物车前，要做判断，购物车是否已经存在该物品
               var  cartList = db.Cart.Where(c => c.UserId == user.Id && c.GoodsId == productid);
                if (cartList.Count()== 1)
                {
                    //购物车中已经存在该物品
                    Cart nowcart = cartList.FirstOrDefault();
                    nowcart.Number++;
                    
                }
                else
                { 
                    //购物车中不存在该物品
                    cart.Number = 1;
                    db.Cart.Add(cart);
                }

                db.SaveChanges();
                return RedirectToAction("List", "Cart");
            }else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Update(int? cartid, string stryunsuan)
        {
            
         
            Cart cart = db.Cart.Find(cartid);
            if (stryunsuan == "-")
            {
                if (cart.Number > 1)
                {
                    cart.Number = cart.Number - 1;
                }else
                {
                    return Content("商品数量不能小于1");
                }
                
            }
            else if (stryunsuan == "+")
            {
                cart.Number = cart.Number + 1;
            }
            
            db.SaveChanges();
            return Content("ok");
        }


        public ActionResult Delete(int? id)
        {

            db.Cart.Remove(db.Cart.Find(id));
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult AreaList(int? id)
        {
            return Json(db.Area.Where(a => a.ParentId == id).Select(a => new
            {
                Id = a.Id,
                Name = a.Name,
                ParentId = a.ParentId
            }), JsonRequestBehavior.AllowGet);
        }
    }
}