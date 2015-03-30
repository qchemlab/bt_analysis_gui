using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace BayesTurchinAnalysis
{
    //グラフ表示の基本ウインドウ
    public partial class GraphWindowBase : Form
    {
        public const string AreaName = "area";

        Deque<AxisRange> history = new Deque<AxisRange>();

        public bool MajorGrid
        {
            set { showMajorGridToolStripMenuItem.Checked = AxisX.MajorGrid.Enabled = AxisY.MajorGrid.Enabled = value; }
        }
        public string  WindowTitle
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public Chart MainChart { get { return chart; } }
        public ChartArea Area { get { return chart.ChartAreas[AreaName]; } }
        public PointF AxisMinVal { get { return new PointF((float)AxisX.Minimum, (float)AxisY.Minimum); } }
        public PointF AxisMaxVal { get { return new PointF((float)AxisX.Maximum, (float)AxisY.Maximum); } }
        public bool ScaleMenuItem
        {
            set { yAutoScaleToolStripMenuItem.Visible = autoScaleToolStripMenuItem.Visible = false; }
        }
        protected int Float_DigitX = 3;
        protected int Float_DigitY = 3;
        public MenuStrip MenuStripMain { get { return menu; } }

        protected Axis AxisX, AxisY;
        protected bool EnableAddGraph
        {
            get { return addGraphToolStripMenuItem.Visible; }
            set { addGraphToolStripMenuItem.Visible = deleteGraphToolStripMenuItem.Visible = value; }
        }
        protected bool RangeToolMenuVisible { get { return rangeToolStripMenuItem.Visible; } set { rangeToolStripMenuItem.Visible = value; } }

        public GraphWindowBase()
        {
            InitializeComponent();
            chart.Location = new Point(0, menu.Height);
            chart.Size = new Size(this.Width, this.Height - menu.Height * 2);
            AxisX = Area.AxisX;
            AxisY = Area.AxisY;
            Area.CursorX.IsUserSelectionEnabled = true;
            Area.CursorX.AutoScroll = true;
            Area.CursorX.Interval = 0.01;
            Area.CursorY.Interval = 0.01;
            AxisY.ScaleView.Zoomable = false;
            AxisX.ScaleView.Zoomable = false;
            Area.AxisX.MinorGrid.LineColor = Color.LightGray;
            Area.AxisY.MinorGrid.LineColor = Color.LightGray;
        }
        public string LabelX
        {
            get { return AxisX.Title; }
            set { AxisX.Title = value; }
        }
        public string LabelY
        {
            get { return AxisY.Title; }
            set { AxisY.Title = value; }
        }

        protected virtual void GraphWindow_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width > 0 && this.Height > 0 && this.Height - menu.Height * 2 > 0)
            {
                chart.Location = new Point(0, menu.Height);
                chart.Size = new Size(this.Width, this.Height - menu.Height * 2);
            }
        }
        //グラフの保存
        private void saveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Graph.png";
            var dic = new Dictionary<string, ChartImageFormat>();
            dic.Add(".png", ChartImageFormat.Png);
            dic.Add(".jpg", ChartImageFormat.Jpeg);
            dic.Add(".bmp", ChartImageFormat.Bmp);
            dic.Add(".gif", ChartImageFormat.Gif);
            dic.Add(".tiff", ChartImageFormat.Tiff);
            sfd.Filter = "";
            bool first = false;
            foreach (string s in dic.Select(k => k.Key))
            {
                sfd.Filter += (first ? "|" : "") + s + " file|*" + s;
                first = true;
            }
            sfd.Filter += "|*.*|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                chart.SaveImage(sfd.FileName, dic[Path.GetExtension(sfd.FileName)]);
            }
        }
        private void saveGraphToTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Graph.txt";
            sfd.Filter = ".txt file|*.txt|.csv file|*.csv|*.*|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.ASCII))
                {
                    SaveGraphToText(sw);
                }
            }
        }
        protected virtual void SaveGraphToText(StreamWriter sw)
        {

        }
        //その他メニュー
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        protected virtual void addGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        protected virtual void deleteGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void changeShowScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RangeSetWindow rsw = new RangeSetWindow(this);
            rsw.Show();
        }
        //拡大表示その他
        public void SetShowArea(double xmin, double xmax, double ymin, double ymax, int digitx,int digity)
        {
            SetShowArea(new PointF((float)xmin, (float)ymin), new PointF((float)xmax, (float)ymax), digitx,digity);
        }
        public void SetShowArea(PointF min, PointF max,int digitx ,int digity)
        {
            history.push_back_queue(new AxisRange(AxisX, AxisY));
            AxisX.Minimum = Math.Round(min.X, digitx);
            AxisY.Minimum = Math.Round(min.Y, digitx);
            AxisX.Maximum = Math.Round(max.X, digity);
            AxisY.Maximum = Math.Round(max.Y, digity);
        }

        private void showMajorGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AxisX.MajorGrid.Enabled = !AxisX.MajorGrid.Enabled;
            AxisY.MajorGrid.Enabled = !AxisY.MajorGrid.Enabled;
        }

        private void showMinorGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AxisX.MinorGrid.Enabled = !AxisX.MinorGrid.Enabled;
            AxisY.MinorGrid.Enabled = !AxisY.MinorGrid.Enabled;
        }
        protected virtual double GetYAbsMax(double x_min, double x_max)
        {
            return -1;
        }
        protected virtual double GetYAbsMax()
        {
            return 1;
        }
        protected virtual double GetYAbsMin(double x_min, double x_max)
        {
            return -1;
        }
        protected virtual double GetYAbsMin()
        {
            return 1;
        }


        private delegate void check_swapdel(System.Windows.Forms.DataVisualization.Charting.Cursor x);
        /// <summary>
        /// 有効桁数で丸める
        /// </summary>
        /// <param name="value">丸めたい数字</param>
        /// <param name="digit">丸めたい桁(2桁とか)</param>
        /// <param name="switch_dig">大きい数とみなす桁数(def = 6)</param>
        /// <returns></returns>
        public static double EssentialFigure(double value, int digit, int switch_dig = 6)
        {
            string v = value.ToString();
            int dig_int = -1;
            // + nの場合は+の後ろが桁数
            if (v.Contains("+"))
            {
                //
                v = v.Substring(0, v.IndexOf(".") + digit) + v.Substring(v.IndexOf("E+"));
                return double.Parse(v);
            }
            else if (v.Contains("."))
            {
                dig_int = 1;
            }
            else
            {
                dig_int = v.Length;
            }

            return Math.Round(value, digit);

        }
        private void chart_AxisViewChanging(object sender, ViewEventArgs e)
        {
            /*  var a = e.Axis;
              //start > end 判定とその場合の入れ替え
              check_swapdel swap_se = (c) =>
              {
                  if (c.SelectionStart < c.SelectionEnd)
                      return;
                  double t = c.SelectionStart;
                  c.SelectionStart = c.SelectionEnd;
                  c.SelectionEnd = t;
              };

              if (e.Axis.AxisName == AxisName.X)
              {
                  if (yAutoScaleToolStripMenuItem.Checked)
                  {
                      //start > end だったら入れ替える
                      swap_se(Area.CursorX);
                      AxisX.Minimum = Math.Round(Area.CursorX.SelectionStart, Float_Digit);
                      AxisX.Maximum = Math.Round(Area.CursorX.SelectionEnd, Float_Digit);
                      double abs_range = 0;
                      abs_range = Math.Round(GetYAbsMax(Area.CursorX.SelectionStart, Area.CursorX.SelectionEnd), Float_Digit);
                      if (abs_range > 0)
                      {
                          AxisY.Minimum = -abs_range;
                          AxisY.Maximum = abs_range;
                      }

                  }
                  if (autoScaleToolStripMenuItem.Checked == false)
                  {
                      if (double.IsNaN(Area.CursorX.SelectionStart))
                          return;

                      //start > end だったら入れ替える
                      swap_se(Area.CursorX);

                      AxisX.Minimum = EssentialFigure(Area.CursorX.SelectionStart, Float_Digit);
                      AxisX.Maximum = EssentialFigure(Area.CursorX.SelectionEnd, Float_Digit);
                  }

              }
              if (e.Axis.AxisName == AxisName.Y)
              {
                  if (yAutoScaleToolStripMenuItem.Checked)
                  {
                      //start > end だったら入れ替える
                      swap_se(Area.CursorX);
                  }
                  else if (autoScaleToolStripMenuItem.Checked == false)
                  {
                      if (double.IsNaN(Area.CursorX.SelectionStart))
                          return;

                      //start > end だったら入れ替える
                      swap_se(Area.CursorY);
                      AxisY.Minimum = EssentialFigure(Area.CursorY.SelectionStart, Float_Digit);
                      AxisY.Maximum = EssentialFigure(Area.CursorY.SelectionEnd, Float_Digit);
                      if (AxisY.Minimum == AxisY.Maximum)
                          AxisY.Maximum += Math.Pow(10, -Float_Digit);
                  }

              }
              //拡縮の取り消し
              e.NewPosition = a.ScaleView.Position;
              e.NewSize = a.ScaleView.Size;
              e.NewSizeType = a.ScaleView.SizeType;*/
        }

        private void yAutoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //チェック時XYオートを解除
            if (yAutoScaleToolStripMenuItem.Checked)
            {
                autoScaleToolStripMenuItem.Checked = false;
            }

            //Yスケールの選択可否はチェック状態と逆(Autoだと選択できない)
            Area.CursorY.IsUserSelectionEnabled = !yAutoScaleToolStripMenuItem.Checked;
            
        }
        private void autoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //チェック時Yスケール解除と初期化
            if (autoScaleToolStripMenuItem.Checked)
            {

                AxisX.Minimum = double.NaN;
                AxisX.Maximum = double.NaN;
                AxisY.Minimum = GetYAbsMin();
                AxisY.Maximum = GetYAbsMax();
                yAutoScaleToolStripMenuItem.Checked = !autoScaleToolStripMenuItem.Checked;
            }
            else
            {
                //Yスケールの選択可否はチェック状態と逆(Autoだと選択できない)
                Area.CursorY.IsUserSelectionEnabled = !yAutoScaleToolStripMenuItem.Checked;
            }
        }


        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if(RangeToolMenuVisible &&  e.Button == MouseButtons.Right)
            {
                if ( double.IsNaN(Area.CursorX.SelectionStart) || double.IsNaN(Area.CursorX.SelectionEnd))
                    return;
                //start > end 判定とその場合の入れ替え
                check_swapdel swap_se = (c) =>
                {
                    if (c.SelectionStart < c.SelectionEnd)
                        return;
                    double t = c.SelectionStart;
                    c.SelectionStart = c.SelectionEnd;
                    c.SelectionEnd = t;
                };

                if (yAutoScaleToolStripMenuItem.Checked)
                {
                    //start > end だったら入れ替える
                    swap_se(Area.CursorX);

                    var xmin = Math.Round(Area.CursorX.SelectionStart, Float_DigitX);
                    var xmax = Math.Round(Area.CursorX.SelectionEnd, Float_DigitX);
                    var ymin = Math.Round(GetYAbsMin(Area.CursorX.SelectionStart, Area.CursorX.SelectionEnd), Float_DigitY);
                    var ymax = Math.Round(GetYAbsMax(Area.CursorX.SelectionStart, Area.CursorX.SelectionEnd), Float_DigitY);

                    if (xmin < xmax && ymin < ymax)
                        SetShowArea(xmin, xmax, ymin, ymax, Float_DigitX, Float_DigitY);


                }
                else if (autoScaleToolStripMenuItem.Checked == false)
                {
                    if (double.IsNaN(Area.CursorX.SelectionStart))
                        return;

                    //start > end だったら入れ替える
                    swap_se(Area.CursorX);
                    //start > end だったら入れ替える
                    swap_se(Area.CursorY);
                    var ymin = EssentialFigure(Area.CursorY.SelectionStart, Float_DigitY);
                    var ymax = EssentialFigure(Area.CursorY.SelectionEnd, Float_DigitY);
                    if (ymin == ymax)
                        ymax += Math.Pow(10, -Float_DigitY);

                    SetShowArea(
                        EssentialFigure(Area.CursorX.SelectionStart, Float_DigitX),
                        EssentialFigure(Area.CursorX.SelectionEnd, Float_DigitX),
                        ymin,
                        ymax,Float_DigitX, Float_DigitY);
                }
                Area.CursorX.SelectionStart = double.NaN;
                Area.CursorY.SelectionStart = double.NaN;
            }
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AxisX.Minimum = double.NaN;
            AxisX.Maximum = double.NaN;
            AxisY.Minimum = GetYAbsMin();
            AxisY.Maximum = GetYAbsMax();
        }

        private void errorBarOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorBarOnOffToolStripMenuItem.Checked = !errorBarOnOffToolStripMenuItem.Checked;
            foreach (Series s in MainChart.Series)
            {
                if (s.ChartType == SeriesChartType.ErrorBar)
                {
                    s.Enabled = errorBarOnOffToolStripMenuItem.Checked;
                }
            }
        }

        private void previousRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(history.Count > 0)
            history.pop_back().SetAxPos(AxisX, AxisY);
        }

    }
    public class CustomSeries : Series
    {
        public bool Error = false;
        public string XName = "";
        public string YName = "";
        public string ErrParent = "";
        public override string ToString()
        {
            if (Error)
            {
                return ErrParent + " vs " + XName;
            }
            return YName + " vs " + XName;
        }
        public CustomSeries(string chartname, SeriesChartType type, string xname, string yname
            , bool err = false, string errbasename = "")
        {
            ChartArea = chartname;
            ChartType = type;
            XName = xname;
            YName = yname;
            Error = err;
            ErrParent = errbasename;
            if (xname == "")
                Name = yname;
            else if (err)
                Name = "Error:" + yname + "(" + errbasename + " vs " + xname + ")";
            else
                Name = yname + " vs " + xname;
        }
        public void SetLabels(Chart c)
        {
            c.ChartAreas[ChartArea].AxisX.Title = XName;
            c.ChartAreas[ChartArea].AxisY.Title = YName;
            Legend l = new Legend();
            l.Alignment = StringAlignment.Near;
            l.DockedToChartArea = ChartArea;
            c.Legends.Add(l);
        }
    }
    class AxisRange
    {
        public double xmin;
        public double xmax;
        public double ymin;
        public double ymax;
        public AxisRange(Axis X,Axis Y)
        {
            xmin = X.Minimum;
            xmax = X.Maximum;
            ymin = Y.Minimum;
            ymax = Y.Maximum;
        }
        public void SetAxPos(Axis X,Axis Y)
        {
            X.Minimum = xmin;
            Y.Minimum = ymin;
            X.Maximum = xmax;
            Y.Maximum = ymax;
        }
    }
}
