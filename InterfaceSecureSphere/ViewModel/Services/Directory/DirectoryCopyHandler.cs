using InterfaceSecureSphere.ViewModel.Services.CopyFile;
using InterfaceSecureSphere.ViewModel.Services.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSecureSphere.ViewModel.Services.Directory
{
    using System.IO;

    internal class DirectoryCopyHandler
    {
        private readonly ILoger _log;
        private readonly ILoger _logStatus;
        private readonly ICopyFileHandler _fileCopyHandler;

        public DirectoryCopyHandler(ICopyFileHandler fileCopyHandler, ILoger log, ILoger logStatus)
        {
            _fileCopyHandler = fileCopyHandler;
            _log = log;
            _logStatus = logStatus;
        }


        // Copies the entire directory from source to target even if the files already exist in the target directory
        public void FullCopyDirectory(string copyName, string sourceDirectoryPath, string targetDirectoryPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);

            FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

            _logStatus.LogStatut(new LogStatutEntry { CopyName = copyName, Timestamp = DateTime.UtcNow, EventType = "FullCopy", SourceDirectory = sourceDirectoryPath, DestinationDirectory = targetDirectoryPath, TotalFileToCopy = fileList.Length, TotalFileCopied = 0, TotalSizeToCopy = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length), TotalSizeCopied = 0, Status = "In Progress" });


            foreach (FileInfo file in fileList)
            {
                _fileCopyHandler.FullCopy(_log, sourceDirectoryPath, targetDirectoryPath, file);
            }
            _logStatus.LogStatut(new LogStatutEntry { CopyName = copyName, Timestamp = DateTime.UtcNow, EventType = "FullCopy", SourceDirectory = sourceDirectoryPath, DestinationDirectory = targetDirectoryPath, TotalFileToCopy = 0, TotalFileCopied = fileList.Length, TotalSizeToCopy = 0, TotalSizeCopied = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length), Status = "Done" });
        }


        // Copies the entire directory from source to target only if the files don't exist in the target directory
        public void DifferentialCopyDirectory(string copyName, string sourceDirectoryPath, string targetDirectoryPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);

            FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

            foreach (FileInfo file in fileList)
            {

                _fileCopyHandler.DifferentialCopy(_log, sourceDirectoryPath, targetDirectoryPath, file);

            }
            _logStatus.LogStatut(new LogStatutEntry { CopyName = copyName, Timestamp = DateTime.UtcNow, EventType = "FullCopy", SourceDirectory = sourceDirectoryPath, DestinationDirectory = targetDirectoryPath, TotalFileToCopy = 0, TotalFileCopied = fileList.Length, TotalSizeToCopy = 0, TotalSizeCopied = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length), Status = "Done" });

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
