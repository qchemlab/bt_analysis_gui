using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using BTSubControl;

//BTinp.datを作るためのウインドウ。
namespace BayesTurchinAnalysis
{
    public partial class BTCreateWindow : Form
    {
        List<ParamControl> param = new List<ParamControl>();
        List<Label> labels = new List<Label>();
        List<TextBox> txts = new List<TextBox>();
        List<Button> bts = new List<Button>();
        List<string> line_explanation = new List<string>();
        string filename = "";

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        #region ValueNames
        string[] line2param = new string[]{
                "itermax","mxpath", "mxspath", "mc3path", "kni", "ircorr", "itrunc", "inpex", 
                "minxq","malphopt", "n0bkg", "ipbkg", "iamatc", "nord", "ircfct", "iboot","lalph"
            };
        string[] line3param = new string[]{
                "bkgw", "dels02", "dele0", "delrp", "delsp", "delc3", "deldg", "delstd"
            };
        string[] line4param = new string[]{
                "q_min", "errxla", "erramp", "errpha", "errrad", "errsig", "cstd", "S02", "E0"
            };
        string[] line5param = new string[]{
                "experr", "erre1", "erre2", "erre12", "erre3", "erre4", "erre34", "errk1", "errk2",
                "errk12","errk3", "errk4", "errk34", "errk5", "errk6", "errk56"
            };
        string[] line6param = new string[]{
                "nfed", "ifed", "fedt", "fedf1", "fedf2", "fedf3", "fedf4", "delfed1", 
                "delfed2", "delfed3", "delfed4", "debt", "deldebt", "rsh1", "rsh2", "rsh3", "o2m"
            };
        string[] line6aparam = new string[]{
                "fedf5", "delfed5", "fedf6", "delfed6", "fedf7", "delfed7", "rsh5", "rsh6", "rsh7"
            };
        string[] line7param = new string[]{
                "mum", "ism", "e0l1", "e0l2", "e0l3", "osca", "oscr", "oscs"
            };
        string[] line8param = new string[]{
                "ifilt", "qwin", "rwin", "qwin1", "qwin2", "rwin1", "rwin2", "nqfft", "ftrm"
            };
        string[] line9param = new string[]{
                "npot", "cpotx(i)"
            };
        string[] line10param = new string[]{
                "lpx", "dparlp0x(i)"
            };
        string[] line11param = new string[]{
                "iwtf", "nwtf(i)", "mwtf", "wtfs(i)"
            };
        string[] line12param = new string[]{
                "mix", "mixdir"
            };
        #endregion
        public BTCreateWindow(string btinp_path)
        {
            InitializeComponent();
            this.ClientSize = new Size(500, 600);
            tabControl1.Size = new Size(480, 500);
            line_explanation.Add("6a:sprint constand(add)");
            line_explanation.Add("1:comment");
            line_explanation.Add("2:bt def setting");
            line_explanation.Add("3:fit parameter");
            line_explanation.Add("4:feff error");
            line_explanation.Add("5:exp error");
            line_explanation.Add("6:spring const/DW temp");
            line_explanation.Add("7:L edge");
            line_explanation.Add("8:fourier filter");
            line_explanation.Add("(9:for testing)");
            line_explanation.Add("10:lattice constant");
            line_explanation.Add("11:layer summation");
            line_explanation.Add("12:mix");


            InitializeTab1();
            InitializeTab2();
            InitializeTab3();
            InitializeButtons();
            txt_comment.Size = new Size(480,90);
            txt_comment.Location = new Point(10, 505);

            InitializeTextBoxes(btinp_path);


        }
        //テキストボックスの値をデフォルトの値か読み込んだデータで埋める
        private void InitializeTextBoxes(string btinp_path)
        {
            //テキストボックスにデフォルトの値を入れる
            StreamReader sr;
            Assembly asm = Assembly.GetExecutingAssembly();
            if (File.Exists(btinp_path))
            {
                sr = new StreamReader(btinp_path, BTMainForm.CharEncode);
            }
            else
            {
                sr = new StreamReader(asm.GetManifestResourceStream("BayesTurchinAnalysis.Resource.BTinp_orig.txt"), BTMainForm.CharEncode);
            }
            try
            {
                ReadBTInpData(ref sr);
            }
            catch
            {
                sr.Close();
                sr = new StreamReader(asm.GetManifestResourceStream("BayesTurchinAnalysis.Resource.BTinp_orig.txt"), BTMainForm.CharEncode);

                ReadBTInpData(ref sr);
            }
            finally
            {
                sr.Close();
            }
            sr = new StreamReader(asm.GetManifestResourceStream("BayesTurchinAnalysis.Resource.BTparams.csv"), BTMainForm.CharEncode);

            CreateDefToolTips(ref sr);
            sr.Close();
        }
        private void SetDefToolTipProperties(ToolTip t)
        {
            //ToolTipが表示されるまでの時間ms
            t.InitialDelay = 500;
            //ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            t.ReshowDelay = 500;
            //ToolTipを表示する時間
            t.AutoPopDelay = 10000;
            //フォームがアクティブでない時でもToolTipを表示する
            t.ShowAlways = true;
        }
        private void CreateToolTip(ParamControl p,string s)
        {
            ToolTip t = new ToolTip();
            SetDefToolTipProperties(t);

            //Button1とButton2にToolTipが表示されるようにする
            t.SetToolTip(p.PLabel, s);
            t.SetToolTip(p.PTextbox, s);
        }
        private void CreateToolTip(Control p,string s)
        {
            ToolTip t = new ToolTip();
            SetDefToolTipProperties(t);

            //Button1とButton2にToolTipが表示されるようにする
            t.SetToolTip(p, s);
        }
        //BTInpファイルを外部から読み込んでテキストボックスに格納
        public void ReadBTInpData(ref StreamReader sr)
        {
            //line1
            foreach (TextBox b in txts)
            {
                if (b.Name == "CommentBox")
                {
                    b.Text = sr.ReadLine();
                    break;
                }
            }
            //line2以降
            int NextStart = 0;
            int NextNum = line2param.Count();
            char[] delimiter = { ' ', '\t' };
            ReadDefNum(ref NextStart, line2param.Count(), ref sr);
            ReadDefNum(ref NextStart, line3param.Count(), ref sr);
            ReadDefNum(ref NextStart, line4param.Count(), ref sr);
            ReadDefNum(ref NextStart, line5param.Count(), ref sr);
            ReadDefNum(ref NextStart, line6param.Count(), ref sr);
            if (param[NextStart - line6param.Count() + 1].Value > 0)
                ReadDefNum(ref NextStart, line6aparam.Count(), ref sr);
            else
            {
                for (int i = 0; i < line6aparam.Count(); i++)
                {
                    param[NextStart + i].Text = "0";
                }
                NextStart += line6aparam.Count();
            }
            ReadDefNum(ref NextStart, line7param.Count(), ref sr);
            ReadDefNum(ref NextStart, line8param.Count(), ref sr);
            //Line9 ~ 11は特殊
            {
                string[] s = sr.ReadLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < s.Count(); i++)
                {
                    if (i == 0)
                        param[NextStart + i].Text = s[i];
                    else if (i == 1)
                    {
                        param[NextStart + 1].Text = s[i] + " ";
                        if (i != s.Count() - 1)
                            param[NextStart + 1].Text += " ";
                    }
                    else
                    {
                        param[NextStart + 1].Text +=s[i];
                        if (i != s.Count() - 1)
                            param[NextStart + 1].Text +=" ";
                    }
                }
                NextStart += line9param.Count();
            }
            {
                string[] s = sr.ReadLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < s.Count(); i++)
                {
                    if (i == 0)
                        param[NextStart + i].Text = s[i];
                    else if (i == 1)
                    {
                        param[NextStart + 1].Text = s[i] + " ";
                        if (i != s.Count() - 1)
                            param[NextStart + 1].Text += " ";
                    }
                    else
                    {
                        param[NextStart + 1].Text += s[i];
                        if (i != s.Count() - 1)
                            param[NextStart + 1].Text += " ";
                    }
                }
                NextStart += line10param.Count();
            }
            {
                string[] s = sr.ReadLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                int nwtf_num = int.Parse(s[0]);
                if (nwtf_num <= 0) nwtf_num = 1;
                int wtfs_num = int.Parse(s[1+nwtf_num]);
                if (wtfs_num <= 0) wtfs_num = 1;
                param[NextStart + 0].Text = s[0];
                for (int i = 1; i < 1+nwtf_num; i++)
                {
                    if (i == 1)
                        param[NextStart + 1].Text = s[i];
                    else
                        param[NextStart + 1].Text += s[i];
                }
                param[NextStart + 2].Text = s[1 + nwtf_num];
                for (int i = 1 + nwtf_num; i < 1 + nwtf_num + wtfs_num; i++)
                {
                    if (i == 1)
                        param[NextStart + 3].Text = s[i];
                    else
                        param[NextStart + 3].Text += s[i];
                }


                NextStart += line11param.Count();
            }

