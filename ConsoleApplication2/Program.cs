using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 8;
           
                Console.WriteLine( number.ToString().PadLeft(5, '0'));  //一共4位,位数不够时从左边开始用0补


            string str = "/Product/List";
            int index = str.LastIndexOf('/');
            Console.WriteLine(str.LastIndexOf('/')+"，str的长度是："+str.Length);
            Console.WriteLine(str.Substring(1, index-1));
            Console.WriteLine(str.Substring(index + 1));
            int index2 = str.LastIndexOf(".");
            Console.WriteLine(index2);

        }
    }
}
