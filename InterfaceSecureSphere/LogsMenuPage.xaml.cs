using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace InterfaceSecureSphere
{
    public sealed partial class LogsMenuPage : Page
    {
        public string LogFolderPath { get; set; }

        public LogsMenuPage()
        {
            this.InitializeComponent();

            // Load the current path of the logs folder (can be retrieved from your application settings)
            LogFolderPath = "C:\\Logs";
            txtLogFolderPath.Text = LogFolderPath;
        }

        private async void BrowseButtonClick(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // Show the FolderPicker and get the selected folder
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            // Display the selected folder path
            if (folder != null)
            {
                LogFolderPath = folder.Path;
                txtLogFolderPath.Text = LogFolderPath;
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            // Save the new path of the logs folder (you need to implement the save logic)
            Console.WriteLine($"Logs folder path saved: {LogFolderPath}");
        }
    }
}
