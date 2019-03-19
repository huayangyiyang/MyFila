using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AddOrderNumber;
using FilaShop.Filter;

namespace FilaShop.Controllers
{
    [LoginFilter]
    public class OrdersController : Controller
    {
        private MyShopEntities db = new MyShopEntities();

        // GET: Orders
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="textpiece_num"></param>
        /// <param name="texttotal_text"></param>
        /// <param name="Goodsid"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public ActionResult Add(int? addressid, double textpiece_num, double texttotal_text, int?[] Goodsid,int?[] Number)
        {
            
            Userinfo user = Session["user"] as Userinfo;
            if (user!=null)
            {

           
            //新增订单
            Orders orders = new Orders {
                UserId = user.Id,
                OrderTime = DateTime.Now,
                Price = (decimal)texttotal_text,
                AddressId= addressid,
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

                    //同时对应得商品减去相应的库存
                    Goods gooods = db.Goods.Find(Goodsid[i]);
                    gooods.Amount = gooods.Amount - Number[i];

            };

            //向订单中添加完毕后，购物车的相关数据清空
            IQueryable<int?> cartdelete = Goodsid.AsQueryable();
            var cartlist = db.Cart.Where(c =>  c.UserId == user.Id && cartdelete.Contains(c.GoodsId));
            db.Cart.RemoveRange(cartlist);
            
            db.SaveChanges();
          
            //获取最新添加的订单编号
           // int Id = orders.Id;
            return RedirectToAction("OrderDetail", new {Id=orders.Id });
            }else
            {
                return RedirectToAction("Login", "User");
            }
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

        /// <summary>
        /// 订单详情页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult OrderDetail(int? Id)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }else
            {
                ViewBag.cartcount = db.Cart.Where(c => c.UserId == user.Id).Sum(c => c.Number);
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

        //Id=21&Id=2019031818565900001&sum=1960.00&paytype=2&name=确认支付
        /// <summary>
        /// 确认支付页面，修改订单状态   为 已支付
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ordernum"></param>
        /// <param name="sum"></param>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public ActionResult Pay(int? Id,string ordernum,double sum,int paytype)
        {
            Userinfo user = Session["user"] as Userinfo;

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.user = user;
            Orders order = db.Orders.Find(Id);
            order.OrderState = 1;
            db.SaveChanges();

            ViewBag.Id = Id;
            ViewBag.ordernum = ordernum;
            ViewBag.paytype = paytype;
            ViewBag.sum = sum;
            return View();
        }
    }
}