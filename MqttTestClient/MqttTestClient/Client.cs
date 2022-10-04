using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            //Console.WriteLine("Received = " + Encoding.UTF8.GetString(arg.ApplicationMessage.Payload) + " on topic " + arg.ApplicationMessage.Topic);
            JObject jObject = JObject.Parse(Encoding.Default.GetString(arg.ApplicationMessage.Payload));
            
            if (jObject != null)
            {
                string test = jObject["attributeState"]["ref"]["name"].ToString();
                if ("colourRgbLed" == test)
                {
                    Console.WriteLine(jObject["attributeState"]["value"].ToString());
                }
            }
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
                    SslProtocol = System.Security.Authentication.SslProtocols.Tls12
                    //Certificates = new List<X509Certificate>()
                })
                .Build();
            await client.ConnectAsync(options);
            Console.WriteLine(client.IsConnected);
        }

        public async Task Subscribe(string assetid)
        {
            var topicFilter = new MqttTopicFilterBuilder()
               .WithTopic("strijp/" + id + "/attribute/+/" + assetid)
               .Build();
            await client.SubscribeAsync(topicFilter);
        }
    }
}