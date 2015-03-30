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
    class OtherWindow : DefGraphWindow
    {
        char[] delimiter = new char[] { ' ', '\t' };
        public void SetAlphaValues(string fname)
        {
            bool readstart = false;
            CustomSeries s = new CustomSeries(AreaName, SeriesChartType.Line, "alpha", "P(alpha)");
            MainChart.Series.Add(s);
            foreach(string line in File.ReadLines(fname))
            {
                if(!readstart)
                {
                    if(line.Contains("alph") && line.Contains("Pc"))
                    {
                        readstart = true;
                        continue;
                    }
                }
                else
                {
                    var t = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < t.Count();i++)
                    {
                        if (  t[i].Contains("+") && !t[i].Contains("E"))
                            t[i] = t[i].Insert(t[i].IndexOf("+"), "E");
                        if (t[i].Contains("-") && !t[i].Contains("E"))
                            t[i] = t[i].Insert(t[i].IndexOf("-"), "E");
                    }

                    s.Points.Add(new DataPoint(double.Parse(t[0]), Math.Log10(double.Parse(t[1]))));
                }
            
            }
            ResetYAbs();
        }
        public void SetEigenValues()
        {
            AxisX.Minimum = 0;
            AxisX.Interval = 4;
            MajorGrid = false;

            
            var fname = "feffdat/BTout.dat";
            double alpha = 0;
            bool readstart = false;
            List<double> Eigenvalues = new List<double>();
            foreach (string line in File.ReadLines(fname))
            {
                if( line.Contains( "Alpha =") )
                {
                    var t = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    alpha = double.Parse(t[2]);
                }

                if (!readstart)
                {
                    if (line.Contains("Eigenvalues") )
                    {
                        readstart = true;
                        continue;
                    }
                }
                else
                {
                    
                    var t = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    if(t.Count() == 0)
                        break;
                    foreach (var i in t.Select(x => double.Parse(x)))
                        Eigenvalues.Add(i);
                }

            }

            CustomSeries s = new CustomSeries(AreaName, SeriesChartType.Point, "number", "eigenvalue");
            CustomSeries s2 = new CustomSeries(AreaName, SeriesChartType.Line, "", "alpha");
            s2.BorderWidth = 3;
            for(int i=0;i<Eigenvalues.Count();i++)
            {
                s.Points.Add(new DataPoint(i,  Eigenvalues[i]));
                s2.Points.Add(new DataPoint(i, alpha));
            }
            MainChart.Series.Add(s);
            MainChart.Series.Add(s2);
            s.SetLabels(MainChart);
            s2.SetLabels(MainChart);
            Area.AxisY.IsLogarithmic = true;
        }
        public void SetSmoothGraphMuAv(string filename, string legendplusname = "")
        {

            bool readstart = false;
            CustomSeries s = new CustomSeries(AreaName, SeriesChartType.Line, "energy",
                Path.GetFileNameWithoutExtension(filename) + legendplusname);
            s.SetLabels(MainChart);
            double factor = 1;
            foreach(var line in File.ReadLines(filename))
            {
                if(!readstart)
                {
                   if(line.Contains( "normalization factor") )
                   {
                      var t = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                      double.TryParse(t[1], out factor);
                   }
                   if(line.Contains("mu") && line.Contains("en"))
                   {
                       readstart = true;
                       continue;
                   }
                }
                else
                {
                    var t = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    s.Points.Add(new DataPoint(double.Parse(t[0]), double.Parse(t[1]) / factor ));
                }
            }
            MainChart.Series.Add(s);
            
        }
        public void SetSmoothGraphMu(string filename,string legendplusname ="" )
        {
            bool readstart = false;
            CustomSeries s = new CustomSeries(AreaName, SeriesChartType.Line, "energy", Path.GetFileNameWithoutExtension(filename) + legendplusname);

            s.SetLabels(MainChart);
            double factor = 1;
            foreach (var line in File.ReadLines(filename))
            {
                if (!readstart)
                {
                    if (line.Contains("normalization factor"))
                    {
                        var t = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        double.TryParse(t[1], out factor);
                    }
                    if (line.Contains("mu") && line.Contains("en"))
                    {
                        readstart = true;
                        continue;
                    }
                }
                else
                {
                    var t = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    s.Points.Add(new DataPoint(double.Parse(t[0]), double.Parse(t[2]) / factor));
                }
            }
            MainChart.Series.Add(s);
        }
        public OtherWindow():base()
        {
            EnableAddGraph = false;
           // RangeToolMenuVisible = false;
            Area.CursorX.IsUserSelectionEnabled = false;
            Area.CursorX.AutoScroll = false;
            Area.CursorY.IsUserSelectionEnabled = false;
            Area.CursorY.AutoScroll = false;
           // ScaleMenuItem = false;
        }

    }
}