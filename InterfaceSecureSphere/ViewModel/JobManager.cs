using InterfaceSecureSphere.ViewModel.Services.CopyFile;
using InterfaceSecureSphere.ViewModel.Services.Directory;
using InterfaceSecureSphere.ViewModel.Services.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceSecureSphere.Model;

namespace InterfaceSecureSphere.ViewModel
{
    
    internal class JobManager
    {
        string logDirectory ;
        string logStatutDirectory;

        List<LogEntry> logEntries;
        List<LogStatutEntry> logStatutEntries = new List<LogStatutEntry>();

        ILoger logger ;
        ILoger loggerStatus;

        ICopyFileHandler fileCopyHandler;


        // Example usage: Copy entire directory with differential copy
        string sourceDirectory = @"C:\Users\Ostiv\Documents\Test3";
        string targetDirectory = @"C:\Users\Ostiv\Documents\Test5";

        DirectoryCopyHandler directoryCopyHandler;

   
        JobConfiguration jobConfiguration;

       
        


        public JobManager(JobConfiguration job ,string logDirectory)
        {
            logEntries = new List<LogEntry>();
            this.logDirectory=logDirectory;
            this.logStatutDirectory=logDirectory;
            logger = new FileLogger(logDirectory, logStatutDirectory);
            loggerStatus = new FileLogger(logDirectory, logStatutDirectory);

            jobConfiguration = job;
            fileCopyHandler = new SimpleFileCopy(logger);

            directoryCopyHandler = new DirectoryCopyHandler(fileCopyHandler, logger, loggerStatus);
        }

        public void RunJob()
        {
            if (jobConfiguration.BackupType== "Differential")
            {
                JobDifferentialCopy();
            }
            else
            {
                JobFullCopy();
            }
        }

        public void JobDifferentialCopy()
        {
           directoryCopyHandler.DifferentialCopyDirectory(jobConfiguration.JobName, jobConfiguration.SourceDirectoryPath, jobConfiguration.TargetDirectoryPath);
            //logger.WriteLogsToFile();
            //logger.WriteLogsToFileStatut();
        }

        public void JobFullCopy()
        {
            directoryCopyHandler.FullCopyDirectory(jobConfiguration.JobName, jobConfiguration.SourceDirectoryPath, jobConfiguration.TargetDirectoryPath);
            //logger.WriteLogsToFile();
            //logger.WriteLogsToFileStatut();
        }


    }
}
