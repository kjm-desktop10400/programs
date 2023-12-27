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

        private double aspect_ratio = 1;
        private int screen_size_x = 10000;
        private int screen_size_y;
        public Range range;

        private Bitmap offscreen;
        public Bitmap Offscreen { get { return offscreen; } }
        private Graphics g;

        public Draw_graph()
        {

            screen_size_y = (int)(screen_size_x * aspect_ratio);
            offscreen = new Bitmap(screen_size_x, screen_size_y);
            g = Graphics.FromImage(offscreen);

        }

        public void Draw_trace(Trace t, Range range)
        {

            

        }


    }
}
