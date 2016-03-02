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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineLearning
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MachineLearning());
        }
    }
}
