// ServerSocketManager.cs

using Newtonsoft.Json;
using SecureSphereV2.Model;
using SecureSphereV2.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SecureSphereV2
{
    public class ServerSocketManager
    {
        private TcpListener tcpListener;
        private Thread listenerThread;
        private SharedDataService sharedDataService;

        public ServerSocketManager(SharedDataService sharedDataService)
        {
            this.sharedDataService = sharedDataService;
        }

        public void Start()
        {
            tcpListener = new TcpListener(IPAddress.Any, 63774);
            listenerThread = new Thread(ListenForClients);
            listenerThread.Start();
        }

        private void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private void HandleClient(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;

            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    // Example: Send the ListJobConfigurations to the client
                    ObservableCollection<JobConfiguration> jobConfigurations = GetJobConfigurations();
                    string jobConfigurationsJson = JsonConvert.SerializeObject(jobConfigurations);

                    // Send the JSON data to the client
                    byte[] response = Encoding.ASCII.GetBytes(jobConfigurationsJson);
                    stream.Write(response, 0, response.Length);
                    byte[] message = Encoding.ASCII.GetBytes("Hello from the server!");
                    stream.Write(message, 0, message.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling client: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                tcpClient.Close();
            }
        }

        private ObservableCollection<JobConfiguration> GetJobConfigurations()
        {
            // Replace this with your actual logic to retrieve ListJobConfigurations
            Debug.WriteLine("Retrieving job configurations...");

            return sharedDataService?.ListJobConfigurations;
        }
    }
}
