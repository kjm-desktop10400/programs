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
        List<Control> upper_x_group;
        List<Control> left_y_group;

        Bitmap screen;

        public Form1()
        {
            InitializeComponent();
        }


        //DRAW button
        private void button2_Click(object sender, EventArgs e)
        {
            Range range = new Range(double.Parse(textBox4.Text), double.Parse(textBox6.Text), double.Parse(textBox5.Text), double.Parse(textBox7.Text));
            Pen pen = new Pen(Color.Black, 10);
            pen.DashStyle = DashStyle.Dash;

            screen = new Bitmap(10000, 10000);
            Graphics graphics = Graphics.FromImage(screen);
            Trace[] trace = new Trace[Tab_controler.Instance().Trace_tab.Count];
            Draw_graph[] dg = new Draw_graph[Tab_controler.Instance().Trace_tab.Count];

            for (int i = 0; i < trace.Length; i++)
            {
                TabPage tp = Tab_controler.Instance().Trace_tab[i];
                Control[] cont = tp.Controls.Find("Source", false);
                Source_data sd = null;
                dg[i] = new Draw_graph();

                //Tab_controler.Trace_tabに登録されている名前でSource_dataを検索し、一致したSource_dataをtraceに渡す
                foreach(Source_data tmp in Sources)
                {
                    if (((string)((ComboBox)cont[0]).SelectedItem) == tmp.Name) 
                    {
                        sd = tmp;
                        break;
                    }
                }
                trace[i] = new Trace(pen, tp.Name, sd, '\t', 0, 1);

                //checkボタンによるtrace描画の可否とその描画処理
                cont = tp.Controls.Find("Show", false);
                if (((CheckBox)cont[0]).Checked)
                {
                    dg[i].Draw_trace(trace[i], range);
                    graphics.DrawImage(dg[i].Offscreen, new Point(0, 0));
                }
            }


            this.pictureBox1.Image = screen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tab_controler = Tab_controler.Instance();
            draw_graph = new Draw_graph();
            //this.pictureBox1.Image = draw_graph.Offscreen;

            #region upper_x_group
            upper_x_group = new List<Control>();
            upper_x_group.Add(upper_x_range_label);
            upper_x_group.Add(upper_x_childa);
            upper_x_group.Add(upper_x_tick_label);
            upper_x_group.Add(upper_x_sub_tick_label);
            upper_x_group.Add(upper_x_logscale);
            upper_x_group.Add(upper_x_range_start);
            upper_x_group.Add(upper_x_range_stop);
            upper_x_group.Add(upper_x_tick);
            upper_x_group.Add(upper_x_sub_tick);
            foreach (Control cont in upper_x_group) cont.Enabled = false;
            #endregion

            #region left_y_group
            left_y_group = new List<Control>();
            left_y_group.Add(left_y_range_label);
            left_y_group.Add(left_y_childa);
            left_y_group.Add(left_y_tick_label);
            left_y_group.Add(left_y_sub_tick_label);
            left_y_group.Add(left_y_logscale);
            left_y_group.Add(left_y_range_start);
            left_y_group.Add(left_y_range_stop);
            left_y_group.Add(left_y_tick);
            left_y_group.Add(left_y_sub_tick);
            foreach (Control cont in left_y_group) cont.Enabled = false;
            #endregion


            this.textBox4.Text = "-0.5";
            this.textBox6.Text = "0.5";
            this.textBox5.Text = "-0.3";
            this.textBox7.Text = "0.3";
        }

        #region past controls
        //DELETE
        private void button5_Click(object sender, EventArgs e)
        {

            draw_graph.Delete_screen();
            this.pictureBox1.Image = draw_graph.Offscreen;

        }

        //ADD tab botton
        private void button4_Click(object sender, EventArgs e)
        {
            tab_controler.Add_Data(this, new TabEventArgs("auto added tab" + (tab_controler.Index - 1).ToString()));
            tab_controler.Add_Trace(this, new TabEventArgs("auto added tab" + (tab_controler.Trace_index - 1).ToString()));
            this.Data_tab_control.Controls.Add(tab_controler.Data_tab[tab_controler.Index - 1]);
            this.Trace_tab_control.Controls.Add(tab_controler.Trace_tab[tab_controler.Trace_index - 1]);
        }
        #endregion

        private void upper_x_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (upper_x_checkbox.Checked == true)
            {
                foreach(Control cont in upper_x_group)
                {
                    cont.Enabled = true;
                }
            }
            else
            {
                foreach (Control cont in upper_x_group)
                {
                    cont.Enabled = false;
                }

            }
        }

        private void left_y_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (left_y_checkbox.Checked == true)
            {
                foreach(Control cont in left_y_group)
                {
                    cont.Enabled = true;
                }
            }
            else
            {
                foreach(Control cont in left_y_group)
                {
                    cont.Enabled = false;
                }
            }
        }

        private void browsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Source_file_import_window());



        }

        private void addTraceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tab_controler.Add_Trace(this, new TabEventArgs("Trace" + (tab_controler.Trace_tab.Count() + 1).ToString()));
            this.Trace_tab_control.Controls.Add(tab_controler.Trace_tab[tab_controler.Trace_tab.Count() - 1]);
        }

    }
}
