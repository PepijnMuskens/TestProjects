using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using static System.Net.Mime.MediaTypeNames;

namespace Mqtt_test_console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var mqttfactory = new MqttFactory();
            IMqttClient client = mqttfactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("    ", 1883)
                .WithCleanSession()
                .Build();
            await client.ConnectAsync(options);

            while (true)
            {
                Console.WriteLine("enter message or press enter to exit");
                string text = Console.ReadLine();
                if(text == "") break;
                await PublishMessageAsync(client, text);
                Console.WriteLine("Message Send");
            }
            await client.DisconnectAsync();

        }

        private static async Task PublishMessageAsync(IMqttClient client, string text)
        {
            string messagestring = text;
            var message = new MqttApplicationMessageBuilder()
                .WithTopic("TestTopicQwerty")
                .WithPayload(messagestring)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();
            if (client.IsConnected)
            {
                await client.PublishAsync(message);
            }
        }
    }
}
