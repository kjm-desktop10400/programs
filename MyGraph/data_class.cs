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
        private double xrange_start;    public double Xrange_start{ set { Set_xrange(value, xrange_stop); }  get{ return xrange_start;}   }
        private double xrange_stop;     public double Xrange_stop { set { Set_xrange(value, xrange_start); } get { return xrange_stop; }  }
        private double yrange_start;    public double Yrange_start{ set { Set_yrange(value, yrange_stop); }  get { return yrange_start; } }
        private double yrange_stop;     public double Yrange_stop { set { Set_yrange(value, yrange_start); } get { return xrange_stop; }  }

        public Range(double x1, double x2, double y1, double y2)
        {

            if(x1 <= x2)
            {
                xrange_start = x1;
                xrange_stop = x2;
            }
            else
            {
                xrange_start = x2;
                xrange_stop = x1;
            }
            if(y1 <= y2)
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

    class Control_Charctor
    {

        #region For singlton field

        private static Control_Charctor _Instance = null;

        public static Control_Charctor Instance()
        {
            if(_Instance == null)
            {
                _Instance = new Control_Charctor();
            }
            return _Instance;
        }

        private Control_Charctor()
        {
            delimiter = ',';
            comment = ';';
        }

        #endregion

        private char delimiter;
        private char comment;

        public char Delimiter
        {
            set
            {
                switch(value)
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
                switch(value)
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

    class Source_data
    {

        private string[,] data;
        public string Data
        {
            get
            {
                string buf = "";
                for (int i = 0; i < colum; i++)
                {
                    for(int j = 0; j < row; j++)
                    {
                        buf += data[i, j];
                        if(j < row -1)
                        {
                            buf += "\t";
                        }
                    }
                    buf += "\r\n";
                }
                return buf;
            }
        }
        private int row;
        private int colum;
        private string path;

        public Source_data(string source_path)
        {
            string[] buf = new string[File.ReadAllLines(source_path).Length];
            buf = File.ReadAllLines(source_path);

            int row_ref;
            for(row_ref = 0; row_ref < buf.Length; row_ref++)
            {
                if(buf[row_ref].Contains(Control_Charctor.Instance().Comment))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            colum = buf.Length;
            row = buf[row_ref].Split(Control_Charctor.Instance().Delimiter).Length;
            data = new string[colum, row];

            for(int i = 0; i < buf.Length; i++)
            {
                //Control Charactorのcomment文字を含む場合
                if(buf[i].Contains(Control_Charctor.Instance().Comment))
                {
                    data[i, 0] = buf[i];
                    continue;
                }

                //Control Charactorのdelimiterで文字列を分割
                string[] tmp = buf[i].Split(Control_Charctor.Instance().Delimiter);
                for(int j = 0; j < tmp.Length; j++)
                {
                    data[i, j] = tmp[j];
                }
            }

        }

    }

}
