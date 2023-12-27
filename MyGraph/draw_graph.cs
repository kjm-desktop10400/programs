using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGraph
{
    internal class Draw_graph
    {

        private double aspect_ratio = 0.5;
        private int window_size_x = 250;
        private int window_size_y;

        private Bitmap offscreen;
        public Bitmap Offscreen { get { return offscreen; } }

        public Draw_graph()
        {

            window_size_y = (int)(window_size_x * aspect_ratio);
            offscreen = new Bitmap(window_size_x, window_size_y);
            Graphics g = Graphics.FromImage(offscreen);

            g.DrawRectangle(new Pen(Color.Black, 2), new Rectangle((int)(window_size_x * 0.05), (int)(window_size_y * 0.05), (int)(window_size_x * 0.8), (int)(window_size_y * 0.8)));

        }


    }
}
