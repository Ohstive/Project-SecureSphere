using System.Configuration;
using System.Data;
using System.Windows;
using System;
using System.Threading;

namespace SecureSphereV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static string SavedPassword { get; set; }
        private const string MutexName = "SecureSphere";

        // Mutex declaration
        private static Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            mutex = new Mutex(true, MutexName, out createdNew);

            // If the mutex already exists, another instance of the application is already running
            if (!createdNew)
            {
                System.Windows.MessageBox.Show("Another instance of the application is already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0); // Close the application
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Release the mutex when the application exits
            mutex.ReleaseMutex();
            mutex.Dispose();
            base.OnExit(e);
        }
    }
}
