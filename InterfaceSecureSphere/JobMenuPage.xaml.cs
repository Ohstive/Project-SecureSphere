using InterfaceSecureSphere;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;



public class JobManager
{
    private JobConfiguration _jobConfig;
    JobManager(JobConfiguration job)
    {
        job = _jobConfig;
    }
}


namespace InterfaceSecureSphere
{
    public sealed partial class JobMenuPage : Page
    {

        private ObservableCollection<JobConfiguration> backupJobs = new ObservableCollection<JobConfiguration>();

        public JobMenuPage()
        {
            this.InitializeComponent();
            JobsListView.ItemsSource = backupJobs;
            backupJobs.Add(new JobConfiguration("Job 1", "C:\\Users\\Public\\Documents\\", "C:\\Users\\Public\\Documents\\", "Full"));
            backupJobs.Add(new JobConfiguration("Job 2", "C:\\Users\\Public\\Documents\\", "C:\\Users\\Public\\Documents\\", "Full"));
            backupJobs.Add(new JobConfiguration("Job 3", "C:\\Users\\Public\\Documents\\", "C:\\Users\\Public\\Documents\\", "Differential"));
                

        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(JobForm), null);

            // Abonnez-vous à l'événement JobConfigurationSubmitted
            (frame.Content as JobForm).JobConfigurationSubmitted += JobConfigurationSubmittedHandler;
           

        }

        // Gérez l'événement JobConfigurationSubmitted pour récupérer les données de la nouvelle page
        private void JobConfigurationSubmittedHandler(object sender, JobConfiguration jobConfig)
        {
            // Ajoutez la configuration de travail à la liste
            backupJobs.Add(jobConfig);
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            JobConfiguration selectedJob = JobsListView.SelectedItem as JobConfiguration;

            if (selectedJob != null)
            {
                backupJobs.Remove(selectedJob);
            }

        }

        private void OnRunButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnRunAllButtonClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
