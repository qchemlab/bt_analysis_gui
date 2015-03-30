using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace BayesTurchinAnalysis
{
    partial class BTMainForm
    {
        public void RunExpSmooth()
        {
            AddLog("-----------------\n"+
                   "Run smoothing process to experimetal data\n"+
                   "-----------------");
            if (!Directory.Exists("expdata"))
                Directory.CreateDirectory("expdata");

            Directory.SetCurrentDirectory("expdata");
            if (Path.GetFullPath(rtb_modexpdata.Text) != Path.GetFullPath("smooth_inp.dat"))
                File.Copy(rtb_modexpdata.Text, "smooth_inp.dat", true);
            if (Path.GetFullPath(rtb_expsmoothinp.Text) != Path.GetFullPath("smooth.inp"))
            File.Copy(rtb_expsmoothinp.Text, "smooth.inp", true);

            //smoothを走らせる
            RunExternalProgram(preference.GetSmoothExePath(ExeDirectory));

            File.Copy("smooth_out.dat", "../feffdat/muexp_av.dat", true);

            Directory.SetCurrentDirectory("../");
            AddLog("-----------------\n" + "smooth finished\n" + "-----------------\n");

        }

        public void RunBT()
        {
            SetDefaultCurrentDirectory();
            ControlProcessButtons(false, true);
            backthread = new Thread(new ParameterizedThreadStart(ExecuteBTexe));
            backthread.IsBackground = true;
            backthread.Start();
        }
        public void ExecuteBTexe(object o)
        {
            AddLog("//////// Run BT.exe ////////");
            try
            {
                if (File.Exists(rtb_btinp.Text))
                    File.Copy(rtb_btinp.Text, "feffdat/BTinp.dat", true);
                Directory.SetCurrentDirectory("feffdat");
                RunExternalProgram(ExeDirectory + "\\bt.exe");
                Directory.SetCurrentDirectory("../");
                AddLog("//////// BT.exe success ////////");
            }
            catch { AddLog("BT.exe Failed"); }
            finally
            {
                ControlProcessButtons(true, true);
            }
        }
        public void RunAllProcess(object o)
        {
            try{
                AddLog("-----------------------"+Environment.NewLine+
                       "All Process "+Environment.NewLine+
                       "-----------------------");
                RunFeffProcess();
                backthread.Join();
                RunExpSmooth();
                RunBT();
                backthread.Join();
                AddLog("-----------------------"+Environment.NewLine+
                       "All Process Finished!!!!"+Environment.NewLine+
                       "-----------------------");
            }
            catch
            {
                AddLog("all process suspended!!!");
            }
        }
        
    }
}
