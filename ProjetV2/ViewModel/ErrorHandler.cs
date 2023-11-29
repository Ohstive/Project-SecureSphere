using ProjetV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetV2.ViewModel
{
    internal class ErrorHandler
    {
        //
        Jobs BackupJob;
        //
        ErrorCatchException BackupJobErrorCatchException;
        public ErrorHandler(Jobs jobs, ErrorCatchException errorCatchException) 
        { 
            //
            BackupJob = jobs;
            //
            BackupJobErrorCatchException = errorCatchException;
        }

        public void PathSourceDirectoryExists()
        {
            this.BackupJobErrorCatchException.arrayOfError["PermissionAccesToCreateError"] = true;
        }
        public void PathTargetDirectoryExists()
        {
            this.BackupJobErrorCatchException.arrayOfError["PermissionAccesToSourceError"] = true;
        }
        public void CanAccessSourceFileOrDirectory()
        {
            this.BackupJobErrorCatchException.arrayOfError["PathSourceDirectoryError"]= true;
        }
       public void CanCreateFileOrDirectory()
        {
            this.BackupJobErrorCatchException.arrayOfError["PathTargetDirectoryError"] = true;
        }
        public void StorageSpaceAvailable()
        {
            this.BackupJobErrorCatchException.arrayOfError["StorageError"] = true;
        }
        public void ExceedsCharacterLimit()
        {
            this.BackupJobErrorCatchException.arrayOfError["ExceedsCharacterLimitError"] = true;
        }
    }
}
