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
    public partial class DeleteGraphWindow : Form
    {
        public ListBox.SelectedObjectCollection GetSelectedItems()
        {
            return list_graph.SelectedItems;
        }
        public DeleteGraphWindow(IEnumerable<string> items)
        {
            InitializeComponent();
            foreach (string s in items)
                list_graph.Items.Add(s);
        }

        private void bt_decide_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
