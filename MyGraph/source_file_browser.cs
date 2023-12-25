using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGraph
{
    internal class source_file_browser
    {

        private string source_path;
        public string Source_path
        {
            get { return source_path; }
        }
        private OpenFileDialog openFileDialog;
        
        public bool Get_Source_File_Path()
        {

            using(openFileDialog = new OpenFileDialog())
            {

                openFileDialog.AutoUpgradeEnabled= true;
                openFileDialog.Title = "source file";

                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    source_path = openFileDialog.FileName;
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }



    }
}
