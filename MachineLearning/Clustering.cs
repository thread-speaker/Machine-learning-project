using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning
{
	internal class Clustering : LearningMachine
	{
		private DataSet Data;
		private int k;
		private List<Centroid> centroids;

		public Clustering(DataSet Data, int k)
		{
			this.Data = Data;
			this.k = k;
		}

		public override void Initialize()
		{
			if (k == 0)
				k = 4;

			centroids = new List<Centroid>();

			for (int i = 0; i < k; i++)
			{
				centroids.Add(new Centroid(Data.Records[i], Data));
			}
		}

		public override double MeasureAccuracy(string mode)
		{
			return 1;
		}

		public override double Predict(List<double> RecordFeatures)
		{
			return 1;
		}

		public override void Train()
		{
			LearnerOutputs = "\n";
			TotalEpochs = 0;
			double sse = 0, sseAfter = 0; //Sum squared error
			bool moved = false;

			do
            {
                LearnerOutputs += "\n";
                LearnerOutputs += "-----------------------------------\n";
                LearnerOutputs += "Iteration " + (++TotalEpochs) + "\n";
                LearnerOutputs += "-----------------------------------\n";
                LearnerOutputs += "Computing Centroids:\n";

                int indx = 0;
				foreach (Centroid centroid in centroids)
				{
					LearnerOutputs += "Centroid " + (indx++) + ": " + centroid.ToString() + "\n";
				}

                //assign all points to a median
                LearnerOutputs += "Making Assignments\n\t";
                int recordindx = 0;
				foreach (List<double> record in Data.Records)
				{
					int cisf = -1; //closest index so far
					double cdsf = double.MaxValue; //closest distance so far
					//find the closest median
					for (int i = 0; i < centroids.Count; i++)
					{
						if (centroids[i].distSquaredTo(record) < cdsf)
						{
							cdsf = centroids[i].distSquaredTo(record);
							cisf = i;
						}
					}
					//give the closest record "ownership" of the record
					centroids[cisf].addRecord(record);
                    LearnerOutputs += recordindx + ":" + cisf + ", ";
                    if (recordindx % 10 == 9)
                        LearnerOutputs += "\n\t";
                    recordindx++;
                }
                LearnerOutputs += "\n";

                //calculate mean squared error
                //move all centroids to the new centroid location
                //if none of them moved, then the system settled, and is done training
                sse = 0.0;
                sseAfter = 0.0;
                moved = false;
				foreach (Centroid centroid in centroids)
				{
                    sse += centroid.getSquaredError();
                    centroid.nudge();
                    sseAfter += centroid.getSquaredError();
                    if (centroid.hasChanged() && sse != sseAfter)
						moved = true;
                    centroid.clearRecords();
				}
                
				LearnerOutputs += "SSE: " + sse + "\n";
			} while (moved);
            LearnerOutputs += "Centroids have converged\n";
        }

		public class Centroid
		{
			public List<double> prevCoords { get; set; }
			public List<double> coords { get; set; }
			public List<List<double>> ownedRecords { get; set; }
			private DataSet Data;

			public Centroid(List<double> coords, DataSet Data)
			{
				this.prevCoords = coords;
				this.coords = coords;
				this.Data = Data;
				this.ownedRecords = new List<List<double>>();

                for (int column = 0; column < coords.Count; column++)
                {
                    if (!Data.NominalFeatures.Contains(column))
                    {
                        coords[column] = Math.Round(coords[column], 3);
                    }
                }
			}

			public void clearRecords()
			{
				ownedRecords = new List<List<double>>();
			}

			public void addRecord(List<double> record)
			{
				ownedRecords.Add(record);
			}

			public double getSquaredError()
			{
				double result = 0;
				foreach (List<double> record in ownedRecords)
				{
					result += distSquaredTo(record);
				}
				return result;
			}

            public void nudge()
			{
                prevCoords = new List<double>(coords);
				List<double> meanCoords = new List<double>();

				//Calculate mean location
				for (int column = 0; column < coords.Count; column++)
				{
					meanCoords.Add(0);
					if (Data.NominalFeatures.Contains(column))
					{
						//set this nominal feature to the mode, not counting unknowns.
						//if everything is unknown, then set it to unknown
						List<double> values = new List<double>();
						foreach (List<double> row in ownedRecords)
						{
							if (row[column] != double.MaxValue)
								values.Add(Math.Round(row[column],1));
						}

						if (values.Count > 0)
						{
							double mode = values.GroupBy(v => v).OrderByDescending(g => g.Count()).First().Key;
							meanCoords[column] = mode;
						}
						else meanCoords[column] = double.MaxValue;
					}
					else
					{
						//Don't count unknowns in the mean for this column. (if all unknowns, then set it to unknown)
						int knownValues = 0;
						foreach (List<double> row in ownedRecords)
						{
							if (row[column] != double.MaxValue)
							{
								meanCoords[column] += row[column];
								knownValues++;
							}
						}

                        if (knownValues == 0) //If all values were unknown, set it to unknown
                            meanCoords[column] = double.MaxValue;
                        else { //Otherwise, divide by the count for the average (mean)
                            meanCoords[column] /= knownValues;
                            meanCoords[column] = Math.Round(meanCoords[column], 3);
                        }
					}
				}

				//Now meanCoords should be calculated. move to that new mean location
				this.coords = meanCoords;
			}

			public double distSquaredTo(List<double> features)
			{
				double distSquared = 0;

				for (int column = 0; column < features.Count; column++)
				{
					//If either is unknown
					if (features[column] == double.MaxValue || coords[column] == double.MaxValue)
					{
						distSquared += 1;
					}
					//else if nominal
					else if (Data.NominalFeatures.Contains(column))
					{
                        if (Math.Round(features[column], 1) == Math.Round(coords[column], 1))
							distSquared += 0;
						else
							distSquared += 1;
					}
					//else continuous
					else
					{
                        distSquared += Math.Pow(Math.Round(features[column], 3) - Math.Round(coords[column], 3), 2);
					}
				}

				return distSquared;
			}

			public bool hasChanged()
			{
				bool result = false;
				for (int column = 0; column < coords.Count; column++)
				{
					if (prevCoords[column] != coords[column])
					{
						result = true;
						break;
					}
				}
				return result;
			}

			public override string ToString()
			{
				string result = "";
				for (int i = 0; i < coords.Count; i++)
				{
                    double value = coords[i];

                    if (value != double.MaxValue)
					{
                        if (Data.NominalFeatures.Contains(i))
                        {
                            bool found = false;
                            foreach (KeyValuePair<string,double> kv in Data.NominalValueMap)
                            {
                                if (Math.Round(kv.Value, 1) == Math.Round(value, 1))
                                {
                                    found = true;
                                    result += kv.Key + ", ";
                                    break;
                                }
                            }
                            if (!found)
                            {
                                result += "COULDN'T PLACE " + value + ", ";
                            }
                        }
                        else
						    result += value + ", ";
					}
					else result += "?, ";
				}
				return result;
			}
		}
	}
}