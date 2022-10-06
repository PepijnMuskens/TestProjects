using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightObserver
{
    public class OverViewer
    {
        public MqttClient Client;
        public readonly List<LightPole> LightPoles;

        public OverViewer()
        {
            Client = new MqttClient();
            Client.client.ApplicationMessageReceivedAsync += Update;
            LightPoles = new List<LightPole>();
        }

        private static Task Update(MqttApplicationMessageReceivedEventArgs arg)
        {
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(arg.ApplicationMessage.Payload) + " on topic " + arg.ApplicationMessage.Topic);


            return Task.CompletedTask;
        }
        public async Task ConnectClient()
        {
            if(await Client.Connect() == false)
            {
                throw new Exception("Failed to connect to host");
            }
        }
        
        public async Task AddLight(Light light, string lightPoleName)
        {
            LightPole lightPole = LightPoles.Find(l => l.Name == lightPoleName);

            if(lightPole == null)
            {
                return;
            }
            lightPole.AddLight(light);
            await Client.Subscribe(light.Id);
        }

        public void AddLightPole(LightPole lightPole)
        {
            LightPole lightpoletest = LightPoles.Find(p => p.Name == lightPole.Name);
            if(lightpoletest == null)
            {
                LightPoles.Add(lightPole);
            }
        }
    }
}
