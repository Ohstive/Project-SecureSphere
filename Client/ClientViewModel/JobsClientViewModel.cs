using SecureSphereV2.Model;
using SecureSphereV2.ViewModel;
using System.Collections.ObjectModel;

namespace Client
{
    public class JobsClientViewModel
    {
        public ObservableCollection<JobConfiguration> ListJobConfigurations { get; }

        public JobsClientViewModel(SharedDataService sharedDataService)
        {
            ListJobConfigurations = sharedDataService.ListJobConfigurations;
        }
    }
}