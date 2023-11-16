using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GnuplotWrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Ditect_output.PipeOut += new Ditect_output.PipeOutputEventHandler(DisplayOutput);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            PipeTest pt = new PipeTest();
            pt.cmd();
        }

        public void DisplayOutput(object sender, PipeOutputEventArgs e)
        {
            //定義済みデリゲート"Action"を使用した書き方。詳細は知らないがInvoke(delegate)に引数を持たせるときはこうするらしい
            this.Invoke(new Action<string>(this.WriteTextBox), e.LINE);

        }


        //クロススレッド回避用デリゲート
        public delegate void DISPLAY_OUTPUT(string str);

        public void WriteTextBox(string str)
        {
            this.textBox1.Text += str + "\r\n";
        }


    }

}
