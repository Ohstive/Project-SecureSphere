using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Usb;
using System.Collections.ObjectModel;
using System.Diagnostics;
using InterfaceSecureSphere.Model;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace InterfaceSecureSphere
{

    
    public sealed partial class JobForm : Page, INotifyPropertyChanged
    {
        private InterfaceSecureSphere.Model.JobConfiguration jobConfig;
        public event EventHandler<InterfaceSecureSphere.Model.JobConfiguration> JobConfigurationSubmitted;


        public InterfaceSecureSphere.Model.JobConfiguration JobConfig
        {
            get { return jobConfig; }
            set
            {
                if (jobConfig != value)
                {
                    jobConfig = value;
                    OnPropertyChanged(nameof(JobConfig));
                }
            }
        }

        public JobForm()
        {
            this.InitializeComponent();

            JobConfig = new InterfaceSecureSphere.Model.JobConfiguration("", "", "", "",""); // Initialisez avec des valeurs par défaut

        }

  
        private async void BrowseSourceButton_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();
            var filePicker = new FileOpenPicker();

            switch (SourceTypeComboBox.SelectedIndex)
            {
                case 0: // Folder
                    folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                    folderPicker.FileTypeFilter.Add("*");

                    StorageFolder folder = await folderPicker.PickSingleFolderAsync();

                    if (folder != null)
                    {
                        SourceTextBox.Text = folder.Path;
                    }
                    break;

                case 1: // File
                    filePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                    filePicker.FileTypeFilter.Add("*");

                    StorageFile file = await filePicker.PickSingleFileAsync();

                    if (file != null)
                    {
                        SourceTextBox.Text = file.Path;
                    }
                    break;
            }
        }


        private async void BrowseTargetButton_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                TargetTextBox.Text = folder.Path;
            }
        }

        private ObservableCollection<InterfaceSecureSphere.Model.JobConfiguration> backupJobs = new ObservableCollection<InterfaceSecureSphere.Model.JobConfiguration>();
        private void OnJobConfigurationSubmitted(InterfaceSecureSphere.Model.JobConfiguration jobConfig)
        {
            JobConfigurationSubmitted?.Invoke(this, jobConfig);
            Debug.WriteLine("Job configuration submitted.");

        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string jobName = JobConfig.JobName;
            string sourceType = ((ComboBoxItem)SourceTypeComboBox.SelectedItem)?.Content.ToString();
            string sourcePath = SourceTextBox.Text;
            string targetPath = TargetTextBox.Text;
            string backupType = ((ComboBoxItem)TypeComboBox.SelectedItem)?.Content.ToString();
            string encryption = ((ComboBoxItem)EncryptionComboBox.SelectedItem)?.Content.ToString();

            if (jobName != "" && sourceType != "" && sourcePath != "" && targetPath != "" && backupType != "" && encryption != "")
            {
                // Create a new job configuration
                var submittedJobConfig = new InterfaceSecureSphere.Model.JobConfiguration(jobName, sourcePath, targetPath, backupType, encryption);

                // add the job to the list of jobs
                ((App)Application.Current).BackupJobs.Add(submittedJobConfig);

                Frame.GoBack();

                System.Diagnostics.Debug.WriteLine($"JobName: {jobName}, SourceType: {sourceType}, Source: {sourcePath}, Target: {targetPath}, BackupType: {backupType}, Encryption: {encryption}");
            }
         
        }




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}