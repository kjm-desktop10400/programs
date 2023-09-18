using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace visual_nimotu
{
    static class Test
    {

        [STAThread]
        public static void Run(){



            //シングルトンのインスタンス取得
            Map map = Map.Instance();

            //入力保存用バッファ
            char input;

            while(true)
            {

                map.show();

                if(map.check())
                {
                    System.Console.WriteLine("congratulations!!");
                    break;
                }

                input = char.Parse(System.Console.ReadLine());

                if(input == 'q')
                {
                    break;
                }
                
                if(input == 'r')
                {
                    map.reset();
                    continue;
                }

                map.Move(input);

            }

        }

        
    }

    class Nimotu_Form : Form
    {
        
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
            this.x = source.X;
            this.y = source.Y;
        }
        public Pos()
        {
            this.x = 0;
            this.y = 0;
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
        public static bool operator != (Pos lhs, Pos rhs)
        {
            if(lhs.x != rhs.x || lhs.y != rhs.y)
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

        //マップサイズ
        private int MAP_X = 10;
        private int MAP_Y = 10;

        //プレイヤーの座標
        Pos player = new Pos(7, 5);

        //ゴールの座標
        Pos Goal = new Pos(4, 5);

        //荷物の座標
        Pos obj = new Pos(6, 5);

        //char型で与えるマップ情報
        private char[,] state = 
        {
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', 'O', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', '.', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', 'O', ' ', '.', ' ', 'P', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
            {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        //シングルトン　プライベートなマップの初期化
        private Map()
        {
            aspect = new Aspect[MAP_Y, MAP_X];

            for(int i = 0; i < MAP_Y; i++)
            {
                for(int j = 0; j < MAP_X; j++)
                {
                    aspect[i, j] = CtoA(state[i, j]);
                }
            }
        }

        //シングルトン　自己のインスタンス
        private static Map _Instance = null;

        //列挙型でのマップ
        private Aspect[,] aspect;

        //シングルトン　インスタンスを返す関数
        public static Map Instance()
        {
            if(_Instance == null)
            {
                _Instance = new Map();
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
                    System.Console.Write(string.Format("{0} ", AtoC(aspect[i, j])));
                }
                System.Console.Write("\n");
            }
        }

        //文字と列挙の変換  
        public static Aspect CtoA(char input)
        {
            Aspect buf;

            switch(input)
            {
                case '#' : 
                    buf = Aspect.Wall;
                    break;
                case 'P' :
                    buf = Aspect.Player;
                    break;
                case 'O' :
                    buf = Aspect.Point;
                    break;
                case 'G' :
                    buf = Aspect.Goal;
                    break;
                case '.' :
                    buf = Aspect.Obj;
                    break;
                case ' ' :
                    buf = Aspect.Space;
                    break;

                default :
                    buf = Aspect.Wall;
                    break;
            }
            return buf;
        }
        public static char AtoC(Aspect input)
        {
            char buf;

            switch(input)
            {
                case Aspect.Wall : 
                    buf = '#';
                    break;
                case Aspect.Player :
                    buf = 'P';
                    break;
                case Aspect.Point :
                    buf = 'O';
                    break;
                case Aspect.Goal :
                    buf = 'G';
                    break;
                case Aspect.Obj :
                    buf = '.';
                    break;
                case Aspect.Space :
                    buf = ' ';
                    break;

                default :
                    buf = '#';
                    break;
            }
            return buf;
        }

        //移動用関数
        public void Move(char input)
        {
            Pos target = new Pos(player);

            //入力が適切であるかのチェックと移動先の計算
            bool invalid_input = false;
            switch(input)
            {
                case 'w' :
                    target.Y -= 1;
                    break;
                case 'a' :
                    target.X -= 1;
                    break;
                case 's' :
                    target.Y += 1;
                    break;
                case 'd' :
                    target.X += 1;
                    break;

                default :
                    invalid_input = false;
                    break;
            }
            if(invalid_input)
            {
                System.Console.WriteLine("input is invalid.");
                return;
            }

            //移動先が壁かゴールならば何もしない
            if(aspect[target.Y, target.X] == Aspect.Wall || aspect[target.Y, target.X] == Aspect.Goal)
            {
                System.Console.WriteLine("cant move that direction.");
                return;
            }

            //移動先に何もなければ位置を更新、移動前の座標に空白を挿入
            if(aspect[target.Y, target.X] == Aspect.Space)
            {
                aspect[player.Y, player.X] = Aspect.Space;
                player.copy(target);
                aspect[player.Y, player.X] = Aspect.Player;
                return;
            }

            //移動先が荷物である時の処理
            if(aspect[target.Y, target.X] == Aspect.Obj)
            {

                //荷物の座標をコピー、このメソッド内ではtargetが荷物の移動先
                Pos nimotu = new Pos();
                nimotu.copy(target);

                //荷物の移動先を計算
                switch(input)
                {
                    case 'w' :
                        target.Y -= 1;
                        break;
                    case 'a' :
                        target.X -= 1;
                        break;
                    case 's' :
                        target.Y += 1;
                        break;
                    case 'd' :
                        target.X += 1;
                        break;
                }

                //荷物の移動先に何もない場合
                if(aspect[target.Y, target.X] == Aspect.Space)
                {
                    aspect[target.Y, target.X] = Aspect.Obj;
                    aspect[nimotu.Y, nimotu.X] = Aspect.Player;
                    aspect[player.Y, player.X] = Aspect.Space;

                    player.copy(nimotu);
                    
                    return;
                }

                //荷物の移動先が荷物の場合
                if(aspect[target.Y, target.X] == Aspect.Obj)
                {
                    System.Console.WriteLine("cant move that direction.");
                    return;
                }

                //荷物の移動先が壁の場合
                if(aspect[target.Y, target.X] == Aspect.Wall)
                {
                    System.Console.WriteLine("cant move that direction.");
                    return;
                }

                //荷物の移動先がゴールの場合
                if(aspect[target.Y, target.X] == Aspect.Point)
                {
                    aspect[target.Y, target.X] = Aspect.Goal;
                    aspect[nimotu.Y, nimotu.X] = Aspect.Player;
                    aspect[player.Y, player.X] = Aspect.Space;

                    player.copy(nimotu);
                    return;
                }

            }

        }

        //初期位置に戻す
        public void reset()
        {
            aspect = new Aspect[MAP_Y, MAP_X];

            for(int i = 0; i < MAP_Y; i++)
            {
                for(int j = 0; j < MAP_X; j++)
                {
                    aspect[i, j] = CtoA(state[i, j]);
                }
            }

            player.X = 7;
            player.Y = 5;
        }

        //終了判定。ゴールが全てなくなっていればtrueを返す
        public bool check()
        {
            for(int i = 0; i < MAP_Y; i++)
            {
                for(int j = 0; j < MAP_X; j++)
                {
                    if(aspect[i, j] == Aspect.Point)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }

}
