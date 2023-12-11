using InterfaceSecureSphere.ViewModel.Services.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSecureSphere.ViewModel.Services.CopyFile
{
    internal interface ICopyFileHandler
    {
        void DifferentialCopy(ILoger log, string sourcePath, string targetPath, FileInfo file);
        void FullCopy(ILoger log, string sourcePath, string targetPath, FileInfo file);
    }
}
