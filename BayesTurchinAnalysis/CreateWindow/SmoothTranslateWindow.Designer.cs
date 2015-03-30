namespace BayesTurchinAnalysis.CreateWindow
{
    partial class SmoothTranslateWindow
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
            this.cb_target = new System.Windows.Forms.ComboBox();
            this.txt_dif = new System.Windows.Forms.TextBox();
            this.lbl_dif = new System.Windows.Forms.Label();
            this.lbl_target = new System.Windows.Forms.Label();
            this.bt_decide = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_yscale = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Size = new System.Drawing.Size(500, 386);
            // 
            // cb_target
            // 
            this.cb_target.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_target.FormattingEnabled = true;
            this.cb_target.Items.AddRange(new object[] {
            "mu exp",
            "mu 0(feff)"});
            this.cb_target.Location = new System.Drawing.Point(111, 430);
            this.cb_target.Name = "cb_target";
            this.cb_target.Size = new System.Drawing.Size(75, 20);
            this.cb_target.TabIndex = 2;
            this.cb_target.SelectedIndexChanged += new System.EventHandler(this.cb_target_SelectedIndexChanged);
            // 
            // txt_dif
            // 
            this.txt_dif.Location = new System.Drawing.Point(254, 430);
            this.txt_dif.Name = "txt_dif";
            this.txt_dif.Size = new System.Drawing.Size(51, 19);
            this.txt_dif.TabIndex = 3;
            this.txt_dif.TextChanged += new System.EventHandler(this.txt_dif_TextChanged);
            this.txt_dif.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_dif_KeyDown);
            // 
            // lbl_dif
            // 
            this.lbl_dif.AutoSize = true;
            this.lbl_dif.Location = new System.Drawing.Point(192, 433);
            this.lbl_dif.Name = "lbl_dif";
            this.lbl_dif.Size = new System.Drawing.Size(56, 12);
            this.lbl_dif.TabIndex = 4;
            this.lbl_dif.Text = "difference";
            // 
            // lbl_target
            // 
            this.lbl_target.AutoSize = true;
            this.lbl_target.Location = new System.Drawing.Point(12, 433);
            this.lbl_target.Name = "lbl_target";
            this.lbl_target.Size = new System.Drawing.Size(93, 12);
            this.lbl_target.TabIndex = 5;
            this.lbl_target.Text = "translation target";
            // 
            // bt_decide
            // 
            this.bt_decide.Location = new System.Drawing.Point(410, 430);
            this.bt_decide.Name = "bt_decide";
            this.bt_decide.Size = new System.Drawing.Size(62, 23);
            this.bt_decide.TabIndex = 6;
            this.bt_decide.Text = "decide";
            this.bt_decide.UseVisualStyleBackColor = true;
            this.bt_decide.Click += new System.EventHandler(this.bt_decide_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 433);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "y scale";
            // 
            // txt_yscale
            // 
            this.txt_yscale.Location = new System.Drawing.Point(359, 430);
            this.txt_yscale.Name = "txt_yscale";
            this.txt_yscale.Size = new System.Drawing.Size(45, 19);
            this.txt_yscale.TabIndex = 8;
            this.txt_yscale.TextChanged += new System.EventHandler(this.txt_yscale_TextChanged);
            // 
            // SmoothTranslateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.txt_yscale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_decide);
            this.Controls.Add(this.lbl_target);
            this.Controls.Add(this.lbl_dif);
            this.Controls.Add(this.txt_dif);
            this.Controls.Add(this.cb_target);
            this.Name = "SmoothTranslateWindow";
            this.Text = "SmoothTranslateWindow";
            this.SizeChanged += new System.EventHandler(this.SmoothTranslateWindow_SizeChanged);
            this.Controls.SetChildIndex(this.cb_target, 0);
            this.Controls.SetChildIndex(this.txt_dif, 0);
            this.Controls.SetChildIndex(this.lbl_dif, 0);
            this.Controls.SetChildIndex(this.lbl_target, 0);
            this.Controls.SetChildIndex(this.bt_decide, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txt_yscale, 0);
            this.Controls.SetChildIndex(this.chart, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_target;
        private System.Windows.Forms.TextBox txt_dif;
        private System.Windows.Forms.Label lbl_dif;
        private System.Windows.Forms.Label lbl_target;
        private System.Windows.Forms.Button bt_decide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_yscale;
    }
}