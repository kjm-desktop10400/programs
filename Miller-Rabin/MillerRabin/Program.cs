using System;
using System.Numerics;

namespace MillerRabin
{

    class Executer
    {
        public static void Run()
        {

            for(int i = 0; i < 65; i++)
            {
                MulitDigitNumber n = new MulitDigitNumber(i);

                Console.WriteLine($"{i} : {n.byteLen()}");
            }

            byte[] bytes = new byte[3];
            bytes[0] = byte.Parse("255");
            bytes[1] = byte.Parse("0");
            bytes[2]= byte.Parse("255");
            MulitDigitNumber m = new MulitDigitNumber(bytes);
            Console.WriteLine($"m length : {m.byteLen()}");
            m.showBytes();


        }
        public static void Main() 
        {
            Executer.Run();
        }
    }

    class MulitDigitNumber
    {

        private int digit;
        private byte[]? bytes;

         #region constructor
       public MulitDigitNumber(int digit)
        {
            if(digit <= 0)
            {
                this.digit = 0;
                this.bytes = null;

                Console.WriteLine("Digit must lager than 0.");
            }
            else if(digit % 8 != 0)
            {
                this.digit = 0;
                this.bytes = null;

                Console.WriteLine("Digit must be multiple of octed");
            }
            else 
            {
                this.digit = digit;
                this.bytes = new byte[digit / 8];
            }

        }
        public MulitDigitNumber(int digit, byte[] bytes) : this(digit)
        {
            if(digit != bytes.Length)
            {
                this.digit = 0;
                this.bytes = null;
                Console.WriteLine("byte length doesn't match to digit.");
            }
            else
            {
                this.digit = digit;
                this.bytes = new byte[this.digit];
                for(int i = 0; i < digit; i++)
                {
                    this.bytes[i] = bytes[i];
                }
            }
        }
        public MulitDigitNumber(byte[] bytes)
        {
            this.digit = bytes.Length;
            this.bytes = new byte[this.digit];
            for (int i = 0; i < digit; i++)
            {
                this.bytes[i] = bytes[i];
            }
        }
        #endregion

        public int byteLen()
        {
            if(digit == 0)
            {
                return 0;
            }
            else
            {
                return bytes.Length;
            }            
        }

        public void showBytes()
        {
            Console.WriteLine($"bytes : {}");
        }


    }

}