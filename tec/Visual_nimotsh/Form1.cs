using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using static Visual_nimotsh.Exclusive_control;

namespace Visual_nimotsh
{
    public partial class Form1 : Form
    {
        //リソースの読み込み用の変数
        readonly Bitmap Wall_Image;
        readonly Bitmap Player_Image;
        readonly Bitmap Goal_Image;
        readonly Bitmap Point_Image;
        readonly Bitmap Obj_Image;

        private Map map;                //マップのインスタンス
        private Label dispray_fps;      //fpsインジケーターのラベルコントロール

        private int MaxFPS = 60;        //最大フレームレート

        private ProcessKeyPush KeyPush = new ProcessKeyPush();

        Thread thread;

        Abort abort;
        Exclusive_control exc;
        End_game end_game;
        bool is_drawing = false;
        private bool goal = false;

        public Form1()
        {
            InitializeComponent();

            //インスタンスの代入
            map = Map.Instance();
            abort = Abort.Instance();
            exc = Exclusive_control.Instance();
            end_game = End_game.Instacne();
           
            //クライアント領域の設定
            this.ClientSize = new Size((map.MAP_X + 2) * 50, (map.MAP_Y + 2) * 50);

            //KeyDownイベントハンドラ
            this.KeyDown += new KeyEventHandler(regist_Process_Key_Pushed_members);

            //スレッド終了用のデリゲートの登録
            abort.Abort_Thread += new EventHandler(End_Draw);

            //排他制御用デリゲートの登録
            exc.Is_Drawing += new ExclusiveControlEventHandller(Enable_Is_Drawing);
            exc.Is_Waiting += new ExclusiveControlEventHandller(Desable_Is_Drawing);

            end_game.Quite_Game += new EventHandler(close_game);


            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true
                );


        }

