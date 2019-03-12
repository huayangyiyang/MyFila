using System;
using System.Collections.Generic;
using System.Web;

namespace SmsApiTest.model.Request
{
    public class TemplateSmsRequest:BaseSmsRequest
    {
        /// <summary>
        /// 模板Id
        /// </summary>
        public long TemplateId { get; set; }
    }
}