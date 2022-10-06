using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LightObserver
{
    public class LightPole
    {
        public string Name { get; private set; }
        public float[] Coords { get; private set; }  
        public Light[] Lights { get; private set; }

        public LightPole(string name, float[] coords)
        {
            Name = name;
            Coords = coords;
            Lights = new Light[0];
        }

        public LightPole(string name, float[] coords, Light light) : this(name, coords)
        {
            Lights = new Light[1];
            Lights[0] = light;
        }

        public LightPole(string name, float[] coords, Light[] lights) : this(name, coords)
        {
            Lights = new Light[lights.Length -1];
            for (int i = 0; i < lights.Length; i++)
            {
                Lights[i] = lights[i];
            }
        }

        public void AddLight(Light light)
        {
            Light[] lights = new Light[Lights.Length];
            for (int i = 0; i < Lights.Length; i++)
            {
                lights[i] = Lights[i];
            }
            lights[Lights.Length] = light;
            Lights = lights;
        }
    }

    public class Light
    {
        public string Name { get; private set; }
        public string Id { get; private set; }
        public int Height { get; private set; }
        public DirectionStruct Direction { get; private set; }
        public int WhiteBrightness { get; private set; }
        public int WarmBrightness { get; private set; }
        public Color Color { get; private set; }

        public Light(string name, string id, int height, DirectionStruct direction)
        {
            Name = name;
            Id = id;
            Height = height;
            Direction = direction;  
            Color = Color.Black; 
        }


        public enum DirectionStruct { North, East, South, West }
    }
}
