using Newtonsoft.Json;
using SmsApiTest.core;
using SmsApiTest.model.Request;
using SmsApiTest.model.Response;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace SmsApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiurl = "http://api.feige.ee";
            int type = 1;
            switch (type)
            {
                case 1:
                    //普通短信
                    SendSms(apiurl);
                    break;
                case 2:
                    //个性短信
                    Gx(apiurl);
                    break;
                case 3:
                    //模板短信
                    Template(apiurl);
                    break;
                case 4:
                    //获取上行
                    Mo(apiurl);
                    break;
                case 5:
                    //获取报告
                    Report(apiurl);
                    break;
                case 6:
                    //查询余额
                    Balance(apiurl);
                    break;
                case 7:
                    //语音短信
                    Voice(apiurl);
                    break;
                case 8:
                    //国际短信
                    InterSms(apiurl);
                    break;
            }
            Console.ReadKey();
        }

        public static void SendSms(string apiurl)
        {
            CommonSmsRequest request = new CommonSmsRequest {
                Account= "账号",
                Pwd= "接口密码",//登录web平台 http://sms.feige.ee  在管理中心--基本资料--接口密码 或者首页 接口秘钥 如登录密码修改，接口密码会发生改变，请及时修改程序
                Content = "发送内容",
                Mobile = "手机号码",
                SignId= 11111, //登录web平台 http://sms.feige.ee  在签名管理中--新增签名--获取id
                SendTime=Convert.ToInt64(common.ToUnixStamp(Convert.ToDateTime("2016-10-19 23:24:30")))//定时短信 把时间转换成时间戳的格式
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            arge.AppendFormat("&Content={0}", request.Content);
            arge.AppendFormat("&Mobile={0}", request.Mobile);
            arge.AppendFormat("&SignId={0}", request.SignId);
            arge.AppendFormat("&SendTime={0}", request.SendTime);
            string weburl = apiurl+"/SmsService/Send";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("SendSms:" + resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                     //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }

        }

        public static void Gx(string apiurl)
        {
            GxSmsRequest request = new GxSmsRequest
            {
                Account = "账号",
                Pwd = "接口密码",
                Content = "发送内容",//格式： 周末团建#@#13800000000#@@#周一早会#@#13900000000
                SignId = 11111,//短信签名
                SendTime=Convert.ToInt64(common.ToUnixStamp(DateTime.Now.AddHours(1)))//定时短信 把时间转换成时间戳的格式
            };
 
            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            arge.AppendFormat("&Content={0}", request.Content);
            arge.AppendFormat("&SignId={0}", request.SignId);
            arge.AppendFormat("&SendTime={0}", request.SendTime);
            string weburl = apiurl + "/SmsService/Gx";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("Gx:"+resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }

        }

        public static void Template(string apiurl)
        {
            TemplateSmsRequest request = new TemplateSmsRequest
            {
                Account = "账号",
                Pwd = "接口密码",
                Content = "发送内容",
                Mobile = "手机号码",
                SignId = 1111,
                TemplateId= 1111,//模板id   //登录web平台 http://sms.feige.ee  在模板管理中--新增模板--获取id
                //SendTime=Convert.ToInt64(common.ToUnixStamp(DateTime.Now.AddHours(1)))//定时短信 把时间转换成时间戳的格式
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            arge.AppendFormat("&Content={0}", request.Content);
            arge.AppendFormat("&Mobile={0}", request.Mobile);
            arge.AppendFormat("&SignId={0}", request.SignId);
            arge.AppendFormat("&TemplateId={0}", request.TemplateId);
            //arge.AppendFormat("&SendTime={0}", request.SendTime);
            string weburl = apiurl + "/SmsService/Template";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("Template:" + resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }

        }

        public static void Mo(string apiurl)
        {
            MoRequest request = new MoRequest
            {
                Account = "账号",
                Pwd = "接口密码",
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            string weburl = apiurl + "/ReportMo/Mo";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("Mo:" + resp);
            try
            {
                MoResponse response = JsonConvert.DeserializeObject<MoResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }
        }

        public static void Report(string apiurl)
        {
            ReportRequest request = new ReportRequest
            {
                Account = "账号",
                Pwd = "接口密码",
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            string weburl = apiurl + "/ReportMo/Report";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("Report:" + resp);
            try
            {
                ReportResponse response = JsonConvert.DeserializeObject<ReportResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }
        }

        public static void Balance(string apiurl)
        {
            BalanceRequest request = new BalanceRequest
            {
                Account = "账号",
                Pwd = "接口密码",
            };

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            string weburl = apiurl + "/Account/Balance";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("Balance:" + resp);
            try
            {
                BalanceResponse response = JsonConvert.DeserializeObject<BalanceResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }
        }

        //国际短信代码
        public static void InterSms(string apiurl)
        {
            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}","账号");
            arge.AppendFormat("&Pwd={0}","接口密码");
            arge.AppendFormat("&Content={0}","模板变量");
            arge.AppendFormat("&Mobile={0}","手机号码");
            arge.AppendFormat("&CountryCode={0}", "接口国际代码");
            arge.AppendFormat("&SignId={0}","签名id");
            arge.AppendFormat("&TemplateId={0}","模板id");
            string weburl = apiurl + "/SmsService/InterSms";
            string resp = common.PushToWeb(weburl, arge.ToString(), Encoding.UTF8);
            Console.WriteLine("InterSms:" + resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }

        }

        //语音短信
        public static void Voice(string apiurl)
        {
            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", "账号");
            arge.AppendFormat("&Pwd={0}", "接口号码");
            arge.AppendFormat("&Content={0}", "123456");//4-8位数字
            arge.AppendFormat("&Mobile={0}", "手机号码");
            string weburl = apiurl + "/SmsService/Voice";
            string resp = common.PushToWeb(weburl,arge.ToString(), Encoding.UTF8);
            Console.WriteLine("Voice:" + resp);
            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                }
                else
                {
                    //失败
                }
            }
            catch (Exception ex)
            {
                //记录日志
            }
        }
    }

}
