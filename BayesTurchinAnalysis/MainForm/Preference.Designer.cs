namespace BayesTurchinAnalysis
{
    partial class PreferenceWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_saveanalysissetting = new System.Windows.Forms.CheckBox();
            this.rtb_feff = new BTSubControl.RefTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtb_chimod = new BTSubControl.RefTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtb_smooth = new BTSubControl.RefTextBox();
            this.bt_apply = new System.Windows.Forms.Button();
            this.cb_saveworkdir = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtb_chimod12d = new BTSubControl.RefTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_default = new System.Windows.Forms.Button();
            this.cb_showsmoothgraph = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rtb_mu0alph = new BTSubControl.RefTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_saveanalysissetting
            // 
            this.cb_saveanalysissetting.AutoSize = true;
            this.cb_saveanalysissetting.Location = new System.Drawing.Point(13, 13);
            this.cb_saveanalysissetting.Name = "cb_saveanalysissetting";
            this.cb_saveanalysissetting.Size = new System.Drawing.Size(267, 16);
            this.cb_saveanalysissetting.TabIndex = 0;
            this.cb_saveanalysissetting.Text = "Save used analysis settings(including file path)";
            this.cb_saveanalysissetting.UseVisualStyleBackColor = true;
            // 
            // rtb_feff
            // 
            this.rtb_feff.Filter = "All File|*.*";
            this.rtb_feff.FolderFlag = false;
            this.rtb_feff.Location = new System.Drawing.Point(96, 18);
            this.rtb_feff.Name = "rtb_feff";
            this.rtb_feff.Size = new System.Drawing.Size(275, 25);
            this.rtb_feff.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Feff path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Chimod12 path";
            // 
            // rtb_chimod
            // 
            this.rtb_chimod.Filter = "All File|*.*";
            this.rtb_chimod.FolderFlag = false;
            this.rtb_chimod.Location = new System.Drawing.Point(96, 49);
            this.rtb_chimod.Name = "rtb_chimod";
            this.rtb_chimod.Size = new System.Drawing.Size(275, 25);
            this.rtb_chimod.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Smooth path";
            // 
            // rtb_smooth
            // 
            this.rtb_smooth.Filter = "All File|*.*";
            this.rtb_smooth.FolderFlag = false;
            this.rtb_smooth.Location = new System.Drawing.Point(96, 110);
            this.rtb_smooth.Name = "rtb_smooth";
            this.rtb_smooth.Size = new System.Drawing.Size(275, 25);
            this.rtb_smooth.TabIndex = 5;
            // 
            // bt_apply
            // 
            this.bt_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_apply.Location = new System.Drawing.Point(311, 287);
            this.bt_apply.Name = "bt_apply";
            this.bt_apply.Size = new System.Drawing.Size(75, 35);
            this.bt_apply.TabIndex = 7;
            this.bt_apply.Text = "apply";
            this.bt_apply.UseVisualStyleBackColor = true;
            this.bt_apply.Click += new System.EventHandler(this.bt_apply_Click);
            // 
            // cb_saveworkdir
            // 
            this.cb_saveworkdir.AutoSize = true;
            this.cb_saveworkdir.Location = new System.Drawing.Point(12, 35);
            this.cb_saveworkdir.Name = "cb_saveworkdir";
            this.cb_saveworkdir.Size = new System.Drawing.Size(169, 16);
            this.cb_saveworkdir.TabIndex = 8;
            this.cb_saveworkdir.Text = "Save selected workdirectory";
            this.cb_saveworkdir.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.rtb_mu0alph);
            this.groupBox1.Controls.Add(this.rtb_chimod12d);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rtb_feff);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rtb_chimod);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rtb_smooth);
            this.groupBox1.Location = new System.Drawing.Point(7, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 182);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program Path";
            // 
            // rtb_chimod12d
            // 
            this.rtb_chimod12d.Filter = "All File|*.*";
            this.rtb_chimod12d.FolderFlag = false;
            this.rtb_chimod12d.Location = new System.Drawing.Point(96, 79);
            this.rtb_chimod12d.Name = "rtb_chimod12d";
            this.rtb_chimod12d.Size = new System.Drawing.Size(275, 25);
            this.rtb_chimod12d.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Chimod12d path";
            // 
            // bt_default
            // 
            this.bt_default.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_default.Location = new System.Drawing.Point(7, 287);
            this.bt_default.Name = "bt_default";
            this.bt_default.Size = new System.Drawing.Size(75, 35);
            this.bt_default.TabIndex = 10;
            this.bt_default.Text = "default";
            this.bt_default.UseVisualStyleBackColor = true;
            this.bt_default.Click += new System.EventHandler(this.bt_default_Click);
            // 
            // cb_showsmoothgraph
            // 
            this.cb_showsmoothgraph.AutoSize = true;
            this.cb_showsmoothgraph.Location = new System.Drawing.Point(13, 57);
            this.cb_showsmoothgraph.Name = "cb_showsmoothgraph";
            this.cb_showsmoothgraph.Size = new System.Drawing.Size(152, 16);
            this.cb_showsmoothgraph.TabIndex = 11;
            this.cb_showsmoothgraph.Text = "Show graph after smooth";
            this.cb_showsmoothgraph.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Mu0alph path";
            // 
            // rtb_mu0alph
            // 
            this.rtb_mu0alph.Filter = "All File|*.*";
            this.rtb_mu0alph.FolderFlag = false;
            this.rtb_mu0alph.Location = new System.Drawing.Point(96, 141);
            this.rtb_mu0alph.Name = "rtb_mu0alph";
            this.rtb_mu0alph.Size = new System.Drawing.Size(275, 25);
            this.rtb_mu0alph.TabIndex = 9;
            // 
            // PreferenceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 334);
            this.Controls.Add(this.cb_showsmoothgraph);
            this.Controls.Add(this.bt_default);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cb_saveworkdir);
            this.Controls.Add(this.bt_apply);
            this.Controls.Add(this.cb_saveanalysissetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PreferenceWindow";
            this.Text = "Preference";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_saveanalysissetting;
        private BTSubControl.RefTextBox rtb_feff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private BTSubControl.RefTextBox rtb_chimod;
        private System.Windows.Forms.Label label3;
        private BTSubControl.RefTextBox rtb_smooth;
        private System.Windows.Forms.Button bt_apply;
        private System.Windows.Forms.CheckBox cb_saveworkdir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_default;
        private System.Windows.Forms.CheckBox cb_showsmoothgraph;
        private BTSubControl.RefTextBox rtb_chimod12d;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private BTSubControl.RefTextBox rtb_mu0alph;
    }
}