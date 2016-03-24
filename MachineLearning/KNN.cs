using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning
{
    internal class KNN : LearningMachine
    {
        private DataSet Data;
        private int k;
        private List<Point> Points;
        private bool Normalize = true;
        private bool DistanceWeighted = false;
        private int Reduction = 1;
        private Dictionary<int, double> Normalizations;

        public KNN(DataSet Data, int k)
        {
            this.Data = Data;
            this.k = k;
            Points = new List<Point>();
            Normalizations = new Dictionary<int, double>();
            if (Reduction < 1)
                Reduction = 1;
        }

        public override void Initialize()
        {
            //Set K if not already set
            if (k <= 0)
            {
                k = Data.Records[0].Count * 2;
            }

            //Split test data out if not already set
            if (Data.TestTargets.Count == 0)
            {
                Data.Shuffle(Rand);
                Data.SplitTestSet(TestSetPercentage);
            }
        }

        public override double MeasureAccuracy(string mode)
        {
            LearnerOutputs = "\n";
            int numCorrect = 0, numWrong = 0;
            double result = 0.0;
            double MSE = 0.0;

            if (mode.ToLower() == "test")
            {
                for (int i = 0; i < Data.TestRecords.Count; i++)
                {
                    result = this.Predict(Data.TestRecords[i]);

                    if (!Data.OutputNominal)
                    {
                        MSE += Math.Pow(result - Data.TestTargets[i], 2);
                    }

                    if (result == Data.TestTargets[i])
                    {
                        numCorrect++;
                    }
                    else numWrong++;
                }
                MSE /= Data.TestRecords.Count;
                LearnerOutputs += "MSE: " + MSE;
            }
            else if (mode.ToLower() == "validation")
            {
                for (int i = 0; i < Data.ValidationRecords.Count; i++)
                {
                    result = this.Predict(Data.ValidationRecords[i]);
                    if (result == Data.ValidationTargets[i])
                    {
                        numCorrect++;
                    }
                    else numWrong++;
                }
            }
            else if (mode.ToLower() == "training")
            {
                for (int i = 0; i < Data.Records.Count; i++)
                {
                    result = this.Predict(Data.Records[i]);
                    if (result == Data.TargetOutputs[i])
                    {
                        numCorrect++;
                    }
                    else numWrong++;
                }
            }

            if (numCorrect + numWrong == 0) return 0.0;
            else return ((double)numCorrect / (double)(numCorrect + numWrong));
        }

        public override double Predict(List<double> RecordFeatures)
        {
            if (Normalize)
            {
                foreach (KeyValuePair<int, double> kv in Normalizations)
                {
                    RecordFeatures[kv.Key] /= kv.Value;
                }
            }

            List<KeyValuePair<double, Point>> nearest = new List<KeyValuePair<double, Point>>();
            for (int i = 0; i < Points.Count; i++)
            {
                Point p = Points[i];
                double distSquared = p.distanceSquaredTo(RecordFeatures);
                if (nearest.Count < k)
                    nearest.Add(new KeyValuePair<double, Point>(distSquared, p));
                else
                {
                    for (int j = 0; j < k; j++)
                    {
                        if (nearest[j].Key > distSquared)
                        {
                            nearest.RemoveAt(j);
                            nearest.Add(new KeyValuePair<double, Point>(distSquared, p));
                            break;
                        }
                    }
                }
            }

            if (Data.OutputNominal)
            {
                if (DistanceWeighted)
                {
                    Dictionary<double, double> outputs = new Dictionary<double, double>();
                    foreach (KeyValuePair<double, Point> kv in nearest)
                    {
                        if (!outputs.ContainsKey(kv.Value.Target))
                        {
                            outputs.Add(kv.Value.Target, 0);
                        }
                        outputs[kv.Value.Target] += 1/(kv.Key); //key is distance squared
                    }

                    double largest = 0;
                    double result = 0;
                    foreach (KeyValuePair<double, double> kv in outputs)
                    {
                        if (kv.Value > largest)
                        {
                            largest = kv.Value;
                            result = kv.Key;
                        }
                    }
                    return result;
                }
                else
                {
                    //I got the LINQ for getting the mode from http://stackoverflow.com/questions/19467492/how-do-i-find-the-mode-of-a-listdouble
                    List<double> outputs = new List<double>();
                    foreach (KeyValuePair<double, Point> kv in nearest)
                        outputs.Add(kv.Value.Target);
                    double modeValue = outputs.GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => x.Key).FirstOrDefault();
                    return modeValue;
                }
            }
            else
            {
                double result = 0.0;
                foreach (KeyValuePair<double, Point> kv in nearest)
                    result += kv.Value.Target;
                result /= k;
                return result;
            }
        }

        public override void Train()
        {
            for (int i = 0; i < Data.Records.Count; i++)
            {
                if (i % Reduction == 0)
                {
                    Points.Add(new Point(Data.Records[i], Data.TargetOutputs[i], Data.NominalFeatures));
                }
            }

            if (Normalize)
            {
                for (int i = 0; i < Data.Records[0].Count; i++)
                {
                    if (Data.NominalFeatures.Contains(i))
                        continue;

                    double highest = 0, lowest = 0;

                    foreach (Point p in Points)
                    {
                        double value = p.Record[i];
                        if (value > highest)
                            highest = value;
                        else if (value < lowest)
                            lowest = value;
                    }

                    lowest = Math.Abs(lowest);
                    if (lowest > highest)
                        highest = lowest; //Get the number with the greatest magnitude

                    if (highest > 0)
                    {
                        Normalizations.Add(i, highest);
                        foreach (Point p in Points)
                        {
                            p.Normalize(i, highest);
                        }
                    }
                }
            }
        }

        private class Point
        {
            public List<double> Record;
            public double Target;
            public List<int> NominalFeatures;

            public Point(List<double> Record, double Target, List<int> NominalFeatures)
            {
                this.Record = new List<double>(Record);
                this.Target = Target;
                this.NominalFeatures = NominalFeatures;
            }

            public double distanceSquaredTo(Point point)
            {
                return distanceSquaredTo(point.Record);
            }

            public double distanceSquaredTo(List<double> RecordFeatures)
            {
                double result = 0.0;
                for (int i = 0; i < Record.Count; i++)
                {
                    if (RecordFeatures[i] == Double.MaxValue || Record[i] == Double.MaxValue)
                    {
                        //Handle unknowns (either the for the point, or the record features)
                        result += 1;
                    }
                    else if (!NominalFeatures.Contains(i))
                    {
                        //continuous feature
                        result += Math.Pow(RecordFeatures[i] - this.Record[i], 2);
                    }
                    else
                    {
                        //nominal feature
                        if (RecordFeatures[i] != Record[i])
                            result += 1;
                    }
                }

                return result;
            }

            internal void Normalize(int featureIndex, double factor)
            {
                Record[featureIndex] /= factor;
            }
        }
    }
}