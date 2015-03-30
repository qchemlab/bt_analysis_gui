using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace BayesTurchinAnalysis
{
    partial class BTMainForm
    {
        Thread backthread = null;
        Process ExProgram = null;
        #region file_externalprogram_operation_method
        private bool IsSameFileName(string f1, string f2)
        {
            if (string.IsNullOrEmpty(f1) || string.IsNullOrEmpty(f2))
                return false;
            return Path.GetFullPath(f1) == Path.GetFullPath(f2);
        }
        public static bool MoveFile_Overwrite(string source, string dest)
        {
            if (!File.Exists(source))
                return false;
            if (File.Exists(dest))
                File.Delete(dest);
            File.Move(source, dest);
            return true;
        }
        private void FindSmoothE0from_Xmudat()
        {
            var smoothinp = File.ReadLines("smooth.inp").ToArray();
            var delimiter = new char[]{' ','\t'};
            var ssopt = StringSplitOptions.RemoveEmptyEntries;
            double e0 = -1;

            if( smoothinp.Count() <2)
                return;
            var line2 =  smoothinp[1].Split(delimiter,ssopt).ToArray();
            //E0の値がマイナスの場合は計算で求めた値をつかう
            if(double.Parse( line2[1] ) < 0 )
            {
                foreach( var line in File.ReadLines("xmu.dat") )
                {
                    if (line.StartsWith("#"))
                        continue;

                    var strsplit = line.Split(delimiter, ssopt).ToArray();
                    if (double.Parse(strsplit[2]) == 0.0) 
                    {
                        e0 = double.Parse(strsplit[0]);
                    }
                }
            }
            //e0を書き出し
            using(StreamWriter sw = new StreamWriter("smooth.inp",false,Encoding.ASCII))
            {
                sw.WriteLine(smoothinp[0]);
                sw.WriteLine(smoothinp[1].Replace(line2[1], e0.ToString()));
            }
        }
        //指定プログラムを走らせる
        private void RunExternalProgram(string program)
        {
            try
            {
                AddLog("CurrentDirectory:" + Directory.GetCurrentDirectory());
                ExProgram = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = program;
                ExProgram.StartInfo = startInfo;
                ExProgram.Start();

                // リダイレクトがあったときに呼ばれるイベントハンドラ
                ExProgram.OutputDataReceived += new DataReceivedEventHandler((obj, args) =>
                {
                    //標準出力からログウィンドウに文字列を流し込む
                    this.BeginInvoke(new Action<String>(str =>
                    {
                        AddLog(str);
                    }), args.Data);

                });

                // 非同期ストリーム読み取りの開始
                ExProgram.BeginOutputReadLine();
                ExProgram.WaitForExit();
                ExProgram.CancelOutputRead();
                ExProgram.Close();
                ExProgram = null;
            }
            catch
            {
                AddLog("Error!:exception ocuured in running program!");
            }
        }
        #endregion
        //Feffの起動はマルチスレッドでやります
        public void RunFeffProcess()
        {
            if (!(Directory.Exists(rtb_workdir.Text) &&
                File.Exists(rtb_feffinp.Text) && File.Exists(rtb_feffsmoothinp.Text)))
            {
                AddLog("essential file paths are lacking");
                return;
            }
            SetDefaultCurrentDirectory();
            backthread = new Thread(new ParameterizedThreadStart(SelectExecuteProcess));
            backthread.IsBackground = true;
            backthread.Start();
        }
        public void SelectExecuteProcess(object obj)
        {
            string swstr = "";
            Invoke(new Func<string>(() => swstr = (string)cb_feffselectrunmode.SelectedItem));
            ControlProcessButtons(false, false);

            //規定位置にfeff.inpをコピー
            if (File.Exists(rtb_feffinp.Text) && IsSameFileName(rtb_feffinp.Text, "feff.inp") == false)
                File.Copy(rtb_feffinp.Text, "feff.inp", true);
            File.Copy("feff.inp", "feff.inp.original.tmp",true);

            //エッジ振り分け
            bool is_L = false;
            Invoke(new Func<bool>(() => is_L = (string)cb_fefftargetedge.SelectedItem == "L edge"));
            string s = "";
            Invoke(new Action(() => s = (string)cb_fefftargetedge.SelectedItem));
            Func<bool> Prepare_Directory;
            Action Run_NormalFeff   ;
            Action Run_DeformFeff   ;
            Action Run_CriteriaFeff ; 
            Action Run_Chimod       ;
            Action Run_Smooth       ;
            if(is_L)
            {
                Prepare_Directory = PrepareFolderAndFeffinpL;
                Run_NormalFeff = RunFeffL_Normal;
                Run_DeformFeff = RunFeffL_Deform;
                Run_CriteriaFeff = RunFeffL_Criteria;
                Run_Chimod = RunChimodTxtL;
                Run_Smooth = RunSmoothL; 
            }
            else
            {
                Prepare_Directory = PrepareFolderAndFeffinpK;
                Run_NormalFeff =  RunFeffK_Normal;
                Run_DeformFeff = RunFeffK_Deform;
                Run_CriteriaFeff =  RunFeffK_Criteria;
                Run_Chimod = RunChimodTxtK;
                Run_Smooth = RunSmoothK; 
            }


            switch (swstr)
            {
                case "All Process":
                    AddLog("////////////Run Feff Process + Smooth////////////" + Environment.NewLine);
                    if (Prepare_Directory() == false)
                        return;
                    Run_NormalFeff();
                    Run_DeformFeff();
                    Run_CriteriaFeff();
                    Run_Chimod();
                    Run_Smooth();
                    Invoke(new Action(() => chk_feff.Checked = true));
                    break;
                case "Feff(normal/deformed/criteria)":
                    AddLog("////////////Run feff process////////////" + Environment.NewLine);
                    if (PrepareFolderAndFeffinpK() == false)
                        return;
                    Run_NormalFeff();
                    Run_DeformFeff();
                    Run_CriteriaFeff();
                    break;
                case "Feff(normal/deformed)":
                    AddLog("////////////Run feff (except criteria)////////////" + Environment.NewLine);
                    if (PrepareFolderAndFeffinpK() == false)
                        return;
                    Run_NormalFeff();
                    Run_DeformFeff();
                    break;
                case "Feff(criteria)":
                    AddLog("////////////Run feff process (criteria only)////////////" + Environment.NewLine);
                    if (PrepareFolderAndFeffinpK() == false)
                        return;
                    Run_CriteriaFeff();
                    break;
                case "Chimod":
                    AddLog("////////////Run chimod only////////////" + Environment.NewLine);
                    Run_Chimod();
                    break;
                case "Smooth":
                    AddLog("////////////Run smooth only////////////" + Environment.NewLine);
                    Run_Smooth();
                    Invoke(new Action(() => chk_feff.Checked = true));
                    break;
                case "Feff + Chimod":
                    AddLog("////////////Run feff process + chimod(except smooth)////////////" + Environment.NewLine);
                    if (PrepareFolderAndFeffinpK() == false)
                        return;
                    Run_NormalFeff();
                    Run_DeformFeff();
                    Run_CriteriaFeff();
                    Run_Chimod();
                    break;
                case "Chimod + Smooth":
                    AddLog("////////////Run chimod and smooth////////////" + Environment.NewLine);
                    Run_Chimod();
                    Run_Smooth();
                    Invoke(new Action(() => chk_feff.Checked = true));
                    break;
            }
            AddLog("////////////process finished!!!!!!////////////" + Environment.NewLine);
            ControlProcessButtons(true, false);
            File.Delete("feff.inp.original.tmp");
        }
        #region K_edge
        //K端のプロセス関数
        public bool PrepareFolderAndFeffinpK()
        {
            // K , Kd　, feffdat , chidat , xmudat フォルダの生成
            foreach (string s in new string[] { "K", "Kd", "feffdat", "chidat", "xmudat" })
            {
                if (!Directory.Exists(s))
                    Directory.CreateDirectory(s);
            }
            if (!File.Exists(rtb_feffinp.Text))
            {
                AddLog("Error!:feff.inp is not exist");
                ControlProcessButtons(true,false);
                return false;
            }

            //FEFF EXAFSとXANESがONになっているか確認
            var feffinp = File.ReadLines("feff.inp", BTMainForm.CharEncode);
            using (StreamWriter sw = new StreamWriter("feff.inp.temp", false, BTMainForm.CharEncode))
            {
                foreach (var line in feffinp)
                {
                    var fix = line.Replace("*", "").Trim();
                    if (fix.StartsWith("EXAFS") || fix.StartsWith("XANES"))
                        sw.Write(line.Replace("*", "") + "\n");
                    else
                        sw.Write(line +"\n");
                }
            }
            File.Copy("feff.inp.temp", "feff.inp",true);
            File.Delete("feff.inp.temp");
            

            return true;
        }
        //通常Feff
        private void RunFeffK_Normal()
        {
            AddLog("-------- Run feff(k edge) --------" + Environment.NewLine);
            //feff.inpを規定位置にコピーしてfeffを実行
            File.Copy("feff.inp", "K/feff.inp", true);
            Directory.SetCurrentDirectory("K");
            RunExternalProgram(preference.GetFeffExePath(ExeDirectory));
            AddLog("-------- feff normal finished --------");

            foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory(), "feff*.dat"))
            {
                string s1 = Path.GetFileName(s);
                string s2 = "../feffdat/" + Path.GetFileName(s.Replace("feff", "ff01"));
                MoveFile_Overwrite(s1, s2);
            }
            MoveFile_Overwrite("chi.dat", "../chidat/chi_K.dat");
            MoveFile_Overwrite("xmu.dat", "../xmudat/xmu_K.dat");
            File.Copy("feff.inp", "../feffdat/feff1.inp", true);
            File.Copy("paths.dat", "../feffdat/paths1.dat", true);
            File.Copy("paths.dat", "../chidat/path00_K.dat", true);
            Directory.SetCurrentDirectory("../");
        }
        //変形Feff
        private void RunFeffK_Deform()
        {
            AddLog("-------- Run feff(k edge) deformed structure--------" + Environment.NewLine);
            using (StreamReader sr = new StreamReader("feff.inp", BTMainForm.CharEncode))
            {
                using (StreamWriter sw = new StreamWriter("Kd/feff.inp", false, BTMainForm.CharEncode))
                {
                    string s;
                    //変形用のRMULTIPRIER変数を追加
                    sw.Write("RMULTIPLIER 0.99\n");
                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        if (s.IndexOf("CRITERIA") >= 0)
                            sw.Write("CRITERIA 0.0 0.0\n");
                        else
                            sw.Write(s + "\n");
                    }
                }
            }

            Directory.SetCurrentDirectory("Kd");
            RunExternalProgram(preference.GetFeffExePath(ExeDirectory));
            AddLog("---------- feff deformed finished -----------");

            foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory(), "feff*.dat"))
            {
                string s1 = Path.GetFileName(s);
                string s2 = "../feffdat/" + Path.GetFileName(s.Replace("feff", "ff02"));
                MoveFile_Overwrite(s1, s2);
            }

            MoveFile_Overwrite("chi.dat", "../chidat/chi_Kd.dat");
            MoveFile_Overwrite("xmu.dat", "../xmudat/xmu_Kd.dat");
            File.Copy("paths.dat", "../chidat/path00_Kd.dat", true);
            Directory.SetCurrentDirectory("../");
        }
        //Criteria変えつつ回す
        private void RunFeffK_Criteria()
        {
            double start = 1.5, end = 0.3, step = 0.03;

            try
            {
                start = double.Parse(txt_criteria_start.Text);
                end = double.Parse(txt_criteria_end.Text);
                step = double.Parse(txt_criteria_step.Text);
            }
            catch
            {
                AddLog("criteria read failed , use default value" + Environment.NewLine);
                start = 1.5; end = 0.3; step = 0.03;
            }
            AddLog("-------- Run feff(k edge) criteria--------" + Environment.NewLine);
            //_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
            //      CRITERIA 0.3 to 1.5 step 0.03       K               _/
            //_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
            AddLog(string.Format("CRITERIA {0} to {1} ,{2} step", start, end,step));
            if (start < end)
            {
                AddLog("Error!! : Criteria range start value must be bigger than end value" + Environment.NewLine);
                return;
            }
            Directory.SetCurrentDirectory("K");
            File.Copy("feff.inp", "feff.inp.original", true);

            for (int i = 0; start - step * i >= end; i++)
            {
                double criteria = start - step * i;
                using (StreamWriter sw = new StreamWriter("feff.inp", false, BTMainForm.CharEncode))
                {
                    foreach (string line in File.ReadLines("feff.inp.original", BTMainForm.CharEncode))
                    {
                        if (line.Contains("CRITERIA"))
                            sw.Write("CRITERIA " + (criteria * 2) + " " + criteria + "\n");
                        else if (line.Contains("CONTROL"))
                            sw.Write("CONTROL   0      0     0     1     1      1\n");
                        else
                            sw.Write(line + "\n");
                    }
                }
                //
                AddLog("run feff CRITERIA : " + (start - step * i));
                RunExternalProgram(preference.GetFeffExePath(ExeDirectory));
                AddLog("---------- feff finished -----------");

                MoveFile_Overwrite("chi.dat", "../feffdat/chi_" + String.Format("{0:00}", i) + ".dat");

                if (!Directory.Exists("cri_feff"))
                    Directory.CreateDirectory("cri_feff");
                File.Copy("feff.inp", "cri_feff/feff" + (start - step * i).ToString().Replace(".","") + ".inp",true);

            }
            Directory.SetCurrentDirectory("../");
        }
        private void RunChimodTxtK()
        {
            AddLog("-------- Run chimodtxt(k edge)--------" + Environment.NewLine);
            try
            {
                //ファイルの存在確認
                if (!File.Exists("chidat/chi_K.dat") || !File.Exists("chidat/chi_Kd.dat"))
                {
                    AddLog("Error!:chi_K.dat or chi_Kd.dat is not exist");
                    throw new Exception("chi_K.dat or chi_Kd.dat is not exist");
                }
                //chimodtxtKの中身
                Directory.SetCurrentDirectory("chidat");

                File.Copy("chi_K.dat", "chi1.dat", true);
                File.Copy("chi_Kd.dat", "chi2.dat", true);

                AddLog("----------- start chimod -------------");
                RunExternalProgram( preference.GetChimod12ExePath(ExeDirectory) );
                RunExternalProgram( preference.GetChimod12dExePath(ExeDirectory));
                AddLog("----------- end chimod ----------------");

                if (File.Exists("chi1log.dat"))
                {
                    AddLog(File.ReadAllText("chi1log.dat", BTMainForm.CharEncode));
                }
                if (File.Exists("chi12log.dat"))
                {
                    AddLog(File.ReadAllText("chi12log.dat", BTMainForm.CharEncode));
                }
                File.Copy("chi1m.dat", "../feffdat/chi1m.dat", true);
                File.Copy("chi2m.dat", "../feffdat/chi2m.dat", true);
                Directory.SetCurrentDirectory("../");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void RunSmoothK()
        {
            AddLog("-------- Run smooth --------" + Environment.NewLine);
            try
            {
                Directory.SetCurrentDirectory("xmudat");
                if (!File.Exists(rtb_feffsmoothinp.Text) && !File.Exists("smooth.inp"))
                {
                    AddLog("Error!:smooth.inp is not exist");
                    throw new Exception("smooth.inp is not exist");
                }
                if (IsSameFileName(rtb_feffsmoothinp.Text, "smooth.inp") == false
                    && string.IsNullOrEmpty(rtb_feffsmoothinp.Text) == false)
                    File.Copy(rtb_feffsmoothinp.Text, "smooth.inp", true);

                if (!File.Exists("xmu_K.dat") || !File.Exists("xmu_Kd.dat"))
                {
                    AddLog("xmu_K.dat or xmu_Kd.dat is not exist");
                    throw new Exception("xmu_K.dat or xmu_Kd.dat is not exist");
                }

                File.Copy("xmu_K.dat", "xmu.dat", true);

                FindSmoothE0from_Xmudat();

                RunExternalProgram(ExeDirectory + "\\smooth.exe");
                MoveFile_Overwrite("smooth_out.dat", "../feffdat/mu0_av.dat");
                Directory.SetCurrentDirectory("../");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region L_edge
        //K端のプロセス関数
        public bool PrepareFolderAndFeffinpL()
        {
            // L(n) , L(n)d　, feffdat , chidat , xmudat フォルダの生成
            for (int i = 3; i >= 1; i--)
            {
                if (!Directory.Exists("L" + i ))
                    Directory.CreateDirectory("L" + i);
                if (!Directory.Exists("L" + i + "d"))
                    Directory.CreateDirectory("L"+ i  + "d");
            }
            foreach (string s in new string[] { "feffdat", "chidat", "xmudat" })
            {
                if (!Directory.Exists(s))
                    Directory.CreateDirectory(s);
            }
            //規定位置にfeff.inpをコピー
            if (File.Exists(rtb_feffinp.Text) && IsSameFileName(rtb_feffinp.Text, "feff.inp") == false)
                File.Copy(rtb_feffinp.Text, "feff.inp", true);
            if (!File.Exists(rtb_feffinp.Text))
            {
                AddLog("Error!:feff.inp is not exist");
                ControlProcessButtons(true,false);
                return false;
            }
            return true;
        }
        private void RunFeffL_Normal()
        {
            string[] replacename = new string[]{ "ff11", "ff21", "ff01"};
            //L端用Feffファイルの作成
            for(int i=3;i>=1;i--)
            {
                AddLog("normal FEFF run for L" + i);
                using (StreamWriter sw = new StreamWriter("L" + i + "/feff.inp", false, BTMainForm.CharEncode))
                {
                    foreach(string origline in File.ReadLines("feff.inp",BTMainForm.CharEncode))
                    {
                            if (origline.Contains("EDGE") )
                            {
                                sw.WriteLine("EDGE L"+i+" 0.0");
                            }
                            else if (origline.Contains("CRITERIA") )
                            {
                                sw.WriteLine("CRITERIA 0.0 0.0");
                            }
                            else
                            {
                                sw.WriteLine(origline);
                            }
                    }
                }
                Directory.SetCurrentDirectory("L"+i);
                RunExternalProgram(preference.GetFeffExePath(ExeDirectory));
                foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory(), "feff*.dat"))
                {
                    string s1 = Path.GetFileName(s);
                    string s2 = "../feffdat/" + Path.GetFileName(s.Replace("feff", replacename[i-1]));
                    MoveFile_Overwrite(s1, s2);
                }
                MoveFile_Overwrite("chi.dat", "../chidat/chi_l"+i+".dat");
                MoveFile_Overwrite("xmu.dat", "../xmudat/xmu_l"+i+".dat");
                File.Copy("paths.dat", "../chidat/path00_Kd.dat", true);
                if (i == 3)
                {
                    File.Copy("feff.inp", "../feffdat/feff1.inp", true);
                    File.Copy("paths.dat", "../feffdat/paths1.dat", true);
                }
                    File.Copy("paths.dat","../feffdat/paths00_l"+i+".dat", true);
                Directory.SetCurrentDirectory("../");
            }
        }
        private void RunFeffL_Deform()
        {
            var replacename = new string[]{"ff12","ff22","ff02"};
            for(int i=3;i>=1;i--)
            {
                AddLog("deformed FEFF run for L" + i);
                using(StreamWriter sw = new StreamWriter("L"+i+"d/feff.inp", false, BTMainForm.CharEncode))
                {
                    sw.WriteLine("RMULTIPLIER 0.99");
                    foreach( string origline in File.ReadLines("feff.inp") )
                    {
                        if (origline.Contains("CRITERIA") )
                        {
                            sw.WriteLine("CRITERIA 0.0 0.0");
                        }
                        else
                        {
                            sw.WriteLine(origline);
                        }
                    }
                }
                Directory.SetCurrentDirectory("L"+i+"d");
                RunExternalProgram(preference.GetFeffExePath(ExeDirectory));
                foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory(), "feff*.dat"))
                {
                    string s1 = Path.GetFileName(s);
                    string s2 = "../feffdat/" + Path.GetFileName(s.Replace("feff", replacename[i-1]));
                    MoveFile_Overwrite(s1, s2);
                }

                MoveFile_Overwrite("chi.dat", "../chidat/chi_l"+i+"d.dat");
                MoveFile_Overwrite("xmu.dat", "../xmudat/xmu_l"+i+"d.dat");
                File.Copy("paths.dat","../feffdat/paths00_l"+i+"d.dat", true);
                Directory.SetCurrentDirectory("../");
            }

        }
        private void RunFeffL_Criteria()
        {
            var chinum = new string[] { "1", "2", "" };
            double start = 1.5, end = 0.3, step = 0.03;
            for(int i=3;i>=1;i--)
            {
                AddLog("-------- Run feff(L edge) criteria--------" );
                Directory.SetCurrentDirectory("L"+i);

                File.Copy("feff.inp","feff.inp.original",true);

                for (int k = 0; start - step * k >= end; k++)
                {
                    double criteria = start - step * k;
                    using (StreamWriter sw = new StreamWriter("feff.inp", false, BTMainForm.CharEncode))
                    {
                        foreach (string line in File.ReadLines("feff.inp.original", BTMainForm.CharEncode))
                        {
                            if (line.Contains("CRITERIA"))
                                sw.WriteLine("CRITERIA " + (criteria * 2) + " " + criteria );
                            else if (line.Contains("CONTROL"))
                                sw.WriteLine("CONTROL   0      0     0     0     1      1");
                            else
                                sw.WriteLine(line );
                        }
                    }
                    //
                    AddLog("run feff CRITERIA : " + (start - step * i));
                    RunExternalProgram(preference.GetFeffExePath(ExeDirectory));
                    AddLog("---------- feff finished -----------");

                    MoveFile_Overwrite("chi.dat", "../feffdat/chi" + chinum[i - 1] + "_" + String.Format("{0:00}", k) + ".dat");

                }
                Directory.SetCurrentDirectory("../");
            }
            File.Copy("feff.inp.original", "feff.inp", true);
        }
        private void RunChimodTxtL()
        {
            Directory.SetCurrentDirectory("chidat");

            AddLog("-----L3 and L2-----");
            File.Copy("chi_l3.dat" , "chi1.dat",true );
            File.Copy("chi_l2.dat" , "chi2.dat",true );
            RunExternalProgram(preference.GetChimod12ExePath(ExeDirectory));
            MoveFile_Overwrite( "chi2m.dat" , "chi1m_l2.dat" );
            MoveFile_Overwrite( "chi1m.dat" , "chi1m_l3.dat" );

            AddLog("-----L3 and L1-----");
            File.Copy("chi_l3.dat" , "chi1.dat",true );
            File.Copy("chi_l1.dat" , "chi2.dat",true );
            RunExternalProgram(preference.GetChimod12ExePath(ExeDirectory));
            MoveFile_Overwrite( "chi2m.dat" , "chi1m_l1.dat" );

            
            AddLog("-----L3m and L3d-----");
            File.Copy("chi1m_l3.dat" , "chi1m.dat",true );
            File.Copy("chi_l3d.dat" , "chi2.dat",true );
            RunExternalProgram(preference.GetChimod12dExePath(ExeDirectory));
            MoveFile_Overwrite( "chi2m.dat" , "chi2m_l3.dat" );
            
            AddLog("-----L2m and L2d-----");
            File.Copy("chi1m_l2.dat" , "chi1m.dat",true );
            File.Copy("chi_l2d.dat" , "chi2.dat",true );
            RunExternalProgram(preference.GetChimod12dExePath(ExeDirectory));
            MoveFile_Overwrite( "chi2m.dat" , "chi2m_l2.dat" );
            
            AddLog("-----L1m and L1d-----");
            File.Copy("chi1m_l1.dat" , "chi1m.dat",true );
            File.Copy("chi_l1d.dat" , "chi2.dat",true );
            RunExternalProgram(preference.GetChimod12dExePath(ExeDirectory));
            MoveFile_Overwrite( "chi2m.dat" , "chi2m_l1.dat" );

        }
        private void RunSmoothL()
        {
            AddLog("-------- Run smooth l --------" + Environment.NewLine);
            try
            {
                Directory.SetCurrentDirectory("xmudat");
                if (!File.Exists(rtb_feffsmoothinp.Text) && !File.Exists("smooth.inp"))
                {
                    AddLog("Error!:smooth.inp is not exist");
                    throw new Exception("smooth.inp is not exist");
                }
                if (IsSameFileName(rtb_feffsmoothinp.Text, "smooth.inp") == false
                    && string.IsNullOrEmpty(rtb_feffsmoothinp.Text) == false)
                    File.Copy(rtb_feffsmoothinp.Text, "smooth.inp", true);

                if (!File.Exists("xmu_K.dat") || !File.Exists("xmu_Kd.dat"))
                {
                    AddLog("xmu_K.dat or xmu_Kd.dat is not exist");
                    throw new Exception("xmu_K.dat or xmu_Kd.dat is not exist");
                }

                File.Copy("xmu_K.dat", "xmu.dat", true);
                FindSmoothE0from_Xmudat();
                RunExternalProgram(ExeDirectory + "\\smooth.exe");
                MoveFile_Overwrite("smooth_out.dat", "../feffdat/mu0_av.dat");
                Directory.SetCurrentDirectory("../");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //TODO:次回ここからFeffLのdeform
        #endregion
    }
}
