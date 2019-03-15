using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Handler2 的摘要说明
    /// </summary>
    public class Handler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            HttpCookie cookie = context.Request.Cookies["user"];
            context.Response.Write(cookie);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}