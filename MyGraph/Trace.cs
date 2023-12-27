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
        private string trace_name;
        private string Trace_name { get { return trace_name; } }
        private Source_data source;
        private int colum;


        public Trace(Pen p, string s, Source_data source_data, char delimiter)
        {
            pen = p;
            trace_name = s;
            source = source_data;
        }
        



    }
}
