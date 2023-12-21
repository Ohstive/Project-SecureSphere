using System;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace SecureSphereV2
{
    public class ServerSocketManager
    {
        private TcpListener tcpListener;
        private Thread listenerThread;

        public void Start()
        {
            tcpListener = new TcpListener(System.Net.IPAddress.Any, 63774);
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
                    byte[] data = new byte[256];
                    int bytesRead = stream.Read(data, 0, data.Length);
                    string clientData = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead).Trim();

                    // Vérifiez si le mot de passe est fourni (non vide)
                    if (string.IsNullOrWhiteSpace(clientData))
                    {
                        // Aucun mot de passe fourni, autoriser la connexion
                        SendJobList(stream);
                    }
                    else if (IsCorrectPassword(clientData))
                    {
                        // Password is correct, perform further actions
                        SendJobList(stream);
                    }
                    else
                    {
                        // Password is incorrect
                        Console.WriteLine("Incorrect password.");
                        // Inform the client about the incorrect password
                        byte[] response = System.Text.Encoding.ASCII.GetBytes("INCORRECT_PASSWORD");
                        stream.Write(response, 0, response.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                tcpClient.Close();
            }
        }

        private void SendJobList(NetworkStream stream)
        {
            // Convert the list of jobs to a JSON string (use a JSON serialization library like Newtonsoft.Json)
            string jobsJson = "[\"Job1\", \"Job2\", \"Job3\"]"; // Replace with your own logic

            byte[] response = System.Text.Encoding.ASCII.GetBytes(jobsJson);
            stream.Write(response, 0, response.Length);
        }

        private bool IsCorrectPassword(string enteredPassword)
        {
            // Replace this with your actual logic to validate the password
            SecureString savedPassword = GetSecureSavedPassword();
            return SecureStringEqual(savedPassword, SecureStringFromPlainText(enteredPassword));
        }

        private SecureString GetSecureSavedPassword()
        {
            // Replace this with your actual logic to retrieve the saved password securely
            return SecureStringFromPlainText("YourStoredSecurePassword");
        }

        private SecureString SecureStringFromPlainText(string plainText)
        {
            SecureString secureString = new SecureString();
            foreach (char c in plainText)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }

        private bool SecureStringEqual(SecureString s1, SecureString s2)
        {
            if (s1.Length != s2.Length)
                return false;

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            try
            {
                bstr1 = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(s1);
                bstr2 = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(s2);

                return string.Equals(System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr1), System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr2), StringComparison.Ordinal);
            }
            finally
            {
                if (bstr1 != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstr1);

                if (bstr2 != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstr2);
            }
        }
    }
}
