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

namespace MyGraph
{
    public partial class Form1 : Form
    {

        Draw_graph draw_graph;

        public Form1()
        {
            InitializeComponent();
        }

        //browsボタンでのファイルのブラウズ
        private void button1_Click(object sender, EventArgs e)
        {

            source_file_browser sf = new source_file_browser();
            if(sf.Get_Source_File_Path())
            {

                this.textBox3.Text = sf.Source_path;

                this.textBox8.Text = File.ReadAllText(sf.Source_path);

            }

        }

        //描画の更新
        private void button2_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = draw_graph.Offscreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            draw_graph = new Draw_graph();
            //this.pictureBox1.Image = draw_graph.Offscreen;
        }

        private void axisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
