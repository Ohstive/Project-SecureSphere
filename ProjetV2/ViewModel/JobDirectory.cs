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
    internal class JobDirectory
    {
        private Jobs BackupJobs;
        private ErrorHandler Error;
      

        //To open the files and directory for the backup
        

        private string[] AlreadySave;

        public JobDirectory(Jobs backupJobs, ErrorHandler error)
        {
            this.BackupJobs = backupJobs;
            this.Error = error;
 
        }

        public void DifferentialCopy()
        {
            BackupJobs.Affiche();
        }
        public void FullCopy()
        {
            /*
            BackupJobs.Affiche();
            foreach (var file in AllFileInSourcesList)
            {
                // Construire le chemin de destination en utilisant le chemin B
                Console.WriteLine(BackupJobs.targetDirectory);
                Console.WriteLine(file.FullName);

                string destinationPath = System.IO.Path.Combine(BackupJobs.targetDirectory, file.FullName.Substring(BackupJobs.sourceDirectory.Length + 1));

                // Assurez-vous que le répertoire de destination existe
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationPath));

                // Copier le fichier vers le nouveau chemin
                System.IO.File.Copy(file.FullName, destinationPath, true);

                Console.WriteLine($"Copié : {file.FullName} vers {destinationPath}");
            }
            */
        }

        public void FileCompare()
        {
            BackupJobs.Affiche();
        }

    }
}
