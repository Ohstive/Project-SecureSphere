
using SecureSphere_V1.ViewModel;
using SecureSphereV2._0.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SecureSphereV2._0
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnCopyFolderButtonClick(object sender, RoutedEventArgs e)
        {
            JobConfiguration job = new JobConfiguration("Job1", @"C:\Users\Ostiv\Documents\Test3", @"C:\Users\Ostiv\Documents\Test4", "Full", "Sequential");
            JobManager jobManager = new JobManager(job, @"C:\Users\Ostiv\Documents\Log", @"C:\Users\Ostiv\Documents\LogStatut");
            jobManager.JobRun();
            Test.Text = "Reussi";
        }
    }
}
