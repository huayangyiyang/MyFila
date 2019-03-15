using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            HttpCookie cookies = new HttpCookie("user");
            cookies.Values.Add("1", "李白,123456,男,15698744563");
            cookies.Values.Add("2", "李白1,123456,男,15698744563");
            cookies.Values.Add("3", "李白2,123456,男,15698744563");
            cookies.Values.Add("4", "李白3,123456,男,15698744563");
            cookies.Values.Add("5", "李白4,123456,男,15698744563");

            context.Response.SetCookie(cookies);

            HttpCookie cookie1 = new HttpCookie("goods");
            cookie1.Values.Add("1", "电脑,3000");
            cookie1.Values.Add("2", "电脑1,3000");
            cookie1.Values.Add("3", "电脑2,3000");
            cookie1.Values.Add("4", "电脑3,3000");
            cookie1.Values.Add("5", "电脑4,3000");
            context.Response.SetCookie(cookie1);
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