        //描画用のイベントハンドラ
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(KeyPush.OFFSCREEN, 50, 50);

        }

        //KeyDownイベントハンドラ。ProcessKeyPushのメンバを渡し、KeyPushedを呼び出す
        private void regist_Process_Key_Pushed_members(object sender, KeyEventArgs e)
        {
            //is_drawingフラグがfalseの時のみ描画用スレッドを起動
            if (is_drawing != true)
            {

                exc.drawing();

                //描画用のスレッドを分ける
                thread = new Thread(new ThreadStart(KeyPush.Start_Draw));

                this.KeyPush.SENDER = sender;
                this.KeyPush.E = e;
                this.KeyPush.THREAD = thread;
                this.KeyPush.CONTLOR = this;

                this.KeyPush.KeyPushed();

            }
            if(is_drawing == false)
            {

            }
        }

        //スレッド終了用イベントハンドラ
        public void End_Draw(object sender, EventArgs e)
        {
            exc.waiting();
            if (map.check() == true)
            {
                end_game.publish();
            }

            thread.Abort();

        }

        //排他制御用イベントハンドラ
        public void Enable_Is_Drawing(object sender, EventArgs e)
        {
            is_drawing = true;
        }
        public void Desable_Is_Drawing(object sender, EventArgs e)
        {
            is_drawing = false;
        }

        //クロススレッド回避用デリゲート
        public delegate void quite();

        public void finish_game()
        {
            this.Close();
        }

        public void close_game(object sender, EventArgs e)
        {


            //クロススレッドを回避するにはinvokeが必要
            this.Invoke(new quite(finish_game));
        }

    }


    //終了判定用クラス、シングルトンでアプリの終了をさせる
    public class End_game
    {

        private End_game() 
        {
            
        }

        private static End_game _Instance = null;

        public static End_game Instacne()
        {
            if(_Instance == null)
            {
                _Instance = new End_game();
            }
            return _Instance;
        }

        public event EventHandler Quite_Game;

        public void publish()
        {
            if(Quite_Game != null)
            {
                Quite_Game(this, EventArgs.Empty);
            }
        }

    }
    

    //スレッド終了用クラス、シングルトン
    public class Abort
    {

        private Abort()
        {

        }

        private static Abort _Instance = null;
        public static Abort Instance()
        {
            if(_Instance == null)
            {
                _Instance = new Abort();
            }
            return _Instance;
        }

        //描画用スレッド"Start_Draw"終了用イベント
        public event EventHandler Abort_Thread;

        //イベント発生用メソッド
        public void publish()
        {
            if(Abort_Thread != null)
            {
                Abort_Thread(this, EventArgs.Empty);
            }
        }


    }

    //スレッドの排他制御用クラス、シングルトン
    public class Exclusive_control        
    {

        public delegate void ExclusiveControlEventHandller(object sender, EventArgs e);
        private Exclusive_control()
        {

        }

        private static Exclusive_control  _Instance = null;

        public static Exclusive_control Instance()
        {
            if(_Instance == null)
            {
                _Instance = new Exclusive_control();
            }
            return _Instance;
        }

        public event ExclusiveControlEventHandller Is_Drawing;
        public event ExclusiveControlEventHandller Is_Waiting;

        public void drawing()
        {
            if(Is_Drawing != null)
            {
                Is_Drawing(this, EventArgs.Empty);
            }
        }
        public void waiting()
        {
            if(Is_Waiting !=null)
            {
                Is_Waiting(this, EventArgs.Empty);
            }
        }
            
    }

    public class Pos       //座標クラス
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
        public static bool operator ==(Pos lhs, Pos rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Pos lhs, Pos rhs)
        {
            if (lhs.x != rhs.x || lhs.y != rhs.y)
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
            if (!(o is Pos))
            {
                return false;
            }
            return this == (Pos)o;
        }
        public static Pos operator +(Pos lhs, Pos rhs)
        {
            return new Pos(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }
        public static Pos operator *(Pos lhs, Pos rhs)
        {
            return new Pos(lhs.X * rhs.X, lhs.Y * rhs.Y);
        }
        public static Pos operator *(Pos lhs, int scaler)
        {
            return new Pos(lhs.X * scaler, lhs.Y * scaler);
        }
        public static Pos operator *(int scaler, Pos rhs)
        {
            return new Pos(scaler * rhs.X, scaler * rhs.Y);
        }


        //コピーの作成
        public object Clone()
        {
            return new Pos(X, Y);
        }
        public void copy(Pos obj)
        {
            this.X = obj.X;
            this.Y = obj.Y;
        }

        //toStringのオーバーライド
        public override string ToString()
        {
            return string.Format("({0}, {1})", this.x, this.y);
        }
    }

    //各シグネチャの列挙、asciiコードで書いてある。文字はそれぞれのコメント
    public enum Aspect
    {
        Wall = 3,   //'#'
        Player = 80,    //'P'
        Goal = 71,      //'G'
        Point = 79,     //'O'
        Obj = 46,       //'.'
        Space = 32,     //' '
    }

    //マップクラス
    public class Map
    {

        //マップサイズ
        private int MAP_x = 10;
        private int MAP_y = 10;

        //マップサイズを返すプロパティ
        public int MAP_X
        {
            get { return MAP_x; }
        }
        public int MAP_Y
        {
            get { return MAP_y; }
        }


        //プレイヤーの座標とゲッタ
        Pos player = new Pos(7, 5);
        public Pos PLAYER
        {
            get
            {
                return player;
            }
        }

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

            for (int i = 0; i < MAP_Y; i++)
            {
                for (int j = 0; j < MAP_X; j++)
                {
                    aspect[i, j] = CtoA(state[i, j]);
                }
            }
        }

        //シングルトン　自己のインスタンス
        private static Map _Instance = null;

        //列挙型でのマップ
        private Aspect[,] aspect;

        //描画用。座標のAspectを返す
        public Aspect GetAspect(int x, int y)
        {
            return aspect[y, x];
        }
        public Aspect GetAspect(Pos pos)
        {
            return aspect[pos.Y, pos.X];
        }
        

        //シングルトン　インスタンスを返す関数
        public static Map Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Map();
            }

            return _Instance;
        }


        //文字と列挙の変換  
        public static Aspect CtoA(char input)
        {
            Aspect buf;

            switch (input)
            {
                case '#':
                    buf = Aspect.Wall;
                    break;
                case 'P':
                    buf = Aspect.Player;
                    break;
                case 'O':
                    buf = Aspect.Point;
                    break;
                case 'G':
                    buf = Aspect.Goal;
                    break;
                case '.':
                    buf = Aspect.Obj;
                    break;
                case ' ':
                    buf = Aspect.Space;
                    break;

                default:
                    buf = Aspect.Wall;
                    break;
            }
            return buf;
        }
        public static char AtoC(Aspect input)
        {
            char buf;

            switch (input)
            {
                case Aspect.Wall:
                    buf = '#';
                    break;
                case Aspect.Player:
                    buf = 'P';
                    break;
                case Aspect.Point:
                    buf = 'O';
                    break;
                case Aspect.Goal:
                    buf = 'G';
                    break;
                case Aspect.Obj:
                    buf = '.';
                    break;
                case Aspect.Space:
                    buf = ' ';
                    break;

                default:
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
            switch (input)
            {
                case 'w':
                    target.Y -= 1;
                    break;
                case 'a':
                    target.X -= 1;
                    break;
                case 's':
                    target.Y += 1;
                    break;
                case 'd':
                    target.X += 1;
                    break;

                default:
                    invalid_input = false;
                    break;
            }
            if (invalid_input)
            {
                System.Console.WriteLine("input is invalid.");
                return;
            }

            //移動先が壁かゴールならば何もしない
            if (aspect[target.Y, target.X] == Aspect.Wall || aspect[target.Y, target.X] == Aspect.Goal)
            {
                System.Console.WriteLine("cant move that direction.");
                return;
            }

            //移動先に何もなければ位置を更新、移動前の座標に空白を挿入
            if (aspect[target.Y, target.X] == Aspect.Space)
            {
                aspect[player.Y, player.X] = Aspect.Space;
                player.copy(target);
                aspect[player.Y, player.X] = Aspect.Player;
                return;
            }

            //移動先が荷物である時の処理
            if (aspect[target.Y, target.X] == Aspect.Obj)
            {

                //荷物の座標をコピー、このメソッド内ではtargetが荷物の移動先
                Pos nimotu = new Pos();
                nimotu.copy(target);

                //荷物の移動先を計算
                switch (input)
                {
                    case 'w':
                        target.Y -= 1;
                        break;
                    case 'a':
                        target.X -= 1;
                        break;
                    case 's':
                        target.Y += 1;
                        break;
                    case 'd':
                        target.X += 1;
                        break;
                }

                //荷物の移動先に何もない場合
                if (aspect[target.Y, target.X] == Aspect.Space)
                {
                    aspect[target.Y, target.X] = Aspect.Obj;
                    aspect[nimotu.Y, nimotu.X] = Aspect.Player;
                    aspect[player.Y, player.X] = Aspect.Space;

                    player.copy(nimotu);

                    return;
                }

                //荷物の移動先が荷物の場合
                if (aspect[target.Y, target.X] == Aspect.Obj)
                {
                    System.Console.WriteLine("cant move that direction.");
                    return;
                }

                //荷物の移動先が壁の場合
                if (aspect[target.Y, target.X] == Aspect.Wall)
                {
                    System.Console.WriteLine("cant move that direction.");
                    return;
                }

                //荷物の移動先がゴールの場合
                if (aspect[target.Y, target.X] == Aspect.Point)
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

            for (int i = 0; i < MAP_Y; i++)
            {
                for (int j = 0; j < MAP_X; j++)
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
            for (int i = 0; i < MAP_Y; i++)
            {
                for (int j = 0; j < MAP_X; j++)
                {
                    if (aspect[i, j] == Aspect.Point)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }


    //KeyPushイベント処理するクラス
    public class ProcessKeyPush
    {
        //イベントハンドラの引数、threadのインスタンスを記録しておく
        private object sender;
        private KeyEventArgs e;
        private Thread thread;
        private Control control;

        //リソースの読み込み
        private Bitmap Wall_Image = Properties.Resources.wall;
        private Bitmap Player_Image = Properties.Resources.player;
        private Bitmap Goal_Image = Properties.Resources.goal;
        private Bitmap Point_Image = Properties.Resources.target;
        private Bitmap Obj_Image = Properties.Resources._object;
        private Bitmap Space_Image = Properties.Resources.space;


        //シングルトンのインスタンス取得
        private Map map = Map.Instance();
        private Abort abort = Abort.Instance();
        private Exclusive_control exc = Exclusive_control.Instance();
        private End_game end_game = End_game.Instacne();

        //描画用バッファ
        private Image offscreen;
        private Image offscreen_origin;

        private Graphics g;

        //コンストラクタ
        public ProcessKeyPush()
        {

            offscreen = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);

            g = Graphics.FromImage(offscreen);

            //offscreenに壁のみのマップを書き込み
            for (int i = 0; i < map.MAP_Y; i++)
            {
                for (int j = 0; j < map.MAP_X; j++)
                {

                    Bitmap image_buf;

                    switch (map.GetAspect(j, i))
                    {
                        case Aspect.Wall:
                            image_buf = Wall_Image;
                            break;
                        default:
                            continue;
                    }

                    g.DrawImage(image_buf, new Point(50 * j, 50 * i));

                }
            }

            //offscreen_originを壁のみのマップとして保存。以降グラフィックオブジェクトはこれで初期化する。
            offscreen_origin = (Image)offscreen.Clone();

            //壁以外の要素の書き込み。
            for (int i = 0; i < map.MAP_Y; i++)
            {
                for (int j = 0; j < map.MAP_X; j++)
                {

                    Bitmap image_buf;

                    switch (map.GetAspect(j, i))
                    {
                        case Aspect.Player:
                            image_buf = Player_Image;
                            break;
                        case Aspect.Goal:
                            image_buf = Goal_Image;
                            break;
                        case Aspect.Point:
                            image_buf = Point_Image;
                            break;
                        case Aspect.Obj:
                            image_buf = Obj_Image;
                            break;

                        default:
                            continue;
                    }

                    g.DrawImage(image_buf, new Point(50 * j, 50 * i));

                }


            }

            g.Dispose();
        }

        //メンバのセッター
        public object SENDER
        {
            set
            {
                sender = value;
            }
        }
        public KeyEventArgs E
        {
            set
            {
                e = value;
            }
        }
        public Thread THREAD
        {
            set
            {
                thread = value;
            }
        }
        public Control CONTLOR
        {
            set
            {
                this.control = value;
            }
        }

        //バッファのゲッタ
        public Image OFFSCREEN
        {
            get { return offscreen; }
        }


        //keypushイベントハンドラの本体
        public void Start_Draw()
        {

            Graphics g = Graphics.FromImage(OFFSCREEN);

            char move = '\0';

            //座標の移動分
            Pos delta = new Pos(0, 0);
            switch(e.KeyCode)
            {
                case Keys.W:
                    delta = new Pos(0, -1);
                    move = 'w';
                    break;
                case Keys.A:
                    delta = new Pos(-1, 0);
                    move = 'a';
                    break;
                case Keys.S:
                    delta = new Pos(0, 1);
                    move = 's';
                    break;
                case Keys.D:
                    delta = new Pos(1, 0);
                    move = 'd';
                    break;

                case Keys.Q:
                    end_game.publish();
                    break;

                default:
                    break;
            }

            //プレイヤーと移動先の座標
            Pos player = new Pos(map.PLAYER);
            Pos target = new Pos(player + delta);

            //移動先がマップ外、壁の場合、スレッド終了イベントを発出
            if(target.X <= 0 || map.MAP_X <= target.X || target.Y < 0 || map.MAP_Y < target.Y)
            {
                abort.publish();
            }

            //移動先に不動オブジェクトがある場合処理を抜ける
            switch(map.GetAspect(target))
            {
                case Aspect.Wall:
                case Aspect.Goal:
                    abort.publish();
                    break;
            }

            //移動先に何もない場合
            if(map.GetAspect(target) == Aspect.Space)
            {

                //描画処理、とりあえず60fps固定。1秒かけて等速で移動させる
                map.Move(move);

                int before_time;
                int current_time;
                int flame_time = 1000 / 60;
                int flame_count = 1;
                before_time = Environment.TickCount;
                current_time = before_time;

                //プレイヤー以外が描画されたビットマップの作製
                Image bitmap = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);
                bitmap = (Image)offscreen_origin.Clone();
                Graphics origin = Graphics.FromImage(bitmap);
                for (int i = 0; i < map.MAP_Y; i++)
                {
                    for (int j = 0; j < map.MAP_X; j++)
                    {

                        Bitmap image_buf = null;

                        switch (map.GetAspect(j, i))
                        {
                            case Aspect.Point:
                                image_buf = new Bitmap(Point_Image);
                                break;
                            case Aspect.Obj:
                                image_buf = new Bitmap(Obj_Image);
                                break;
                            case Aspect.Goal:
                                image_buf = new Bitmap(Goal_Image);
                                break;
                            case Aspect.Space:
                                //image_buf = new Bitmap(Space_Image);
                                break;

                            default:
                                continue;
                        }

                        if (image_buf != null)
                        {
                            origin.DrawImage(image_buf, new Point(50 * j, 50 * i));
                        }

                    }
                }


                while (true)
                {

                    Pos is_moving = new Pos(player * 50 + delta * (50 * flame_count / 60));
                    Image image_buffer = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);

                    image_buffer = (Image)bitmap.Clone();


                    Graphics gr = Graphics.FromImage(image_buffer);

                    gr.DrawImage(Player_Image, is_moving.X, is_moving.Y);

                    control.Invalidate();

                    flame_count++;

                    if(flame_count >= 60)
                    {
                        break;
                    }

                    current_time = Environment.TickCount;

                    offscreen = (Image)image_buffer.Clone();

                    if(current_time -before_time >= flame_time)
                    {
                        flame_count++;
                        Thread.Sleep((current_time - before_time) % flame_time);
                    }
                    else
                    {
                        Thread.Sleep((flame_time - (current_time - before_time)));
                    }

                }


            }

            //移動先に荷物がある場合
            if(map.GetAspect(target) == Aspect.Obj)
            {

                //荷物の移動先が壁か荷物、得点済みのゴールの場合処理を抜ける
                switch(map.GetAspect(target+delta))
                {
                    case Aspect.Obj:
                    case Aspect.Wall:
                    case Aspect.Goal:
                        abort.publish();
                        break;
                }

                //荷物の移動先に何もない場合
                if(map.GetAspect(target+delta) == Aspect.Space)
                {
                    //描画処理、とりあえず60fps固定。1秒かけて等速で移動させる
                    int before_time;
                    int current_time;
                    int flame_time = 1000 / 60;
                    int flame_count = 1;
                    before_time = Environment.TickCount;
                    current_time = before_time;

                    //プレイヤー以外が描画されたビットマップの作製
                    Image bitmap = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);
                    bitmap = (Image)offscreen_origin.Clone();
                    Graphics origin = Graphics.FromImage(bitmap);
                    for (int i = 0; i < map.MAP_Y; i++)
                    {
                        for (int j = 0; j < map.MAP_X; j++)
                        {

                            Bitmap image_buf = null;

                            switch (map.GetAspect(j, i))
                            {
                                case Aspect.Point:
                                    image_buf = new Bitmap(Point_Image);
                                    break;
                                case Aspect.Goal:
                                    image_buf = new Bitmap(Goal_Image);
                                    break;
                                case Aspect.Obj:
                                    //移動する荷物以外の荷物を描画
                                    if (new Pos(j, i) == target)
                                    {
                                        break;
                                    }
                                    else if(new Pos(j, i) == player)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        image_buf = new Bitmap(Obj_Image);
                                    }

                                    break;
                                case Aspect.Space:
                                    //image_buf = new Bitmap(Space_Image);
                                    break;

                                default:
                                    continue;
                            }

                            if (image_buf != null)
                            {
                                origin.DrawImage(image_buf, new Point(50 * j, 50 * i));
                            }

                        }
                    }


                    while (true)
                    {

                        Pos is_moving_player = new Pos(player * 50 + delta * (50 * flame_count / 60));
                        Pos is_moving_obj = new Pos(target * 50 + delta * (50 * flame_count / 60));
                        Image image_buffer = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);

                        image_buffer = (Image)bitmap.Clone();


                        Graphics gr = Graphics.FromImage(image_buffer);

                        gr.DrawImage(Player_Image, is_moving_player.X, is_moving_player.Y);
                        gr.DrawImage(Obj_Image, is_moving_obj.X, is_moving_obj.Y);

                        control.Invalidate();

                        flame_count++;

                        if (flame_count >= 60)
                        {
                            map.Move(move);
                            break;
                        }

                        current_time = Environment.TickCount;

                        offscreen = (Image)image_buffer.Clone();

                        if (current_time - before_time >= flame_time)
                        {
                            flame_count++;
                            Thread.Sleep((current_time - before_time) % flame_time);
                        }
                        else
                        {
                            Thread.Sleep((flame_time - (current_time - before_time)));
                        }

                    }
                }

                //荷物の移動先がゴールの場合
                if (map.GetAspect(target+delta) == Aspect.Point)
                {

                    //描画処理、とりあえず60fps固定。1秒かけて等速で移動させる
                    int before_time;
                    int current_time;
                    int flame_time = 1000 / 60;
                    int flame_count = 1;
                    before_time = Environment.TickCount;
                    current_time = before_time;

                    //プレイヤー以外が描画されたビットマップの作製
                    Image bitmap = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);
                    bitmap = (Image)offscreen_origin.Clone();
                    Graphics origin = Graphics.FromImage(bitmap);
                    for (int i = 0; i < map.MAP_Y; i++)
                    {
                        for (int j = 0; j < map.MAP_X; j++)
                        {

                            Bitmap image_buf = null;

                            switch (map.GetAspect(j, i))
                            {
                                case Aspect.Point:
                                    image_buf = new Bitmap(Point_Image);
                                    break;
                                case Aspect.Goal:
                                    image_buf = new Bitmap(Goal_Image);
                                    break;
                                case Aspect.Obj:
                                    //移動する荷物以外の荷物を描画
                                    if (new Pos(j, i) == target)
                                    {
                                        break;
                                    }
                                    else if (new Pos(j, i) == player)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        image_buf = new Bitmap(Obj_Image);
                                    }

                                    break;
                                case Aspect.Space:
                                    //image_buf = new Bitmap(Space_Image);
                                    break;

                                default:
                                    continue;
                            }

                            if (image_buf != null)
                            {
                                origin.DrawImage(image_buf, new Point(50 * j, 50 * i));
                            }

                        }
                    }


                    while (true)
                    {

                        Pos is_moving_player = new Pos(player * 50 + delta * (50 * flame_count / 60));
                        Pos is_moving_obj = new Pos(target * 50 + delta * (50 * flame_count / 60));
                        Image image_buffer = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);

                        image_buffer = (Image)bitmap.Clone();


                        Graphics gr = Graphics.FromImage(image_buffer);

                        gr.DrawImage(Player_Image, is_moving_player.X, is_moving_player.Y);
                        gr.DrawImage(Obj_Image, is_moving_obj.X, is_moving_obj.Y);

                        control.Invalidate();

                        flame_count++;

                        if (flame_count >= 60)
                        {
                            map.Move(move);

                            gr.Dispose();

                            //ゴール後のマップ描画
                            Image goal = new Bitmap((map.MAP_X) * 50, (map.MAP_Y) * 50);
                            Graphics ge = Graphics.FromImage(goal);
                            for(int i = 0; i < map.MAP_Y; i++)
                            {
                                for (int j = 0; j < map.MAP_X; j++)
                                {
                                    Bitmap image_buf = null;
                                    switch(map.GetAspect(j,i))
                                    {
                                        case Aspect.Goal:
                                            image_buf = Goal_Image;
                                            break;
                                        case Aspect.Obj:
                                            image_buf = Obj_Image;
                                            break;
                                        case Aspect.Player:
                                            image_buf = Player_Image;
                                            break;
                                        case Aspect.Point:
                                            image_buf = Point_Image;
                                            break;
                                        case Aspect.Wall:
                                            image_buf = Wall_Image;
                                            break;
                                        case Aspect.Space:
                                            //image_buf = Space_Image;
                                            break;

                                        default:
                                            break;
                                    }

                                    if(image_buf != null)
                                    {
                                        ge.DrawImage(image_buf, new Point(50 * j, 50 * i));
                                    }
                                }

                            }
                            offscreen = goal;
                            control.Invalidate();

                            break;
                        }

                        current_time = Environment.TickCount;

                        offscreen = (Image)image_buffer.Clone();

                        if (current_time - before_time >= flame_time)
                        {
                            flame_count++;
                            Thread.Sleep((current_time - before_time) % flame_time);
                        }
                        else
                        {
                            Thread.Sleep((flame_time - (current_time - before_time)));
                        }

                    }

                }

            }

            //画面の更新
            control.Invalidate();

            //グラフィックオブジェクトの破棄
            g.Dispose();

            //スレッドの終了
            abort.publish();
        }

        //KeyDownイベントハンドラ
        public void KeyPushed()
        {
            thread.Start();
        }


    }

}