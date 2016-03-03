//@Title: CS478 Machine Learning Projects Shell & Toolkit
//@Author: Curtis Merrell, CS478 Student Winter 2016
//@Version: 1.1
//@DateLastUpdated: February 29, 2016
//@Purpose:
//	Shell functionality: Windows form application with an output console and some learner specific text input fields.
//	Will parse, shuffle, split & concatenate training/test/validation sets given a provided arff data file.
//	Only the shell should be distributed (MachineLearning.cs, LearningMachine.cs, DataSet.cs, Program.cs, App.config)
//	Any specific implementations of LearningMachines should be implemented by the student submitting the project for credit.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineLearning
{
	public partial class MachineLearning : Form
	{
		private string LearnerType = "Baseline";
		private string DataFileName = "";
		private DataSet Data;
		private LearningMachine Learner;
		public MachineLearning()
		{
			InitializeComponent();

			txtConsole.AppendText("Default values entered for Learning Rate, etc.\n"
				+ "\tNOTE: When random seed is set to 0, the learning will be randomized on a truly random seed.\n"
				+ "\tTo test a learner against identical conditions repeatedly, set random seed to any positive non-zero integer");

			txtLearningRate.Text = ".1";
			txtValidationPercent.Text = ".15";
			txtTestPercent.Text = ".25";
			txtRandomSeed.Text = "0";
			txtNumHidden.Text = "0";
			txtMomentum.Text = ".9";
			ddlLearningMethod.SelectedIndex = 0; //Defaults to Perceptron
			LearnerType = ddlLearningMethod.Text;
		}

		private void btnFileUpload_Click(object sender, EventArgs e)
		{
			var fileDialog = new System.Windows.Forms.OpenFileDialog();
			var result = fileDialog.ShowDialog();
			switch (result)
			{
				case System.Windows.Forms.DialogResult.OK:
					var file = fileDialog.FileName;
					txtFileName.Text = file;
					DataFileName = file;

					//Make sure the arff file is valid. Calling the DataSet constructor will make the whole thing to validate it.
					//This wasteful, because we remake the DataSet on each Train(). (To undo any shuffling from previous trains)
					//If you know your data file is valid and don't want to waste time validating it, comment out the next several lines
					Data = new DataSet(file);
					if (Data.IsValid)
					{
						txtConsole.AppendText("\n\nDataset successfully validated.");
						txtConsole.ScrollToCaret();
					}
					else
					{
						txtConsole.AppendText("\n\nAn unknown error occured trying to load the data set.");
						txtConsole.ScrollToCaret();
					}
					//End file validation

					break;
				case System.Windows.Forms.DialogResult.Cancel:
				default:
					txtFileName.Text = null;
					DataFileName = null;
					Data = null;
					txtConsole.AppendText("\n\nAn unknown error occured trying to load the data set.");
					txtConsole.ScrollToCaret();
					break;
			}
		}

		private void ddlLearningMethod_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox ddlLearningMethod = sender as ComboBox;
			LearnerType = ddlLearningMethod.Text;

			// Hide/show all learnerType-specific fields
			lblNumHiddenNodes.Visible = false;
			txtNumHidden.Visible = false;
			lblMomentum.Visible = false;
			txtMomentum.Visible = false;

			if (LearnerType == "Neural Net")
			{ // Neural Net Specific Fields
				lblNumHiddenNodes.Visible = true;
				txtNumHidden.Visible = true;
				lblMomentum.Visible = true;
				txtMomentum.Visible = true;
			}

			if (LearnerType == "Decision Tree")
			{
				txtConsole.AppendText("\n\nLearning method set to " + ddlLearningMethod.Text);
				txtConsole.ScrollToCaret();
			}
			else
			{
				txtConsole.AppendText("\n\nLearning method not yet implemented");
				txtConsole.ScrollToCaret();
			}
		}

		private void btnTrain_Click(object sender, EventArgs e)
		{
			if (DataFileName == null)
			{
				txtConsole.AppendText("\n\nInvalid data file");
				return;
			}
			else
			{
				//Recreate the Data Set so it hasn't been altered by sorts during the previous train.
				Data = new DataSet(DataFileName);
				if (!Data.IsValid)
					return;
			}

			txtConsole.AppendText("\n\n\n\nCreating new " + LearnerType);
			//Once implemented, instantiate learners here.
			if (LearnerType == "")
				Learner = null;
			else if (LearnerType == "Decision Tree")
			{
				Learner = new DecisionTree(Data);
			}
			else
				return;

			SetLearnerValues();

			DateTime start = DateTime.Now;
			txtConsole.AppendText("\nTraining beginning at " + DateTime.Now.TimeOfDay);
			Learner.Train();
			txtConsole.AppendText("\nTraining completed at " + DateTime.Now.TimeOfDay);
			txtConsole.AppendText("\nTotal time spent training: " + (DateTime.Now - start).Milliseconds + " milliseconds");
			txtConsole.AppendText(Learner.LearnerOutputs);
			txtConsole.ScrollToCaret();
		}

		private void SetLearnerValues()
		{
			//Generic settings for all learner types
			double LearningRate = 0.1; //Default value. Will be overwritten by user input
			double.TryParse(txtLearningRate.Text, out LearningRate);

			double ValidationPercent = 0.15; //Default value. Will be overwritten by user input
			double.TryParse(txtValidationPercent.Text, out ValidationPercent);

			double TestPercent = 0.25; //Default value. Will be overwritten by user input
			double.TryParse(txtTestPercent.Text, out TestPercent);

			int RandomSeed = 128; //Default value. Will be overwritten by user input
			int.TryParse(txtRandomSeed.Text, out RandomSeed);
			Random Rand;
			if (RandomSeed == 0)
			{
				Rand = new Random();
				txtConsole.AppendText("\nRandom Seed set to zero. Running tests using a new random seed");
				txtConsole.ScrollToCaret();
			}
			else
				Rand = new Random(RandomSeed);

			Learner.LearningRate = LearningRate;
			Learner.ValidationSetPercentage = ValidationPercent;
			Learner.TestSetPercentage = TestPercent;
			Learner.Rand = Rand;

			// Neural Net Specific settings
			if (LearnerType == "Neural Net")
			{
				int NumHiddenNodes = 10;
				int.TryParse(txtNumHidden.Text, out NumHiddenNodes);

				double Momentum = 0;
				if (!string.IsNullOrEmpty(txtMomentum.Text))
					double.TryParse(txtMomentum.Text, out Momentum);

				Learner.NumHiddenNodes = NumHiddenNodes;
				Learner.Momentum = Momentum;
			}

			//Learner initialization once all its necessary settings are in place
			Learner.Initialize();
		}

		private void btnTestAccuracy_Click(object sender, EventArgs e)
		{
			txtConsole.AppendText("\nMeasuring learner's accuracy against a reserved test set taken from the input data...");
			double Accuracy = Learner.MeasureAccuracy("Test") * 100;
			txtConsole.AppendText("\nLearner's measured accuracy: " + Accuracy);
			txtConsole.AppendText("\nTotal training epochs completed: " + Learner.TotalEpochs);
            txtConsole.AppendText(Learner.LearnerOutputs);
            txtConsole.ScrollToCaret();
		}
	}
}
