using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetV2.Model
{
    internal class Jobs
    {
        //Error Management
        private ErrorCatchException Error = new ErrorCatchException();


        //Job Management
        private string JobName;
        private string SourceDirectoryPath;
        private string TargetDirectoryPath;

        //Directory Management
        private System.IO.DirectoryInfo SourceDirectory;
        private System.IO.DirectoryInfo TargetDirectory;
        //File Management
        private readonly System.IO.FileInfo[] AllFileInSourcesList;
        private readonly System.IO.FileInfo[] AllFileInTargetList;

        //Type Management if the user want to copy file or directory 0 for file and 1 for directory
        private int TypeOfJob;

        //Size Management
        private long SizeOfSource;
        private long FreeSpaceInTarget;

        //Constructor
        public Jobs(string name, string source, string target)
        {
            //Job Management
            this.JobName = name;
            //Path directory Management
            this.SourceDirectoryPath = source;
            this.TargetDirectoryPath = target;
            //Type Management

            //If the user want to copy a file to directory
            if (File.Exists(this.SourceDirectoryPath))
            {
                Console.WriteLine("Fichier");
                this.TypeOfJob = 0;
                this.AllFileInSourcesList = new System.IO.FileInfo[1];
                this.AllFileInSourcesList[0] = new System.IO.FileInfo(this.SourceDirectoryPath);
                this.sizeOfSource = this.AllFileInSourcesList[0].Length;
                this.TargetDirectory = new System.IO.DirectoryInfo(this.TargetDirectoryPath);

            }
            //If the user want to copy a directory to directory
            else if (Directory.Exists(source))
            {
                Console.WriteLine("Dossier");
                this.TypeOfJob = 1;
                this.SourceDirectory = new System.IO.DirectoryInfo(this.SourceDirectoryPath);
                this.TargetDirectory = new System.IO.DirectoryInfo(this.TargetDirectoryPath);
                // Take a snapshot of the file system.
                this.AllFileInSourcesList = SourceDirectory.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                this.AllFileInTargetList = TargetDirectory.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                this.SizeOfSource = this.SourceDirectory.EnumerateFiles("*", System.IO.SearchOption.AllDirectories).Sum(fi => fi.Length);
            }
            //if the user enter a wrong source path
            else
            {
                Console.WriteLine("Source doesn't exist");
                this.error.PathSourceDirectoryExists();
            }

            // if the user enter a file as target
            if (File.Exists(target))
            {
                Console.WriteLine("Target is a file");
                this.error.TargetIsFile();
            }
            //if the user enter a directory as target
            else if (Directory.Exists(target))
            {
                Console.WriteLine("Target exist");

                //Take a snapshot of the file system.
                if (this.TargetDirectory != null && this.TargetDirectory.Root != null)
                {
                    this.FreeSpaceInTarget = new System.IO.DriveInfo(this.TargetDirectory.Root.Name).AvailableFreeSpace;
                }
                else
                {
                    Console.WriteLine("Target root doesn't exist");
                }
              
                //if the user doesn't have enough storage space
                if (this.FreeSpaceInTarget < this.SizeOfSource)
                {
                    Console.WriteLine("Not enough storage space available");
                    this.error.StorageSpaceAvailable();
                }
            }
            //if the user enter a wrong target path
            else
            {
                Console.WriteLine("Target doesn't exist");
                this.error.PathTargetDirectoryExists();
            }
        }
        // Get the size of the source in a readable format
        public void GetSourceSize()
        {
            Console.WriteLine(FormatBytes(this.SizeOfSource));
        }

        //Function to convert bytes to a readable format
        public string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            
            while (bytes >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                bytes /= 1024;
                suffixIndex++;
            }

            return $"{bytes} {suffixes[suffixIndex]}";
        }

        //Getter and Setter
        public string jobsName { get => this.JobName; set => this.JobName = value; }
        public string sourceDirectoryPath { get => this.SourceDirectoryPath; set => this.SourceDirectoryPath = value; }
        public string targetDirectoryPath { get => this.TargetDirectoryPath; set => TargetDirectoryPath = value; }
        public int typeOfJob { get => this.TypeOfJob; set => this.TypeOfJob = value; }
        public long sizeOfSource { get => this.SizeOfSource; set => this.SizeOfSource = value; }
        public ErrorCatchException error { get => this.Error; set => this.Error = value; }

        //Temporary function for testing
        public void Affiche()
        {
            Console.WriteLine(this.JobName);
            Console.WriteLine(this.SourceDirectoryPath);
            Console.WriteLine(this.TargetDirectoryPath);
        }
    }
}
