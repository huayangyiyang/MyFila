using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AddOrderNumber;

namespace FilaShop.Controllers
{
    public class OrdersController : Controller
    {
        private MyFilaEntities db = new MyFilaEntities();
        
        // GET: Orders
        public ActionResult Add(double total_text, double piece_num,int?[] Goodsid,int?[] Number)
        {
            
            Userinfo user = Session["user"] as Userinfo;
            //新增订单
            Orders orders = new Orders {
                UserId = user.Id,
                OrderTime = DateTime.Now,
                Price = (decimal)total_text,
                OrderState = 0,
                ordernum = AddNumber.AddSeralNum(DateTime.Now) 
           };
            db.Orders.Add(orders);
            //新增订单明细
            
            //循环添加商品到订单中
            for (int i = 0; i < Goodsid.Length; i++)
            {
                OrderDetail odetail = new OrderDetail
                {
                    Orders = orders,
                    GoodsId= Goodsid[i],
                    Number= Number[i],
                    
                };
                db.OrderDetail.Add(odetail);


            };

            //向订单中添加完毕后，购物车的相关数据清空
            IQueryable<int?> cartdelete = Goodsid.AsQueryable();
            var cartlist = db.Cart.Where(c =>  c.UserId == user.Id && cartdelete.Contains(c.ProductId));
            db.Cart.RemoveRange(cartlist);


            

            db.SaveChanges();
          
            //获取最新添加的订单编号
           // int Id = orders.Id;
            return RedirectToAction("Detail",new {Id=orders.Id });
        }




        /// <summary>
        /// 订单--详情，添加收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(string Id)
        {
             Userinfo user = Session["user"] as Userinfo;
               if (user == null)
               {
                   return RedirectToAction("Login", "User");
                }
               
            ViewBag.user = user;
            //当前用户的购物车的所有物品
           /// IEnumerable<Cart> clist = db.Cart.Where(c => c.UserId == user.Id).OrderByDescending(c => c.Id);
           // ViewBag.cartcount = clist.Sum(c => c.Number);


            //获得当前的省级菜单
            ViewBag.province = db.Area.Where(a => a.ParentId == null).ToList();

            //当前用户的收货地址列表
            ViewBag.addresslist = db.Address.Where(a => a.UserId == user.Id).ToList();



            //当前用户的最近订单
            int orderid = Convert.ToInt32(Id);
            // var uorder = db.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.OrderTime).FirstOrDefault();
            var uorder = db.Orders.Find(orderid);
            var uorderdetail = db.OrderDetail.Where(or => or.OrdersId == orderid).ToList();

            ViewBag.uorder = uorder;
            ViewBag.uorderdetail = uorderdetail;
            return View();
        }
    

        /// <summary>
        /// 给订单添加收货地址
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="addressid"></param>
        /// <returns></returns>
        public ActionResult Update(int? orderId ,int? addressid)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction( "Login", "User");
            }


            Orders oldorders = db.Orders.Find(orderId);
            oldorders.AddressId = addressid;
            db.SaveChanges();

            return RedirectToAction("OrderDetail", "Orders",new { Id= orderId });
        }

        public ActionResult OrderDetail(string Id)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }else
            {
                ViewBag.user = user;
                int orderid = Convert.ToInt32(Id);
                //当前用户的最近订单
                var uorder = db.Orders.Where(o=> o.Id== orderid && o.UserId==user.Id).FirstOrDefault();

                //下单时间格式化
                ViewBag.endtime = AddNumber.AddTimestr(uorder.OrderTime);

                
                var uorderdetail = db.OrderDetail.Where(or => or.OrdersId == orderid).ToList();

            ViewBag.uorder = uorder;
            ViewBag.uorderdetail = uorderdetail;

            return View();
            }
        }
/*
0 已下单，未付款
1 已付款，未发货
2 已发货，未签收
3 已签收，未评论
4 已评论
5 交易成功，交易关闭
6 退货中
7 已下单，未付款，取消
8 已付款，未发货，取消
9 已发货，未签收，取消
10 已签收，未评论 ，取消

*/
        public ActionResult UpdateState(int? id,string state,string memo)
        {   Userinfo user = Session["user"] as Userinfo;

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.user = user;
                Orders order= db.Orders.Where(o => o.Id == id && o.UserId == user.Id).FirstOrDefault();
               
                order.OrderState = 3;
                db.SaveChanges();
                return RedirectToAction("Detail", "OrderDetail", new { Id = id });
            }

                
        }
    }
}