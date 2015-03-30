using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace BayesTurchinAnalysis
{
    class MatrixGraphWindow : GraphWindowBase
    {
        int matrixXNum = 0;
        int matrixYNum = 0;

        List<double> Z = new List<double>();
        private PictureBox pic_gauge;
        List<string> paramname = new List<string>();

        enum LineTag
        {
            Comment,
            Matrix_XYNumber,
            Matrix_Element,
        };

        public MatrixGraphWindow()
            : base()
        {
            InitializeComponent();

            EnableAddGraph = false;
            //Area3DStyle = Area.Area3DStyle;
            //Area3DStyle.Enable3D = true;
            RangeToolMenuVisible = false;
            Area.CursorX.IsUserSelectionEnabled = false;
            Area.CursorX.AutoScroll = false;
            Area.CursorY.IsUserSelectionEnabled = false;
            Area.CursorY.AutoScroll = false;
            ScaleMenuItem = false;

            LoadBTMat();
            CreateMatrixGraph();
            RangeToolMenuVisible = false;
            this.GraphWindow_SizeChanged(this,new EventArgs());
        }
        protected override void GraphWindow_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width > 0 && this.Height > 0 && this.Height - menu.Height * 2 > 0)
            {
                chart.Location = new Point(0, menu.Height);
                chart.Size = new Size(this.Width*9/10, this.Height - menu.Height * 2);
                if (pic_gauge != null)
                {
                    pic_gauge.Location = new Point(this.Width * 9 / 10, menu.Height);
                    pic_gauge.Size = new Size(this.Width / 10, this.Height - menu.Height * 2);
                    pic_gauge.Invalidate();
                }
            }
        }
       
        private void LoadBTMat()
        {
            LineTag tag = LineTag.Comment;
            int nowRef = 0;

            foreach (string line in File.ReadLines("feffdat/BTmat.dat", BTMainForm.CharEncode))
            {
                switch (tag)
                {
                    case LineTag.Comment:
                        if (line.IndexOf("----") > 0)
                        {
                            tag = LineTag.Matrix_XYNumber;
                            continue;
                        }
                        break;
                    case LineTag.Matrix_XYNumber:
                        string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        matrixXNum = int.Parse(s[0]);
                        matrixYNum = int.Parse(s[1]);
                        tag = LineTag.Matrix_Element;
                        continue;
                    case LineTag.Matrix_Element:
                        string[] d = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < d.Count(); i++)
                        {
                            if (i == nowRef) //対角成分を０にする
                                Z.Add(0);
                            else
                                Z.Add(double.Parse(d[i]));
                        }
                        nowRef++;
                        break;
                }
                if (tag == LineTag.Matrix_Element && nowRef >= matrixYNum)
                    break;
            }
        }
        private void CreateMatrixGraph()
        {
            AxisX.Minimum = 0;
            AxisX.Maximum = matrixXNum;
            AxisY.Minimum = 0;
            AxisY.Maximum = matrixYNum;
            AxisX.Title = "parameter number";
            AxisY.Title = "parameter number";

            #region paramname_loading
            Sn2GraphWindow.ReadMode readstart = Sn2GraphWindow.ReadMode.FINDLINE;

            //Sn2の読み込み
            foreach (string line in File.ReadLines("feffdat/BTout.dat", BTMainForm.CharEncode))
            {
                if (readstart == Sn2GraphWindow.ReadMode.ENDREAD)
                    break;

                switch (readstart)
                {
                    case Sn2GraphWindow.ReadMode.FINDLINE:
                        if (line.IndexOf("Fit parameters") >= 0)
                            readstart = Sn2GraphWindow.ReadMode.READFITPARAM;
                        break;
                    case Sn2GraphWindow.ReadMode.READFITPARAM:
                        {
                            int temp; //tryparse受け取るよう
                            if (line.IndexOf(" Multiple scattering parameters") >= 0)
                            {
                                readstart = Sn2GraphWindow.ReadMode.ENDREAD;
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
                                paramname.Add(paramlines[(int)Sn2GraphWindow.ParamName.Param_name]);
                            }

                        }
                        break;
                }
            }
            #endregion

            for (int y = 0; y < matrixYNum; y++)
            {
                Series temp = new Series();
                temp.ChartArea = AreaName;
                temp.ChartType = SeriesChartType.Bubble;
                temp.MarkerStyle = MarkerStyle.Square;
                temp.SetCustomProperty("BubbleMinSize", "1");
                temp.SetCustomProperty("BubbleMaxSize", "8");
                MainChart.Series.Add(temp);
                /*Area3DStyle.Inclination = 30;
                Area3DStyle.Rotation = 30;
                Area3DStyle.WallWidth = 5;
                Area3DStyle.PointGapDepth = 10;*/
                AxisX.Interval = 1;
                AxisY.IsReversed = true;
                AxisY.Interval = 1;
                AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90;
                var max = 1;//Z.Max();
                var min = -1;//Z.Min();
                AxisX.IntervalOffset = 1;
                AxisY.IntervalOffset = 1;


                for (int x = 0; x < matrixXNum; x++)
                {
                    DataPoint dp = new DataPoint(x+1-0.5, new double[]{y-0.5+1});
                    var z = Z[x + y * matrixXNum];
                    if (z == 0)
                        continue;
                    if (z > 0)
                        dp.Color = Color.FromArgb((int)(192*z/max)+48, 196, 0, 0 );
                    else
                        dp.Color = Color.FromArgb((int)(192 * z / min) + 48, 0, 0, 196); 
                    
                    dp.ToolTip = paramname[y]  + " vs " +  paramname[x]  + Environment.NewLine + (z*100).ToString("f1")+"%";
                    MainChart.Series.Last().Points.Add(dp);

                    //軸作成
                    AxisX.CustomLabels.Add(new CustomLabel(x + 0 - 0.5, x + 1 - 0.5, paramname[x], 0,LabelMarkStyle.None));
                    AxisY.CustomLabels.Add(new CustomLabel(y + 0 - 0.5, y + 1 - 0.5, paramname[y], 0, LabelMarkStyle.None));
                }

            }
            AxisX.CustomLabels.Add(new CustomLabel(matrixXNum - 0.5 + 0, matrixXNum - 0.5 + 1, "", 0, LabelMarkStyle.None));
            AxisY.CustomLabels.Add(new CustomLabel(matrixYNum - 0.5 + 0, matrixYNum - 0.5 + 1, "", 0, LabelMarkStyle.None));

            foreach(var c in AxisX.CustomLabels)
            {
                c.ForeColor = Color.Black;
            }

        }
        protected override void SaveGraphToText(StreamWriter sw)
        {
            sw.Write("# matrix:" + matrixXNum + "*" + matrixYNum);
            for (int y = 0; y < matrixYNum; y++)
            {
                for (int x = 0; x < matrixXNum; x++)
                {
                    sw.Write(Z[x + y * matrixXNum] + " ");
                }
                sw.Write("\n");
            }
        }

        private void InitializeComponent()
        {
            this.pic_gauge = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_gauge)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_gauge
            // 
            this.pic_gauge.BackColor = System.Drawing.Color.White;
            this.pic_gauge.Location = new System.Drawing.Point(364, 232);
            this.pic_gauge.Name = "pic_gauge";
            this.pic_gauge.Size = new System.Drawing.Size(57, 50);
            this.pic_gauge.TabIndex = 2;
            this.pic_gauge.TabStop = false;
            this.pic_gauge.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_gauge_Paint);
            // 
            // MatrixGraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.pic_gauge);
            this.Name = "MatrixGraphWindow";
            this.SizeChanged += new System.EventHandler(this.MatrixGraphWindow_SizeChanged);
            this.Resize += new System.EventHandler(this.MatrixGraphWindow_Resize);
            this.Controls.SetChildIndex(this.chart, 0);
            this.Controls.SetChildIndex(this.pic_gauge, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_gauge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MatrixGraphWindow_Resize(object sender, EventArgs e)
        {
         //   throw new Exception();
        //    ClientSize = new Size((int)(ClientSize.Height + 0.5),ClientSize.Height);
        }

        private void MatrixGraphWindow_SizeChanged(object sender, EventArgs e)
        {
          //  throw new Exception();
          //  ClientSize = new Size((int)(ClientSize.Height + 0.5), ClientSize.Height);
        }

        private void pic_gauge_Paint(object sender, PaintEventArgs e)
        {
            var max = 100;//Z.Max();
            var min = -100;//Z.Min();
            var maxcolor_plus = Color.FromArgb((int)(192 * max / max) + 48, 196, 0, 0);
            var mincolor_plus = Color.FromArgb((int)(192 * 0 / max) + 48, 196, 0, 0);
            var maxcolor_minus = Color.FromArgb((int)(192 * 0 / min) + 48, 0, 0, 196);
            var mincolor_minus =Color.FromArgb((int)(192 * min / min) + 48, 0, 0, 196);

            var plusblush = new LinearGradientBrush(new Point(0, 0), new Point(0, pic_gauge.Height / 2),maxcolor_plus,mincolor_plus);
            var minusblush = new LinearGradientBrush(new Point(0, pic_gauge.Height / 2), new Point(0, pic_gauge.Height), maxcolor_minus, mincolor_minus);

            var g = e.Graphics;

            var point = new Point(pic_gauge.Width * 0/100, pic_gauge.Height * 10/100);
            var size = new Size(pic_gauge.Width * 20/100, pic_gauge.Height * 40/100);
            var point2 =   new Point(point.X ,point.Y + size.Height);

            g.FillRectangle(plusblush,
                new Rectangle(point,size));
            g.FillRectangle(minusblush,
                new Rectangle(point2, size));
            g.DrawString(max.ToString("F0") + "%", chart.Series[0].Font, Brushes.Black
                , new Point(pic_gauge.Width * 0 / 100, pic_gauge.Height * 7 / 100));
            g.DrawString(0.ToString(), chart.Series[0].Font, Brushes.Black
                , new Point(pic_gauge.Width * 20 / 100, pic_gauge.Height * 50 / 100));
            g.DrawString(min.ToString("F0") + "%", chart.Series[0].Font, Brushes.Black
                , new Point(pic_gauge.Width * 0 / 100, pic_gauge.Height * 90 / 100));
        }

    }
}
