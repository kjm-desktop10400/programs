using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyGraph;

namespace Draw_graph_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            path = @"E:\Desktop\programs\Draw_graph_test\testdata\sample.csv";
            pen = new Pen(Color.Black, 50);
            range = new Range(-0.5, 0.5, -0.4, 0.4);
            source_data = new Source_data(path);
            this.textBox1.Text = source_data.Data;
            trace = new Trace(pen, "test_graph", source_data, '\t', 0, 1);

            dg = new Draw_graph();


            dg.Draw_trace(trace, range);

            bmp = new Bitmap(10000, 10000);
            graphics = Graphics.FromImage(bmp);
        }

        Pen pen;
        Range range;
        Source_data source_data;
        Trace trace;
        string path;
        Draw_graph dg;

        Bitmap bmp;
        Graphics graphics;

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.DrawLine(pen, 1000, 1000, 9000, 9000);
            this.pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics.DrawLine(pen, 9000, 1000, 1000, 9000);
            this.pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = dg.Offscreen;

        }
    }

}
