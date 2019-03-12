using System;
using System.Collections.Generic;
using System.Web;

namespace SmsApiTest.model.Response
{
    public class ReportResponse:BaseCode
    {
        public Report Report { get; set; }
       
    }
    public class Report
    {
        /// <summary>
        /// 发送ID
        /// </summary>
        public string SendID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public long Times { get; set; }
        /// <summary>
        /// 接收号码
        /// </summary>
        public string Mobile { get; set; }

    }
}