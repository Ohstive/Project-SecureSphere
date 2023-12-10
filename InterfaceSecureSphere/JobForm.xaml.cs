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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace InterfaceSecureSphere
{

    public class JobConfiguration
    {
        // Propriétés de la tâche
        public string JobName { get; set; } // Rendre l'accès en écriture public
        public string SourceDirectoryPath { get; private set; }
        public string TargetDirectoryPath { get; private set; }
        public string BackupType { get; private set; }

        

        // Constructeur
        public JobConfiguration(string name, string source, string target, string backupType)
        {
            JobName = name;
            SourceDirectoryPath = source;
            TargetDirectoryPath = target;
            BackupType = backupType;

            
           
 
        }
    }
    public sealed partial class JobForm : Page, INotifyPropertyChanged
    {
        private JobConfiguration jobConfig;
        public event EventHandler<JobConfiguration> JobConfigurationSubmitted;


        

        public JobConfiguration JobConfig
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

            JobConfig = new JobConfiguration("", "", "", ""); // Initialisez avec des valeurs par défaut

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

        private void OnJobConfigurationSubmitted(JobConfiguration jobConfig)
        {
            JobConfigurationSubmitted?.Invoke(this, jobConfig);
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Vous pouvez accéder directement à JobConfig pour obtenir les valeurs mises à jour
            // Traitez les données de configuration du travail ici
            string jobName = JobConfig.JobName;
            string sourceType = ((ComboBoxItem)SourceTypeComboBox.SelectedItem)?.Content.ToString();
            string sourcePath = SourceTextBox.Text;
            string targetPath = TargetTextBox.Text;
            string backupType = ((ComboBoxItem)TypeComboBox.SelectedItem)?.Content.ToString();

            // Créez une nouvelle instance de JobConfiguration avec les données actuelles
            var submittedJobConfig = new JobConfiguration(jobName, sourcePath, targetPath, backupType);

            // Émettez l'événement avec la configuration du travail
            OnJobConfigurationSubmitted(submittedJobConfig);

            Frame.GoBack();

            System.Diagnostics.Debug.WriteLine($"JobName: {jobName}, SourceType: {sourceType}, Source: {sourcePath}, Target: {targetPath}, BackupType: {backupType}");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}