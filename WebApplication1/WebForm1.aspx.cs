using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected HttpCookie cookie;
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpCookie cookies = new HttpCookie("user");
            cookies.Values.Add("1", "李白,123456,男,15698744563");
            cookies.Values.Add("2", "李白1,123456,男,15698744563");
            cookies.Values.Add("3", "李白2,123456,男,15698744563");
            cookies.Values.Add("4", "李白3,123456,男,15698744563");
            cookies.Values.Add("5", "李白4,123456,男,15698744563");
            cookies.Values.Add("6", "李白2,123456,男,15698744563");
            cookies.Values.Add("7", "李白3,123456,男,15698744563");
            cookies.Values.Add("8", "李白2,123456,男,15698744563");
            cookies.Values.Add("9", "李白3,123456,男,15698744563");
            cookies.Values.Add("10", "李白4,123456,男,15698744563");
            cookies.Values.Add("11", "李白4,123456,男,15698744563");

            Response.SetCookie(cookies);

            HttpCookie cookie1 = new HttpCookie("goods");
            cookie1.Values.Add("1", "电脑,3000");
            cookie1.Values.Add("2", "电脑1,3000");
            cookie1.Values.Add("3", "电脑2,3000");
            cookie1.Values.Add("4", "电脑3,3000");
            cookie1.Values.Add("5", "电脑4,3000");
            Response.SetCookie(cookie1);



            cookie = Request.Cookies["user"];
            for (int i = 0; i < cookie.Values.Count; i++)
            {
               // cookie.Values[i][i];
            }



            
            String str1 = "A,B,C,D";//正常字符串
            String[] arr1 = str1.Split( ',');
                //str1.split(",");
            Console.Write(arr1);
             
            Response.Write("cookie的长度："+ arr1);
        }
    }
}