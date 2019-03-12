using System;
using System.Collections.Generic;
using System.Web;

namespace SmsApiTest.model.Response
{
    public class MoResponse:BaseCode
    {
        /// <summary>
        /// 上行报告集合
        /// </summary>
        public List<Mo> MoList { get; set; }
    }

    public class Mo
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 短信接收时间 时间戳
        /// </summary>
        public long Times { get; set; }
        /// <summary>
        /// 扩展码
        /// </summary>
        public string  ExtNo { get; set; }
    }
}