using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjetV2.Model
{
    internal class ErrorCatchException
    {
        private Dictionary<string, bool> ArrayOfError = new Dictionary<string, bool>();
        public ErrorCatchException()
        {
            ArrayOfError.Add("PermissionAccesToCreateError", false);
            ArrayOfError.Add("PermissionAccesToSourceError", false);
            ArrayOfError.Add("PathSourceDirectoryError", false);
            ArrayOfError.Add("PathTargetDirectoryError", false);
            ArrayOfError.Add("StorageError", false);
            ArrayOfError.Add("ExceedsCharacterLimitError", false);
        }

        public Dictionary<string, bool> arrayOfError {get=>ArrayOfError;set=>ArrayOfError=value;}

        //Temporary function for testing
        public void Affiche()
        {
            foreach(var item in ArrayOfError) 
            { 
                Console.WriteLine(item);
            }
        }
    }
}
