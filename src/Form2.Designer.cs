namespace ScreenSaverParticles
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			DensityUnD = new NumericUpDown();
			DensityLbl = new Label();
			OkBtn = new Button();
			ResetBtn = new Button();
			PictureBoxGitHub = new PictureBox();
			toolTipGitHub = new ToolTip(components);
			BackgroundColorLbl = new Label();
			colorDialog1 = new ColorDialog();
			BackgroundColorPB = new PictureBox();
			PointsGB = new GroupBox();
			PointColorPB = new PictureBox();
			PointColorLbl = new Label();
			ChaoticUnD = new NumericUpDown();
			ChaoticLbl = new Label();
			RotateSpeedUnD = new NumericUpDown();
			RotateSpeedLbl = new Label();
			SpeedUnD = new NumericUpDown();
			SpeedLbl = new Label();
			PointRadiusUnD = new NumericUpDown();
			PointRadiusLbl = new Label();
			DrawPointsCB = new CheckBox();
			ConnectionsGB = new GroupBox();
			ConnectionsColorPB = new PictureBox();
			ConnectionsColorLbl = new Label();
			ConnectionsAlphaUnd = new NumericUpDown();
			ConnectionsAlphaLbl = new Label();
			LineWidthUnD = new NumericUpDown();
			LineWidthLbl = new Label();
			ShadingUnD = new NumericUpDown();
			ShadingLbl = new Label();
			DistanceUnD = new NumericUpDown();
			DistanceLbl = new Label();
			DrawConCB = new CheckBox();
			ClockModeCB = new CheckBox();
			ClockSizeUnD = new NumericUpDown();
			ClockSizeLbl = new Label();
			ClockModeGB = new GroupBox();
			((System.ComponentModel.ISupportInitialize)DensityUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxGitHub).BeginInit();
			((System.ComponentModel.ISupportInitialize)BackgroundColorPB).BeginInit();
			PointsGB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PointColorPB).BeginInit();
			((System.ComponentModel.ISupportInitialize)ChaoticUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)RotateSpeedUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)SpeedUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)PointRadiusUnD).BeginInit();
			ConnectionsGB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ConnectionsColorPB).BeginInit();
			((System.ComponentModel.ISupportInitialize)ConnectionsAlphaUnd).BeginInit();
			((System.ComponentModel.ISupportInitialize)LineWidthUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)ShadingUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)DistanceUnD).BeginInit();
			((System.ComponentModel.ISupportInitialize)ClockSizeUnD).BeginInit();
			ClockModeGB.SuspendLayout();
			SuspendLayout();
			// 
			// DensityUnD
			// 
			DensityUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			DensityUnD.Location = new Point(421, 14);
			DensityUnD.Margin = new Padding(4, 3, 4, 3);
			DensityUnD.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
			DensityUnD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			DensityUnD.Name = "DensityUnD";
			DensityUnD.Size = new Size(111, 23);
			DensityUnD.TabIndex = 0;
			DensityUnD.Value = new decimal(new int[] { 10, 0, 0, 0 });
			DensityUnD.ValueChanged += DensityUnD_ValueChanged;
			// 
			// DensityLbl
			// 
			DensityLbl.AutoSize = true;
			DensityLbl.Location = new Point(28, 22);
			DensityLbl.Margin = new Padding(4, 0, 4, 0);
			DensityLbl.Name = "DensityLbl";
			DensityLbl.Size = new Size(81, 15);
			DensityLbl.TabIndex = 1;
			DensityLbl.Text = "Points density";
			// 
			// OkBtn
			// 
			OkBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			OkBtn.Location = new Point(363, 389);
			OkBtn.Margin = new Padding(4, 3, 4, 3);
			OkBtn.Name = "OkBtn";
			OkBtn.Size = new Size(88, 27);
			OkBtn.TabIndex = 5;
			OkBtn.Text = "OK";
			OkBtn.UseVisualStyleBackColor = true;
			OkBtn.Click += OkBtn_Click;
			// 
			// ResetBtn
			// 
			ResetBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			ResetBtn.Location = new Point(99, 389);
			ResetBtn.Margin = new Padding(4, 3, 4, 3);
			ResetBtn.Name = "ResetBtn";
			ResetBtn.Size = new Size(88, 27);
			ResetBtn.TabIndex = 4;
			ResetBtn.Text = "Reset";
			ResetBtn.UseVisualStyleBackColor = true;
			ResetBtn.Click += ResetBtn_Click;
			// 
			// PictureBoxGitHub
			// 
			PictureBoxGitHub.Anchor = AnchorStyles.Bottom;
			PictureBoxGitHub.Cursor = Cursors.Hand;
			PictureBoxGitHub.Image = (Image)resources.GetObject("PictureBoxGitHub.Image");
			PictureBoxGitHub.InitialImage = null;
			PictureBoxGitHub.Location = new Point(259, 389);
			PictureBoxGitHub.Margin = new Padding(4, 3, 4, 3);
			PictureBoxGitHub.Name = "PictureBoxGitHub";
			PictureBoxGitHub.Size = new Size(27, 27);
			PictureBoxGitHub.SizeMode = PictureBoxSizeMode.Zoom;
			PictureBoxGitHub.TabIndex = 4;
			PictureBoxGitHub.TabStop = false;
			toolTipGitHub.SetToolTip(PictureBoxGitHub, "Source Code");
			PictureBoxGitHub.Click += PictureBoxGitHub_Click;
			// 
			// toolTipGitHub
			// 
			toolTipGitHub.AutoPopDelay = 5000;
			toolTipGitHub.InitialDelay = 200;
			toolTipGitHub.ReshowDelay = 100;
			// 
			// BackgroundColorLbl
			// 
			BackgroundColorLbl.AutoSize = true;
			BackgroundColorLbl.Location = new Point(28, 48);
			BackgroundColorLbl.Margin = new Padding(4, 0, 4, 0);
			BackgroundColorLbl.Name = "BackgroundColorLbl";
			BackgroundColorLbl.Size = new Size(101, 15);
			BackgroundColorLbl.TabIndex = 5;
			BackgroundColorLbl.Text = "Background color";
			// 
			// colorDialog1
			// 
			colorDialog1.FullOpen = true;
			// 
			// BackgroundColorPB
			// 
			BackgroundColorPB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BackgroundColorPB.BorderStyle = BorderStyle.FixedSingle;
			BackgroundColorPB.Cursor = Cursors.Hand;
			BackgroundColorPB.Location = new Point(421, 44);
			BackgroundColorPB.Margin = new Padding(4, 3, 4, 3);
			BackgroundColorPB.Name = "BackgroundColorPB";
			BackgroundColorPB.Size = new Size(110, 19);
			BackgroundColorPB.TabIndex = 7;
			BackgroundColorPB.TabStop = false;
			BackgroundColorPB.Click += BackgroundColorPB_Click;
			// 
			// PointsGB
			// 
			PointsGB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			PointsGB.Controls.Add(PointColorPB);
			PointsGB.Controls.Add(PointColorLbl);
			PointsGB.Controls.Add(ChaoticUnD);
			PointsGB.Controls.Add(ChaoticLbl);
			PointsGB.Controls.Add(RotateSpeedUnD);
			PointsGB.Controls.Add(RotateSpeedLbl);
			PointsGB.Controls.Add(SpeedUnD);
			PointsGB.Controls.Add(SpeedLbl);
			PointsGB.Controls.Add(PointRadiusUnD);
			PointsGB.Controls.Add(PointRadiusLbl);
			PointsGB.Controls.Add(DrawPointsCB);
			PointsGB.Location = new Point(14, 167);
			PointsGB.Margin = new Padding(4, 3, 4, 3);
			PointsGB.Name = "PointsGB";
			PointsGB.Padding = new Padding(4, 3, 4, 3);
			PointsGB.Size = new Size(257, 203);
			PointsGB.TabIndex = 2;
			PointsGB.TabStop = false;
			PointsGB.Text = "Points";
			// 
			// PointColorPB
			// 
			PointColorPB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			PointColorPB.BorderStyle = BorderStyle.FixedSingle;
			PointColorPB.Cursor = Cursors.Hand;
			PointColorPB.Location = new Point(144, 168);
			PointColorPB.Margin = new Padding(4, 3, 4, 3);
			PointColorPB.Name = "PointColorPB";
			PointColorPB.Size = new Size(110, 19);
			PointColorPB.TabIndex = 16;
			PointColorPB.TabStop = false;
			PointColorPB.Click += PointColorPB_Click;
			// 
			// PointColorLbl
			// 
			PointColorLbl.AutoSize = true;
			PointColorLbl.Location = new Point(12, 168);
			PointColorLbl.Margin = new Padding(4, 0, 4, 0);
			PointColorLbl.Name = "PointColorLbl";
			PointColorLbl.Size = new Size(36, 15);
			PointColorLbl.TabIndex = 15;
			PointColorLbl.Text = "Color";
			// 
			// ChaoticUnD
			// 
			ChaoticUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ChaoticUnD.Location = new Point(144, 137);
			ChaoticUnD.Margin = new Padding(4, 3, 4, 3);
			ChaoticUnD.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			ChaoticUnD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			ChaoticUnD.Name = "ChaoticUnD";
			ChaoticUnD.Size = new Size(111, 23);
			ChaoticUnD.TabIndex = 4;
			ChaoticUnD.Value = new decimal(new int[] { 10, 0, 0, 0 });
			ChaoticUnD.ValueChanged += ChaoticUnD_ValueChanged;
			// 
			// ChaoticLbl
			// 
			ChaoticLbl.AutoSize = true;
			ChaoticLbl.Location = new Point(12, 140);
			ChaoticLbl.Margin = new Padding(4, 0, 4, 0);
			ChaoticLbl.Name = "ChaoticLbl";
			ChaoticLbl.Size = new Size(48, 15);
			ChaoticLbl.TabIndex = 9;
			ChaoticLbl.Text = "Chaotic";
			// 
			// RotateSpeedUnD
			// 
			RotateSpeedUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			RotateSpeedUnD.DecimalPlaces = 2;
			RotateSpeedUnD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
			RotateSpeedUnD.Location = new Point(144, 107);
			RotateSpeedUnD.Margin = new Padding(4, 3, 4, 3);
			RotateSpeedUnD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
			RotateSpeedUnD.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
			RotateSpeedUnD.Name = "RotateSpeedUnD";
			RotateSpeedUnD.Size = new Size(111, 23);
			RotateSpeedUnD.TabIndex = 3;
			RotateSpeedUnD.Value = new decimal(new int[] { 1, 0, 0, 0 });
			RotateSpeedUnD.ValueChanged += RotateSpeedUnD_ValueChanged;
			// 
			// RotateSpeedLbl
			// 
			RotateSpeedLbl.AutoSize = true;
			RotateSpeedLbl.Location = new Point(12, 110);
			RotateSpeedLbl.Margin = new Padding(4, 0, 4, 0);
			RotateSpeedLbl.Name = "RotateSpeedLbl";
			RotateSpeedLbl.Size = new Size(75, 15);
			RotateSpeedLbl.TabIndex = 13;
			RotateSpeedLbl.Text = "Rotate speed";
			// 
			// SpeedUnD
			// 
			SpeedUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			SpeedUnD.DecimalPlaces = 1;
			SpeedUnD.Location = new Point(144, 77);
			SpeedUnD.Margin = new Padding(4, 3, 4, 3);
			SpeedUnD.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
			SpeedUnD.Minimum = new decimal(new int[] { 5, 0, 0, 131072 });
			SpeedUnD.Name = "SpeedUnD";
			SpeedUnD.Size = new Size(111, 23);
			SpeedUnD.TabIndex = 2;
			SpeedUnD.Value = new decimal(new int[] { 10, 0, 0, 0 });
			SpeedUnD.ValueChanged += SpeedUnD_ValueChanged;
			// 
			// SpeedLbl
			// 
			SpeedLbl.AutoSize = true;
			SpeedLbl.Location = new Point(12, 80);
			SpeedLbl.Margin = new Padding(4, 0, 4, 0);
			SpeedLbl.Name = "SpeedLbl";
			SpeedLbl.Size = new Size(39, 15);
			SpeedLbl.TabIndex = 11;
			SpeedLbl.Text = "Speed";
			// 
			// PointRadiusUnD
			// 
			PointRadiusUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			PointRadiusUnD.Location = new Point(144, 47);
			PointRadiusUnD.Margin = new Padding(4, 3, 4, 3);
			PointRadiusUnD.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
			PointRadiusUnD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			PointRadiusUnD.Name = "PointRadiusUnD";
			PointRadiusUnD.Size = new Size(111, 23);
			PointRadiusUnD.TabIndex = 1;
			PointRadiusUnD.Value = new decimal(new int[] { 10, 0, 0, 0 });
			PointRadiusUnD.ValueChanged += PointRadiusUnD_ValueChanged;
			// 
			// PointRadiusLbl
			// 
			PointRadiusLbl.AutoSize = true;
			PointRadiusLbl.Location = new Point(12, 50);
			PointRadiusLbl.Margin = new Padding(4, 0, 4, 0);
			PointRadiusLbl.Name = "PointRadiusLbl";
			PointRadiusLbl.Size = new Size(42, 15);
			PointRadiusLbl.TabIndex = 9;
			PointRadiusLbl.Text = "Radius";
			// 
			// DrawPointsCB
			// 
			DrawPointsCB.AutoSize = true;
			DrawPointsCB.Location = new Point(15, 22);
			DrawPointsCB.Margin = new Padding(4, 3, 4, 3);
			DrawPointsCB.Name = "DrawPointsCB";
			DrawPointsCB.Size = new Size(89, 19);
			DrawPointsCB.TabIndex = 0;
			DrawPointsCB.Text = "Draw points";
			DrawPointsCB.UseVisualStyleBackColor = true;
			DrawPointsCB.CheckedChanged += DrawPointsCB_CheckedChanged;
			// 
			// ConnectionsGB
			// 
			ConnectionsGB.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			ConnectionsGB.Controls.Add(ConnectionsColorPB);
			ConnectionsGB.Controls.Add(ConnectionsColorLbl);
			ConnectionsGB.Controls.Add(ConnectionsAlphaUnd);
			ConnectionsGB.Controls.Add(ConnectionsAlphaLbl);
			ConnectionsGB.Controls.Add(LineWidthUnD);
			ConnectionsGB.Controls.Add(LineWidthLbl);
			ConnectionsGB.Controls.Add(ShadingUnD);
			ConnectionsGB.Controls.Add(ShadingLbl);
			ConnectionsGB.Controls.Add(DistanceUnD);
			ConnectionsGB.Controls.Add(DistanceLbl);
			ConnectionsGB.Controls.Add(DrawConCB);
			ConnectionsGB.Location = new Point(278, 167);
			ConnectionsGB.Margin = new Padding(4, 3, 4, 3);
			ConnectionsGB.Name = "ConnectionsGB";
			ConnectionsGB.Padding = new Padding(4, 3, 4, 3);
			ConnectionsGB.Size = new Size(257, 203);
			ConnectionsGB.TabIndex = 3;
			ConnectionsGB.TabStop = false;
			ConnectionsGB.Text = "Connections";
			// 
			// ConnectionsColorPB
			// 
			ConnectionsColorPB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ConnectionsColorPB.BorderStyle = BorderStyle.FixedSingle;
			ConnectionsColorPB.Cursor = Cursors.Hand;
			ConnectionsColorPB.Location = new Point(144, 168);
			ConnectionsColorPB.Margin = new Padding(4, 3, 4, 3);
			ConnectionsColorPB.Name = "ConnectionsColorPB";
			ConnectionsColorPB.Size = new Size(110, 19);
			ConnectionsColorPB.TabIndex = 16;
			ConnectionsColorPB.TabStop = false;
			ConnectionsColorPB.Click += ConnectionsColorPB_Click;
			// 
			// ConnectionsColorLbl
			// 
			ConnectionsColorLbl.AutoSize = true;
			ConnectionsColorLbl.Location = new Point(12, 168);
			ConnectionsColorLbl.Margin = new Padding(4, 0, 4, 0);
			ConnectionsColorLbl.Name = "ConnectionsColorLbl";
			ConnectionsColorLbl.Size = new Size(36, 15);
			ConnectionsColorLbl.TabIndex = 15;
			ConnectionsColorLbl.Text = "Color";
			// 
			// ConnectionsAlphaUnd
			// 
			ConnectionsAlphaUnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ConnectionsAlphaUnd.DecimalPlaces = 2;
			ConnectionsAlphaUnd.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
			ConnectionsAlphaUnd.Location = new Point(144, 137);
			ConnectionsAlphaUnd.Margin = new Padding(4, 3, 4, 3);
			ConnectionsAlphaUnd.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
			ConnectionsAlphaUnd.Name = "ConnectionsAlphaUnd";
			ConnectionsAlphaUnd.Size = new Size(111, 23);
			ConnectionsAlphaUnd.TabIndex = 4;
			ConnectionsAlphaUnd.Value = new decimal(new int[] { 1, 0, 0, 0 });
			ConnectionsAlphaUnd.ValueChanged += ConnectionsAlphaUnd_ValueChanged;
			// 
			// ConnectionsAlphaLbl
			// 
			ConnectionsAlphaLbl.AutoSize = true;
			ConnectionsAlphaLbl.Location = new Point(12, 140);
			ConnectionsAlphaLbl.Margin = new Padding(4, 0, 4, 0);
			ConnectionsAlphaLbl.Name = "ConnectionsAlphaLbl";
			ConnectionsAlphaLbl.Size = new Size(68, 15);
			ConnectionsAlphaLbl.TabIndex = 9;
			ConnectionsAlphaLbl.Text = "Color alpha";
			// 
			// LineWidthUnD
			// 
			LineWidthUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LineWidthUnD.Location = new Point(144, 107);
			LineWidthUnD.Margin = new Padding(4, 3, 4, 3);
			LineWidthUnD.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
			LineWidthUnD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			LineWidthUnD.Name = "LineWidthUnD";
			LineWidthUnD.Size = new Size(111, 23);
			LineWidthUnD.TabIndex = 3;
			LineWidthUnD.Value = new decimal(new int[] { 1, 0, 0, 0 });
			LineWidthUnD.ValueChanged += LineWidthUnD_ValueChanged;
			// 
			// LineWidthLbl
			// 
			LineWidthLbl.AutoSize = true;
			LineWidthLbl.Location = new Point(12, 110);
			LineWidthLbl.Margin = new Padding(4, 0, 4, 0);
			LineWidthLbl.Name = "LineWidthLbl";
			LineWidthLbl.Size = new Size(62, 15);
			LineWidthLbl.TabIndex = 13;
			LineWidthLbl.Text = "Line width";
			// 
			// ShadingUnD
			// 
			ShadingUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ShadingUnD.InterceptArrowKeys = false;
			ShadingUnD.Location = new Point(144, 77);
			ShadingUnD.Margin = new Padding(4, 3, 4, 3);
			ShadingUnD.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
			ShadingUnD.Name = "ShadingUnD";
			ShadingUnD.Size = new Size(111, 23);
			ShadingUnD.TabIndex = 2;
			ShadingUnD.Value = new decimal(new int[] { 10, 0, 0, 0 });
			ShadingUnD.ValueChanged += ShadingUnD_ValueChanged;
			// 
			// ShadingLbl
			// 
			ShadingLbl.AutoSize = true;
			ShadingLbl.Location = new Point(12, 80);
			ShadingLbl.Margin = new Padding(4, 0, 4, 0);
			ShadingLbl.Name = "ShadingLbl";
			ShadingLbl.Size = new Size(76, 15);
			ShadingLbl.TabIndex = 11;
			ShadingLbl.Text = "Shading start";
			// 
			// DistanceUnD
			// 
			DistanceUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			DistanceUnD.Location = new Point(144, 47);
			DistanceUnD.Margin = new Padding(4, 3, 4, 3);
			DistanceUnD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
			DistanceUnD.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
			DistanceUnD.Name = "DistanceUnD";
			DistanceUnD.Size = new Size(111, 23);
			DistanceUnD.TabIndex = 1;
			DistanceUnD.Value = new decimal(new int[] { 10, 0, 0, 0 });
			DistanceUnD.ValueChanged += DistanceUnD_ValueChanged;
			// 
			// DistanceLbl
			// 
			DistanceLbl.AutoSize = true;
			DistanceLbl.Location = new Point(12, 50);
			DistanceLbl.Margin = new Padding(4, 0, 4, 0);
			DistanceLbl.Name = "DistanceLbl";
			DistanceLbl.Size = new Size(67, 15);
			DistanceLbl.TabIndex = 9;
			DistanceLbl.Text = "Max length";
			// 
			// DrawConCB
			// 
			DrawConCB.AutoSize = true;
			DrawConCB.Location = new Point(15, 22);
			DrawConCB.Margin = new Padding(4, 3, 4, 3);
			DrawConCB.Name = "DrawConCB";
			DrawConCB.Size = new Size(121, 19);
			DrawConCB.TabIndex = 0;
			DrawConCB.Text = "Draw connections";
			DrawConCB.UseVisualStyleBackColor = true;
			DrawConCB.CheckedChanged += DrawConCB_CheckedChanged;
			// 
			// ClockModeCB
			// 
			ClockModeCB.AutoSize = true;
			ClockModeCB.Location = new Point(15, 22);
			ClockModeCB.Margin = new Padding(4, 3, 4, 3);
			ClockModeCB.Name = "ClockModeCB";
			ClockModeCB.Size = new Size(90, 19);
			ClockModeCB.TabIndex = 0;
			ClockModeCB.Text = "Clock mode";
			ClockModeCB.UseVisualStyleBackColor = true;
			ClockModeCB.CheckedChanged += ClockModeCB_CheckedChanged;
			// 
			// ClockSizeUnD
			// 
			ClockSizeUnD.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			ClockSizeUnD.DecimalPlaces = 2;
			ClockSizeUnD.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
			ClockSizeUnD.Location = new Point(407, 48);
			ClockSizeUnD.Margin = new Padding(4, 3, 4, 3);
			ClockSizeUnD.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
			ClockSizeUnD.Name = "ClockSizeUnD";
			ClockSizeUnD.Size = new Size(111, 23);
			ClockSizeUnD.TabIndex = 1;
			ClockSizeUnD.ValueChanged += ClockSizeUnD_ValueChanged;
			// 
			// ClockSizeLbl
			// 
			ClockSizeLbl.AutoSize = true;
			ClockSizeLbl.Location = new Point(12, 51);
			ClockSizeLbl.Margin = new Padding(4, 0, 4, 0);
			ClockSizeLbl.Name = "ClockSizeLbl";
			ClockSizeLbl.Size = new Size(59, 15);
			ClockSizeLbl.TabIndex = 21;
			ClockSizeLbl.Text = "Clock size";
			// 
			// ClockModeGB
			// 
			ClockModeGB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ClockModeGB.Controls.Add(ClockModeCB);
			ClockModeGB.Controls.Add(ClockSizeUnD);
			ClockModeGB.Controls.Add(ClockSizeLbl);
			ClockModeGB.Location = new Point(14, 73);
			ClockModeGB.Margin = new Padding(4, 3, 4, 3);
			ClockModeGB.Name = "ClockModeGB";
			ClockModeGB.Padding = new Padding(4, 3, 4, 3);
			ClockModeGB.Size = new Size(520, 88);
			ClockModeGB.TabIndex = 1;
			ClockModeGB.TabStop = false;
			ClockModeGB.Text = "Clock mode";
			// 
			// Form2
			// 
			AcceptButton = OkBtn;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(546, 429);
			Controls.Add(ClockModeGB);
			Controls.Add(ConnectionsGB);
			Controls.Add(PointsGB);
			Controls.Add(BackgroundColorPB);
			Controls.Add(BackgroundColorLbl);
			Controls.Add(DensityUnD);
			Controls.Add(DensityLbl);
			Controls.Add(PictureBoxGitHub);
			Controls.Add(ResetBtn);
			Controls.Add(OkBtn);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4, 3, 4, 3);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "Form2";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Screensaver Particles";
			((System.ComponentModel.ISupportInitialize)DensityUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)PictureBoxGitHub).EndInit();
			((System.ComponentModel.ISupportInitialize)BackgroundColorPB).EndInit();
			PointsGB.ResumeLayout(false);
			PointsGB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)PointColorPB).EndInit();
			((System.ComponentModel.ISupportInitialize)ChaoticUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)RotateSpeedUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)SpeedUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)PointRadiusUnD).EndInit();
			ConnectionsGB.ResumeLayout(false);
			ConnectionsGB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ConnectionsColorPB).EndInit();
			((System.ComponentModel.ISupportInitialize)ConnectionsAlphaUnd).EndInit();
			((System.ComponentModel.ISupportInitialize)LineWidthUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)ShadingUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)DistanceUnD).EndInit();
			((System.ComponentModel.ISupportInitialize)ClockSizeUnD).EndInit();
			ClockModeGB.ResumeLayout(false);
			ClockModeGB.PerformLayout();
			ResumeLayout(false);
			PerformLayout();

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
		private System.Windows.Forms.GroupBox ConnectionsGB;
		private System.Windows.Forms.PictureBox ConnectionsColorPB;
		private System.Windows.Forms.Label ConnectionsColorLbl;
		private System.Windows.Forms.NumericUpDown ConnectionsAlphaUnd;
		private System.Windows.Forms.Label ConnectionsAlphaLbl;
		private System.Windows.Forms.NumericUpDown LineWidthUnD;
		private System.Windows.Forms.Label LineWidthLbl;
		private System.Windows.Forms.NumericUpDown ShadingUnD;
		private System.Windows.Forms.Label ShadingLbl;
		private System.Windows.Forms.NumericUpDown DistanceUnD;
		private System.Windows.Forms.Label DistanceLbl;
		private System.Windows.Forms.CheckBox DrawConCB;
		private System.Windows.Forms.CheckBox ClockModeCB;
		private System.Windows.Forms.NumericUpDown ClockSizeUnD;
		private System.Windows.Forms.Label ClockSizeLbl;
		private System.Windows.Forms.GroupBox ClockModeGB;
	}
}