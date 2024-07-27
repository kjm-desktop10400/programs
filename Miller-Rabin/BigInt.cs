using System;

namespace MillerRabin
{
    public class UndefinedArray
    {
        private static UndefinedArray? _Instance = null;
        private static ushort[]? arrayRef;

        public ushort[] ARRAYREF
        {
            get { return arrayRef; }
        }

        private UndefinedArray()
        {

        }

        public static UndefinedArray Instance()
        {
            if(_Instance == null)
            {
                _Instance = new UndefinedArray();
                arrayRef = new ushort[0];
            }
            
            return _Instance;
        }


    }

    class BigInt
    {

        private byte[] digits;          //intrinsick number
        private bool sign;              //sign of number (true is +, false is -)

        public static bool Sanitizer(char[] digits, ref BigInt obj)
        {

            bool sign = true;
            bool initWithNoSign = true;
            bool initWithZero = false;

            for(int i = 0; i < digits.Length; i++)
            {

                switch(digits[i])
                {
                    case '+':
                        if (i != 0)
                        {
                            return false;
                        }
                        sign = true;
                        initWithNoSign = false;
                        break;
                    case '-':
                        if(i != 0)
                        {
                            return false;
                        }
                        sign = false;
                        initWithNoSign = false;
                        break;

                    case '0':
                        if(i == 0 || (i == 1 && initWithNoSign == false))
                        {
                            initWithZero = true;
                        }
                        break;
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;

                    default:
                        return false;

                }

            }

            int diff = 0;
            if (initWithNoSign == false)
            {
                diff++;
            }
            else
            {
                sign = true;
            }
            if (initWithZero)
            {
                diff++;
            }

            char[] fixedDigits = new char[digits.Length - diff];


            for (int i = 0; i < fixedDigits.Length; i++)
            {
                fixedDigits[i] = digits[i + diff];
            }

            obj = new BigInt(fixedDigits, sign);


            return true;

        }

        private static byte CharToByte(char obj)
        {

            byte buf;

            switch(obj)
            {
                case '0':
                    buf = 0;
                    break;
                case '1':
                    buf = 1;
                    break;
                case '2':
                    buf = 2;
                    break;
                case '3':
                    buf = 3;
                    break;
                case '4':
                    buf = 4;
                    break;
                case '5':
                    buf = 5;
                    break;
                case '6':
                    buf = 6;
                    break;
                case '7':
                    buf = 7;
                    break;
                case '8':
                    buf = 8;
                    break;
                case '9':
                    buf = 9;
                    break;
                default:
                    buf = 10;
                    break;
            }

            return buf;
            
        }

        private BigInt(char[] digits, bool sign)
        {
            string buf;
            this.digits = new byte[digits.Length];
            for (int i = 0; i < digits.Length; i++)
            {
                this.digits[i] = CharToByte(digits[i]);
            }
            this.sign = sign;
        }
        private BigInt(byte[] digits, bool sign)
        {
            this.digits = digits;
            this.sign = sign;
        }


        private static BigInt add(BigInt lhs, BigInt rhs)
        {

            byte[] buf;

            BigInt larger;
            BigInt smaller;

            if (lhs.digits.Length > rhs.digits.Length)
            {
                larger = new BigInt(lhs.digits, lhs.sign);
                smaller = new BigInt(rhs.digits, rhs.sign); 
            }
            else
            {
                larger = new BigInt(rhs.digits, rhs.sign);
                smaller = new BigInt(lhs.digits, lhs.sign);
            }

            buf = new byte[larger.digits.Length];

            for(int i = 0; i < larger.digits.Length; i++)
            {

                if(i < smaller.digits.Length)
                {
                    buf[i] = (byte)(larger.digits[larger.digits.Length - 1 - i] + smaller.digits[smaller.digits.Length - 1 - i]);
                }
                else
                {
                    buf[i] = larger.digits[larger.digits.Length - 1 - i];
                }

            }

            byte[] result = new byte[larger.digits.Length + 1];
            bool carry = false;
            for(int i = 0; i < buf.Length; i++)
            {
                if(carry == true)
                {
                    buf[i] += 1;
                }

                if (buf[i] >= 10)
                {
                    carry = true;
                    result[i] = (byte)(buf[i] - 10);
                }
                else
                {
                    carry = false;
                    result[i] = buf[i];
                }
            }

            byte[] rev;
            if (result[result.Length - 1] == 0)
            {
                rev = new byte[result.Length - 1];
            }
            else
            {
                rev = new byte[result.Length];
            }

            for (int i = 0; i < rev.Length; i++)
            {
                rev[rev.Length - 1 - i] = result[i];
            }


            return new BigInt(rev, true);
        }
        public static BigInt operator +(BigInt lhs , BigInt rhs)
        {
            return BigInt.add(lhs, rhs);
        }

        public override string ToString()
        {
            byte[] buf = new byte[digits.Length];
            string output = "";
            
            for(int i =0; i<digits.Length; i++)
            {
                output += digits[i].ToString();
            }

            return output;

        }


    }
}