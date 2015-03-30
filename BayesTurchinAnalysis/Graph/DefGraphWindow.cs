using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.IO;

namespace BayesTurchinAnalysis
{
    //2値を取る普通のグラフ
    public class DefGraphWindow : GraphWindowBase
    {
        double y_abs , y_minus;
        class GraphData
        {
            public List<double> Xval = new List<double>();
            public List<double> Yval = new List<double>();
            public List<double> Errval = new List<double>();
            public string XName = "";
            public string YName = "";
            public string ErrName = "";
            public override string ToString()
            {
                return XName + " vs " + YName + (Errval.Count != 0 ? "(error" + ErrName + ")" : "");
            }
        }
        
        public void AddGraphByXYWindow(SelectXYDataWindow sw)
        {
            //グラフ用の系列を生成
            CustomSeries srs = new CustomSeries(AreaName, SeriesChartType.Line, sw.ParamXName, sw.ParamYName);
            
            CustomSeries errsrs = null;
            MainChart.Series.Add(srs);
            srs.SetLabels( MainChart);

            //エラーバーも必要な場合はそれも生成
            if (sw.ParamErrName != null)
            {
                errsrs = new CustomSeries(AreaName, SeriesChartType.ErrorBar, sw.ParamXName, sw.ParamErrName, true, sw.ParamYName);
                MainChart.Series.Add(errsrs);
            }
            srs.BorderWidth = 2;

            //初期、終了、RDF、BG振動の4ブロックにデータが分かれているので各開始の文字を指定
            string[] data_block_startval = { "Eexp_0", "Eexp_1", "f(R)_SS", "kbg" };
            //ファイルの読み込み
            var filecontents = File.ReadLines("feffdat/BTdat.dat", BTMainForm.CharEncode);
            bool read = false;
            int line_element_num = -1;
            //必要なデータを抜き出す簡易関数定義
            Func<string, bool> searchfunc = s =>
                {
                    //開始位置検索
                    if (!read && s.Contains(data_block_startval[sw.Line]))
                    {
                        read = true;
                        return false;
                    }
                    //読み込み終了判定
                    if (read)
                    {
                        //要素個数を記録
                        if (line_element_num == -1)
                        {
                            line_element_num = s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Count();
                        }
                        //要素個数が違うか、次のブロックの開始に差し掛かったら終了
                        if (line_element_num != s.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Count() ||
                            (sw.Line < data_block_startval.Count() && s.Contains(data_block_startval[sw.Line + 1])
                        ))
                        {
                            read = false;
                            return false;
                        }
                    }
                    //trueを返したら読み込み
                    return read;
                };

            try
            {
                //LINQ、whereを使って条件に合うブロックを全て抜き出した後処理
                foreach (string line in filecontents.Where(searchfunc))
                {
                    //要素に分割
                    string[] d = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    //それぞれ読み込む
                    double x = double.Parse(d[sw.Xindex]);
                    double y = double.Parse(d[sw.Yindex]);
                    if (errsrs != null)
                    {
                        double err = double.Parse(d[sw.Errorindex]);
                        srs.Points.Add(new DataPoint(x, y));   //グラフにデータ追加
                        errsrs.Points.Add(new DataPoint(x, new double[] { y, y - err, y + err }));   //グラフにデータ追加
                        errsrs.Points.Last()["ErrorBarCenterMarkerStyle"] = "Circle";
                        errsrs.Points.Last()["ErrorBarSeries"] = "Exp";
                    }
                    else
                    {
                        srs.Points.Add(new DataPoint(x, y));   //グラフにデータ追加
                    }
                }
                ResetYAbs();
            }
            catch( ArgumentOutOfRangeException e )
            {
                MessageBox.Show("divergence of yvalue : " + sw.ParamYName );
            }
        }
        public void AddGraphBySeries(CustomSeries s)
        {
            MainChart.Series.Add(s);
            ResetYAbs();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DefGraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "DefGraphWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected override void addGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectXYDataWindow sw = new SelectXYDataWindow();
            if(sw.ShowDialog() == DialogResult.OK)
            {
                AddGraphByXYWindow(sw);
            }
        }
        protected override void deleteGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteGraphWindow dgw = new DeleteGraphWindow(MainChart.Series.Select(x => x.Name));
            if (dgw.ShowDialog() == DialogResult.OK)
            {
                foreach(string s in dgw.GetSelectedItems())
                {
                    MainChart.Series.Remove(MainChart.Series.FindByName(s));
                }
            }
        }
        protected override double GetYAbsMax(double x_min, double x_max)
        {
            double abs = 0;
            foreach(Series s in MainChart.Series)
            {
                foreach( double val in s.Points.Where( x => x.XValue >= x_min && x.XValue <= x_max ).Select(x => x.YValues[0]) )
                {
                    abs = Math.Max(abs, val);
                }
            }
            return abs;
        }
        protected override double GetYAbsMax()
        {
            return y_abs;
        }
        protected override double GetYAbsMin(double x_min, double x_max)
        {
            double abs = 0;
            foreach (Series s in MainChart.Series)
            {
                foreach (double val in s.Points.Where(x => x.XValue >= x_min && x.XValue <= x_max).Select(x => x.YValues[0]))
                {
                    abs = Math.Min(abs, val);
                }
            }
            return abs;
        }
        protected override double GetYAbsMin()
        {
            return y_minus;
        }
        protected void ResetYAbs()
        {
            y_abs = 0;
            y_minus = 0;
            foreach (Series s in MainChart.Series)
            {
                if (s.ChartType != SeriesChartType.ErrorBar)
                {
                    foreach (double val in s.Points.Select(x => x.YValues[0]))
                    {
                        y_abs = Math.Max(y_abs, val);
                        y_minus = Math.Min(y_minus, val);
                    }
                }
            }
            AxisY.Minimum = y_minus;
            AxisY.Maximum = y_abs;
            var m = Math.Min(Math.Abs(y_abs), Math.Abs(y_minus));
            if ( m != 0)
            {
                var m2 = m;
                while(m<1)
                    m*=10;

                //Y軸のインターバル設定しておかないとひどい目に
                Area.CursorY.Interval = m2 / m;
            }
        }
        protected override void SaveGraphToText(StreamWriter sw)
        {
            int pointnum = MainChart.Series[0].Points.Count;
            sw.Write("#");
            foreach (CustomSeries s in MainChart.Series)
            {
                if (s.Error)
                    sw.Write( s.YName + " ");
                else
                    sw.Write(s.XName+" " + s.YName + " " );
            }
            sw.Write("\n");
            for (int i = 0; i < pointnum;i++ )
            {
                foreach (CustomSeries s in MainChart.Series)
                {
                    
                    if (s.Error)
                    {
                        sw.Write( Math.Abs(s.Points[i].YValues[0]-s.Points[i].YValues[1]) + " ");
                    }
                    else
                        sw.Write(s.Points[i].XValue + " " + s.Points[i].YValues[0] + " ");
                }
                sw.Write("\n");
            }


        }
    }
}