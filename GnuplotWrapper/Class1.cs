using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuplotWrapper
{
    class PipeTest
    {

        public void cmd()
        {

            System.Diagnostics.Process pipe = new System.Diagnostics.Process();

            //コマンドプロンプトを実行するプログラムとして指定
            pipe.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");

            //入力できるようにする　
            pipe.StartInfo.UseShellExecute = false;
            pipe.StartInfo.RedirectStandardInput = true;

            //非同期で出力を読み取れるようにする
            pipe.StartInfo.RedirectStandardOutput = true;
            pipe.OutputDataReceived += pipe_OutputDataReceived;

            //ウィンドウ表示の可否。trueで非表示
            pipe.StartInfo.CreateNoWindow = true;

            //実行コマンドの指定("/c"は実行後閉じるために必要)
            //pipe.StartInfo.Arguments = @"dir";

            //起動
            pipe.Start();

            //非同期で出力の読み取りを開始
            pipe.BeginOutputReadLine();

            //入力のストリームを取得
            System.IO.StreamWriter sw = pipe.StandardInput;

            if(sw.BaseStream.CanWrite)
            {
                sw.WriteLine(@"gnuplot");
                //sw.WriteLine(@"plot sin(x)");
                //sw.WriteLine("exit");
            }

            //pipe.WaitForExit();
            pipe.Close();

        }

        //OutputDataReceivedイベントハンドラ
        //行が出力されるたびに呼び出される
        void pipe_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            //出力された文字列を表示する
            //今回は自作のPipeOutイベントとそのイベントハンドラで処理
            Ditect_output.publish(e.Data);
            
        }

    }

    //自作EventArgsの定義
    //EventArgsを継承し、コンストラクタ・ゲッターのみ定義する。
    public class PipeOutputEventArgs : EventArgs
    {
        private string line = "";
        
        public PipeOutputEventArgs(string str)
        {
            line = str;
        }

        public string LINE
        {
            get
            {
                return line;
            }
        }
    }

    //
    public class Ditect_output
    {
        //シングルトン用フィールド
        #region
        private static Ditect_output _Instacnce = null;
        private Ditect_output()
        {

        }
        public static Ditect_output Instance()
        {
            if (_Instacnce == null) _Instacnce = new Ditect_output();
            return _Instacnce;
        }
        #endregion

        //
        public delegate void PipeOutputEventHandler(object sender, PipeOutputEventArgs e);
        public static event PipeOutputEventHandler PipeOut;

        public static void publish(string str)
        {
            if(PipeOut != null)
            {
                PipeOut(_Instacnce , new PipeOutputEventArgs(str));
            }
        }

    }

}
