using LightObserver;
using LightControler;

namespace FromLayer
{
    public partial class Form1 : Form
    {
        OverViewer overViewer;
        LightController lightController;
        Thread thread;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lightController = new LightController();
            foreach(string light in lightController.lights)
            {
                listBox1.Items.Add(light);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lightController.Stop = true;
            lightController.Reset();
        }

        private void btnFadeAllLights_Click(object sender, EventArgs e)
        {
            lightController.FadeAllLights(lightController.lights);
        }

        private void btnFadeLight_Click(object sender, EventArgs e)
        {
            
            lightController.FadeLight(listBox1.SelectedItem.ToString());
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            
            string light = listBox1.SelectedItem.ToString();
            if (light != null)
            {
                lightController.TurnOn(listBox1.SelectedItem.ToString());
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            lightController.Stop = true;
            string light = listBox1.SelectedItem.ToString();
            if(light != null)
            {
                lightController.TurnOff(listBox1.SelectedItem.ToString());
            }
            
        }
    }
}