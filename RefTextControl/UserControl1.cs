using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BTSubControl
{
    [DefaultProperty("RtbTextChanged")]
    public partial class RefTextBox: UserControl
    {
        public event EventHandler RtbTextChanged;
        public bool FolderFlag { get; set; }
        static public string initialdir = "";
        static public string RefInitialDir
        {
            get { return initialdir; }
            set { initialdir = value; }
        }
        string filter = "All File|*.*";
        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }
        public RefTextBox()
        {
            InitializeComponent();
            FolderFlag = false;
            RefTextBox_Resize(this, new EventArgs());
        }

        public string FileName
        {
            get { return Path.GetFileName(textbox.Text); }
        }
        public string FullPath
        {
            get { return textbox.Text; }
        }
        [Browsable(true)]
        public override string Text
        {
            get { return textbox.Text; }
            set { textbox.Text = value; }
        }
        public delegate void MessageMethodDelegate(object sender, float x, float y, float ans);

        private void refbutton_Click(object sender, EventArgs e)
        {
            if( initialdir != "" && !Directory.Exists(initialdir) )
            {
                initialdir = "";
            }
            if (FolderFlag)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (initialdir != "")
                    fbd.SelectedPath = initialdir;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textbox.Text = fbd.SelectedPath;
                }
                
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = filter;
                if (initialdir != "")
                    ofd.InitialDirectory = initialdir;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textbox.Text = ofd.FileName;
                }
            }
        }
        public void AddFilterExtension(string [] exts)
        {
            foreach( string s in exts)
            {
                filter =  s + " file|*.|" + s + filter;
            }
        }

        private void RefTextBox_Resize(object sender, EventArgs e)
        {
            textbox.Location = new Point(4, 2);
            textbox.Size = new Size(this.Size.Width - 50, textbox.Size.Height);
            refbutton.Location = new Point(this.Size.Width - 42, 0);
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if(RtbTextChanged != null)
            RtbTextChanged(this.textbox, new EventArgs());
        }
    }
}
