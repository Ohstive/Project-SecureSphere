using System;

namespace SecureSphereV2.Model
{
    public class JobConfiguration
    {
        // Propriétés de la tâche
        public string JobName { get; set; } // Rendre l'accès en écriture public
        public string SourceDirectoryPath { get; set; }
        public bool IsSourceDirectory { get; set; }
        public string TargetDirectoryPath { get; set; }
        public string BackupType { get; set; }
        public string EncryptionKey { get; set; }
        public bool IsEncryptionEnabled { get; set; }
        public string Encryption { get; set; }
        public bool IsFinished { get; set; }

       
        public string IsPriority { get; set; }
        public List<string> ExtensionPriority { get; set; }
        // Constructeur
        public JobConfiguration(string name, string source, bool isSourceDirectory, string target, string backupType, string encryptionKey, bool isEncryptionEnabled)
        {
            JobName = name;
            SourceDirectoryPath = source;
            IsSourceDirectory = isSourceDirectory;
            TargetDirectoryPath = target;
            BackupType = backupType;
            EncryptionKey = encryptionKey;
            IsEncryptionEnabled = isEncryptionEnabled;

            if (IsEncryptionEnabled)
            {
                Encryption = "Enabled";
            }
            else
            {
                Encryption = "Disabled";
            }

        }
    }
}
