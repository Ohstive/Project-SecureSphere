
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using SecureSphereV2.Model;

namespace SecureSphereV2.ViewModel
{
    public class SharedDataService : INotifyPropertyChanged
    {
        private ObservableCollection<JobConfiguration> listJobConfigurations = new ObservableCollection<JobConfiguration>();
        private ObservableCollection<string> _extensionPriority;
        private ObservableCollection<string> _extensionCrypted;


        public SharedDataService()
        {
            ExtensionPriority = new ObservableCollection<string>();
            ExtensionCrypted = new ObservableCollection<string>();
        }
        public ObservableCollection<string> ExtensionPriority
        {
            get { return _extensionPriority; }
            set
            {
                if (_extensionPriority != value)
                {
                    _extensionPriority = value;
                    OnPropertyChanged(nameof(ExtensionPriority));
                }
            }
        }

        
        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<string> ExtensionCrypted
        {
            get { return _extensionCrypted; }
            set
            {
                if (_extensionCrypted != value)
                {
                    _extensionCrypted = value;
                    OnPropertyChanged(nameof(ExtensionCrypted));
                }
            }
        }
        public LogInit LogInitInstance { get; } = new LogInit();

        public ObservableCollection<JobConfiguration> ListJobConfigurations
        {
            get { return listJobConfigurations; }
            set
            {
                listJobConfigurations = value;
            }
        }
    }
}
public class LogInit : INotifyPropertyChanged
{
    private string rootPath;
    private string logStatusFolderPath;
    private string dailylogFolderPath;

    public string LogStatusFolderPath
    {
        get { return logStatusFolderPath; }
        set
        {
            logStatusFolderPath = value;
            OnPropertyChanged(nameof(LogStatusFolderPath));
        }
    }

    public string DailylogFolderPath
    {
        get { return dailylogFolderPath; }
        set
        {
            dailylogFolderPath = value;
            OnPropertyChanged(nameof(DailylogFolderPath));
        }
    }



    public LogInit()
    {
        // Get the root directory path of the application
        string systemDirectory = Environment.SystemDirectory;
        string systemDrive = Path.GetPathRoot(systemDirectory);
        rootPath = systemDrive;

        LogStatusFolderPath = Path.Combine(rootPath, "LogStatus");
        DailylogFolderPath = Path.Combine(rootPath, "DailyLog");
        CreateLogStatusFolder();
        CreateDailyLogFolder();
    }

    private void CreateDailyLogFolder()
    {
        try
        {

            // Check if the folder exists
            if (!Directory.Exists(logStatusFolderPath))
            {
                Directory.CreateDirectory(logStatusFolderPath);
            }
        }
        catch (Exception ex)
        {
            // Handle errors as needed
            System.Windows.MessageBox.Show($"Error creating the folder: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CreateLogStatusFolder()
    {
        try
        {
            if (!Directory.Exists(DailylogFolderPath))
            {
                Directory.CreateDirectory(DailylogFolderPath);
            }
        }
        catch (Exception ex)
        {
            // Handle errors as needed
            System.Windows.MessageBox.Show($"Error creating the folder: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

