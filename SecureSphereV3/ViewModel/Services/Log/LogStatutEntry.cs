namespace SecureSphereV2.ViewModel.Services.Log
{
    public class LogStatutEntry
    {
        public string CopyName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TotalTime { get; set; }
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
