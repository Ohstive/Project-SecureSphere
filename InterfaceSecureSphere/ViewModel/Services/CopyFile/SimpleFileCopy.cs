using InterfaceSecureSphere.ViewModel.Services.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSecureSphere.ViewModel.Services.CopyFile
{
    internal class SimpleFileCopy : ICopyFileHandler
    {
        private readonly ILoger _log;

        public SimpleFileCopy(ILoger log)
        {
            _log = log;
        }

        public void DifferentialCopy(ILoger log, string sourcePath, string targetPath, FileInfo file)
        {
            string relativePath = Path.GetRelativePath(sourcePath, file.FullName);
            string path = Path.Combine(targetPath, relativePath);
            if (!File.Exists(path))
            {
                File.Copy(file.FullName, path);
                _log.LogDaily(new LogEntry { Timestamp = DateTime.UtcNow, EventType = "DifferentialCopy", SourceDirectory = sourcePath, DestinationDirectory = targetPath, SourceFile = file.FullName, DestinationFile = path, FileSize = file.Length });
                Console.WriteLine($"{path} copied ");
            }
        }

        public void FullCopy(ILoger log, string sourcePath, string targetPath, FileInfo file)
        {
            string newFilePath = DuplicateFileHandler(sourcePath, targetPath, file);
            File.Copy(file.FullName, newFilePath);
            _log.LogDaily(new LogEntry { Timestamp = DateTime.UtcNow, EventType = "FullCopy", SourceDirectory = sourcePath, DestinationDirectory = targetPath, SourceFile = file.FullName, DestinationFile = newFilePath, FileSize = file.Length });
            Console.WriteLine($"{newFilePath} copied ");
        }


        // This method handles the case when the file already exists in the target directory
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
