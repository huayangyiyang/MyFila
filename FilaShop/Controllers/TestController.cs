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
        private MyFilaEntities db = new MyFilaEntities();
        // GET: Test
        public ActionResult List()
        {

            return View();
        }


       
        
    }
}