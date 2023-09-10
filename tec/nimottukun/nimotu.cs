using System;

#define MAP_X 10
#define MAP_Y 10

namespace nimotu
{
    class Test
    {
        public static void Run(){

            while(1)
            {

                Map* map = Map.Instance();
                map.show();

                break;

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

    //各シグネチャの列挙、asciiコードで書いてある。文字はそれぞれのコメント
    enum Aspect
    {
        Wall = 3,   //'#'
        Player = 80,    //'P'
        Goal = 71,      //'G'
        Point = 79,     //'O'
        Obj = 46,       //'.'
        Space = 32,     //' '
    }

    //マップクラス
    class Map
    {

        //シングルトン　プライベートなマップの初期化
        private Map()
        {
            aspect = new Aspect[MAP_Y][MAP_X];

            for(int i = 0; i < MAP_Y; i++)
            {
                for(int j = 0; j < MAP_X; j++)
                {
                    aspect[i][j] = Aspect.Wall;
                }
            }
        }

        //シングルトン　自己のインスタンス
        private static void Map* _Instance = 0;

        private Aspect[MAP_X][MAP_Y] aspect;

        //シングルトン　インスタンスを返す関数
        public Map* Instance()
        {
            if(_Instance == 0)
            {
                _Instance = new Mapp();
            }

            return _Instance;
        }

        //マップの表示用
        public void show()
        {
            for(int i = 0; i < MAP_Y; i++)
            {
                for(int j = 0; j < MAP_X; j++)
                {
                    System.Console.Write((char)aspect[i][j]);
                }
                System.Console.Write("\n");
            }
        }
    }

}
