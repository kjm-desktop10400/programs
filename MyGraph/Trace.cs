using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGraph
{
    class Trace
    {

        private Pen pen;
        public Pen Pen { get { return pen; } set { pen = value; } }
        private string trace_name;
        public string Trace_name { get { return trace_name; } }
        private Source_data source;
        public Source_data Source { get { return source; }set { source = value; } }
        private int colum_y;
        private int colum_x;
        public int Colum_x { get { return colum_x; } }
        public int Colum_y { get { return colum_y; } }


        public Trace(Pen p, string s, Source_data source_data, char delimiter, int x, int y)
        {
            pen = p;
            trace_name = s;
            source = source_data;

            this.colum_x = x;
            this.colum_y = y;
        }

    }
}
