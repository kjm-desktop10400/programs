using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Text;

namespace cutoff_extructer
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            Global_variable.Instance().ARGS = args;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*
            Extructer ex = new Extructer();
            ex.SetFilePath(args);
            ex.Extruct_Data();
            ex.CulcCutoff();
            ex.WriteFile();
            */

        }

    }

    public class Global_variable
    {
        private string[] args;

        public string[] ARGS
        { get; set; }

        #region
        private Global_variable()
        {

        }

        private static Global_variable _Instance = null;
        public static Global_variable Instance()
        {
            if (_Instance == null) _Instance = new Global_variable();

            return _Instance;
        }

        #endregion//シングルトン用のフィールド
    }

    public class Extructer
    {

        private double[,] data;
        private string path;
        private int data_num;
        private int sample_num;
        private string outfile;

        private string[] label;

        private string[,] cut_off;

        //ファイルパスの指定と書き出しファイル名の設定
        public static string SetFilePath(string[] args)
        {
            string path = "";

            //コマンドラインからパスの受け取り
            if (args.Length == 1)
            {
                return args[0];
            }
            else
            {
                //ファイル選択ウィンドウからパスの選択
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "select data file";
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return null;
                }

                path = ofd.FileName;

            }


            return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_cutoff.data");
        }

        //指定されたパスのデータをdouble型に変換、周波数は[,0]にまとめる
        public void Extruct_Data()
        {
            //とりあえず配列にすべてのデータを格納
            string[] buf = File.ReadAllLines(@path);

            //データ数のカウント
            data_num = 1;
            for (int i = 0; i < buf[4].Length; i++)
            {
                if (buf[4][i] == ';') data_num++;
            }

            //サンプル数のカウント
            sample_num = buf.Length - 6;

            data = new double[sample_num, data_num];

            //6行目から記録
            for (int i = 6; i < sample_num; i++)
            {

                //デリミタで文字列を分割
                string[] data_buf = buf[i].Split(',');

                for (int j = 0; j < data_num; j++)
                {

                    if (j == 0)
                    {
                        data[i - 6, 0] = double.Parse(data_buf[0]);
                    }
                    else
                    {
                        data[i - 6, j] = double.Parse(data_buf[2 * j - 1]);
                    }

                }


            }

            //データラベルの作成(ラベルに不要な文字を削る)。必要ならばコードを足す。
            label = new string[data_num + 1];
            label = buf[1].Split(';');

            for(int i = 1; i < label.Length; i++)
            {
                string tmp = label[i];
                label[i] = "";

                int equal = 0;
                int init = 0;
                int last = 0;

                //'='の位置の走査
                while(true)
                {
                    if (tmp[equal] == '=') break;

                    equal++;
                }

                //先頭の位置の検索
                init = equal;
                while(true)
                {
                    if (tmp[init - 1] == '(') break;
                    init--;
                }

                //ラベルの書き込み(1行目のみ)
                if (i == 1)
                {
                    label[0] = "";
                    for (int j = init; j < equal; j++)
                    {
                        label[0] += tmp[j];
                    }

                }

                //末尾の位置の検索
                last = equal;
                while(true)
                {
                    if (tmp[last + 1] == ')') break;
                    last++;
                }

                //検索した先頭から末尾までの書き込み
                for (int j = equal + 1; j <= last; j++)
                {
                    label[i] += tmp[j];
                }

            }

            //メモリの解放をガベコレに伝える
            buf = null;

        }


        //カットオフ周波数の記録
        public void CulcCutoff()
        {
            double dc;

            cut_off = new string[3, data_num + 1];

            cut_off[0, 0] = "";
            for(int i = 0; i < data_num;i++)
            {
                cut_off[0, i] = label[i];
            }

            cut_off[1, 0] = "cutoff_freq[Hz]";
            for (int i = 1; i < data_num; i++)
            {
                int count = 0;
                dc = data[0, i];

                //直流との絶対値の差が3dB以上になったらループを抜ける
                while (abs(dc, data[count, i]) < 3)
                {
                    count++;
                }

                cut_off[1, i] = data[count, 0].ToString();
                cut_off[2,i] = data[count, i].ToString();

                Console.WriteLine("sample : " + cut_off[0,i] + "\tdc : " + dc.ToString() + "\tcutoff : " + data[count, i].ToString() + " / " + cut_off[1, i] + " Hz");
            }

        }

        //ファイルへの書き込み
        public void WriteFile()
        {

            using (StreamWriter sw = new StreamWriter(outfile, false))
            {

                //sw.WriteLine("sample\tfreq[Hz]\tAmplitude");

                sw.WriteLine(cut_off[0, 0] + "\t" + "freq[Hz]" + "\t");

                for (int i = 1; i < data_num + 1; i++)
                {
                    sw.WriteLine(cut_off[0, i] + "\t" + cut_off[1, i] + "\t");
                }
            }

        }


        //二つの数字を比較し絶対値が3未満の時trueを、3以上の時falseを返す
        private static double abs(double lhs, double rhs)
        {
            double tmp = lhs - rhs;

            if(tmp < 0)
            {
                tmp *= -1;
            }

            return tmp;
        }

    }

}
