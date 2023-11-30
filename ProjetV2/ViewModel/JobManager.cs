using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetV2.Model;

namespace ProjetV2.ViewModel
{
    internal class JobManager
    {
        private Jobs BackupJobs;

        //public Jobs jobs { get => BackupJobs; set => BackupJobs = value; }
        public Jobs jobs { get => BackupJobs; set => BackupJobs = value; }


        private string[] AlreadySave;

        public JobManager(Jobs backupJobs)
        {
            this.BackupJobs = backupJobs;
        }

        public void DifferentialCopy()
        {

        }
        public void FullCopy()
        {
            Console.WriteLine("Full Copy");
        }

        public void FileCompare()
        {
            Console.WriteLine("Full Copy");
        }

    }
}
