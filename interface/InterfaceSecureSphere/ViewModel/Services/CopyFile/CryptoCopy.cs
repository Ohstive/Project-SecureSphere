using InterfaceSecureSphere.ViewModel.Services.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSecureSphere.ViewModel.Services.CopyFile
{
    internal class CryptoCopy : ICopyFileHandler
    {
        private readonly ILoger _log;

        public CryptoCopy(ILoger log)
        {
            _log = log;
        }

        public void DifferentialCopy(ILoger log, string sourcePath, string targetPath, FileInfo file)
        {
            IsFile(sourcePath);
            string relativePath = Path.GetRelativePath(sourcePath, file.FullName);
            string path = Path.Combine(targetPath, relativePath);
            if (!File.Exists(path))
            {
                FileCryptoCopy(file.FullName, path);
                _log.LogDaily(new LogEntry { Timestamp = DateTime.UtcNow, EventType = "DifferentialCopy", SourceDirectory = sourcePath, DestinationDirectory = targetPath, SourceFile = file.FullName, DestinationFile = path, FileSize = file.Length });
                Console.WriteLine($"{path} copied ");
            }
        }

        public void FullCopy(ILoger log, string sourcePath, string targetPath, FileInfo file)
        {
            IsFile(sourcePath);
            string newFilePath = DuplicateFileHandler(sourcePath, targetPath, file);
            FileCryptoCopy(file.FullName, newFilePath);
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

        //This method runs the CryptoSoft.exe program to encrypt the file
        public void FileCryptoCopy(string sourcePath, string targetPath)
        {
            string CryptoSoftPath = "C:\\Users\\matv3\\source\\repos\\CryptoSoft\\CryptoSoft\\bin\\Release\\net8.0\\CryptoSoft.exe";

            Console.WriteLine(File.Exists(sourcePath) ? "File exists." : "File does not exist.");

            if (File.Exists(sourcePath) == false)
            {
                Console.WriteLine("File doesn't exists.");
            }

            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = CryptoSoftPath,
                    Arguments = $"{sourcePath} {targetPath}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                process.StartInfo = startInfo;
                process.Start();

                process.WaitForExit();

            }

            Console.WriteLine("Opération terminée.");
        }

        public static bool IsFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
                return false;
            else
                return true;
        }
    }
}
