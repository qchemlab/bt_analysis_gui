namespace BayesTurchinAnalysis
{
    partial class BackGroundWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_exp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bt_decide = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_rightvalue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_exp)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_exp
            // 
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.Name = "ChartArea1";
            this.chart_exp.ChartAreas.Add(chartArea3);
            this.chart_exp.Location = new System.Drawing.Point(0, 31);
            this.chart_exp.Name = "chart_exp";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "EvsXmu";
            this.chart_exp.Series.Add(series3);
            this.chart_exp.Size = new System.Drawing.Size(365, 251);
            this.chart_exp.TabIndex = 0;
            this.chart_exp.Text = "chart1";
            this.chart_exp.Paint += new System.Windows.Forms.PaintEventHandler(this.chart_exp_Paint);
            this.chart_exp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_exp_MouseDown);
            // 
            // bt_decide
            // 
            this.bt_decide.Location = new System.Drawing.Point(193, 288);
            this.bt_decide.Name = "bt_decide";
            this.bt_decide.Size = new System.Drawing.Size(172, 23);
            this.bt_decide.TabIndex = 1;
            this.bt_decide.Text = "Decide";
            this.bt_decide.UseVisualStyleBackColor = true;
            this.bt_decide.Click += new System.EventHandler(this.bt_decide_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "RightMaxValue";
            // 
            // txt_rightvalue
            // 
            this.txt_rightvalue.Location = new System.Drawing.Point(101, 6);
            this.txt_rightvalue.Name = "txt_rightvalue";
            this.txt_rightvalue.Size = new System.Drawing.Size(100, 19);
            this.txt_rightvalue.TabIndex = 3;
            this.txt_rightvalue.TextChanged += new System.EventHandler(this.txt_rightvalue_TextChanged);
            // 
            // BackGroundWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 311);
            this.Controls.Add(this.txt_rightvalue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_decide);
            this.Controls.Add(this.chart_exp);
            this.Name = "BackGroundWindow";
            this.Text = "BackGroundWindow";
            this.SizeChanged += new System.EventHandler(this.BackGroundWindow_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.chart_exp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_exp;
        private System.Windows.Forms.Button bt_decide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_rightvalue;
    }
}