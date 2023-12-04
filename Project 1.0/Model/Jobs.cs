using Project_1._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1._0.Model
{
    internal class Jobs
    {
        private readonly JobConfiguration _jobConfiguration;
        public JobConfiguration JobConfiguration { get { return _jobConfiguration; } }

        private readonly ErrorManager _errorManager;
        private readonly DirectoryValidator _directoryValidator;
        private readonly TargetDirectoryValidator _targetDirectoryValidator;
        private readonly StorageValidator _storageValidator;

       
        private DirectoryInfo _sourceDirectory;
        private DirectoryInfo _targetDirectory;

        private FileInfo[] _allFileInSourcesList;
        public FileInfo[] AllFileInSourcesList { get { return _allFileInSourcesList; } }
        private int _typeOfJob;
        private int _typeOfSource;
        private long _sizeOfSource;
        private long _freeSpaceInTarget;

        public Jobs(string name, string source, string target, int type)
        {
            _errorManager = new ErrorManager();
            _jobConfiguration = new JobConfiguration(name, source, target, type);
            _directoryValidator = new DirectoryValidator(_errorManager);
            _targetDirectoryValidator = new TargetDirectoryValidator(_errorManager);
            _storageValidator = new StorageValidator(_errorManager);

            Initialize();
        }


        private void Initialize()
        {
            // Check Source
            CheckInitializeSourceDirectory();

            // Check Target
            CheckInitializeTargetDirectory();
        }

        private void CheckInitializeSourceDirectory()
        {
            if (_directoryValidator.IsDirectoryValid(_jobConfiguration.SourceDirectoryPath, out _sourceDirectory))
            {
                if (File.Exists(_jobConfiguration.SourceDirectoryPath))
                {
                    _typeOfSource = 0; // File

                    try
                    {
                        _allFileInSourcesList = new FileInfo[1];
                        _allFileInSourcesList[0] = new FileInfo(_jobConfiguration.SourceDirectoryPath);
                        _sizeOfSource = _allFileInSourcesList[0].Length;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Read or write access to the directory is not allowed.");
                        _errorManager.SetError("PermissionToAccessSourceError");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }
                else if (Directory.Exists(_jobConfiguration.SourceDirectoryPath))
                {
                    _typeOfSource = 1; // Directory

                    try
                    {
                        _allFileInSourcesList = _sourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);
                        _sizeOfSource = _sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Read or write access to the directory is not allowed.");
                        _errorManager.SetError("PermissionToAccessSourceError");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Source doesn't exist");
                    _errorManager.SetError("SourceDirectoryExistError");
                }
            }
            else
            {
                Console.WriteLine("Source is not a valid directory or file");
            }
        }


        private void CheckInitializeTargetDirectory()
        {
            if (_targetDirectoryValidator.IsTargetDirectoryValid(_jobConfiguration.TargetDirectoryPath, out _targetDirectory))
            {
                try
                {
                    _storageValidator.CheckReadWriteAccess(_targetDirectory);
                    _storageValidator.CheckFreeSpace(_targetDirectory, _sizeOfSource);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Read or write access to the directory is not allowed.");
                    _errorManager.SetError("PermissionToCreateInTargetError");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

    }
}

