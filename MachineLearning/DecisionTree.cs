using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearning
{
	class DecisionTree : LearningMachine
	{
		private DataSet Data;
		private Node rootNode;

		public DecisionTree(DataSet Data)
		{
			this.Data = Data;
		}

		public override void Initialize()
		{
			Data.Shuffle(Rand);
			Data.SplitTestSet(TestSetPercentage);
		}

		public override void Train()
		{
			rootNode = new Node(Data);

			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(rootNode);
			while (queue.Count > 0)
			{
				Node currentNode = queue.Dequeue();
				if (currentNode.canSplit())
				{
					List<double> infos = currentNode.getFeatureSplitInfos();
					double min = infos.Min();
					List<Node> children = currentNode.splitOn(infos.IndexOf(min));
					foreach (Node child in children)
					{
						queue.Enqueue(child);
					}
				}
			}

			//if the code worked correctly, then I should have a full tree at this point and a variable pointing to the root node
			//Should I want to prune, this is the place
		}

		public override double Predict(List<double> RecordFeatures)
		{
			throw new NotImplementedException();
		}

		public override double MeasureAccuracy(string mode)
		{
			throw new NotImplementedException();
		}

		#region NodeClass
		private class Node
		{
			public DataSet Data;
			public List<int> excludeFeatures;
			public List<Node> children;

			public Node(DataSet Data)
			{
				this.Data = new DataSet(Data);
				excludeFeatures = new List<int>();
				children = new List<Node>();
			}

			public Node(DataSet Data, int featureIndex, double featureValue, List<int> excludeFeatures)
			{
				this.Data = new DataSet(Data);
				excludeFeatures = new List<int>(excludeFeatures);
				children = new List<Node>();

				for (int row = Data.Records.Count - 1; row >= 0; row--)
				{
					if (Data.Records[row][featureIndex] != featureValue)
					{
						Data.Records.RemoveAt(row);
					}
				}
				excludeFeatures.Add(featureIndex);
			}

			public double getEntropy()
			{
				return 0;
			}

			public int getSize()
			{
				return Data.Records.Count;
			}

			public int getBestSplit()
			{
				return 0;
			}

			public bool canSplit()
			{
				Data.TargetOutputs.Distinct();
				return (
					Data.Records.Count > 0
					&& excludeFeatures.Count < Data.Records[0].Count
					&& Data.TargetOutputs.Distinct().ToList().Count > 1
				);
			}

			public List<Node> splitOn(int featureIndex)
			{
				List<double> seenValues = new List<double>();
				foreach (List<double> row in Data.Records)
				{
					if (seenValues.Contains(row[featureIndex]))
					{
						continue;
					}
					else
					{
						seenValues.Add(row[featureIndex]);
						children.Add(new Node(Data, featureIndex, row[featureIndex], excludeFeatures));
					}
				}

				return children;
			}

			public List<double> getFeatureSplitInfos()
			{
				List<double> infos = new List<double>();

				for (int column = 0; column < Data.Records[0].Count; column++)
				{
					if (excludeFeatures.Contains(column))
					{
						infos.Add(Double.MaxValue);
						continue;
					}

					Dictionary<double, int> inputCount = new Dictionary<double, int>();
					Dictionary<double, List<int>> outputCount = new Dictionary<double, List<int>>();
					for (int row = 0; row < Data.Records.Count; row++)
					{
						if (!inputCount.ContainsKey(Data.Records[row][column]))
						{
							inputCount.Add(Data.Records[row][column], 0);
							outputCount.Add(Data.Records[row][column], new List<int>());
							for (int k = 0; k < Data.NumClassifications; k++)
							{
								outputCount[Data.Records[row][column]].Add(0);
							}
						}
						inputCount[Data.Records[row][column]]++;
						outputCount[Data.Records[row][column]][(int)Data.TargetOutputs[row]]++;
					}

					double featureInfo = 0.0;
					foreach (KeyValuePair<double, int> entry in inputCount)
					{
						if (entry.Value > 0)
						{
							double sRatio = ((double)entry.Value / (double)Data.Records.Count);
							double info = 0.0;
							for (int output = 1; output <= Data.NumClassifications; output++)
							{
								int count = outputCount[entry.Key][output];
								if (count > 0)
								{
									double probability = ((double)count / (double)entry.Value);
									info += (probability * Math.Log(probability, 2));
								}
							}
							featureInfo += -sRatio * info;
						}
					}

					infos.Add(featureInfo);
				}

				return infos;
			}
		}
		#endregion
	}
}
