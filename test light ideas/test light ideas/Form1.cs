using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace test_light_ideas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string s = "";
        List<Button> buttons;
        Thread thread;
        bool speedtest = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            buttons = new List<Button>();
            buttons.Add(button1);
            buttons.Add(button2);
            buttons.Add(button3);
            buttons.Add(button4);
            buttons.Add(button5);
            buttons.Add(button6);
            buttons.Add(button7);
            buttons.Add(button8);

            thread = new Thread(cycleCollor);
        }

        private void cycleCollor()
        {
            List<Color> colors = new List<Color>();
            colors.Add(Color.Red);
            colors.Add(Color.Orange);
            colors.Add(Color.Green);
            colors.Add(Color.Yellow);
            colors.Add(Color.Blue);
            colors.Add(Color.Purple);
            int i = colors.IndexOf(buttons[0].BackColor);
            foreach (Button button in buttons)
            {
                i++;
                if (i >= colors.Count)
                {
                    i = i - colors.Count;
                }
                button.BackColor = colors[i];
            }
        }

        private void dimLights()
        {
            foreach(Button button in buttons)
            {
                button.BackColor = Color.White;
            }
        }
        private void reactionSpeed()
        {
            bool temp = true;
            while (temp)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (speedtest == false)
                    {
                        temp = false;
                        break;
                    }
                    Thread.Sleep(100);
                    buttons[i].BackColor = Color.Blue;
                    if (i != 0)
                    {
                        buttons[i - 1].BackColor = Color.White;
                    }
                    else
                    {
                        buttons[buttons.Count - 1].BackColor = Color.White;
                    }

                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            cycleCollor();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dimLights();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (speedtest)
            {
                speedtest = false;
            }
            else
            {
                speedtest = true;
                Thread thread = new Thread(reactionSpeed);
                thread.Start();
            }
            
            
        }
    }
}