using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MqttTestClient
{
    internal class Client
    {
        private IMqttClient client;
        private string id;
        public Client()
        {
            var mqttfactory = new MqttFactory();
            client = mqttfactory.CreateMqttClient();
            id = Guid.NewGuid().ToString();
            client.ApplicationMessageReceivedAsync += Client_ApplicationMessageReceivedAsync;
        }

        private static Task Client_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(arg.ApplicationMessage.Payload) + " on topic " + arg.ApplicationMessage.Topic);
            return Task.CompletedTask;
        }

        public async Task Connect()
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId(id)
                .WithTcpServer("staging.strijp.openremote.app", 8883)
                .WithCredentials("strijp:fontys", "TsuunSkVxfmSvkDOXpaBQygcW6Lpn8RN")
                .WithCleanSession()
                .WithTls(new MqttClientOptionsBuilderTlsParameters()
                    {
                        UseTls = true,
                        SslProtocol = System.Security.Authentication.SslProtocols.Tls12,
                        Certificates = new List<X509Certificate>()
                    })
                .Build();
            await client.ConnectAsync(options);
            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic("strijp/" + id + "/asset/3v4kD62VpTAHAmMFW536hQ")
                .Build();
            Console.WriteLine("connected");
            await client.SubscribeAsync(topicFilter);
            Console.WriteLine("subscribed");
        }
    }
}
