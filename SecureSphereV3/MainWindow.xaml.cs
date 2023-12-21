
using SecureSphereV2.View;
using SecureSphereV2.ViewModel;
using System.Windows;
using System.Windows.Navigation;

namespace SecureSphereV2
{
    public partial class MainWindow : Window
    {
        private System.Windows.Shapes.Rectangle[] activeBarMenus;
        private bool[] switchMenu;
        private int activeMenuIndex = -1;
        private SharedDataService sharedDataService = new SharedDataService();

        public MainWindow()
        {
            InitializeComponent(); 
            activeBarMenus = new System.Windows.Shapes.Rectangle[] { ActiveBarMenu1, ActiveBarMenu2, ActiveBarMenu4 };
            switchMenu = new bool[activeBarMenus.Length];

            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobMenuPage(sharedDataService));
        }

        private void SetVisibility(int index, bool isVisible)
        {
            if (index >= 0 && index < activeBarMenus.Length)
            {
                activeBarMenus[index].Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
            }
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


        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobMenuPage(sharedDataService));
        }

        private void btnJobs_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobMenuPage(sharedDataService));
        }


        private void btnLogs_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(1);
            MainContentFrame.Navigate(new LogsMenuPage(sharedDataService));
        }

       

        private void btnParameters_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(3);
            MainContentFrame.Navigate(new ParametersPage(sharedDataService));
        }


    }
}
