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
using System.Drawing.Drawing2D;

namespace MyGraph
{
    public partial class Form1 : Form
    {

        Draw_graph draw_graph;
        List<Source_data> Sources = null;
        Source_data source_data;
        Tab_controler tab_controler;

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
                if(Sources == null)
                {
                    Sources = new List<Source_data>();
                }

                this.textBox3.Text = sf.Source_path;
                source_data = new Source_data(sf.Source_path);

                Sources.Add(source_data);

            }

        }

        //描画の更新
        private void button2_Click(object sender, EventArgs e)
        {
            draw_graph.range = new Range(double.Parse(textBox4.Text), double.Parse(textBox6.Text), double.Parse(textBox5.Text), double.Parse(textBox7.Text));
            Pen pen = new Pen(Color.Black, 10);
            pen.DashStyle = DashStyle.Dash;
            Trace trace = new Trace(pen, "test_graph", Sources[0], '\t', 0, 1);
            draw_graph.Draw_trace(trace, draw_graph.range);

            this.pictureBox1.Image = draw_graph.Offscreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tab_controler = Tab_controler.Instance();
            draw_graph = new Draw_graph();
            //this.pictureBox1.Image = draw_graph.Offscreen;

            this.textBox4.Text = "-0.5";
            this.textBox6.Text = "0.5";
            this.textBox5.Text = "-0.3";
            this.textBox7.Text = "0.3";
        }

        //DELETE
        private void button5_Click(object sender, EventArgs e)
        {

            draw_graph.Delete_screen();
            this.pictureBox1.Image = draw_graph.Offscreen;

        }

        //ADD tab botton
        private void button4_Click(object sender, EventArgs e)
        {
            tab_controler.Add_data_tab(this, new TabEventArgs("auto added tab" + (tab_controler.Index - 1).ToString()));
            this.Setting_Tab.Controls.Add(tab_controler.Controls[tab_controler.Index - 2]);
        }

    }
}
