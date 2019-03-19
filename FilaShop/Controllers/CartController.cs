
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
            
            //当前用户的购物车的所有物品
            IEnumerable<Cart> clist = db.Cart.Where(c => c.UserId == user.Id).OrderByDescending(c => c.Id);
            ViewBag.cartcount = clist.Sum(c => c.Number);
           

            //当前用户的收货地址列表
            ViewBag.addresslist = db.Address.Where(a => a.UserId == user.Id).ToList();
            //addrelist;
            

            //获得当前的省级菜单
            ViewBag.province = db.Area.Where(a => a.ParentId == null).ToList();

            //由编辑按钮传值过来的id查找
            ViewBag.editAddress= db.Address.Find(id);


            //获取当前商品的库存
           // int? amount = db.Goods.Find(goodsid).Amount;
           // ViewBag.amount = amount;
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
            else if (stryunsuan=="=")
            {
                cart.Number = cart.Goods.Amount;
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

        /// <summary>
        /// 购物车页面添加新地址
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAddress(Address address)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user != null)
            {
                address.UserId = user.Id;

                if (address.Isdefault == true)
                {
                    //默认收货地址只能有1个，如果其他的默认收货地址，要把其他收货地址更改为非默认的
                    var oleaddresslist = db.Address.Where(a => a.UserId == user.Id && a.Isdefault == true).FirstOrDefault();

                    if (oleaddresslist != null)
                    {
                        oleaddresslist.Isdefault = false;
                        address.Isdefault = true;
                    }
                }
                else
                {
                    address.Isdefault = false;

                }
                db.Address.Add(address);
                db.SaveChanges();



                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

           
        }



        /// <summary>
        /// 购物车页面修改收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAddress(Address naddress)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }


            //查找数据库中的本条记录的isdetail是true或false
            var oldaddress = db.Address.Find(naddress.Id);
            Address firstaddress;
            if (oldaddress != null && oldaddress.Isdefault == true)
            {
                //如果把默认值改为false，则选择其余中的第一项设为默认值
                if (naddress.Isdefault == false || naddress.Isdefault == false)
                {
                    firstaddress = db.Address.Where(a => a.UserId == user.Id && a.Id != naddress.Id).FirstOrDefault();
                    firstaddress.Isdefault = true;
                    oldaddress.Isdefault = false;
                }
                else
                {
                    oldaddress.Isdefault = true;
                }

                //不改变默认地址的值，但是改变其他项的值

                oldaddress.ReceiverName = naddress.ReceiverName;
                oldaddress.Phone = naddress.Phone;
                oldaddress.AreaId = naddress.AreaId;
                oldaddress.DetailAddress = naddress.DetailAddress;



            }

            //获取到当前用户的最新订单
            Orders neworder = db.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.OrderTime).FirstOrDefault();
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}