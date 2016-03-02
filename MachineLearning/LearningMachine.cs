//@Author: Curtis Merrell
//@Version: 1.0
//@DateLastUpdated: February 22, 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{

	abstract class LearningMachine
	{
		#region Generic Learner Properties
		public double LearningRate { get; set; }
		public double TestSetPercentage { get; set; }
		public double ValidationSetPercentage { get; set; }
		public int TotalEpochs { get; set; }
		public string LearnerOutputs { get; set; }
		public Random Rand { get; set; }
		#endregion

		#region Neural Net Specific Properties
		public int NumHiddenNodes { get; set; }
		public double Momentum { get; set; }

		#endregion

		#region Generic Methods
		abstract public void Initialize();

		abstract public void Train();

		abstract public double Predict(List<double> RecordFeatures);

		abstract public double MeasureAccuracy(string mode);
		#endregion
	}
}
