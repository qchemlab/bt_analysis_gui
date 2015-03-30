namespace BayesTurchinAnalysis
{
    partial class LogWindow
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
            this.bt_logout = new System.Windows.Forms.Button();
            this.bt_append = new System.Windows.Forms.Button();
            this.bt_clear = new System.Windows.Forms.Button();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bt_logout
            // 
            this.bt_logout.Location = new System.Drawing.Point(150, 243);
            this.bt_logout.Name = "bt_logout";
            this.bt_logout.Size = new System.Drawing.Size(136, 23);
            this.bt_logout.TabIndex = 1;
            this.bt_logout.Text = "Save Log File";
            this.bt_logout.UseVisualStyleBackColor = true;
            this.bt_logout.Click += new System.EventHandler(this.bt_logout_Click);
            // 
            // bt_append
            // 
            this.bt_append.Location = new System.Drawing.Point(63, 243);
            this.bt_append.Name = "bt_append";
            this.bt_append.Size = new System.Drawing.Size(97, 23);
            this.bt_append.TabIndex = 2;
            this.bt_append.Text = "Append to file";
            this.bt_append.UseVisualStyleBackColor = true;
            this.bt_append.Click += new System.EventHandler(this.bt_append_Click);
            // 
            // bt_clear
            // 
            this.bt_clear.Location = new System.Drawing.Point(0, 243);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(75, 23);
            this.bt_clear.TabIndex = 3;
            this.bt_clear.Text = "Clear";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(12, 12);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_log.Size = new System.Drawing.Size(260, 194);
            this.txt_log.TabIndex = 5;
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.bt_clear);
            this.Controls.Add(this.bt_append);
            this.Controls.Add(this.bt_logout);
            this.Name = "LogWindow";
            this.Text = "LogWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogWindow_FormClosing);
            this.SizeChanged += new System.EventHandler(this.LogWindow_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_logout;
        private System.Windows.Forms.Button bt_append;
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.TextBox txt_log;
    }
}