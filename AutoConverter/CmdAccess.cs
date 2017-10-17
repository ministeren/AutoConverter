using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoConverter
{
    public class CmdAccess : NotifyPropertyChangedBase
    {
        public CmdAccess()
        {
            //psi.RedirectStandardInput = true;
            //psi.RedirectStandardOutput = true;
            //psi.CreateNoWindow = false;
            //Process pro = Process.Start(psi);
            //StreamWriter sw = pro.StandardInput;
            //sw.WriteLine("ipconfig");            
            //sw.Close();
            //Console.ReadLine();

            //var p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = "/c echo Foo && echo Bar";
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.CreateNoWindow = true;
            //p.Start();
        }

        //private static readonly CmdAccess _self = new CmdAccess();
        //public static CmdAccess Instance => _self;

        private string videoPath = "";

        

        public void runConv()
        {
            
            //string command = "for %G in (\"*.mkv\") do \"D:\\Program Files\\ffmpeg-3.2.2-win64-shared\\bin\\ffmpeg\" -i %G -vf yadif -c:v libx264 -preset superfast -map 0:0 -map 0:1 -map 0:2 -map 0:3 -c:a copy -c:s copy %~nG.mp4";
            string command = "for %G in (\"*.mkv\") do \"C:\\Programmer\\ffmpeg322\\bin\\ffmpeg\" -i \"%G\" -vf yadif -c:v libx264 -preset superfast -map 0:0 -map 0:1 -map 0:2 -map 0:3 -c:a copy -c:s copy \"%~nG.mp4\"";

            runProcess(command);
        }

        public void runConv(string command)
        {
            runProcess(command);
        }

        private void runProcess(string command)
        {
            Console.WriteLine(command);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.UseShellExecute = false;
            psi.FileName = "cmd.exe";
            //psi.WorkingDirectory = "D:\\conv";
            psi.WorkingDirectory = videoPath;
            //psi.Arguments = "/c " + command;
            psi.Arguments = "/k " + command;
            Process.Start(psi);
        }

        public void setVideoPath(string vpath)
        {
            videoPath = vpath;
        }
    }

    
}
