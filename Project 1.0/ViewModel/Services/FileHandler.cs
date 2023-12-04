

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel.Services
{
    // This interface is used to handle the copy of a file

    
    internal class FileHandler : IFileHandler
    {

        public void CopyFileFull(string sourcePath, string targetPath, FileInfo file)
        {
          
            string newFilePath = DuplicateFileHandler(sourcePath, targetPath, file);
            File.Copy(file.FullName, newFilePath);
            Console.WriteLine($"{newFilePath} copied ");

        }

        public void CopyFileDifferential(string sourcePath, string targetPath, FileInfo file)
        {
            
            string relativePath = Path.GetRelativePath(sourcePath, file.FullName);
            string path = Path.Combine(targetPath, relativePath);
           
           
            if (!File.Exists(path))
            {
                File.Copy(file.FullName, path);
                Console.WriteLine($"{path} copied ");
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
     
            return path;
        }

    }
}

