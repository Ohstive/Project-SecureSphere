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
        private readonly IDirectoryHandler _directoryHandler;
        private readonly IFileHandler _fileHandler;
        private readonly Jobs _jobs;

        //directoryHandler.FullCopyDirectory(sourceDirectoryPath, targetDirectoryPath);
        /*
        JobManager(Jobs jobs)
        {
            _jobs = jobs;
            IFileHandler fileHandler = new FileHandler();
            _directoryHandler = new DirectoryHandler(fileHandler);

        }

        public void JobFullCopy()
        {
            _directoryHandler.FullCopyDirectory(_jobs.JobConfiguration.SourceDirectoryPath, _jobs.JobConfiguration.TargetDirectoryPath);
        }

        public void JobDifferentialCopy()
        {
            _directoryHandler.DifferentialCopyDirectory(_jobs.JobConfiguration.SourceDirectoryPath, _jobs.JobConfiguration.TargetDirectoryPath);
        }
        */
    }
}
