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
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblData
			// 
			this.lblData.AutoSize = true;
			this.lblData.Location = new System.Drawing.Point(17, 633);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(126, 20);
			this.lblData.TabIndex = 0;
			this.lblData.Text = "Select data file...";
			// 
			// txtFileName
			// 
			this.txtFileName.Enabled = false;
			this.txtFileName.Location = new System.Drawing.Point(149, 627);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(329, 26);
			this.txtFileName.TabIndex = 1;
			// 
			// btnFileUpload
			// 
			this.btnFileUpload.Location = new System.Drawing.Point(501, 619);
			this.btnFileUpload.Name = "btnFileUpload";
			this.btnFileUpload.Size = new System.Drawing.Size(105, 43);
			this.btnFileUpload.TabIndex = 2;
			this.btnFileUpload.Text = "Browse...";
			this.btnFileUpload.UseVisualStyleBackColor = true;
			this.btnFileUpload.Click += new System.EventHandler(this.btnFileUpload_Click);
			// 
			// btnTrain
			// 
			this.btnTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
			this.btnTrain.Location = new System.Drawing.Point(770, 614);
			this.btnTrain.Name = "btnTrain";
			this.btnTrain.Size = new System.Drawing.Size(138, 44);
			this.btnTrain.TabIndex = 3;
			this.btnTrain.Text = "Train";
			this.btnTrain.UseVisualStyleBackColor = true;
			this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
			// 
			// btnTestAccuracy
			// 
			this.btnTestAccuracy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
			this.btnTestAccuracy.Location = new System.Drawing.Point(914, 614);
			this.btnTestAccuracy.Name = "btnTestAccuracy";
			this.btnTestAccuracy.Size = new System.Drawing.Size(269, 44);
			this.btnTestAccuracy.TabIndex = 4;
			this.btnTestAccuracy.Text = "Test Accuracy";
			this.btnTestAccuracy.UseVisualStyleBackColor = true;
			this.btnTestAccuracy.Click += new System.EventHandler(this.btnTestAccuracy_Click);
			// 
			// lblLearningMethod
			// 
			this.lblLearningMethod.AutoSize = true;
			this.lblLearningMethod.Location = new System.Drawing.Point(21, 62);
			this.lblLearningMethod.Name = "lblLearningMethod";
			this.lblLearningMethod.Size = new System.Drawing.Size(132, 20);
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
			this.ddlLearningMethod.Location = new System.Drawing.Point(159, 54);
			this.ddlLearningMethod.Name = "ddlLearningMethod";
			this.ddlLearningMethod.Size = new System.Drawing.Size(300, 28);
			this.ddlLearningMethod.TabIndex = 6;
			this.ddlLearningMethod.SelectedIndexChanged += new System.EventHandler(this.ddlLearningMethod_SelectedIndexChanged);
			// 
			// txtConsole
			// 
			this.txtConsole.BackColor = System.Drawing.SystemColors.WindowText;
			this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtConsole.ForeColor = System.Drawing.SystemColors.Window;
			this.txtConsole.Location = new System.Drawing.Point(11, 3);
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ReadOnly = true;
			this.txtConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.txtConsole.Size = new System.Drawing.Size(1148, 478);
			this.txtConsole.TabIndex = 7;
			this.txtConsole.Text = "";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.WindowText;
			this.panel1.Controls.Add(this.txtConsole);
			this.panel1.Location = new System.Drawing.Point(15, 103);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1168, 496);
			this.panel1.TabIndex = 8;
			// 
			// txtLearningRate
			// 
			this.txtLearningRate.Location = new System.Drawing.Point(202, 12);
			this.txtLearningRate.Name = "txtLearningRate";
			this.txtLearningRate.Size = new System.Drawing.Size(59, 26);
			this.txtLearningRate.TabIndex = 10;
			// 
			// lblLearningRate
			// 
			this.lblLearningRate.AutoSize = true;
			this.lblLearningRate.Location = new System.Drawing.Point(21, 15);
			this.lblLearningRate.Name = "lblLearningRate";
			this.lblLearningRate.Size = new System.Drawing.Size(175, 20);
			this.lblLearningRate.TabIndex = 9;
			this.lblLearningRate.Text = "Learning Rate (.1 ~ 1)...";
			// 
			// txtValidationPercent
			// 
			this.txtValidationPercent.Location = new System.Drawing.Point(492, 12);
			this.txtValidationPercent.Name = "txtValidationPercent";
			this.txtValidationPercent.Size = new System.Drawing.Size(59, 26);
			this.txtValidationPercent.TabIndex = 12;
			// 
			// lblValidationPercent
			// 
			this.lblValidationPercent.AutoSize = true;
			this.lblValidationPercent.Location = new System.Drawing.Point(282, 15);
			this.lblValidationPercent.Name = "lblValidationPercent";
			this.lblValidationPercent.Size = new System.Drawing.Size(204, 20);
			this.lblValidationPercent.TabIndex = 11;
			this.lblValidationPercent.Text = "Validation Set % (.05 ~ .5)...";
			// 
			// txtTestPercent
			// 
			this.txtTestPercent.Location = new System.Drawing.Point(743, 12);
			this.txtTestPercent.Name = "txtTestPercent";
			this.txtTestPercent.Size = new System.Drawing.Size(59, 26);
			this.txtTestPercent.TabIndex = 14;
			// 
			// lblTestPercent
			// 
			this.lblTestPercent.AutoSize = true;
			this.lblTestPercent.Location = new System.Drawing.Point(572, 15);
			this.lblTestPercent.Name = "lblTestPercent";
			this.lblTestPercent.Size = new System.Drawing.Size(165, 20);
			this.lblTestPercent.TabIndex = 13;
			this.lblTestPercent.Text = "Test Set % (.05 ~ .5)...";
			// 
			// txtRandomSeed
			// 
			this.txtRandomSeed.Location = new System.Drawing.Point(1003, 12);
			this.txtRandomSeed.Name = "txtRandomSeed";
			this.txtRandomSeed.Size = new System.Drawing.Size(170, 26);
			this.txtRandomSeed.TabIndex = 16;
			// 
			// lblRandomSeed
			// 
			this.lblRandomSeed.AutoSize = true;
			this.lblRandomSeed.Location = new System.Drawing.Point(824, 15);
			this.lblRandomSeed.Name = "lblRandomSeed";
			this.lblRandomSeed.Size = new System.Drawing.Size(173, 20);
			this.lblRandomSeed.TabIndex = 15;
			this.lblRandomSeed.Text = "Random Seed (0 ~ n)...";
			// 
			// lblNumHiddenNodes
			// 
			this.lblNumHiddenNodes.AutoSize = true;
			this.lblNumHiddenNodes.Location = new System.Drawing.Point(485, 59);
			this.lblNumHiddenNodes.Name = "lblNumHiddenNodes";
			this.lblNumHiddenNodes.Size = new System.Drawing.Size(338, 20);
			this.lblNumHiddenNodes.TabIndex = 17;
			this.lblNumHiddenNodes.Text = "Number Hidden Nodes (0 for numFeatures * 2)";
			this.lblNumHiddenNodes.Visible = false;
			// 
			// txtNumHidden
			// 
			this.txtNumHidden.Location = new System.Drawing.Point(835, 56);
			this.txtNumHidden.Name = "txtNumHidden";
			this.txtNumHidden.Size = new System.Drawing.Size(59, 26);
			this.txtNumHidden.TabIndex = 18;
			this.txtNumHidden.Visible = false;
			// 
			// txtMomentum
			// 
			this.txtMomentum.Location = new System.Drawing.Point(1029, 56);
			this.txtMomentum.Name = "txtMomentum";
			this.txtMomentum.Size = new System.Drawing.Size(125, 26);
			this.txtMomentum.TabIndex = 20;
			this.txtMomentum.Visible = false;
			// 
			// lblMomentum
			// 
			this.lblMomentum.AutoSize = true;
			this.lblMomentum.Location = new System.Drawing.Point(922, 59);
			this.lblMomentum.Name = "lblMomentum";
			this.lblMomentum.Size = new System.Drawing.Size(101, 20);
			this.lblMomentum.TabIndex = 19;
			this.lblMomentum.Text = "Momentum...";
			this.lblMomentum.Visible = false;
			// 
			// MachineLearning
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(1195, 670);
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
	}
}

