using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureSphereV2._0.ViewModel.Services.Log;  


namespace SecureSphereV2._0.ViewModel.Services.CopyFile
{
    internal interface ICopyFileHandler
    {
        void DifferentialCopy(string CopieName,string sourcePath, string targetPath, FileInfo file);
        void FullCopy(string CopieName, string sourcePath, string targetPath, FileInfo file);
    }
}
