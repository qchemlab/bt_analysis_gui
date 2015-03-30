using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTSubControl
{
    [DefaultProperty("TextChanged")]
    public partial class ParamControl: UserControl
    {
        public event EventHandler TextChanged;
        bool autoxsize = false;
        int labelxsize = 45;
        protected override void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        } 
        public bool AutoLabelSizeX
        {
            get { return autoxsize; }
            set { autoxsize = value; SizeChange(); }
        }
        public int LabelXSize
        {
            get { return labelxsize; }
            set { labelxsize = value; SizeChange(); }
        }
        private string comment = "";
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public ParamControl()
        {
            InitializeComponent();
            SizeChange();
        }
        public ParamControl(int x,int y,string s)
        {
            InitializeComponent();
            this.Location = new Point(x, y);
            this.label.Text = s;
            SizeChange();
            
        }
        public string LabelText
        {
            get { return label.Text; }
            set 
            { 
                label.Text = value;
                SizeChange();
            }
        }
        [Browsable(true)]
        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value;  }
        }
        public int Value
        {
            get 
            {
                try
                {
                    return int.Parse(Text);
                }
                catch
                {
                    return -1;
                }
            }
        }
        private void SizeChange()
        {
            label.Location = new Point(1, 3);
            var txtloc = (autoxsize ? label.Size.Width : labelxsize) + 10;
            textBox.Location = new Point(txtloc, 0);
            textBox.Size = new Size(this.Width - txtloc-3, textBox.Size.Height);
        }
        private void ParamControl_Resize(object sender, EventArgs e)
        {
            label.Location = new Point(1, 3);
            var txtloc = label.Size.Width + 10;
            textBox.Location = new Point(txtloc, 0);
            textBox.Size = new Size(this.Width - txtloc, textBox.Size.Height);
        }
        public override string ToString()
        {
            return textBox.Text;
        }
        public Label PLabel { get { return label; } }
        public TextBox PTextbox { get { return textBox; } }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }
    }
}
