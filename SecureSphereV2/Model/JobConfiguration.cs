using System;

namespace SecureSphereV2.Model
{
    public class JobConfiguration
    {
        // Propriétés de la tâche
        public string JobName { get; set; } // Rendre l'accès en écriture public
        public string SourceDirectoryPath { get; private set; }
        public string TargetDirectoryPath { get; private set; }
        public string BackupType { get; private set; }
        public string CryptKey { get; set; }

        // Constructeur
        public JobConfiguration(string name, string source, string target, string backupType,string cryptKey )
        {
            JobName = name;
            SourceDirectoryPath = source;
            TargetDirectoryPath = target;
            BackupType = backupType;
            CryptKey = cryptKey;
        }
    }
}
