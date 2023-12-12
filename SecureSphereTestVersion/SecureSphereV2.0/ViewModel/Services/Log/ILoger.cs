using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSphereV2._0.ViewModel.Services.Log
{
    internal interface ILoger
    {
        void LogDaily(LogEntry logEntry);

        void LogStatut(LogStatutEntry logStatut);

        void WriteLogsToFile();

        void WriteLogsToFileStatut();
    }
}
