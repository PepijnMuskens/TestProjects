using System.Text;
using MQTTnet.Client;
using MQTTnet;
namespace LightObserver
{
    public class MqttClient
    {
        public IMqttClient client;
        private string id;
        public MqttClient()
        {
            var mqttfactory = new MqttFactory();
            client = mqttfactory.CreateMqttClient();
            id = Guid.NewGuid().ToString();
        }

        public async Task<bool> Connect()
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
                })
                .Build();
            await client.ConnectAsync(options);
            return client.IsConnected;
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