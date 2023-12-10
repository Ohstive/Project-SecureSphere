using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSecureSphere.ViewModel.Services.Log
{
    public class LogStatutEntry
    {
        public string CopyName { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; }
        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public long TotalSizeToCopy { get; set; }
        public long TotalSizeCopied { get; set; }
        public long TotalFileToCopy { get; set; }
        public long TotalFileCopied { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

    }
}
