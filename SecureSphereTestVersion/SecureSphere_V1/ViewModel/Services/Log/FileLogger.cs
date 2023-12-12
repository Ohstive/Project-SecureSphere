using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SecureSphere_V1.ViewModel.Services.Log
{
    internal class FileLogger : ILoger
    {
        private readonly string logDirectory;
        private readonly string logStatutDirectory;

        private readonly List<LogEntry> logEntries;
        private readonly List<LogStatutEntry> logStatutEntries;

        public FileLogger(string logDirectory, string logStatutDirectory)
        {
            this.logDirectory = logDirectory;
            this.logStatutDirectory = logStatutDirectory;

            this.logEntries = new List<LogEntry>();
            this.logStatutEntries = new List<LogStatutEntry>();
        }

        public void LogDaily(LogEntry logEntry)
        {
            try
            {
                // Accumulate log entries in memory
                logEntries.Add(logEntry);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during logging
                Console.WriteLine($"Error during logging: {ex.Message}");
            }
        }

        public void LogStatut(LogStatutEntry logStatut)
        {
            try
            {
                // Accumulate log entries in memory
                logStatutEntries.Add(logStatut);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during logging
                Console.WriteLine($"Error during logging: {ex.Message}");
            }
        }

        public void WriteLogsToFile()
        {
            try
            {
                // Generate a timestamp-based file name
                string fileName = $"Daily_log_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                string logFilePath = Path.Combine(logDirectory, fileName);

                // Serialize all accumulated log entries to JSON and write to the log file
                string logJson = JsonConvert.SerializeObject(logEntries, Formatting.Indented);
                File.WriteAllText(logFilePath, logJson);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during writing to the log file
                Console.WriteLine($"Error during writing logs to file: {ex.Message}");
            }
        }

        public void WriteLogsToFileStatut()
        {
            try
            {
                // Generate a timestamp-based file name
                string fileName = $"log_Status_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                string logFilePath = Path.Combine(logStatutDirectory, fileName);

                // Serialize all accumulated log entries to JSON and write to the log file
                string logJson = JsonConvert.SerializeObject(logStatutEntries, Formatting.Indented);
                File.WriteAllText(logFilePath, logJson);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during writing to the log file
                Console.WriteLine($"Error during writing logs to file: {ex.Message}");
            }
        }
    }

}
