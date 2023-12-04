

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace Project_1._0.ViewModel.Services
{
    internal class DirectoryHandler : IDirectoryHandler
    {

        private readonly IFileHandler _fileHandler;
        public DirectoryHandler(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        // Copies the entire directory from source to target even if the files already exist in the target directory
        public void FullCopyDirectory(string sourceDirectoryPath, string targetDirectoryPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);

            FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

            foreach (FileInfo file in fileList)
            {
                _fileHandler.CopyFileFull(sourceDirectoryPath, targetDirectoryPath, file);
            }
            

        }


        // Copies the entire directory from source to target only if the files don't exist in the target directory
        public void DifferentialCopyDirectory(string sourceDirectoryPath, string targetDirectoryPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);
            
            FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

            foreach (FileInfo file in fileList)
            {
               
                _fileHandler.CopyFileDifferential(sourceDirectoryPath, targetDirectoryPath, file);
               
            }
            
        }

        // Recreates the folder structure of the source directory in the target directory
        private void RecreateFolderStructure(string source, string destination)
        {
            DirectoryInfo sourceInfo = new DirectoryInfo(source);

            // Create the target folder if it doesn't exist
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            // Get the list of subdirectories in the source folder
            DirectoryInfo[] subDirectories = sourceInfo.GetDirectories();

            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                string newSubDirectoryPath = Path.Combine(destination, subDirectory.Name);
                RecreateFolderStructure(subDirectory.FullName, newSubDirectoryPath);
            }
        }

      
    }
}

