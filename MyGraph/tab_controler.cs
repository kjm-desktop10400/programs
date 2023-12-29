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

        private TabControl tab;
        private List<TabPage> controls;     public List<TabPage> Controls { get { return controls; } }
        private static int index;           public int Index { get { return index; } }

        #region singlton field

        private static Tab_controler _Instance = null;
        private Tab_controler()
        {
            controls= new List<TabPage>();
        }
        public static Tab_controler Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Tab_controler();




                index = 1;
            }
            return _Instance;
        }

        #endregion

        public delegate void TabEventHandler(object sender, TabEventArgs e);
        
        public event TabEventHandler TabEvent;

        public void Add_data_tab(object sender,TabEventArgs e)
        {

            TabPage ctrl = new TabPage();
            ctrl.Location = new Point(4, 22);
            ctrl.Name = e.Tab_name;
            ctrl.Size = new Size(517, 464);
            ctrl.TabIndex = index;
            ctrl.Text = e.Tab_name;

            TextBox textBox= new TextBox();
            textBox.Location = new Point(0, 0);
            textBox.Multiline = true;
            textBox.Size = new Size(517, 462);
            textBox.ReadOnly= true;
            textBox.ScrollBars= ScrollBars.Both;
            textBox.Name = "data_text_" + e.Tab_name;

            ctrl.Controls.Add(textBox);

            controls.Add(ctrl);

            index++;
        }

    }


    public class TabEventArgs : EventArgs
    {
        private string tab_name;        public string Tab_name { get { return tab_name; } }

        public TabEventArgs(string tab_name_r)
        {
            tab_name = tab_name_r;
        }

    }


}
