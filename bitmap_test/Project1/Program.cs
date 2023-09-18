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
            Application.Run(new Form3());
            
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

/*
    class Form3_1 : Form
    {

        Bitmap bitmapImage;
        public Form3_1()
        {
            //resoursesから背景画像を読み込み
            bitmapImage = Project1.Properties.Resources.test_image;

            this.BackgroundImage = bitmapImage;
            this.BackgroundImageLayout = ImageLayout.Tile;
        }
    }

    */

    class Form4 : Form
    {
        //Labelコントロールのインスタンス
        Label label = new Label();
        //マウス座標のLavel
        Label MousePos = new Label();
        //buttonコントロールのインスタンス
        Button button = new Button();

        private int count = 0;
        private int pos_origin = 30;
        public Form4()
        {
            //Labelコントロールのプロパティ
            label.Text = "Hellow, world!";
            label.Location = new Point(10, 10);
            label.AutoSize = true;

            //MousePosコントロールのプロパティ。初期位置は(30,30)
            MousePos.Location = new Point(30, 30);
            MousePos.Text = string.Format("{0}, {1}", pos_origin, pos_origin);

            //Buttonコントロールのプロパティ
            button.Text = "OK";
            button.Location = new Point(10, 60);
            button.Size = new Size(120, 40);
            //EventHandler   引数は任意で、Handler本体の関数名を入れる。
            button.Click += new EventHandler(button_Push);



            //「閉じる」ボタンのHandler
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);

            //マウス移動のHandler
            this.MouseMove += new MouseEventHandler(From4_MouseMove);

            //カーソルがフォーム上にないときのHandler
            this.MouseLeave += new EventHandler(Form4_MouseLeave);

            //コントロールをフォームに追加
            this.Controls.Add(label);
            this.Controls.Add(MousePos);
            this.Controls.Add(button);
        }

        //EventHandler本体。今回はlabelの内容を書き換える。引数は固定が安心
        void button_Push(object sender, EventArgs e)
        {
            label.Text = "Hellow, world!    " + count.ToString("d");
            count++;
        }

        //「閉じる」ボタンのHandler本体。EventArgsの型が通常と違うことに注意
        void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //終了確認のメッセージボックス
            DialogResult result = MessageBox.Show(
                "終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );

            if(result == DialogResult.No)
            {
                e.Cancel = true;        //終了を中止
            }
        }

        //MouseMoveのEventHandler本体
        void From4_MouseMove(object sender, MouseEventArgs e)
        {

            //OKボタン上にあるときはカーソルは初期位置で、labelを変える
            if(10 <= e.X && e.X < 130 && 60 <= e.Y && e.Y < 100)
            {
                MousePos.Location = new Point(pos_origin, pos_origin);
                MousePos.Text = "selected OK";
            }
            else
            {
                MousePos.Location = new Point(e.X, e.Y);
                MousePos.Text = string.Format("({0}, {1})", e.X, e.Y);
            }

        }

        //MouseLeaveのEventHandler本体
        void Form4_MouseLeave(object sender, EventArgs e){
            //カーソルを初期位置に戻す
            MousePos.Location = new Point(pos_origin, pos_origin);
            //表示座標の変更
            MousePos.Text = string.Format("({0}, {1})", pos_origin, pos_origin);
        }

    }
}
