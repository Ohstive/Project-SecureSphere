namespace SecureSphereV2.ViewModel.Services.Log
{
    //LogEntry is a class that contains all the information that will be logged in the log file
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
        public TimeSpan EncryptionTime { get; set; }

    }
}
