using System;

namespace my_interface
{

    //各言語のコードとそのメタデータのベースクラス
    abstract public class Code
    {

        public enum Lang
        {
            CS = 1342,
            VB = 23147,
            OTHER = 24146
        }

        private string txt;    //記述されたコード
        private Lang syntax;    //コードが書かれた文法
        private Lang semantics; //コードを解釈する文法

        //fieldのプロパティ
        public string Txt
        {
            set
            {
                txt = value;
            }
            get
            {
                return txt;
            }
        }
        public Lang Syntax
        {
            set
            {
                syntax = value;
            }
            get
            {
                return syntax;
            }
        }
        public Lang Semantics
        {
            set
            {
                semantics = value;
            }
            get
            {
                return semantics;
            }
        }

        //言語毎のsyntax check
        public void CheckSyntax()
        {
            if(Syntax == Semantics)
            {
                Console.WriteLine("collect code.");
            }
            else
            {
                Console.WriteLine("syntax error\n\tsyntax\t\t: {0}\n\tsemantics\t: {1}", Syntax, Semantics);
            }
        }

        //ToStringのオーバーライド
        public override string ToString()
        {
            return "\n\tcode\t\t: " + Txt + "\n\tsyntax\t\t: " + Syntax + "\n\tsemantics\t: " + Semantics + "\n";
        }

    }

    class CSCode : Code
    {
        //コンストラクタ
        public CSCode(string str, Lang lang)
        {
            Txt = str;
            Syntax = lang;
            Semantics = Lang.CS;
        }
        public CSCode(string str)
        {
            Txt = str;
            Syntax = Lang.CS;
            Semantics = Lang.CS;
        }
        public CSCode(Code code)
        {
            Txt = code.Txt;
            Syntax = code.Syntax;
            Semantics = Lang.CS;
        }

    }

    class VBCode : Code
    {
        //コンストラクタ
        public VBCode(string str, Lang lang)
        {
            Txt = str;
            Syntax = lang;
            Semantics = Lang.VB;
        }
        public VBCode(string str)
        {
            Txt = str;
            Syntax = Lang.VB;
            Semantics = Lang.VB;
        }
        public VBCode(Code code)
        {
            Txt = code.Txt;
            Syntax = code.Syntax;
            Semantics = Lang.VB;
        }

    }

    //インターフェースの定義
    interface IConvertible
    {
        CSCode ConvertToCSharp(Code obj);
        VBCode ConvertToVB2005(Code obj);
    }

    //インターフェースを持つクラスの定義
    class ProgramHelper : IConvertible
    {   
        //インターフェースの実装
        #region
            public CSCode ConvertToCSharp(Code obj)
            {
                CSCode code = obj as CSCode;
                if(code != null)
                {
                    Console.WriteLine("this code is CS.");
                }
                else
                {
                    Console.WriteLine("convert to CS.");
                    code = new CSCode(obj);
                }
                return code;
            }
            public VBCode ConvertToVB2005(Code obj)
            {
                VBCode code = obj as VBCode;
                if(code != null)
                {
                    Console.WriteLine("this code is VB.");
                }
                else
                {
                    Console.WriteLine("convert to VB.");
                    code = new VBCode(obj);
                }
                return code;
            }
        #endregion

    }
    class Test
    {

        static void Main(){
            Test.Run();
        }

        public static void Run()
        {
            CSCode code1 = new CSCode("CS program1.");
            CSCode code2 = new CSCode("CS program2.");
            VBCode code3 = new VBCode("VB program3.");
            VBCode code4 = new VBCode("VB program4.");
            ProgramHelper ph = new ProgramHelper();

            //cs to vb
            Console.WriteLine("code1 : {0}", code1.ToString());
            code1.CheckSyntax();
            VBCode code21 = ph.ConvertToVB2005(code1);
            Console.WriteLine("code21 : {0}", code21.ToString());
            code21.CheckSyntax();

            LineFeed();

            //cs to cs
            Console.WriteLine("code2 : {0}", code2.ToString());
            code2.CheckSyntax();
            CSCode code22 = ph.ConvertToCSharp(code2);
            Console.WriteLine("code22 : {0}", code22.ToString());
            code22.CheckSyntax();

            LineFeed();

            //vb to cs
            Console.WriteLine("code3 : {0}", code3.ToString());
            code3.CheckSyntax();
            VBCode code23 = ph.ConvertToVB2005(code3);
            Console.WriteLine("code23 : {0}", code23.ToString());
            code23.CheckSyntax();

            LineFeed();

            //vb to vb
            Console.WriteLine("code4 : {0}", code4.ToString());
            code4.CheckSyntax();
            CSCode code24 = ph.ConvertToCSharp(code4);
            Console.WriteLine("code24 : {0}", code24.ToString());
            code24.CheckSyntax();

        }

        public static void LineFeed()
        {
            Console.WriteLine("\n\n\n");
        }
    }
}