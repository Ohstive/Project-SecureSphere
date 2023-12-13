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

    internal class JobManager
    {
        private string logDirectory;
        private string logStatutDirectory;

        private List<LogEntry> logEntries;
        private List<LogStatutEntry> logStatutEntries ;

        private ILoger logger;
        private  ILoger loggerStatus;

        ICopyFileHandler fileCopyHandler;

        DirectoryCopyHandler directoryCopyHandler;
        JobConfiguration jobConfiguration;

        public JobManager(JobConfiguration job, string logDirectory,string logStatutDirectory)
        {
            logEntries = new List<LogEntry>();
            logStatutEntries=new List<LogStatutEntry>();
            this.logDirectory = logDirectory;
            this.logStatutDirectory = logStatutDirectory;
            logger = new FileLogger(logDirectory, logDirectory);
            loggerStatus = new FileLogger(logDirectory, logStatutDirectory);

            jobConfiguration = job;
            fileCopyHandler = new SimpleFileCopy(logger);
            directoryCopyHandler = new DirectoryCopyHandler(fileCopyHandler, logger, loggerStatus);
        }

        public void JobRun()
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
        }

        public void JobFullCopy()
        {
            directoryCopyHandler.FullCopyDirectory(jobConfiguration.JobName, jobConfiguration.SourceDirectoryPath, jobConfiguration.TargetDirectoryPath);
            logger.WriteLogsToFile();
            loggerStatus.WriteLogsToFileStatut();

        }

    }
}
