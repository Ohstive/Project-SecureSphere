namespace SecureSphereV2.ViewModel.Services.Log
{
    internal interface ILoger
    {
        void LogDaily(LogEntry logEntry);

        void LogStatut(LogStatutEntry logStatut);

        void WriteLogsToFile();

        void WriteLogsToFileStatut();
    }
}
