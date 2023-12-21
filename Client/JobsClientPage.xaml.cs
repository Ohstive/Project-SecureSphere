using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Client
{
    public partial class JobsClientPage : Window
    {
        public ObservableCollection<JobStatusViewModel> Jobs { get; } = new ObservableCollection<JobStatusViewModel>();

        public JobsClientPage()
        {
            InitializeComponent();
            // Initialize the Jobs ListView through data binding
            jobsListView.ItemsSource = Jobs;

            // Add sample jobs (replace this with your actual logic)
            AddSampleJobs();
        }

        // Add a constructor that accepts arguments
        public JobsClientPage(TcpClient client)
        {
            InitializeComponent();
            // You can use the TcpClient instance as needed.
            // For example, you might want to pass it to a ViewModel.

            // Initialize the Jobs ListView through data binding
            jobsListView.ItemsSource = Jobs;

            // Add sample jobs (replace this with your actual logic)
            AddSampleJobs();
        }

        private void AddSampleJobs()
        {
            // Add sample jobs to the ObservableCollection
            Jobs.Add(new JobStatusViewModel { JobName = "Job 1", Status = "Running" });
            Jobs.Add(new JobStatusViewModel { JobName = "Job 2", Status = "Completed" });
        }
    }

    public class JobStatusViewModel : INotifyPropertyChanged
    {
        private string jobName;
        public string JobName
        {
            get { return jobName; }
            set
            {
                if (value != jobName)
                {
                    jobName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
