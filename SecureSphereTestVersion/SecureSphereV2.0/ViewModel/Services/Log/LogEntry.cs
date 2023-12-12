﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSphereV2._0.ViewModel.Services.Log
{
    public class LogEntry
    {
        public string CopyName { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; }
        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public string SourceFile { get; set; }
        public string DestinationFile { get; set; }
        public string ErrorMessage { get; set; }
        public long FileSize { get; set; }


    }
}
