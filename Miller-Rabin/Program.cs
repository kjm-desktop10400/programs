using MillerRabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miller_Rabin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program.Run();
        }
        public static void Run()
        {

            BigInt bi1 = null;
            BigInt bi2 = null;

            BigInt.Sanitizer("100".ToCharArray(), ref bi1);
            BigInt.Sanitizer("0".ToCharArray(), ref bi2);


            Console.WriteLine("first  : " + bi1.ToString());
            Console.WriteLine("second : " + bi2.ToString());
            Console.WriteLine("sum    : " + (bi1 + bi2).ToString());

        }
    }
}
