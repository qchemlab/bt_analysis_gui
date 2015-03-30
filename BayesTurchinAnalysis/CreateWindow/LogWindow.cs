using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace BayesTurchinAnalysis
{
    public partial class LogWindow : Form
    {
        #region scrollbar_valuefuncs_notused
        public enum ScrollBarKind
        {
            Horizonal = 0x0000,
            Vertical = 0x0001
        }
        [DllImport("USER32.DLL", CharSet=CharSet.Auto)]
        private static extern int GetScrollPos(
            System.IntPtr handle,
            ScrollBarKind kind
        );

        [DllImport("USER32.DLL", CharSet=CharSet.Auto)]
        private static extern bool GetScrollRange(
            System.IntPtr handle,
            ScrollBarKind kind,
            out int iMinimum,
            out int lMaximum
        );
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern int SetScrollPos(
          IntPtr hWnd,     // ウィンドウのハンドル
          ScrollBarKind nBar,      // スクロールバー
          int nPos,      // スクロールボックスの新しい位置
          bool bRedraw   // 再描画フラグ
        );

        public static bool IsScrollBarEnd(TextBox t, int maxline)
        {
            int iMinimum = 0;
            int iMaximum = 0;

            // スクロール バーの範囲を取得する
            if (!GetScrollRange(t.Handle, ScrollBarKind.Vertical, out iMinimum, out iMaximum))
            {
                return false;
            }

            int iPostion = GetScrollPos(t.Handle, ScrollBarKind.Vertical);
            if (iMaximum - iPostion <= maxline - 1)
            {
                return true;
            }

            return false;
        }
        public static int GetScrollBarValue(TextBox t)
        {
            return GetScrollPos(t.Handle, ScrollBarKind.Vertical);
        }
        #endregion

        public LogWindow(Point p,Size s)
        {
            InitializeComponent();
            this.ClientSize = s;
            txt_log.Location = new Point(0,0);
            txt_log.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 25); ;
            bt_clear.Location = new Point(0, this.ClientSize.Height - 25);
            bt_clear.Size = new Size(this.ClientSize.Width / 3, 25);
            bt_append.Location = new Point(this.ClientSize.Width / 3, this.ClientSize.Height - 25);
            bt_append.Size = new Size(this.ClientSize.Width / 3, 25);
            bt_logout.Location = new Point(this.ClientSize.Width * 2 / 3, this.ClientSize.Height - 25);
            bt_logout.Size = new Size(this.ClientSize.Width / 3, 25);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = p;
            txt_log.HideSelection = false;
            txt_log.ScrollToCaret();
        }
        public void ShowClearWindow(Point p)
        {
            this.Location = p;
            if(!this.Visible)
            {
                this.Visible = true;
                txt_log.Text = "";
            }
        }
        private void LogWindow_SizeChanged(object sender, EventArgs e)
        {
            txt_log.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 25);

            bt_clear.Location = new Point(0, this.ClientSize.Height - 25);
            bt_clear.Size = new Size(this.ClientSize.Width / 3, 25);
            bt_append.Location = new Point(this.ClientSize.Width  / 3, this.ClientSize.Height - 25);
            bt_append.Size = new Size(this.ClientSize.Width / 3, 25);
            bt_logout.Location = new Point(this.ClientSize.Width *2/ 3, this.ClientSize.Height - 25);
            bt_logout.Size = new Size(this.ClientSize.Width/3, 25);
        }
        public void Flush()
        {
            txt_log.Text = "";
        }
        public void Add(string s)
        {
            txt_log.Text += s;
            txt_log.SelectionStart = txt_log.Text.Length;
            txt_log.ScrollToCaret();

        }
        public void AddL(string s)
        {
            txt_log.AppendText(s + Environment.NewLine);
        }
        public void SaveException()
        {
            using (StreamWriter sw = new StreamWriter("log.txt", false, BTMainForm.CharEncode))
            {
                sw.Write(txt_log.Text);
            }
        }
        private void bt_logout_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "log.txt";
            sfd.Filter = "txt file|*.txt|all file|*.*";
            sfd.Title = "save log file";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName,false, BTMainForm.CharEncode))
                {
                    sw.Write(txt_log.Text);
                }
            }
        }

        private void bt_append_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "log.txt";
            sfd.Filter = "txt file|*.txt|all file|*.*";
            sfd.Title = "save log file";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, true, BTMainForm.CharEncode))
                {
                    sw.Write(txt_log.Text);
                }
            }

        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            txt_log.Text = "";
        }

        private void LogWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
    }
}
