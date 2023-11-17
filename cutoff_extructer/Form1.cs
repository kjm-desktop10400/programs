using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cutoff_extructer
{
    public partial class Form1 : Form
    {

        Global_variable gv = Global_variable.Instance();

        public Form1()
        {
            InitializeComponent();
        }

        //Controls
        #region

        //Buttons
        #region
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Extructer.SetFilePath(gv.ARGS);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Extructer.SetFilePath(gv.ARGS);
        }
        //実行ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = Extructer.SetFilePath(gv.ARGS);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = Extructer.SetFilePath(gv.ARGS);
        }
        #endregion//Buttons


        //CheckBoxes
        #region
        //ac phase file
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                button2.Enabled = true;
                textBox2.Enabled = true;
                checkBox2.Enabled = true;

                button5.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                textBox2.Enabled = false;
                checkBox2.Enabled= false;

                button5.Enabled = false;
                textBox4.Enabled = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                button5.Enabled= true;
                textBox4.Enabled= true;
            }
            else
            {
                button5.Enabled= false;
                textBox4.Enabled= false;
            }
        }


        #endregion

        #endregion

    }
}
