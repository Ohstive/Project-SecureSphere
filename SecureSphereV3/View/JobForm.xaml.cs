using SecureSphereV2.Model;
using SecureSphereV2.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms; // Add this using directive

namespace SecureSphereV2.View
{


    public partial class JobForm : Window
    {

        public JobConfiguration JobConfig
        {
            get; set; 
        }


        private SharedDataService sharedDataService;

        public JobForm(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;
            HandleEncryptionCheckboxState();
            DataContext = sharedDataService.LogInitInstance;
        }

        private void BrowseSourceButton_Click(object sender, RoutedEventArgs e)
        {
            // Check the selected item in SourceTypeComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)SourceTypeComboBox.SelectedItem;

            if (selectedItem != null)
            {
                string sourceType = selectedItem.Content.ToString();

                // Use Windows Forms FolderBrowserDialog for folders
                if (sourceType.Equals("Folder", StringComparison.OrdinalIgnoreCase))
                {
                    using (var folderDialog = new FolderBrowserDialog())
                    {
                        DialogResult result = folderDialog.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            SourceTextBox.Text = folderDialog.SelectedPath;
                        }
                    }
                }
                // Use Windows Forms OpenFileDialog for files
                else if (sourceType.Equals("File", StringComparison.OrdinalIgnoreCase))
                {
                    using (var fileDialog = new OpenFileDialog())
                    {
                        fileDialog.Filter = "All Files (*.*)|*.*";

                        DialogResult result = fileDialog.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            SourceTextBox.Text = fileDialog.FileName;
                        }
                    }
                }
            }
        }


        private void BrowseTargetButton_Click(object sender, RoutedEventArgs e)
        {
            // Use Windows Forms FolderBrowserDialog
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    TargetTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }


        private void MyEncryptionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HandleEncryptionCheckboxState();
        }

        private void MyEncryptionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HandleEncryptionCheckboxState();
        }

        private void HandleEncryptionCheckboxState()
        {
            // Set the visibility of EncryptionKeyTextBox based on the checkbox state
            EncryptionKeyTextBlock.Visibility = MyEncryptionCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            EncryptionKeyTextBox.Visibility = MyEncryptionCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            EncryptionKeyTextBox.IsReadOnly = !(MyEncryptionCheckBox.IsChecked == true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            // Create a new JobConfiguration based on the entered data
            JobConfiguration newJobConfig = new JobConfiguration(
                name: NameTextBox.Text,
                source: SourceTextBox.Text,
                isSourceDirectory: (SourceTypeComboBox.SelectedIndex == 0), // Check if "Folder" is selected
                target: TargetTextBox.Text,
                backupType: ((ComboBoxItem)TypeComboBox.SelectedItem).Content.ToString(),
                encryptionKey: EncryptionKeyTextBox.Text,
                isEncryptionEnabled: MyEncryptionCheckBox.IsChecked == true
            );

            JobManager manager = new JobManager(newJobConfig, sharedDataService.LogInitInstance.DailylogFolderPath, sharedDataService.LogInitInstance.LogStatusFolderPath, sharedDataService.ExtensionCrypted, sharedDataService.ExtensionPriority);
            // Add the new job configuration to sharedDataService.ListJobConfigurations


            if (newJobConfig.JobName != "" && newJobConfig.SourceDirectoryPath != "" && newJobConfig.TargetDirectoryPath != "")
            {
                if (File.Exists(newJobConfig.SourceDirectoryPath) || Directory.Exists(newJobConfig.SourceDirectoryPath))
                {
                    if (manager.JobIsValid())
                    {
                        if (Directory.Exists(newJobConfig.TargetDirectoryPath))
                        {
                            sharedDataService.ListJobConfigurations.Add(newJobConfig);
                            Close();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Please fill in with a valid target path");
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Backup Job with software in are not allowed");
                    }
                }

                else
                {
                    System.Windows.MessageBox.Show("Please fill in with a valid source path");
                }

            }
            else
            {
                System.Windows.MessageBox.Show("Please fill in the necessary information");
            }
        }
    }
}






