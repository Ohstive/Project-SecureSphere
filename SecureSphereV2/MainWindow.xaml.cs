using System.Windows;
using System.Windows.Navigation;
using SecureSphereV2.ViewModel;
using SecureSphereV2.Model;
using SecureSphereV2.View;




namespace SecureSphereV2
{
    public partial class MainWindow : Window
    {
        private SharedDataService sharedDataService = new SharedDataService();

        public MainWindow()
        {
            InitializeComponent();
            NavigateToJobMenu();  // Set NavigateToJobMenu as the default action
            ContentFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        private void NavigateToJobMenu(object sender = null, RoutedEventArgs e = null)
        {
            ContentFrame.Navigate(new JobMenuPage(sharedDataService));
        }

        private void NavigateToLogsMenu(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LogsMenuPage(sharedDataService));

        }

        private void ToggleMenuVisibility(object sender, RoutedEventArgs e)
        {
            NavigateToJobMenu();  // You can change this to navigate to the default menu
        }
    }
}