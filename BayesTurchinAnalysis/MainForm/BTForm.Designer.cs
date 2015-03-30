namespace BayesTurchinAnalysis
{
    partial class BTMainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TabAnalysis = new System.Windows.Forms.TabPage();
            this.gb_btanalysis = new System.Windows.Forms.GroupBox();
            this.bt_smoothfit = new System.Windows.Forms.Button();
            this.bt_allprocess = new System.Windows.Forms.Button();
            this.bt_runbt = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.bt_btinpcreate = new System.Windows.Forms.Button();
            this.rtb_btinp = new BTSubControl.RefTextBox();
            this.gb_expdata = new System.Windows.Forms.GroupBox();
            this.bt_runexpxmooth = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.bt_prepareexpdata = new System.Windows.Forms.Button();
            this.bt_expsmoothcreate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rtb_expsmoothinp = new BTSubControl.RefTextBox();
            this.rtb_modexpdata = new BTSubControl.RefTextBox();
            this.chk_expdata = new System.Windows.Forms.CheckBox();
            this.gb_feff = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.bt_runfeff = new System.Windows.Forms.Button();
            this.txt_criteria_step = new System.Windows.Forms.TextBox();
            this.cb_feffselectrunmode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cb_fefftargetedge = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_criteria_end = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_criteria_start = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_feffsmoothcreate = new System.Windows.Forms.Button();
            this.rtb_feffsmoothinp = new BTSubControl.RefTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_feffinp = new BTSubControl.RefTextBox();
            this.chk_feff = new System.Windows.Forms.CheckBox();
            this.gb_workdir = new System.Windows.Forms.GroupBox();
            this.setdeffilepath = new System.Windows.Forms.Button();
            this.rtb_workdir = new BTSubControl.RefTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_difference = new System.Windows.Forms.Button();
            this.cb_smoothplot = new System.Windows.Forms.ComboBox();
            this.bt_smooth = new System.Windows.Forms.Button();
            this.bt_alpha = new System.Windows.Forms.Button();
            this.txt_alphanum = new System.Windows.Forms.TextBox();
            this.bt_eigenvalue = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.bt_showfolder = new System.Windows.Forms.Button();
            this.gb_graph = new System.Windows.Forms.GroupBox();
            this.bt_paramtable = new System.Windows.Forms.Button();
            this.bt_grothergr = new System.Windows.Forms.Button();
            this.bt_grsn2 = new System.Windows.Forms.Button();
            this.bt_grmatrix = new System.Windows.Forms.Button();
            this.bt_grrdf = new System.Windows.Forms.Button();
            this.bt_grfinal = new System.Windows.Forms.Button();
            this.bt_grinitial = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideLogWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllSubWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.TabAnalysis.SuspendLayout();
            this.gb_btanalysis.SuspendLayout();
            this.gb_expdata.SuspendLayout();
            this.gb_feff.SuspendLayout();
            this.gb_workdir.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gb_graph.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TabAnalysis);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(638, 500);
            this.tabControl.TabIndex = 0;
            // 
            // TabAnalysis
            // 
            this.TabAnalysis.Controls.Add(this.gb_btanalysis);
            this.TabAnalysis.Controls.Add(this.gb_expdata);
            this.TabAnalysis.Controls.Add(this.gb_feff);
            this.TabAnalysis.Controls.Add(this.gb_workdir);
            this.TabAnalysis.Location = new System.Drawing.Point(4, 22);
            this.TabAnalysis.Name = "TabAnalysis";
            this.TabAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.TabAnalysis.Size = new System.Drawing.Size(630, 474);
            this.TabAnalysis.TabIndex = 0;
            this.TabAnalysis.Text = "Analysis";
            this.TabAnalysis.UseVisualStyleBackColor = true;
            // 
            // gb_btanalysis
            // 
            this.gb_btanalysis.Controls.Add(this.bt_smoothfit);
            this.gb_btanalysis.Controls.Add(this.bt_allprocess);
            this.gb_btanalysis.Controls.Add(this.bt_runbt);
            this.gb_btanalysis.Controls.Add(this.label7);
            this.gb_btanalysis.Controls.Add(this.bt_btinpcreate);
            this.gb_btanalysis.Controls.Add(this.rtb_btinp);
            this.gb_btanalysis.Location = new System.Drawing.Point(9, 360);
            this.gb_btanalysis.Name = "gb_btanalysis";
            this.gb_btanalysis.Size = new System.Drawing.Size(614, 104);
            this.gb_btanalysis.TabIndex = 3;
            this.gb_btanalysis.TabStop = false;
            this.gb_btanalysis.Text = "Bayes-Turchin Analysis";
            // 
            // bt_smoothfit
            // 
            this.bt_smoothfit.Location = new System.Drawing.Point(9, 58);
            this.bt_smoothfit.Name = "bt_smoothfit";
            this.bt_smoothfit.Size = new System.Drawing.Size(75, 38);
            this.bt_smoothfit.TabIndex = 17;
            this.bt_smoothfit.Text = "smooth fit";
            this.bt_smoothfit.UseVisualStyleBackColor = true;
            this.bt_smoothfit.Click += new System.EventHandler(this.bt_smoothfit_Click);
            // 
            // bt_allprocess
            // 
            this.bt_allprocess.Location = new System.Drawing.Point(520, 58);
            this.bt_allprocess.Name = "bt_allprocess";
            this.bt_allprocess.Size = new System.Drawing.Size(85, 38);
            this.bt_allprocess.TabIndex = 16;
            this.bt_allprocess.Text = "run all processes";
            this.bt_allprocess.UseVisualStyleBackColor = true;
            this.bt_allprocess.Click += new System.EventHandler(this.bt_allprocess_Click);
            // 
            // bt_runbt
            // 
            this.bt_runbt.Location = new System.Drawing.Point(520, 6);
            this.bt_runbt.Name = "bt_runbt";
            this.bt_runbt.Size = new System.Drawing.Size(85, 46);
            this.bt_runbt.TabIndex = 15;
            this.bt_runbt.Text = "run BT analysis";
            this.bt_runbt.UseVisualStyleBackColor = true;
            this.bt_runbt.Click += new System.EventHandler(this.bt_runbt_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "BTinp.dat";
            // 
            // bt_btinpcreate
            // 
            this.bt_btinpcreate.Location = new System.Drawing.Point(440, 18);
            this.bt_btinpcreate.Name = "bt_btinpcreate";
            this.bt_btinpcreate.Size = new System.Drawing.Size(75, 23);
            this.bt_btinpcreate.TabIndex = 14;
            this.bt_btinpcreate.Text = "create";
            this.bt_btinpcreate.UseVisualStyleBackColor = true;
            this.bt_btinpcreate.Click += new System.EventHandler(this.bt_btinpcreate_Click);
            // 
            // rtb_btinp
            // 
            this.rtb_btinp.Filter = "All File|*.*";
            this.rtb_btinp.FolderFlag = false;
            this.rtb_btinp.Location = new System.Drawing.Point(104, 18);
            this.rtb_btinp.Name = "rtb_btinp";
            this.rtb_btinp.Size = new System.Drawing.Size(330, 25);
            this.rtb_btinp.TabIndex = 0;
            // 
            // gb_expdata
            // 
            this.gb_expdata.Controls.Add(this.bt_runexpxmooth);
            this.gb_expdata.Controls.Add(this.label6);
            this.gb_expdata.Controls.Add(this.bt_prepareexpdata);
            this.gb_expdata.Controls.Add(this.bt_expsmoothcreate);
            this.gb_expdata.Controls.Add(this.label5);
            this.gb_expdata.Controls.Add(this.rtb_expsmoothinp);
            this.gb_expdata.Controls.Add(this.rtb_modexpdata);
            this.gb_expdata.Controls.Add(this.chk_expdata);
            this.gb_expdata.Location = new System.Drawing.Point(9, 270);
            this.gb_expdata.Name = "gb_expdata";
            this.gb_expdata.Size = new System.Drawing.Size(614, 86);
            this.gb_expdata.TabIndex = 2;
            this.gb_expdata.TabStop = false;
            // 
            // bt_runexpxmooth
            // 
            this.bt_runexpxmooth.Location = new System.Drawing.Point(521, 37);
            this.bt_runexpxmooth.Name = "bt_runexpxmooth";
            this.bt_runexpxmooth.Size = new System.Drawing.Size(84, 37);
            this.bt_runexpxmooth.TabIndex = 14;
            this.bt_runexpxmooth.Text = "run exp smooth";
            this.bt_runexpxmooth.UseVisualStyleBackColor = true;
            this.bt_runexpxmooth.Click += new System.EventHandler(this.bt_runexpxmooth_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "smooth.inp";
            // 
            // bt_prepareexpdata
            // 
            this.bt_prepareexpdata.Location = new System.Drawing.Point(440, 18);
            this.bt_prepareexpdata.Name = "bt_prepareexpdata";
            this.bt_prepareexpdata.Size = new System.Drawing.Size(75, 23);
            this.bt_prepareexpdata.TabIndex = 11;
            this.bt_prepareexpdata.Text = "prepare";
            this.bt_prepareexpdata.UseVisualStyleBackColor = true;
            this.bt_prepareexpdata.Click += new System.EventHandler(this.bt_prepareexpdata_Click);
            // 
            // bt_expsmoothcreate
            // 
            this.bt_expsmoothcreate.Location = new System.Drawing.Point(440, 50);
            this.bt_expsmoothcreate.Name = "bt_expsmoothcreate";
            this.bt_expsmoothcreate.Size = new System.Drawing.Size(75, 23);
            this.bt_expsmoothcreate.TabIndex = 12;
            this.bt_expsmoothcreate.Text = "create";
            this.bt_expsmoothcreate.UseVisualStyleBackColor = true;
            this.bt_expsmoothcreate.Click += new System.EventHandler(this.bt_expsmoothcreate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Modified Experimental Data";
            // 
            // rtb_expsmoothinp
            // 
            this.rtb_expsmoothinp.Filter = "All File|*.*";
            this.rtb_expsmoothinp.FolderFlag = false;
            this.rtb_expsmoothinp.Location = new System.Drawing.Point(104, 49);
            this.rtb_expsmoothinp.Name = "rtb_expsmoothinp";
            this.rtb_expsmoothinp.Size = new System.Drawing.Size(330, 25);
            this.rtb_expsmoothinp.TabIndex = 11;
            this.rtb_expsmoothinp.RtbTextChanged += new System.EventHandler(this.rtb_modexpdata_RtbTextChanged);
            // 
            // rtb_modexpdata
            // 
            this.rtb_modexpdata.Filter = "All File|*.*";
            this.rtb_modexpdata.FolderFlag = false;
            this.rtb_modexpdata.Location = new System.Drawing.Point(161, 18);
            this.rtb_modexpdata.Name = "rtb_modexpdata";
            this.rtb_modexpdata.Size = new System.Drawing.Size(273, 25);
            this.rtb_modexpdata.TabIndex = 1;
            this.rtb_modexpdata.RtbTextChanged += new System.EventHandler(this.rtb_modexpdata_RtbTextChanged);
            // 
            // chk_expdata
            // 
            this.chk_expdata.AutoCheck = false;
            this.chk_expdata.AutoSize = true;
            this.chk_expdata.BackColor = System.Drawing.Color.White;
            this.chk_expdata.Location = new System.Drawing.Point(7, 0);
            this.chk_expdata.Name = "chk_expdata";
            this.chk_expdata.Size = new System.Drawing.Size(118, 16);
            this.chk_expdata.TabIndex = 0;
            this.chk_expdata.Text = "Experimental Data";
            this.chk_expdata.UseVisualStyleBackColor = false;
            // 
            // gb_feff
            // 
            this.gb_feff.Controls.Add(this.label11);
            this.gb_feff.Controls.Add(this.bt_runfeff);
            this.gb_feff.Controls.Add(this.txt_criteria_step);
            this.gb_feff.Controls.Add(this.cb_feffselectrunmode);
            this.gb_feff.Controls.Add(this.label10);
            this.gb_feff.Controls.Add(this.cb_fefftargetedge);
            this.gb_feff.Controls.Add(this.label9);
            this.gb_feff.Controls.Add(this.label4);
            this.gb_feff.Controls.Add(this.txt_criteria_end);
            this.gb_feff.Controls.Add(this.label3);
            this.gb_feff.Controls.Add(this.label8);
            this.gb_feff.Controls.Add(this.txt_criteria_start);
            this.gb_feff.Controls.Add(this.label2);
            this.gb_feff.Controls.Add(this.bt_feffsmoothcreate);
            this.gb_feff.Controls.Add(this.rtb_feffsmoothinp);
            this.gb_feff.Controls.Add(this.label1);
            this.gb_feff.Controls.Add(this.rtb_feffinp);
            this.gb_feff.Controls.Add(this.chk_feff);
            this.gb_feff.Location = new System.Drawing.Point(9, 69);
            this.gb_feff.Name = "gb_feff";
            this.gb_feff.Size = new System.Drawing.Size(614, 175);
            this.gb_feff.TabIndex = 1;
            this.gb_feff.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(293, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "step";
            // 
            // bt_runfeff
            // 
            this.bt_runfeff.Location = new System.Drawing.Point(520, 128);
            this.bt_runfeff.Name = "bt_runfeff";
            this.bt_runfeff.Size = new System.Drawing.Size(85, 32);
            this.bt_runfeff.TabIndex = 10;
            this.bt_runfeff.Text = "run FEFF";
            this.bt_runfeff.UseVisualStyleBackColor = true;
            this.bt_runfeff.Click += new System.EventHandler(this.bt_runfeff_Click);
            // 
            // txt_criteria_step
            // 
            this.txt_criteria_step.Enabled = false;
            this.txt_criteria_step.Location = new System.Drawing.Point(234, 145);
            this.txt_criteria_step.Name = "txt_criteria_step";
            this.txt_criteria_step.Size = new System.Drawing.Size(53, 19);
            this.txt_criteria_step.TabIndex = 21;
            this.txt_criteria_step.Text = "0.03";
            // 
            // cb_feffselectrunmode
            // 
            this.cb_feffselectrunmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_feffselectrunmode.FormattingEnabled = true;
            this.cb_feffselectrunmode.Items.AddRange(new object[] {
            "All Process",
            "Feff(normal/deformed/criteria)",
            "Chimod",
            "Smooth",
            "Feff + Chimod",
            "Chimod + Smooth",
            "Feff(normal/deformed)",
            "Feff(criteria)"});
            this.cb_feffselectrunmode.Location = new System.Drawing.Point(104, 112);
            this.cb_feffselectrunmode.Name = "cb_feffselectrunmode";
            this.cb_feffselectrunmode.Size = new System.Drawing.Size(196, 20);
            this.cb_feffselectrunmode.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(211, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "by";
            // 
            // cb_fefftargetedge
            // 
            this.cb_fefftargetedge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_fefftargetedge.FormattingEnabled = true;
            this.cb_fefftargetedge.Items.AddRange(new object[] {
            "K edge",
            "L edge"});
            this.cb_fefftargetedge.Location = new System.Drawing.Point(104, 82);
            this.cb_fefftargetedge.Name = "cb_fefftargetedge";
            this.cb_fefftargetedge.Size = new System.Drawing.Size(121, 20);
            this.cb_fefftargetedge.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(131, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "select run mode";
            // 
            // txt_criteria_end
            // 
            this.txt_criteria_end.Location = new System.Drawing.Point(152, 145);
            this.txt_criteria_end.Name = "txt_criteria_end";
            this.txt_criteria_end.Size = new System.Drawing.Size(53, 19);
            this.txt_criteria_end.TabIndex = 18;
            this.txt_criteria_end.Text = "0.3";
            this.txt_criteria_end.TextChanged += new System.EventHandler(this.txt_criteria_changed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "target edge";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "CRITERIA";
            // 
            // txt_criteria_start
            // 
            this.txt_criteria_start.Location = new System.Drawing.Point(72, 145);
            this.txt_criteria_start.Name = "txt_criteria_start";
            this.txt_criteria_start.Size = new System.Drawing.Size(53, 19);
            this.txt_criteria_start.TabIndex = 16;
            this.txt_criteria_start.Text = "1.5";
            this.txt_criteria_start.TextChanged += new System.EventHandler(this.txt_criteria_changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "smooth.inp";
            // 
            // bt_feffsmoothcreate
            // 
            this.bt_feffsmoothcreate.Location = new System.Drawing.Point(440, 50);
            this.bt_feffsmoothcreate.Name = "bt_feffsmoothcreate";
            this.bt_feffsmoothcreate.Size = new System.Drawing.Size(75, 23);
            this.bt_feffsmoothcreate.TabIndex = 4;
            this.bt_feffsmoothcreate.Text = "create";
            this.bt_feffsmoothcreate.UseVisualStyleBackColor = true;
            this.bt_feffsmoothcreate.Click += new System.EventHandler(this.bt_feffsmoothcreate_Click);
            // 
            // rtb_feffsmoothinp
            // 
            this.rtb_feffsmoothinp.Filter = "All File|*.*";
            this.rtb_feffsmoothinp.FolderFlag = false;
            this.rtb_feffsmoothinp.Location = new System.Drawing.Point(104, 49);
            this.rtb_feffsmoothinp.Name = "rtb_feffsmoothinp";
            this.rtb_feffsmoothinp.Size = new System.Drawing.Size(330, 25);
            this.rtb_feffsmoothinp.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "feff.inp path";
            // 
            // rtb_feffinp
            // 
            this.rtb_feffinp.Filter = "All File|*.*";
            this.rtb_feffinp.FolderFlag = false;
            this.rtb_feffinp.Location = new System.Drawing.Point(104, 18);
            this.rtb_feffinp.Name = "rtb_feffinp";
            this.rtb_feffinp.Size = new System.Drawing.Size(330, 25);
            this.rtb_feffinp.TabIndex = 1;
            // 
            // chk_feff
            // 
            this.chk_feff.AutoSize = true;
            this.chk_feff.BackColor = System.Drawing.Color.White;
            this.chk_feff.Location = new System.Drawing.Point(6, 0);
            this.chk_feff.Name = "chk_feff";
            this.chk_feff.Size = new System.Drawing.Size(45, 16);
            this.chk_feff.TabIndex = 0;
            this.chk_feff.Text = "Feff";
            this.chk_feff.UseVisualStyleBackColor = false;
            // 
            // gb_workdir
            // 
            this.gb_workdir.Controls.Add(this.setdeffilepath);
            this.gb_workdir.Controls.Add(this.rtb_workdir);
            this.gb_workdir.Location = new System.Drawing.Point(9, 7);
            this.gb_workdir.Name = "gb_workdir";
            this.gb_workdir.Size = new System.Drawing.Size(655, 55);
            this.gb_workdir.TabIndex = 0;
            this.gb_workdir.TabStop = false;
            this.gb_workdir.Text = "WorkDirectory";
            // 
            // setdeffilepath
            // 
            this.setdeffilepath.Location = new System.Drawing.Point(458, 18);
            this.setdeffilepath.Name = "setdeffilepath";
            this.setdeffilepath.Size = new System.Drawing.Size(75, 23);
            this.setdeffilepath.TabIndex = 2;
            this.setdeffilepath.Text = "SetFilesDef";
            this.setdeffilepath.UseVisualStyleBackColor = true;
            this.setdeffilepath.Click += new System.EventHandler(this.setdeffilepath_Click);
            // 
            // rtb_workdir
            // 
            this.rtb_workdir.Filter = "All File|*.*";
            this.rtb_workdir.FolderFlag = false;
            this.rtb_workdir.Location = new System.Drawing.Point(6, 18);
            this.rtb_workdir.Name = "rtb_workdir";
            this.rtb_workdir.Size = new System.Drawing.Size(359, 25);
            this.rtb_workdir.TabIndex = 1;
            this.rtb_workdir.RtbTextChanged += new System.EventHandler(this.rtb_workdir_RtbTextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.bt_showfolder);
            this.tabPage2.Controls.Add(this.gb_graph);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(630, 474);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TabResult";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_difference);
            this.groupBox1.Controls.Add(this.cb_smoothplot);
            this.groupBox1.Controls.Add(this.bt_smooth);
            this.groupBox1.Controls.Add(this.bt_alpha);
            this.groupBox1.Controls.Add(this.txt_alphanum);
            this.groupBox1.Controls.Add(this.bt_eigenvalue);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(312, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 246);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "other datas";
            // 
            // bt_difference
            // 
            this.bt_difference.Location = new System.Drawing.Point(19, 124);
            this.bt_difference.Name = "bt_difference";
            this.bt_difference.Size = new System.Drawing.Size(132, 40);
            this.bt_difference.TabIndex = 12;
            this.bt_difference.Text = "parameter difference";
            this.bt_difference.UseVisualStyleBackColor = true;
            this.bt_difference.Click += new System.EventHandler(this.bt_difference_Click);
            // 
            // cb_smoothplot
            // 
            this.cb_smoothplot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_smoothplot.FormattingEnabled = true;
            this.cb_smoothplot.Items.AddRange(new object[] {
            "Feff Smooth Before/After",
            "Experiment Smooth Before/After",
            "Feff/Experiment Smooth After"});
            this.cb_smoothplot.Location = new System.Drawing.Point(19, 27);
            this.cb_smoothplot.Name = "cb_smoothplot";
            this.cb_smoothplot.Size = new System.Drawing.Size(180, 20);
            this.cb_smoothplot.TabIndex = 11;
            // 
            // bt_smooth
            // 
            this.bt_smooth.Location = new System.Drawing.Point(205, 16);
            this.bt_smooth.Name = "bt_smooth";
            this.bt_smooth.Size = new System.Drawing.Size(75, 40);
            this.bt_smooth.TabIndex = 10;
            this.bt_smooth.Text = "smooth";
            this.bt_smooth.UseVisualStyleBackColor = true;
            this.bt_smooth.Click += new System.EventHandler(this.bt_smooth_Click);
            // 
            // bt_alpha
            // 
            this.bt_alpha.Location = new System.Drawing.Point(205, 69);
            this.bt_alpha.Name = "bt_alpha";
            this.bt_alpha.Size = new System.Drawing.Size(75, 40);
            this.bt_alpha.TabIndex = 6;
            this.bt_alpha.Text = "alpha";
            this.bt_alpha.UseVisualStyleBackColor = true;
            this.bt_alpha.Click += new System.EventHandler(this.bt_alpha_Click);
            // 
            // txt_alphanum
            // 
            this.txt_alphanum.Location = new System.Drawing.Point(148, 90);
            this.txt_alphanum.Name = "txt_alphanum";
            this.txt_alphanum.Size = new System.Drawing.Size(48, 19);
            this.txt_alphanum.TabIndex = 9;
            this.txt_alphanum.Text = "0";
            // 
            // bt_eigenvalue
            // 
            this.bt_eigenvalue.Location = new System.Drawing.Point(19, 69);
            this.bt_eigenvalue.Name = "bt_eigenvalue";
            this.bt_eigenvalue.Size = new System.Drawing.Size(75, 40);
            this.bt_eigenvalue.TabIndex = 7;
            this.bt_eigenvalue.Text = "eigenValue";
            this.bt_eigenvalue.UseVisualStyleBackColor = true;
            this.bt_eigenvalue.Click += new System.EventHandler(this.bt_eigenvalue_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(139, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "alpha_num";
            // 
            // bt_showfolder
            // 
            this.bt_showfolder.Location = new System.Drawing.Point(15, 298);
            this.bt_showfolder.Name = "bt_showfolder";
            this.bt_showfolder.Size = new System.Drawing.Size(223, 23);
            this.bt_showfolder.TabIndex = 2;
            this.bt_showfolder.Text = "show result data folder";
            this.bt_showfolder.UseVisualStyleBackColor = true;
            this.bt_showfolder.Click += new System.EventHandler(this.bt_showfolder_Click);
            // 
            // gb_graph
            // 
            this.gb_graph.Controls.Add(this.bt_paramtable);
            this.gb_graph.Controls.Add(this.bt_grothergr);
            this.gb_graph.Controls.Add(this.bt_grsn2);
            this.gb_graph.Controls.Add(this.bt_grmatrix);
            this.gb_graph.Controls.Add(this.bt_grrdf);
            this.gb_graph.Controls.Add(this.bt_grfinal);
            this.gb_graph.Controls.Add(this.bt_grinitial);
            this.gb_graph.Location = new System.Drawing.Point(8, 6);
            this.gb_graph.Name = "gb_graph";
            this.gb_graph.Size = new System.Drawing.Size(298, 246);
            this.gb_graph.TabIndex = 1;
            this.gb_graph.TabStop = false;
            this.gb_graph.Text = "Parameters";
            // 
            // bt_paramtable
            // 
            this.bt_paramtable.Location = new System.Drawing.Point(7, 181);
            this.bt_paramtable.Name = "bt_paramtable";
            this.bt_paramtable.Size = new System.Drawing.Size(124, 40);
            this.bt_paramtable.TabIndex = 6;
            this.bt_paramtable.Text = "parameter values";
            this.bt_paramtable.UseVisualStyleBackColor = true;
            this.bt_paramtable.Click += new System.EventHandler(this.bt_paramtable_Click);
            // 
            // bt_grothergr
            // 
            this.bt_grothergr.Location = new System.Drawing.Point(193, 16);
            this.bt_grothergr.Name = "bt_grothergr";
            this.bt_grothergr.Size = new System.Drawing.Size(75, 40);
            this.bt_grothergr.TabIndex = 5;
            this.bt_grothergr.Text = "other";
            this.bt_grothergr.UseVisualStyleBackColor = true;
            this.bt_grothergr.Click += new System.EventHandler(this.bt_grothergr_Click);
            // 
            // bt_grsn2
            // 
            this.bt_grsn2.Location = new System.Drawing.Point(100, 69);
            this.bt_grsn2.Name = "bt_grsn2";
            this.bt_grsn2.Size = new System.Drawing.Size(75, 40);
            this.bt_grsn2.TabIndex = 4;
            this.bt_grsn2.Text = "sn2";
            this.bt_grsn2.UseVisualStyleBackColor = true;
            this.bt_grsn2.Click += new System.EventHandler(this.bt_grsn2_Click);
            // 
            // bt_grmatrix
            // 
            this.bt_grmatrix.Location = new System.Drawing.Point(7, 69);
            this.bt_grmatrix.Name = "bt_grmatrix";
            this.bt_grmatrix.Size = new System.Drawing.Size(75, 40);
            this.bt_grmatrix.TabIndex = 3;
            this.bt_grmatrix.Text = "correlation matrix";
            this.bt_grmatrix.UseVisualStyleBackColor = true;
            this.bt_grmatrix.Click += new System.EventHandler(this.bt_grmatrix_Click);
            // 
            // bt_grrdf
            // 
            this.bt_grrdf.Location = new System.Drawing.Point(7, 124);
            this.bt_grrdf.Name = "bt_grrdf";
            this.bt_grrdf.Size = new System.Drawing.Size(75, 40);
            this.bt_grrdf.TabIndex = 2;
            this.bt_grrdf.Text = "RDF";
            this.bt_grrdf.UseVisualStyleBackColor = true;
            this.bt_grrdf.Click += new System.EventHandler(this.bt_grrdf_Click);
            // 
            // bt_grfinal
            // 
            this.bt_grfinal.Location = new System.Drawing.Point(100, 16);
            this.bt_grfinal.Name = "bt_grfinal";
            this.bt_grfinal.Size = new System.Drawing.Size(75, 40);
            this.bt_grfinal.TabIndex = 1;
            this.bt_grfinal.Text = "a posteriori";
            this.bt_grfinal.UseVisualStyleBackColor = true;
            this.bt_grfinal.Click += new System.EventHandler(this.bt_grfinal_Click);
            // 
            // bt_grinitial
            // 
            this.bt_grinitial.Location = new System.Drawing.Point(7, 16);
            this.bt_grinitial.Name = "bt_grinitial";
            this.bt_grinitial.Size = new System.Drawing.Size(75, 40);
            this.bt_grinitial.TabIndex = 0;
            this.bt_grinitial.Text = "a priori";
            this.bt_grinitial.UseVisualStyleBackColor = true;
            this.bt_grinitial.Click += new System.EventHandler(this.bt_grinitial_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(636, 26);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideLogWindowToolStripMenuItem,
            this.preferenceToolStripMenuItem,
            this.closeAllSubWindowToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(51, 22);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // showHideLogWindowToolStripMenuItem
            // 
            this.showHideLogWindowToolStripMenuItem.Name = "showHideLogWindowToolStripMenuItem";
            this.showHideLogWindowToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.showHideLogWindowToolStripMenuItem.Text = "Show/Hide Log Window";
            this.showHideLogWindowToolStripMenuItem.Click += new System.EventHandler(this.showHideLogWindowToolStripMenuItem_Click);
            // 
            // preferenceToolStripMenuItem
            // 
            this.preferenceToolStripMenuItem.Name = "preferenceToolStripMenuItem";
            this.preferenceToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.preferenceToolStripMenuItem.Text = "Preference";
            this.preferenceToolStripMenuItem.Click += new System.EventHandler(this.preferenceToolStripMenuItem_Click);
            // 
            // closeAllSubWindowToolStripMenuItem
            // 
            this.closeAllSubWindowToolStripMenuItem.Name = "closeAllSubWindowToolStripMenuItem";
            this.closeAllSubWindowToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.closeAllSubWindowToolStripMenuItem.Text = "Close All Sub Window";
            // 
            // BTMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 528);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "BTMainForm";
            this.Text = "Bayes-Turchin Analysis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BTMainForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.TabAnalysis.ResumeLayout(false);
            this.gb_btanalysis.ResumeLayout(false);
            this.gb_btanalysis.PerformLayout();
            this.gb_expdata.ResumeLayout(false);
            this.gb_expdata.PerformLayout();
            this.gb_feff.ResumeLayout(false);
            this.gb_feff.PerformLayout();
            this.gb_workdir.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb_graph.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage TabAnalysis;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.GroupBox gb_workdir;
        private BTSubControl.RefTextBox rtb_workdir;
        private System.Windows.Forms.GroupBox gb_feff;
        private System.Windows.Forms.Button bt_runfeff;
        private System.Windows.Forms.ComboBox cb_feffselectrunmode;
        private System.Windows.Forms.ComboBox cb_fefftargetedge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_feffsmoothcreate;
        private BTSubControl.RefTextBox rtb_feffsmoothinp;
        private System.Windows.Forms.Label label1;
        private BTSubControl.RefTextBox rtb_feffinp;
        private System.Windows.Forms.CheckBox chk_feff;
        private System.Windows.Forms.GroupBox gb_expdata;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bt_prepareexpdata;
        private System.Windows.Forms.Button bt_expsmoothcreate;
        private System.Windows.Forms.Label label5;
        private BTSubControl.RefTextBox rtb_expsmoothinp;
        private BTSubControl.RefTextBox rtb_modexpdata;
        private System.Windows.Forms.CheckBox chk_expdata;
        private System.Windows.Forms.GroupBox gb_btanalysis;
        private System.Windows.Forms.Button bt_runbt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bt_btinpcreate;
        private BTSubControl.RefTextBox rtb_btinp;
        private System.Windows.Forms.GroupBox gb_graph;
        private System.Windows.Forms.Button bt_grothergr;
        private System.Windows.Forms.Button bt_grsn2;
        private System.Windows.Forms.Button bt_grmatrix;
        private System.Windows.Forms.Button bt_grrdf;
        private System.Windows.Forms.Button bt_grfinal;
        private System.Windows.Forms.Button bt_grinitial;
        private System.Windows.Forms.Button setdeffilepath;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideLogWindowToolStripMenuItem;
        private System.Windows.Forms.Button bt_showfolder;
        private System.Windows.Forms.ToolStripMenuItem preferenceToolStripMenuItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_criteria_step;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_criteria_end;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_criteria_start;
        private System.Windows.Forms.Button bt_eigenvalue;
        private System.Windows.Forms.Button bt_alpha;
        private System.Windows.Forms.TextBox txt_alphanum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button bt_smooth;
        private System.Windows.Forms.Button bt_allprocess;
        private System.Windows.Forms.Button bt_runexpxmooth;
        private System.Windows.Forms.ComboBox cb_smoothplot;
        private System.Windows.Forms.Button bt_smoothfit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_difference;
        private System.Windows.Forms.Button bt_paramtable;
        private System.Windows.Forms.ToolStripMenuItem closeAllSubWindowToolStripMenuItem;
    }
}

