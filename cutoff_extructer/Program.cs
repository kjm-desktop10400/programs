using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Messaging;

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
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */

            Extructer ex = new Extructer();
            ex.SetFilePath(args);
            ex.Extruct_Data();
            ex.CulcCutoff();
            ex.WriteFile();

        }

    }

    public class Extructer
    {

        private double[,] data;
        private string path;
        private int data_num;
        private int sample_num;

        private string[] label;

        private string[,] cut_off;

        //ファイルパスの指定
        public void SetFilePath(string[] args)
        {
            //コマンドラインからパスの受け取り
            if (args.Length == 1)
            {
                path = args[0];
                return;
            }

            //ファイル選択ウィンドウからパスの選択
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "select data file";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                path = null;
                return;
            }


            path = ofd.FileName;
            return;
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
            for(int i = 0; i < label.Length; i++)
            {
                string tmp = label[i];
                label[i] = "";

                for(int j = 0; j<tmp.Length;j++)
                {
                    switch(tmp[j])
                    {
                        case ' ':
                        case ',':

                            break;

                        default:
                            label[i] += tmp[j];
                            break;
                    }
                }

            }

            //メモリの解法をガベコレに伝える
            buf = null;

        }


        //カットオフ周波数の記録
        public void CulcCutoff()
        {
            double dc;

            cut_off = new string[3, data_num + 1];

            cut_off[0, 0] = "";
            for(int i = 1; i < data_num;i++)
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

            using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(path), "cutoff.dat"), false))
            {

                //sw.WriteLine("sample\tfreq[Hz]\tAmplitude");

                for (int i = 1; i < data_num + 1; i++)
                {
                    sw.Write("L[um]\t" + "freq[Hz]" + "\t");
                }
                sw.Write("\n");

                for (int i = 1; i < data_num + 1; i++)
                {
                    double tmp = 0.18 + 0.02 * (i - 1);
                    sw.Write(tmp + "\t" + cut_off[1,i] + "\t");
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
