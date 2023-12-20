using SecureSphereV2.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;

namespace SecureSphereV2.ViewModel
{
    internal class Manager : INotifyPropertyChanged
    {
        private ObservableCollection<JobConfiguration> _listJobConfigurations;
        private ObservableCollection<string> _extensionPriority;
        private ObservableCollection<string> _extensionCrypted;
        private string _serverPassword;




        public ObservableCollection<JobConfiguration> ListJobConfigurations
        {
            get { return _listJobConfigurations; }
            set
            {
                if (_listJobConfigurations != value)
                {
                    _listJobConfigurations = value;
                    OnPropertyChanged(nameof(ListJobConfigurations));
                }
            }
        }

        public ObservableCollection<string> ExtensionPriority
        {
            get { return _extensionPriority; }
            set
            {
                if (_extensionPriority != value)
                {
                    _extensionPriority = value;
                    OnPropertyChanged(nameof(ExtensionPriority));
                }
            }
        }

        public ObservableCollection<string> ExtensionCrypted
        {
            get { return _extensionCrypted; }
            set
            {
                if (_extensionCrypted != value)
                {
                    _extensionCrypted = value;
                    OnPropertyChanged(nameof(ExtensionCrypted));
                }
            }
        }

        public string ServerPassword
        {
            get { return _serverPassword; }
            set
            {
                if (_serverPassword != value)
                {
                    _serverPassword = value;
                    OnPropertyChanged(nameof(ServerPassword));
                }
            }
        }


        public Manager()
        {
            // Initialize your collections or perform other setup here
            ListJobConfigurations = new ObservableCollection<JobConfiguration>();
            ExtensionPriority = new ObservableCollection<string>();
            ExtensionCrypted = new ObservableCollection<string>();
        }

        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Method to add a job configuration to the list
        public void AddJobConfiguration(JobConfiguration jobConfig)
        {
            ListJobConfigurations.Add(jobConfig);

 
        }


        public void RemoveJobConfiguration(JobConfiguration jobConfig)
        {
            ListJobConfigurations.Remove(jobConfig);

        }

        public void RemoveAllContent()
        {
            ListJobConfigurations.Clear();
            ExtensionPriority.Clear();
            ExtensionCrypted.Clear();
        }

    }
}
