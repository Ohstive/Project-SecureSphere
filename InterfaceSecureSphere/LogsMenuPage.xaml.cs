using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace InterfaceSecureSphere
{
    public sealed partial class LogsMenuPage : Page
    {
        public string LogFolderPath { get; set; }

        public LogsMenuPage()
        {
            this.InitializeComponent();

            // Charger la valeur depuis les paramètres de l'application
            LoadLogFolderPath();
            txtLogFolderPath.Text = LogFolderPath;
        }

        private void LoadLogFolderPath()
        {
            // Charger la valeur depuis les paramètres de l'application
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue("LogFolderPath", out object logFolderPath))
            {
                LogFolderPath = logFolderPath.ToString();
            }
            else
            {
                // Valeur par défaut si la clé n'est pas trouvée
                LogFolderPath = "C:\\Logs";
            }
        }

        private void SaveLogFolderPath()
        {
            // Sauvegarde de la valeur dans les paramètres de l'application
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["LogFolderPath"] = LogFolderPath;
        }

        private async void BrowseButtonClick(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // Afficher le FolderPicker et obtenir le dossier sélectionné
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            // Afficher le chemin du dossier sélectionné
            if (folder != null)
            {
                LogFolderPath = folder.Path;
                txtLogFolderPath.Text = LogFolderPath;
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            // Sauvegarde de la nouvelle valeur du chemin du dossier des journaux
            SaveLogFolderPath();
            Console.WriteLine($"Chemin du dossier des journaux enregistré : {LogFolderPath}");
        }

        // Appelé lorsque la navigation vers une autre page échoue
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // Sauvegarde de la valeur lorsque la page est quittée
            SaveLogFolderPath();
            base.OnNavigatingFrom(e);
        }
    }
}
