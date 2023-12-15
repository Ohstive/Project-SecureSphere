using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureSphere_V1.Model;
using SecureSphere_V1.ViewModel.Services;
using SecureSphere_V1.ViewModel.Services.CopyFile;
using SecureSphere_V1.ViewModel.Services.Directory;
using SecureSphere_V1.ViewModel.Services.Log;
using System.IO;

namespace SecureSphere_V1.ViewModel
{

    internal class JobManager
    {

        // Log manager
        private string logDirectory;
        private string logStatutDirectory;

        private List<LogEntry> logEntries;
        private List<LogStatutEntry> logStatutEntries ;

        private ILoger logger;
        private  ILoger loggerStatus;

        // Copy manager
        ICopyFileHandler fileCopyHandler;


        // Job manager
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

            //fileCopyHandler = new SimpleFileCopy(logger);
            fileCopyHandler = new CryptoCopy(logger);
            directoryCopyHandler = new DirectoryCopyHandler(fileCopyHandler, logger, loggerStatus);
            jobConfiguration = job;
           
            
        }

        public void RunJob()
        {
          if (jobConfiguration.BackupType=="Full")
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
