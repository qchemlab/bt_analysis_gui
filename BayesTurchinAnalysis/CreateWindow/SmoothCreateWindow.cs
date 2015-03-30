using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using BTSubControl;

namespace BayesTurchinAnalysis
{
    public partial class SmoothCreateWindow : Form
    {
        Dictionary<string, int> readfrom_dic = new Dictionary<string, int>();
        List<ParamControl> param = new List<ParamControl>();
        List<Label> labels = new List<Label>();
        List<TextBox> txts = new List<TextBox>();
        List<Button> bts = new List<Button>();
        string[] line1param = new string[]{
            "read from","ibox","ism","ibeg","iend"
        };
        string[] line1explanation = new string[]{
            "read data from file xmu.dat or smooth_inp,dat",
            "do not use smoothing polynomials"+Environment.NewLine+"ibox = 1 : use 5 point box smoothing"
                +Environment.NewLine+"ibos = 2 : do not smooth",
            "in addition to smoothing polynomials use",
            "set the weight of the first ibeg data points to wt = 2",
            "set the weight of the last iend data points to wt = 2"
        };
        string[] line2param = new string[]{
            "kk","E0","fk1","alph1","alph2"
        };
        string[] line2explanation = new string[]{
            "kk   = 0 : run polynomial order from kk_min = 5 to kk_max = 13"+Environment.NewLine+
               "kk   > 0 : run for polynomial order kk"+Environment.NewLine+
               "kk   < 0 : run polynomial order from kk_min = 5 to kk_max = -kk",
            "edge energy used for transformation of energy to wave number",
            "smooth data between wave number fk1 and last point",
            "smallest weight parameter",
            "largest weight parameter"

        };

        string filename = "";

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        private void CreateToolTip(ParamControl p, string s)
        {
            ToolTip t = new ToolTip();            
            //ToolTipが表示されるまでの時間ms
            t.InitialDelay = 500;
            //ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            t.ReshowDelay = 500;
            //ToolTipを表示する時間
            t.AutoPopDelay = 10000;
            //フォームがアクティブでない時でもToolTipを表示する
            t.ShowAlways = true;

            //Button1とButton2にToolTipが表示されるようにする
            t.SetToolTip(p.PLabel, s);
            t.SetToolTip(p.PTextbox, s);
        }
        private void CreateToolTip(Control p, string s)
        {
            ToolTip t = new ToolTip();
            //ToolTipが表示されるまでの時間ms
            t.InitialDelay = 500;
            //ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            t.ReshowDelay = 500;
            //ToolTipを表示する時間
            t.AutoPopDelay = 10000;
            //フォームがアクティブでない時でもToolTipを表示する
            t.ShowAlways = true;

            //Button1とButton2にToolTipが表示されるようにする
            t.SetToolTip(p, s);
        }

        ComboBox cmb_readfrom;
        public SmoothCreateWindow(string smoothpath,bool exp = false)
        {
            InitializeComponent();
            InitializeDesign();
            this.ClientSize = new Size(400, 300);
            Assembly asm = Assembly.GetExecutingAssembly();
            StreamReader sr;
            string s = exp ? "BayesTurchinAnalysis.Resource.smooth_exp.inp" : "BayesTurchinAnalysis.Resource.smooth_thr.inp";
            if (File.Exists(smoothpath))
                sr = new StreamReader(smoothpath, BTMainForm.CharEncode);
            else
                sr = new StreamReader(asm.GetManifestResourceStream(s), BTMainForm.CharEncode);
            try
            {
                ReadSmoothInp(ref sr);
            }
            catch
            {
                sr.Close();
                sr = new StreamReader(asm.GetManifestResourceStream(s), BTMainForm.CharEncode);
                ReadSmoothInp(ref sr);
            }
            
            sr.Close();
        }
        public void InitializeDesign()
        {
            int LineParamYStart = 5;
            int LineParamXInterval = 200;
            for (int i = 0; i < 2; i++)
            {
                labels.Add(ControlFactory.Create<Label>( new Point(LineParamXInterval * i, LineParamYStart)
                    , new Size(35, 12),"Line" + (i + 1).ToString(),"Line" + (i + 1).ToString()));
                this.Controls.Add(labels.Last());
            }
            LineParamYStart += 40;
            for (int i = 1; i < line1param.Count(); i++)
            {
                param.Add(new ParamControl(0, LineParamYStart + i * 20, line1param[i]));
                CreateToolTip(param.Last(), line1explanation[i]);
            }
            for (int i = 0; i < line2param.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval, LineParamYStart + (i + 1) * 20, line2param[i]));
                CreateToolTip(param.Last(), line2explanation[i]);
            }
            foreach( var p in param)
            {
                this.Controls.Add(p);
            }


            labels.Add(ControlFactory.Create<Label>(new Point(0, LineParamYStart)
                , new Size(35, 12), "ReadFrom", "ReadFrom"));
            this.Controls.Add(labels.Last());

            cmb_readfrom = new ComboBox();
            cmb_readfrom.Location = new Point(60, LineParamYStart - 5);
            cmb_readfrom.Size = new Size(300, 19);
            CreateToolTip(cmb_readfrom, line1explanation[0]);

