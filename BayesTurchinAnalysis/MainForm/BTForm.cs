using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BayesTurchinAnalysis.CreateWindow;
using System.IO;
using BTSubControl;

////////////////////////////////////////////////////////////////////////
//
//  GUIの管理部分
//
////////////////////////////////////////////////////////////////////////

namespace BayesTurchinAnalysis
{
    public partial class BTMainForm : Form
    {
        //デフォルトのWindowsテキストエンコード
        public static Encoding CharEncode = Encoding.GetEncoding("Shift-JIS");
        //このプログラムの存在ディレクトリ
        string ExeDirectory = "";
        
        //ログウィンドウと実験データ調整ウインドウは普段は非表示にしておく
        ModExperimentWindow mew;
        LogWindow log;
        //このプログラム自体の設定ファイルのパス
        const string PreferenceFileName = "config.cfg";
        //設定データ格納クラス
        PreferenceData preference;


        public void SetModExpFile(string f)
        {
            rtb_modexpdata.Text = f;
        }


        void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("errorlog.txt"))
            {
                sw.WriteLine("Exception:\n" + e.ToString());
                sw.WriteLine("Message:\n" + e.ExceptionObject);
            }

            SavePreferenceFile();
          //  Application.Exit();
        }
        //設定ファイルの読み出し
        void LoadPreferenceFile()
        {
            if (!File.Exists(ExeDirectory + "\\" + PreferenceFileName))
                return;

            //xmlのデシリアライズ
            System.Xml.Serialization.XmlSerializer xml
                = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceData));
            using (StreamReader sr = new StreamReader(ExeDirectory + "\\" + PreferenceFileName))
            {
                preference = (PreferenceData)xml.Deserialize(sr);
            }

            //設定に応じて読み出し
            if (preference.SaveUsedPath)
            {
                preference.LoadAnalysisSettings(rtb_workdir, rtb_feffinp, rtb_feffsmoothinp
                    , rtb_modexpdata, rtb_expsmoothinp, rtb_btinp, cb_fefftargetedge, txt_criteria_start, txt_criteria_end);
            }
            if (preference.SaveSelectedDirectory)
            {
                rtb_workdir.Text = preference.WorkDir;
            }

            Action<string> CheckExePath = (path) =>
                {
                    if (!File.Exists(path))
                        MessageBox.Show(path + " is not found!" + Environment.NewLine
                            + "please set valid program");
                };

            CheckExePath(preference.GetChimod12dExePath(ExeDirectory));
            CheckExePath(preference.GetChimod12ExePath(ExeDirectory));
            CheckExePath(preference.GetFeffExePath(ExeDirectory));
            CheckExePath(preference.GetSmoothExePath(ExeDirectory));

        }
        //設定ファイルへの書き込み
        void SavePreferenceFile()
        {
            //preference構造体にデータを流し込む
            preference.SetAnalysisSettings(rtb_workdir,rtb_feffinp, rtb_feffsmoothinp
                , rtb_modexpdata, rtb_expsmoothinp, rtb_btinp, cb_fefftargetedge,txt_criteria_start,txt_criteria_end);

            //シリアライズして保存
            System.Xml.Serialization.XmlSerializer xml
                = new System.Xml.Serialization.XmlSerializer(typeof(PreferenceData));
            using (StreamWriter sw = new StreamWriter(ExeDirectory + "\\" + PreferenceFileName))
            {
                xml.Serialize(sw, preference);
            }
        }
        //カレントディレクトリをworkdirectoryテキストボックスに入力されている値に変更
        void SetDefaultCurrentDirectory()
        {
            if(File.Exists(rtb_workdir.Text))
                Directory.SetCurrentDirectory(rtb_workdir.Text);
        }
        //GUI初期化
        public BTMainForm()
        {
            InitializeComponent();
            mew = new ModExperimentWindow(this);
            log = new LogWindow(new Point(this.Location.X + this.Size.Width, this.Location.Y)
                ,new Size(400,this.Height));
            log.Owner = this;

            cb_fefftargetedge.SelectedIndex = 0;
            cb_feffselectrunmode.SelectedIndex = 0;
            cb_smoothplot.SelectedIndex = 0;
            rtb_workdir.FolderFlag = true;
            ExeDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            tabControl.Location = new Point(0, MenuStrip.Size.Height);
            tabControl.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - MenuStrip.Size.Height);
            this.AddOwnedForm(mew);
            preference = new PreferenceData();

          //  this.rtb_workdir.Text = "C:\\Users\\yoshihito\\Desktop\\test";
            LoadPreferenceFile();
            System.Threading.Thread.GetDomain().UnhandledException += UnhandledException;
        }
        //smooth.inpファイルの生成
        private string CreateSmoothInp(bool exp)
        {
            string res;
            SmoothCreateWindow smw = new SmoothCreateWindow(exp?rtb_expsmoothinp.Text:rtb_feffsmoothinp.Text,exp);
            AddOwnedForm(smw);

            if (smw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                res = smw.Filename;
            else
                res = "";
            RemoveOwnedForm(smw);
            return res;
        }
        //ログウィンドウの表示
        private void ShowLogWindow()
        {
            log.ShowClearWindow(new Point(this.Location.X + this.Size.Width, this.Location.Y));
        }
        //マルチスレッド対応ログ出力関数
        public void AddLog(string s)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddLog), s);
                return;
            }
            if (s == null)
                return;
            s = s.Replace("\n", Environment.NewLine);
            log.AddL(s);
        }
        //大きなプロセス実行時にはそれらに関するボタンを切る
        public void ControlProcessButtons(bool enable,bool bt )
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool,bool>(ControlProcessButtons), enable,bt);
                return;
            }
            if (enable)
            {
                bt_runfeff.Enabled = enable;
                bt_runbt.Text =  "Run BT Analysis";
                bt_runbt.Enabled = enable;
                bt_runfeff.Text = "Run Feff";
                bt_allprocess.Enabled = enable;
            }
            else
            {
                if (bt)
                {
                    bt_runfeff.Enabled = enable;
                    bt_runbt.Text = "Cancel";
                    bt_allprocess.Enabled = enable;
                }
                else
                {
                    bt_runbt.Enabled = enable;
                    bt_runfeff.Text = "Cancel";
                    bt_allprocess.Enabled = enable;
                }
            }
        }
        //大元のウインドウが閉じられるときの終了処理
        private void BTMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePreferenceFile();
        }
        //カレントディレクトリの情報からデフォルトの値を他のテキストボックスに挿入
        private void setdeffilepath_Click(object sender, EventArgs e)
        {
            string feff = rtb_workdir.Text + "\\feff.inp";
            string fsmooth = rtb_workdir.Text + "\\xmudat\\smooth.inp";
            string btinp = rtb_workdir.Text + "\\feffdat\\BTinp.dat";
            string main =
                "Would you like to set file paths default?" + Environment.NewLine + Environment.NewLine +
                "Feff:  " + feff + Environment.NewLine +
                "Feff_smooth:  " + fsmooth + Environment.NewLine +
                "BTinp.dat:  " + btinp + Environment.NewLine;
            if (MessageBox.Show(main, "set path", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                rtb_feffinp.Text = feff;
                rtb_feffsmoothinp.Text = fsmooth;
                rtb_btinp.Text = btinp;
            }
        }
        //LogWindowの表示非表示
        private void showHideLogWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            log.Visible = !log.Visible;
        }
        /// ファイルの存在有無を返す。ない場合はメッセージボックスを表示
        private bool IsFileExist(string path)
        {
            if (File.Exists(path) == false)
            {
                MessageBox.Show(path + " is not found");
                return false;
            }
            return true;
        }
        //設定を変更する
        private void preferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferenceWindow pw = new PreferenceWindow(preference);
            pw.ShowDialog();
        }

        //ボタン押下イベントは以下
        #region button_events
        //feffで使うsmooth.inp生成ウインドウ
        private void bt_feffsmoothcreate_Click(object sender, EventArgs e)
        {
            string s = CreateSmoothInp(false);
            if (s != "") rtb_feffsmoothinp.Text = s;
        }
        //実験データでつかうsmooth.ino生成ウインドウ
        private void bt_expsmoothcreate_Click(object sender, EventArgs e)
        {
            string s = CreateSmoothInp(true);
            if (s != "") rtb_expsmoothinp.Text = s;
        }
        //BTinp.datの生成ウインドウ
        private void bt_btinpcreate_Click(object sender, EventArgs e)
        {
            BTCreateWindow btw;
            btw = new BTCreateWindow(rtb_btinp.Text);
            AddOwnedForm(btw);
            if (btw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtb_btinp.Text = btw.Filename;
            }
            RemoveOwnedForm(btw);
        }
        //実験データ調整ウインドウ
        private void bt_prepareexpdata_Click(object sender, EventArgs e)
        {
            mew.StartPosition = FormStartPosition.Manual;
            mew.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
            mew.Show();
        }
        //FEFFの実行
        private void bt_runfeff_Click(object sender, EventArgs e)
        {
            ShowLogWindow();
            if (bt_runfeff.Text == "Cancel")
            {
                if (backthread != null)
                {
                    backthread.Abort();
                    backthread = null;
                    if (ExProgram != null)
                    {
                        ExProgram.Kill();
                        ExProgram = null;
                    }
                    ControlProcessButtons(true, true);
                    Directory.SetCurrentDirectory(rtb_workdir.Text);
                    File.Delete("feff.inp");
                    File.Copy("feff.inp.original.tmp", "feff.inp");
                    File.Delete("feff.inp.original.tmp");
                }
            }
            else
                RunFeffProcess();
        }
        //BT計算の実行
        private void bt_runbt_Click(object sender, EventArgs e)
        {
            ShowLogWindow();
            if (bt_runbt.Text == "Cancel")
            {
                if (backthread != null)
                {
                    backthread.Abort();
                    backthread = null;
                    if (ExProgram != null)
                    {
                        ExProgram.Kill();
                        ExProgram = null;
                    }
                    ControlProcessButtons(true, true);
                }
            }
            else
                RunBT();
        }
        //BT結果フォルダの表示
        private void bt_showfolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\feffdat");
        }
        //実験データのsmoothing
        private void bt_runexpxmooth_Click(object sender, EventArgs e)
        {
            RunExpSmooth();
        }
        //全行程の一括実行
        private void bt_allprocess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("run feff -> exp process -> bt " + Environment.NewLine
                + "are you sure ?", "all process", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                cb_feffselectrunmode.SelectedIndex = 0;
                ShowLogWindow();
                System.Threading.Thread t = new System.Threading.Thread(
                    new System.Threading.ParameterizedThreadStart(RunAllProcess));
                t.IsBackground = true;
                t.Start();
            }
        }
        //smooth結果を平行移動して合わせるウインドウ
        private void bt_smoothfit_Click(object sender, EventArgs e)
        {
            SmoothTranslateWindow stw = new SmoothTranslateWindow();
            if (stw.create_failed == false)
                stw.ShowDialog();
            else
                stw.Close();
        }
        //実験結果を表で表示
        //TODO:現状メモ帳を呼んでいるので時間があればコントロールに直す
        private void bt_paramtable_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad", "feffdat/BTout.dat");
        }
        //グラフ表示：parameter difference
        private void bt_difference_Click(object sender, EventArgs e)
        {
            if (File.Exists("feffdat/BTout.dat"))
            {
                DifferenceGraphWindow dgw = new DifferenceGraphWindow();
                dgw.Show();
            }
            else
                MessageBox.Show("BTout.dat isn't exist.");
        }
        //CRITERIAテキストボックスの値が変更された時に調整
        private void txt_criteria_changed(object sender, EventArgs e)
        {
            try
            {
                double start = double.Parse(txt_criteria_start.Text);
                double end = double.Parse(txt_criteria_end.Text);

                txt_criteria_step.Text = ((start - end) / 40.0).ToString();

            }
            catch
            { }
        }
        //workdirテキストボックスが変更された時
        private void rtb_workdir_RtbTextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(rtb_workdir.Text))
            {
                BTSubControl.RefTextBox.RefInitialDir = rtb_workdir.Text;
                Directory.SetCurrentDirectory(rtb_workdir.Text);
            }
        }
        //実験ファイル調整テキストボックスが変更された時
        private void rtb_modexpdata_RtbTextChanged(object sender, EventArgs e)
        {
            chk_expdata.Checked = rtb_modexpdata.Text != "" && rtb_expsmoothinp.Text != "";
        }
        //グラフ表示：その他いろいろ
        private void bt_grothergr_Click(object sender, EventArgs e)
        {
            if (IsFileExist("feffdat/BTdat.dat") == false)
                return;

            SelectXYDataWindow sw = new SelectXYDataWindow();
            if (sw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (sw.Xindex == -1 || sw.Yindex == -1)
                {
                    MessageBox.Show("select x,y value");
                    return;
                }
                DefGraphWindow ow = new DefGraphWindow();
                ow.AddGraphByXYWindow(sw);
                ow.WindowTitle = "data graph window";
                ow.Show();
            }
        }
        //グラフ表示：初期値(a priori)
        private void bt_grinitial_Click(object sender, EventArgs e)
        {
            if (IsFileExist("feffdat/BTdat.dat") == false)
                return;

            SelectXYDataWindow xy = new SelectXYDataWindow();
            DefGraphWindow dgw = new DefGraphWindow();
            xy.SetXY_Manual("k_0", "exp_0", "");
            dgw.AddGraphByXYWindow(xy);
            xy.SetXY_Manual("k_0", "fit_0", "err_chi_0");
            dgw.AddGraphByXYWindow(xy);
            dgw.WindowTitle = "a priori exp/fit spectrum";
            dgw.Show();
        }
        //グラフ表示：結果(a posteriori)
        private void bt_grfinal_Click(object sender, EventArgs e)
        {
            if (IsFileExist("feffdat/BTdat.dat") == false)
                return;

            SelectXYDataWindow xy = new SelectXYDataWindow();
            DefGraphWindow dgw = new DefGraphWindow();
            xy.SetXY_Manual("k_1", "exp_1", "");
            dgw.AddGraphByXYWindow(xy);
            xy.SetXY_Manual("k_1", "fit_1", "err_chi_1");
            dgw.AddGraphByXYWindow(xy);
            dgw.WindowTitle = "a posteriori exp/fit spectrum";
            dgw.Show();
        }
        //グラフ表示：動径分布関数
        private void bt_grrdf_Click(object sender, EventArgs e)
        {
            if (IsFileExist("feffdat/BTdat.dat") == false)
                return;

            SelectXYDataWindow xy = new SelectXYDataWindow();
            DefGraphWindow dgw = new DefGraphWindow();
            xy.SetXY_Manual("R", "g(R)");
            dgw.AddGraphByXYWindow(xy);
            dgw.WindowTitle = "radial distribution function";
            dgw.Show();
        }
        //グラフ表示：行列の表示
        private void bt_grmatrix_Click(object sender, EventArgs e)
        {
            MatrixGraphWindow mgw = new MatrixGraphWindow();
            mgw.WindowTitle = "error correlation matrix";
            mgw.Show();
        }
        //グラフ表示：sn2データ
        private void bt_grsn2_Click(object sender, EventArgs e)
        {
            Sn2GraphWindow sgw = new Sn2GraphWindow();
            sgw.WindowTitle = "sn2 data";
            sgw.Show();
        }
        //グラフ表示：固有値
        private void bt_eigenvalue_Click(object sender, EventArgs e)
        {
            OtherWindow aew = new OtherWindow();
            aew.SetEigenValues();
            aew.WindowTitle = "eigenvalues";
            aew.Show();
        }
        //グラフ表示：α
        private void bt_alpha_Click(object sender, EventArgs e)
        {
            var fname = "feffdat/BTpa" + txt_alphanum.Text + ".dat";
            if (File.Exists(fname))
            {
                OtherWindow aew = new OtherWindow();
                aew.SetAlphaValues(fname);
                aew.WindowTitle = "propability distribution of alpha";
                aew.Show();
            }
            else
            {
                MessageBox.Show(fname + " not found");
            }
        }
        //グラフ表示：smoothing前後
        private void bt_smooth_Click(object sender, EventArgs e)
        {
            OtherWindow ow = new OtherWindow();
            switch (cb_smoothplot.SelectedIndex)
            {
                case 0: // feff before after
                    ow.SetSmoothGraphMuAv("feffdat/mu0_av.dat", "(after)");
                    ow.SetSmoothGraphMu("feffdat/mu0_av.dat", "(before)");
                    ow.WindowTitle = "feff smooth before/after";
                    break;
                case 1: // exp before after
                    ow.SetSmoothGraphMuAv("feffdat/muexp_av.dat", "(after)");
                    ow.SetSmoothGraphMu("feffdat/muexp_av.dat", "(before)");
                    ow.WindowTitle = "experiment data smooth before/after";
                    break;
                case 2: // exp feff after
                    ow.SetSmoothGraphMuAv("feffdat/mu0_av.dat");
                    ow.SetSmoothGraphMuAv("feffdat/muexp_av.dat");
                    ow.WindowTitle = "feff/exp data smooth after";
                    break;
            }

            ow.Show();
        }
        #endregion
  
    }

    //保存データ及び設定ファイル
    public class PreferenceData
    {
        //各必要プログラムのデフォルトパス
        public const string DefSmoothExeName = "smooth.exe";
        public const string DefFeffExeName = "feff83_mod.exe";
        public const string DefChimod12ExeName = "chimod12.exe";
        public const string DefChimod12dExeName = "chimod12d.exe";
        public const string DefMu0AlphExeName = "mu0alph.exe";
        //実際に使っているパス
        public string FeffExeName;
        public string Chimod12ExeName;
        public string Chimod12dExeName;
        public string SmoothExeName;
        public string Mu0AlphExeName;
        //外側から各実行ファイルの位置について親ディレクトリを指定して得る
        public string GetFeffExePath(string parent_directory)
        {
            return FeffExeName == "default" ?
                parent_directory + "\\" + DefFeffExeName :
                FeffExeName;
        }
        public string GetChimod12ExePath(string parent_directory)
        {
            return Chimod12ExeName == "default" ?
                parent_directory + "\\" + DefChimod12ExeName :
                Chimod12ExeName;
        }
        public string GetChimod12dExePath(string parent_directory)
        {
            return Chimod12dExeName == "default" ?
                parent_directory + "\\" + DefChimod12dExeName :
                Chimod12dExeName;
        }
        public string GetSmoothExePath(string parent_directory)
        {
            return SmoothExeName == "default" ?
                parent_directory + "\\" + DefSmoothExeName :
                 SmoothExeName;
        }
        public string GetSmoothMu0Alph(string parent_directory)
        {
            return Mu0AlphExeName == "default" ?
                parent_directory + "\\" + DefMu0AlphExeName :
                 Mu0AlphExeName;
        }

        public bool SaveUsedPath;
        public bool SaveSelectedDirectory;
        public bool ShowGraphAfterSmooth;
        public string WorkDir;
        public string FeffinpPath, FeffSmoothinpPath;
        public string ExpSmoothinpPath, ExpModPath;
        public string BTinpPath;
        public string Edge;
        public string CriteriaStart;
        public string CriteriaEnd;
        public void SetDefault()
        {
            string[] str = new string[] 
            { SmoothExeName, FeffExeName, Chimod12ExeName, Chimod12dExeName, Mu0AlphExeName };
            for (int i = 0; i < str.Length; i++)
                str[i] = "default";
                    
            SaveUsedPath = false;
            SaveSelectedDirectory = false;
            ShowGraphAfterSmooth = false;
            CriteriaStart = "3.0";
            CriteriaEnd = "1.5";
        }
        public PreferenceData()
        {
            SetDefault();
        }

        //書き出しと読み出し
        public void SetAnalysisSettings(
            RefTextBox workdir,RefTextBox feffinp, RefTextBox feffsmooth
            , RefTextBox expmod, RefTextBox expsmooth
            , RefTextBox btinp, ComboBox target,TextBox criteria_start,TextBox criteria_end)
        {
            FeffSmoothinpPath = feffsmooth.Text;
            FeffinpPath = feffinp.Text;
            ExpSmoothinpPath = expsmooth.Text;
            ExpModPath = expmod.Text;
            BTinpPath = btinp.Text;
            WorkDir = workdir.Text;
            Edge = ((string)target.SelectedItem);
            CriteriaStart = criteria_start.Text;
            CriteriaEnd = criteria_end.Text;

        }
        public void LoadAnalysisSettings(
            RefTextBox workdir, RefTextBox feffinp, RefTextBox feffsmooth
            , RefTextBox expmod, RefTextBox expsmooth
            , RefTextBox btinp, ComboBox target, TextBox criteria_start, TextBox criteria_end)
        {
            feffsmooth.Text = FeffSmoothinpPath;
            feffinp.Text = FeffinpPath;
            expsmooth.Text = ExpSmoothinpPath;
            expmod.Text = ExpModPath;
            btinp.Text = BTinpPath;
            workdir.Text = WorkDir;
            int i = target.Items.IndexOf(Edge);
            if (i >= 0)
                target.SelectedIndex = i;
            criteria_end.Text = CriteriaEnd;
            criteria_start.Text = CriteriaStart;
        }
    }
}
