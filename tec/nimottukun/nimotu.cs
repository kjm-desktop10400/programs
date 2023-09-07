using System;

namespace nimotu
{
    class Test
    {
        public static void Run(){

            while(1)
            {
                Map.show();


            }

        }

        public static void Main()
        {
            Test.Run();
        }
    }

    class Pos       //座標クラス
    {
        private int x;
        private int y;

        //コンストラクタ
        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Pos(Pos source)
        {
            
        }
        
        //プロパティ
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        //等価演算子のオーバーロード
        public static bool operator == (Pos lhs, Pos rhs)
        {
            if(lhs.x == rhs.x && lhs.y == rhs.y)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        public override bool Equals(object o)
        {
            if(!(o is Pos))
            {
                return false;
            }
            return this == (Pos)o;
        } 
        
        //コピーの作成
        public void copy(Pos o)
        {
            this.x = o.x;
            this.y = o.y;
        }

        //toStringのオーバーライド
        public override string ToString()
        {
            return string.Format("({0}, {1})", this.x, this.y);
        }
    }

    //各シグネチャの列挙
    enum Aspect
    {
        Wall = 0,
        Player = 1,
        Goal = 2,
        Point = 4,
        Obj = 5,
        Space = 6,
    }

    //マップクラス
    class Map
    {
        private Pos pos;
        private Aspect sapect;
    }

}
