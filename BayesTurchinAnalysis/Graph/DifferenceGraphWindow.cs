using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace BayesTurchinAnalysis
{
    public partial class DifferenceGraphWindow : DefGraphWindow
    {
        class BToutData
        {
            public int number;
            public double apriori;
            public double aposteriori;
            public double error;
            public double difference;
            public double differr;
            public BToutData(int number ,double apriori,double aposteriori,double error,double difference,double differr)
            {
                this.number = number;
                this.apriori = apriori;
                this.aposteriori = aposteriori;
                this.error = error;
                this.difference = difference;
                this.differr = differr;
            }
        }

        ToolStripMenuItem menuparameters = new ToolStripMenuItem();
        Dictionary<string, List<BToutData>> datadic = new Dictionary<string,List<BToutData>>();

        public DifferenceGraphWindow()
        {
            InitializeComponent();

            menuparameters.Text = "Parameter";
            MenuStripMain.Items.Add( menuparameters );
            ReadBTout();
            Float_DigitX = 1;
            Float_DigitY = 6;
        }
        void ReadBTout()
        {
            bool readfitparam = false;
            foreach (var line in File.ReadLines("feffdat/BTout.dat"))
            {
                //Fit parameterまで読む
                if (!readfitparam )
                {
                    if( line.Contains("Fit parameters"))
                    readfitparam = true;
                    continue;
                }
                //multiple scattering paramters見つけたら終了 
                if (line.Contains("Eigenvalues") || line.Contains("Multiple scattering parameters"))
                {
                    break;
                }
                if (line == "")
                    continue;

                //読み込む文字数指定
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

                string[] paramlines = FixedTextLoader.Split(line, param_charlength
                    , s => s.IndexOf("+-") < 0 && s != "");
                //最初の変換が整数だったらパラメータとみなして読み込む
                int temp;
                if (int.TryParse(paramlines[0], out temp))
                {
                    string name = paramlines[1].Substring(0, paramlines[1].Length < 3 ? paramlines[1].Length : 3 ).Trim().Replace("_","");
                    if( !datadic.ContainsKey(name) )
                    {
                        datadic.Add(name, new List<BToutData>());
                        var item = menuparameters.DropDownItems.Add(name) as ToolStripMenuItem;
                        item.Click += SelectParamMenuStripClicked;
                        item.CheckOnClick = true;
                    }
                    var number = int.Parse(paramlines[0]);
                    var aposteriori = double.Parse(paramlines[2]);
                    var error = double.Parse(paramlines[3]);
                    var apriori = double.Parse(paramlines[4]);
                    var difference = double.Parse(paramlines[5]);
                    var differr = double.Parse(paramlines[6]);
                    datadic[name].Add(new BToutData(number, apriori, aposteriori, error, difference, differr));

                }
            }
            ((ToolStripMenuItem)menuparameters.DropDownItems[0]).Checked = true;
            SelectParamMenuStripClicked(menuparameters.DropDownItems[0], new EventArgs());

        }
        void SelectParamMenuStripClicked(object sender , EventArgs e)
        {
            foreach(ToolStripMenuItem item in menuparameters.DropDownItems )
            {
                if (sender != item)
                    item.Checked = false;
            }
            var selecteditem = sender as ToolStripMenuItem;
            MainChart.Series.Clear();
            AxisX.Interval = 1;
            MajorGrid = false;
            
            CustomSeries c = new CustomSeries("area", SeriesChartType.Point,"parameter number","diff");
            CustomSeries cerr = new CustomSeries("area",SeriesChartType.ErrorBar,"parameter number","error",true,"diff"); 
            
            c.SetLabels(MainChart);
            cerr.SetLabels(MainChart);
            MainChart.Legends.Clear();

            foreach(var l in MainChart.Legends)
            {
                l.Alignment = StringAlignment.Far;
            }

            var ymax = double.MinValue;
            var ymin = double.MaxValue;


            CustomSeries s = new CustomSeries("area", SeriesChartType.Line, "", "");
            MainChart.Series.Add(s);

            s.Points.AddXY(datadic[selecteditem.Text].Min(p => p.number) - 1, 0);
            foreach( var points in datadic[selecteditem.Text])
            {
                s.Points.AddXY(points.number, 0);

                c.Points.AddXY(points.number, points.difference);
                cerr.Points.Add(new DataPoint(points.number, new double[] {points.difference, points.difference - points.error,points.difference+ points.error }));
                if (ymin > points.difference - points.error)
                    ymin = points.difference - points.error;
                if (ymax < points.difference + points.error)
                    ymax = points.difference + points.error; 
                var difzero = (points.difference - points.error) * (points.difference + points.error) < 0;
                cerr.Points.Last().Color = difzero ? Color.Red : Color.Black;
            }
            s.Points.AddXY(s.Points.Max(p => p.XValue) +1, 0);


            MainChart.Series.Add(c);
            MainChart.Series.Add(cerr);
            ResetYAbs();
            var xmin = cerr.Points.Min(p => p.XValue) - 1;
            var xmax = cerr.Points.Max(p => p.XValue) + 1;
            AxisX.Minimum = xmin;
            AxisX.Maximum = xmax;
            var absdif = Math.Max(Math.Abs(ymax) * 1.05, Math.Abs(ymin) * 1.05);
            AxisY.Minimum = -Math.Round(absdif,8);
            AxisY.Maximum = Math.Round(absdif,8);
        }
    }
}
