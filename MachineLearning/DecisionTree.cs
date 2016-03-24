using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearning
{
	class DecisionTree : LearningMachine
	{
		private DataSet Data;
		private DataSet startData;
		private Node rootNode;
		private bool prune = true;

		public DecisionTree(DataSet Data)
		{
			this.Data = Data;
		}

		public override void Initialize()
		{
			Data.Shuffle(Rand);
			startData = new DataSet(Data);
			Data.SplitTestSet(TestSetPercentage);
		}

		public double runNFold(int folds) {
			double accuracySum = 0.0;
			double percentage = 1.0 / folds;

			for (int i = 0; i < folds; i++) {
				Data = new DataSet(startData);
				int numToSplit = (int)(percentage * Data.TargetOutputs.Count);
				if (numToSplit == 0)
					numToSplit = 1;

				Data.TestRecords = Data.Records.GetRange(numToSplit * i, numToSplit);
				Data.Records.RemoveRange(numToSplit * i, numToSplit);
				Data.TestTargets = Data.TargetOutputs.GetRange(numToSplit * i, numToSplit);
				Data.TargetOutputs.RemoveRange(numToSplit * i, numToSplit);

				this.Train();
				double accuracy = this.MeasureAccuracy("test");
				accuracySum += accuracy;
			}

			double result = accuracySum / folds;

			return result;
		}

		public override void Train()
		{
			if (prune)
			{
				Data.SplitValidationSet(ValidationSetPercentage);
			}

			rootNode = new Node(Data);
			
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(rootNode);
			while (queue.Count > 0)
			{
				Node currentNode = queue.Dequeue();
				if (currentNode.canSplit())
				{
					int bestSplit = currentNode.getBestSplit();
					if (bestSplit < 0) continue;
					Dictionary<double, Node> children = currentNode.splitOn(bestSplit);
					foreach (KeyValuePair<double, Node> child in children)
					{
						queue.Enqueue(child.Value);
					}
				}
			}
			
			//Should I want to prune, this is the place
			if (prune)
			{
				double currentAccuracy = this.MeasureAccuracy("Validation");
				bool improved;
				do
				{
					improved = false;
					Stack<Node> nodes = new Stack<Node>(); //I suspect this will find the nodes I want to prune faster doing depth first search, so I use a stack not a queue
					nodes.Push(rootNode);
					while (nodes.Count > 0)
					{
						Node nextNode = nodes.Pop();
						foreach (KeyValuePair<double, Node> child in nextNode.children)
						{
							if (!child.Value.isPruned)
							{
								nodes.Push(child.Value);
							}
						}

						nextNode.isPruned = true;
						double accuracyWithOutNode = this.MeasureAccuracy("Validation");
						if (accuracyWithOutNode >= currentAccuracy)
						{
							currentAccuracy = accuracyWithOutNode;
							improved = true;
							nextNode.children = new Dictionary<double, Node>();
							break;
						}
						else
						{
							nextNode.isPruned = false;
						}
					}
				} while (improved);
			}

			this.LearnerOutputs = "\n";
		}

		public override double Predict(List<double> RecordFeatures)
		{
			if (rootNode.isPruned)
			{
				return rootNode.prediction();
			}

			Node currentNode = rootNode;
			while (currentNode.myFeatureSplit >= 0)
			{
				double featureValue = RecordFeatures[currentNode.myFeatureSplit];
				if (currentNode.children.ContainsKey(featureValue)) {
					Node nextNode = currentNode.children[featureValue];
					if (nextNode.isPruned)
					{
						return currentNode.prediction();
					}
					currentNode = nextNode;
				}
				else
				{
					return currentNode.prediction();
				}
			}
			return currentNode.prediction();
		}

		public override double MeasureAccuracy(string mode)
		{
			int numCorrect = 0, numWrong = 0;

			if (mode.ToLower() == "test")
			{
				for (int i = 0; i < Data.TestRecords.Count; i++)
				{
					double result = this.Predict(Data.TestRecords[i]);
					if (result == Data.TestTargets[i])
					{
						numCorrect++;
					}
					else numWrong++;
				}
			}
			else if (mode.ToLower() == "validation")
			{
				for (int i = 0; i < Data.ValidationRecords.Count; i++)
				{
					double result = this.Predict(Data.ValidationRecords[i]);
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
					double result = this.Predict(Data.Records[i]);
					if (result == Data.TargetOutputs[i])
					{
						numCorrect++;
					}
					else numWrong++;
				}
			}

			LearnerOutputs = "\n";
			Queue<Node> map = new Queue<Node>();
			map.Enqueue(rootNode);
			while (map.Count > 0) {
				Node nextNode = map.Dequeue();
				LearnerOutputs += nextNode.myFeatureSplit + " (";
				int count = 0;
				foreach (KeyValuePair<double, Node> child in nextNode.children)
				{
					map.Enqueue(child.Value);
					count++;
				}
				LearnerOutputs += count + "), ";
			}

			if (numCorrect + numWrong == 0) return 0.0;
			else return ((double)numCorrect / (double)(numCorrect + numWrong));
		}
		
		private class Node
		{
			public Node parent;
			public DataSet Data;
			public Dictionary<double, Node> children;
			public List<int> excludeFeatures;
			public int myFeatureSplit;
			public bool isPruned = false;
			private bool gainRatio = false; // Gain ratio is an alternative splitting criteria to information gain. I implemented this as my experiment in part 6

			public Node(DataSet Data)
			{
				this.Data = new DataSet(Data);
				children = new Dictionary<double, Node>();
				myFeatureSplit = -1;
				excludeFeatures = new List<int>();
			}

			public Node(Node parent, DataSet Data, int featureIndex, double featureValue, List<int> excludeFeatures)
			{
				this.parent = parent;
				this.Data = new DataSet(Data);
				children = new Dictionary<double, Node>();
				this.excludeFeatures = new List<int>(excludeFeatures);
				myFeatureSplit = -1;

				for (int row = this.Data.Records.Count - 1; row >= 0; row--)
				{
					if (this.Data.Records[row][featureIndex] != featureValue)
					{
						this.Data.Records.RemoveAt(row);
						this.Data.TargetOutputs.RemoveAt(row);
					}
				}
				this.excludeFeatures.Add(featureIndex);
			}

			public double getEntropy()
			{
				double result = 0.0;

				//Get the count of how many times each classification occurrs in this node
				Dictionary<double, int> classificationCount = new Dictionary<double, int>();
				foreach (double classification in Data.TargetOutputs)
				{
					if (!classificationCount.ContainsKey(classification))
					{
						classificationCount.Add(classification, 0);
					}
					classificationCount[classification]++;
				}

				//Add each division's entropy
				foreach (KeyValuePair<double, int> division in classificationCount)
				{
					double probability = ((double)division.Value / (double)this.getSize());
					result += (probability * Math.Log(probability, 2));
				}

				result *= -1;
				return result;
			}

			public int getSize()
			{
				return Data.Records.Count;
			}

			/// <summary>
			/// Get the index of the best feature to split this node upon.
			/// </summary>
			/// <returns>-1 if no valid split exists</returns>
			public int getBestSplit()
			{
				if (!this.canSplit())
				{
					return -1;
				}

				//Try all features, ignoring the already used features
				Dictionary<int, double> entropies = new Dictionary<int, double>();
				for (int index = 0; Data.Records.Count > 0 && index < Data.Records[0].Count; index++)
				{
					if (this.excludeFeatures.Contains(index))
					{
						continue;
					}

					//Record the entropy of the split across all potential children.
					double entropySum = 0.0;
					double splitEntropy = 0.0;
					Dictionary<double, Node> potentialChildren = this.splitOn(index);
					foreach(KeyValuePair<double, Node> potentialChild in potentialChildren)
					{
						Node child = potentialChild.Value;
						if (child != null)
						{
							double theirSize = child.getSize();
							double mySize = this.getSize();
							double theirEntropy = child.getEntropy();
							entropySum += (theirSize / mySize) * theirEntropy;
							if (gainRatio)
							{
								splitEntropy += (theirSize / mySize) * Math.Log((theirSize / mySize), 2);
							}
						}
					}
					if (gainRatio)
					{
						entropies.Add(index, entropySum / -splitEntropy);
					}
					else {
						entropies.Add(index, entropySum);
					}
				}

				//Remove the children, as they might not be correct from potential children calculations
				this.children = new Dictionary<double, Node>();

				//Find the lowest entropy value, and return it's corresponding index
				double lowest = double.MaxValue;
				int lowestIndex = -1;
				foreach(KeyValuePair<int, double> entropy in entropies)
				{
					if (entropy.Value < lowest)
					{
						lowest = entropy.Value;
						lowestIndex = entropy.Key;
					}
				}
				return lowestIndex;
			}

			public bool canSplit()
			{
				if (Data.Records.Count == 0) return false;

				HashSet<double> classifications = new HashSet<double>(Data.TargetOutputs);
				bool somethingToSplit = classifications.Count > 1;
				bool featuresRemain = excludeFeatures.Count < Data.Records[0].Count;
				bool hasNotAlreadySplit = myFeatureSplit < 0;

				return somethingToSplit && featuresRemain && hasNotAlreadySplit;
			}

			public Dictionary<double, Node> splitOn(int featureIndex)
			{
				myFeatureSplit = featureIndex;
				children = new Dictionary<double, Node>();

				HashSet<double> seenValues = new HashSet<double>();
				foreach (List<double> row in Data.Records)
				{
					if (!seenValues.Contains(row[featureIndex]))
					{
						seenValues.Add(row[featureIndex]);
					}
				}

				foreach (double seen in seenValues)
				{
					Node child = new Node(this, Data, featureIndex, seen, new List<int>(excludeFeatures));

					//Only add children that are not empty
					if (child.getSize() > 0)
					{
						children.Add(seen, child);
					}
				}

				return children;
			}

			public double prediction()
			{
				Dictionary<double, int> classificationCount = new Dictionary<double, int>();
				foreach (double classification in Data.TargetOutputs)
				{
					if (!classificationCount.ContainsKey(classification))
						classificationCount.Add(classification, 0);
					classificationCount[classification]++;
				}

				double prediction = 0.0;
				int min = int.MaxValue;
				foreach (KeyValuePair<double, int> count in classificationCount)
				{
					if (count.Value < min)
					{
						min = count.Value;
						prediction = count.Key;
					}
				}

				return prediction;
			}
		}
	}
}
