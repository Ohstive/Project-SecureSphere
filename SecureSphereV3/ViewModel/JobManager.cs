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

using System.Collections.ObjectModel;

namespace SecureSphereV2.ViewModel
{

    public class JobManager
    {
        private string _logDirectory;
        private string _logStatutDirectory;


        private List<LogEntry> _logEntries;
        private List<LogStatutEntry> _logStatutEntries;


        private ILoger _logger;
        private ILoger _loggerStatus;

        private ICopyFileHandler _fileCopyHandler;

        private DirectoryCopyHandler _directoryCopyHandler;
        private JobConfiguration _jobConfiguration;

        private string _cryptoCopyPath;



        public JobManager(JobConfiguration job, string logDirectory, string logStatutDirectory, ObservableCollection<string> extensioncrypted, ObservableCollection<string> extensionPriority)
        {
            _logEntries = new List<LogEntry>();
            _logStatutEntries = new List<LogStatutEntry>();
            this._logDirectory = logDirectory;
            this._logStatutDirectory = logStatutDirectory;
            _logger = new FileLogger(logDirectory, logDirectory);
            _loggerStatus = new FileLogger(logDirectory, logStatutDirectory);


            _cryptoCopyPath = @"CryptoSoft\CryptoSoft.exe";


            _jobConfiguration = job;

            // Define the type of copy
            if (_jobConfiguration.IsEncryptionEnabled)
            {
                _fileCopyHandler = new CryptoCopy(_logger,_cryptoCopyPath,job.EncryptionKey, extensioncrypted);
            }
            else
            {
                _fileCopyHandler = new SimpleFileCopy(_logger);
                //fileCopyHandler = new CopyWithProgress(logger);
            }

            _directoryCopyHandler = new DirectoryCopyHandler(_fileCopyHandler, _logger, _loggerStatus, extensionPriority);
        }

        public void RunJob()
        {
            if (_jobConfiguration.BackupType == "Full")
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
            _directoryCopyHandler.DifferentialCopyDirectory(_jobConfiguration.JobName, _jobConfiguration.SourceDirectoryPath, _jobConfiguration.TargetDirectoryPath);
            _logger.WriteLogsToFile();
            _loggerStatus.WriteLogsToFileStatut();
            _jobConfiguration.IsFinished = true;
        }

        public void JobFullCopy()
        {
            _directoryCopyHandler.FullCopyDirectory(_jobConfiguration.JobName, _jobConfiguration.SourceDirectoryPath, _jobConfiguration.TargetDirectoryPath);
            _logger.WriteLogsToFile();
            _loggerStatus.WriteLogsToFileStatut();
            _jobConfiguration.IsFinished = true;
        }

        public bool JobIsValid()
        {
            if (_jobConfiguration.IsSourceDirectory)
            {
                System.IO.DirectoryInfo SourceDirectory = new System.IO.DirectoryInfo(_jobConfiguration.SourceDirectoryPath);
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
                FileInfo file = new FileInfo(_jobConfiguration.SourceDirectoryPath);
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