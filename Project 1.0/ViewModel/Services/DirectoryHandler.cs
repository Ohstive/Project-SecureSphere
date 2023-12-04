using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel.Services
{
    internal class DirectoryHandler : IDirectoryHandler
    {
        private readonly IFileHandler _fileHandler;
        private readonly ILogger _logger;

        public DirectoryHandler(IFileHandler fileHandler, ILogger logger)
        {
            _fileHandler = fileHandler;
            _logger = logger;
        }

        // Copies the entire directory from source to target even if the files already exist in the target directory
        public void FullCopyDirectory(string sourceDirectoryPath, string targetDirectoryPath, FolderLog folderLog)
    {
        DateTime startTime = DateTime.Now;
        DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);
        FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);

        long totalFolderSize = sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);

        RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

        foreach (FileInfo file in fileList)
        {
            _fileHandler.CopyFileFull(sourceDirectoryPath, targetDirectoryPath, file, folderLog);
        }

        DateTime finishTime = DateTime.Now;
        folderLog.StartTime = startTime;
        folderLog.FinishTime = finishTime;
        folderLog.TotalTime = finishTime - startTime;
        folderLog.TotalFolderSize = totalFolderSize;
        folderLog.LogName = $"FullCopy_Log_{DateTime.Now:yyyyMMddHHmmss}";
        LogFolderDetails(folderLog);
    }


        // Copies the entire directory from source to target only if the files don't exist in the target directory
        public void DifferentialCopyDirectory(string sourceDirectoryPath, string targetDirectoryPath, FolderLog folderLog)
        {
            DateTime startTime = DateTime.Now;
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);
            
            FileInfo[] fileList = sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            RecreateFolderStructure(sourceDirectoryPath, targetDirectoryPath);

            foreach (FileInfo file in fileList)
            {
                _fileHandler.CopyFileDifferential(sourceDirectoryPath, targetDirectoryPath, file, folderLog);
            }

            DateTime finishTime = DateTime.Now;
            folderLog.StartTime = startTime;
            folderLog.FinishTime = finishTime;
            folderLog.TotalTime = finishTime - startTime;
            folderLog.LogName = $"DifferentialCopy_Log_{DateTime.Now:yyyyMMddHHmmss}";
            LogFolderDetails(folderLog);
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

        private void LogFolderDetails(FolderLog folderLog)
        {
            string logMessage = $"Folder Copy Log: {folderLog.LogName} " +
                                $"Total Time: {FormatTime(folderLog.TotalTime)} " +
                                $"Start Time: {folderLog.StartTime:yyyy-MM-dd HH:mm:ss} " +
                                $"Finish Time: {folderLog.FinishTime:yyyy-MM-dd HH:mm:ss} " +
                                $"Total Folder Size: {FormatBytes(folderLog.TotalFolderSize)} " +
                                $"Total File Size: {FormatBytes(folderLog.TotalFileSize)}";
            _logger.Log(logMessage);
        }

        private string FormatTime(TimeSpan time)
        {
            if (time.Days > 0)
            {
                return $"{time.Days} days, {time.Hours} hours, {time.Minutes} minutes, {time.Seconds} seconds";
            }
            else if (time.Hours > 0)
            {
                return $"{time.Hours} hours, {time.Minutes} minutes, {time.Seconds} seconds";
            }
            else if (time.Minutes > 0)
            {
                return $"{time.Minutes} minutes, {time.Seconds} seconds";
            }
            else
            {
                return $"{time.Seconds} seconds";
            }
        }


        private static string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int index = 0;
            while (bytes >= 1024 && index < suffixes.Length - 1)
            {
                bytes /= 1024;
                index++;
            }

            return $"{bytes:N2} {suffixes[index]}";
        }



    }
}
