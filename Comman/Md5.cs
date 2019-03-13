using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Comman
{
  public  class Md5
    {
        public static string JiaMi(string str)
        {
            var input = Encoding.Default.GetBytes(str);
            MD5 md5 = MD5.Create("MD5");
            var output = md5.ComputeHash(input);
            string o = BitConverter.ToString(output);
            o = o.Replace("-", "");
            return o;
        }
    }
}
