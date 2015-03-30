using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTSubControl;
using System.IO;

namespace BayesTurchinAnalysis.CreateWindow
{
    public partial class ModExperimentWindow : Form
    {
        BTMainForm parent;
        public ModExperimentWindow(BTMainForm _parent)
        {
            parent = _parent;
            InitializeComponent();
        }

        private void rtb_original_RtbTextChanged(object sender, EventArgs e)
        {
             chk_original.Checked = ( rtb_original.Text != "" );
        }

        private void ModExperimentWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            if ( e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        private void bt_preedge_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(rtb_original.Text))
            {
                BackGroundWindow bgw = new BackGroundWindow(rtb_original.Text);
                if( bgw.ShowDialog() == DialogResult.OK)
                {
                    chk_preedge.Checked = true;
                }
            }
            else { }
         }

        private void txt_error_TextChanged(object sender, EventArgs e)
        {
            chk_error.Checked = txt_error.Text != "";
        }

        private void bt_createmodfile_Click(object sender, EventArgs e)
        {
            List<double> ee = new List<double>(), normW = new List<double>();
            double error;
            try
            {
                error = double.Parse(txt_error.Text);
                foreach (string line in File.ReadLines(rtb_original.Text, Encoding.ASCII))
                {
                    if (line.StartsWith("#"))   //コメント行は読み飛ばす
                        continue;

                    string[] param = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ee.Add(double.Parse(param[0]));
                    normW.Add(double.Parse(param[1]));
                }

                string filename = Directory.GetCurrentDirectory() + "\\smooth_inp.dat";
                using (StreamWriter sw = new StreamWriter(filename, false, Encoding.ASCII))
                {
                    sw.Write("#"+txt_comment.Text + "\r\n");
                    sw.Write("#----------------------\r\n");
                    sw.Write("#ee normW error\r\n");
                    for (int i = 0; i < ee.Count; i++)
                    {
                        sw.Write(ee[i] + " " + normW[i] + " " + normW[i] * error + "\r\n");
                    }
                }
                DialogResult = DialogResult.OK;
                parent.Focus();
                this.Visible = false;
                parent.SetModExpFile(filename);
                MessageBox.Show("smooth_inp.dat file creation success");
            }
            catch { MessageBox.Show("error!"); }
        }

    }
}
