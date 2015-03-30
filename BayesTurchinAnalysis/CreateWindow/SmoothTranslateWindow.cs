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

namespace BayesTurchinAnalysis.CreateWindow
{
    public partial class SmoothTranslateWindow : GraphWindowBase
    {
        class Mudata
        {
            List<string> datas;
            public Mudata(string line)
            {
                datas = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
       
            }
            public double Energy
            {
                get { return double.Parse(datas[0]); }
                set { datas[0] = value.ToString("F3"); }
            }
            public double MuAv { get { return double.Parse(datas[1]); } }
            public string LineData
            {
                get
                {
                    string ret = "    ";
                    foreach (var s in datas)
                    {
                        ret += s + "   ";
                    }
                    return ret;
                }
            }
        }
        CustomSeries srs_feff;
        CustomSeries srs_exp;
        double mu0av_dif = 0;
        double muexp_dif = 0;
        double yscale_mu0 = 1;
        double yscale_muexp = 1;
        public bool create_failed = false;


        public SmoothTranslateWindow()
        {
            InitializeComponent();
            SmoothTranslateWindow_SizeChanged(this, new EventArgs());
            LoadGraphs();
            Float_DigitY = 6;
        }
        void LoadGraphs()
        {
            if (!(File.Exists("feffdat/mu0_av.dat") && File.Exists("feffdat/muexp_av.dat")))
            {
                MessageBox.Show("mu0_av and muexp_av isn't exist!");
                Close();
                create_failed = true;
                return;
            }
            List<Mudata> mu0av = new List<Mudata>();
            List<Mudata> muexp = new List<Mudata>();
            bool readstart = false;
            bool dashflag = false;

            double norm_mu0 = 1;
            double norm_muexp = 1;
            cb_target.SelectedIndex = 0;

            foreach (string line in File.ReadLines("feffdat/mu0_av.dat"))
            {
                if (readstart)
                    mu0av.Add(new Mudata(line));
                else
                {
                    if(line.Contains("energy shift"))
                    {
                        double temp;
                        var s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(x => double.TryParse(x, out temp)).First();
                        mu0av_dif = double.Parse(s);
                    }
                    if (line.Contains("normalization factor"))
                    {
                        var t = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        double.TryParse(t[1], out norm_mu0);
                    }
                    if (!line.StartsWith("#") && dashflag)
                        readstart = true;
                    if (line.Contains("---"))
                        dashflag = true;
                }
            }
            readstart = false;
            dashflag = false;
            foreach (string line in File.ReadLines("feffdat/muexp_av.dat"))
            {
                if (readstart)
                    muexp.Add(new Mudata(line));
                else
                {
                    if (line.Contains("energy shift"))
                    {
                        double temp;
                        var s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(x => double.TryParse(x, out temp)).First();
                        muexp_dif = double.Parse(s);
                    }
                    if (line.Contains("normalization factor"))
                    {
                        var t = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        double.TryParse(t[1], out norm_muexp);
                    }
                    if (!line.StartsWith("#") && dashflag)
                        readstart = true;
                    if (line.Contains("---"))
                        dashflag = true;
                }
            }

            srs_feff = new CustomSeries("area", SeriesChartType.Line
                , "energy", "mu0_av");
            foreach (var t in mu0av)
            {
                srs_feff.Points.AddXY(t.Energy, t.MuAv);
            }

            srs_exp = new CustomSeries("area", SeriesChartType.Line
                , "energy", "muexp_av");
            foreach (var t in muexp)
            {
                srs_exp.Points.AddXY(t.Energy, t.MuAv * norm_mu0 / norm_muexp);
            }
            chart.Series.Add(srs_feff);
            chart.Series.Add(srs_exp);
            srs_exp.SetLabels(chart);
            srs_feff.SetLabels(chart);
            txt_dif.Text = muexp_dif.ToString() ;


            //"../feffdat/mu0_av.dat");
            //File.Copy("smooth_out.dat", "../feffdat/muexp_av.dat", true);
        }
        private void SmoothTranslateWindow_SizeChanged(object sender, EventArgs e)
        {
            var size = (int)(this.ClientSize.Height - 30);
            chart.Location = new Point(0, menu.Height);
            chart.Size = new Size(this.ClientSize.Width, size- menu.Height);

            foreach (Control c in this.Controls)
            {
                if (c != chart)
                   c.Location = new Point(c.Location.X, size + 5);
            }
        }

