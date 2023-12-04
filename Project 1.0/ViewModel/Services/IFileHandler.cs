using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel.Services
{
    internal interface IFileHandler
    {
        
        void CopyFileFull(string sourcePath, string targetPath, FileInfo file);
        void CopyFileDifferential(string sourcePath, string targetPath, FileInfo file);

        //In progress
        //void CopyFileFullWithLog(string sourcePath, string targetPath, FileInfo file, FolderLog folderLog);
        //void CopyFileDifferentialWithLog(string sourcePath, string targetPath, FileInfo file, FolderLog folderLog);

    }
}
