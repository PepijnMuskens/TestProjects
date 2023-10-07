using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using datacreator.Limits;
using System.Diagnostics;

namespace datacreator
{
    public partial class Form1 : Form
    {
        NeuralNetwork neuralNetwork = new NeuralNetwork(new int[] { 2,10,10,2});
        JsonCreator JsonCreator;
        List<Datapoint> datapoints;
        Dictionary<string, List<Datapoint>> Powerdatapoints;
        List<Series> powerSeries;
        List<Attribute> Attributes;

        enum Datatype
        {
            Random,
            Wave,
            Bool,
            Csv
        };

        public Form1()
        {
            InitializeComponent();
        }

       
        Datatype datatype;
        private void Form1_Load(object sender, EventArgs e)
        {
            datatype = Datatype.Random;
            comboBox1.Items.Add(Datatype.Random);
            comboBox1.Items.Add(Datatype.Wave);
            comboBox1.Items.Add(Datatype.Bool);
            comboBox1.Items.Add(Datatype.Csv);
            comboBox1.SelectedItem = datatype;
            datapoints = new List<Datapoint>();
            JsonCreator = new JsonCreator();
            Powerdatapoints = new Dictionary<string, List<Datapoint>>();
            powerSeries = new List<Series>();
            Attributes = new List<Attribute>();
            ChartArea CA = chart1.ChartAreas[0];  // quick reference
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;
        }


        public void Generate()
        {
            chart1.Series.Clear();
            string data = "";
            Attributes.Clear();
            switch(datatype)
            {
                case Datatype.Random:
                    data = JsonCreator.getRandomData(trackBar1.Value,trackBar9.Value);
                    Attribute attribute = new Attribute("Randomdata");
                    datapoints = JsonConvert.DeserializeObject<List<Datapoint>>(data);
                    JArray dtapoints = JArray.Parse(data);
                    foreach (JObject item in dtapoints)
                    {
                        attribute.DataSet.datapoints.Add(new Datapoint((int)item.GetValue("timestamp"), (double)item.GetValue("value")));
                    }
                    attribute.displaylimits.Add("changelimit");
                    Attributes.Add(attribute);
                    DrawAttribute(attribute);
                    break;
                case Datatype.Wave:
                    data = JsonCreator.getData(trackBar1.Value, trackBar9.Value, trackBar3.Value, trackBar2.Value, trackBar4.Value, trackBar7.Value, trackBar8.Value);
                    Attribute waveAtr = new Attribute("Wavedata");
                    datapoints = JsonConvert.DeserializeObject<List<Datapoint>>(data);
                    waveAtr.DataSet.datapoints = datapoints;
                    waveAtr.displaylimits.Add("changelimit");
                    Attributes.Add(waveAtr);
                    DrawAttribute(waveAtr);
                    break;
                case Datatype.Bool:
                    data = JsonCreator.getBoolData(trackBar1.Value, trackBar9.Value);
                    Attribute boolAtr = new Attribute("Booldata");
                    datapoints = JsonConvert.DeserializeObject<List<Datapoint>>(data);
                    boolAtr.DataSet.datapoints = datapoints;
                    Attributes.Add(boolAtr);
                    DrawAttribute(boolAtr);
                    break;
                case Datatype.Csv:
                    Dictionary<string, JsonCreator.DataSet> points = JsonCreator.getCsvData();
                    foreach (KeyValuePair<string, JsonCreator.DataSet> keyValuePair in points)
                    {
                        Attribute attributepower = new Attribute(keyValuePair.Key);
                        attributepower.DataSet.datapoints = keyValuePair.Value.datapoints;
                        attributepower.displaylimits.Add("valuelimit");
                        Attributes.Add(attributepower);
                    }
                    break;
            }
            checkedListBox1.Items.Clear();
            for (int i = 0; i < Attributes.Count; i++)
            {
                checkedListBox1.Items.Add(Attributes[i]);
                checkedListBox1.SetItemChecked(0, true);
            }
            DrawAttribute(Attributes[0]);
        }

