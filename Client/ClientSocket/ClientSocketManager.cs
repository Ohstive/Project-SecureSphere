// ClientSocketManager.cs

using Newtonsoft.Json;
using SecureSphereV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace SecureSphereV2.Client
{
    public class ClientSocketManager
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public List<JobConfiguration> Connect(string ipAddress, int port)
        {
            try
            {
                client = new TcpClient(ipAddress, port);
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                // Read the list of job configurations from the stream
                string jobConfigurationsJson = reader.ReadLine();

                // Convert the JSON to a list of JobConfiguration
                List<JobConfiguration> jobConfigurations = JsonConvert.DeserializeObject<List<JobConfiguration>>(jobConfigurationsJson);

                return jobConfigurations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error connecting: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
                return null; // Return null in case of an error
            }
        }
    }
}
