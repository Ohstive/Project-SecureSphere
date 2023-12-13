using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SecureSphereV2.Model;
using System.Diagnostics;
using SecureSphereV2.ViewModel;
using System.Windows.Media;

namespace SecureSphereV2.View
{
    public partial class JobMenuPage : Page, INotifyPropertyChanged
    {
        private SharedDataService sharedDataService;
        private ObservableCollection<JobConfiguration> listJobConfigurations;


        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<JobConfiguration> ListJobConfigurations
        {
            get { return listJobConfigurations; }
            set
            {
                if (value != listJobConfigurations)
                {
                    listJobConfigurations = value;
                    OnPropertyChanged(nameof(ListJobConfigurations));
                }
            }
        }

        public JobMenuPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;
            DataContext = sharedDataService.LogInitInstance;

            // Initialize ListJobConfigurations with ObservableCollection
            ListJobConfigurations = new ObservableCollection<JobConfiguration>();
            ListJobConfigurations.Add(new JobConfiguration("Name", @"Sources", @"Target", "TypeOfCopy", "Key"));
            ListJobConfigurations.Add(new JobConfiguration("Name", @"Sources", @"Target", "TypeOfCopy", "Key"));
            ListJobConfigurations.Add(new JobConfiguration("Name", @"Sources", @"Target", "TypeOfCopy", "Key"));

        }

        private void OnRunButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the Run button click event for the selected job
            if (JobsListView.SelectedItem is JobConfiguration selectedJob)
            {
                // Handle the run operation for JobConfiguration
            }
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the Delete button click event for the selected job
            if (JobsListView.SelectedItem is JobConfiguration selectedJob)
            {
                ListJobConfigurations.Remove(selectedJob);
            }
        }
        private void OnDeleteItemButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button deleteButton && deleteButton.Tag is JobConfiguration jobToDelete)
            {
                ListJobConfigurations.Remove(jobToDelete);
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            // ... your existing code

            // Create an instance of the JobForm window
            JobForm jobFormWindow = new JobForm();

            // Show the JobForm window
            jobFormWindow.Show();

            // Optionally, close the current window or handle navigation logic
            Window.GetWindow(this)?.Close();
        }


        private void OnRunAllButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the RunAll button click event (run all jobs)
            foreach (var jobConfig in ListJobConfigurations)
            {

            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
