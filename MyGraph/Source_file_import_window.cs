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
    public partial class Source_file_import_window : Form
    {

        Form main_form;
        List<Source_data> Sources;
        Tab_controler tab_controler;



        public Source_file_import_window(Form f, List<Source_data> s)
        {
            InitializeComponent();
            main_form = f;
            Sources = s;
            tab_controler = Tab_controler.Instance();

        }

        private void Brows_button_Click(object sender, EventArgs e)
        {
            Source_data source_data = null;

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

            Control[] cont = main_form.Controls.Find("Data_tab_control", true);
            cont[0].Controls.Add(tab_controler.Data_tab[tab_controler.Data_tab.Count() - 1]);
            this.Path_textbox.Text = source_data.Path;

        }

        private void Preview_button_Click(object sender, EventArgs e)
        {

        }

        private void Inport_button_Click(object sender, EventArgs e)
        {

        }
    }
}
