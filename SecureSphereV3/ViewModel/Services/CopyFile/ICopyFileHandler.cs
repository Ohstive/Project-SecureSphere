using System.IO;
using SecureSphereV2.ViewModel.Services.Log;  

namespace SecureSphereV2.ViewModel.Services.CopyFile
{
    internal interface ICopyFileHandler
    {
        void DifferentialCopy(string CopieName,string sourcePath, string targetPath, FileInfo file);
        void FullCopy(string CopieName, string sourcePath, string targetPath, FileInfo file);
    }
}
