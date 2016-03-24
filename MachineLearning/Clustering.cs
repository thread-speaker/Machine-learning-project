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
			double sse = 0; //Sum squared error
            bool moved = false;

            do
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(TotalEpochs + 1);
                Console.WriteLine("---------------------------------------------------");
                LearnerOutputs += "\n";

				int indx = 0;
				foreach (Centroid centroid in centroids)
				{
					LearnerOutputs += "Centroid " + (++indx) + ": " + centroid.ToString() + "\n";
				}

				//assign all points to a median
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
				}
				//calculate mean squared error
				sse = 0.0;
				foreach (Centroid centroid in centroids)
				{
					sse += centroid.getSquaredError();
				}

                //move all centroids to the new centroid location
                //if none of them moved, then the system settled, and is done training
                moved = false;
				foreach (Centroid centroid in centroids)
                {
                    Console.WriteLine(centroid);
                    centroid.nudge();
                    if (centroid.hasChanged())
                        moved = true;
				}

				TotalEpochs++;
				LearnerOutputs += "SSE: " + sse;
                Console.WriteLine(sse);
                Console.WriteLine();
            } while (moved);
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
								values.Add(row[column]);
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
						else //Otherwise, divide by the count for the average (mean)
							meanCoords[column] /= knownValues;
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
					if (features[column] == double.MaxValue || coords[column] == double.MaxValue)
					{
						distSquared += 1;
					}
					else if (Data.NominalFeatures.Contains(column))
					{
						if (features[column] == coords[column])
							distSquared += 0;
						else
							distSquared += 1;
					}
					else
					{
						distSquared += Math.Pow(features[column] - coords[column], 2);
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
				foreach (double value in coords)
				{
					if (value != double.MaxValue)
					{
						result += value + ", ";
					}
					else result += "?, ";
				}
				return result;
			}
		}
	}
}