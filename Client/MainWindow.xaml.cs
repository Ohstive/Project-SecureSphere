using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using SecureSphereV2;
using SecureSphereV2.Client;
using SecureSphereV2.View;
using SecureSphereV2.ViewModel;

namespace Client
{
    public partial class MainWindow : Window
    {

        private readonly SharedDataService sharedDataService = new SharedDataService();
        private readonly ClientSocketManager clientSocketManager = new ClientSocketManager();
        private System.Windows.Shapes.Rectangle[] activeBarMenus;
        private bool[] switchMenu;
        private int activeMenuIndex = -1;
        

        public MainWindow()
        {
            InitializeComponent();
            activeBarMenus = new System.Windows.Shapes.Rectangle[] { ActiveBarMenu1, ActiveBarMenu4 };
            switchMenu = new bool[activeBarMenus.Length];
            LoadJobs();

        }

        private void LoadJobs()
        {
            var jobConfigurationsFromServer = clientSocketManager.Connect("127.0.0.1", 63774);

            if (jobConfigurationsFromServer != null)
            {
                // La connexion au serveur a réussi, vous pouvez maintenant mettre à jour les configurations de travail.
                sharedDataService.ListJobConfigurations = new ObservableCollection<SecureSphereV2.Model.JobConfiguration>(jobConfigurationsFromServer);
                DataContext = new JobsClientViewModel(sharedDataService);
            }
            else
            {
                MessageBox.Show("Failed to load job configurations from the server.");
            }
        }


        private void SendJobList(NetworkStream stream)
        {
            // TODO: Implement the logic to send job list to the client
            // Convert the list of jobs to a JSON string (use a JSON serialization library like Newtonsoft.Json)
            string jobsJson = "[{\"JobName\": \"Job1\", \"Status\": \"Running\"}, {\"JobName\": \"Job2\", \"Status\": \"Completed\"}]";

            byte[] response = System.Text.Encoding.ASCII.GetBytes(jobsJson);
            stream.Write(response, 0, response.Length);
        }

                          
        private void btnJobs_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobsClientPage(sharedDataService));
        }

        private void ToggleVisibility(int index)
        {
            if (index >= 0 && index < activeBarMenus.Length)
            {
                if (activeMenuIndex != index)
                {
                    if (activeMenuIndex != -1)
                    {
                        SetVisibility(activeMenuIndex, false);
                    }
                    SetVisibility(index, true);
                    activeMenuIndex = index;

                }
            }
        }

        private void SetVisibility(int index, bool isVisible)
        {
            if (index >= 0 && index < activeBarMenus.Length)
            {
                activeBarMenus[index].Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void btnParameters_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(3);
            MainContentFrame.Navigate(new ClientParametersPage());
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobsClientPage(sharedDataService));

        }
    }
}