        private void cb_target_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_target.SelectedText == "mu exp")
            {
                txt_dif.Text = muexp_dif.ToString();
                txt_yscale.Text = yscale_muexp.ToString();
            }
            else
            {
                txt_dif.Text = mu0av_dif.ToString();
                txt_yscale.Text = yscale_mu0.ToString();
            }
        }

        private void txt_dif_TextChanged(object sender, EventArgs e)
        {
            double tempdif;
            if (double.TryParse(txt_dif.Text, out tempdif))
            {
                if (Math.Abs(tempdif) > 1000000000 || srs_exp == null )
                {
                    return;
                }

                if (cb_target.SelectedItem.ToString() == "mu exp")
                {
                    foreach (var s in srs_exp.Points)
                        s.XValue = s.XValue - muexp_dif + tempdif;
                    muexp_dif = tempdif;
                }
                else
                {
                    foreach (var s in srs_feff.Points)
                        s.XValue = s.XValue - mu0av_dif + tempdif;
                    mu0av_dif = tempdif;
                }
               // SetXAxisRange();
            }
        }

        void SetXAxisRange()
        {
            AxisX.Minimum 
                = Math.Min(srs_exp.Points.Min(x => x.XValue), srs_feff.Points.Min(x => x.XValue) );
            AxisX.Maximum
                = Math.Max(srs_exp.Points.Max(x => x.XValue), srs_feff.Points.Max(x => x.XValue) );
            AxisX.Minimum = Math.Round(AxisX.Minimum);
            AxisX.Maximum = Math.Round(AxisX.Maximum);
        }
        private void bt_decide_Click(object sender, EventArgs e)
        {
            bool readstart;
            bool dashflag;

            {
                dashflag = false;
                readstart = false;

                File.Copy("feffdat/mu0_av.dat", "feffdat/mu0_av_temp.dat", true);
                var contents = File.ReadLines("feffdat/mu0_av_temp.dat").ToArray();

                using (StreamWriter sw = new StreamWriter("feffdat/mu0_av_temp.dat", false, Encoding.ASCII))
                {
                    if (mu0av_dif != 0.0)
                        sw.WriteLine("#energy shift from original data : " + mu0av_dif + " eV");
                    for (int i = 0; i < contents.Count(); i++)
                    {
                        if (readstart)
                        {
                            Mudata m = new Mudata(contents[i]);
                            m.Energy = m.Energy + mu0av_dif;
                            sw.WriteLine(m.LineData);
                        }
                        else
                        {
                            if (!contents[i].StartsWith("#") && dashflag)
                                readstart = true;
                            if (contents[i].Contains("---"))
                                dashflag = true;
                            sw.WriteLine(contents[i]);
                        }
                    }
                }
                BTMainForm.MoveFile_Overwrite("feffdat/mu0_av.dat", "feffdat/mu0_av_old.dat");
                BTMainForm.MoveFile_Overwrite("feffdat/mu0_av_temp.dat", "feffdat/mu0_av.dat");
            }
            {
                readstart = false;
                dashflag = false;
                File.Copy("feffdat/muexp_av.dat", "feffdat/muexp_av_temp.dat", true);
                var contents = File.ReadLines("feffdat/muexp_av_temp.dat").ToArray();

                using (StreamWriter sw = new StreamWriter("feffdat/muexp_av_temp.dat", false, Encoding.ASCII))
                {
                    if (muexp_dif != 0.0)
                        sw.WriteLine("#energy shift from original data : " + muexp_dif + " eV");
                    for (int i = 0; i < contents.Count(); i++)
                    {
                        if (readstart)
                        {
                            Mudata m = new Mudata(contents[i]);
                            m.Energy = m.Energy + muexp_dif;
                            
                            sw.WriteLine(m.LineData);
                        }
                        else
                        {
                            if (!contents[i].StartsWith("#") && dashflag)
                                readstart = true;
                            if (contents[i].Contains("---"))
                                dashflag = true;

                            if(!contents[i].Contains("energy shift"))
                            sw.WriteLine(contents[i]);
                        }
                    }
                }
                BTMainForm.MoveFile_Overwrite("feffdat/muexp_av.dat", "feffdat/muexp_av_old.dat");
                BTMainForm.MoveFile_Overwrite("feffdat/muexp_av_temp.dat", "feffdat/muexp_av.dat");
            }

            MessageBox.Show("update smooth mu files");
            Close();
        }

        private void txt_yscale_TextChanged(object sender, EventArgs e)
        {
            double tempsc;
            if (double.TryParse(txt_yscale.Text, out tempsc))
            {
                if (Math.Abs(tempsc) > 10000000 || srs_exp == null || tempsc == 0)
                {
                    return;
                }

                if (cb_target.SelectedItem.ToString() == "mu exp")
                {
                    foreach (var s in srs_exp.Points)
                        s.YValues[0] = (double)((decimal)s.YValues[0] / (decimal)yscale_muexp * (decimal)tempsc);
                    yscale_muexp = tempsc;
                }
                else
                {
                    foreach (var s in srs_feff.Points)
                        s.YValues[0] = (double)((decimal)s.YValues[0] / (decimal)yscale_mu0 * (decimal)tempsc);
                    yscale_mu0 = tempsc;
                }
                SetXAxisRange();
            }
        }
        protected override double GetYAbsMax(double x_min, double x_max)
        {
            var expymax = srs_exp.Points.Where(t => t.XValue >= x_min && t.XValue <= x_max).Max(t => t.YValues[0]);
            var feffymax = srs_feff.Points.Where(t => t.XValue >= x_min && t.XValue <= x_max).Max(t => t.YValues[0]);
            return Math.Max(expymax,feffymax);
        }
        protected override double GetYAbsMax()
        {
            var expymax = srs_exp.Points.Max(t => t.YValues[0]);
            var feffymax = srs_feff.Points.Max(t => t.YValues[0]);
            return Math.Max(expymax,feffymax);
        }
        protected override double GetYAbsMin(double x_min, double x_max)
        {
            var expymax = srs_exp.Points.Where( t => t.XValue >= x_min && t.XValue <= x_max).Min(t => t.YValues[0]);
            var feffymax = srs_feff.Points.Where(t => t.XValue >= x_min && t.XValue <= x_max).Min(t => t.YValues[0]);
            return Math.Min(expymax, feffymax);
        }
        protected override double GetYAbsMin()
        {
            var expymax = srs_exp.Points.Min(t => t.YValues[0]);
            var feffymax = srs_feff.Points.Min(t => t.YValues[0]);
            return Math.Min(expymax, feffymax);
        }

        private void txt_dif_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                SetXAxisRange();
            }
        }
    }
}
