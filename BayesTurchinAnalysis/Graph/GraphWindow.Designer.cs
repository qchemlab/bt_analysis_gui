namespace BayesTurchinAnalysis
{
    partial class GraphWindowBase
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGraphToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeShowScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yAutoScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMajorGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMinorGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorBarOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.rangeToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(484, 26);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGraphToolStripMenuItem,
            this.saveGraphToTextToolStripMenuItem,
            this.addGraphToolStripMenuItem,
            this.deleteGraphToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveGraphToolStripMenuItem
            // 
            this.saveGraphToolStripMenuItem.Name = "saveGraphToolStripMenuItem";
            this.saveGraphToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveGraphToolStripMenuItem.Text = "Save Graph";
            this.saveGraphToolStripMenuItem.Click += new System.EventHandler(this.saveGraphToolStripMenuItem_Click);
            // 
            // saveGraphToTextToolStripMenuItem
            // 
            this.saveGraphToTextToolStripMenuItem.Name = "saveGraphToTextToolStripMenuItem";
            this.saveGraphToTextToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saveGraphToTextToolStripMenuItem.Text = "Save Graph to Text";
            this.saveGraphToTextToolStripMenuItem.Click += new System.EventHandler(this.saveGraphToTextToolStripMenuItem_Click);
            // 
            // addGraphToolStripMenuItem
            // 
            this.addGraphToolStripMenuItem.Name = "addGraphToolStripMenuItem";
            this.addGraphToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.addGraphToolStripMenuItem.Text = "Add Graph";
            this.addGraphToolStripMenuItem.Click += new System.EventHandler(this.addGraphToolStripMenuItem_Click);
            // 
            // deleteGraphToolStripMenuItem
            // 
            this.deleteGraphToolStripMenuItem.Name = "deleteGraphToolStripMenuItem";
            this.deleteGraphToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.deleteGraphToolStripMenuItem.Text = "Delete Graph";
            this.deleteGraphToolStripMenuItem.Click += new System.EventHandler(this.deleteGraphToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // rangeToolStripMenuItem
            // 
            this.rangeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeShowScaleToolStripMenuItem,
            this.autoScaleToolStripMenuItem,
            this.yAutoScaleToolStripMenuItem,
            this.defaultToolStripMenuItem,
            this.previousRangeToolStripMenuItem});
            this.rangeToolStripMenuItem.Name = "rangeToolStripMenuItem";
            this.rangeToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.rangeToolStripMenuItem.Text = "Range";
            // 
            // changeShowScaleToolStripMenuItem
            // 
            this.changeShowScaleToolStripMenuItem.Name = "changeShowScaleToolStripMenuItem";
            this.changeShowScaleToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.changeShowScaleToolStripMenuItem.Text = "Change Show Scale";
            this.changeShowScaleToolStripMenuItem.Click += new System.EventHandler(this.changeShowScaleToolStripMenuItem_Click);
            // 
            // autoScaleToolStripMenuItem
            // 
            this.autoScaleToolStripMenuItem.CheckOnClick = true;
            this.autoScaleToolStripMenuItem.Name = "autoScaleToolStripMenuItem";
            this.autoScaleToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.autoScaleToolStripMenuItem.Text = "XY Auto Scale";
            this.autoScaleToolStripMenuItem.Click += new System.EventHandler(this.autoScaleToolStripMenuItem_Click);
            // 
            // yAutoScaleToolStripMenuItem
            // 
            this.yAutoScaleToolStripMenuItem.Checked = true;
            this.yAutoScaleToolStripMenuItem.CheckOnClick = true;
            this.yAutoScaleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.yAutoScaleToolStripMenuItem.Name = "yAutoScaleToolStripMenuItem";
            this.yAutoScaleToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.yAutoScaleToolStripMenuItem.Text = "Y Auto Scale";
            this.yAutoScaleToolStripMenuItem.Click += new System.EventHandler(this.yAutoScaleToolStripMenuItem_Click);
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // previousRangeToolStripMenuItem
            // 
            this.previousRangeToolStripMenuItem.Name = "previousRangeToolStripMenuItem";
            this.previousRangeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.previousRangeToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.previousRangeToolStripMenuItem.Text = "Previous Range";
            this.previousRangeToolStripMenuItem.Click += new System.EventHandler(this.previousRangeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMajorGridToolStripMenuItem,
            this.showMinorGridToolStripMenuItem,
            this.errorBarOnOffToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showMajorGridToolStripMenuItem
            // 
            this.showMajorGridToolStripMenuItem.Checked = true;
            this.showMajorGridToolStripMenuItem.CheckOnClick = true;
            this.showMajorGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMajorGridToolStripMenuItem.Name = "showMajorGridToolStripMenuItem";
            this.showMajorGridToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showMajorGridToolStripMenuItem.Text = "Show Major Grid";
            this.showMajorGridToolStripMenuItem.Click += new System.EventHandler(this.showMajorGridToolStripMenuItem_Click);
            // 
            // showMinorGridToolStripMenuItem
            // 
            this.showMinorGridToolStripMenuItem.CheckOnClick = true;
            this.showMinorGridToolStripMenuItem.Name = "showMinorGridToolStripMenuItem";
            this.showMinorGridToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showMinorGridToolStripMenuItem.Text = "Show Minor Grid";
            this.showMinorGridToolStripMenuItem.Click += new System.EventHandler(this.showMinorGridToolStripMenuItem_Click);
            // 
            // errorBarOnOffToolStripMenuItem
            // 
            this.errorBarOnOffToolStripMenuItem.Checked = true;
            this.errorBarOnOffToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorBarOnOffToolStripMenuItem.Name = "errorBarOnOffToolStripMenuItem";
            this.errorBarOnOffToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.errorBarOnOffToolStripMenuItem.Text = "ErrorBar On/Off";
            this.errorBarOnOffToolStripMenuItem.Click += new System.EventHandler(this.errorBarOnOffToolStripMenuItem_Click);
            // 
            // chart
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 8;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 8;
            chartArea1.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
            chartArea1.AxisX.LabelStyle.Angle = 90;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Meiryo UI", 8.75F);
            chartArea1.AxisX.MaximumAutoSize = 30F;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Meiryo UI", 8.75F);
            chartArea1.AxisY.MaximumAutoSize = 30F;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "area";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Location = new System.Drawing.Point(0, 30);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(284, 231);
            this.chart.TabIndex = 1;
            this.chart.Text = "chart1";
            this.chart.AxisViewChanging += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart_AxisViewChanging);
            this.chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDown);
            // 
            // GraphWindowBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "GraphWindowBase";
            this.Text = "GraphWindow";
            this.SizeChanged += new System.EventHandler(this.GraphWindow_SizeChanged);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGraphToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeShowScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMajorGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMinorGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yAutoScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorBarOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousRangeToolStripMenuItem;
        protected System.Windows.Forms.MenuStrip menu;
        protected System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}