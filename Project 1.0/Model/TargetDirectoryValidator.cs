using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project_1._0.Model
{
    internal class TargetDirectoryValidator
    {
        private readonly ErrorManager _errorManager;

        public TargetDirectoryValidator(ErrorManager errorManager)
        {
            _errorManager = errorManager ?? throw new ArgumentNullException(nameof(errorManager));
        }

        public bool IsTargetDirectoryValid(string path, out DirectoryInfo directoryInfo)
        {
            directoryInfo = null;

            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Target is a file");
                    _errorManager.SetError("TargetIsFileError");
                    return false;
                }
                else if (Directory.Exists(path))
                {
                    Console.WriteLine("Target exists");
                    directoryInfo = new DirectoryInfo(path);
                    return true;
                }

                else
                {
                    Console.WriteLine("Target doesn't exist");
                    _errorManager.SetError("TargetDirectoryExistError");
                    return false;
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Read or write access to the directory is not allowed.");
                _errorManager.SetError("PermissionToAccessTargetError");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
