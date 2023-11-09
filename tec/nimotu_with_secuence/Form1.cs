using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace nimotu_with_secuence
{
    public partial class Form1 : Form
    {

        Label test = new Label();
        MANEGE_MAP manege_map = MANEGE_MAP.Instance();
        MAP map;

        public Form1()
        {
            InitializeComponent();


        }

    }


    //マップ本体のデータ保存用クラス
    public class MAP
    {

        readonly int x;
        readonly int y;
        private char[,] field;

        //コンストラクタ
        public MAP(int x, int y, char[,] data)
        {
            this.x = x;
            this.y = y;
            field = data;
        }

        //移動用の関数。成功時trueを、失敗時falseを返す
        public bool Move(Point direction)
        {
            


        }

        //テスト用。ToString関数のオーバーライド
        public override string ToString()
        {
            string line = null;

            for (int i = 0; i < y; i++)
            {
                for(int j = 0; j < x; j++)
                {
                    line += field[i, j];
                }
                line += '\n';
            }

            return line;
        }

    }

    //マップをコントロールするクラス
    public class MANEGE_MAP
    {

        //ロード中のマップ
        private static MAP map;

        //シングルトン用フィールド
        #region
        private static MANEGE_MAP _Instance = null;
        private MANEGE_MAP()
        {
        }
        public static MANEGE_MAP Instance()
        {
            if(_Instance == null)
            {
                _Instance = new MANEGE_MAP();
            }
            return _Instance;
        }
        #endregion

        //マップロード用関数。フォルダが選択されない場合nullを返す。現状マップの整合性はチェックしない。
        public MAP Load_Map()
        {
            //フォルダー選択用クラスの初期化
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "マップファイルの選択";
            openFile.InitialDirectory = @"E:\Desktop\programs\tec\Resources";

            //フォルダの選択
            if(openFile.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            //ファイルの読み込み
            int x = 0;
            int y = 0;
            string[] line = new string[100];
            char buf = '\0';
            int count = 0;
            int colum_count = 0;
            StreamReader sr = new StreamReader(openFile.FileName);

            //行数、列数のカウント
            while(true)
            {

                line[count] = sr.ReadLine();

                if (line[count] == null)
                {
                    y = count;
                    break;
                }

                line[count].Replace('\n', '\0');

                if (x < line[count].Length) x = line[count].Length;

                count++;

            }
            sr.Close();

            //mapクラスに保存するようにデータを作成
            char[,] data = new char[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                    if(j < line[i].Length)
                    {
                        data[i, j] = line[i][j];
                    }
                    else
                    {
                        data[i, j] = '\0';
                    }

                }
            }

            MAP load_data = new MAP(x, y, data);

            return load_data;
        }


        //マップの移動関数
        public bool MoveMap(Keys key)
        {
            //移動方向
            Point direction;

            //入力を移動方向に変換。不適切な入力を排除
            switch(key)
            {
                case Keys.W:
                    direction = new Point(0, -1);
                    break;
                case Keys.A:
                    direction = new Point(-1, 0);
                    break;
                case Keys.S:
                    direction = new Point(0, 1);
                    break;
                case Keys.D:
                    direction = new Point(1, 0);
                    break;

                default:
                    return false;
            }

            //移動方向をMAPクラスに渡す
            return map.Move(direction);

        }

    }

}
