namespace BayesTurchinAnalysis
{
    partial class DeleteGraphWindow
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
            this.list_graph = new System.Windows.Forms.ListBox();
            this.bt_decide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_graph
            // 
            this.list_graph.FormattingEnabled = true;
            this.list_graph.ItemHeight = 12;
            this.list_graph.Location = new System.Drawing.Point(0, 0);
            this.list_graph.Name = "list_graph";
            this.list_graph.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.list_graph.Size = new System.Drawing.Size(281, 220);
            this.list_graph.TabIndex = 0;
            // 
            // bt_decide
            // 
            this.bt_decide.Location = new System.Drawing.Point(0, 226);
            this.bt_decide.Name = "bt_decide";
            this.bt_decide.Size = new System.Drawing.Size(281, 35);
            this.bt_decide.TabIndex = 1;
            this.bt_decide.Text = "delete selected graphs";
            this.bt_decide.UseVisualStyleBackColor = true;
            this.bt_decide.Click += new System.EventHandler(this.bt_decide_Click);
            // 
            // DeleteGraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.bt_decide);
            this.Controls.Add(this.list_graph);
            this.Name = "DeleteGraphWindow";
            this.Text = "DeleteGraph";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_graph;
        private System.Windows.Forms.Button bt_decide;
    }
}