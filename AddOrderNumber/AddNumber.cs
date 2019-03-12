
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOrderNumber
{
    public class AddNumber
    {

        public static string  AddTimestr(DateTime? str)
        {
            DateTime dtime = (DateTime)str;
            int nian = dtime.Year;
            int month = dtime.Month;
            int day = dtime.Day;
            int shi = dtime.Hour;
            int minute = dtime.Minute;
            int second = dtime.Second;

            string endtime = nian + "年" + month + "月" + day + "日" + shi + "时" + minute + "分" + second + "秒";

            return endtime;
            
        }

        public static string AddSeralNum(DateTime? str)
        {
            int index = 1;
           string indexstr= index.ToString().PadLeft(5, '0');  //一共4位,位数不够时从左边开始用0补
            DateTime dtime = (DateTime)str;
            StringBuilder sb = new StringBuilder();
            sb.Append(dtime.ToString("yyyy") + dtime.ToString("MM") + dtime.ToString("dd") + dtime.ToString("HH") + dtime.ToString("mm") + dtime.ToString("ss"));
            index++;
            return sb.ToString()+indexstr;

        }


    }
}
