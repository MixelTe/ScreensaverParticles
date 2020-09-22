namespace ScreenSaverConections
{
	partial class Form2
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			this.DensityUnD = new System.Windows.Forms.NumericUpDown();
			this.DensityLbl = new System.Windows.Forms.Label();
			this.OkBtn = new System.Windows.Forms.Button();
			this.ResetBtn = new System.Windows.Forms.Button();
			this.PictureBoxGitHub = new System.Windows.Forms.PictureBox();
			this.toolTipGitHub = new System.Windows.Forms.ToolTip(this.components);
			this.BackgroundColorLbl = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.BackgroundColorPB = new System.Windows.Forms.PictureBox();
			this.PointsGB = new System.Windows.Forms.GroupBox();
			this.DrawPointsCB = new System.Windows.Forms.CheckBox();
			this.PointRadiusUnD = new System.Windows.Forms.NumericUpDown();
			this.PointRadiusLbl = new System.Windows.Forms.Label();
			this.SpeedUnD = new System.Windows.Forms.NumericUpDown();
			this.SpeedLbl = new System.Windows.Forms.Label();
			this.RotateSpeedUnD = new System.Windows.Forms.NumericUpDown();
			this.RotateSpeedLbl = new System.Windows.Forms.Label();
			this.ChaoticUnD = new System.Windows.Forms.NumericUpDown();
			this.ChaoticLbl = new System.Windows.Forms.Label();
			this.PointColorLbl = new System.Windows.Forms.Label();
			this.PointColorPB = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.DensityUnD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxGitHub)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BackgroundColorPB)).BeginInit();
			this.PointsGB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PointRadiusUnD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SpeedUnD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RotateSpeedUnD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ChaoticUnD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PointColorPB)).BeginInit();
			this.SuspendLayout();
			// 
			// DensityUnD
			// 
			this.DensityUnD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DensityUnD.Location = new System.Drawing.Point(135, 25);
			this.DensityUnD.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.DensityUnD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DensityUnD.Name = "DensityUnD";
			this.DensityUnD.Size = new System.Drawing.Size(95, 20);
			this.DensityUnD.TabIndex = 2;
			this.DensityUnD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.DensityUnD.ValueChanged += new System.EventHandler(this.DensityUnD_ValueChanged);
			// 
			// DensityLbl
			// 
			this.DensityLbl.AutoSize = true;
			this.DensityLbl.Location = new System.Drawing.Point(22, 27);
			this.DensityLbl.Name = "DensityLbl";
			this.DensityLbl.Size = new System.Drawing.Size(42, 13);
			this.DensityLbl.TabIndex = 1;
			this.DensityLbl.Text = "Density";
			// 
			// OkBtn
			// 
			this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkBtn.Location = new System.Drawing.Point(155, 266);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.Size = new System.Drawing.Size(75, 23);
			this.OkBtn.TabIndex = 2;
			this.OkBtn.Text = "OK";
			this.OkBtn.UseVisualStyleBackColor = true;
			this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
			// 
			// ResetBtn
			// 
			this.ResetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ResetBtn.Location = new System.Drawing.Point(13, 266);
			this.ResetBtn.Name = "ResetBtn";
			this.ResetBtn.Size = new System.Drawing.Size(75, 23);
			this.ResetBtn.TabIndex = 3;
			this.ResetBtn.Text = "Reset";
			this.ResetBtn.UseVisualStyleBackColor = true;
			this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
			// 
			// PictureBoxGitHub
			// 
			this.PictureBoxGitHub.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.PictureBoxGitHub.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBoxGitHub.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxGitHub.Image")));
			this.PictureBoxGitHub.InitialImage = null;
			this.PictureBoxGitHub.Location = new System.Drawing.Point(110, 266);
			this.PictureBoxGitHub.Name = "PictureBoxGitHub";
			this.PictureBoxGitHub.Size = new System.Drawing.Size(23, 23);
			this.PictureBoxGitHub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.PictureBoxGitHub.TabIndex = 4;
			this.PictureBoxGitHub.TabStop = false;
			this.toolTipGitHub.SetToolTip(this.PictureBoxGitHub, "Source Code");
			this.PictureBoxGitHub.Click += new System.EventHandler(this.PictureBoxGitHub_Click);
			// 
			// toolTipGitHub
			// 
			this.toolTipGitHub.AutoPopDelay = 5000;
			this.toolTipGitHub.InitialDelay = 200;
			this.toolTipGitHub.ReshowDelay = 100;
			// 
			// BackgroundColorLbl
			// 
			this.BackgroundColorLbl.AutoSize = true;
			this.BackgroundColorLbl.Location = new System.Drawing.Point(22, 55);
			this.BackgroundColorLbl.Name = "BackgroundColorLbl";
			this.BackgroundColorLbl.Size = new System.Drawing.Size(91, 13);
			this.BackgroundColorLbl.TabIndex = 5;
			this.BackgroundColorLbl.Text = "Background color";
			// 
			// colorDialog1
			// 
			this.colorDialog1.FullOpen = true;
			// 
			// BackgroundColorPB
			// 
			this.BackgroundColorPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BackgroundColorPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundColorPB.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BackgroundColorPB.Location = new System.Drawing.Point(135, 51);
			this.BackgroundColorPB.Name = "BackgroundColorPB";
			this.BackgroundColorPB.Size = new System.Drawing.Size(95, 17);
			this.BackgroundColorPB.TabIndex = 7;
			this.BackgroundColorPB.TabStop = false;
			this.BackgroundColorPB.Click += new System.EventHandler(this.BackgroundColorPB_Click);
			// 
			// PointsGB
			// 
			this.PointsGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PointsGB.Controls.Add(this.PointColorPB);
			this.PointsGB.Controls.Add(this.PointColorLbl);
			this.PointsGB.Controls.Add(this.ChaoticUnD);
			this.PointsGB.Controls.Add(this.ChaoticLbl);
			this.PointsGB.Controls.Add(this.RotateSpeedUnD);
			this.PointsGB.Controls.Add(this.RotateSpeedLbl);
			this.PointsGB.Controls.Add(this.SpeedUnD);
			this.PointsGB.Controls.Add(this.SpeedLbl);
			this.PointsGB.Controls.Add(this.PointRadiusUnD);
			this.PointsGB.Controls.Add(this.PointRadiusLbl);
			this.PointsGB.Controls.Add(this.DrawPointsCB);
			this.PointsGB.Location = new System.Drawing.Point(12, 74);
			this.PointsGB.Name = "PointsGB";
			this.PointsGB.Size = new System.Drawing.Size(220, 176);
			this.PointsGB.TabIndex = 8;
			this.PointsGB.TabStop = false;
			this.PointsGB.Text = "Points";
			// 
			// DrawPointsCB
			// 
			this.DrawPointsCB.AutoSize = true;
			this.DrawPointsCB.Location = new System.Drawing.Point(13, 19);
			this.DrawPointsCB.Name = "DrawPointsCB";
			this.DrawPointsCB.Size = new System.Drawing.Size(82, 17);
			this.DrawPointsCB.TabIndex = 0;
			this.DrawPointsCB.Text = "Draw points";
			this.DrawPointsCB.UseVisualStyleBackColor = true;
			this.DrawPointsCB.CheckedChanged += new System.EventHandler(this.DrawPointsCB_CheckedChanged);
			// 
			// PointRadiusUnD
			// 
			this.PointRadiusUnD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PointRadiusUnD.Location = new System.Drawing.Point(123, 41);
			this.PointRadiusUnD.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.PointRadiusUnD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PointRadiusUnD.Name = "PointRadiusUnD";
			this.PointRadiusUnD.Size = new System.Drawing.Size(95, 20);
			this.PointRadiusUnD.TabIndex = 10;
			this.PointRadiusUnD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.PointRadiusUnD.ValueChanged += new System.EventHandler(this.PointRadiusUnD_ValueChanged);
			// 
			// PointRadiusLbl
			// 
			this.PointRadiusLbl.AutoSize = true;
			this.PointRadiusLbl.Location = new System.Drawing.Point(10, 43);
			this.PointRadiusLbl.Name = "PointRadiusLbl";
			this.PointRadiusLbl.Size = new System.Drawing.Size(40, 13);
			this.PointRadiusLbl.TabIndex = 9;
			this.PointRadiusLbl.Text = "Radius";
			// 
			// SpeedUnD
			// 
			this.SpeedUnD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SpeedUnD.DecimalPlaces = 1;
			this.SpeedUnD.Location = new System.Drawing.Point(123, 67);
			this.SpeedUnD.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.SpeedUnD.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
			this.SpeedUnD.Name = "SpeedUnD";
			this.SpeedUnD.Size = new System.Drawing.Size(95, 20);
			this.SpeedUnD.TabIndex = 12;
			this.SpeedUnD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.SpeedUnD.ValueChanged += new System.EventHandler(this.SpeedUnD_ValueChanged);
			// 
			// SpeedLbl
			// 
			this.SpeedLbl.AutoSize = true;
			this.SpeedLbl.Location = new System.Drawing.Point(10, 69);
			this.SpeedLbl.Name = "SpeedLbl";
			this.SpeedLbl.Size = new System.Drawing.Size(38, 13);
			this.SpeedLbl.TabIndex = 11;
			this.SpeedLbl.Text = "Speed";
			// 
			// RotateSpeedUnD
			// 
			this.RotateSpeedUnD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RotateSpeedUnD.DecimalPlaces = 2;
			this.RotateSpeedUnD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.RotateSpeedUnD.Location = new System.Drawing.Point(123, 93);
			this.RotateSpeedUnD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.RotateSpeedUnD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.RotateSpeedUnD.Name = "RotateSpeedUnD";
			this.RotateSpeedUnD.Size = new System.Drawing.Size(95, 20);
			this.RotateSpeedUnD.TabIndex = 14;
			this.RotateSpeedUnD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.RotateSpeedUnD.ValueChanged += new System.EventHandler(this.RotateSpeedUnD_ValueChanged);
			// 
			// RotateSpeedLbl
			// 
			this.RotateSpeedLbl.AutoSize = true;
			this.RotateSpeedLbl.Location = new System.Drawing.Point(10, 95);
			this.RotateSpeedLbl.Name = "RotateSpeedLbl";
			this.RotateSpeedLbl.Size = new System.Drawing.Size(71, 13);
			this.RotateSpeedLbl.TabIndex = 13;
			this.RotateSpeedLbl.Text = "Rotate speed";
			// 
			// ChaoticUnD
			// 
			this.ChaoticUnD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChaoticUnD.Location = new System.Drawing.Point(123, 119);
			this.ChaoticUnD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.ChaoticUnD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.ChaoticUnD.Name = "ChaoticUnD";
			this.ChaoticUnD.Size = new System.Drawing.Size(95, 20);
			this.ChaoticUnD.TabIndex = 10;
			this.ChaoticUnD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.ChaoticUnD.ValueChanged += new System.EventHandler(this.ChaoticUnD_ValueChanged);
			// 
			// ChaoticLbl
			// 
			this.ChaoticLbl.AutoSize = true;
			this.ChaoticLbl.Location = new System.Drawing.Point(10, 121);
			this.ChaoticLbl.Name = "ChaoticLbl";
			this.ChaoticLbl.Size = new System.Drawing.Size(43, 13);
			this.ChaoticLbl.TabIndex = 9;
			this.ChaoticLbl.Text = "Chaotic";
			// 
			// PointColorLbl
			// 
			this.PointColorLbl.AutoSize = true;
			this.PointColorLbl.Location = new System.Drawing.Point(10, 146);
			this.PointColorLbl.Name = "PointColorLbl";
			this.PointColorLbl.Size = new System.Drawing.Size(31, 13);
			this.PointColorLbl.TabIndex = 15;
			this.PointColorLbl.Text = "Color";
			// 
			// PointColorPB
			// 
			this.PointColorPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PointColorPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PointColorPB.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PointColorPB.Location = new System.Drawing.Point(123, 146);
			this.PointColorPB.Name = "PointColorPB";
			this.PointColorPB.Size = new System.Drawing.Size(95, 17);
			this.PointColorPB.TabIndex = 16;
			this.PointColorPB.TabStop = false;
			this.PointColorPB.Click += new System.EventHandler(this.PointColorPB_Click);
			// 
			// Form2
			// 
			this.AcceptButton = this.OkBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 301);
			this.Controls.Add(this.PointsGB);
			this.Controls.Add(this.BackgroundColorPB);
			this.Controls.Add(this.BackgroundColorLbl);
			this.Controls.Add(this.DensityUnD);
			this.Controls.Add(this.DensityLbl);
			this.Controls.Add(this.PictureBoxGitHub);
			this.Controls.Add(this.ResetBtn);
			this.Controls.Add(this.OkBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Screensaver";
			((System.ComponentModel.ISupportInitialize)(this.DensityUnD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxGitHub)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BackgroundColorPB)).EndInit();
			this.PointsGB.ResumeLayout(false);
			this.PointsGB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PointRadiusUnD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SpeedUnD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RotateSpeedUnD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ChaoticUnD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PointColorPB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.NumericUpDown DensityUnD;
		private System.Windows.Forms.Label DensityLbl;
		private System.Windows.Forms.Button OkBtn;
		private System.Windows.Forms.Button ResetBtn;
		private System.Windows.Forms.PictureBox PictureBoxGitHub;
		private System.Windows.Forms.ToolTip toolTipGitHub;
		private System.Windows.Forms.Label BackgroundColorLbl;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.PictureBox BackgroundColorPB;
		private System.Windows.Forms.GroupBox PointsGB;
		private System.Windows.Forms.CheckBox DrawPointsCB;
		private System.Windows.Forms.NumericUpDown PointRadiusUnD;
		private System.Windows.Forms.Label PointRadiusLbl;
		private System.Windows.Forms.NumericUpDown SpeedUnD;
		private System.Windows.Forms.Label SpeedLbl;
		private System.Windows.Forms.NumericUpDown RotateSpeedUnD;
		private System.Windows.Forms.Label RotateSpeedLbl;
		private System.Windows.Forms.NumericUpDown ChaoticUnD;
		private System.Windows.Forms.Label ChaoticLbl;
		private System.Windows.Forms.PictureBox PointColorPB;
		private System.Windows.Forms.Label PointColorLbl;
	}
}