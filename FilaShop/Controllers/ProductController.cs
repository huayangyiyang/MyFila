﻿using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{
    public class ProductController : Controller
    {
        private MyShopEntities db = new MyShopEntities();
        /// <summary>
        /// 商品列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int? producttypeid,int? order,int? currentpage,string keyword)
        {
            /*
            Userinfo user = new Userinfo
            {
                Id = 1,
                Username = "admin",
                uPassword = "123456",
                Nickname="张文"
            };
            Session.Add("user", user);
            */
            //商品集合，且是已经上架商品
           IEnumerable<Goods> list= db.Goods.Where(p => p.OnSale == true);
            //页面中左侧商品类别列表集合
            ViewBag.ProductTypes = db.GoodsType.Where(g => g.PId == null).OrderBy(ptype=>ptype.Id);
            var user = Session["user"] as Userinfo;
            if (user != null)
            {
                ViewBag.cartcount = db.Cart.Where(c => c.UserId == user.Id).Sum(c => c.Number);
            }


            //搜索
            if (keyword!=null && keyword !="")
            {
                list = list.Where(p => p.Name.Contains(keyword));
            }
            else
            {
               // return RedirectToAction("List", "Product");
            }

            Response.Write(keyword);
            //producttypeid  在页面中，用户点击了分类中的一项的筛选后的结果
            if (producttypeid !=null)
            {
                list = list.Where(p => p.TypeId == producttypeid);
                
                ViewBag.producttype = db.GoodsType.Find(producttypeid);
            }

            // 排序  方式【综合排序，按照id降序，新价格降序，销量降序】
            if (order!=null)
            {
                switch (order)
                {
                    case 1://综合排序，按照id降序，新价格降序，销量降序
                        list = list.OrderByDescending(p => p.Id).OrderByDescending(py => py.NewPrice).OrderByDescending(pyt => pyt.Saled);
                        break;
                    case 2://销量降序
                        list = list.OrderByDescending(pyt => pyt.Saled);

                        break;
                    case 3://新价格升序
                        list = list.OrderBy(py => py.NewPrice);
                        break;
                    case 4://综合排序，新价格降序
                        list = list.OrderByDescending(py => py.NewPrice);
                        break;
                    case 5://上架时间
                        list = list.OrderByDescending(p => p.Id);
                        break;
                    default:
                       
                        break;
                }

            }

            ViewBag.order = order;

            //分页
            ViewBag.allCount = list.Count();
            int pageSize;
            if (currentpage ==null)
            {
                currentpage = 1;
            }
            pageSize = 5;
                list = list.Skip(((Int32)currentpage - 1) * pageSize).Take(pageSize);
            
            ViewBag.currentpage = currentpage;//当前页
            ViewBag.pageSize = pageSize;//每页多少条数据



            return View(list.ToList());
        }


        /// <summary>
        /// 商品详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(int? productid)
        {
            //页面中左侧商品类别列表集合
            ViewBag.ProductTypes = db.GoodsType.Where(g => g.PId == null).OrderBy(ptype => ptype.Id);

            //购物车中当前用户的商品数量
            var user = Session["user"] as Userinfo;
            if (user != null)
            {
                ViewBag.cartcount = db.Cart.Where(c => c.UserId == user.Id).Sum(c => c.Number);
                ViewBag.user = user;
            }

            //由商品id查询到的商品具体信息
            Goods productdetail= db.Goods.Find(productid);
            //商品尺码表
            ViewBag.productSizes = db.GoodsSize;
            //商品颜色表
            ViewBag.productColors = db.GoodsColor;

            return View(productdetail);
        }

        /// <summary>
        /// 核实商品库存是否充足
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public ActionResult CheckGoodsAccount(int? goodsid)
        {
            return Json(db.Goods.Find(goodsid).Amount,JsonRequestBehavior.AllowGet);
        }
    }
}