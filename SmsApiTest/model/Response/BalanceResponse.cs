using System;
using System.Collections.Generic;
using System.Web;

namespace SmsApiTest.model.Response
{
    public class BalanceResponse:BaseCode
    {
        /// <summary>
        /// 账户余额
        /// </summary>
        public int  Balance { get; set; }
    }
}