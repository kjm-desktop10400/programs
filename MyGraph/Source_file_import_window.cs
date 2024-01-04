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
        List<Source_data> Source;
        Tab_controler tab_controler;
        Source_data sd;
        source_file_browser sf;



        public Source_file_import_window(Form f, List<Source_data> s)
        {
            InitializeComponent();
            main_form = f;
            Source = s;
            tab_controler = Tab_controler.Instance();

        }

        private void Brows_button_Click(object sender, EventArgs e)
        {
            sd = null;

            sf = new source_file_browser();
            if (sf.Get_Source_File_Path())
            {
                if (Source == null)
                {
                    Source = new List<Source_data>();
                }

            }

            sd = new Source_data(sf.Source_path);
            this.Path_textbox.Text = sd.Path;

        }

        private void Preview_button_Click(object sender, EventArgs e)
        {

            this.Previw_window.Text = sd.Data;

        }

        private void Inport_button_Click(object sender, EventArgs e)
        {

            Source.Add(sd);

            tab_controler.Add_Data(this, new TabEventArgs(Path.GetFileName(Source[Source.Count - 1].Path), sd));

            Control[] cont = main_form.Controls.Find("Data_tab_control", true);
            cont[0].Controls.Add(tab_controler.Data_tab[tab_controler.Data_tab.Count() - 1]);

            this.Close();
        }
    }
}
