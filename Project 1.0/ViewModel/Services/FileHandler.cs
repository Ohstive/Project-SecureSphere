using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel.Services
{
    // This interface is used to handle the copy of a file
    internal class FileHandler : IFileHandler
    {

        private readonly ILogger _logger;

        public FileHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void CopyFileFull(string sourcePath, string targetPath, FileInfo file, FolderLog folderLog)
        {
            DateTime startTime = DateTime.Now;
            string newFilePath = DuplicateFileHandler(sourcePath, targetPath, file);
            File.Copy(file.FullName, newFilePath);
            DateTime finishTime = DateTime.Now;

            UpdateFolderLog(folderLog, startTime, finishTime, file.Length);
            LogFileCopyDetails(sourcePath, newFilePath, file, startTime, finishTime);
        }

        public void CopyFileDifferential(string sourcePath, string targetPath, FileInfo file, FolderLog folderLog)
        {
            DateTime startTime = DateTime.Now;
            string destinationPath = Path.Combine(targetPath, file.FullName.Substring(sourcePath.Length));
            if (!File.Exists(destinationPath))
            {
                File.Copy(file.FullName, destinationPath);
                DateTime finishTime = DateTime.Now;

                UpdateFolderLog(folderLog, startTime, finishTime, file.Length);
                LogFileCopyDetails(sourcePath, destinationPath, file, startTime, finishTime);
            }
        }

        // This method handles the case when the file already exists in the target directory
        static string DuplicateFileHandler(string sourcePath, string targetPath, FileInfo file)
        {
            string subdirectoryPath = Path.GetRelativePath(sourcePath, file.DirectoryName);

            string relativePath = Path.GetRelativePath(sourcePath, file.FullName);
            string path = Path.Combine(targetPath, relativePath);


            string fileName = Path.GetFileNameWithoutExtension(file.FullName);
            string fileExtension = Path.GetExtension(file.FullName);
            int i = 1;

            while (File.Exists(path))
            {
                fileName = Path.GetFileNameWithoutExtension(file.FullName);
                fileExtension = Path.GetExtension(file.FullName);
                path = Path.Combine(targetPath, subdirectoryPath, $"{fileName}_{i}{fileExtension}");
                i++;
            }
            Console.WriteLine(path);
            return path;
        }
        // This method logs the details of the copied file
        private void LogFileCopyDetails(string sourcePath, string targetPath, FileInfo file, DateTime startTime, DateTime finishTime)
        {
            string logMessage = $"Copied file: {file.FullName} from {sourcePath} to {targetPath} " +
                                $"Size: {FormatBytes(file.Length)} " +
                                $"Start Time: {startTime:yyyy-MM-dd HH:mm:ss} " +
                                $"Finish Time: {finishTime:yyyy-MM-dd HH:mm:ss}";
            _logger.Log(logMessage);
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


        private void UpdateFolderLog(FolderLog folderLog, DateTime startTime, DateTime finishTime, long fileSize)
        {
            folderLog.StartTime = startTime;
            folderLog.FinishTime = finishTime;
            folderLog.TotalTime += finishTime - startTime;
            folderLog.TotalFileSize += fileSize;
        }

    }
}
