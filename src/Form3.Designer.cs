namespace ScreenSaverParticles
{
	partial class Form3
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
			OKbutton = new Button();
			Cancelbutton = new Button();
			pictureBox1 = new PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// OKbutton
			// 
			OKbutton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			OKbutton.Location = new Point(230, 238);
			OKbutton.Margin = new Padding(4, 3, 4, 3);
			OKbutton.Name = "OKbutton";
			OKbutton.Size = new Size(88, 27);
			OKbutton.TabIndex = 0;
			OKbutton.Text = "OK";
			OKbutton.UseVisualStyleBackColor = true;
			OKbutton.Click += OKbutton_Click;
			// 
			// Cancelbutton
			// 
			Cancelbutton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			Cancelbutton.DialogResult = DialogResult.Cancel;
			Cancelbutton.Location = new Point(14, 238);
			Cancelbutton.Margin = new Padding(4, 3, 4, 3);
			Cancelbutton.Name = "Cancelbutton";
			Cancelbutton.Size = new Size(88, 27);
			Cancelbutton.TabIndex = 1;
			Cancelbutton.Text = "Cancel";
			Cancelbutton.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			pictureBox1.BorderStyle = BorderStyle.FixedSingle;
			pictureBox1.Location = new Point(15, 15);
			pictureBox1.Margin = new Padding(4, 3, 4, 3);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(302, 196);
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			pictureBox1.MouseDown += PictureBox1_MouseDown;
			pictureBox1.MouseMove += PictureBox1_MouseMove;
			pictureBox1.MouseUp += PictureBox1_MouseUp;
			// 
			// Form3
			// 
			AcceptButton = OKbutton;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = Cancelbutton;
			ClientSize = new Size(331, 278);
			Controls.Add(pictureBox1);
			Controls.Add(Cancelbutton);
			Controls.Add(OKbutton);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4, 3, 4, 3);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "Form3";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Screensaver: points color";
			Load += Form3_Load;
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button OKbutton;
		private System.Windows.Forms.Button Cancelbutton;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}