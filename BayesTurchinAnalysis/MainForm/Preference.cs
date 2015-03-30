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
    public partial class PreferenceWindow : Form
    {
        PreferenceData data;
        public PreferenceWindow(PreferenceData p)
        {
            data = p;
            InitializeComponent();
            cb_showsmoothgraph.Checked = data.ShowGraphAfterSmooth;
            cb_saveworkdir.Checked = data.SaveSelectedDirectory;
            cb_saveanalysissetting.Checked = data.SaveUsedPath;


            rtb_feff.Text = data.FeffExeName;
            rtb_chimod.Text = data.Chimod12ExeName;
            rtb_chimod12d.Text = data.Chimod12dExeName;
            rtb_smooth.Text = data.SmoothExeName;
            rtb_mu0alph.Text = data.Mu0AlphExeName;

        }


        IEnumerable<T> EnumControl<T>(Control parent) where T : Control
        {
            foreach(Control c in parent.Controls)
            {
                if(c.HasChildren)
                {
                    foreach (var t in  EnumControl<T>(c))
                        yield return t;
                }
                if (c is T)
                    yield return c as T;
            }
        }
        private void bt_default_Click(object sender, EventArgs e)
        {
            foreach (var cont in EnumControl<BTSubControl.RefTextBox>(this))
            {
                cont.Text = "default";
            }

            cb_showsmoothgraph.Checked = false;
            cb_saveworkdir.Checked = false;
            cb_saveanalysissetting.Checked = false;

        }

        private void bt_apply_Click(object sender, EventArgs e)
        {
            data.ShowGraphAfterSmooth = cb_showsmoothgraph.Checked;
            data.SaveSelectedDirectory = cb_saveworkdir.Checked;
            data.SaveUsedPath = cb_saveanalysissetting.Checked;
            data.FeffExeName = rtb_feff.Text;
            data.Chimod12ExeName = rtb_chimod.Text;
            data.Chimod12dExeName = rtb_chimod12d.Text;
            data.SmoothExeName = rtb_smooth.Text;
            data.Mu0AlphExeName = rtb_mu0alph.Text;

            Close();
        }
    }
}
