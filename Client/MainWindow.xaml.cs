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

namespace Client
{
    public partial class MainWindow : Window
    {
        private SharedDataService sharedDataService;

        public MainWindow()
        {
            InitializeComponent();
            sharedDataService = new SharedDataService(); // You may need to create an instance of SharedDataService here
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = serverPasswordBox.Password;

            try
            {
                // Continue with the connection to the server (async operation)
                TcpClient client = await Task.Run(() => TryConnectToServer(enteredPassword));

                if (client != null)
                {
                    JobsClientPage jobsPage = new JobsClientPage(client);
                    NavigationWindow navigationWindow = new NavigationWindow();
                    navigationWindow.Content = jobsPage;
                    navigationWindow.Show();

                    // Close the current window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Connection failed. Check credentials.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                // Log the exception for further analysis
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

        private TcpClient TryConnectToServer(string password)
        {
            try
            {
                TcpClient client = new TcpClient(System.Net.IPAddress.Loopback.ToString(), 12345);

                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.WriteLine(password);
                    writer.Flush();

                    // Read the server response
                    string serverResponse = reader.ReadLine();

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
    }
}
