using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SecureSphereV2.View
{
    public class SharedDataService
    { 
        public LogInit LogInitInstance { get; } = new LogInit();
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
        rootPath = @"C:\";
        LogStatusFolderPath = Path.Combine(rootPath, "LogStatus");
        DailylogFolderPath = Path.Combine(rootPath, "DailyLog");
        CreateLogStatusFolder();
        CreateDailyLogFolder();
    }

    private void CreateDailyLogFolder()
    {
        try
        {
            // Get the root directory path of the application

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