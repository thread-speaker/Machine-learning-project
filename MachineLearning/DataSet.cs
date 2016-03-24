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
	class DataSet
	{
		#region Properties
		//IsValid Data File
		public bool IsValid { get; set; }

		//Training set
		public List<List<double>> Records { get; set; }
		public List<double> TargetOutputs { get; set; }

		//Validation set
		public List<List<double>> ValidationRecords { get; set; }
		public List<double> ValidationTargets { get; set; }

		//Test set
		public List<List<double>> TestRecords { get; set; }
		public List<double> TestTargets { get; set; }

		public List<int> NominalFeatures { get; set; }
		public bool OutputNominal { get; set; }
		public bool hasClass = false;

		//Number of possible output classifications (Given nominal target outputs)
		public int? NumClassifications
		{
			get
			{
				if (NominalOutputMap.Count > 0)
					return NominalOutputMap.Count;
				else
					return null;
			}
		}

		//On creation of data, map nominal features/outputs to double values. 
		//Outputs will be converted to "integers" (1.0, 2.0, etc...)
		private Dictionary<string, double> NominalValueMap = new Dictionary<string, double>();
		private Dictionary<string, double> NominalOutputMap = new Dictionary<string, double>();
		#endregion

		#region Constructor
		/// <summary>
		/// Given a file path string, parses the file as an arff and converts the data provided into a 
		/// 2D List of doubles (Features) and another List of doubles (TargetOutputs)
		/// </summary>
		/// <param name="FileLocation"></param>
		public DataSet(string FileLocation)
		{
			IsValid = true;

			Records = new List<List<double>>();
			TargetOutputs = new List<double>();

			TestRecords = new List<List<double>>();
			TestTargets = new List<double>();

			ValidationRecords = new List<List<double>>();
			ValidationTargets = new List<double>();

			NominalFeatures = new List<int>();
			OutputNominal = false;

			try {
				List<string> data = System.IO.File.ReadAllLines(FileLocation).ToList();
				//Remove everything before the first attribute
				for (int i = 0; i < data.Count; i++)
				{
					if (data[i].ToLower().Contains("@attribute"))
					{
						data.RemoveRange(0, i);
						break;
					}
				}
				
				//Process attributes, then remove them when @data is found
				for (int i = 0; i < data.Count; i++)
				{
					String line = data[i];
					if (line.ToLower().Contains("@data"))
					{
						data.RemoveRange(0, i + 1);
						break;
					}
					if (!line.Contains('{'))
						continue;

					if (line.ToLower().Contains("class"))
					{
						hasClass = true;
						OutputNominal = true;
						//Get to the first nominal value
						string attribute = line.Substring(line.IndexOf('{') + 1);
						//Remove any spaces and the trailing bracket
						attribute = attribute.Replace(" ", "");
						attribute = attribute.Replace("}", "");
						//Split the values on commas
						List<string> values = attribute.Split(',','\t').ToList();
						foreach (string s in values)
						{
							NominalOutputMap.Add(s, NominalOutputMap.Count + 1.0);
						}
					}
					else
					{
						NominalFeatures.Add(i);
						//Get to the first nominal value
						string attribute = line.Substring(line.IndexOf('{') + 1);
						//Remove any spaces and the trailing bracket
						attribute = attribute.Replace(" ", "");
						attribute = attribute.Replace("}", "");
						//Split the values on commas
						List<string> values = attribute.Split(',','\t').ToList();
						foreach (string s in values)
						{
							if (!NominalValueMap.ContainsKey(s)) //Nominal inputs mapped to fractions of 10 (.1, .2, .3, etc...)
								NominalValueMap.Add(s, (NominalValueMap.Count / 10.0) + .1);
						}
					}
				}

				foreach (string line in data)
				{
					if (line.Trim(' ', '\t') == "" || line[0] == ('%'))
						continue;
					List<string> record = line.Split(',','\t').ToList();
					Records.Add(parseFeatures(record));
					TargetOutputs.Add(parseTargetOutput(record));
				}
			}
			catch (Exception e)
			{
				this.IsValid = false;
				return;
			}
		}

		public DataSet(DataSet Data)
		{
			IsValid = (Data.IsValid == true);

			Records = new List<List<double>>(Data.Records);
			TargetOutputs = new List<double>(Data.TargetOutputs);

			TestRecords = new List<List<double>>(Data.TestRecords);
			TestTargets = new List<double>(Data.TestTargets);

			ValidationRecords = new List<List<double>>(Data.ValidationRecords);
			ValidationTargets = new List<double>(Data.ValidationTargets);

			NominalValueMap = new Dictionary<string, double>(Data.NominalValueMap);
			NominalOutputMap = new Dictionary<string, double>(Data.NominalOutputMap);

			NominalFeatures = new List<int>(Data.NominalFeatures);
			OutputNominal = Data.OutputNominal;
		}
		#endregion

		#region Manipulate Training/Test/Validation sets
		/// <summary>
		/// Shuffles both feature records and targetOutputs on a randomly generated seed
		/// </summary>
		public void Shuffle()
		{
			Random rand = new Random();
			this.Shuffle(rand);
		}

		/// <summary>
		/// Shuffles both Feature records and targetOutputs on the same provided random seed
		/// </summary>
		/// <param name="rand"></param>
		public void Shuffle(Random rand)
		{
			int n = Records.Count;
			while (n > 1)
			{
				n--;
				int k = rand.Next(n + 1);
				//Shuffle features
				var value1 = Records[k];
				Records[k] = Records[n];
				Records[n] = value1;

				//Shuffle outputs on same k value
				var value2 = TargetOutputs[k];
				TargetOutputs[k] = TargetOutputs[n];
				TargetOutputs[n] = value2;
			}
		}

		/// <summary>
		/// Removes a percentage of the training set for use as a reserved test set AFTER TRAINING
		/// </summary>
		/// <param name="percentage">Double between .05 ~ .5</param>
		public void SplitTestSet(double percentage)
		{
			int numToSplit = (int)(percentage * TargetOutputs.Count);
			if (numToSplit == 0)
				numToSplit = 1;

			TestRecords = Records.GetRange(Records.Count - numToSplit, numToSplit);
			Records.RemoveRange(Records.Count - numToSplit, numToSplit);
			TestTargets = TargetOutputs.GetRange(TargetOutputs.Count - numToSplit, numToSplit);
			TargetOutputs.RemoveRange(TargetOutputs.Count - numToSplit, numToSplit);
		}

		/// <summary>
		/// Puts the test set back into the training set if you want to shuffle them all together
		/// </summary>
		public void ConcatenateTestSet()
		{
			Records.AddRange(TestRecords);
			TestRecords.Clear();
			TargetOutputs.AddRange(TestTargets);
			TestTargets.Clear();
		}

		/// <summary>
		/// Removes a percentage of the training set for use as a reserved set for testing accuracy AFTER EACH TRAINING EPOCH
		/// </summary>
		/// <param name="percentage">Double between .05 ~ .5</param>
		public void SplitValidationSet(double percentage)
		{
			int numToSplit = (int)(percentage * TargetOutputs.Count);
			if (numToSplit == 0)
				numToSplit = 1;

			ValidationRecords = Records.GetRange(Records.Count - numToSplit, numToSplit);
			Records.RemoveRange(Records.Count - numToSplit, numToSplit);
			ValidationTargets = TargetOutputs.GetRange(TargetOutputs.Count - numToSplit, numToSplit);
			TargetOutputs.RemoveRange(TargetOutputs.Count - numToSplit, numToSplit);
		}

		/// <summary>
		/// Puts the validation set back into the training set if you want to shuffle them all together
		/// </summary>
		public void ConcatenateValidationSet()
		{
			Records.AddRange(ValidationRecords);
			ValidationRecords.Clear();
			TargetOutputs.AddRange(ValidationTargets);
			ValidationTargets.Clear();
		}
		#endregion

		#region parseArff Features & Outputs
		//Private methods used for parsing each data record into the respective arrays. Called during constructor
		private List<double> parseFeatures(List<string> rec)
		{
			List<string> record = new List<string>(rec);
			List<double> result = new List<double>();
			if (hasClass)
				record.RemoveAt(record.Count - 1);
			foreach (string val in record)
			{
				//Trim off any leading or trailing quotation marks
				//input.Trim(new char[] { '\'', '\"' });

				//See if it's continuous
				double value = 0.0;
				if (double.TryParse(val, out value))
					result.Add(value);

				//Value map should have it if it's not an unknown entry
				else if (NominalValueMap.ContainsKey(val))
					result.Add(NominalValueMap[val]);

				else //Represent unknown ('?') values as double.MaxValue
					result.Add(double.MaxValue);

			}
			return result;
		}

		private double parseTargetOutput(List<string> rec)
		{
			List<string> record = new List<string>(rec);
			double result = 0.0;
			string val = record.Last();

			//See if it's continuous
			double value = 0.0;
			if (double.TryParse(val, out value))
				result = value;

			//Value map should have it if it's not an unknown entry
			else if (NominalOutputMap.ContainsKey(val))
				result = NominalOutputMap[val];

			else //Represent unknown ('?') values as double.MaxValue
				result = double.MaxValue;

			return result;
		}
		#endregion
	}
}
