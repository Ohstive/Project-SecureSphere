using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.Model
{
    // This class is used to store the configuration of a job
    internal class JobConfiguration
    {
        // Properties of a job 

        // Name of the job
        public string JobName { get; private set; }

        // Path of the source directory
        public string SourceDirectoryPath { get; private set; }

        // Path of the target directory
        public string TargetDirectoryPath { get; private set; }

        // 0 = Full, 1 = Differential
        public int BackupType { get; private set; }

        // Constructor
        public JobConfiguration(string name, string source, string target, int backupType)
        {
            JobName = name;
            SourceDirectoryPath = source;
            TargetDirectoryPath = target;

            // Validate backupType to be within the specified range
            if (backupType >= 0 && backupType <= 1)
            {
                BackupType = backupType;
            }
            else
            {
                //default value
                BackupType = 0; // Default to "Full"
            }
        }
    }

}
