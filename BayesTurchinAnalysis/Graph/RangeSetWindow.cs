using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BayesTurchinAnalysis
{
    public partial class RangeSetWindow : Form
    {
        private GraphWindowBase parent;
        public RangeSetWindow(GraphWindowBase gw)
        {
            InitializeComponent();
            parent = gw;
            txt_xmin.Text = parent.AxisMinVal.X.ToString();
            txt_xmax.Text = parent.AxisMaxVal.X.ToString();
            txt_ymin.Text = parent.AxisMinVal.Y.ToString();
            txt_ymax.Text = parent.AxisMaxVal.Y.ToString();
        }
        
        private void bt_apply_Click(object sender, EventArgs e)
        {
            try
            {
                float xmin = float.Parse(txt_xmin.Text), xmax = float.Parse(txt_xmax.Text);
                float ymin = float.Parse(txt_ymin.Text), ymax = float.Parse(txt_ymax.Text);
                if( xmin > xmax )
                {
                    float t = xmin; xmin = xmax; xmax = t;
                    string s = txt_xmin.Text; txt_xmax.Text = txt_xmin.Text; txt_xmin.Text = s;
                }
                if(ymin > ymax)
                {
                    float t = ymin; ymin = ymax; ymax = t;
                    string s = txt_ymin.Text; txt_ymax.Text = txt_ymin.Text; txt_ymin.Text = s;
                }

                parent.SetShowArea( new PointF(xmin,ymin), new PointF(xmax,ymax),1,1 );
            }
            catch { MessageBox.Show("textbox content must be value"); }
        }

    }
}
