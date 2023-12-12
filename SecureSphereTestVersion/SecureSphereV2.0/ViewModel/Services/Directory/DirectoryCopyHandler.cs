using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using SecureSphereV2._0.ViewModel.Services.CopyFile;
using SecureSphereV2._0.ViewModel.Services.Log;



namespace SecureSphereV2._0.ViewModel.Services.Directory
{
    using System.IO;
    internal class DirectoryCopyHandler
    {

        private readonly ILoger _log;
        private readonly ILoger _logStatus;
        private readonly ICopyFileHandler _fileCopyHandler;

        public DirectoryCopyHandler(ICopyFileHandler fileCopyHandler, ILoger log, ILoger logStatus)
        {
            _fileCopyHandler = fileCopyHandler ?? throw new ArgumentNullException(nameof(fileCopyHandler));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _logStatus = logStatus ?? throw new ArgumentNullException(nameof(logStatus));
        }


        // Copies the entire directory from source to target even if the files already exist in the target directory
        public void FullCopyDirectory(string copyName, string sourceDirectoryPath, string targetDirectoryPath)
        {
            try
            {
                DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);

                FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);


                RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);
               
                long totalFileToCopy =  fileList.Length;
                long totalFileCopied =  0;
                long totalSizeToCopy = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                long totalSizeCopied = 0;
                DateTime startTime = DateTime.UtcNow;
               

                _logStatus.LogStatut(new LogStatutEntry
                {  
                    CopyName = copyName,
                    StartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    TotalTime = FormatElapsedTime(DateTime.UtcNow - startTime),
                    EventType = "FullCopy",
                    SourceDirectory = sourceDirectoryPath,
                    DestinationDirectory = targetDirectoryPath,
                    TotalFileToCopy = totalFileToCopy,
                    TotalFileCopied = totalFileCopied,
                    TotalSizeToCopy = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length),
                    TotalSizeCopied = totalSizeCopied,
                    Status = "In Progress"
                 });


            foreach (FileInfo file in fileList)
            {
                _fileCopyHandler.FullCopy( copyName,sourceDirectoryPath, targetDirectoryPath, file);
                totalFileCopied++;
                totalFileToCopy--;
                totalSizeCopied += file.Length;
                totalSizeToCopy -= file.Length;
               
                _logStatus.LogStatut(new LogStatutEntry
                  {
                        CopyName = copyName,
                        StartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        TotalTime = FormatElapsedTime(DateTime.UtcNow - startTime),
                        EventType = "FullCopy",
                        SourceDirectory = sourceDirectoryPath,
                        DestinationDirectory = targetDirectoryPath,
                        TotalFileToCopy = totalFileToCopy,
                        TotalFileCopied = totalFileCopied,
                        TotalSizeToCopy = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length),
                        TotalSizeCopied = totalSizeCopied,
                        Status = "In Progress"
                    });


            }

                _logStatus.LogStatut(new LogStatutEntry
                {
                    CopyName = copyName,
                    StartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    TotalTime = FormatElapsedTime(DateTime.UtcNow - startTime),
                    EventType = "FullCopy",
                    SourceDirectory = sourceDirectoryPath,
                    DestinationDirectory = targetDirectoryPath,
                    TotalFileToCopy = 0,
                    TotalFileCopied = totalFileToCopy,
                    TotalSizeToCopy = totalSizeToCopy,
                    TotalSizeCopied = totalSizeCopied,
                    Status = "Done"
                });
                

            }
            catch (Exception ex)
            {
                // Log exception details
                _logStatus.LogStatut(new LogStatutEntry
                {
                    CopyName = copyName,
                    StartTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventType = "Error",
                    SourceDirectory = sourceDirectoryPath,
                    DestinationDirectory = targetDirectoryPath,
                    ErrorMessage = $"Error during full directory copy: {ex.Message}"
                });
            }
        }

        // Copies the entire directory from source to target only if the files don't exist in the target directory
        public void DifferentialCopyDirectory(string copyName, string sourceDirectoryPath, string targetDirectoryPath)
        {
            try
            {

                DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);
                FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);
                RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

                DateTime startTime = DateTime.UtcNow;

                foreach (FileInfo file in fileList)
                {

                    _fileCopyHandler.DifferentialCopy(copyName, sourceDirectoryPath, targetDirectoryPath, file);

                }

                _logStatus.LogStatut(new LogStatutEntry
                {
                    CopyName = copyName,
                    StartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    TotalTime = FormatElapsedTime(DateTime.UtcNow - startTime),
                    EventType = "DifferentialCopy",
                    SourceDirectory = sourceDirectoryPath,
                    DestinationDirectory = targetDirectoryPath,
                    Status = "Done"
                });
            }
            catch (Exception ex)
            {
                // Log exception details
                _logStatus.LogStatut(new LogStatutEntry
                {
                    CopyName = copyName,
                    StartTime=DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    EventType = "Error",
                    SourceDirectory = sourceDirectoryPath,
                    DestinationDirectory = targetDirectoryPath,
                    ErrorMessage = $"Error during differential directory copy: {ex.Message}"
                });
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

        // Formats the elapsed time in a human-readable format
        static string FormatElapsedTime(TimeSpan timeSpan)
        {
            string formattedElapsedTime = $"{timeSpan.Days} days, {timeSpan.Hours} hours, {timeSpan.Minutes} minutes, {timeSpan.Seconds} seconds";
            return formattedElapsedTime;
        }

    }
}
