using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SecureSphereV2.Model;
using System.Diagnostics;
using SecureSphereV2.ViewModel;
using System.Windows.Navigation;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SecureSphereV2.View
{
    public partial class JobMenuPage : Page, INotifyPropertyChanged
    {
        private SharedDataService sharedDataService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<JobConfiguration> ListJobConfigurations
        {
            get { return sharedDataService?.ListJobConfigurations; }
        }

        public JobMenuPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;

            //sharedDataService.ListJobConfigurations.Add(new JobConfiguration("Name", @"C:\Users\Ostiv\Documents\Test3", false, @"C:\Users\Ostiv\Documents\Test4", "Full", "Key",true));
        }

        // OnPropertyChanged method to raise the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void OnRunButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button runButton && runButton.DataContext is JobConfiguration clickedJob)
            {
                if (!clickedJob.IsFinished)
                {
                    JobManager jobManager = new JobManager(clickedJob, sharedDataService.LogInitInstance.DailylogFolderPath, sharedDataService.LogInitInstance.LogStatusFolderPath, sharedDataService.ExtensionCrypted, sharedDataService.ExtensionPriority);
                    jobManager.RunJob();

                    //Change the play icon the finish one
                    PackIcon packIcon = VisualTreeHelperExtensions.FindChild<PackIcon>(runButton, "RunButtonIcon");
                    if (packIcon != null)
                    {
                        packIcon.Kind = PackIconKind.CheckCircle;
                    }
                }
            }
        }


        public static class VisualTreeHelperExtensions
        {
            public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
            {
                if (parent == null)
                    return null;

                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                    if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                        return typedChild;

                    T childOfChild = FindChild<T>(child, childName);

                    if (childOfChild != null)
                        return childOfChild;
                }

                return null;
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

            JobForm jobForm = new JobForm(sharedDataService);
            jobForm.Show();

        }

        private void OnDeleteAllButtonClick(object sender, RoutedEventArgs e) 
        {
            if (ListJobConfigurations.Count == 0) 
            {
                System.Windows.MessageBox.Show("There is no jobs.");
            }
            else if(System.Windows.MessageBox.Show("Do you really want to delete all jobs ?","Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ListJobConfigurations.Clear();
            }
            
            
            
        }
        private void OnRunAllButtonClick(object sender, RoutedEventArgs e)
        {
            // Handle the RunAll button click event (run all jobs)
            foreach (JobConfiguration jobConfig in ListJobConfigurations)
            {
                JobManager jobManager = new JobManager(jobConfig, sharedDataService.LogInitInstance.DailylogFolderPath, sharedDataService.LogInitInstance.LogStatusFolderPath, sharedDataService.ExtensionCrypted, sharedDataService.ExtensionPriority);
                jobManager.RunJob();
                System.Windows.MessageBox.Show("All jobs are finished.");


            }
        }

        private void OnRunAllAtOnceButtonClick(object sender, RoutedEventArgs e)
        {
            // List to store all created tasks
            List<Task> tasks = new List<Task>();

            // Handle the RunAll button click event (run all jobs)
            foreach (var jobConfig in ListJobConfigurations)
            {
                // Create a task for each JobManager.RunJob() and add it to the list
                tasks.Add(Task.Run(() => new JobManager(jobConfig, sharedDataService.LogInitInstance.DailylogFolderPath, sharedDataService.LogInitInstance.LogStatusFolderPath, sharedDataService.ExtensionCrypted, sharedDataService.ExtensionPriority).RunJob()));
            }


        }

    }
}