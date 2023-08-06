using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Project1
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            //ビジュアルスタイルの有効化
            Application.EnableVisualStyles();

            //Formの表示
            Application.Run(new Form4());
            
        }
    }

    class Form1 : Form
    {
        public Form1(string str)
        {
            this.Text = str;
        }
    }

    class Form2 : Form
    {
        public Form2(string str)
        {
            this.Text = str;
            Application.Run(new Form1("Hey"));
        }
    }

    class Form3 : Form
    {
        public Form3()
        {
            //bitmapクラスのオブジェクトにラスタ画像を取り込み。
            Bitmap bitmap = new Bitmap("test_image.png");

            // 背景画像にbitmapオブジェクトを使用
            this.BackgroundImage = bitmap;
            //背景画像の並べ方の設定、ImageLayoutのフィールドはenumで定義
            this.BackgroundImageLayout = ImageLayout.Tile;
        }
    }

    class Form4 : Form
    {
        //Labelコントロールのインスタンス
        Label label = new Label();
        //buttonコントロールのインスタンス
        Button button = new Button();
        public Form4()
        {
            //Labelコントロールのプロパティ
            label.Text = "Hellow, world!";
            label.Location = new Point(10, 10);
            label.AutoSize = true;

            //Buttonコントロールのプロパティ
            button.Text = "OK";
            button.Location = new Point(10, 50);
            button.Size = new Size(120, 40);

            //コントロールをフォームに追加
            this.Controls.Add(label);
            this.Controls.Add(button);
        }
    }
}
