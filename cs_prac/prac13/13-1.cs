using System;

namespace my_interface
{

    //インターフェースの定義
    #region 
        interface IConvertible
        {
            string ConvertToCSharp(string);
            string ConvertToVB2005(string);
        }
    #endregion

    //インターフェースを持つクラスの定義
    class ProgramHelper : IConvertible
    {   
        //インターフェースの実装
        string IConvertible.ConvertToCSharp(string str)
        {
            System.WriteLine("convert to cs.");
            return str;
        }
        string IConvertible.ConvertToVB2005(string str)
        {
            System.WriteLine("convert to vb");
            return str;
        }
    }
    class Test
    {

        public static void Run()
        {
            ProgramHelper ph = new ProgramHelper();

        }
        static void Main(){
            Test.Run();
        }
    }
}