using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace datacreator
{
    internal static class DataPredictor
    {
        public static List<Datapoint> predictlineare(int nrOfPredictions, List<Datapoint> dataPoints )
        {
            List<Datapoint> predictions = new List<Datapoint>();
            double avgIncreasePerSecond = 0;
            double avgTimeIncrease = 0;
            //dont need a for loop can just use oldest value and the newest
            for(int i = 0; i < dataPoints.Count -1; i++)
            {
                int timeincrease = dataPoints[i + 1].timestamp - dataPoints[i].timestamp;
                avgTimeIncrease += timeincrease;
                avgIncreasePerSecond += (dataPoints[i + 1].value - dataPoints[i].value) / timeincrease;

            }
            avgTimeIncrease /= (dataPoints.Count -1);
            avgIncreasePerSecond /= (dataPoints.Count -1);
            predictions.Add(dataPoints[0]);
            for (int i = -1; i < nrOfPredictions; i++)
            {
                predictions.Add(new Datapoint((int)(dataPoints[dataPoints.Count-1].timestamp + avgTimeIncrease * (i +1)), dataPoints[dataPoints.Count - 1].value + avgIncreasePerSecond * (i + 1) * avgTimeIncrease));
            }

            return predictions;
        }

        public static List<Datapoint> MovingAverage(int nrOfDatapoints, List<Datapoint> dataPoints)
        {
            List<Datapoint> avgpoints = new List<Datapoint>();
            for (int i = nrOfDatapoints -1; i < dataPoints.Count; i++)
            {
                double avg = 0;
                for(int x =0; x < nrOfDatapoints; x++)
                {
                    avg += dataPoints[i - x].value;
                }
                avgpoints.Add(new Datapoint(dataPoints[i- nrOfDatapoints /2].timestamp, avg / (double)nrOfDatapoints));
            }

            return avgpoints;
        }

        public static double[] Limmits(List<Datapoint> datapoints, double errormargine)
        {
            double maxIncrease = 0;
            double maxDecrease = 0;
            for(int i = 1; i < datapoints.Count; i++)
            {
                double diffrence = datapoints[i].value - datapoints[i - 1].value;
                if (diffrence > maxIncrease)
                {
                    maxIncrease = diffrence;
                }
                else if(diffrence < maxDecrease)
                {
                    maxDecrease = diffrence;
                }
            }
            double value = datapoints[datapoints.Count - 1].value;
            return new double[] {value + maxDecrease * errormargine, value + maxIncrease * errormargine };

        }

        public static double[] LimmitsTotalValue(List<Datapoint> datapoints, double errormargine)
        {
            double maxValue = datapoints[0].value;
            double MinValue = datapoints[0].value;
            for (int i = 1; i < datapoints.Count; i++)
            {
                double value = datapoints[i].value;
                if (value > maxValue)
                {
                    maxValue = value;
                }
                else if (value < MinValue)
                {
                    MinValue = value;
                }
            }
            double dif = maxValue - MinValue;
            return new double[] { MinValue -  dif * errormargine, maxValue + dif * errormargine };

        }
    }
}
