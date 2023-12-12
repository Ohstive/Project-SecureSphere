using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SecureSphereV2._0.Model
{
    public class JobConfiguration
    {
        // Propriétés de la tâche
        public string JobName { get; set; } // Rendre l'accès en écriture public
        public string SourceDirectoryPath { get; private set; }
        public string TargetDirectoryPath { get; private set; }
        public string BackupType { get; private set; }

        public string RunType { get; private set; }

        // Constructeur
        public JobConfiguration(string name, string source, string target, string backupType, string runtype)
        {
            JobName = name;
            SourceDirectoryPath = source;
            TargetDirectoryPath = target;
            BackupType = backupType;
            RunType = runtype;
        }
    }
}
