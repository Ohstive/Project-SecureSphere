using SecureSphereV2.Client;
using SecureSphereV2.Model;
using SecureSphereV2.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Client
{
    public partial class JobsClientPage
    {
        

        // Assurez-vous d'accéder correctement à ListJobConfigurations depuis la classe parente
        // ou de le déclarer ici s'il appartient à cette classe.
        private SharedDataService sharedDataService;
        private ClientSocketManager clientSocketManager = new ClientSocketManager();

        public ObservableCollection<JobConfiguration> ListJobConfigurations { get; set; }

        public JobsClientPage(SharedDataService sharedDataService)
        {
            InitializeComponent();
            this.sharedDataService = sharedDataService;

            LoadJobs();
        }

        private void LoadJobs()
        {
            var jobConfigurationsFromServer = clientSocketManager.Connect("localhost", 63774);

            if (jobConfigurationsFromServer != null)
            {
                sharedDataService.ListJobConfigurations = new ObservableCollection<JobConfiguration>(jobConfigurationsFromServer);
                DataContext = new JobsClientViewModel(sharedDataService);
            }
            else
            {
                MessageBox.Show("Failed to load job configurations from the server.");
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
}
