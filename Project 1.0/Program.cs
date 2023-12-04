using Project_1._0.Model;
using Project_1._0.ViewModel;
using Project_1._0.ViewModel.Services;

namespace Project_1._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ErrorManager errorManager = new ErrorManager();
            string sourceDirectory = @"C:\Users\Ostiv\Documents\Test";
            string targetDirectory = @"C:\Users\Ostiv\Documents\Test5";
            string name = "BackupJob";
            int type = 0;

            // Example instantiation of Jobs
            Jobs myJob = new Jobs(name,sourceDirectory, targetDirectory, type);
            Console.WriteLine("Job created");

            string sourceDirectoryPath = @"C:\Users\Ostiv\Documents\Test";
            string targetDirectoryPath = @"C:\Users\Ostiv\Documents\Test5";

            ILogger logger = new ConsoleLogger();
            IFileHandler fileHandler = new FileHandler(logger);
            IDirectoryHandler directoryHandler = new DirectoryHandler(fileHandler, logger);
            FolderLog folderLog = new FolderLog();

            directoryHandler.FullCopyDirectory(sourceDirectoryPath, targetDirectoryPath, folderLog);
            // directoryHandler.DifferentialCopyDirectory(sourceDirectoryPath, targetDirectoryPath, folderLog);




        }
    }
}
