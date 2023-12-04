
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

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

        public List<DirectoryLog> DirectoryLogs { get; set; } = new List<DirectoryLog>();
        public List<FileLog> FileLogs { get; set; } = new List<FileLog>();

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }
    }

}
