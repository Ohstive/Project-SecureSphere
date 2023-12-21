using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using SecureSphereV2;
using SecureSphereV2.View;
using SecureSphereV2.ViewModel;

namespace Client
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
            activeBarMenus = new System.Windows.Shapes.Rectangle[] { ActiveBarMenu1, ActiveBarMenu4 };
            switchMenu = new bool[activeBarMenus.Length];

            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobsClientPage());
        }

        


        private void SendJobList(NetworkStream stream)
        {
            // TODO: Implement the logic to send job list to the client
            // Convert the list of jobs to a JSON string (use a JSON serialization library like Newtonsoft.Json)
            string jobsJson = "[{\"JobName\": \"Job1\", \"Status\": \"Running\"}, {\"JobName\": \"Job2\", \"Status\": \"Completed\"}]";

            byte[] response = System.Text.Encoding.ASCII.GetBytes(jobsJson);
            stream.Write(response, 0, response.Length);
        }

        private async Task<TcpClient> TryConnectToServer(string enteredPassword)
        {
            try
            {
                TcpClient client = new TcpClient(System.Net.IPAddress.Loopback.ToString(), 63774);

                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // Vérifiez si le mot de passe sauvegardé existe
                    if (!string.IsNullOrWhiteSpace(enteredPassword))
                    {
                        // Envoyez le mot de passe saisi au serveur
                        writer.WriteLine(enteredPassword);
                        writer.Flush();
                    }

                    // Read the server response
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string serverResponse = await reader.ReadLineAsync();

                        if (serverResponse == "INCORRECT_PASSWORD")
                        {
                            MessageBox.Show("Incorrect password.");
                            client.Close();
                            return null;
                        }
                        else
                        {
                            // Connection successful
                            return client;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting: {ex.Message}");
                return null;
            }
        }


        private bool IsCorrectPassword(string enteredPassword)
        {
            // Retrieve the saved password securely
            SecureString savedPassword = GetSecureSavedPassword();

            // Compare entered password with saved password
            return SecureStringEqual(savedPassword, SecureStringFromPlainText(enteredPassword));
        }

        // Helper method to convert plain text to SecureString
        private SecureString SecureStringFromPlainText(string plainText)
        {
            SecureString secureString = new SecureString();
            foreach (char c in plainText)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        // Helper method to check equality of two SecureString instances
        private bool SecureStringEqual(SecureString s1, SecureString s2)
        {
            if (s1.Length != s2.Length)
                return false;

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(s1);
                bstr2 = Marshal.SecureStringToBSTR(s2);

                // Compare the two BSTR strings
                return string.Equals(Marshal.PtrToStringBSTR(bstr1), Marshal.PtrToStringBSTR(bstr2), StringComparison.Ordinal);
            }
            finally
            {
                if (bstr1 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr1);

                if (bstr2 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr2);
            }
        }

        // Dummy method to simulate retrieving the saved password securely
        private SecureString GetSecureSavedPassword()
        {
            // Replace this with your actual logic to retrieve the saved password securely
            return SecureStringFromPlainText("YourStoredSecurePassword");
        }

        private void btnJobs_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(0);
            MainContentFrame.Navigate(new JobsClientPage());
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
            MainContentFrame.Navigate(new JobsClientPage());
        }
    }
}
