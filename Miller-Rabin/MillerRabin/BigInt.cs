using System;
using System.Reflection.Metadata;

namespace MillerRabin
{
    class BigInt
    {

        private byte[] digits;          //intrinsick number
        private bool sign;              //sign of number (true is +, false is -)


        public static bool Sanitizer(char[] digits, ref BigInt obj)
        {//To create BigInt obect, use this fuction. Sanitizer(char[] digits, ref BigInt obj). digits : a number you want to create in char array. obj : If this function could create BigInt, return obj as createrd. This function returns boolian could create obj or not.

            bool sign = true;
            bool initWithNoSign = true;
            bool initWithZero = false;

            for (int i = 0; i < digits.Length; i++)
            {

                switch (digits[i])
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
                        if (i != 0)
                        {
                            return false;
                        }
                        sign = false;
                        initWithNoSign = false;
                        break;

                    case '0':
                        if (i == 0 || (i == 1 && initWithNoSign == false))
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

            if (digits.Length == diff)
            {
                obj = new BigInt("0".ToCharArray(), true);
                return true;
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

            switch (obj)
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


        private static BigInt Add(BigInt lhs, BigInt rhs)
        {

            byte[] buf;

            BigInt larger;
            BigInt smaller;

            bool resultSign = true;

            if((lhs.sign == true) && (rhs.sign == false))
            {
                return BigInt.Sub(lhs, rhs);
            }
            else if((lhs.sign == false) && (rhs.sign == true))
            {
                return BigInt.Sub(rhs, lhs);
            }
            else if((lhs.sign == false)&&(rhs.sign == false))
            {
                resultSign = false;
            }

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

            for (int i = 0; i < larger.digits.Length; i++)
            {

                if (i < smaller.digits.Length)
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
            for (int i = 0; i < buf.Length; i++)
            {
                if (carry == true)
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


            return new BigInt(rev, resultSign);
        }

        private static BigInt Sub(BigInt lhs, BigInt rhs)
        {

            int[] buf;

            BigInt larger;
            BigInt smaller;

            int carry = 0;

            if (lhs > rhs)
            {
                larger = new BigInt(lhs.digits, lhs.sign);
                smaller = new BigInt(rhs.digits, rhs.sign);
            }
            else
            {
                larger = new BigInt(rhs.digits, rhs.sign);
                smaller = new BigInt(lhs.digits, lhs.sign);
            }

            buf = new int[larger.digits.Length];

            for(int i = 0; i < larger.digits.Length; i++)
            {

                if(i < smaller.digits.Length)
                {
                    buf[i] = larger.digits[larger.digits.Length - 1 - i] - smaller.digits[smaller.digits.Length - 1 - i];
                }
                else
                {
                    buf[i] = larger.digits[larger.digits.Length - 1 - i];
                }

            }

            for (int i = 0; i < larger.digits.Length; i++)
            {
                if (buf[i] + carry < 0)
                {
                    buf[i] += 10;
                    carry = -1;
                }
                else
                {
                    carry = 0;
                }
            }

            int count = 0;
            for (int i = 0; i < buf.Length; i++)
            {
                if (buf[i] == 0)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
            }

            byte[] result = new byte[buf.Length - count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)buf[buf.Length - 1 - i - count];
            }

            if (lhs > rhs)
            {
                return new BigInt(result, true);
            }
            else
            { 
                return new BigInt(result, false);
            }

        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is BigInt))
            {
                return false;
            }

            if (this.digits.Length != ((BigInt)obj).digits.Length)
            {
                return false;
            }

            for (int i = 0; i < this.digits.Length; i++)
            {
                if (this.digits[i] != ((BigInt)obj).digits[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool LargerThan(BigInt lhs, BigInt rhs)
        {
            if (lhs == rhs) return false;

            if (lhs.digits.Length > rhs.digits.Length)
            {
                return true;
            }
            else
            {

                for (int i = 0; i < lhs.digits.Length; i++)
                {
                    if (lhs.digits[i] < rhs.digits[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override string ToString()
        {
            byte[] buf = new byte[digits.Length];
            string output = "";

            if (sign == false)
            {
                output += "-";
            }

            for (int i = 0; i < digits.Length; i++)
            {
                output += digits[i].ToString();
            }

            return output;

        }
        public static BigInt operator +(BigInt lhs, BigInt rhs)
        {
            return BigInt.Add(lhs, rhs);
        }
        public static BigInt operator -(BigInt lhs, BigInt rhs)
        {
            return BigInt.Sub(lhs, rhs);
        }
        public static bool operator ==(BigInt lhs, BigInt rhs)
        {
            if (lhs.Equals(rhs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(BigInt lhs, BigInt rhs)
        {
            if (lhs.Equals(rhs))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool operator >(BigInt lhs, BigInt rhs)
        {
            if (BigInt.LargerThan(lhs, rhs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <(BigInt lhs, BigInt rhs)
        {
            if (!(lhs > rhs) && (lhs != rhs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator >=(BigInt lhs, BigInt rhs)
        {
            if((lhs > rhs) || (lhs == rhs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <=(BigInt lhs, BigInt rhs)
        {
            if ((lhs < rhs) || (lhs == rhs))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}