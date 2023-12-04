using Project_1._0.Model;
using Project_1._0.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel
{
    internal class JobManager
    {
     
        private string logDirectory { get; set;}
        private readonly Jobs _jobs;
        IFileHandler _fileHandler ; 
        IDirectoryHandler _directoryHandler ;


        public JobManager(Jobs jobs)
        {
            this._jobs = jobs;
            this._fileHandler = new FileHandler();
            this._directoryHandler = new DirectoryHandler(_fileHandler);
            
        }

        public void SetLogDirectory(string path)
        {
            logDirectory = path;
        }
   
        public void JobRun()
        {
            if (_jobs.JobConfiguration.BackupType == 0)
            {
                JobFullCopy();
            }
            else
            {
                JobDifferentialCopy();
            }
        }

        public void JobFullCopy()
        {
            if (_jobs.GetSourceType() == 0)
            {
                this._fileHandler.CopyFileFull(this._jobs.JobConfiguration.SourceDirectoryPath, this._jobs.JobConfiguration.TargetDirectoryPath, this._jobs.AllFileInSourcesList[0]);
                Console.WriteLine($"Full copy done at {DateTime.Now}");
            }
            else
            {
                _directoryHandler.FullCopyDirectory(this._jobs.JobConfiguration.SourceDirectoryPath, this._jobs.JobConfiguration.TargetDirectoryPath);
                Console.WriteLine($"Full copy done at {DateTime.Now}");
            }
            
        }


        public void JobDifferentialCopy()
        {
            if (this._jobs.GetSourceType() == 0)
            {
                this._fileHandler.CopyFileDifferential(this._jobs.JobConfiguration.SourceDirectoryPath, this._jobs.JobConfiguration.TargetDirectoryPath, this._jobs.AllFileInSourcesList[0]);
                Console.WriteLine($"Differential copy done at {DateTime.Now}");
            }
            else
            {
                _directoryHandler.DifferentialCopyDirectory(this._jobs.JobConfiguration.SourceDirectoryPath, this._jobs.JobConfiguration.TargetDirectoryPath);
                Console.WriteLine($"Differential copy done at {DateTime.Now}");
            }
        }

    }
}
