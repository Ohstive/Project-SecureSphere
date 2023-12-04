using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel
{
    internal class FolderLog
    {
        public string LogName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public long TotalFolderSize { get; set; }
        public long TotalFileSize { get; set; }
    }
}
