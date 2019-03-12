using FilaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Controllers
{
    public class AreaController : Controller
    {
        private MyFilaEntities db = new MyFilaEntities();
        // GET: Area
        public ActionResult List(Int32? id)
        {
            Response.Write("地址id=" + id);
            return 
               Json(db.Area.Where(a => a.ParentId == id).Select(a => new
               {
                   Id = a.Id,
                   Name = a.Name,
                   ParentId = a.ParentId
               }), JsonRequestBehavior.AllowGet);
        }
    }
}