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
                    _errorManager.SetError("TargetIsFileError");
                    return false;
                }
                else if (Directory.Exists(path))
                {
                 
                    directoryInfo = new DirectoryInfo(path);
                    return true;
                }

                else
                {
                    _errorManager.SetError("TargetDirectoryExistError");
                    return false;
                }
            }
            catch (UnauthorizedAccessException)
            {
                _errorManager.SetError("PermissionToAccessTargetError");
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
