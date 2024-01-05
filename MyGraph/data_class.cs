using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyGraph
{

    class Range
    {
        private double xrange_start; public double Xrange_start { set { Set_xrange(value, xrange_stop); } get { return xrange_start; } }
        private double xrange_stop; public double Xrange_stop { set { Set_xrange(value, xrange_start); } get { return xrange_stop; } }
        private double yrange_start; public double Yrange_start { set { Set_yrange(value, yrange_stop); } get { return yrange_start; } }
        private double yrange_stop; public double Yrange_stop { set { Set_yrange(value, yrange_start); } get { return yrange_stop; } }

        public Range(double x1, double x2, double y1, double y2)
        {

            if (x1 <= x2)
            {
                xrange_start = x1;
                xrange_stop = x2;
            }
            else
            {
                xrange_start = x2;
                xrange_stop = x1;
            }
            if (y1 <= y2)
            {
                yrange_start = y1;
                yrange_stop = y2;
            }
            else
            {
                yrange_start = y2;
                yrange_stop = y1;
            }

        }

        private void Set_xrange(double x1, double x2)
        {
            if (x1 <= x2)
            {
                xrange_start = x1;
                xrange_stop = x2;
            }
            else
            {
                xrange_start = x2;
                xrange_stop = x1;
            }
        }
        private void Set_yrange(double y1, double y2)
        {
            if (y1 <= y2)
            {
                yrange_start = y1;
                yrange_stop = y2;
            }
            else
            {
                yrange_start = y2;
                yrange_stop = y1;
            }
        }

    }

    public class Control_Charctor
    {

        public Control_Charctor()
        {
            delimiter = '\t';
            comment = ';';
        }
        public Control_Charctor(char del)
        {
            delimiter = del;
            comment = ';';
        }
        public Control_Charctor(char del, char com)
        {
            delimiter = del;
            comment = com;
        }


        private char delimiter;
        private char comment;

        public char Delimiter
        {
            set
            {
                switch (value)
                {
                    //delimiter list
                    case ' ':
                        delimiter = ' ';
                        break;
                    case ',':
                        delimiter = ',';
                        break;
                    case ';':
                        delimiter = ';';
                        break;
                    case ':':
                        delimiter = ':';
                        break;
                    case '\t':
                        delimiter = '\t';
                        break;
                    case '\n':
                        delimiter = '\n';
                        break;

                    default:
                        delimiter = ',';
                        break;
                }
            }
            get { return delimiter; }
        }
        public char Comment
        {
            set
            {
                switch (value)
                {
                    case ';':
                        comment = ';';
                        break;

                    default:
                        comment = ';';
                        break;
                }
            }
            get { return comment; }
        }

    }

    public class Source_data
    {

        private string[,] data;
        public string Data
        {
            get
            {
                string buf = "";
                for (int i = 0; i < colum; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        buf += data[i, j];
                        if (j < row - 1)
                        {
                            buf += "\t";
                        }
                    }
                    buf += "\r\n";
                }
                return buf;
            }
        }
        private int row; public int Row { get { return row; } }
        private int colum; public int Colum { get { return colum; } }
        private string path; public string Path { get { return path; } }
        private string name; public string Name { get { return name; } }

        private Control_Charctor cc;

        public Source_data(string source_path)
        {
            cc = new Control_Charctor();
            path = source_path;
            _Inport_source();
        }

        public Source_data(string source_path ,Control_Charctor CC)
        {
            cc= CC;
            path = source_path;
            _Inport_source();
        }

        private void _Inport_source()
        {
            name = System.IO.Path.GetFileName(path);

            string[] buf = new string[File.ReadAllLines(path).Length];
            buf = File.ReadAllLines(path);

            int row_ref;
            for (row_ref = 0; row_ref < buf.Length; row_ref++)
            {
                if (buf[row_ref].Contains(cc.Comment))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            colum = buf.Length;
            row = buf[row_ref].Split(cc.Delimiter).Length;
            data = new string[colum, row];

            for (int i = 0; i < buf.Length; i++)
            {
                //Control Charactorのcomment文字を含む場合
                if (buf[i].Contains(cc.Comment))
                {
                    data[i, 0] = buf[i];
                    continue;
                }

                //Control Charactorのdelimiterで文字列を分割
                string[] tmp = buf[i].Split(cc.Delimiter);
                for (int j = 0; j < tmp.Length; j++)
                {
                    data[i, j] = tmp[j];
                }
            }

        }

        public string Fetch(int c, int r)
        {
            return data[c, r];
        }

    }

}
