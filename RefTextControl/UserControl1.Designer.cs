namespace BTSubControl
{
    partial class RefTextBox
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.refbutton = new System.Windows.Forms.Button();
            this.textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // refbutton
            // 
            this.refbutton.Location = new System.Drawing.Point(125, 0);
            this.refbutton.Name = "refbutton";
            this.refbutton.Size = new System.Drawing.Size(40, 23);
            this.refbutton.TabIndex = 0;
            this.refbutton.Text = "ref";
            this.refbutton.UseVisualStyleBackColor = true;
            this.refbutton.Click += new System.EventHandler(this.refbutton_Click);
            // 
            // textbox
            // 
            this.textbox.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textbox.Location = new System.Drawing.Point(0, 2);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(116, 19);
            this.textbox.TabIndex = 1;
            this.textbox.TextChanged += new System.EventHandler(this.textbox_TextChanged);
            // 
            // RefTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.refbutton);
            this.Name = "RefTextBox";
            this.Size = new System.Drawing.Size(168, 25);
            this.Resize += new System.EventHandler(this.RefTextBox_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refbutton;
        private System.Windows.Forms.TextBox textbox;
    }
}
