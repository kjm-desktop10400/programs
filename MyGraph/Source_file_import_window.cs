using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGraph
{
    public partial class Source_file_import_window : Form
    {
        public Source_file_import_window()
        {
            InitializeComponent();
        }

        private void Brows_button_Click(object sender, EventArgs e)
        {



            source_file_browser sf = new source_file_browser();
            if (sf.Get_Source_File_Path())
            {
                if (Sources == null)
                {
                    Sources = new List<Source_data>();
                }

                source_data = new Source_data(sf.Source_path);

                Sources.Add(source_data);
            }

            tab_controler.Add_Data(this, new TabEventArgs(Path.GetFileName(Sources[Sources.Count - 1].Path), source_data));
            this.Data_tab_control.Controls.Add(tab_controler.Data_tab[tab_controler.Index - 1]);

        }

        private void Preview_button_Click(object sender, EventArgs e)
        {

        }

        private void Inport_button_Click(object sender, EventArgs e)
        {

        }
    }
}