            cmb_readfrom.Name = "combo_readfrom";
            cmb_readfrom.TabIndex = 1;
            cmb_readfrom.DropDownStyle = ComboBoxStyle.DropDownList;
            readfrom_dic.Add("xmu.dat(x,dummy,dummy,dummy,y)(feff def)"  ,-2);
            readfrom_dic.Add("xmu.dat(x,dummy,dummy,y)"                  ,-1);
            readfrom_dic.Add( "smooth_inp.dat(x,y)"                     , 0 );
            readfrom_dic.Add("smooth_inp.dat(x,y,err_y)(exp def)", 1);
            readfrom_dic.Add("smooth_inp.dat(x,y,dummy,err_y)", 2);
            readfrom_dic.Add( "smooth_inp.dat(x,dummy,err_y,y)"         , 3 );
            readfrom_dic.Add( "smooth_inp.dat(x,dummy,y)"               , 4 );

            cmb_readfrom.Items.Add("xmu.dat(x,dummy,dummy,dummy,y)(feff def)");
            cmb_readfrom.Items.Add("xmu.dat(x,dummy,dummy,y)"                 );
            cmb_readfrom.Items.Add( "smooth_inp.dat(x,y)"                    );
            cmb_readfrom.Items.Add("smooth_inp.dat(x,y,err_y)(exp def)");
            cmb_readfrom.Items.Add( "smooth_inp.dat(x,y,dummy,err_y)"        );
            cmb_readfrom.Items.Add( "smooth_inp.dat(x,dummy,err_y,y)"        );
            cmb_readfrom.Items.Add("smooth_inp.dat(x,dummy,y)");

            cmb_readfrom.SelectedIndex = 0;
            this.Controls.Add(cmb_readfrom);

            // 
            for (int i = 0; i < 3; i++)
            {
                bts.Add(new Button());
                bts.Last().Location = new System.Drawing.Point(260, 240);
                bts.Last().Name = "bt_SmoothCreate";
                bts.Last().Size = new System.Drawing.Size(100, 30);
                bts.Last().TabIndex = 1;
                bts.Last().Text = "Create smooth";
                bts.Last().UseVisualStyleBackColor = true;
                bts.Last().Click += new System.EventHandler(this.CreateSmooth);
                this.Controls.Add(bts.Last());
                bts.Add(new Button());
                bts.Last().Location = new System.Drawing.Point(140, 240);
                bts.Last().Name = "bt_SmoothCancel";
                bts.Last().Size = new System.Drawing.Size(100, 30);
                bts.Last().TabIndex = 1;
                bts.Last().Text = "Cancel";
                bts.Last().UseVisualStyleBackColor = true;
                bts.Last().Click += new System.EventHandler(this.Cancel);
                this.Controls.Add(bts.Last());
                bts.Add(new Button());
                bts.Last().Location = new System.Drawing.Point(20, 240);
                bts.Last().Name = "bt_SmoothRead";
                bts.Last().Size = new System.Drawing.Size(100, 30);
                bts.Last().TabIndex = 1;
                bts.Last().Text = "Read File";
                bts.Last().UseVisualStyleBackColor = true;
                bts.Last().Click += new System.EventHandler(this.ReadFile);
                this.Controls.Add(bts.Last());
            }

            txts.Add(ControlFactory.Create<TextBox>(
                new Point( LineParamXInterval , LineParamYStart+6*20 ),
                new Size(200,170),"aaaaaaaaaaa")
                ); 
        }
        private void CreateSmooth(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "inp file|*.inp";
            sfd.Title = "save smooth.inp";
            sfd.FileName = "smooth.inp";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = sfd.FileName;
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, BTMainForm.CharEncode))
                {
                    sw.Write(readfrom_dic[(string)cmb_readfrom.SelectedItem].ToString() + " ");
                    for(int i=0;i<line1param.Count()-1;i++)
                    {
                        sw.Write( param[i].ToString() + (i==line1param.Count()-2?"\r\n":" ") );
                    }
                    for (int i = 0; i < line2param.Count(); i++)
                    {
                        sw.Write( param[i+line1param.Count()-1].ToString() + (i==line2param.Count()-1?"\r\n":" "));
                    }
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ReadFile(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "smooth.inp";
            ofd.Filter = "inpファイル|*.inp";
            ofd.Title = "Read smooth.inp from existing file";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, BTMainForm.CharEncode);
                filename = ofd.FileName;
                ReadSmoothInp(ref sr);
                sr.Close();
            }
        }
        private void ReadSmoothInp(ref StreamReader sr)
        {

            char[] delimiter = { ' ', '\t' };
            string[] s = sr.ReadLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            int value = int.Parse(s[0]);
            foreach (var d in readfrom_dic)
            {
                if (value == d.Value)
                {
                    cmb_readfrom.SelectedIndex = cmb_readfrom.Items.IndexOf(d.Key) ;
                    break;
                }
            }

            for (int i = 0; i < s.Count()-1 && i < param.Count(); i++)
            {
                param[i].Text = s[i+1];
            }
            s = sr.ReadLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s.Count() && i + line1param.Count()-1 < param.Count(); i++)
            {
                param[i+line1param.Count()-1].Text = s[i];
            }


        }
    }
}
