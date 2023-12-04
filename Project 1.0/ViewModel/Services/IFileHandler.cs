using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel.Services
{
    internal interface IFileHandler
    {
        void CopyFileFull(string sourcePath, string targetPath, FileInfo file, FolderLog folderLog);
        void CopyFileDifferential(string sourcePath, string targetPath, FileInfo file, FolderLog folderLog);
    }
}
