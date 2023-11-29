using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * try
 * {}
 * Catch (Exception e)
 * {
 * Console.WriteLine(e.Message);
 * }
 */
namespace ProjetV2.Model
{
    internal class Jobs
    {
        //Error Management
        private ErrorCatchException Error = new ErrorCatchException();
        public ErrorCatchException error { get => this.Error; set => this.Error = value; }

        //Job Management
        private string JobName;
        private string SourceDirectory;
        private string TargetDirectory;

        //Directory Management
        private System.IO.DirectoryInfo dir1;
        private System.IO.DirectoryInfo dir2;
        private System.IO.FileInfo[] AllFileInSourcesList;
        private System.IO.FileInfo[] AllFileInTargetList;

        //Type Management if the user want to copy file or directory 0 for file and 1 for directory
        private int TypeOfJob;

        

        public Jobs(string name, string source, string target) 
        { 
            this.JobName = name;
            this.SourceDirectory = source;
            this.TargetDirectory = target;

            if (File.Exists(this.SourceDirectory))
            {
                Console.WriteLine("Le fichier existe.");
            }

            if (Directory.Exists(source))
            {
               this.dir1 = new System.IO.DirectoryInfo(this.sourceDirectory);
               this.dir2 = new System.IO.DirectoryInfo(this.targetDirectory);
               // Take a snapshot of the file system.  
               this.AllFileInSourcesList = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
               this.AllFileInTargetList = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                
            }
            else
            {

            }   
        }
        public string jobsName { get=>this.JobName; set=> this.JobName=value;}
        public string sourceDirectory { get => this.SourceDirectory; set => this.SourceDirectory= value; }
        public string targetDirectory { get=> this.TargetDirectory; set=>TargetDirectory=value; }
        //Temporary function for testing
        public void Affiche() 
        {
            Console.WriteLine(this.JobName);
            Console.WriteLine(this.SourceDirectory);
            Console.WriteLine(this.TargetDirectory);
        }

        public void A()
        {

        }
    }
}
