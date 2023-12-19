using System;
using System.IO;
using SecureSphereV2.ViewModel.Services.Log;

namespace SecureSphereV2.ViewModel.Services.CopyFile
{
    internal class SimpleFileCopy : ICopyFileHandler
    {
        private readonly ILoger _log;

        public SimpleFileCopy(ILoger log)
        {
            _log = log;
        }

        public void DifferentialCopy(string CopieName, string sourcePath, string targetPath, FileInfo file)
        {
            string relativePath = Path.GetRelativePath(sourcePath, file.FullName);
            string path = Path.Combine(targetPath, relativePath);

            try
            {
                if (!File.Exists(path))
                {
                    File.Copy(file.FullName, path);
                    _log.LogDaily(new LogEntry
                    {

                        CopyName = CopieName,
                        Timestamp = DateTime.Now,
                        EventType = "DifferentialCopy",
                        SourceDirectory = sourcePath,
                        DestinationDirectory = targetPath,
                        SourceFile = file.FullName,
                        DestinationFile = path,
                        FileSize = file.Length
                    });
                }
            }
            catch (Exception ex)
            {
                _log.LogDaily(new LogEntry
                {
                    Timestamp = DateTime.Now,
                    EventType = "Error",
                    SourceDirectory = sourcePath,
                    DestinationDirectory = targetPath,
                    SourceFile = file.FullName,
                    ErrorMessage = $"Error during differential copy: {ex.Message}"
                });
            }
        }

        public void FullCopy(string CopieName, string sourcePath, string targetPath, FileInfo file)
        {
            string newFilePath = DuplicateFileHandler(sourcePath, targetPath, file);

            try
            {
                File.Copy(file.FullName, newFilePath);
                Console.WriteLine($"{newFilePath} copied");
                _log.LogDaily(new LogEntry
                {
                    CopyName = CopieName,
                    Timestamp = DateTime.Now,
                    EventType = "FullCopy",
                    SourceDirectory = sourcePath,
                    DestinationDirectory = targetPath,
                    SourceFile = file.FullName,
                    DestinationFile = newFilePath,
                    FileSize = file.Length
                });
            }
            catch (Exception ex)
            {
                _log.LogDaily(new LogEntry
                {
                    CopyName = CopieName,
                    Timestamp = DateTime.Now,
                    EventType = "Error",
                    SourceDirectory = sourcePath,
                    DestinationDirectory = targetPath,
                    SourceFile = file.FullName,
                    ErrorMessage = $"Error during full copy: {ex.Message}"
                });
            }
        }

        private static string DuplicateFileHandler(string sourcePath, string targetPath, FileInfo file)
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
            return path;
        }
    }
}
