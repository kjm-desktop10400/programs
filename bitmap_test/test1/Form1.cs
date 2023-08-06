using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void Form1_Paint(object sender, PaintEventArgs pe)
        {

            //penの設定。Brushesクラス、Colorクラスで色を指定
            Pen pen1 = new Pen(Brushes.Aqua);
            Pen pen2 = new Pen(Color.Blue, 3);

            //Pointクラスで座標を指定
            Point startpoint = new Point(100, 100);
            Point endpoint = new Point(200, 200);

            //PaintEventArgsオブジェクトからDrawLineメソッドを呼び出し、描画
            pe.Graphics.DrawLine(pen1, startpoint, endpoint);

        }
    }
}
