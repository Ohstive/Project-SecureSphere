using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.ViewModel.Services
{
    // This interface is used to handle the copy of a directory
    internal interface IDirectoryHandler
    {
        void FullCopyDirectory(string sourceDirectoryPath, string targetDirectoryPath, FolderLog folderLog);
        void DifferentialCopyDirectory(string sourceDirectoryPath, string targetDirectoryPath, FolderLog folderLog);
    }
}
