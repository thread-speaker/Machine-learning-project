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
        private bool prune = false;

		public DecisionTree(DataSet Data)
		{
			this.Data = Data;
		}

		public override void Initialize()
		{
			Data.Shuffle(Rand);
			Data.SplitTestSet(TestSetPercentage);
            if (prune)
            {
                Data.SplitValidationSet(ValidationSetPercentage);
            }
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
                    int bestSplit = currentNode.getBestSplit();
                    if (bestSplit < 0) continue;
                    Dictionary<double, Node> children = currentNode.splitOn(bestSplit);
					foreach (KeyValuePair<double, Node> child in children)
					{
						queue.Enqueue(child.Value);
					}
				}
			}

            //if the code worked correctly, then I should have a full tree at this point and a variable pointing to the root node
            //Should I want to prune, this is the place

            this.LearnerOutputs = "\n";
        }

		public override double Predict(List<double> RecordFeatures)
		{
            Node currentNode = rootNode;
            while (currentNode.myFeatureSplit >= 0)
            {
                double featureValue = RecordFeatures[currentNode.myFeatureSplit];
                if (currentNode.children.ContainsKey(featureValue)) {
                    currentNode = currentNode.children[featureValue];
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
            else if (mode.ToLower() == "training")
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

            LearnerOutputs = "\n# correct = " + numCorrect + ", # wrong = " + numWrong;
            if (numCorrect + numWrong == 0) return 0.0;
            else return ((double)numCorrect / (double)(numCorrect + numWrong));
		}
        
		private class Node
		{
			public DataSet Data;
			public Dictionary<double, Node> children;
            public List<int> excludeFeatures;
            public int myFeatureSplit;

            public Node(DataSet Data)
			{
				this.Data = new DataSet(Data);
				children = new Dictionary<double, Node>();
                myFeatureSplit = -1;
				excludeFeatures = new List<int>();
			}

			public Node(DataSet Data, int featureIndex, double featureValue, List<int> excludeFeatures)
			{
				this.Data = new DataSet(Data);
				children = new Dictionary<double, Node>();
				excludeFeatures = new List<int>(excludeFeatures);
                myFeatureSplit = -1;

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
                    double splitEntropy = 0.0;
                    Dictionary<double, Node> potentialChildren = this.splitOn(index);
                    foreach(KeyValuePair<double, Node> potentialChild in potentialChildren)
                    {
                        Node child = potentialChild.Value;
                        if (child != null)
                        {
                            splitEntropy += ((double)child.getSize() / (double)this.getSize()) * child.getEntropy();
                        }
                    }
                    entropies.Add(index, splitEntropy);
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
                    Node child = new Node(Data, featureIndex, seen, new List<int>(excludeFeatures));

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
