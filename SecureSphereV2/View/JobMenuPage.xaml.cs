using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using SecureSphereV2.ViewModel;
using SecureSphereV2.Model;
using SecureSphereV2.View;

namespace SecureSphereV2.View
{
    public partial class JobMenuPage : Page
    {
        private SharedDataService sharedDataService;
        private List<JobManager> ListJob;

        public JobMenuPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;
            DataContext = sharedDataService.LogInitInstance;

            // Create a sample job
            JobConfiguration job = new JobConfiguration("Name", @"Sources", @"Target", "TypeOfCopy", "Key");
            JobManager jobManager = new JobManager(job, sharedDataService.LogInitInstance.DailylogFolderPath, sharedDataService.LogInitInstance.LogStatusFolderPath);

            // Initialize ListJob if necessary
            ListJob = new List<JobManager>();

            // Add the sample job to the list
            ListJob.Add(jobManager);
        }

        private void OnRunButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the Run button click event for the selected job
            if (JobsListView.SelectedItem is JobManager selectedJob)
            {
                selectedJob.JobRun();
            }
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the Delete button click event for the selected job
            if (JobsListView.SelectedItem is JobManager selectedJob)
            {
                ListJob.Remove(selectedJob);
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            // Add a new job (sample job for demonstration purposes)
            JobConfiguration newJobConfig = new JobConfiguration("NewJob", @"NewSource", @"NewTarget", "Incremental", "NewKey");
            JobManager newJobManager = new JobManager(newJobConfig, sharedDataService.LogInitInstance.DailylogFolderPath, sharedDataService.LogInitInstance.LogStatusFolderPath);

            // Add the new job to the list
            ListJob.Add(newJobManager);
        }

        private void OnRunAllButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the RunAll button click event (run all jobs)
            foreach (var job in ListJob)
            {
                job.JobRun();
            }
        }
    }
}


