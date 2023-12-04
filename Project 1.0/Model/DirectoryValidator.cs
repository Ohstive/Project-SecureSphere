using Project_1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.Model
{
    internal class DirectoryValidator
    {
        private readonly ErrorManager _errorManager;

        public DirectoryValidator(ErrorManager errorManager)
        {
            _errorManager = errorManager;
        }

        public bool IsDirectoryValid(string path, out DirectoryInfo directoryInfo)
        {
            directoryInfo = null;

            try
            {
                if (File.Exists(path))
                {
                    _errorManager.SetError("SourceDirectoryExistError");
                    return false;
                }
                else if (Directory.Exists(path))
                {
                    directoryInfo = new DirectoryInfo(path);
                    return true;
                }
                else
                {
                    _errorManager.SetError("SourceDirectoryExistError");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the unexpected error
                // Log.Error($"Unexpected error in IsDirectoryValid: {ex.Message}", ex);
                _errorManager.SetError("PermissionToAccessSourceError");
                return false;
            }
        }
    }
}
