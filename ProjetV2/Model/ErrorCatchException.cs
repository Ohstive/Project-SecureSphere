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
        //Error Management
        private Dictionary<string, bool> ArrayOfError = new Dictionary<string, bool>();

        //Constructor
        public ErrorCatchException()
        {
            //Error to create the file or directory in the target directory
            ArrayOfError.Add("PermissionAccesToCreateInTargetError", false);

            //Error to access to the source directory or file
            ArrayOfError.Add("PermissionAccesToSourceError", false);
            //Error to access to the target directory
            ArrayOfError.Add("PermissionAccesToTargetError", false);


            //Path Source Directory doesn't exist
            ArrayOfError.Add("PathSourceDirectoryExistError", false);
            //Path Target Directory doesn't exist
            ArrayOfError.Add("PathTargetDirectoryExistError", false);


            //Not enough storage space available
            ArrayOfError.Add("StorageError", false);

            //Exceeds character limit for file name
            ArrayOfError.Add("ExceedsCharacterLimitError", false);

            //Source and Target are the same
            ArrayOfError.Add("SourceTargetSimilarError", false);

            //Target is a file
            ArrayOfError.Add("TargetIsFileError", false);
        }
        //Getter and Setter
        public Dictionary<string, bool> arrayOfError { get => ArrayOfError; set => ArrayOfError = value; }

        //Temporary function for testing
        public void Affiche()
        {
            foreach (var item in ArrayOfError)
            {
                Console.WriteLine(item);
            }
        }
        //Function to set the error
        
        //Function to set the error of the file or directory creation
        public void PermissionToCreateInTargetAllowed()
        {
            this.arrayOfError["PermissionAccesToCreateInTargetError"] = true;
        }

        //Function to set the error of the access to the source and target
        public void IsAccessToSourceAllowed ()
        {
            this.arrayOfError["PermissionAccesToSourceError"] = true;
        }
        public void IsAccessToTargetAllowed()
        {
            this.arrayOfError["PermissionAccesToTargetError"] = true;
        }

        //Function to set the existence of the source and target directory
        public void PathSourceDirectoryExists()
        {
            this.arrayOfError["PathSourceDirectoryExistError"] = true;
        }
        public void PathTargetDirectoryExists()
        {
            this.arrayOfError["PathTargetDirectoryExistError"] = true;
        }

        

        //Function to set the availability of the storage space
        public void StorageSpaceAvailable()
        {
            this.arrayOfError["StorageError"] = true;
        }

        //Function to set the error of the file name
        public void ExceedsCharacterLimit()
        {
            this.arrayOfError["ExceedsCharacterLimitError"] = true;
        }
        //Function to set the similarity of the source and target
        public void SourceTargetSimilar()
        {
            this.arrayOfError["SourceTargetSimilarError"] = true;
        }

        //Function to set the error of the target is a file
        public void TargetIsFile()
        {
            this.arrayOfError["TargetIsFileError"] = true;
        }

        //Function to reset the error
        public void ResetError()
        {
            foreach (var item in ArrayOfError)
            {
                ArrayOfError[item.Key] = false;
            }
        }
        //Function to check if there is an error
        public bool IsNotError()
        {
            return this.ArrayOfError.All(x => x.Value == false);
        }
    }
}
