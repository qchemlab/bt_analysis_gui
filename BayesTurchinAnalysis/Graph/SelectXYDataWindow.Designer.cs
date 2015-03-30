namespace BayesTurchinAnalysis
{
    partial class SelectXYDataWindow
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
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_X = new System.Windows.Forms.ComboBox();
            this.cmb_Y = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_Error = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bt_OK
            // 
            this.bt_OK.Location = new System.Drawing.Point(104, 142);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(75, 23);
            this.bt_OK.TabIndex = 0;
            this.bt_OK.Text = "OK";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(185, 142);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_Cancel.TabIndex = 1;
            this.bt_Cancel.Text = "Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "XData";
            // 
            // cmb_X
            // 
            this.cmb_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_X.FormattingEnabled = true;
            this.cmb_X.Items.AddRange(new object[] {
            "Eexp_0",
            "k_0",
            "exp_0",
            "fit_0",
            "err_chi_0",
            "f_l_0",
            "mue_0",
            "muf_0",
            "mu0c_0/mu0_0",
            "mu0_0",
            "err_mue_0",
            "err_eff_0",
            "Eexp_1",
            "k_1",
            "exp_1",
            "fit_1",
            "err_chi_1",
            "f_l_1",
            "mue_1",
            "muf_1",
            "mu0c_1/mu0_1",
            "mu0_1",
            "err_mue_1",
            "err_eff_1",
            "R",
            "f(R)_SS",
            "f(R)",
            "g(R)",
            "FT(chie)",
            "FT(chim)",
            "FT(mu0i)",
            "FT(mu0f)",
            "FT(mu0fp)",
            "FT(mu0fm)",
            "kbg",
            "initial",
            "final",
            "finalm",
            "finalp",
            "Erel"});
            this.cmb_X.Location = new System.Drawing.Point(61, 51);
            this.cmb_X.Name = "cmb_X";
            this.cmb_X.Size = new System.Drawing.Size(199, 20);
            this.cmb_X.TabIndex = 3;
            this.cmb_X.SelectedIndexChanged += new System.EventHandler(this.cmb_X_SelectedIndexChanged);
            // 
            // cmb_Y
            // 
            this.cmb_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Y.FormattingEnabled = true;
            this.cmb_Y.Location = new System.Drawing.Point(61, 77);
            this.cmb_Y.Name = "cmb_Y";
            this.cmb_Y.Size = new System.Drawing.Size(199, 20);
            this.cmb_Y.TabIndex = 5;
            this.cmb_Y.SelectedIndexChanged += new System.EventHandler(this.cmb_Y_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "YData";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select data for plot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Error";
            // 
            // cmb_Error
            // 
            this.cmb_Error.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Error.FormattingEnabled = true;
            this.cmb_Error.Location = new System.Drawing.Point(61, 105);
            this.cmb_Error.Name = "cmb_Error";
            this.cmb_Error.Size = new System.Drawing.Size(199, 20);
            this.cmb_Error.TabIndex = 8;
            this.cmb_Error.SelectedIndexChanged += new System.EventHandler(this.cmb_Error_SelectedIndexChanged);
            // 
            // SelectXYDataWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 184);
            this.Controls.Add(this.cmb_Error);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_Y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_X);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_OK);
            this.Name = "SelectXYDataWindow";
            this.Text = "SelectXYDataWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_X;
        private System.Windows.Forms.ComboBox cmb_Y;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_Error;
    }
}