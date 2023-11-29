using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetV2.ViewModel
{
    internal class Log
    {
        JobDirectory LogDirectory;
        int TotalFilesToCopy;
        float TotalFilesToCopySize;
        DateTime TotalFileTransferTime;
        DateTime Timestamp;
        string SavePath;


        public Log() {
            TotalFilesToCopy = 0;
            TotalFilesToCopySize = 0;
            SavePath = string.Empty;
        }
        public void SaveToJSON()
        {

        }
    }
}
