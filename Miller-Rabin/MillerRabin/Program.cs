using System;
using System.Numerics;

namespace MillerRabin
{

    class Executer
    {
        public static void Run()
        {



        }
        public static void Main() 
        {
            Executer.Run();
        }
    }

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

    class MulitDigitNumber
    {
        private ushort[] num;             //multi bit number (little endian)
        private int digit;

        #region constructor

        public MulitDigitNumber(ushort[] number, bool isBigEndian)      //create multi digit number as big endian
        {

            if(isBigEndian)     //if number is big endian, convert to little endian.
            {
                convertEndian(number);
            }

            num = new ushort[number.Length];            //make deep copy
            for (int i = 0; i < number.Length; i++)
            {
                num[i] = number[i];
            }



        }


        #endregion



        public static void convertEndian(ushort[] obj)      //convert little endian and big endian
        {
            ushort[] tmp = new ushort[obj.Length];

            for(int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = obj[i];
            }
            for(int i = 0; i <tmp.Length; i++)
            {
                obj[i] = tmp[tmp.Length - i];
            }
        }


    }

}