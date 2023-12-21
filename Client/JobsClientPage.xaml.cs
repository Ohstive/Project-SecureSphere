using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Client
{
    public partial class JobsClientPage
    {
        public ObservableCollection<JobStatusViewModel> Jobs { get; } = new ObservableCollection<JobStatusViewModel>();

        public JobsClientPage()
        {
            InitializeComponent();
            // Initialize the Jobs ListView through data binding


            // Add sample jobs (replace this with your actual logic)
            AddSampleJobs();
        }

        // Add a constructor that accepts arguments
        

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