        private void DrawAttribute(Attribute attribute)
        {
            chart1.Series.Remove(chart1.Series.FindByName(attribute.Name));
            chart1.Series.Add(attribute.Name);
            var chart = chart1.Series[attribute.Name];
            chart.ChartType = SeriesChartType.Line;
            chart.Color = Color.Red;
            chart.MarkerColor = Color.Red;
            chart.MarkerStyle = MarkerStyle.Circle;
            chart.MarkerSize = 4;
            foreach (Datapoint item in attribute.DataSet.datapoints)
            {
                chart.Points.AddXY(item.timestamp, item.value);
            }

            foreach(string l in attribute.displaylimits)
            {
                List<Datapoint> tempPoints = new List<Datapoint>();
                int pointcount;
                switch (l)
                {
                    case "changelimit":
                        
                        pointcount = 50;
                        if (attribute.DataSet.datapoints.Count < pointcount) pointcount = attribute.DataSet.datapoints.Count;
                        for (int x = 0; x < pointcount; x++)
                        {
                            tempPoints.Add(attribute.DataSet.datapoints[x]);
                        }
                        Limit limit = new ChangeLimit("changelimit", (double)trackBar10.Value / 100, tempPoints, pointcount);
                        attribute.AddLimit(limit);
                        chart1.Series.Remove(chart1.Series.FindByName(limit.Name));
                        chart1.Series.Add(limit.Name);
                        var limits = chart1.Series[limit.Name];
                        limits.ChartType = SeriesChartType.Range;
                        limits.Color = Color.FromArgb(100, 100, 255, 200);
                        for (int i = limit.SamplePointcount; i < attribute.DataSet.datapoints.Count(); i++)
                        {
                            limits.Points.Add(new DataPoint() { XValue = attribute.DataSet.datapoints[i].timestamp, YValues = new double[] { limit.MinValue, limit.MaxValue } });
                            limit.AddDatapoint(attribute.DataSet.datapoints[i]);
                        }
                        break;
                    case "valuelimit":
                        tempPoints = new List<Datapoint>();
                        pointcount = 1440;

                        if (attribute.DataSet.datapoints.Count < pointcount) pointcount = attribute.DataSet.datapoints.Count;
                        for (int x = 0; x < pointcount; x++)
                        {
                            tempPoints.Add(attribute.DataSet.datapoints[x]);
                        }
                        Limit valuelimit = new ValueLimit("valuelimit", (double)trackBar10.Value / 100, tempPoints, pointcount);
                        attribute.AddLimit(valuelimit);
                        chart1.Series.Remove(chart1.Series.FindByName(valuelimit.Name));
                        chart1.Series.Add(valuelimit.Name);
                        var s = chart1.Series[valuelimit.Name];
                        s.ChartType = SeriesChartType.Range;
                        s.Color = Color.FromArgb(100, 100, 255, 200);
                        for (int i = valuelimit.SamplePointcount; i < attribute.DataSet.datapoints.Count(); i++)
                        {
                            s.Points.Add(new DataPoint() { XValue = attribute.DataSet.datapoints[i].timestamp, YValues = new double[] { valuelimit.MinValue, valuelimit.MaxValue } });
                            valuelimit.AddDatapoint(attribute.DataSet.datapoints[i]);
                        }
                        break;
                }
            }
            


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            switch (datatype)
            {
                case Datatype.Wave:
                    DataPointnetwork[] dataPointnetwork = new DataPointnetwork[Attributes[0].DataSet.datapoints.Count];
                    for(int i =0; i < Attributes[0].DataSet.datapoints.Count;i++)
                    {
                        dataPointnetwork[i] = new DataPointnetwork(new double[] { Attributes[0].DataSet.datapoints[i].timestamp, Attributes[0].DataSet.datapoints[i].value },0,2);
                    }
                    Stopwatch s = Stopwatch.StartNew();
                    while (s.ElapsedMilliseconds < 1000)
                    {
                        neuralNetwork.Learn(dataPointnetwork, 1);
                    }
                    neuralNetwork.Learn(dataPointnetwork, 1);
                    Attribute att = new Attribute("Validation");
                    for (int i = 0; i < Attributes[0].DataSet.datapoints.Count; i++)
                    {
                        double[] values = neuralNetwork.CalculateOutputs(new double[] { Attributes[0].DataSet.datapoints[i].timestamp, Attributes[0].DataSet.datapoints[i].value});
                        double val;
                        if (values[0] > values[1]) val = values[0];
                        else val = values[1]*-1;
                        att.DataSet.datapoints.Add(new Datapoint(Attributes[0].DataSet.datapoints[i].timestamp, val));
                    }
                    DrawAttribute(att);
                    
                    break;
                case Datatype.Random:
                   
                    break;
                case Datatype.Bool:
                    
                    break;
                case Datatype.Csv:
                    
                    break;
            }

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar1.Value.ToString();
            Generate();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar3.Value.ToString();
            Generate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label6.Text = trackBar2.Value.ToString();
            Generate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar4.Value.ToString();
            Generate();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar5.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var avg = chart1.Series["avg"];
            avg.Points.Clear();
            List<Datapoint> datapoints = DataPredictor.MovingAverage(trackBar5.Value, this.datapoints);
            foreach (Datapoint item in datapoints)
            {
                avg.Points.AddXY(item.timestamp / 3600f, item.value);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            label11.Text = trackBar6.Value.ToString();
            var pre = chart1.Series["predict"];
            pre.Points.Clear();
            int presize = trackBar6.Value;
            List<Datapoint> tempPoints = new List<Datapoint>();
            List<Datapoint> avgpoints = DataPredictor.MovingAverage(5, datapoints);
            for (int i = avgpoints.Count - presize; i < avgpoints.Count; i++)
            {
                tempPoints.Add(avgpoints[i]);
            }
            List<Datapoint> predata = DataPredictor.predictlineare(trackBar6.Value, tempPoints);
            foreach (Datapoint item in predata)
            {
                pre.Points.AddXY(item.timestamp / 3600f, item.value);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            label15.Text = trackBar8.Value.ToString();
            Generate();
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            label13.Text = trackBar7.Value.ToString();
            Generate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            datatype = (Datatype)comboBox1.SelectedItem;
            if(datatype == Datatype.Csv)
            {
                groupBoxchartvariables.Visible = false;
            }
            else
            {
                groupBoxchartvariables.Visible = true;
            }
            
        }

        

        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            label18.Text = trackBar9.Value.ToString();
            Generate();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Generate();
            //chart1.ChartAreas[powerSeries[0].ChartArea].AxisX.ScaleView.Zoom(23414340, 23968980);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            foreach (Attribute attribute in checkedListBox1.CheckedItems)
            {
                DrawAttribute(attribute);
            }

            
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            label20.Text = trackBar10.Value.ToString();
            foreach (Attribute attribute in checkedListBox1.CheckedItems)
            {
                DrawAttribute(attribute);
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
