using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace InterfaceSecureSphere
{
    public sealed partial class JobMenuPage : Page
    {
        // Classe représentant un travail de sauvegarde
        public class BackupJob
        {
            public string Nom { get; set; }
            public string Source { get; set; }
            public string Target { get; set; }
        }

        // Collection pour stocker les travaux de sauvegarde
        private ObservableCollection<BackupJob> backupJobs = new ObservableCollection<BackupJob>();

        public JobMenuPage()
        {
            this.InitializeComponent();

            // Assigner la collection à la source de données de la List
            backupJobs.Add(new BackupJob { Nom = "Test1", Source = "Source1", Target = "Target1" });
            backupJobs.Add(new BackupJob { Nom = "Test2", Source = "Source2", Target = "Target2" });
            backupJobs.Add(new BackupJob { Nom = "Test3", Source = "Source3", Target = "Target3" });
        }



        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            // Ajouter un travail de sauvegarde à la collection
            backupJobs.Add(new BackupJob { Nom = "NewJob", Source = "NewSource", Target = "NewTarget" });

            JobForm jobFormPage = new JobForm();
            jobFormPage.DataContext = backupJobs;
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(JobForm), null);
           
        }



        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // Supprimer le travail de sauvegarde sélectionné de la collection
           
        }

        private void OnRunButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnRunAllButtonClick(object sender, RoutedEventArgs e)
        {

        }

       


    }
}
