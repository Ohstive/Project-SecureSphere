using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;
//using System.Windows.Forms; // Add this using statement

using Microsoft.Win32;

namespace SecureSphereV2.View
{

    
    public partial class LogsMenuPage : Page
    {
        private SharedDataService sharedDataService;

        public LogsMenuPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;
            DataContext = sharedDataService.LogInitInstance;
        }

        private void SelectFolderStatusLog_Click(object sender, RoutedEventArgs e)
        {
            
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select Folder",
                ShowNewFolderButton = false
            };

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                //folderPathTextBoxStatusLog.Text =
                sharedDataService.LogInitInstance.LogStatusFolderPath = folderDialog.SelectedPath;
            }
        }

        private void SelectFolderDailyLog_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select Folder"
            };

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
            {
                //folderPathTextBoxDailyLog.Text = folderDialog.SelectedPath;
                sharedDataService.LogInitInstance.DailylogFolderPath = folderDialog.SelectedPath;
            }
        }



    }
}
