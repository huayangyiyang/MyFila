using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilaShop.Filter
{
    public class LoginFilter : ActionFilterAttribute
    {
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/User/Login");
                filterContext.HttpContext.Session.Add("errormessage", "系统检测到您还没有登录");
            }
            
        }


    }
}