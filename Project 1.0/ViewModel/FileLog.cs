using System;

namespace Project_1._0.ViewModel
{
    internal class FileLog
    {
        public string FileName { get; set; }
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
        public long FileSize { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan TotalTime { get; set; }
    }

}


