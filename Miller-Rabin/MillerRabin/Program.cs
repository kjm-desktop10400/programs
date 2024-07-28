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

            BigInt.Sanitizer("0".ToCharArray(), ref bi1);
            BigInt.Sanitizer("999999".ToCharArray(), ref bi2);


            Console.WriteLine("lhs   : " + bi1.ToString());
            Console.WriteLine("rhs   : " + bi2.ToString());
            Console.WriteLine("sub   : " + (bi1 - bi2).ToString());

        }
    }
}