            //ここまで
            ReadDefNum(ref NextStart, line12param.Count(), ref sr);

        }
        //
        private int calcparamstartindex(int num)
        {
            int[] lineparams_count = { line6aparam.Count(),line2param.Count(),line3param.Count()
                                          ,line4param.Count(),line5param.Count(),line6param.Count(),line7param.Count()
                                          ,line8param.Count(),line9param.Count(),line10param.Count(),line11param.Count()
                                          ,line12param.Count()};
            if (num == 0) // i==0は6aとみなす
            {
                int sum = 0;
                for (int i = 1; i < 6; i++)
                {
                    sum += lineparams_count[i];
                }
                return sum;
            }
            if (num <= 6 - 1) //6行目まではそのまま
            {
                int sum = 0;
                for (int i = 1; i < num; i++)
                {
                    sum += lineparams_count[i];
                }
                return sum;
            }
            else if (num >= 7 - 1) //7行目以降は6aの存在を考慮してから足す
            {
                int sum = 0;
                for (int i = 0; i < num; i++)
                {
                    sum += lineparams_count[i];
                }
                return sum;
            } 
            return -1;
        }
        //ツールチップの生成
        public void CreateDefToolTips(ref StreamReader sr)
        {
            //line1
            foreach (TextBox b in txts)
            {
                if (b.Name == "CommentBox")
                {
                    CreateToolTip(b, "Comment(max : 79 character)");
                    break;
                }
            }
            List<int> lineparams_startindex = new List<int>();
            for(int i=0;i<13;i++)
            {
                lineparams_startindex.Add( calcparamstartindex(i ));
            }
            
            //line2
         

            int nowrefline = -1;
            int ref_param_num = 0;

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.IndexOf("Line") >= 0)
                {
                    int linenum = -1;
                    string linestr = line.Substring(line.IndexOf("Line") + 4, line.IndexOf("]") - (line.IndexOf("Line") + 4));
                    ref_param_num = 0;
                    if (int.TryParse(linestr, out linenum))
                    {
                        nowrefline = linenum - 1;
                    }
                    else if (linestr.IndexOf("6a") >= 0)
                        nowrefline = 0;
                    continue;
                }
                if (line.Trim() == "" || line.StartsWith("#"))
                    continue;

                //パラメータなら読み取り
                string paramname;
                string comment;
                paramname = line.Substring(0, line.IndexOf(",")).Trim();
                comment = line.Substring(line.IndexOf(",") + 1).Replace("\\n", Environment.NewLine);
                param[lineparams_startindex[nowrefline] + ref_param_num].Comment = comment;
                param[lineparams_startindex[nowrefline] + ref_param_num].Enter += ParamControl_GotFocus;
                CreateToolTip(param[lineparams_startindex[nowrefline] + ref_param_num], comment);
                ref_param_num++;
            }
        }
        private void ParamControl_GotFocus(object sender,EventArgs e)
        {
            ParamControl p = (ParamControl)sender;
            txt_comment.Text = p.Comment;
        }

        //内部用。うまく格納するための関数
        private void ReadDefNum(ref int NextStartPoint, int NextMaxNum, ref StreamReader sr)
        {
            char[] delimiter = { ' ', '\t' };
            string[] s = sr.ReadLine().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s.Count() && i+NextStartPoint < param.Count() ; i++)
            {
                param[NextStartPoint + i].Text = s[i];
            }
            NextStartPoint += NextMaxNum;
        }


        //Tab1,Line1～4の初期化
        private void InitializeTab1()
        {
            Size deflabelsize = new Size(35, 12);
            labels.Add( ControlFactory.Create<Label>(
                new Point(5,5) , deflabelsize ,  line_explanation[1] , "Comment" , 0 ) );
            tabPage1.Controls.Add(labels.Last());

            txts.Add(ControlFactory.Create<TextBox>(
                new Point(5, 25), new Size(460, 19), "", "CommentBox", 1));
            tabPage1.Controls.Add(txts.Last());

            int LineParamYStart = 55;
            int LineParamXInterval = 150;
            for (int i = 0; i < 3; i++)
            {
                labels.Add( ControlFactory.Create<Label>( new Point(LineParamXInterval*i, LineParamYStart) 
                    , deflabelsize , line_explanation[i+2],null,0));
                tabPage1.Controls.Add(labels.Last());
            }

            LineParamYStart += 25;
            for(int i=0;i<line2param.Count();i++)
            {
                param.Add(new ParamControl(0, LineParamYStart + i * 20, line2param[i]));
                //ifedのみ他の行へ影響
                if (line2param[i] == "mxpath")
                {
                    ((ParamControl)param.Last()).TextChanged += new EventHandler(this.MxpathTextChanged);
                }
                tabPage1.Controls.Add(param.Last());

            }
            for (int i = 0; i < line3param.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval, LineParamYStart + i * 20, line3param[i]));
                tabPage1.Controls.Add(param.Last());
            }
            for (int i = 0; i < line4param.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval * 2, LineParamYStart + i * 20, line4param[i]));
                tabPage1.Controls.Add(param.Last());
            }

        }
        private void IFedTextChanged(object sender,EventArgs e)
        {
            if( ((ParamControl)sender).Text == "0" || ((ParamControl)sender).Text == "1")
            {
                bool enable = ((ParamControl)sender).Text == "1";
                int start = calcparamstartindex(0);
                for (int i = 0; i < line6aparam.Count(); i++)
                {
                    param[i+start].Enabled = enable;
                }
            }
        }
        private void MxpathTextChanged(object sender, EventArgs e)
        {
            double v;
            if(double.TryParse( ((ParamControl)sender).Text,out v ))
            {
                bool enable = v<0;
                int start = calcparamstartindex(9);
                for (int i = 0; i < line10param.Count(); i++)
                {
                    param[i + start].Enabled = enable;
                }
            }     
        }
        //Tab2.Line5～7(6a含む)の初期化
        private void InitializeTab2()
        {
            Size deflabelsize = new Size(35,12);
            int LineParamYStart = 25;
            int LineParamXInterval = 150;
            for (int i = 0; i < 3; i++)
            {
                labels.Add( ControlFactory.Create<Label>( new Point(LineParamXInterval * i, LineParamYStart)
                    , deflabelsize , line_explanation[i+5] , null , 0 ) );
                tabPage2.Controls.Add(labels.Last());
            }
            LineParamYStart += 25;
            for (int i = 0; i < line5param.Count(); i++)
            {
                param.Add(new ParamControl(0, LineParamYStart + i * 20, line5param[i]));
                tabPage2.Controls.Add(param.Last());
            }
            for (int i = 0; i < line6param.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval, LineParamYStart + i * 20, line6param[i]));
                tabPage2.Controls.Add(param.Last());
                //ifedのみ他の行へ影響
                if( line6param[i] == "ifed" )
                {
                    ((ParamControl)param.Last()).TextChanged += new EventHandler(this.IFedTextChanged);
                }
            }
            //Line6a


            labels.Add(ControlFactory.Create<Label>(new Point(LineParamXInterval * 2, 5 + line7param.Count() * 20 + LineParamYStart)
                    , deflabelsize, line_explanation[0], null, 0));
            tabPage2.Controls.Add(labels.Last());

            for (int i = 0; i < line6aparam.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval * 2, 30 + line7param.Count() * 20 + LineParamYStart + i * 20, line6aparam[i]));
                tabPage2.Controls.Add(param.Last());
                param.Last().Enabled = false;
            }

            //
            for (int i = 0; i < line7param.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval * 2, LineParamYStart + i * 20, line7param[i]));
                if (line7param[i].Contains("osc"))
                    param.Last().Enabled = false;
                tabPage2.Controls.Add(param.Last());
            }



        }
        //Tab2.Line8～12の初期化
        private void InitializeTab3()
        {
            Size deflabelsize = new Size(35, 12);
            int LineParamYStart = 25;
            int LineParamXInterval = 150;


            labels.Add(ControlFactory.Create<Label>(new Point(0, LineParamYStart)
                , deflabelsize, line_explanation[8], null, 0));
            tabPage3.Controls.Add(labels.Last());
            LineParamYStart += 25;
            for (int i = 0; i < line8param.Count(); i++)
            {
                param.Add(new ParamControl(0, LineParamYStart + i * 20, line8param[i]));
                tabPage3.Controls.Add(param.Last());
            }

            //Line9は特殊
            #region line9control
            labels.Add(ControlFactory.Create<Label>(new Point(0, LineParamYStart + 20 * line8param.Count() + 25)
                    , deflabelsize, line_explanation[9], null, 0));
            tabPage3.Controls.Add(labels.Last());
            labels.Last().Enabled = false;
                param.Add(new ParamControl(0, LineParamYStart +  line8param.Count() * 20 + 50, line9param[0]));
            tabPage3.Controls.Add(param.Last());
            param.Last().Enabled = false;
            //長いやつ
            param.Add(new ParamControl(0, LineParamYStart +  line8param.Count() * 20 + 50+20 , line9param[1]));
            tabPage3.Controls.Add(param.Last());
            param.Last().Enabled = false;
            #endregion


            //Line10も特殊
            labels.Add(ControlFactory.Create<Label>(new Point(LineParamXInterval, LineParamYStart + 20 * 0 + 5)
                    , deflabelsize, line_explanation[10], null, 0));
            tabPage3.Controls.Add(labels.Last());

            param.Add(new ParamControl(LineParamXInterval , LineParamYStart + 20*0+30   , line10param[0]));
            tabPage3.Controls.Add(param.Last());
            //TODO:長いやつ
            param.Add(new ParamControl(LineParamXInterval , LineParamYStart + 20*1+30   , line10param[1]));
            tabPage3.Controls.Add(param.Last());

            //Line11も(ry
            labels.Add(ControlFactory.Create<Label>(new Point(LineParamXInterval, LineParamYStart + 20 * 4 + 5)
                    , deflabelsize, line_explanation[11], null, 0));
            tabPage3.Controls.Add(labels.Last());

            param.Add(new ParamControl(LineParamXInterval, LineParamYStart + 20 * 4 + 30, line11param[0]));
            tabPage3.Controls.Add(param.Last());
            //TODO:長いやつ
            param.Add(new ParamControl(LineParamXInterval, LineParamYStart + 20 * 5 + 30, line11param[1]));
            tabPage3.Controls.Add(param.Last());
            param.Add(new ParamControl(LineParamXInterval, LineParamYStart + 20 * 6 + 35, line11param[2]));
            tabPage3.Controls.Add(param.Last());
            //TODO:長いやつ
            param.Add(new ParamControl(LineParamXInterval, LineParamYStart + 20 * 7 + 35, line11param[3]));
            tabPage3.Controls.Add(param.Last());

            //Line12はLine8の下に持ってくる
            #region line12
            labels.Add(ControlFactory.Create<Label>(new Point(LineParamXInterval, LineParamYStart + 20 * 10 + 5)
                    , deflabelsize, line_explanation[12], null, 0));
            tabPage3.Controls.Add(labels.Last());
            
            for (int i = 0; i < line12param.Count(); i++)
            {
                param.Add(new ParamControl(LineParamXInterval, LineParamYStart + 20 * (i+10) + 35, line12param[i]));
                tabPage3.Controls.Add(param.Last());
                
            }
            #endregion


        }
        //ボタンとか
        private void InitializeButtons()
        {
            Size defsize = new Size(100,25);

            for (int i = 0; i < 3; i++)
            {
                bts.Add( ControlFactory.CreateButton( new Point(360, 430), defsize , 
                    "Create BTinp.dat","bt_BTinpCreate", new EventHandler(this.CreateBTinp),1));
                tabControl1.TabPages["tabPage" + (i + 1)].Controls.Add(bts.Last());

                bts.Add(ControlFactory.CreateButton(new Point(240, 430), defsize,
                    "Cancel", "bt_BTinpCancel", new EventHandler(this.Cancel), 1));
                tabControl1.TabPages["tabPage" + (i + 1)].Controls.Add(bts.Last());

                bts.Add(ControlFactory.CreateButton(new Point(120, 430), defsize,
                    "Read File", "bt_BTinpRead", new EventHandler(this.ReadFile), 1));
                tabControl1.TabPages["tabPage" + (i + 1)].Controls.Add(bts.Last());
            }
        }
        //内部用イベントハンドラ。ボタンなど
        private void CreateBTinp(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "dat file|*.dat";
            sfd.Title = "save BTinp.dat";
            sfd.FileName = "BTinp.dat";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = sfd.FileName;
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, BTMainForm.CharEncode))
                {
                    //line1
                    foreach (TextBox b in txts)
                    {
                        if (b.Name == "CommentBox")
                        {
                            if(b.Text.Length > 79)
                            {
                                sw.Write(b.Text.Substring(0,79) + "\n");
                                MessageBox.Show("Comment must be less than 79 characters");
                            }
                            else
                                sw.Write(b.Text + "\n");
                            break;
                        }
                    }
                    //line2～
                    //サイズで改行
                    int[] nextsize = new int[]{ line2param.Count(),line3param.Count(),line4param.Count()
                    ,line5param.Count(),line6param.Count(),line6aparam.Count(),line7param.Count(),line8param.Count()
                    ,line9param.Count(),line10param.Count(),line11param.Count(),line12param.Count()};
                    int next = nextsize[0];
                    int nextline = 1;
                    for (int i = 0; i < param.Count; i++)
                    {
                        sw.Write(param[i].ToString());
                        
                        if (i == next - 1)
                        {
                            sw.Write("\n");
                            if (nextline == 5&&param[next+1-line6param.Count()].Value==0)
                            {
                                i += nextsize[nextline];
                                next += nextsize[nextline];
                                nextline++;
                            }
                            if (nextline < nextsize.Count())
                            {
                                next += nextsize[nextline];
                                nextline++;
                            }
                        }
                        else
                            sw.Write(' ');
                    }
                }
                this.DialogResult = DialogResult.OK;
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
            ofd.FileName = "BTinp.dat";
            ofd.Filter = "dat file|*.dat";
            ofd.Title = "Read BTinp.dat from existing file";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, BTMainForm.CharEncode);
                filename = ofd.FileName;
                ReadBTInpData(ref sr);
                sr.Close();
            }
        }
    }
}
