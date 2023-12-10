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
        public int BackupType { get; private set; }

        // Constructeur
        public JobConfiguration(string name, string source, string target, int backupType)
        {
            JobName = name;
            SourceDirectoryPath = source;
            TargetDirectoryPath = target;

            // Validez que backupType est dans la plage spécifiée
            if (backupType >= 0 && backupType <= 1)
            {
                BackupType = backupType;
            }
            else
            {
                // Valeur par défaut
                BackupType = 0; // Par défaut à "Full"
            }
        }
    }
    public sealed partial class JobForm : Page, INotifyPropertyChanged
    {
        private JobConfiguration jobConfig;

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

            JobConfig = new JobConfiguration("", "", "", 0); // Initialisez avec des valeurs par défaut

            // Désactiver l'agrandissement de la fenêtre
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(Width = 700, Height = 325);

            // Désactiver la possibilité de redimensionner la fenêtre
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Rétablir la taille préférée
            ApplicationView.GetForCurrentView().TryResizeView(new Size(700, 325));
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

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Vous pouvez accéder directement à JobConfig pour obtenir les valeurs mises à jour
            // Traitez les données de configuration du travail ici
            string jobName = JobConfig.JobName;
            string sourceType = ((ComboBoxItem)SourceTypeComboBox.SelectedItem)?.Content.ToString();
            string sourcePath = SourceTextBox.Text;
            string targetPath = TargetTextBox.Text;
            string backupType = ((ComboBoxItem)TypeComboBox.SelectedItem)?.Content.ToString();

            System.Diagnostics.Debug.WriteLine($"JobName: {jobName}, SourceType: {sourceType}, Source: {sourcePath}, Target: {targetPath}, BackupType: {backupType}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}