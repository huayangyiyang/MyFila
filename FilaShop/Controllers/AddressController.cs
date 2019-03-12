﻿using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{
    public class AddressController : Controller
    {
        private MyFilaEntities db = new MyFilaEntities();
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Address
        public ActionResult Delete(int? addressid)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            //由id从数据库 查询到 用户要删除的数据完整信息
            Address address= db.Address.Where(a=>a.Id== addressid && a.UserId==user.Id).FirstOrDefault();
            //判断地址对象的Isdefault 是True 或false
            // Address Isdefaultaddress = new Address();
            Address firstaddress;
            if (address.Isdefault==true)
            {
                //查找到除本id外的其他的地址，修改默认地址的 值
                db.Address.Where(a => a.UserId == address.UserId).ToList().ForEach(a => { a.Isdefault = false; });
                //查找到非本条数据外的数据集合中的第一条数据
                firstaddress = db.Address.Where(a => a.UserId == address.UserId && a.Id != addressid).FirstOrDefault();
                if (firstaddress != null)
                {
                    firstaddress.Isdefault = true;
                }
            }
            
                db.Address.Remove(address);
                db.SaveChanges();
            
            
            return Content("ok");
        }

        /// <summary>
        /// 添加新地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public ActionResult Add(Address address)
        {
            Userinfo user= Session["user"] as Userinfo;
            if (user !=null)
            {
                address.UserId = user.Id;
                
                if (address.Isdefault==true)
                {
                    //默认收货地址只能有1个，如果其他的默认收货地址，要把其他收货地址更改为非默认的
                   var oleaddresslist =  db.Address.Where(a => a.UserId == user.Id && a.Isdefault ==true).FirstOrDefault();

                    if (oleaddresslist !=null)
                    {
                        oleaddresslist.Isdefault = false;
                        address.Isdefault = true;
                    }
                       
                    

                }else
                {
                    address.Isdefault = false;
                    
                }
                db.Address.Add(address);
                db.SaveChanges();



                return RedirectToAction("Detail", "Orders");
            }else
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        /// <summary>
        /// 修改  --默认地址
        /// </summary>
        /// <param name="addressid"></param>
        /// <returns></returns>
        public ActionResult Update(int? addressid)

        {
            Userinfo user = Session["user"] as Userinfo;
            if (user==null)
            {
                return RedirectToAction("Login", "User");
            }
            Address address = db.Address.Find(addressid);
            var address2 = db.Address.Where(a => a.Isdefault == true && a.UserId == address.UserId).FirstOrDefault();
            if (address.Isdefault ==false)
            {
                if (address2!=null)
                {
                    address2.Isdefault = false;
                    address2.Id = address2.Id;
                    address2.ReceiverName = address2.ReceiverName;
                    address2.Phone = address2.Phone;
                    address2.DetailAddress = address2.DetailAddress;
                    address2.AreaId = address2.AreaId;
                }
               
                address.Isdefault = true;
                db.SaveChanges();
            }

            return RedirectToAction("Detail","Orders");
        }

       
        /// <summary>
        /// Edit编辑地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(Address naddress)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }


            //查找数据库中的本条记录的isdetail是true或false
             var oldaddress=db.Address.Find(naddress.Id);
            Address firstaddress;
            if (oldaddress!=null && oldaddress.Isdefault==true)
            {
                //如果把默认值改为false，则选择其余中的第一项设为默认值
                if (naddress.Isdefault == false || naddress.Isdefault==false )
                {
                    firstaddress= db.Address.Where(a => a.UserId == user.Id && a.Id != naddress.Id).FirstOrDefault();
                    firstaddress.Isdefault = true;
                    oldaddress.Isdefault = false;
                }else
                {
                    oldaddress.Isdefault = true;
                }

                //不改变默认地址的值，但是改变其他项的值
               
                oldaddress.ReceiverName = naddress.ReceiverName;
                oldaddress.Phone = naddress.Phone;
                oldaddress.AreaId = naddress.AreaId;
                oldaddress.DetailAddress = naddress.DetailAddress;
                


            }


            db.SaveChanges();
            return RedirectToAction("Detail","Orders");
        }


        public ActionResult List(int? Id)
        {
            Userinfo user = Session["user"] as Userinfo;
            if (user==null)
            {
                return RedirectToAction("Login", "User");
            }

            IEnumerable<Address> addresslist = db.Address.Where(a => a.UserId == user.Id);

            return View(addresslist.ToList());
        }
    }
}