using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureSphere_V1.ViewModel.Services.Log;   


namespace SecureSphere_V1.ViewModel.Services.CopyFile
{
    internal interface ICopyFileHandler
    {
        void DifferentialCopy(string CopieName,string sourcePath, string targetPath, FileInfo file);
        void FullCopy(string CopieName, string sourcePath, string targetPath, FileInfo file);
    }
}
