using System;

namespace ClassTest
{
    
    public class Math
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }
        public static int Subtract(int x, int y)
        {
            return x - y;
        }
        public static int Multiply(int x, int y)
        {
            return x * y;
        }
        public static double Divide(int x, int y)
        {
            return (double)x / y;
        }
    }


    class Test
    {

        public void Run()
        {
            Console.WriteLine("2 + 3 = {0}", Math.Add(2, 3));
            Console.WriteLine("2 - 3 = {0}", Math.Subtract(2, 3));
            Console.WriteLine("2 * 3 = {0}", Math.Multiply(2, 3));
            Console.WriteLine("2 / 3 = {0}", Math.Divide(2, 3));
        }

        static void Main()
        {

            Test testObj = new Test();
            testObj.Run();

        }
    }

}