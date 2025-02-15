using System;
using System.Net.Sockets;
using System.Text;

namespace TcpClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverIp = "127.0.0.1";
            const int port = 8888;

            try
            {
                using (TcpClient client = new(serverIp, port))
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new(stream, Encoding.UTF8))
                using (StreamWriter writer = new(stream, Encoding.UTF8) { AutoFlush = true })
                {
                    string[] commands = { "GET_STATUS", "GET_TEMP", "GET_STATUS", "INVALID_CMD" };

                    foreach (var cmd in commands)
                    {
                        Console.WriteLine($"Sending command: {cmd}");
                        writer.WriteLine(cmd);

                        string response = reader.ReadLine();
                        Console.WriteLine($"Response: {response}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
        }
    }
}