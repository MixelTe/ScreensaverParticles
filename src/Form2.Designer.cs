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
			this.SpeedUnD = new System.Windows.Forms.NumericUpDown();
			this.SpeecLbl = new System.Windows.Forms.Label();
			this.OkBtn = new System.Windows.Forms.Button();
			this.ResetBtn = new System.Windows.Forms.Button();
			this.PictureBoxGitHub = new System.Windows.Forms.PictureBox();
			this.toolTipGitHub = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.SpeedUnD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxGitHub)).BeginInit();
			this.SuspendLayout();
			// 
			// SpeedUnD
			// 
			this.SpeedUnD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SpeedUnD.Location = new System.Drawing.Point(135, 35);
			this.SpeedUnD.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.SpeedUnD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SpeedUnD.Name = "SpeedUnD";
			this.SpeedUnD.Size = new System.Drawing.Size(95, 20);
			this.SpeedUnD.TabIndex = 2;
			this.SpeedUnD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.SpeedUnD.ValueChanged += new System.EventHandler(this.MaxLengthUnD_ValueChanged);
			// 
			// SpeecLbl
			// 
			this.SpeecLbl.AutoSize = true;
			this.SpeecLbl.Location = new System.Drawing.Point(22, 37);
			this.SpeecLbl.Name = "SpeecLbl";
			this.SpeecLbl.Size = new System.Drawing.Size(38, 13);
			this.SpeecLbl.TabIndex = 1;
			this.SpeecLbl.Text = "Speed";
			// 
			// OkBtn
			// 
			this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkBtn.Location = new System.Drawing.Point(155, 239);
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
			this.ResetBtn.Location = new System.Drawing.Point(13, 239);
			this.ResetBtn.Name = "ResetBtn";
			this.ResetBtn.Size = new System.Drawing.Size(75, 23);
			this.ResetBtn.TabIndex = 3;
			this.ResetBtn.Text = "Reset";
			this.ResetBtn.UseVisualStyleBackColor = true;
			this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
			// 
			// PictureBoxGitHub
			// 
			this.PictureBoxGitHub.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PictureBoxGitHub.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxGitHub.Image")));
			this.PictureBoxGitHub.InitialImage = null;
			this.PictureBoxGitHub.Location = new System.Drawing.Point(110, 239);
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
			// Form2
			// 
			this.AcceptButton = this.OkBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 274);
			this.Controls.Add(this.SpeedUnD);
			this.Controls.Add(this.SpeecLbl);
			this.Controls.Add(this.PictureBoxGitHub);
			this.Controls.Add(this.ResetBtn);
			this.Controls.Add(this.OkBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Screensaver";
			((System.ComponentModel.ISupportInitialize)(this.SpeedUnD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBoxGitHub)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.NumericUpDown SpeedUnD;
		private System.Windows.Forms.Label SpeecLbl;
		private System.Windows.Forms.Button OkBtn;
		private System.Windows.Forms.Button ResetBtn;
		private System.Windows.Forms.PictureBox PictureBoxGitHub;
		private System.Windows.Forms.ToolTip toolTipGitHub;
	}
}