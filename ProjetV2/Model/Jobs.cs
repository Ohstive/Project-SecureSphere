using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetV2.Model
{
   

    internal class Jobs
    {
        // Error Management
        private ErrorCatchException Error = new ErrorCatchException();

        // Job Management
        private string JobName;
        private string SourceDirectoryPath;
        private string TargetDirectoryPath;
        private string BackupType;

        // Directory Management
        private DirectoryInfo SourceDirectory;
        private DirectoryInfo TargetDirectory;

        // File Management
        private FileInfo[] AllFileInSourcesList;
        private FileInfo[] AllFileInTargetList;

        // Type Management: 0 for file, 1 for directory
        private int TypeOfJob;

        // Size Management
        private long SizeOfSource;
        private long FreeSpaceInTarget;

        // Constructor
        public Jobs(string name, string source, string target, string type)
        {
            // Job Management
            this.JobName = name;
            // Path directory Management
            this.SourceDirectoryPath = source;
            this.TargetDirectoryPath = target;
            this.BackupType = type;

            //Check Source
            CheckInitializeSourceDirectory();

            // Check Target
            CheckInitializeTargetDirectory();

        }

        // Initialize TypeOfJob based on source path
        private void CheckInitializeSourceDirectory()
        {
            if (File.Exists(this.SourceDirectoryPath))
            {
                Console.WriteLine("File");
                this.TypeOfJob = 0;
                try 
                {
                    this.AllFileInSourcesList = new FileInfo[1];
                    this.AllFileInSourcesList[0] = new FileInfo(this.SourceDirectoryPath);
                    this.SizeOfSource = this.AllFileInSourcesList[0].Length;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Read or write access to the directory is not allowed.");
                    this.Error.IsAccessToSourceAllowed();
                }
                
            }
            else if (Directory.Exists(this.SourceDirectoryPath))
            {
                Console.WriteLine("Directory");
                this.TypeOfJob = 1;
                try
                {
                    this.SourceDirectory = new DirectoryInfo(this.SourceDirectoryPath);
                    this.AllFileInSourcesList = SourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);
                    this.SizeOfSource = this.SourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Read or write access to the directory is not allowed.");
                    this.Error.IsAccessToSourceAllowed();
                } 
            }
            else
            {
                Console.WriteLine("Source doesn't exist");
                this.Error.PathSourceDirectoryExists();
            }
        }

        

        // Check the Target Directory
        private void CheckInitializeTargetDirectory()
        {
            if (File.Exists(this.TargetDirectoryPath))
            {
                Console.WriteLine("Target is a file");
                this.Error.TargetIsFile();
            }
            else if (Directory.Exists(this.TargetDirectoryPath))
            {
                Console.WriteLine("Target exists");
                try
                {
                    this.TargetDirectory = new DirectoryInfo(this.TargetDirectoryPath);
                    CheckReadWriteAccessToTarget();
                    CheckFreeSpaceInTarget();
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Read or write access to the directory is not allowed.");
                    this.Error.PermissionToCreateInTargetAllowed();
                }
            }
            else
            {
                Console.WriteLine("Target doesn't exist");
                this.Error.PathTargetDirectoryExists();
            }
        }

        // Check read and write access to the target directory
        private void CheckReadWriteAccessToTarget()
        {
            Console.WriteLine("Checking read and write access to the directory...");

            // Test read access
            this.AllFileInTargetList = TargetDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            // Test write access by creating a subdirectory
            DirectoryInfo subdirectory = this.TargetDirectory.CreateSubdirectory("TestSubdirectory");

            Console.WriteLine("Read and write access to the directory is allowed.");
            subdirectory.Delete();
        }

        // Check free space in the target directory
        private void CheckFreeSpaceInTarget()
        {
            Console.WriteLine("Checking free space in the target directory...");
            if (this.TargetDirectory != null && this.TargetDirectory.Root != null)
            {
                this.FreeSpaceInTarget = new DriveInfo(this.TargetDirectory.Root.Name).AvailableFreeSpace;
            }
            else
            {
                Console.WriteLine("Target root doesn't exist");
            }

            // If the user doesn't have enough storage space
            if (this.FreeSpaceInTarget < this.SizeOfSource)
            {
                Console.WriteLine("Not enough storage space available");
                this.Error.StorageSpaceAvailable();
            }
        }

        // Calculate the size of the source
        private void GetSizeSource()
        {
            Console.WriteLine("Source Size:");
            Console.WriteLine(FormatBytes(this.SizeOfSource));
        }

        // Function to convert bytes to a readable format
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

        // Temporary function for testing
        public void Display()
        {
            Console.WriteLine(this.JobName);
            Console.WriteLine(this.SourceDirectoryPath);
            Console.WriteLine(this.TargetDirectoryPath);
        }

        // Getter and Setter
        public string jobsName { get => this.JobName; set => this.JobName = value; }
        public string sourceDirectoryPath { get => this.SourceDirectoryPath; set => this.SourceDirectoryPath = value; }
        public string targetDirectoryPath { get => this.TargetDirectoryPath; set => TargetDirectoryPath = value; }
        public int typeOfJob { get => this.TypeOfJob; set => this.TypeOfJob = value; }
        public long sizeOfSource { get => this.SizeOfSource; set => this.SizeOfSource = value; }
        public ErrorCatchException error { get => this.Error; set => this.Error = value; }
        public string backupType { get => this.BackupType; set => this.BackupType = value}
    }

}
