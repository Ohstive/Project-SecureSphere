using MaterialDesignThemes.Wpf;
using SecureSphereV2.Model;
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
        public JobForm()
        {
            InitializeComponent();
            HandleEncryptionCheckboxState();

        }

        private void BrowseSourceButton_Click(object sender, RoutedEventArgs e)
        {
            // Use Windows Forms FolderBrowserDialog
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    SourceTextBox.Text = folderDialog.SelectedPath;
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

        public ObservableCollection<JobConfiguration> ListJobConfigurations { get; } = new ObservableCollection<JobConfiguration>();
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve data from the UI controls
            string jobName = NameTextBox.Text;
            string sourceType = ((ComboBoxItem)SourceTypeComboBox.SelectedItem)?.Content.ToString();
            string source = SourceTextBox.Text;
            string target = TargetTextBox.Text;
            bool isEncryptionEnabled = MyEncryptionCheckBox.IsChecked ?? false;
            string encryptionKey = EncryptionKeyTextBox.Text;
            string jobType = ((ComboBoxItem)TypeComboBox.SelectedItem)?.Content.ToString();

            // Handle the logic to add a job with the retrieved information
            JobConfiguration newJob = new JobConfiguration(jobName, source, target, jobType, encryptionKey);

            // Add the new job to ListJobConfigurations
            


        }
    }
}






