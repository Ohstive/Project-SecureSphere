using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSecureSphere.Model

    //public class JobConfiguration here we will put the properties of a job
{
    public class JobConfiguration
    {
        // Propriétés de la tâche
        public string JobName { get; set; } // Rendre l'accès en écriture public
        public string SourceDirectoryPath { get; private set; }
        public string TargetDirectoryPath { get; private set; }
        public string BackupType { get; private set;}
        public string Encryption { get; private set;}

        // Constructeur
        public JobConfiguration(string name, string source, string target, string backupType, string encryption)
        {
            JobName = name;
            SourceDirectoryPath = source;
            TargetDirectoryPath = target;
            BackupType = backupType;
            Encryption = encryption;
        }
    }
}
