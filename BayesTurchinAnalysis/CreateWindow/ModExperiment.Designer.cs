namespace BayesTurchinAnalysis.CreateWindow
{
    partial class ModExperimentWindow
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
            this.rtb_original = new BTSubControl.RefTextBox();
            this.bt_preedge = new System.Windows.Forms.Button();
            this.chk_original = new System.Windows.Forms.CheckBox();
            this.chk_preedge = new System.Windows.Forms.CheckBox();
            this.chk_error = new System.Windows.Forms.CheckBox();
            this.txt_error = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_comment = new System.Windows.Forms.TextBox();
            this.bt_createmodfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_original
            // 
            this.rtb_original.Location = new System.Drawing.Point(106, 12);
            this.rtb_original.Name = "rtb_original";
            this.rtb_original.Size = new System.Drawing.Size(207, 25);
            this.rtb_original.TabIndex = 0;
            this.rtb_original.RtbTextChanged += new System.EventHandler(this.rtb_original_RtbTextChanged);
            // 
            // bt_preedge
            // 
            this.bt_preedge.Location = new System.Drawing.Point(163, 48);
            this.bt_preedge.Name = "bt_preedge";
            this.bt_preedge.Size = new System.Drawing.Size(150, 24);
            this.bt_preedge.TabIndex = 2;
            this.bt_preedge.Text = "substract pre-edge line";
            this.bt_preedge.UseVisualStyleBackColor = true;
            this.bt_preedge.Click += new System.EventHandler(this.bt_preedge_Click);
            // 
            // chk_original
            // 
            this.chk_original.AutoCheck = false;
            this.chk_original.AutoSize = true;
            this.chk_original.Location = new System.Drawing.Point(12, 16);
            this.chk_original.Name = "chk_original";
            this.chk_original.Size = new System.Drawing.Size(91, 16);
            this.chk_original.TabIndex = 3;
            this.chk_original.Text = "Original Data";
            this.chk_original.UseVisualStyleBackColor = true;
            // 
            // chk_preedge
            // 
            this.chk_preedge.AutoCheck = false;
            this.chk_preedge.AutoSize = true;
            this.chk_preedge.Location = new System.Drawing.Point(12, 53);
            this.chk_preedge.Name = "chk_preedge";
            this.chk_preedge.Size = new System.Drawing.Size(71, 16);
            this.chk_preedge.TabIndex = 4;
            this.chk_preedge.Text = "Pre-edge";
            this.chk_preedge.UseVisualStyleBackColor = true;
            // 
            // chk_error
            // 
            this.chk_error.AutoCheck = false;
            this.chk_error.AutoSize = true;
            this.chk_error.Location = new System.Drawing.Point(12, 93);
            this.chk_error.Name = "chk_error";
            this.chk_error.Size = new System.Drawing.Size(107, 16);
            this.chk_error.TabIndex = 5;
            this.chk_error.Text = "Error Estimation";
            this.chk_error.UseVisualStyleBackColor = true;
            // 
            // txt_error
            // 
            this.txt_error.Location = new System.Drawing.Point(80, 115);
            this.txt_error.Name = "txt_error";
            this.txt_error.Size = new System.Drawing.Size(100, 19);
            this.txt_error.TabIndex = 6;
            this.txt_error.TextChanged += new System.EventHandler(this.txt_error_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "error";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "comment";
            // 
            // txt_comment
            // 
            this.txt_comment.Location = new System.Drawing.Point(80, 140);
            this.txt_comment.Name = "txt_comment";
            this.txt_comment.Size = new System.Drawing.Size(100, 19);
            this.txt_comment.TabIndex = 9;
            // 
            // bt_createmodfile
            // 
            this.bt_createmodfile.Location = new System.Drawing.Point(163, 165);
            this.bt_createmodfile.Name = "bt_createmodfile";
            this.bt_createmodfile.Size = new System.Drawing.Size(150, 37);
            this.bt_createmodfile.TabIndex = 10;
            this.bt_createmodfile.Text = "create modfile";
            this.bt_createmodfile.UseVisualStyleBackColor = true;
            this.bt_createmodfile.Click += new System.EventHandler(this.bt_createmodfile_Click);
            // 
            // ModExperimentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 213);
            this.Controls.Add(this.bt_createmodfile);
            this.Controls.Add(this.txt_comment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_error);
            this.Controls.Add(this.chk_error);
            this.Controls.Add(this.chk_preedge);
            this.Controls.Add(this.chk_original);
            this.Controls.Add(this.bt_preedge);
            this.Controls.Add(this.rtb_original);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ModExperimentWindow";
            this.Text = "ModExperiment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModExperimentWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BTSubControl.RefTextBox rtb_original;
        private System.Windows.Forms.Button bt_preedge;
        private System.Windows.Forms.CheckBox chk_original;
        private System.Windows.Forms.CheckBox chk_preedge;
        private System.Windows.Forms.CheckBox chk_error;
        private System.Windows.Forms.TextBox txt_error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_comment;
        private System.Windows.Forms.Button bt_createmodfile;
    }
}