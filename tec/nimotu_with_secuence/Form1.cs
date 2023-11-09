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

        public MAP(int x, int y, char[,] data)
        {
            this.x = x;
            this.y = y;
            field = data;
        }

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

        //マップロード用関数。フォルダが選択されない場合nullを返す
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

    }

}
