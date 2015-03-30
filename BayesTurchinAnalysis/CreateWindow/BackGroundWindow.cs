using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace BayesTurchinAnalysis
{
    public partial class BackGroundWindow : Form
    {
        public string SaveFile{ get; set; }
        string openfilename;
        List<double> energy = new List<double>();
        List<double> absorption = new List<double>();
        double pos_left_x = -1;
        double pos_left_y = -1;
        double pos_right_x = -1;
        double pos_right_y = -1;
        double defxmax ;
        Axis AxisX, AxisY;

        public BackGroundWindow(string filename)
        {
            InitializeComponent();
            openfilename = filename;
            this.ClientSize = new Size(500, 500);
            LoadFileAndDisplayToChart();
            AxisX = chart_exp.ChartAreas["ChartArea1"].AxisX;
            AxisY = chart_exp.ChartAreas["ChartArea1"].AxisY;
      
            defxmax = AxisX.Maximum;
        }
        private void LoadFileAndDisplayToChart()
        {
            foreach( string line in File.ReadLines(openfilename, BTMainForm.CharEncode))
            {
                if (line.StartsWith("#"))
                        continue;
                string[] values = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    energy.Add(     DoubleParse( values[0] ));
                    absorption.Add( DoubleParse( values[1] ));
                }
                catch
                {
                    MessageBox.Show("parse function failed");
                    return;
                }
            }
            for (int i = 0; i < energy.Count; i++)
            {
                DataPoint dp = new DataPoint();
                dp.SetValueXY(energy[i], absorption[i]);
                
                chart_exp.Series["EvsXmu"].Points.Add(dp);
            }

        }
        private void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
        //.44のようなものも読み込めるように
        private double DoubleParse(string s)
        {
            if (s.StartsWith("."))
            {
                s = "0" + s;
            }
            try
            {
                return double.Parse(s);
            }
            catch
            {
                throw new Exception();
            }
        }

        #region Events
        private void BackGroundWindow_SizeChanged(object sender, EventArgs e)
        {
            label1.Location = new Point(0, 5);
            txt_rightvalue.Location = new Point(label1.Width + 10, 5);
            chart_exp.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 50);
            bt_decide.Location = new Point(0, this.ClientSize.Height - 25);
            bt_decide.Size = new Size(this.ClientSize.Width, 25);
        }
        private void chart_exp_Paint(object sender, PaintEventArgs e)
        {
            if (pos_left_x >= 0)
            {
                double x = AxisX.ValueToPixelPosition(pos_left_x);
                double y = AxisY.ValueToPixelPosition(pos_left_y);
                Rectangle r = new Rectangle((int)x - 5, (int)y - 5, 10, 10);
                e.Graphics.DrawEllipse(Pens.Red, r);
            }
            if (pos_right_x >= 0)
            {
                double x = AxisX.ValueToPixelPosition(pos_right_x);
                double y = AxisY.ValueToPixelPosition(pos_right_y);
                Rectangle r = new Rectangle((int)x - 5, (int)y - 5, 10, 10);
                e.Graphics.DrawEllipse(Pens.Red, r);
            }
            //直線引きます
            if (pos_left_x >= 0 && pos_right_x >= 0)
            {
                if (pos_left_x > pos_right_x)
                {
                    Swap(ref pos_left_x, ref pos_right_x);
                    Swap(ref pos_left_y, ref pos_right_y);
                }
                if (pos_right_x != pos_left_x)
                {
                    double a = (pos_right_y - pos_left_y) / (pos_right_x - pos_left_x);
                    double b = pos_left_y - a * pos_left_x;
                    e.Graphics.DrawLine(Pens.Green
                     , new Point((int)AxisX.ValueToPixelPosition(AxisX.Minimum), (int)AxisY.ValueToPixelPosition(AxisX.Minimum * a + b))
                    , new Point((int)AxisX.ValueToPixelPosition(AxisX.Maximum), (int)AxisY.ValueToPixelPosition(AxisX.Maximum * a + b)));
                }
            }
        }
        private void chart_exp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pos_left_x = AxisX.PixelPositionToValue(e.X);
                //マウスX座標との差が最低になるエネルギーを探す
                double minv = energy.Min(x => Math.Abs(x - pos_left_x));
                //そのときのデータのインデックスを得る
                int k = energy.FindIndex(x => Math.Abs(x - pos_left_x) == minv);
                //セット
                pos_left_x = energy[k];
                pos_left_y = absorption[k];
                chart_exp.Invalidate();
            }
            if (e.Button == MouseButtons.Right)
            {
                pos_right_x = AxisX.PixelPositionToValue(e.X);
                double minv = energy.Min(x => Math.Abs(x - pos_right_x));
                int k = energy.FindIndex(x => Math.Abs(x - pos_right_x) == minv);
                pos_right_x = energy[k];
                pos_right_y = absorption[k];

                int sum_count = 0;
                double sum = 0;
                for (int i = -20; i <= 20;i++ )
                {
                    if (k + i < 0)
                        ;//sum += absorption[0];
                    else if (k + i >= absorption.Count)
                        ;//sum += absorption[absorption.Count-1];
                    else
                    {
                        sum += absorption[k];
                        sum_count++;
                    }
                }
                pos_right_y = sum / sum_count;
                    chart_exp.Invalidate();
            }

        }
        private void txt_rightvalue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int rval = int.Parse(txt_rightvalue.Text);
                if (rval > energy[0])
                    AxisX.Maximum = rval;
                if (rval < -1)
                    AxisX.Maximum = defxmax;
            }
            catch
            {
                AxisX.Maximum = defxmax;
            }
        }
        private double bgline(int enindex)
        {
            //ax+b
            if (pos_right_x != pos_left_x)
            {
                double a = (pos_right_y - pos_left_y) / (pos_right_x - pos_left_x);
                double b = pos_left_y - a * pos_left_x;
                return a * energy[enindex] + b;
            }
            return 0;
        }
        private void bt_decide_Click(object sender, EventArgs e)
        {
            SaveFile = Directory.GetCurrentDirectory() + "\\" + Path.GetFileNameWithoutExtension(openfilename) + ".nor";
            using (StreamWriter sw = new StreamWriter(SaveFile, false, Encoding.ASCII))
            {
                sw.Write("#experimentdata without background\n");
                sw.Write("#energy xmu(nor)\n");
                for (int i = 0; i < energy.Count; i++)
                {
                    sw.Write(energy[i] + " " + (absorption[i] - bgline(i)) + "\n");
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion
    }
}
