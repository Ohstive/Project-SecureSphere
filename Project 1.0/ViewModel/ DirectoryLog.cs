using System;

namespace Project_1._0.ViewModel
{
    internal class DirectoryLog
    {
        public string Status { get; set; }
        public string LogName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan TotalTime { get; set; }
    }

}

