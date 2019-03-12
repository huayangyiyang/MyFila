﻿using System;
using System.Collections.Generic;
using System.Web;

namespace SmsApiTest.model.Request
{
    public class BalanceRequest
    {
        /// <summary>
        /// 账号信息
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}