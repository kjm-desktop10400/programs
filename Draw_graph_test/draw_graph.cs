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
        private int from = 0;

        private Bitmap offscreen;
        public Bitmap Offscreen { get { return offscreen; } }
        private Graphics g;

        public Draw_graph()
        {

            screen_size_y = (int)(screen_size_x * aspect_ratio);
            offscreen = new Bitmap(screen_size_x, screen_size_y);
            g = Graphics.FromImage(offscreen);

        }

        public void Draw_trace(Trace t, Range recieve)
        {

            this.range = recieve;

            Point befor = Tranceform(t.Source.Fetch(from, t.Colum_x), t.Source.Fetch(from, t.Colum_y));
            Point after = Tranceform(t.Source.Fetch(from + 1, t.Colum_x), t.Source.Fetch(from + 1, t.Colum_y));

            for (int i = from + 1; i + 1 < t.Source.Colum; i++)
            {

                g.DrawLine(t.Pen, befor, after);

                befor = after;
                after = Tranceform(t.Source.Fetch(i + 1, t.Colum_x), t.Source.Fetch(i + 1, t.Colum_y));

            }

            g.DrawRectangle(t.Pen, new Rectangle(0, 0, screen_size_x, screen_size_y));
            g.DrawLine(t.Pen, Tranceform(range.Xrange_start, 0), Tranceform(range.Xrange_stop, 0));
            g.DrawLine(t.Pen, Tranceform(0, range.Yrange_start), Tranceform(0, range.Yrange_stop));

        }

        //transform point to screen
        private Point Tranceform(double x, double y)
        {

            double screen_x_d = (x - range.Xrange_start) * screen_size_x / (range.Xrange_stop - range.Xrange_start);
            double screen_y_d = (1 - (y - range.Yrange_start) / (range.Yrange_stop - range.Yrange_start)) * screen_size_y;

            return new Point((int)screen_x_d, (int)screen_y_d);

        }
        private Point Tranceform(string x, string y)
        {
            return Tranceform(double.Parse(x), double.Parse(y));
        }


    }
}
