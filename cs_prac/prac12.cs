using System;

namespace prac12
{

    class Invoice
    {
        private string vendor;
        private double amount;

        public Invoice(string str, double num)
        {
            vendor = str;
            amount = num;
        }

        public static Invoice operator+ (Invoice first, Invoice second)
        {
            if(first.vendor == second.vendor)
            {
                return new Invoice(first.vendor, first.amount + second.amount);
            }
            else
            {
                return new Invoice("\0", 0);
            }
        } 

        public override bool Equals(object obj)
        {

            if( obj == null || this.GetType() != obj.GetType() )
            {
                return false;
            }
            else
            {
                Invoice copy = (Invoice)obj;
                return (this.vendor == copy.vendor) && (this.amount == copy.amount);
            }
            
        }
        public override int GetHashCode()
        {
            return this.vendor.GetHashCode() ^ this.amount.GetHashCode();
        }        

        public override string ToString()
        {
            return "vendor : " + vendor.ToString() + "\tamount : " + amount.ToString();
        }
    }

    class Test
    {
        public static void Run()
        {

            Invoice firstInv = new Invoice("a.inc", 1000);
            Invoice secondInv = new Invoice("a.inc", 1000);
            Invoice thiredInv = new Invoice("b.inc", 200);
            Invoice fourthInv = new Invoice("c.inc", 500);

            Console.WriteLine("f + s : {0}", firstInv + secondInv);
            Console.WriteLine("s + t : {0}", secondInv + thiredInv);
            Console.WriteLine("t + f : {0}", thiredInv + fourthInv);

            if( firstInv.Equals(secondInv) )
            {
                Console.WriteLine("f is s");
            }
            else 
            {
                Console.WriteLine("f is not s");
            }

        }

        public static void Main()
        {
            Test.Run();
        }
    }
}