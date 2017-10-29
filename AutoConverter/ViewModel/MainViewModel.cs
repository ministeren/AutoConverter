using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using NReco.VideoConverter;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading.Tasks;

namespace AutoConverter.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : NotifyPropertyChangedBase
    {       
        private CmdAccess cmda;

        public MainViewModel()
        {
            cmda = new CmdAccess();
            SetInit();
        }

        //private CmdAccess cmda => CmdAccess.Instance;

        private string _filePathName = null;

        private string _fileName;
        public string FileName { get { return _fileName; } set { _fileName = value; if (value.Length > 0) { StartVisible = Visibility.Visible; }; NotifyPropertyChanged(); } }
        
        private string _outFileName;
        public string OutFileName { get { return _outFileName; } set { _outFileName = value;  NotifyPropertyChanged(); } }

        private string _convertArgs;
        public string ConvertArgs { get { return _convertArgs; } set { _convertArgs = value; NotifyPropertyChanged(); } }

        private string _folderName;
        public string FolderName { get { return _folderName; } set { _folderName = value; NotifyPropertyChanged(); } }

        private string _ffmpegfolderName;
        public string FfmpegFolderName { get { return _ffmpegfolderName; } set { _ffmpegfolderName = value; NotifyPropertyChanged(); } }

        private bool _copyvideo;
        public bool CopyVideo { get { return _copyvideo; } set { _copyvideo = value; setEnabled(); NotifyPropertyChanged(); } }

        private bool _copyaudio;
        public bool CopyAudio { get { return _copyaudio; } set { _copyaudio = value; setEnabled(); NotifyPropertyChanged(); } }

        private bool _copysub;
        public bool CopySub { get { return _copysub; } set { _copysub = value; setEnabled(); NotifyPropertyChanged(); } }
        
        private bool _map0;
        public bool Map0 { get { return _map0; } set { _map0 = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _map0enabled;
        public bool Map0enabled { get { return _map0enabled; } set { _map0enabled = value; NotifyPropertyChanged(); } }

        private bool _map1;
        public bool Map1 { get { return _map1; } set { _map1 = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _map1enabled;
        public bool Map1enabled { get { return _map1enabled; } set { _map1enabled = value; NotifyPropertyChanged(); } }

        private bool _map2;
        public bool Map2 { get { return _map2; } set { _map2 = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _map2enabled;
        public bool Map2enabled { get { return _map2enabled; } set { _map2enabled = value; NotifyPropertyChanged(); } }

        private bool _map3;
        public bool Map3 { get { return _map3; } set { _map3 = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _map3enabled;
        public bool Map3enabled { get { return _map3enabled; } set { _map3enabled = value; NotifyPropertyChanged(); } }

        private bool _deinterlace;
        public bool Deinterlace { get { return _deinterlace; } set { _deinterlace = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _deinterlaceEnabled;
        public bool DeinterlaceEnabled { get { return _deinterlaceEnabled; } set { _deinterlaceEnabled = value; NotifyPropertyChanged(); } }

        private bool _codecx264;
        public bool Codecx264 { get { return _codecx264; } set { _codecx264 = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _codecx264enabled;
        public bool Codecx264enabled { get { return _codecx264enabled; } set { _codecx264enabled = value; NotifyPropertyChanged(); } }

        private bool _codecAC3;
        public bool CodecAC3 { get { return _codecAC3; } set { _codecAC3 = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _codecAC3enabled;
        public bool CodecAC3enabled { get { return _codecAC3enabled; } set { _codecAC3enabled = value; NotifyPropertyChanged(); } }

        private bool _mkvout;
        public bool MkvOut { get { return _mkvout; } set { _mkvout = value; if (value) { Mp4Out = false;}; updateCustomArgs(); NotifyPropertyChanged(); } }

        private bool _mp4out;
        public bool Mp4Out { get { return _mp4out; } set { _mp4out = value; if (value) { MkvOut = false; }; updateCustomArgs(); NotifyPropertyChanged(); } }
        
        private int _progressValue;
        public int ProgressValue { get { return _progressValue; } set { _progressValue = value; updateCustomArgs(); NotifyPropertyChanged(); } }

        private Visibility _startVisible;
        public Visibility StartVisible { get { return _startVisible; } set { _startVisible = value; if (value.Equals(Visibility.Visible)) { ProgressVisible = Visibility.Hidden; }; NotifyPropertyChanged(); } }

        private Visibility _progressVisible;
        public Visibility ProgressVisible { get { return _progressVisible; } set { _progressVisible = value; if (value.Equals(Visibility.Visible)) { StartVisible = Visibility.Hidden; }; NotifyPropertyChanged(); } }

        public ICommand Convert { get { return new RelayCommand(StartConvert); } }

        public ICommand ChooseFile { get { return new RelayCommand(setFile); } }

        //public ICommand ChooseFolder { get { return new RelayCommand(setFolder); } }

        //public ICommand ChooseFfmpegFolder { get { return new RelayCommand(setFfmpegFolder); } }

        private void StartConvert()
        {
            Task T = new Task(() => ExeConv());
            T.Start();
        }

        //FFMpegConverter conv = new FFMpegConverter();
        //ConvertSettings convSet = new ConvertSettings();

        private void ExeConv()
        {

            Debug.WriteLine(ConvertArgs);

            FFMpegConverter conv = new FFMpegConverter();

            conv.ConvertProgress += UpdateProgress;

            ConvertSettings convSet = new ConvertSettings();

            convSet.CustomOutputArgs = ConvertArgs;

            string outExtension = "";

            string outFormat = "";

            string inFormat = "";

            if (MkvOut)
            {
                outExtension = ".mkv";
                outFormat = Format.matroska;
            } else if (Mp4Out)
            {
                outExtension = ".mp4";
                outFormat = Format.mp4;
            }

            if (_filePathName.Contains("mkv"))
            {
                inFormat = Format.matroska;
            }
            else if (_filePathName.Contains("mp4"))
            {
                inFormat = Format.mp4;
            }

            string outName = OutFileName + outExtension;

            //ConsoleAllocator.ShowConsoleWindow();

            //ProgressVisible = Visibility.Visible;
            try
            {
                conv.ConvertMedia(_filePathName, inFormat, _filePathName.Remove(_filePathName.Length-FileName.Length) + outName, outFormat, convSet);
            }
            catch (FFMpegException e)
            {
                Debug.WriteLine(e.StackTrace);
            }

            StartVisible = Visibility.Visible;

            //ConsoleAllocator.HideConsoleWindow();
        }

        private void UpdateProgress(object sender, ConvertProgressEventArgs e)
        {
            if (ProgressVisible.Equals(Visibility.Hidden))
            {
                ProgressVisible = Visibility.Visible;
            }



            ProgressValue = (int)((e.Processed.TotalSeconds / e.TotalDuration.TotalSeconds) * 100);

            //Debug.WriteLine("");
            //Debug.WriteLine("\rProcessed: " + e.Processed.TotalSeconds);
            //Debug.WriteLine("\rTotalDuration: " + e.TotalDuration.TotalSeconds);
            //Debug.WriteLine("");
            //Console.Write("\rTotalDuration: " + e.TotalDuration);
        }

        private void UpdateBar(ConvertProgressEventArgs e)
        {

        }

        private void SetInit()
        {
            FileName = "VÆLG FIL";
            FolderName = "VÆLG MAPPE";
            FfmpegFolderName = "VÆLG MAPPE";
            CopyVideo = true;
            CopyAudio = true;
            CopySub = true;
            Codecx264 = false;
            MkvOut = true;
            StartVisible = Visibility.Hidden;
            ProgressVisible = Visibility.Hidden;
            setEnabled();
            //conv.ConvertProgress += UpdateProgress;
        }

        private void setEnabled()
        {
            if (CopyVideo && CopyAudio && CopySub)
            {
                Map0enabled = false;
                Map0 = false;
                Map1enabled = false;
                Map1 = false;
                Map2enabled = false;
                Map2 = false;
                Map3enabled = false;
                Map3 = false;
                DeinterlaceEnabled = false;
                Deinterlace = false;
                Codecx264enabled = false;
                Codecx264 = false;
                CodecAC3enabled = false;
                CodecAC3 = false;
            } else
            {
                Map0enabled = true;
                Map1enabled = true;
                Map2enabled = true;
                Map3enabled = true;
                DeinterlaceEnabled = true;
                Codecx264enabled = true;
                Codecx264 = false;
                CodecAC3enabled = true;
                CodecAC3 = false;
            }
            updateCustomArgs();
        }

        private void updateCustomArgs()
        {
            ConvertArgs = makeCustomOutputArgs();
        }

        private string makeCustomOutputArgs()
        {
            string video = "";
            string audio = "";
            string sub = "";
            string deinterlace = "";
            string outFormat = "";
            string map0 = "";
            string map1 = "";
            string map2 = "";
            string map3 = "";

            if (CopyVideo)
            {
                video = "-c:v copy ";
            } else if (Codecx264)
            {
                video = "-c:v libx264 -preset superfast ";
            }
            if (CopyAudio)
            {
                audio = "-c:a copy ";
            } else if (CodecAC3)
            {
                audio = "-c:a ac3_fixed ";
            }
            if (CopySub)
            {
                sub = "-c:s copy ";
            }
            if (Deinterlace)
            {
                deinterlace = "-vf yadif ";
            }
            if (Map0)
            {
                map0 = "-map 0:0 ";
            }
            if (Map1)
            {
                map1 = "-map 0:1 ";
            }
            if (Map2)
            {
                map2 = "-map 0:2 ";
            }
            if (Map3)
            {
                map3 = "-map 0:3 ";
            }
            //if (MkvOut)
            //{
            //    outFormat = "mkv";
            //}
            //if (Mp4Out)
            //{
            //    outFormat = "mp4";
            //}

            StringBuilder sb = new StringBuilder();
            sb.Append(video).Append(audio).Append(sub).Append(deinterlace).Append(outFormat).Append(map0).Append(map1).Append(map2).Append(map3);

            return sb.ToString();
            //return "for %G in (\"*.mkv\") do \"D:\\Program Files\\ffmpeg-3.2.2-win64-shared\\bin\\ffmpeg\" -i %G " + deinterlace + " -c:v libx264 -preset superfast " + map0 + map1 + map2 + map3 + "-c:a copy -c:s copy %~nG." + mkvout;
            //return "for %G in (\"*.mkv\") do " + ffmpeg + " -i \"%G\" " + deinterlace + map0 + map1 + map2 + map3 + video + audio + sub + "\"%~nG_new." + outFormat + "\"";
        }

        private void setFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();

            Debug.WriteLine("File name: " + ofd.SafeFileName);

            if (!string.IsNullOrWhiteSpace(ofd.SafeFileName))
            {
                string name = ofd.SafeFileName;
                FileName = name;
                OutFileName = name.Remove(name.Length - 4, 4) + "_out";
                _filePathName = ofd.FileName;
            }
        }

        //private void setFolder()
        //{
        //    FolderBrowserDialog fbd = new FolderBrowserDialog();
        //    DialogResult result = fbd.ShowDialog();
            
        //    if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //    {
        //        cmda.setVideoPath(fbd.SelectedPath);
                
        //        FolderName = fbd.SelectedPath.Substring(fbd.SelectedPath.LastIndexOf("\\"));
        //    }
        //}

        //private void setFfmpegFolder()
        //{
        //    FolderBrowserDialog fbd = new FolderBrowserDialog();

        //    DialogResult result = fbd.ShowDialog();            

        //    if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //    {
        //        FfmpegFolderName = fbd.SelectedPath;
        //    }
        //}
    }

    internal static class ConsoleAllocator
    {
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport(@"kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SwHide = 0;
        const int SwShow = 5;


        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SwShow);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SwHide);
        }
    }
}