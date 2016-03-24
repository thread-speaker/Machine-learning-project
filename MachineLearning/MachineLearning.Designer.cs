namespace MachineLearning
{
	partial class MachineLearning
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblData = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.btnFileUpload = new System.Windows.Forms.Button();
			this.btnTrain = new System.Windows.Forms.Button();
			this.btnTestAccuracy = new System.Windows.Forms.Button();
			this.lblLearningMethod = new System.Windows.Forms.Label();
			this.ddlLearningMethod = new System.Windows.Forms.ComboBox();
			this.txtConsole = new System.Windows.Forms.RichTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtLearningRate = new System.Windows.Forms.TextBox();
			this.lblLearningRate = new System.Windows.Forms.Label();
			this.txtValidationPercent = new System.Windows.Forms.TextBox();
			this.lblValidationPercent = new System.Windows.Forms.Label();
			this.txtTestPercent = new System.Windows.Forms.TextBox();
			this.lblTestPercent = new System.Windows.Forms.Label();
			this.txtRandomSeed = new System.Windows.Forms.TextBox();
			this.lblRandomSeed = new System.Windows.Forms.Label();
			this.lblNumHiddenNodes = new System.Windows.Forms.Label();
			this.txtNumHidden = new System.Windows.Forms.TextBox();
			this.txtMomentum = new System.Windows.Forms.TextBox();
			this.lblMomentum = new System.Windows.Forms.Label();
			this.BrowseTestBtn = new System.Windows.Forms.Button();
			this.txtTestName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblData
			// 
			this.lblData.AutoSize = true;
			this.lblData.Location = new System.Drawing.Point(11, 422);
			this.lblData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(86, 13);
			this.lblData.TabIndex = 0;
			this.lblData.Text = "Select data file...";
			// 
			// txtFileName
			// 
			this.txtFileName.Enabled = false;
			this.txtFileName.Location = new System.Drawing.Point(99, 418);
			this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(221, 20);
			this.txtFileName.TabIndex = 1;
			// 
			// btnFileUpload
			// 
			this.btnFileUpload.Location = new System.Drawing.Point(334, 413);
			this.btnFileUpload.Margin = new System.Windows.Forms.Padding(2);
			this.btnFileUpload.Name = "btnFileUpload";
			this.btnFileUpload.Size = new System.Drawing.Size(70, 29);
			this.btnFileUpload.TabIndex = 2;
			this.btnFileUpload.Text = "Browse...";
			this.btnFileUpload.UseVisualStyleBackColor = true;
			this.btnFileUpload.Click += new System.EventHandler(this.btnFileUpload_Click);
			// 
			// btnTrain
			// 
			this.btnTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
			this.btnTrain.Location = new System.Drawing.Point(513, 409);
			this.btnTrain.Margin = new System.Windows.Forms.Padding(2);
			this.btnTrain.Name = "btnTrain";
			this.btnTrain.Size = new System.Drawing.Size(92, 29);
			this.btnTrain.TabIndex = 3;
			this.btnTrain.Text = "Train";
			this.btnTrain.UseVisualStyleBackColor = true;
			this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
			// 
			// btnTestAccuracy
			// 
			this.btnTestAccuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
			this.btnTestAccuracy.Location = new System.Drawing.Point(609, 409);
			this.btnTestAccuracy.Margin = new System.Windows.Forms.Padding(2);
			this.btnTestAccuracy.Name = "btnTestAccuracy";
			this.btnTestAccuracy.Size = new System.Drawing.Size(179, 29);
			this.btnTestAccuracy.TabIndex = 4;
			this.btnTestAccuracy.Text = "Test Accuracy";
			this.btnTestAccuracy.UseVisualStyleBackColor = true;
			this.btnTestAccuracy.Click += new System.EventHandler(this.btnTestAccuracy_Click);
			// 
			// lblLearningMethod
			// 
			this.lblLearningMethod.AutoSize = true;
			this.lblLearningMethod.Location = new System.Drawing.Point(14, 41);
			this.lblLearningMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblLearningMethod.Name = "lblLearningMethod";
			this.lblLearningMethod.Size = new System.Drawing.Size(90, 13);
			this.lblLearningMethod.TabIndex = 5;
			this.lblLearningMethod.Text = "Select a learner...";
			// 
			// ddlLearningMethod
			// 
			this.ddlLearningMethod.FormattingEnabled = true;
			this.ddlLearningMethod.Items.AddRange(new object[] {
			"Perceptron",
			"Neural Net",
			"Decision Tree",
			"Instance Based",
			"Clustering"});
			this.ddlLearningMethod.Location = new System.Drawing.Point(106, 36);
			this.ddlLearningMethod.Margin = new System.Windows.Forms.Padding(2);
			this.ddlLearningMethod.Name = "ddlLearningMethod";
			this.ddlLearningMethod.Size = new System.Drawing.Size(201, 21);
			this.ddlLearningMethod.TabIndex = 6;
			this.ddlLearningMethod.SelectedIndexChanged += new System.EventHandler(this.ddlLearningMethod_SelectedIndexChanged);
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.SystemColors.WindowText;
			this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtConsole.ForeColor = System.Drawing.SystemColors.Window;
			this.txtConsole.Location = new System.Drawing.Point(7, 2);
			this.txtConsole.Margin = new System.Windows.Forms.Padding(2);
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtConsole.Size = new System.Drawing.Size(765, 319);
			this.txtConsole.TabIndex = 7;
			this.txtConsole.Text = "";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.WindowText;
			this.panel1.Controls.Add(this.txtConsole);
			this.panel1.Location = new System.Drawing.Point(10, 69);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(779, 331);
			this.panel1.TabIndex = 8;
			// 
			// txtLearningRate
			// 
			this.txtLearningRate.Location = new System.Drawing.Point(135, 8);
			this.txtLearningRate.Margin = new System.Windows.Forms.Padding(2);
			this.txtLearningRate.Name = "txtLearningRate";
			this.txtLearningRate.Size = new System.Drawing.Size(41, 20);
			this.txtLearningRate.TabIndex = 10;
			// 
			// lblLearningRate
			// 
			this.lblLearningRate.AutoSize = true;
			this.lblLearningRate.Location = new System.Drawing.Point(14, 10);
			this.lblLearningRate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblLearningRate.Name = "lblLearningRate";
			this.lblLearningRate.Size = new System.Drawing.Size(120, 13);
			this.lblLearningRate.TabIndex = 9;
			this.lblLearningRate.Text = "Learning Rate (.1 ~ 1)...";
			// 
			// txtValidationPercent
			// 
			this.txtValidationPercent.Location = new System.Drawing.Point(328, 8);
			this.txtValidationPercent.Margin = new System.Windows.Forms.Padding(2);
			this.txtValidationPercent.Name = "txtValidationPercent";
			this.txtValidationPercent.Size = new System.Drawing.Size(41, 20);
			this.txtValidationPercent.TabIndex = 12;
			// 
			// lblValidationPercent
			// 
			this.lblValidationPercent.AutoSize = true;
			this.lblValidationPercent.Location = new System.Drawing.Point(188, 10);
			this.lblValidationPercent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblValidationPercent.Name = "lblValidationPercent";
			this.lblValidationPercent.Size = new System.Drawing.Size(138, 13);
			this.lblValidationPercent.TabIndex = 11;
			this.lblValidationPercent.Text = "Validation Set % (.05 ~ .5)...";
			// 
			// txtTestPercent
			// 
			this.txtTestPercent.Location = new System.Drawing.Point(495, 8);
			this.txtTestPercent.Margin = new System.Windows.Forms.Padding(2);
			this.txtTestPercent.Name = "txtTestPercent";
			this.txtTestPercent.Size = new System.Drawing.Size(41, 20);
			this.txtTestPercent.TabIndex = 14;
			// 
			// lblTestPercent
			// 
			this.lblTestPercent.AutoSize = true;
			this.lblTestPercent.Location = new System.Drawing.Point(381, 10);
			this.lblTestPercent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblTestPercent.Name = "lblTestPercent";
			this.lblTestPercent.Size = new System.Drawing.Size(113, 13);
			this.lblTestPercent.TabIndex = 13;
			this.lblTestPercent.Text = "Test Set % (.05 ~ .5)...";
			// 
			// txtRandomSeed
			// 
			this.txtRandomSeed.Location = new System.Drawing.Point(669, 8);
			this.txtRandomSeed.Margin = new System.Windows.Forms.Padding(2);
			this.txtRandomSeed.Name = "txtRandomSeed";
			this.txtRandomSeed.Size = new System.Drawing.Size(115, 20);
			this.txtRandomSeed.TabIndex = 16;
			// 
			// lblRandomSeed
			// 
			this.lblRandomSeed.AutoSize = true;
			this.lblRandomSeed.Location = new System.Drawing.Point(549, 10);
			this.lblRandomSeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblRandomSeed.Name = "lblRandomSeed";
			this.lblRandomSeed.Size = new System.Drawing.Size(118, 13);
			this.lblRandomSeed.TabIndex = 15;
			this.lblRandomSeed.Text = "Random Seed (0 ~ n)...";
			// 
			// lblNumHiddenNodes
			// 
			this.lblNumHiddenNodes.AutoSize = true;
			this.lblNumHiddenNodes.Location = new System.Drawing.Point(323, 39);
			this.lblNumHiddenNodes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblNumHiddenNodes.Name = "lblNumHiddenNodes";
			this.lblNumHiddenNodes.Size = new System.Drawing.Size(225, 13);
			this.lblNumHiddenNodes.TabIndex = 17;
			this.lblNumHiddenNodes.Text = "Number Hidden Nodes (0 for numFeatures * 2)";
			this.lblNumHiddenNodes.Visible = false;
			// 
			// txtNumHidden
			// 
			this.txtNumHidden.Location = new System.Drawing.Point(557, 37);
			this.txtNumHidden.Margin = new System.Windows.Forms.Padding(2);
			this.txtNumHidden.Name = "txtNumHidden";
			this.txtNumHidden.Size = new System.Drawing.Size(41, 20);
			this.txtNumHidden.TabIndex = 18;
			this.txtNumHidden.Visible = false;
			// 
			// txtMomentum
			// 
			this.txtMomentum.Location = new System.Drawing.Point(686, 37);
			this.txtMomentum.Margin = new System.Windows.Forms.Padding(2);
			this.txtMomentum.Name = "txtMomentum";
			this.txtMomentum.Size = new System.Drawing.Size(85, 20);
			this.txtMomentum.TabIndex = 20;
			this.txtMomentum.Visible = false;
			// 
			// lblMomentum
			// 
			this.lblMomentum.AutoSize = true;
			this.lblMomentum.Location = new System.Drawing.Point(615, 39);
			this.lblMomentum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblMomentum.Name = "lblMomentum";
			this.lblMomentum.Size = new System.Drawing.Size(68, 13);
			this.lblMomentum.TabIndex = 19;
			this.lblMomentum.Text = "Momentum...";
			this.lblMomentum.Visible = false;
			// 
			// BrowseTestBtn
			// 
			this.BrowseTestBtn.Location = new System.Drawing.Point(334, 447);
			this.BrowseTestBtn.Margin = new System.Windows.Forms.Padding(2);
			this.BrowseTestBtn.Name = "BrowseTestBtn";
			this.BrowseTestBtn.Size = new System.Drawing.Size(70, 29);
			this.BrowseTestBtn.TabIndex = 23;
			this.BrowseTestBtn.Text = "Browse...";
			this.BrowseTestBtn.UseVisualStyleBackColor = true;
			this.BrowseTestBtn.Click += new System.EventHandler(this.BrowseTestBtn_Click);
			// 
			// txtTestName
			// 
			this.txtTestName.Enabled = false;
			this.txtTestName.Location = new System.Drawing.Point(99, 452);
			this.txtTestName.Margin = new System.Windows.Forms.Padding(2);
			this.txtTestName.Name = "txtTestName";
			this.txtTestName.Size = new System.Drawing.Size(221, 20);
			this.txtTestName.TabIndex = 22;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 456);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Select test file...";
			// 
			// MachineLearning
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(797, 487);
			this.Controls.Add(this.BrowseTestBtn);
			this.Controls.Add(this.txtTestName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtMomentum);
			this.Controls.Add(this.txtRandomSeed);
			this.Controls.Add(this.lblMomentum);
			this.Controls.Add(this.lblRandomSeed);
			this.Controls.Add(this.txtNumHidden);
			this.Controls.Add(this.txtTestPercent);
			this.Controls.Add(this.lblNumHiddenNodes);
			this.Controls.Add(this.lblTestPercent);
			this.Controls.Add(this.txtValidationPercent);
			this.Controls.Add(this.lblValidationPercent);
			this.Controls.Add(this.txtLearningRate);
			this.Controls.Add(this.lblLearningRate);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ddlLearningMethod);
			this.Controls.Add(this.lblLearningMethod);
			this.Controls.Add(this.btnTestAccuracy);
			this.Controls.Add(this.btnTrain);
			this.Controls.Add(this.btnFileUpload);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.lblData);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MachineLearning";
			this.Text = "Machine Learning";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblData;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button btnFileUpload;
		private System.Windows.Forms.Button btnTrain;
		private System.Windows.Forms.Button btnTestAccuracy;
		private System.Windows.Forms.Label lblLearningMethod;
		private System.Windows.Forms.ComboBox ddlLearningMethod;
		private System.Windows.Forms.RichTextBox txtConsole;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtLearningRate;
		private System.Windows.Forms.Label lblLearningRate;
		private System.Windows.Forms.TextBox txtValidationPercent;
		private System.Windows.Forms.Label lblValidationPercent;
		private System.Windows.Forms.TextBox txtTestPercent;
		private System.Windows.Forms.Label lblTestPercent;
		private System.Windows.Forms.TextBox txtRandomSeed;
		private System.Windows.Forms.Label lblRandomSeed;
		private System.Windows.Forms.Label lblNumHiddenNodes;
		private System.Windows.Forms.TextBox txtNumHidden;
		private System.Windows.Forms.TextBox txtMomentum;
		private System.Windows.Forms.Label lblMomentum;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtTestName;
		private System.Windows.Forms.Button BrowseTestBtn;
	}
}

