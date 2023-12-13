using System;

namespace SecureSphereV2.Model
{
    public class JobConfiguration
    {
        // Task Priority
        public string JobName { get; set; } // Give public access to writing
        public string SourceDirectoryPath { get; private set; }
        public string TargetDirectoryPath { get; private set; }
        public string BackupType { get; private set; }
        public string CryptKey { get; set; }

        // Constructor
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
