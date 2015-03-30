using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Drawing;

namespace BayesTurchinAnalysis
{
    public class Sn2GraphWindow : GraphWindowBase
    {
        List<double> param_i = new List<double>();
        List<string> param_name = new List<string>();
        List<double> Sn2 = new List<double>();
        public enum ReadMode
        {
            FINDLINE,
            SKIPONESENTENCE,
            READSN2,
            FINDFITPARAM,
            READFITPARAM,
            ENDREAD
        };
        public enum ParamName
        {
            Param_number,
            Param_name,
            A_Posteriori,
            A_Posteriori_Err,
            A_Priori,
            Difference,
            Diff_Err,
        };
        public Sn2GraphWindow()
            : base()
        {
            RangeToolMenuVisible = false;

            EnableAddGraph = false;
            Area.CursorX.IsUserSelectionEnabled = false;
            Area.CursorX.AutoScroll = false;
            Area.CursorY.IsUserSelectionEnabled = false;
            Area.CursorY.AutoScroll = false;
            ScaleMenuItem = false;


            ReadMode readstart = ReadMode.FINDLINE;

            //Sn2の読み込み
            foreach (string line in File.ReadLines("feffdat/BTout.dat", BTMainForm.CharEncode))
            {
                if (readstart == ReadMode.ENDREAD)
                    break;

                switch (readstart)
                {
                    case ReadMode.FINDLINE:
                        if (line.IndexOf("----------") > 0)
                        {
                            readstart = ReadMode.SKIPONESENTENCE;
                            continue;
                        }
                        break;
                    case ReadMode.SKIPONESENTENCE:
                        //i     w_n2       x_n2      x_err     devpar     s_n2      prior    aeff  nleg
                        //の行を読み飛ばす
                        readstart = ReadMode.READSN2;
                        continue;
                    case ReadMode.READSN2:
                        {
                            //iとsn2を読み込む
                            string[] d = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            //読み込み終わり
                            if (line == "" || d.Count() == 0)
                            {
                                readstart = ReadMode.FINDFITPARAM;
                                break;
                            }
                            //それぞれ読み込む
                            param_i.Add(double.Parse(d[0]));
                            param_name.Add(""); //名前は保留
                            Sn2.Add(double.Parse(d[5]));
                        }
                        break;
                    case ReadMode.FINDFITPARAM:
                        if (line.IndexOf("Fit parameters") >= 0)
                            readstart = ReadMode.READFITPARAM;
                        break;
                    case ReadMode.READFITPARAM:
                        {
                            int temp; //tryparse受け取るよう
                            if (line.Contains("Eigenvalues") || line.Contains("Multiple scattering parameters"))
                            {
                                readstart = ReadMode.ENDREAD;
                                break;
                            }
                            //string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            int[] param_charlength = new int[]
                            {
                                5 , //param_number
                                7 , //param_name
                                10, //a posteriori
                                5 , // +-
                                8,  //a posteriori _ error
                                13, //a priori
                                14, //difference
                                12  //diff/err
                            };
                            if (line == "")
                                continue;

                            string[] paramlines = FixedTextLoader.Split(line, param_charlength
                                , s => s.IndexOf("+-") < 0 && s != "");

                            //最初の変換が整数だったらパラメータとみなして読み込む
                            if (int.TryParse(paramlines[0], out temp))
                            {
                                param_name[temp - 1] = paramlines[(int)ParamName.Param_name];
                            }

                        }
                        break;
                }
            }

            MainChart.Series.Clear();
            List<string> lbl = new List<string>();
            Series srs = new Series();
            srs.ChartArea = "area";
            srs.ChartType = SeriesChartType.Column;
            srs.Name = "Sn2";
            srs.SetCustomProperty("PointWidth", "1.0");

            MainChart.Series.Add(srs);
            MainChart.Legends.Clear();
            AxisY.Maximum = 1.1;
            AxisX.Title = "parameter number";
            AxisY.Title = "data(1) or a priori(0)";

            Color c = Color.Blue;
            string now_param_series = param_name[0].Substring(0, 3);
            int color_ring_pos = 0;
            for (int i = 0; i < param_i.Count; i++)
            {
                //parameter
                if (!param_name[i].Contains(now_param_series))
                {
                    //パラメータ系列が違ったら別系列に移ったとみなしてその名前に変更
                    //最初3文字が識別子
                    now_param_series = param_name[i].Substring(0, Math.Min(param_name[i].Count(), 3));
                    color_ring_pos++;
                }
                c = GetRGB(color_ring_pos * 60 % 360, 0.5f, 0.6f);
                lbl.Add(param_name[i]);
                MainChart.Series[0].Points.Add(new DataPoint(param_i[i], Sn2[i]));   //グラフにデータ追加
                MainChart.Series[0].Points[i].ToolTip = param_name[i] + " : " + "(" + param_i[i] + "," + Sn2[i] + ")";
                MainChart.Series[0].Points[i].Color = c;
                MainChart.Series[0].Points[i].BorderColor = Color.Black;
                //MainChart.Series[0].IsValueShownAsLabel = true;
            }



            AxisX.LabelStyle.Interval = 2;
            AxisX.Maximum = param_i.Last() + 1;
            AxisX.MajorTickMark.Interval = 1;
        }
        protected override void SaveGraphToText(StreamWriter sw)
        {
            sw.Write("#Sn2 Data\n");
            sw.Write("#Number Name Sn2\n");
            for (int i = 0; i < param_i.Count; i++)
            {
                sw.Write(param_i[i] + " " + param_name[i] + " " + Sn2[i] + "\n");
            }
        }
        public Color GetRGB(float h, float s, float v)
        {
            float r = 0, g = 0, b = 0;
            float f, p, q, t;
            int hi = ((int)h / 60) % 6;
            f = h / 60.0f - (hi);
            p = v * (1 - s);
            q = v * (1 - f * s);
            t = v * (1 - (1 - f) * s);
            switch (hi)
            {
                case 0:
                    r = v;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = v;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = v;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = v;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = p;
                    b = q;
                    break;
            }
            return Color.FromArgb(255, (int)(200 * r), (int)(200 * g), (int)(200 * b));
        }
    }
    class FixedTextLoader
    {
        public static string[] Split(string text, int[] num, Func<string, bool> where_param = null)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < num.Count(); i++)
            {
                //-1の場合は次の空白まで
                if (num[i] == -1)
                {
                    num[i] = text.IndexOf(' ') < 0 ? text.Length : text.IndexOf(' ');
                }
                if (text.Length < num[i])
                    break;

                result.Add(text.Substring(0, num[i]).Trim());

                text = text.Substring(num[i]);
                if (text == "")
                    break;
            }
            if (where_param == null)
                where_param = s => true;

            return result.Where(where_param).ToArray();
        }
    }
}
