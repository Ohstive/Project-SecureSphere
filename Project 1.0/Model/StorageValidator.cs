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
            Console.WriteLine("Checking read and write access to the directory...");

            try
            {
                // Test read access
                var allFiles = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);

                // Test write access by creating a subdirectory
                var subdirectory = directoryInfo.CreateSubdirectory("TestSubdirectory");

                Console.WriteLine("Read and write access to the directory is allowed.");
                subdirectory.Delete();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Read or write access to the directory is not allowed.");
                _errorManager.SetError("PermissionToAccessTargetError");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void CheckFreeSpace(DirectoryInfo directoryInfo, long sizeOfSource)
        {
            Console.WriteLine("Checking free space in the target directory...");

            try
            {
                if (directoryInfo != null && directoryInfo.Root != null)
                {
                    var freeSpaceInTarget = new DriveInfo(directoryInfo.Root.Name).AvailableFreeSpace;

                    if (freeSpaceInTarget < sizeOfSource)
                    {
                        Console.WriteLine("Not enough storage space available");
                        _errorManager.SetError("StorageError");
                    }
                }
                else
                {
                    Console.WriteLine("Target root doesn't exist");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Read or write access to the directory is not allowed.");
                _errorManager.SetError("PermissionToAccessTargetError");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
