using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{
    
    public class TestController : Controller
    {
        private MyShopEntities db = new MyShopEntities();
        // GET: Test
        public ActionResult List()
        {
            ViewBag.Area = db.Area.ToList();
            return View(db.Address.ToList());
        }


       
        
    }
}