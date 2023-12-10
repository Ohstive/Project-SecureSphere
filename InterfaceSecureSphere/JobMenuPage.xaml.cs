using InterfaceSecureSphere;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;



public class JobManager
{
    private JobConfiguration _jobConfig;

    // Correction : le paramètre doit être assigné à _jobConfig, pas l'inverse
    public JobManager(JobConfiguration job)
    {
        _jobConfig = job;
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
            backupJobs = ((App)Application.Current).BackupJobs;
            JobsListView.ItemsSource = backupJobs;

            
            Debug.WriteLine(backupJobs.Count);



        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(JobForm), null);

            frame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is JobForm jobForm)
            {
                // Abonnez-vous à l'événement JobConfigurationSubmitted
                jobForm.JobConfigurationSubmitted += JobConfigurationSubmittedHandler;
            }
        }

        private void JobConfigurationSubmittedHandler(object sender, JobConfiguration jobConfig)
        {
            // Ajoutez la configuration de travail à la liste
            backupJobs.Add(jobConfig);

            // Revenez à la page précédente (JobMenuPage)
            Frame.GoBack();
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
