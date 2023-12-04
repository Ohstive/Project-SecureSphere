using Project_1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.Model
{
    internal class StorageValidator
    {
        private readonly ErrorManager _errorManager;

        public StorageValidator(ErrorManager errorManager)
        {
            _errorManager = errorManager;
        }

        public void CheckReadWriteAccess(DirectoryInfo directoryInfo)
        {

            try
            {
                // Test read access
                var allFiles = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);

                // Test write access by creating a subdirectory
                var subdirectory = directoryInfo.CreateSubdirectory("TestSubdirectory");

                subdirectory.Delete();
            }
            catch (UnauthorizedAccessException)
            {
                _errorManager.SetError("PermissionToAccessTargetError");
            }
        }

        public void CheckFreeSpace(DirectoryInfo directoryInfo, long sizeOfSource)
        {

            try
            {
                if (directoryInfo != null && directoryInfo.Root != null)
                {
                    var freeSpaceInTarget = new DriveInfo(directoryInfo.Root.Name).AvailableFreeSpace;

                    if (freeSpaceInTarget < sizeOfSource)
                    {
                        _errorManager.SetError("StorageError");
                    }
                }
              
            }
            catch (UnauthorizedAccessException)
            {
                _errorManager.SetError("PermissionToAccessTargetError");
            }
        }
    }
}
