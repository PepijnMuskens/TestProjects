using MQTTnet.Client;
using MQTTnet;
using System;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace MqttTestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Client client = new Client();
            await client.Connect();

            Console.ReadLine();
           
        }

        

    }
}