using SecureSphereV2.ViewModel.Services.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace SecureSphereV2.ViewModel.Services.CopyFile
{
    internal class CryptoCopy : ICopyFileHandler
    {
        private readonly ILoger _log;
        private readonly string _cryptoCopyPath;
        private readonly string _key;

        public CryptoCopy(ILoger log,string cryptoCopyPath, string key)
        {
            _log = log;
            _cryptoCopyPath = cryptoCopyPath;
            _key = key;
        }

        public void DifferentialCopy(string CopieName, string sourcePath, string targetPath, FileInfo file)
        {
            string relativePath = Path.GetRelativePath(sourcePath, file.FullName);
            string path = Path.Combine(targetPath, relativePath);

            try
            {
                if (!File.Exists(path))
                {
                    FileCryptoCopy(file.FullName, path);
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
                FileCryptoCopy(file.FullName, newFilePath);
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
            string executablePath = this._cryptoCopyPath;
            string inputFilePath = sourcePath;
            string outputFolderPath = targetPath;
            string encryptionKey = this._key;
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = executablePath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,  // Redirect standard error output
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"{inputFilePath} {outputFolderPath} {encryptionKey}"
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    // Read standard output
                    string output = process.StandardOutput.ReadToEnd();
                    // Read standard error
                    string error = process.StandardError.ReadToEnd();
                    Console.WriteLine(error);
                }
            }
           
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"CryptoCopy2_{ex.Message}");
            }

            
        }
    }
}
