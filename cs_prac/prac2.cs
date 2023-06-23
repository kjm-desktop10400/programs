using System;

namespace prac2
{

    public class OverLoad
    {
        public static void Tt(int x, out int twice, out int triple)
        {
            twice = 2 * x;
            triple = 3 * x;
        }
    }

    public class Test
    {

        public void Run()
        {
            int twice, triple;
            OverLoad.Tt(6, out twice, out triple);
            Console.WriteLine("6 * 2 = {0}\n6 * 3 = {1}", twice, triple);
        }

        static void Main()
        {
            Test t = new Test();
            t.Run();
        } 

    }

}