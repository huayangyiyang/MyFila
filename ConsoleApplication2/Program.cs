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
            


        }
    }
}
