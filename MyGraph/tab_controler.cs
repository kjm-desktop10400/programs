using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGraph
{
    internal class Tab_controler
    {
        //Data tab管理用Listとインデックス
        private List<TabPage> data_tab;     public List<TabPage> Data_tab { get { return data_tab; } }
        private static int data_index;           public int Data_index { get { return data_index; } }


        //Trace tab管理用Listとインデックス
        private List<TabPage> trace_tab;    public List<TabPage> Trace_tab { get { return trace_tab; } }
        private static int trace_index;     public int Trace_index { get { return trace_index; } }




        #region singlton field

        private static Tab_controler _Instance = null;
        private Tab_controler()
        {
            data_tab = new List<TabPage>();
            data_index = 0;
            trace_tab = new List<TabPage>();
            trace_index = 0;
        }
        public static Tab_controler Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Tab_controler();
            }
            return _Instance;
        }

        #endregion

        public delegate void TabEventHandler(object sender, TabEventArgs e);
        
        public event TabEventHandler TabEvent;

        public void Add_Data(object sender,TabEventArgs e)
        {

            TabPage ctrl = new TabPage();
            ctrl.Location = new Point(4, 22);
            ctrl.Name = e.Tab_name;
            ctrl.Size = new Size(517, 464);
            ctrl.TabIndex = data_index;
            ctrl.Text = e.Tab_name;
            ctrl.UseVisualStyleBackColor = true;

            TextBox textBox= new TextBox();
            textBox.Location = new Point(0, 0);
            textBox.Multiline = true;
            textBox.Size = new Size(517, 462);
            textBox.ReadOnly= true;
            textBox.ScrollBars= ScrollBars.Both;
            textBox.Name = e.Tab_name;
            textBox.BackColor = Color.WhiteSmoke;
            textBox.Text = e.Source.Data;

            ctrl.Controls.Add(textBox);

            data_tab.Add(ctrl);

            //data tabが追加されたとき、既存のtrace tabのsourceに追加するdata tabの名前を追加
            foreach(TabPage tt in trace_tab)
            {
                Control[] cont = tt.Controls.Find("Source", false);
                ((ComboBox)cont[0]).Items.Add(ctrl.Name);
            }

            data_index++;
        }

        public void Add_Trace(object sender, TabEventArgs e)
        {

            TabPage tt = new TabPage();
            tt.Name = e.Tab_name;
            tt.Text = e.Tab_name;
            tt.Location = new Point(4, 22);
            tt.Size = new Size(517, 464);
            tt.TabIndex = trace_index;
            tt.Padding = new Padding(3);
            tt.UseVisualStyleBackColor = true;

            #region  label

            Label[] label = new Label[14];

            for(int i = 0; i<14;i++)
            {
                label[i] = new Label();
                label[i].AutoSize = true;
                label[i].Size = new Size(33, 12);
            }

            label[0].Name = "Name_label";
            label[0].Text = "Name";
            label[0].Location = new Point(69, 24);

            label[1].Name = "Source_label";
            label[1].Text = "Source";
            label[1].Location = new Point(53, 52);

            label[2].Name = "Color_label";
            label[2].Text = "Color";
            label[2].Location = new Point(15, 93);

            label[3].Name = "Line_Type_label";
            label[3].Text = "Line Type";
            label[3].Location = new Point(190, 93);

            label[4].Name = "Colum_x_label";
            label[4].Text = "Colum x";
            label[4].Location = new Point(15, 121);

            label[5].Name = "Colum_Y_label";
            label[5].Text = "Colum y";
            label[5].Location = new Point(189, 120);

            label[6].Name = "Ref_x_label";
            label[6].Text = "Ref x";
            label[6].Location = new Point(15, 146);

            label[7].Name = "Ref_y_label";
            label[7].Text = "Ref y";
            label[7].Location = new Point(191, 149);

            label[8].Name = "Filter_label";
            label[8].Text = "Filter";
            label[8].Location = new Point(15, 173);

            label[9].Name = "Filter_x_label";
            label[9].Text = "Filter x";
            label[9].Location = new Point(42, 192);

            label[10].Name = "Filter_y_label";
            label[10].Text = "Filter y";
            label[10].Location = new Point(42, 217);

            label[11].Name = "Tranceform_label";
            label[11].Text = "Tranceform";
            label[11].Location = new Point(12, 252);

            label[12].Name = "Tranceform_x_label";
            label[12].Text = "Tranceform x";
            label[12].Location = new Point(38, 272);

            label[13].Name = "Tranceform_y_label";
            label[13].Text = "Tranceform y";
            label[13].Location = new Point(38, 296);

            for (int i = 0; i < 14; i++)
            {
                tt.Controls.Add(label[i]);
            }

            #endregion  label

            #region text box

            TextBox[] textBox = new TextBox[5];
            for (int i = 0; i < 5; i++)
            {
                textBox[i] = new TextBox();
                textBox[i].Margin = new Padding(2);
            }

            textBox[0].Name = "Name";
            textBox[0].Location = new Point(107, 21);
            textBox[0].Size = new Size(230, 19);
            textBox[0].Text = e.Tab_name + "-" + (Trace_tab.Count + 1).ToString();

            textBox[1].Name = "xFilter";
            textBox[1].Location = new Point(116, 189);
            textBox[1].Size = new Size(251, 19);

            textBox[2].Name = "yFilter";
            textBox[2].Location = new Point(116, 214);
            textBox[2].Size = new Size(251, 19);

            textBox[3].Name = "xTranceform";
            textBox[3].Location = new Point(116, 269);
            textBox[3].Size = new Size(251, 19);

            textBox[4].Name = "yTranceform";
            textBox[4].Location = new Point(116, 293);
            textBox[4].Size = new Size(251, 19);

            for (int i = 0; i < 5; i++)
            {
                tt.Controls.Add(textBox[i]);
            }

            #endregion

            #region combobox

            ComboBox[] comboBoxes = new ComboBox[7];

            for (int i = 0; i < 7; i++)
            {
                comboBoxes[i] = new ComboBox();
                comboBoxes[i].FormattingEnabled = true;
                comboBoxes[i].Margin = new Padding(2);
                comboBoxes[i].Size = new Size(92, 20);
            }

            comboBoxes[0].Name = "Source";
            comboBoxes[0].Location = new Point(100, 48);
            foreach(TabPage tp in data_tab)
            {
                comboBoxes[0].Items.Add(tp.Name);
            }


            comboBoxes[1].Name = "Color";
            comboBoxes[1].Location = new Point(71, 91);
            

            comboBoxes[2].Name = "Line_Type";
            comboBoxes[2].Location = new Point(245, 91);

            comboBoxes[3].Name = "Colum_x";
            comboBoxes[3].Location = new Point(71, 118);

            comboBoxes[4].Name = "Colum_y";
            comboBoxes[4].Location = new Point(245, 118);

            comboBoxes[5].Name = "Ref_x";
            comboBoxes[5].Location = new Point(71, 143);

            comboBoxes[6].Name = "Ref_y";
            comboBoxes[6].Location = new Point(245, 143);

            for (int i = 0; i < 7; i++)
            {
                tt.Controls.Add(comboBoxes[i]);
            }

            #endregion

            #region checkbox

            CheckBox checkBox = new CheckBox();
            checkBox.AutoSize = true;
            checkBox.Location = new Point(220, 52);
            checkBox.Margin = new System.Windows.Forms.Padding(2);
            checkBox.Name = "Show";
            checkBox.Size = new System.Drawing.Size(66, 16);
            checkBox.Text = "Show Trace";
            checkBox.UseVisualStyleBackColor = true;
            checkBox.Checked = true;

            tt.Controls.Add(checkBox);

            #endregion


            trace_tab.Add(tt);
            trace_index++;
        }



    }


    public class TabEventArgs : EventArgs
    {
        private string tab_name;        public string Tab_name { get { return tab_name; } }
        private int width;              public int Width { get { return width; } }
        private int height;             public int Height { get { return height; } }
        private Source_data source;    public Source_data Source { get { return source; } }

        public TabEventArgs(string tab_name_r, int w, int h)
        {
            tab_name = tab_name_r;
            width = w;
            height = h;
        }
        public TabEventArgs(string tab_name_r)
        {
            tab_name = tab_name_r;
        }
        public TabEventArgs(string tab_name_r,Source_data s)
        {
            tab_name = tab_name_r;
            source = s;

        }


    }


}
