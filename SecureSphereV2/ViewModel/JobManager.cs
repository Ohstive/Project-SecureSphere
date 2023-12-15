using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureSphereV2.Model;
using SecureSphereV2.ViewModel.Services;
using SecureSphereV2.ViewModel.Services.CopyFile;
using SecureSphereV2.ViewModel.Services.Directory;
using SecureSphereV2.ViewModel.Services.Log;



namespace SecureSphereV2.ViewModel
{

    public class JobManager
    {
        private string logDirectory;
        private string logStatutDirectory;

        private List<LogEntry> logEntries;
        private List<LogStatutEntry> logStatutEntries;

        private ILoger logger;
        private ILoger loggerStatus;

        ICopyFileHandler fileCopyHandler;

        DirectoryCopyHandler directoryCopyHandler;
        JobConfiguration jobConfiguration;

        public JobManager(JobConfiguration job, string logDirectory, string logStatutDirectory)
        {
            logEntries = new List<LogEntry>();
            logStatutEntries = new List<LogStatutEntry>();
            this.logDirectory = logDirectory;
            this.logStatutDirectory = logStatutDirectory;
            logger = new FileLogger(logDirectory, logDirectory);
            loggerStatus = new FileLogger(logDirectory, logStatutDirectory);

            jobConfiguration = job;

            // Define the type of copy
            if (jobConfiguration.IsEncryptionEnabled)
            {
                fileCopyHandler = new CryptoCopy(logger);
            }
            else
            {
                fileCopyHandler = new SimpleFileCopy(logger);
            }

            directoryCopyHandler = new DirectoryCopyHandler(fileCopyHandler, logger, loggerStatus);
        }

        public void RunJob()
        {
            if (jobConfiguration.BackupType == "Full")
            {
                JobFullCopy();
            }
            else
            {
                JobDifferentialCopy();
            }
        }

        public void JobDifferentialCopy()
        {
            directoryCopyHandler.DifferentialCopyDirectory(jobConfiguration.JobName, jobConfiguration.SourceDirectoryPath, jobConfiguration.TargetDirectoryPath);
            logger.WriteLogsToFile();
            loggerStatus.WriteLogsToFileStatut();
            jobConfiguration.IsFinished = true;
        }

        public void JobFullCopy()
        {
            directoryCopyHandler.FullCopyDirectory(jobConfiguration.JobName, jobConfiguration.SourceDirectoryPath, jobConfiguration.TargetDirectoryPath);
            logger.WriteLogsToFile();
            loggerStatus.WriteLogsToFileStatut();
            jobConfiguration.IsFinished = true;
        }

        public bool JobIsValid()
        {
            if (jobConfiguration.IsSourceDirectory)
            {
                System.IO.DirectoryInfo SourceDirectory = new System.IO.DirectoryInfo(jobConfiguration.SourceDirectoryPath);
                IEnumerable<System.IO.FileInfo> list1 = SourceDirectory.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                foreach (System.IO.FileInfo fi in list1)
                {
                    string Extension = fi.Extension;
                    if (ExtensionIsExecutable(Extension))
                    {
                        return false;
                    }
                }

            }
            else
            {
                FileInfo file = new FileInfo(jobConfiguration.SourceDirectoryPath);
                if (ExtensionIsExecutable(file.Extension))
                {
                    return false;
                }
            }
            return true;
        }


        private bool ExtensionIsExecutable(string extension)
        {
            string[] extensionsExecutables =
            {
                ".exe", ".bat", ".cmd", ".com", ".msi", ".ps1", ".vbs",
                ".js", ".jar", ".ws", ".wsf", ".psm1", ".psd1", ".ps1xml",
                ".scr", ".dll", ".app", ".vb", ".vbe", ".jse", ".rbw", ".vb",
                ".vbe", ".vbs", ".vbScript", ".sh", ".run", ".out", ".appref-ms",
                ".ws", ".wsf", ".appx", ".appxbundle", ".bat", ".cmd"
            };
            return Array.Exists(extensionsExecutables, e => e.Equals(extension, StringComparison.OrdinalIgnoreCase));
        }

    }
}