using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.Model
{

    internal class ErrorManager
    {
        private Dictionary<string, bool> errors;

        public ErrorManager()
        {
            InitializeErrors();
        }

        // Initialize all errors to false
        private void InitializeErrors()
        {
            errors = new Dictionary<string, bool>
            {
                { "PermissionToCreateInTargetError", false },
                { "PermissionToAccessSourceError", false },
                { "PermissionToAccessTargetError", false },
                { "SourceDirectoryExistError", false },
                { "TargetDirectoryExistError", false },
                { "StorageError", false },
                { "ExceedsCharacterLimitError", false },
                { "SourceTargetSimilarError", false },
                { "TargetIsFileError", false }
            };
        }


        // To set an error, use SetError("ErrorType")
        public void SetError(string errorType)
        {
            if (errors.ContainsKey(errorType))
            {
                errors[errorType] = true;
            }
            else
            {
                throw new ArgumentException($"Error type '{errorType}' not recognized.");
            }
        }

        // To reset an error, use ResetError("ErrorType")
        public void ResetError(string errorType)
        {
            if (errors.ContainsKey(errorType))
            {
                errors[errorType] = false;
            }
            else
            {
                throw new ArgumentException($"Error type '{errorType}' not recognized.");
            }
        }
        // To reset all errors, use ResetAllErrors()
        public void ResetAllErrors()
        {
            foreach (var errorType in errors.Keys)
            {
                errors[errorType] = false;
            }
        }
        // To check if an error is set, use IsErrorSet("ErrorType")
        public bool IsErrorSet(string errorType)
        {
            if (errors.ContainsKey(errorType))
            {
                return errors[errorType];
            }
            else
            {
                throw new ArgumentException($"Error type '{errorType}' not recognized.");
            }
        }

        // To check if any error is set, use IsAnyErrorSet()
        public bool IsNotError()
        {
            foreach (var errorType in errors.Keys)
            {
                if (errors[errorType])
                {
                    return false;
                }
            }
            return true;
        }
        // To have a list of all set errors, use GetSetErrors()
        public List<string> GetSetErrors()
        {
            List<string> setErrors = new List<string>();
            foreach (var errorType in errors.Keys)
            {
                if (errors[errorType])
                {
                    setErrors.Add(errorType);
                }
            }
            return setErrors;
        }

        public void PermissionToCreateInTargetAllowed()
        {
           
            this.errors["PermissionToCreateInTargetError"]= true;
        }

        public void IsAccessToSourceAllowed()
        {
            this.errors[" PermissionToAccessSourceError"] = true;
        }

        public void IsAccessToTargetAllowed()
        {
            this.errors["PermissionToAccessTargetError"] = true;
        }

        public void PathSourceDirectoryExists()
        {
            this.errors["SourceDirectoryExistError"] = true;
        }

        public void PathTargetDirectoryExists()
        {
            this.errors["TargetDirectoryExistError"] = true;
        }

        public void StorageSpaceAvailable()
        {
            this.errors["StorageError"] = true;
        }

        public void ExceedsCharacterLimit()
        {
            this.errors["ExceedsCharacterLimitError"] = true;
        }

        public void SourceTargetSimilar()
        {
            this.errors["SourceTargetSimilarError"] = true;
        }

        public void TargetIsFile()
        {
            this.errors["TargetIsFileError"] = true;
        }

    }
}
